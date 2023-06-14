using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace screen_recorder.AudioCapture
{
    /// <summary>
    /// Represents an asynchronous operation activating a WASAPI interface and provides a method to retrieve the results of the activation.
    /// </summary>
    [ComImport]
    [Guid("72A22D78-CDE4-431D-B8CC-843A71199B6D")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IActivateAudioInterfaceAsyncOperation
    {
        [PreserveSig]
        int GetActivateResult(out int activateResult, [MarshalAs(UnmanagedType.IUnknown)] out object activatedInterface);
    }

    /// <summary>
    /// Provides a callback to indicate that activation of a WASAPI interface is complete.
    /// </summary>
    [ComImport]
    [Guid("41D949AB-9862-444A-80F6-C261334DA5EB")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IActivateAudioInterfaceCompletionHandler
    {
        /// <summary>
        /// Indicates that activation of a WASAPI interface is complete and results are available.
        /// </summary>
        /// <param name="activateOperation"></param>
        void ActivateCompleted(IActivateAudioInterfaceAsyncOperation activateOperation);
    }

    public class ActivateAudioInterfaceCompletionHandler<T> : IActivateAudioInterfaceCompletionHandler where T : class
    {
        AutoResetEvent _completionEvent = new(false);
        void IActivateAudioInterfaceCompletionHandler.ActivateCompleted(IActivateAudioInterfaceAsyncOperation activateOperation)
            => _completionEvent.Set();

        public void WaitForCompletion()
            => _completionEvent.WaitOne();
    }
}
