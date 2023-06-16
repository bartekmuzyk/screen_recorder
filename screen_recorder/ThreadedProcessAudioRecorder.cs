using NAudio.Wave;
using screen_recorder.AudioCapture;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace screen_recorder
{
    internal class ThreadedProcessAudioRecorder
    {
        private readonly Thread recordingThread;

        private readonly ConcurrentQueue<string> opQueue = new();

        private readonly AutoResetEvent waitHandle = new(false);

        private bool successfulInit = false;

        public ThreadedProcessAudioRecorder(Process process, string savePath)
        {
            recordingThread = new Thread(() =>
            {
                Debug.WriteLine("2.1");
                using var capturingService = new ProcessAudioCaptureService(process.Id);
                Debug.WriteLine("2.2");
                successfulInit = capturingService.SuccessfulInit;
                Debug.WriteLine("2.3");
                waitHandle.Set();
                Debug.WriteLine("2.4");

                if (!successfulInit)
                {
                    Debug.WriteLine("2.5");
                    return;
                }
                
                Debug.WriteLine("2.6");

                using var writer = new WaveFileWriter(savePath, capturingService.Format);

                capturingService.Record(
                    args =>
                    {
                        writer.Write(args.Buffer, 0, args.BytesRecorded);

                        opQueue.TryDequeue(out string? op);
                        return op == "stop";
                    }
                );
            })
            {
                IsBackground = true
            };
        }

        public bool Start()
        {
            Debug.WriteLine("1.1");
            recordingThread.Start();
            Debug.WriteLine("1.2");
            waitHandle.WaitOne();
            Debug.WriteLine("1.3");

            return successfulInit;
        }

        public void Stop()
        {
            opQueue.Enqueue("stop");
            recordingThread.Join();
        }
    }
}
