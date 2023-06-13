using NAudio.Wave;
using screen_recorder.Recording.Audio.Wasapi;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace screen_recorder.Recording.Audio.WinCaptureAudio
{
    internal class MixHandler
    {
        public ConcurrentQueue<byte[]> Buffer { get; set; } = new();
    }
}
