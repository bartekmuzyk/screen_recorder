using NAudio.Wave;
using screen_recorder.AudioCapture;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace screen_recorder
{
    internal class ThreadedRecorder
    {
        private readonly List<Thread> recordingThreads = new();

        private bool endFlag = false;

        public void CreateProcessRecordingThread(Process process, string savePath)
        {
            var thread = new Thread(() =>
            {
                using var capturingService = new CapturingService(process.Id);
                using var writer = new WaveFileWriter(savePath, capturingService.Format);

                capturingService.Record(args =>
                {
                    writer.Write(args.Buffer, 0, args.BytesRecorded);

                    return !endFlag;
                });
            });

            recordingThreads.Add(thread);
        }

        public void StartAll()
        {
            foreach (var thread in recordingThreads)
            {
                thread.Start();
            }
        }

        public void StopAll()
        {
            endFlag = true;

            foreach (var thread in recordingThreads)
            {
                thread.Join();
            }

            recordingThreads.Clear();
        }
    }
}
