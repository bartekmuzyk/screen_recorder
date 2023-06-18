using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace screen_recorder.Models
{
    public enum RecordingType
    {
        CapAudio,
        CapMain
    }

    public record RecordingInfo(
        DateTime Date,
        int RecordingNumber,
        string GameName,
        RecordingType Type,
        string Path
    );
}
