using NAudio.Wave;
using screen_recorder.AudioCapture;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace screen_recorder
{
    internal class ThreadedProcessAudioRecorder
    {
        private readonly Thread recordingThread;

        private readonly ConcurrentQueue<string> opQueue = new();

        public ThreadedProcessAudioRecorder(Process process, string savePath)
        {
            recordingThread = new Thread(() =>
            {
                using var capturingService = new ProcessAudioCaptureService(process.Id);
                using var writer = new WaveFileWriter(savePath, capturingService.Format);

                capturingService.Record(args =>
                {
                    writer.Write(args.Buffer, 0, args.BytesRecorded);

                    opQueue.TryDequeue(out string? op);
                    return op == "stop";
                });
            })
            {
                IsBackground = true
            };
        }

        public void Start()
        {
            recordingThread.Start();
        }

        public void Stop()
        {
            opQueue.Enqueue("stop");
            recordingThread.Join();
        }
    }
}
