﻿using NAudio.CoreAudioApi;
using NAudio.CoreAudioApi.Interfaces;
using NAudio.Wave;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace screen_recorder.AudioCapture
{
    [ComImport]
    [Guid("5B0D3235-4DBA-4D44-865E-8F1D0E4FD04D")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    unsafe interface IMemoryBufferByteAccess
    {
        void GetBuffer(out byte* buffer, out uint capacity);
    }

    internal class CapturingService : IDisposable
    {
        public static readonly WaveFormat DefaultFormat = new(rate: 44100, bits: 16, channels: 2);

        private WaveFormat waveFormat;

        public WaveFormat Format => waveFormat;

        private AudioClient client;

        private static MMDevice? DefaultMMDevice //Speaker
        {
            get
            {
                try
                {
                    return WasapiLoopbackCapture.GetDefaultLoopbackCaptureDevice();
                }
                catch
                {
                    Debug.WriteLine("[Error] Can't get DefaultMMDevice");
                    return null;
                }
            }
        }
        private static WaveFormat? DeviceMixFormat => DefaultMMDevice?.AudioClient?.MixFormat;

        public CapturingService(int processId)
        {
            Initialize(processId);
        }

        void IDisposable.Dispose()
        {
            client.Dispose();
        }

        private void Initialize(int processId)
        {
            var completionHandler = new ActivateAudioInterfaceCompletionHandler<IAudioClient>();
            var @params = new ActivationParameters()
            {
                Type = ActivationType.ProcessLoopback,
                ProcessLoopbackParams = new() { TargetProcessId = processId, ProcessLoopbackMode = LoopbackMode.IncludeProcessTree }
            };
            var propVariant = new PropVariant() { vt = (short)VarEnum.VT_BLOB };

            var size = Marshal.SizeOf(@params);
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(@params, ptr, false);

            propVariant.blobVal.Data = ptr;
            propVariant.blobVal.Length = size;

            Marshal.ThrowExceptionForHR(
                Mmdevapi.ActivateAudioInterfaceAsync(
                    Mmdevapi.VIRTUAL_AUDIO_DEVICE_PROCESS_LOOPBACK,
                    typeof(IAudioClient).GUID,
                    ref propVariant,
                    completionHandler,
                    out var resultHandler
                )
            );
            completionHandler.WaitForCompletion();
            Marshal.ThrowExceptionForHR(resultHandler.GetActivateResult(out _, out var result));
            
            client = new((IAudioClient)result);
            waveFormat = DeviceMixFormat ?? DefaultFormat;
            client.Initialize(
                AudioClientShareMode.Shared,
                AudioClientStreamFlags.Loopback | AudioClientStreamFlags.EventCallback,
                10_000L * 100,
                0,
                waveFormat,
                Guid.Empty
            );
        }

        public void Record(Func<WaveInEventArgs, bool> onDataAvailable)
        {
            client.Start();

            var bytesPerFrame = waveFormat.Channels * waveFormat.BitsPerSample / 8;
            int bufferSize = client.BufferSize;
            var recordBuffer = new byte[bufferSize * bytesPerFrame];
            int millisecondsTimeout = (int)((long)(10000000.0 * bufferSize / waveFormat.SampleRate) / 10000 / 2);
            AudioCaptureClient audioCaptureClient = client.AudioCaptureClient;

            bool end = false;

            while (!end)
            {
                Thread.Sleep(millisecondsTimeout);

                if (!end)
                {
                    int nextPacketSize = audioCaptureClient.GetNextPacketSize();
                    int num = 0;
                    while (nextPacketSize != 0)
                    {
                        IntPtr buffer = audioCaptureClient.GetBuffer(out int numFramesToRead, out AudioClientBufferFlags bufferFlags);
                        int num2 = numFramesToRead * bytesPerFrame;
                        if (Math.Max(0, recordBuffer.Length - num) < num2 && num > 0)
                        {
                            onDataAvailable(new WaveInEventArgs(recordBuffer, num));
                            num = 0;
                        }

                        if ((bufferFlags & AudioClientBufferFlags.Silent) != AudioClientBufferFlags.Silent)
                        {
                            Marshal.Copy(buffer, recordBuffer, num, num2);
                        }
                        else
                        {
                            Array.Clear(recordBuffer, num, num2);
                        }

                        num += num2;
                        audioCaptureClient.ReleaseBuffer(numFramesToRead);
                        nextPacketSize = audioCaptureClient.GetNextPacketSize();
                    }

                    onDataAvailable.Invoke(new WaveInEventArgs(recordBuffer, num));

                    continue;
                }

                break;
            }
        }
    }
}
