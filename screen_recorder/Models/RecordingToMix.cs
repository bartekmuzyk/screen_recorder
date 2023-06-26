using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace screen_recorder.Models
{
    public record RecordingToMix(
        DateTime Date,
        int RecordingNumber,
        string GameName,
        string Identifier,
        RecordingInfo CapAudioRecording,
        RecordingInfo CapMainRecording
    );
}
