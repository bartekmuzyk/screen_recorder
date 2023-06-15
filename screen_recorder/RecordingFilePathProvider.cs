using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace screen_recorder
{
    internal class RecordingFilePathProvider
    {
        // {date}-{recordingNumber}_{appName}_{type}_{identifier}.{format}
        private const string FILE_NAME_TEMPLATE = "{0}-{1}_{2}_{3}_{4}.{5}";

        public string CapAudioFilePath { get; private set; } = "";

        public string CapMainFilePath { get; private set; } = "";

        public RecordingFilePathProvider(string directory, string appName, string identifier)
        {
            SetNextAvailableFilePaths(directory, appName, identifier);
        }

        private void SetNextAvailableFilePaths(string directory, string appName, string identifier, int recordingNumber = 1)
        {
            CapAudioFilePath = GetFilePath(directory, appName, "CapAudio", identifier, "wav", recordingNumber);
            CapMainFilePath = GetFilePath(directory, appName, "CapMain", identifier, "mp4", recordingNumber);

            if (File.Exists(CapAudioFilePath) || File.Exists(CapMainFilePath))
            {
                SetNextAvailableFilePaths(directory, appName, identifier, recordingNumber + 1);
            }
        }

        private string GetFilePath(string directory, string appName, string type, string identifier, string format, int recordingNumber)
        {
            return Path.Combine(
                directory,
                string.Format(FILE_NAME_TEMPLATE,
                    DateTime.Now.ToString("yyyyddMM"),
                    recordingNumber,
                    appName,
                    type,
                    identifier,
                    format
                )
            );
        }
    }
}
