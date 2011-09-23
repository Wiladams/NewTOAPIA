using System;
using System.Runtime.InteropServices;
using System.Threading;

using TOAPI.Kernel32;
using TOAPI.Types;
using TOAPI.User32;

/// <summary>
/// Class: Win32Window
/// 
/// Purpose: To create a window that runs on a separate thread than the thread
/// that created the window.
/// 
/// Note: These windows have a few key characteristics that make them special  from
/// other windows.
/// 1) Each window is running on its own thread.  This means that it does not have to go
/// back through Windows to have messages dispatched.  All messages that come through the message
/// loop are meant to be processed by this window, or one of its children.
/// 2) The window owns its Device Context (DC).  When the WM_PAINT message comes in, there
/// is no need to get the device context because it will be the same every time.  Additionally,
/// we create a GDI32DeviceContext object in the beginning, and this can hold device context state
/// throughout the existence of this window object.
/// 3) Messages are dispatched to the window directly rather than going back through User32's 
/// DispatchMessage() call.  This may change the message processing characteristics slightly.
/// How they change exactly is unknown.
/// 
/// </summary>
    public class Win32Window : IPlatformWindow, IHandle
	{
		protected static string gWindowClassName = "Win32Window";
        protected static bool gClassRegistered = false;

		// Here's where you hook up to the generic machinery
        private ushort fClassAtom;		// atom representing class registration
        private MessageProc fWindowProc;
        private WNDCLASSEX fWndClass;
        private IntPtr fAppInstance;	// Application Instance
        private Rectangle fWindowFrame;
        private string fTitle;
        protected IWindow fDelegateWindow;

        protected IntPtr fHandle;			// Handle to Win32 window

        // Drawing device context
        protected GDIDeviceContext fDeviceContext;	// Handle to window's device context
        protected GDIRenderer fClientGraphPort;

		// The caret object we use to control the caret
		//protected Win32Caret fCaret;

		// These are used to startup the thread
		protected Thread fThread;
		protected ManualResetEvent fQueueReady;

        // Device context and renderer for the Window title bar
        protected GDIDeviceContext fWindowDeviceContext;
        protected IGraphPort fWindowGraphPort;

		static Win32Window()
		{
			// Put class static initializers in here.
		}

        public IGraphPort WindowGraphPort
        {
            get { return fWindowGraphPort; }
        }

        public IGraphPort ClientAreaGraphPort
        {
            get { return fClientGraphPort; }
        }

        //public Win32Window(int exStyle, string className, string caption, int style,
        //        int x, int y, int width, int height,
        //        IntPtr parent, IntPtr menu, IntPtr appInstance,
        //        Window delegatewindow)
        //    : base(gWindowClassName, caption,
        //        style,
        //        x, y, width, height,
        //        parent, menu, appInstance)
        //{
        //    // By the time we return from the base class constructor, we 
        //    // have already done everything with User32 that is necessary to 
        //    // setup a window.  the message queue is running in a separate thread,
        //    // the window handle is set, and the graph port is set.
        //    // All that's left is to setup the delegate window, and anything else
        //    // that the caller to this constructor will need to do their work.

        //    fCaret = new Win32Caret(2, 14);
        //}

		public Win32Window(string title, int x, int y, int width, int height, IWindow delegatewindow)
		{
            fWindowFrame = new Rectangle(x, y, width, height);
            fTitle = title;

            // By the time we return from the base class constructor, we 
			// have already done everything with User32 that is necessary to 
			// setup a window.  the message queue is running in a separate thread,
			// the window handle is set, and the graph port is set.
			// All that's left is to setup the delegate window, and anything else
			// that the caller to this constructor will need to do their work.
			fDelegateWindow = delegatewindow;

            // Create components
            CreateComponents();


            // Register the window
            RegisterWindowClass();

            // Now create the window handle
            CreateHandle();
		}

        protected virtual void RegisterWindowClass()
        {
            if (gClassRegistered)
                return;

            Win32Window.gClassRegistered = true;

            fWindowProc = new MessageProc(this.Callback);
            fAppInstance = Kernel32.GetModuleHandle(null);
            fWndClass = new WNDCLASSEX();
            fWndClass.Init();

            fWndClass.style = (int)(CS.HREDRAW | CS.VREDRAW | CS.DBLCLKS | CS.GLOBALCLASS | CS.OWNDC);
            fWndClass.lpfnWndProc = fWindowProc;
            fWndClass.cbClsExtra = 0;
            fWndClass.cbWndExtra = 0;
            fWndClass.hInstance = fAppInstance;
            fWndClass.hIcon = IntPtr.Zero;
            fWndClass.hCursor = IntPtr.Zero;
            fWndClass.hbrBackground = (IntPtr)(User32.COLOR_WINDOW + 1); 
            fWndClass.lpszMenuName = null;
            fWndClass.lpszClassName = Win32Window.gWindowClassName;
            fWndClass.hIconSm = IntPtr.Zero;

            // Register the window class. 
            fClassAtom = User32.RegisterClassEx(ref fWndClass);
            uint error = Kernel32.GetLastError();

            if (fClassAtom == 0)
            {
                fWindowProc = null;
                //throw new Win32Exception();
            }
         
        }

		protected virtual void CreateHandle()
		{
            ThreadRoutine();
			//fQueueReady = new ManualResetEvent(false);
			//fThread = new Thread(new ThreadStart(this.ThreadRoutine));

			// And the start the thread going.
			//fThread.Start();

            // Wait here until the queue says it is ready 
            // for processing messages.
            //fQueueReady.WaitOne();
		}

        public virtual void CreateComponents()
        {
            //if (null != fDelegateWindow)
            //    fDelegateWindow.CreateComponents();
        }

		/// <summary>
		/// The routines within WindowInitialize are running in
		/// their own thread.  This is the right place to create a window.  
		/// Our message loop will only be dealing with messages meant
		/// for this window, and general thread messages.
		/// </summary>
		private void ThreadRoutine()
		{
			//CreateParams cp = ViewParameters;

            //fCaret = new Win32Caret(2, 14);

            // WS_EX_LAYERED

            fHandle = User32.CreateWindowEx(
                User32.WS_EX_ACCEPTFILES | User32.WS_EX_OVERLAPPEDWINDOW,
                gWindowClassName,
				this.fTitle,
                User32.WS_OVERLAPPEDWINDOW,
				fWindowFrame.Left,
				fWindowFrame.Top,
				fWindowFrame.Width,
				fWindowFrame.Height,
				IntPtr.Zero,
				IntPtr.Zero,
				fAppInstance,
				0);

            //User32.SetLayeredWindowAttributes(fHandle, 0, 255, User32.LWA_ALPHA);

			// Get DeviceContext for client area so we can draw
			fDeviceContext = GDIDeviceContext.CreateForWindow(fHandle);

			// And signal that we're actually ready to
			// entertain the outside world.
			//fQueueReady.Set();

			//Run();
		}

		/// <summary>
		/// </summary>
		// The message processing chain is like this:
		// First, in Run(), we're pulling messages off of the 
		// message queue using GetMessage().  
		// 2) Translate that message into a proper keyboard 
		//    message using TranslateMessage()
		// 3) Dispatch the message using the WindowProc for 
		//    the window, namely, fWindowProc.
		// 4) fWindowProc being this.Callback(), Callback() then
		//    packages up the message into a Message object, and
		//    Calls this.DispatchMessage()
		// 5) If DispatchMessage() can deal with it alone, it
		//    will deal with it.  If it can't, then it then
		//    calls the default window dispatch function
		// 6) DefWindowProc
		//
		// And that's about it.
		public virtual void Run()
		{
			MSG msg = new MSG();

            while (User32.GetMessage(out msg, IntPtr.Zero, 0, 0))
			{
				/// Messages that come in here are typically keyboard
				/// mouse, and timer events.
				/// For the keyboard ones, there may be Hot-Key combinations
				/// that need to be translated into regular characters.  So,
				/// we first call the TranslateMessage() function to do that.
                User32.TranslateMessage(ref msg);

				/// Now, we want to actually handle the message.  
				/// There are three different cases where we will receive
				/// messages.
				/// 1) Thread messages - These messages have a msg.hWnd == 0
				/// Right now we will do nothing with those.
				/// 2) Messages with a msg.hWnd == Handle
				/// These are messages that are meant for the specific window
				/// that this code is dealing with.  We will dispatch these directly
				/// because we know which window they are meant for, and we don't need 
				/// the system to figure that out for us.
				/// 3) Messages with a msg.hWnd != 0 && msg.hWnd != Handle
				/// These are messages that are meant for a child window of our managed
				/// windows.  This would be a User32 Window that is a child window
				/// of the one we are handling.  These we allow the system to dispatch
				/// as it normally would.
				/// By covering these three cases, we're all set.

				//if (msg.hWnd == Handle)
				//	OnMessage(this, msg);
				//else if (msg.hWnd != IntPtr.Zero)
                    User32.DispatchMessage(ref msg);
				//else
				// It's a thread message and we need to do something about that

			}

			Dispose();

			// We must have received the WM_QUIT message
			// So, tell the delegate window it's time to quit before
			// we terminate the thread.
			fDelegateWindow.OnQuit();
		}

        /// <summary>
        /// This is where all the messages from the system come to.
        /// We instantly turn them into Message objects so that we can
        /// dispatch them throughout the rest of the system.
        /// </summary>
        /// <param name="hWnd"> </param>
        /// <param name="msg"> </param>
        /// <param name="wParam"> </param>
        /// <param name="lParam"> </param>
        public virtual int Callback(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            MSG m = MSG.Create(hWnd, msg, (uint)wParam.ToInt32(), lParam.ToInt32());
            //Console.WriteLine("User32View.Callback - {0}", MessageDecoder.MsgToString(msg));

            //if (Win32MessageEvent != null)
            //    Win32MessageEvent(this, m);

            int retValue = OnMessage(this, m);
            
            return retValue;
        }

		protected virtual int OnMessage(Object sender, MSG msg)
		{
            //Console.WriteLine("Win32Window.OnMessage(default) - {0}", MessageDecoder.MsgToString(msg.message));

            int result = 0;

			switch (msg.message)
			{
					//
					// Keyboard messages
					//
                //case MSG.WM_SYSKEYDOWN:
                //case MSG.WM_SYSKEYUP:

                case MSG.WM_KEYDOWN:
					WmKeyDown(ref msg);
					break;

                case MSG.WM_KEYUP:
					WmKeyUp(ref msg);
					break;

					// 
					// Mouse processing messages
					//
					// Left button processing
				case MSG.WM_LBUTTONDBLCLK:
                    WmMouseDown(ref msg, MouseButtons.Left, 2);
					break;
				case MSG.WM_LBUTTONDOWN:
                    WmMouseDown(ref msg, MouseButtons.Left, 1);
					break;
				case MSG.WM_LBUTTONUP:
                    WmMouseUp(ref msg, MouseButtons.Left,1);
					break;

					// Middle button processing
				case MSG.WM_MBUTTONDBLCLK:
                    WmMouseDown(ref msg, MouseButtons.Middle, 2);
					break;
				case MSG.WM_MBUTTONDOWN:
                    WmMouseDown(ref msg, MouseButtons.Middle, 1);
					break;
				case MSG.WM_MBUTTONUP:
                    WmMouseUp(ref msg, MouseButtons.Middle,1);
					break;

				// Right button processing
				case MSG.WM_RBUTTONDBLCLK:
                    WmMouseDown(ref msg, MouseButtons.Right, 2);
					break;
				case MSG.WM_RBUTTONDOWN:
                    WmMouseDown(ref msg, MouseButtons.Right, 1);
					break;
				case MSG.WM_RBUTTONUP:
                    WmMouseUp(ref msg, MouseButtons.Right,1);
					break;


                case MSG.WM_MOUSEHOVER:
                    WmMouseHover(ref msg);
                    break;
				case MSG.WM_MOUSELEAVE:
					WmMouseLeave(ref msg);
					break;
				case MSG.WM_MOUSEMOVE:
					WmMouseMove(ref msg);
					break;
				case MSG.WM_MOUSEWHEEL:
					WmMouseWheel(ref msg);
					break;

				// Then paint events
				case MSG.WM_PAINT:
                    PAINTSTRUCT ps = new PAINTSTRUCT();
                    User32.BeginPaint(Handle, out ps);

                    //fDeviceContext = new GDIDeviceContext(ps.hdc);
                    //fClientGraphPort = new GDIRenderer(fDeviceContext);
                    DrawEventArgs dea = new DrawEventArgs(fClientGraphPort, ps);
                    OnPaint(dea);

                    // If "BeginPaint/EndPaint", are not used, we have to 
                    // explicitly call "Validate()".  If we don't, Windows will
                    // keep sending an enless stream of WM_PAINT messages until we do
					//Validate();
                    User32.EndPaint(Handle, ref ps);

					break;

				// And finally timing events
				case MSG.WM_TIMER:
					WmTimer(ref msg);
					break;





				//
				// These messages are sent to the windowproc directly.
				// They do not come from the posted message queue.
				//
				//
				// These character events are the ones generated from
				// the TranslateMessage() function, after a WM_KEYDOWN
				// message.
                //case MSG.WM_SYSCHAR:
                case MSG.WM_CHAR:
					WmKeyChar(ref msg);
					break;

				//case MSG.WM_COMMAND:
				//	fDelegateWindow.OnCommand(Utils.LOWORD(msg.wParam));
				//	break;

				case MSG.WM_CLOSE:
					// we give the delegate window a chance to say something about whether
					// the window should close or not.  If OnCloseRequested returns 'true'
					// the delegate is saying "it's ok to close", so we then call the default
					// window procedure to proceed.
					// If the delegate window returns 'false', then the message is essentially 
					// swallowed without any action.
					if (fDelegateWindow.OnCloseRequested())
					{
                        //intResult =User32.CallWindowProc(gDefaultWindowProc, msg.hWnd, msg.message, new IntPtr((int)msg.wParam), new IntPtr(msg.lParam));
                        result = User32.DefWindowProc(msg.hWnd, msg.message, new IntPtr((int)msg.wParam), new IntPtr(msg.lParam));
					}
					break;

				//case MSG.WM_CREATE:
                    // We receive this message during the 'CreateWindowEx' function call.
                    // The window handle is valid, so we can do some setup based on
                    // that, like setting our handle.  
                   // fHandle = msg.hWnd;

                   // // At this point, since we used CS.OWNDC, the Device context
                   // // for the entire window, and for the client area are also valid.
                   // // So, we can setup those handles, and create the renderers as well.
                   // fDeviceContext = User32.GetDC(msg.hWnd);
                   // fClientGraphPort = new GDIRenderer(fDeviceContext);

                   //// Get device context for window title area
                   // // and create a GDIRenderer for it.
                   // fWindowDeviceContext = User32.GetDCEx(msg.hWnd, IntPtr.Zero, User32.DeviceContextValues.Window);
                   // fWindowGraphPort = new GDIRenderer(fWindowDeviceContext);

                   // // And signal that we're actually ready to
                   // // entertain the outside world.
                   // fQueueReady.Set();
					//fDelegateWindow.OnCreated();  // can't do this because we're still in the constructor
				//	break;

                //case MSG.WM_ERASEBKGND:
                //    // the msg.wparam is a pointer to a device context
                //    // So, we create a renderer to it, and pass it along.
                //    //GDIRenderer rend = new GDIRenderer(new IntPtr(msg.wParam));
                //    result = fDelegateWindow.OnEraseBackground(null);
                //break;

				case MSG.WM_MOVE:
					fDelegateWindow.OnMovedTo(BitUtils.LOWORD((int)msg.lParam), BitUtils.HIWORD((int)msg.lParam));
					break;

				//case MSG.WM_MOVING:
				//	fDelegateWindow.OnMoving(Utils.LOWORD(msg.lParam), Utils.HIWORD(msg.lParam));
				//break;

				case MSG.WM_SIZE:
					fDelegateWindow.OnResized(BitUtils.LOWORD((int)msg.lParam), BitUtils.HIWORD((int)msg.lParam));
					break;

				//case MSG.WM_SIZING:
				//	fDelegateWindow.OnResizing(Utils.LOWORD(msg.lParam), Utils.HIWORD(msg.lParam));
				//break;

				case MSG.WM_KILLFOCUS:
					//fCaret.FocusWindow = null;
					Win32Cursor.Current.FocusWindow = null;
					fDelegateWindow.OnKillFocus();
					break;

				case MSG.WM_SETFOCUS:
					//fCaret.FocusWindow = this;
					Win32Cursor.Current.FocusWindow = this;
					fDelegateWindow.OnSetFocus();
					break;

                case MSG.WM_SETCURSOR:
                    Win32Cursor.Current = Win32Cursor.Arrow;

                //    // return '1' to halt further processing
                //    // otherwise return '0'
                    result = 1;
                    break;

                case MSG.WM_NCCREATE:
                    // We receive this message during the 'CreateWindowEx' function call.
                    // The window handle is valid, so we can do some setup based on
                    // that, like setting our handle.  
                    fHandle = msg.hWnd;

                    // At this point, if we are using CS.OWNDC, the Device context
                    // for the entire window, and for the client area are also valid.
                    // So, we can setup those handles, and create the renderers as well.
                    fDeviceContext = GDIDeviceContext.CreateForWindow(msg.hWnd);
                    fClientGraphPort = new GDIRenderer(fDeviceContext);

                    // Get device context for window title area
                    // and create a GDIRenderer for it.
                    fWindowDeviceContext = GDIDeviceContext.CreateForWindow(msg.hWnd);
                    fWindowGraphPort = new GDIRenderer(fWindowDeviceContext);

                    // And signal that we're actually ready to
                    // entertain the outside world.
                    //fQueueReady.Set();

                    // If '0' is returned from here, the window creation
                    // discontinues and the handle is deleted.
                    result = 1;
                    break;

				// By the time we get this message, the window handle has already
				// been destroyed.  Posting the WM_QUIT message will clean out the 
				// thread's message queue, and some other stuff.
                // This is called after WM_DESTROY, and after all child windows
                // have already been destroyed.
				case MSG.WM_NCDESTROY:
					User32.PostQuitMessage(0);
					break;


				default:
					// Do what should be done by default if we don't handle it specifically
                    //Console.WriteLine("Window.DispatchMessage(default) - {0}", MessageDecoder.MsgToString(msg.message));
                    result = User32.DefWindowProc(msg.hWnd, msg.message, new IntPtr((int)msg.wParam), new IntPtr(msg.lParam));
                    break;
			}

			return result;
		}

		/// <summary>
		/// These various 'WmXXX' method calls are used to deal with the various events from the 
		/// message loop.  In most cases, they pass parameters along to the appropriate 'OnXXX' 
		/// method call in the delegate.
		/// </summary>

        public void OnPaint(DrawEventArgs dea)
        {
            fDelegateWindow.OnPaint(dea);
        }

		/// <summary>
		/// Called to handle WM_KEYCHAR messages.
		/// </summary>
		/// <param name="m"></param>
		/// <returns></returns>
		private bool WmKeyChar(ref MSG m)
		{
            KeyCharEventArgs kpe = new KeyCharEventArgs((char)m.wParam);
            KeyboardEvent ke = new KeyboardEvent(KeyEventType.KeyChar, 0, m.lParam, (char)m.wParam);

			fDelegateWindow.OnKeyPress(kpe);

			return true;
		}

		private bool WmKeyDown(ref MSG m)
		{
			KeyEventArgs ke = null;

            ke = new KeyEventArgs((Keyboard.VirtualKeyCodes)(((int)m.wParam) | (int)ModifierKeys),
                m.lParam);
            KeyboardEvent kbde = new KeyboardEvent(KeyEventType.KeyDown, (Keyboard.VirtualKeyCodes)(((int)m.wParam) | (int)ModifierKeys), m.lParam, '\0');
			
            fDelegateWindow.OnKeyDown(ke);

			return true;
		}

		private bool WmKeyUp(ref MSG m)
		{
			KeyEventArgs ke = null;

            ke = new KeyEventArgs((Keyboard.VirtualKeyCodes)(((int)m.wParam) | (int)ModifierKeys),
                m.lParam);

            KeyboardEvent kbde = new KeyboardEvent(KeyEventType.KeyDown, (Keyboard.VirtualKeyCodes)(((int)m.wParam) | (int)ModifierKeys), m.lParam, '\0');
			
            fDelegateWindow.OnKeyUp(ke);

			return true;
		}

		/// <summary>
		///     Handles the WM_MOUSEDOWN message
		/// </summary>
		/// <internalonly/>
        private void WmMouseDown(ref MSG m, MouseButtons button, int clicks) 
		{
			fDelegateWindow.OnMouseDown(new MouseEventArgs(button, clicks, (short)BitUtils.LOWORD(m.lParam), (short)BitUtils.HIWORD(m.lParam), 0));
		}

		/// <summary>
		///     Handles the WM_MOUSEENTER message
		/// </summary>
		/// <internalonly/>
		private void WmMouseEnter(ref MSG m) 
		{
			fDelegateWindow.OnMouseEnter(EventArgs.Empty);
		}

        /// <summary>
        ///     Handles the WM_MOUSEHOVER message
        /// </summary>
        private int WmMouseHover(ref MSG m)
        {
            fDelegateWindow.OnMouseHover(EventArgs.Empty);
            
            // We return 0 to say we handled it.
            return 0;
        }
        
        /// <summary>
		///     Handles the WM_MOUSELEAVE message
		/// </summary>
		/// <internalonly/>
		private void WmMouseLeave(ref MSG m) 
		{
			fDelegateWindow.OnMouseLeave(EventArgs.Empty);
		}

		/// <summary>
		///     Handles the WM_MOUSEMOVE message
		/// </summary>
		/// <internalonly/>
		private void WmMouseMove(ref MSG m) 
		{
            short x = (short)BitUtils.LOWORD(m.lParam);
            short y = (short)BitUtils.HIWORD(m.lParam);

            fDelegateWindow.OnMouseMove(new MouseEventArgs(MouseButtons.None, 0, x, y, 0));
		}

		/// <summary>
		///     Handles the WM_MOUSEUP message
		/// </summary>
		/// <internalonly/>
        private void WmMouseUp(ref MSG m, MouseButtons button, int clicks) 
		{

			// Get the mouse location
			//
			short x = BitUtils.LOWORD(m.lParam);
			short y = BitUtils.HIWORD(m.lParam);
			//Point aPoint = new Point(x, y);

			//bool aValue = GDI.ScreenToClient(Handle, ref  aPoint);
			// Convert the point through the transform
			//Point[] P = new Point[1]{aPoint};
			//GDI.DPtoLP(DeviceContext, P, 1); // map device coordinate to logical size

			//fDelegateWindow.OnMouseUp(new MouseEventArgs(button, 0, P[0].x, P[0].y, 0));
			//fDelegateWindow.OnMouseUp(new MouseEventArgs(button, 0, aPoint.X, aPoint.Y, 0));
			fDelegateWindow.OnMouseUp(new MouseEventArgs(button, clicks, x, y, 0));

		}

		/// <summary>
		///     Handles the WM_MOUSEWHEEL message
		/// </summary>
		/// <internalonly/>
		private void WmMouseWheel(ref MSG m) 
		{
            short x = BitUtils.LOWORD(m.lParam);
            short y = BitUtils.HIWORD(m.lParam);
            MouseEventArgs mArgs = new MouseEventArgs(MouseButtons.None, 0, x, y, BitUtils.HIWORD((int)m.wParam));
            fDelegateWindow.OnMouseWheel(mArgs);
		}

		private void WmTimer(ref MSG m)
		{
            //MSG msg = new MSG();
            //msg.hWnd = m.hWnd;
            //msg.lParam = m.lParam;
            //msg.message = m.message;
            //msg.wParam = m.wParam;

			fDelegateWindow.OnTimer();
		}

        public Rectangle Frame
        {
            get
            {
                Rectangle aFrame = new Rectangle();
                User32.GetWindowRect(Handle, out aFrame);
                return aFrame;
            }
        }

        public Rectangle Bounds
        {
            get
            {
                Rectangle aFrame = new Rectangle();
                User32.GetClientRect(Handle, out aFrame);
                return aFrame;
            }
        }

        
        public virtual bool SetWindowAlpha(byte alpha)
        {
            int res1;

            res1 = User32.GetWindowLong(Handle, User32.GWL_EXSTYLE);
            
            // If the alpha is == 255, then turn off layered window
            if (255 == alpha)
            {
                User32.SetWindowLong(Handle, User32.GWL_EXSTYLE, res1 & ~User32.WS_EX_LAYERED);
                
                return true;
            }

            // If we are here, then alpha is less than 255 (opaque), so
            // we need to first ensure the window is a layered window.
            User32.SetWindowLong(Handle, User32.GWL_EXSTYLE, res1 | User32.WS_EX_LAYERED);

            // Next we setup the alpha value.
            bool res2 = User32.SetLayeredWindowAttributes(Handle, 0, alpha, User32.LWA_ALPHA);

            return true;
        }


        /// <summary>
        /// Invalidate the entire client area.
        /// </summary>
		public virtual void Invalidate()
		{
			Rectangle rect = new Rectangle(0, 0, 0, 0);
            
            User32.GetClientRect(Handle, out rect);
            User32.InvalidateRect(Handle, ref rect, true);
		}

		public virtual void Invalidate(Rectangle rect)
		{
            User32.InvalidateRect(Handle, ref rect, true);
		}

		public virtual void Validate()
		{
			Rectangle rect = new Rectangle(0,0,0,0);
            User32.GetClientRect(Handle, out rect);
            User32.ValidateRect(Handle, ref rect);	
		}

        /// <summary>
        /// Move the window to a specific location.
        /// </summary>
        /// <param name="x">The X coordinate of the window frame's upper left corner.</param>
        /// <param name="y">The Y coordinate of the window frame's upper left corner.</param>
        /// <returns>Always returns 'true'.</returns>
        public virtual bool MoveTo(int x, int y)
        {
            User32.MoveWindow(Handle, x, y, fWindowFrame.Width, fWindowFrame.Height, true);

            return true;
        }

        /// <summary>
        /// Move the window by a specied amount.
        /// </summary>
        /// <param name="dx">The distance to move in the x-axis.</param>
        /// <param name="dy">The number of pixels to move in the y-axis.</param>
        /// <returns>Always returns true.</returns>
        public virtual bool MoveBy(int dx, int dy)
        {
            return MoveTo(Frame.Left + dx, Frame.Top + dy);
        }

        // Frame resizing command
        public virtual void ResizeBy(int dw, int dh)
        {
            ResizeTo(Frame.Width + dw, Frame.Height + dh);
        }

        public virtual void ResizeTo(int width, int height)
        {
            User32.MoveWindow(Handle, Frame.Left, Frame.Top, width, height, true);
        }

        public virtual void CaptureMouse()
        {
        
        }

        public virtual void ReleaseMouse()
        {

        }

        public void Show()
        {
            bool result;
            result = User32.ShowWindow(Handle, ShowHide.ShowNormal);
        }

        public void Hide()
        {
            bool result;
            result = User32.ShowWindow(Handle, ShowHide.SW_HIDE);
        }

		public void StartTimer(uint millis)
		{
            IntPtr timer = User32.SetTimer(Handle, IntPtr.Zero, millis, null);
		}

		public void StopTimer()
		{
		}

        public IntPtr Handle
        {
            get{ return fHandle; }
        }

        public GDIDeviceContext DeviceContext
        {
            get
            {
                if (null == fDeviceContext)
                {
                    if (Handle == User32.GetDesktopWindow())
                        fDeviceContext = new GDIDeviceContext(User32.GetDC(IntPtr.Zero));
                    else
                        fDeviceContext = GDIDeviceContext.CreateForWindow(Handle);
                }

                return fDeviceContext;
            }
        }

        public IGraphPort GraphPort
        {
            get
            {
                if (fClientGraphPort == null)
                {
                    fClientGraphPort = new GDIRenderer(DeviceContext);
                }

                return fClientGraphPort;
            }
        }

        public static Keyboard.KeyMasks ModifierKeys
        {
            get
            {
                Keyboard.KeyMasks modifiers = Keyboard.KeyMasks.None;


                if (Keyboard.KeyState((int)Keyboard.VirtualKeyCodes.ShiftKey) < 0)
                    modifiers |= Keyboard.KeyMasks.Shift;
                if (Keyboard.KeyState((int)Keyboard.VirtualKeyCodes.ControlKey) < 0)
                    modifiers |= Keyboard.KeyMasks.Control;
                if (Keyboard.KeyState((int)Keyboard.VirtualKeyCodes.Menu) < 0)
                    modifiers |= Keyboard.KeyMasks.Alt;

                // Toggle keys
                if (Keyboard.KeyState((int)Keyboard.VirtualKeyCodes.Capital) < 0)
                    modifiers |= Keyboard.KeyMasks.CapsLock;
                if (Keyboard.KeyState((int)Keyboard.VirtualKeyCodes.Scroll) < 0)
                    modifiers |= Keyboard.KeyMasks.ScrollLock;
                if (Keyboard.KeyState((int)Keyboard.VirtualKeyCodes.NumLock) < 0)
                    modifiers |= Keyboard.KeyMasks.NumLock;

                return modifiers;
            }
        }

        //public static Mouse.ButtonIdentifiers MouseButtons
        //{
        //    get
        //    {
        //        Mouse.ButtonIdentifiers buttons = MouseButtons.None;


        //        if (Keyboard.KeyState((int)Keyboard.VirtualKeyCodes.LButton) < 0)
        //            buttons |= MouseButtons.Left;
        //        if (Keyboard.KeyState((int)Keyboard.VirtualKeyCodes.RButton) < 0)
        //            buttons |= MouseButtons.Right;
        //        if (Keyboard.KeyState((int)Keyboard.VirtualKeyCodes.MButton) < 0)
        //            buttons |= MouseButtons.Middle;
        //        if (Keyboard.KeyState((int)Keyboard.VirtualKeyCodes.XButton1) < 0)
        //            buttons |= MouseButtons.XButton1;
        //        if (Keyboard.KeyState((int)Keyboard.VirtualKeyCodes.XButton2) < 0)
        //            buttons |= MouseButtons.XButton2;

        //        return buttons;
        //    }
        //}


		~Win32Window()
		{
			Dispose(false);
		}

		public void Dispose()
		{
			Dispose(true);
			// Take yourself off the Finalization queue 
			// to prevent finalization code for this object
			// from executing a second time.
			GC.SuppressFinalize(this);
		}

		/// <summary>
		///    <para>
		///       Releases the handle associated with this window.
		///    </para>
		/// </summary>
		/// <remarks>
		///    <para>
		///       This method
		///       does not destroy the window's handle. Instead, it sets the handle's window
		///       procedure to the default window
		///       procedure. It sets the <see cref='System.WinForms.Win32Window.Handle'/> property to zero and calls <see cref='System.WinForms.Win32Window.OnHandleChange'/>
		///       to reflect the
		///       change.
		///    </para>
		///    <para>
		///       In addition, a window automatically calls this method if it receives a <see cref='Microsoft.Win32.Interop.win.WM_NCDESTROY'/> window message, indicating that
		///       Windows has destroyed the handle.
		///    </para>
		protected virtual void Dispose(bool disposing)
		{
			lock(this) 
			{
				/*
				 *                 if (Handle != 0) {
									SafeWin32Methods.RemoveProp(handle, windowAtom);
									handleCount--;
									if (handleCount == 0) {
										SafeWin32Methods.GlobalDeleteAtom((short)windowAtom);
										windowAtom = 0;
									}
									SafeWin32Methods.SetWindowLong(handle, NativeMethods.GWL_WNDPROC, defWindowProc);

									defWindowProc = 0;
									if (root.IsAllocated) {
										root.Free();
									}
									windowProc = null;
									handle = 0;
									OnHandleChange();
				*/
			}
		}
	}
