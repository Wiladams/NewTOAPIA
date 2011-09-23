namespace NewTOAPIA.UI
{
    using System;

    using TOAPI.Types;

    /// <summary>
    /// Takes a MSG observable stream, and turns it into a stream of keyboard
    /// event observables.
    /// </summary>
    public class KeyboardEventSource : Observable<KeyboardActivityArgs>, IObserver<MSG>
    {


        #region IObserver<MSG>
        public virtual void OnCompleted()
        {
        }

        public virtual void OnError(System.Exception excep)
        {
            throw excep;
        }

        public virtual void OnNext(MSG msg)
        {
            // Create a keyboard event
            switch(msg.message)
            {
                // These messages are sent to the windowproc directly.
                // They do not come from the posted message queue.
                //
                //case MSG.WM_SYSKEYDOWN:
                //case MSG.WM_SYSKEYUP:
                //case MSG.WM_SYSCHAR:

                case (int)WinMsg.WM_KEYDOWN:
                case (int)WinMsg.WM_KEYUP:
                case (int)WinMsg.WM_CHAR:   // these are generated from TranslateMessage after WM_KEYDOWN
                    {
                        KeyboardActivityArgs kbda = KeyboardActivityArgs.CreateFromWindowsMessage(msg.hWnd, msg.message, msg.wParam, msg.lParam);
                        Console.WriteLine("KEYBOARD: {0}", kbda);

                        DispatchData(kbda);
                    }
                    break;
            }
        }
        #endregion

    }
}