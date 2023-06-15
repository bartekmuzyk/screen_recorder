using NAudio.Wave;
using screen_recorder.AudioCapture;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreadState = System.Threading.ThreadState;

namespace screen_recorder
{
    internal class ThreadedProcessRecorder
    {
        private readonly Thread recordingThread;

        private readonly ConcurrentQueue<string> queue = new();

        public ThreadedProcessRecorder(Process process, string savePath)
        {
            recordingThread = new Thread(() =>
            {
                using var capturingService = new CapturingService(process.Id);
                using var writer = new WaveFileWriter(savePath, capturingService.Format);

                capturingService.Record(args =>
                {
                    writer.Write(args.Buffer, 0, args.BytesRecorded);

                    var success = queue.TryDequeue(out string? op);
                    return op == "stop";
                });
            });
            recordingThread.IsBackground = true;
        }

        public void Start()
        {
            recordingThread.Start();
        }

        public void Stop()
        {
            queue.Enqueue("stop");
            recordingThread.Join();
        }
    }
}
