using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace screen_recorder.AudioCapture
{
    /// <summary>
    /// Specifies the loopback mode for an <see cref="ActivationParameters">AUDIOCLIENT_ACTIVATION_PARAMS</see> structure passed into a call to <see cref="Helper.ActivateAudioInterfaceAsync(String, Guid, ByRef PropVariant, ByRef IActivateAudioInterfaceCompletionHandler)"/>.
    /// </summary>
    public enum LoopbackMode
    {
        /// <summary>
        /// Render streams from the specified process and its child processes are included in the activated process loopback stream.
        /// </summary>
        IncludeProcessTree,
        /// <summary>
        /// Render streams from the specified process and its child processes are excluded from the activated process loopback stream.
        /// </summary>
        ExcludeProcessTree
    }

    /// <summary>
    /// Specifies the activation type for an <see cref="ActivationParameters">AUDIOCLIENT_ACTIVATION_PARAMS</see> structure passed into a call to <see cref="Helper.ActivateAudioInterfaceAsync(String, Guid, ByRef PropVariant, ByRef IActivateAudioInterfaceCompletionHandler)" />.
    /// </summary>
    public enum ActivationType
    {
        /// <summary>
        /// Default activation.
        /// </summary>
        Default,
        /// <summary>
        /// Process loopback activation, allowing for the inclusion or exclusion of audio rendered by the specified process and its child processes. <br/>
        /// For sample code that demonstrates the process loopback capture scenario, see the Application Loopback <see href="https://docs.microsoft.com/en-us/samples/microsoft/windows-classic-samples/applicationloopbackaudio-sample/">API Capture Sample.</see>
        /// </summary>
        ProcessLoopback
    }
}
