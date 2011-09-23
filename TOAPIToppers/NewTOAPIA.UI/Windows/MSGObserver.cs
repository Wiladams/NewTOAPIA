namespace NewTOAPIA.UI
{
    using System;

    using TOAPI.Types;

    public class MSGObserver : IObserver<MSG>
    {
        protected WindowProc CallbackDelegate;
        List<IObserver<MSG>> Observers = new List<IObserver<MSG>>();

        public void Subscribe(IObserver<MSG> observer)
        {
            Observers.Add(observer);
        }


        protected virtual void OnNext(MSG msg)
        {
            //Console.WriteLine("Win32Window.OnMessage(default) - {0}", msg.ToString());

            IntPtr result = IntPtr.Zero;


            switch (msg.message)
            {
                //
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
                        result = OnKeyboardActivity(this, kbda);
                    }
                    break;


                case (int)WinMsg.WM_COMMAND:
                    {
                        int commandType = (int)BitUtils.Hiword((int)msg.wParam);
                        int commandParam = (int)BitUtils.Loword((int)msg.wParam);

                        switch (commandType)
                        {
                            case 0: // Menu item
                                OnMenuItemSelected(commandParam);
                                break;

                            case 1: // Accelerator selected
                                break;

                            default:
                                OnControlCommand(msg.lParam);
                                break;
                        }
                    }
                    break;


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
                        OnMouseActivity(this, ma);
                    }
                    break;

                case (int)WinMsg.WM_INPUT:
                    {
                        uint dwSize = 0;

                        // First find out how much space is needed to store the data that will
                        // be retrieved.
                        User32.GetRawInputData((IntPtr)msg.lParam, User32.RID_INPUT, IntPtr.Zero, ref dwSize,
                            (uint)Marshal.SizeOf(typeof(RAWINPUTHEADER)));

                        // Now allocate the specified number of bytes
                        IntPtr buffer = Marshal.AllocHGlobal((int)dwSize);

                        // Now get the data for real, and check the size to see
                        // if we got what we thought we should get.
                        if (User32.GetRawInputData((IntPtr)msg.lParam, User32.RID_INPUT, buffer, ref dwSize,
                            (uint)Marshal.SizeOf(typeof(RAWINPUTHEADER))) == dwSize)
                        {
                            RAWINPUT raw = (RAWINPUT)Marshal.PtrToStructure(buffer, typeof(RAWINPUT));

                            // At this point, we should have raw mouse data
                            // Create a reasonable MouseActivityArgs object that
                            // can be dealt with by the rest of the system.
                            // Here we turn the raw data into things like button activity
                            // as well as mapping the generic information into something
                            // applications can deal with.
                            //MouseActivityArgs mevent = new MouseActivityArgs(null,
                            //    (RawMouseEventType)raw.data.mouse.usFlags,
                            //    (MouseButtonActivity)raw.data.mouse.Union1.Struct1.usButtonFlags,
                            //    raw.data.mouse.lLastX, raw.data.mouse.lLastY,
                            //    raw.data.mouse.Union1.Struct1.usButtonData, 1);

                            //OnMouseActivity(this, mevent);
                        }
                    }
                    break;

                // Then paint events
                case (int)WinMsg.WM_PAINT:
                    PAINTSTRUCT ps = new PAINTSTRUCT();
                    ps.Init();
                    //IntPtr paintDC = User32.BeginPaint(Handle, out ps);

                    Rectangle paintRect = new Rectangle(ps.rcPaint.X, ps.rcPaint.Y, ps.rcPaint.Width, ps.rcPaint.Height);
                    DrawEvent dea = new DrawEvent(fClientAreaRenderer, paintRect);
                    OnPaint(dea);

                    // If "BeginPaint/EndPaint", are not used, we have to 
                    // explicitly call "Validate()".  If we don't, Windows will
                    // keep sending an enless stream of WM_PAINT messages until we do
                    Validate();
                    //User32.EndPaint(Handle, ref ps);

                    break;

                // And finally timing events
                case (int)WinMsg.WM_TIMER:
                    OnTimer();
                    break;

                case (int)WinMsg.WM_CLOSE:
                    // we give the delegate window a chance to say something about whether
                    // the window should close or not.  If OnCloseRequested returns 'true'
                    // the delegate is saying "it's ok to close", so we then call the default
                    // window procedure to proceed.
                    // If the delegate window returns 'false', then the message is essentially 
                    // swallowed without any action.
                    if (OnCloseRequested())
                        result = User32.DefWindowProc(msg.hWnd, msg.message, msg.wParam, msg.lParam);
                    break;

                case (int)WinMsg.WM_ENABLE:
                    {
                        bool flag;
                        if (IntPtr.Zero != msg.wParam)
                            flag = true;
                        else
                            flag = false;

                        OnEnable(flag);
                        break;
                    }

                case (int)WinMsg.WM_ERASEBKGND:
                    {
                        result = OnEraseBackground();
                    }
                    break;

                case (int)WinMsg.WM_MOVE:
                    OnMovedTo(BitUtils.Loword((int)msg.lParam), BitUtils.Hiword((int)msg.lParam));
                    break;

                case (int)WinMsg.WM_MOVING:
                    OnMoving(BitUtils.Loword((int)msg.lParam), BitUtils.Hiword((int)msg.lParam));
                    break;

                case (int)WinMsg.WM_SIZE:
                    {
                        // The wParam indicates what the resizing Action was
                        // SIZE_MAXHIDE
                        //   Message is sent to all pop-up windows when some other window is maximized.
                        // SIZE_MAXIMIZED
                        //   The window has been maximized.
                        // SIZE_MAXSHOW
                        //   Message is sent to all pop-up windows when some other window has been restored to its former size.
                        // SIZE_MINIMIZED
                        //   The window has been minimized.
                        // SIZE_RESTORED
                        //   The window has been resized, but neither the SIZE_MINIMIZED nor SIZE_MAXIMIZED value applies.

                        // The low-order word of lParam specifies the new width of the client area. 
                        // The high-order word of lParam specifies the new height of the client area. 
                        int width = BitUtils.Loword((int)msg.lParam);
                        int height = BitUtils.Hiword((int)msg.lParam);

                        switch ((int)msg.wParam)
                        {

                            case MSG.SIZE_MAXIMIZED:
                                OnMaximized(width, height);
                                break;

                            case MSG.SIZE_MINIMIZED:
                                OnMinimized();
                                break;

                            case MSG.SIZE_MAXHIDE:
                            case MSG.SIZE_MAXSHOW:
                            case MSG.SIZE_RESTORED:
                            default:
                                OnResizedTo(width, height);
                                break;
                        }
                    }
                    break;

                case (int)WinMsg.WM_SIZING:
                    // wParam specifies which edges are being resized
                    // lParam is a pointer to a RECT structure which gives the drag rectangle
                    OnResizing(BitUtils.Loword((int)msg.lParam), BitUtils.Hiword((int)msg.lParam));
                    break;

                case (int)WinMsg.WM_EXITSIZEMOVE:
                    OnResized();
                    break;

                case (int)WinMsg.WM_KILLFOCUS:
                    //fCaret.FocusWindow = null;
                    Win32Cursor.SetFocusWindow(null);
                    OnKillFocus();
                    break;

                case (int)WinMsg.WM_SETFOCUS:
                    //fCaret.FocusWindow = this;
                    Win32Cursor.SetFocusWindow(fPlatformWindow);
                    OnSetFocus();
                    break;

                case (int)WinMsg.WM_SETCURSOR:
                    Win32Cursor.SetCurrentCursor(Win32Cursor.Arrow);

                    //    // return '1' to halt further processing
                    //    // otherwise return '0'
                    result = new IntPtr(1);
                    break;


                case (int)WinMsg.WM_DESTROY:
                    OnDestroy();
                    break;

                // By the time we get this message, the window handle has already
                // been destroyed.  Posting the WM_QUIT message will clean out the 
                // thread's message queue, and some other stuff.
                // This is called after WM_DESTROY, and after all child windows
                // have already been destroyed.
                case (int)WinMsg.WM_NCDESTROY:
                    OnDestroyed();
                    break;


                // If '0' is returned from here, the window creation
                // discontinues and the handle is deleted.
                case (int)WinMsg.WM_CREATE:
                case (int)WinMsg.WM_NCCREATE:
                    result = User32.DefWindowProc(msg.hWnd, msg.message, msg.wParam, msg.lParam);
                    break;

                default:
                    // Do what should be done by default if we don't handle it specifically
                    //Console.WriteLine("Window.DispatchMessage(default) - {0}", MessageDecoder.MsgToString(msg.message));
                    result = User32.DefWindowProc(msg.hWnd, msg.message, msg.wParam, msg.lParam);
                    break;
            }

            //return result;
        }
    }
}