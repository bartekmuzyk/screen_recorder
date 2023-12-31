﻿using NAudio.Wave;
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

        private readonly AutoResetEvent successfulInitWaitHandle = new(false);

        private readonly AutoResetEvent recordingBufferInitWaitHandle = new(false);

        private bool successfulInit = false;

        public ThreadedProcessAudioRecorder(Process process, string savePath)
        {
            recordingThread = new Thread(() =>
            {
                using var capturingService = new ProcessAudioCaptureService(process.Id);
                successfulInit = capturingService.SuccessfulInit;
                successfulInitWaitHandle.Set();

                if (!successfulInit)
                {
                    return;
                }

                using var writer = new WaveFileWriter(savePath, capturingService.Format);

                capturingService.Record(
                    args =>
                    {
                        writer.Write(args.Buffer, 0, args.BytesRecorded);

                        opQueue.TryDequeue(out string? op);
                        return op == "stop";
                    },
                    recordingBufferInitWaitHandle
                );
            })
            {
                IsBackground = true
            };
        }

        public bool Start()
        {
            recordingThread.Start();
            successfulInitWaitHandle.WaitOne();

            if (!successfulInit)
            {
                return false;
            }

            recordingBufferInitWaitHandle.WaitOne();

            return true;
        }

        public void Stop()
        {
            opQueue.Enqueue("stop");
            recordingThread.Join();
        }
    }
}
