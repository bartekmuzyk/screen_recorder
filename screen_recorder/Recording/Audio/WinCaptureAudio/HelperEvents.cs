using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace screen_recorder.Recording.Audio.WinCaptureAudio
{
    public enum HelperEvents:uint
    {
        PacketReady,
        Shutdown,
        Count
    }
}
