using CoreAudioApi.Interfaces;
using NAudio.CoreAudioApi;
using NAudio.CoreAudioApi.Interfaces;
using NAudio.Wasapi.CoreAudioApi.Interfaces;
using NAudio.Wave;
using screen_recorder.AudioCapture.Enums;
using screen_recorder.AudioCapture.Structs;
using screen_recorder.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using IAudioClient = NAudio.CoreAudioApi.Interfaces.IAudioClient;

namespace screen_recorder.AudioCapture
{
    public enum HelperEvents : uint
    {
        PacketReady,
        Shutdown,
        Count
    }

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("94ea2b94-e9cc-49e0-c0ff-ee64ca8f5b90")]
    interface IAgileObject
    {

    }

    [StructLayout(LayoutKind.Sequential)]
    internal class CompletionHandler : IActivateAudioInterfaceCompletionHandler, IAgileObject
    {
        public IAudioClient client;

        public int hr = HRESULT.E_FAIL;
        public IntPtr eventFinished;

        public CompletionHandler()
        {
            eventFinished = Kernel32.CreateEvent(IntPtr.Zero, false, false, null);
        }

        public void ActivateCompleted(IActivateAudioInterfaceAsyncOperation operation)
        {
            operation.GetActivateResult(out hr, out var obj);
            client = (IAudioClient)obj;
            Kernel32.SetEvent(eventFinished);
        }
    }

    internal class CapturingService
    {
        private const int REFTIMES_PER_SEC = 10_000_000;

        private const int REFTIMES_PER_MILLISEC = 10_000;

        public static readonly WaveFormat Format = new(rate: 44100, bits: 16, channels: 2);

        private IAudioClient _client;

        private IAudioCaptureClient _captureClient;

        private IntPtr[] _events = new IntPtr[(int)HelperEvents.Count];

        public CapturingService(int processId)
        {
            for (int i = 0; i < _events.Length; i++)
                _events[i] = Kernel32.CreateEvent(IntPtr.Zero, false, false, null);

            Initialize(processId);
        }

        private void Initialize(int processId)
        {
            int result;
            Guid guid = Guid.Empty;

            var _params = GetParams((uint)processId);
            var propvariant = GetPropvariant(_params, out var paramsPtr);

            CompletionHandler completionHandler = new CompletionHandler();
            result = Mmdevapi.ActivateAudioInterfaceAsync(
                Mmdevapi.VIRTUAL_AUDIO_DEVICE_PROCESS_LOOPBACK,
                typeof(IAudioClient).GUID,
                propvariant,
                completionHandler,
                out _
            );
            Marshal.ThrowExceptionForHR(result);

            Kernel32.WaitForSingleObject(completionHandler.eventFinished, uint.MaxValue);
            Marshal.ThrowExceptionForHR(completionHandler.hr);

            _client = completionHandler.client;
            result = _client.Initialize(
                AudioClientShareMode.Shared,
                AudioClientStreamFlags.Loopback | AudioClientStreamFlags.EventCallback,
                5 * 10000000,
                0,
                Format,
                ref guid
            );

            Marshal.ThrowExceptionForHR(result);

            result = _client.SetEventHandle(_events[0]);
            Marshal.ThrowExceptionForHR(result);

            //for propvariant
            Marshal.FreeHGlobal(paramsPtr);
            Marshal.FreeHGlobal(propvariant);

            result = _client.GetService(typeof(IAudioCaptureClient).GUID, out object obj);
            Marshal.ThrowExceptionForHR(result);
            _captureClient = obj as IAudioCaptureClient;
        }

        public void Record(Func<byte[]?, bool> onDataAvailable)
        {
            int result;

            result = _client.GetBufferSize(out uint bufferFrameCount);
            Marshal.ThrowExceptionForHR(result);

            var actualBufferDuration = (double)REFTIMES_PER_SEC * bufferFrameCount / Format.SampleRate;
            
            result = _client.Start();
            Marshal.ThrowExceptionForHR(result);

            var end = false;

            while (!end)
            {
                Task.Delay((int)Math.Round(actualBufferDuration / REFTIMES_PER_MILLISEC / 2)).GetAwaiter().GetResult();

                uint packetLength;

                result = _captureClient.GetNextPacketSize(out packetLength);
                Marshal.ThrowExceptionForHR(result);

                while (packetLength != 0)
                {
                    result = _captureClient.GetBuffer(
                        out IntPtr dataPointer,
                        out uint numFramesAvailable,
                        out AudioClientBufferFlags flags,
                        out _,
                        out _
                    );
                    Marshal.ThrowExceptionForHR(result);

                    byte[]? data = new byte[numFramesAvailable];
                    Marshal.Copy(dataPointer, data, 0, (int)numFramesAvailable);

                    if (flags.HasFlag(AudioClientBufferFlags.Silent))
                    {
                        data = null;
                    }

                    end = onDataAvailable(data);

                    result = _captureClient.ReleaseBuffer(numFramesAvailable);
                    Marshal.ThrowExceptionForHR(result);

                    result = _captureClient.GetNextPacketSize(out packetLength);
                    Marshal.ThrowExceptionForHR(result);
                }
            }
        }

        private AUDIOCLIENT_ACTIVATION_PARAMS GetParams(uint processId)
        {
            var mode = PROCESS_LOOPBACK_MODE.PROCESS_LOOPBACK_MODE_INCLUDE_TARGET_PROCESS_TREE;

            return new AUDIOCLIENT_ACTIVATION_PARAMS()
            {
                ActivationType = AUDIOCLIENT_ACTIVATION_TYPE.AUDIOCLIENT_ACTIVATION_TYPE_PROCESS_LOOPBACK,
                ProcessLoopbackParams = new AUDIOCLIENT_PROCESS_LOOPBACK_PARAMS()
                {
                    TargetProcessId = processId,
                    ProcessLoopbackMode = mode
                }
            };
        }

        private IntPtr GetPropvariant(AUDIOCLIENT_ACTIVATION_PARAMS _params, out IntPtr paramsPtr)
        {
            int size = Marshal.SizeOf<AUDIOCLIENT_ACTIVATION_PARAMS>();
            int propSize = Marshal.SizeOf<PROPVARIANT>();
            IntPtr propPtr;
            PROPVARIANT prop;

            paramsPtr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(_params, paramsPtr, false);
            prop = new PROPVARIANT()
            {
                inner = new tag_inner_PROPVARIANT()
                {
                    vt = (ushort)VarEnum.VT_BLOB,
                    blob = new BLOB()
                    {
                        cbSize = (ulong)size,
                        pBlobData = paramsPtr
                    }
                }
            };
            propPtr = Marshal.AllocHGlobal(propSize);
            Marshal.StructureToPtr(prop, propPtr, false);

            return propPtr;
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    internal struct PROPVARIANT
    {
        [FieldOffset(0)]
        public tag_inner_PROPVARIANT inner;
        [FieldOffset(0)]
        public decimal decVal;
    }

    [StructLayout(LayoutKind.Explicit)]
    internal struct tag_inner_PROPVARIANT
    {
        [FieldOffset(0)]
        public ushort vt;
        [FieldOffset(2)]
        public ushort wReserved1;
        [FieldOffset(4)]
        public ushort wReserved2;
        [FieldOffset(6)]
        public ushort wReserved3;
        [FieldOffset(8)]
        public BLOB blob;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct BLOB
    {
        public ulong cbSize;
        public IntPtr pBlobData;
    }
}
