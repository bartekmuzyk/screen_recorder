using NAudio.CoreAudioApi.Interfaces;
using NAudio.Wasapi.CoreAudioApi.Interfaces;
using screen_recorder.Extensions;
using System.Runtime.InteropServices;

namespace screen_recorder.Recording.Audio.WinCaptureAudio
{
    [StructLayout(LayoutKind.Sequential)]
    internal class CompletionHandler : IActivateAudioInterfaceCompletionHandler, IAgileObject
    {
        public IAudioClient client;

        public int hr = HRESULT.E_FAIL;
        public IntPtr eventFinished;

        public CompletionHandler()
        {
            eventFinished = Kernel32.CreateEvent(IntPtr.Zero, false, false, null);
        }

        public void ActivateCompleted(IActivateAudioInterfaceAsyncOperation operation)
        {
            operation.GetActivateResult(out hr, out var obj);
            client = (IAudioClient)obj;
            Kernel32.SetEvent(eventFinished);
        }
    }
}
