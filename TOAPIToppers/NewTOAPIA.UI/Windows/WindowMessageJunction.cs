namespace NewTOAPIA.UI
{
    using System;

    using TOAPI.Types;

    public class WindowMessageJunction : Observable<MSG>, 
        IObserver<MSG>,
        IObservable<KeyboardActivityArgs>, 
        IObservable<MouseActivityArgs>
    {
        KeyboardEventSource fKeyboardEventSource = new KeyboardEventSource();
        MouseEventSource fMouseEventSource = new MouseEventSource();


        public virtual IDisposable Subscribe(IObserver<KeyboardActivityArgs> observer)
        {
            return fKeyboardEventSource.Subscribe(observer);
        }

        public virtual IDisposable Subscribe(IObserver<MouseActivityArgs> observer)
        {
            return fMouseEventSource.Subscribe(observer);
        }

        public virtual void OnCompleted()
        {
        }

        public virtual void OnError(Exception excep)
        {
        }

        public virtual void OnNext(MSG msg)
        {
            //Console.WriteLine("WindowMessageJunction.OnNext - {0}", msg);

            IntPtr result = IntPtr.Zero;

            switch (msg.message)
            {
                // Pass all the mouse messages on to the mouse message
                    // filter.
                case (int)WinMsg.WM_MOUSEHOVER:
                case (int)WinMsg.WM_MOUSELEAVE:
                case (int)WinMsg.WM_MOUSEMOVE:
                case (int)WinMsg.WM_MOUSEWHEEL:
                case (int)WinMsg.WM_LBUTTONDBLCLK:
                case (int)WinMsg.WM_LBUTTONDOWN:
                case (int)WinMsg.WM_LBUTTONUP:
                case (int)WinMsg.WM_MBUTTONDBLCLK:
                case (int)WinMsg.WM_MBUTTONDOWN:
                case (int)WinMsg.WM_MBUTTONUP:
                case (int)WinMsg.WM_RBUTTONDBLCLK:
                case (int)WinMsg.WM_RBUTTONDOWN:
                case (int)WinMsg.WM_RBUTTONUP:
                    {
                        fMouseEventSource.OnNext(msg);
                    }
                    break;

                    // Pass all the keyboard messages on to the keyboard
                    // message filter.
                //case MSG.WM_SYSKEYDOWN:
                //case MSG.WM_SYSKEYUP:
                //case MSG.WM_SYSCHAR:

                case (int)WinMsg.WM_KEYDOWN:
                case (int)WinMsg.WM_KEYUP:
                case (int)WinMsg.WM_CHAR:   // these are generated from TranslateMessage after WM_KEYDOWN
                    {
                        fKeyboardEventSource.OnNext(msg);
                    }
                    break;

                    // For the remainder of the filters, dispatch
                    // them to whomever is looking for MSG
                default:
                    DispatchData(msg);
                    break;
            }
        }

    }
}