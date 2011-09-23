namespace NewTOAPIA.UI
{
    using System;

    using TOAPI.Types;

    public class MouseEventSource : Observable<MouseActivityArgs>, IObserver<MSG>
    {
        //IObservable<MSG> fMessageSource;

        //public MouseEventSource()
        //{
        //}

        //public MouseEventSource(IObservable<MSG> observable)
        //{
        //    MessageSource = observable;
        //}

        //public IObservable<MSG> MessageSource
        //{
        //    get { return fMessageSource; }
        //    set
        //    {
        //        fMessageSource = value;
        //        if (fMessageSource != null)
        //            fMessageSource.AddObserver(this);
        //    }
        //}

        public virtual void OnCompleted()
        {
        }

        public virtual void OnError(Exception excep)
        {
        }

        public virtual void OnNext(MSG msg)
        {
            switch (msg.message)
            {
                // Left button processing
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
                        MouseActivityArgs ma = MouseActivityArgs.CreateFromWindowsMessage(msg.hWnd, msg.message, msg.wParam, msg.lParam);
                        //Console.WriteLine("MOUSE: {0}", ma);

                        DispatchData(ma);
                    }
                    break;
            }
        }
    }
}