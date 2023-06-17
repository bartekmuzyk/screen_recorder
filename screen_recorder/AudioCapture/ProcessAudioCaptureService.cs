using NAudio.CoreAudioApi;
using NAudio.CoreAudioApi.Interfaces;
using NAudio.Wave;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace screen_recorder.AudioCapture
{
    internal class ProcessAudioCaptureService : IDisposable
    {
        public static readonly WaveFormat DefaultFormat = new(rate: 44100, bits: 16, channels: 2);

        private readonly WaveFormat waveFormat;

        public WaveFormat Format => waveFormat;

        private readonly AudioClient client;

        private readonly int initialBufferSize = 0;

        public bool SuccessfulInit { get; } = false;

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

        public ProcessAudioCaptureService(int processId)
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
                5 * 10_000_000,
                0,
                waveFormat,
                Guid.Empty
            );
            client.Start();

            var tries = 0;
            while (initialBufferSize <= 0)
            {
                if (tries > 20)
                {
                    break;
                }

                initialBufferSize = client.BufferSize;
                Task.Delay(100).Wait();
                tries++;
            }

            SuccessfulInit = initialBufferSize > 0;
        }

        void IDisposable.Dispose()
        {
            client.Dispose();
        }

        public void Record(Func<WaveInEventArgs, bool> onDataAvailable)
        {
            var bytesPerFrame = waveFormat.Channels * waveFormat.BitsPerSample / 8;
            var recordBuffer = new byte[initialBufferSize * bytesPerFrame];

            int millisecondsTimeout = (int)((long)(10000000.0 * initialBufferSize / waveFormat.SampleRate) / 10000 / 2);
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

                        if (num2 > recordBuffer.Length)
                        {
                            recordBuffer = new byte[num2];
                        }

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

                    end = onDataAvailable(new WaveInEventArgs(recordBuffer, num));

                    continue;
                }

                break;
            }
        }
    }
}
