using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace screen_recorder.AudioCapture
{
    /// <summary>
    /// Specifies parameters for a call to <see cref="Helper.ActivateAudioInterfaceAsync(String, Guid, ByRef PropVariant, ByRef IActivateAudioInterfaceCompletionHandler)"/> where loopback activation is requested.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ProcessLoopbackParams
    {
        /// <summary>
        /// The ID of the process for which the render streams, and the render streams of its child processes, will be included or excluded when activating the process loopback stream.
        /// </summary>
        public int TargetProcessId;
        /// <summary>
        /// A value from the <see cref="LoopbackMode">PROCESS_LOOPBACK_MODE</see> enumeration specifying whether the render streams for the process and child processes specified in the TargetProcessId field should be included or excluded when activating the audio interface. <br />
        /// For sample code that demonstrates the process loopback capture scenario, see the <see href="https://docs.microsoft.com/en-us/samples/microsoft/windows-classic-samples/applicationloopbackaudio-sample/">Application Loopback API Capture Sample</see>.
        /// </summary>
        public LoopbackMode ProcessLoopbackMode;
    }

    /// <summary>
    /// Specifies the activation parameters for a call to <see cref="Helper.ActivateAudioInterfaceAsync(String, Guid, ByRef PropVariant, ByRef IActivateAudioInterfaceCompletionHandler)"/>.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ActivationParameters
    {
        /// <summary>
        /// A member of the <see cref="ActivationType">AUDIOCLIENT_ACTIVATION_TYPE</see> specifying the type of audio interface activation. <br/>
        /// Currently default activation and loopback activation are supported.
        /// </summary>
        public ActivationType Type;
        /// <summary>
        /// A <see cref="ProcessLoopbackParams">AUDIOCLIENT_PROCESS_LOOPBACK_PARAMS</see> specifying the loopback parameters for the audio interface activation.
        /// </summary>
        public ProcessLoopbackParams ProcessLoopbackParams;
    }
}
