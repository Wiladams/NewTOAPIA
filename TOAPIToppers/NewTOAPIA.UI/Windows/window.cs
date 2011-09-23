

namespace NewTOAPIA.UI
{
    using System;
    using System.Collections.Generic;

    using TOAPI.Types;
    using TOAPI.User32;

    using NewTOAPIA.Drawing;
    using NewTOAPIA.Drawing.GDI;
    using NewTOAPIA.Graphics;

    public delegate void TimerHandler ();

    public partial class Window : IWindow, IObserver<MSG>, IObserver<KeyboardActivityArgs>, IObserver<MouseActivityArgs>, IObservable<MouseActivityArgs>, IObservable<PositionActivity>
    {
        protected WindowProc CallbackDelegate;
        protected User32MessageSource fMessageSource;
        protected WindowMessageJunction MessageJunction { get; set; }

        static Dictionary<IntPtr, Window> fWindowDictionary = new Dictionary<IntPtr, Window>();

        public event TimerHandler TimerEvent;

        #region Fields
        WindowProfile fWindowProfile;
        User32WindowClass fUser32WindowClass;
        User32WindowWrapper fPlatformWindow;

        IBrush fBackgroundBrush;
        Colorref fBackgroundColor;
        bool fEnabled;
        bool fActive;
        bool fMouseCaptured;
        bool fIsApplicationWindow;
        bool fIsWindowed = true;
        protected string fName;

        GDIContext fClientAreaContext;
        GDIContext fWholeWindowContext;

        GDIRenderer fClientAreaRenderer;
        GDIRenderer fWholeWindowRenderer;

        #region Observables
        Observable<MouseActivityArgs> fMouseObservable = new Observable<MouseActivityArgs>();
        Observable<PositionActivity> fPositionObservable = new Observable<PositionActivity>();
        #endregion
        #endregion

        #region Constructors

        public Window(string title, int x, int y, int width, int height)
            : this(title, x, y, width, height, IntPtr.Zero)
        {
        }

        public Window(string title, int x, int y, int width, int height, IntPtr parentWindow)
        {
            fIsApplicationWindow = true;
            fName = title;

            WindowClass = CreateWindowClass();
            WindowProfile = CreateWindowProfile(x,y,width,height);

            MessageJunction = new WindowMessageJunction();

            // Set ourself as an observer of the messages coming in
            MessageJunction.Subscribe((IObserver<KeyboardActivityArgs>)this);
            MessageJunction.Subscribe((IObserver<MouseActivityArgs>)this);
            MessageJunction.Subscribe((IObserver<MSG>)this);

            fMessageSource = new User32MessageSource(WindowClass, WindowProfile);
            fMessageSource.Subscribe(MessageJunction);


            // We need to be able to talk to the Window class, so we ask
            // the source for a handle on the window.
            PlatformWindow = fMessageSource.CreateWindow(title, IntPtr.Zero, WindowProfile);

            OnCreated();

            //BackgroundColor = (Colorref)Colorrefs.LtGray;
        }

        #endregion

        #region Properties
        public Colorref BackgroundColor
        {
            get { return fBackgroundColor; }
            set
            {
                fBackgroundColor = value;
                fBackgroundBrush = new GDIBrush(fBackgroundColor);
            }
        }

        public virtual User32WindowClass WindowClass
        {
            get { return fUser32WindowClass; }
            set { fUser32WindowClass = value; }
        }

        public virtual WindowProfile WindowProfile
        {
            get { return fWindowProfile; }
            set
            {
                fWindowProfile = value;
            }
        }

        public bool IsApplicationWindow
        {
            get { return fIsApplicationWindow; }
            set { fIsApplicationWindow = value; }
        }

        public User32WindowWrapper PlatformWindow
        {
            get { return fPlatformWindow; }
            set { fPlatformWindow = value; }
        }


        public IntPtr Handle
        {
            get
            {
                return fPlatformWindow.Handle;
            }
        }

        public RectangleI Frame
        {
            get { return fPlatformWindow.Frame; }
            set
            {
                //fPlatformWindow.WindowFrame = value;
            }
        }

        public string Title
        {
            set
            {
                fPlatformWindow.SetTitle(value);
            }
        }

        public RectangleI ClientRectangle
        {
            get { return fPlatformWindow.ClientRectangle; }
            set
            {
                // We want to change the window to match the specified
                // client area size.
                // Create a RECT structure representing the desired size.
                //RECT clientRect = (RECT)value;

                // Now ask Windows what the window dimensions should be to accomodate the 
                // specified client rectangle.
                //User32.AdjustWindowRectEx(ref clientRect, fUser32WindowClass.ClassStyle, MenuElementType, extStyle);

            }
        }

        public bool Capture
        {
            get { return fMouseCaptured; }
            set
            {
                if (value == true)
                {
                    fMouseCaptured = true;
                    User32.SetCapture(Handle);
                }
                else
                {
                    fMouseCaptured = false;
                    User32.SetCapture(IntPtr.Zero);
                }
            }
        }

        public double Opacity
        {
            get { return 1; }
            set
            {
                byte aByte = (byte)(value * 255);
                SetWindowAlpha(aByte);
            }
        }

        #endregion

        public IntPtr Callback(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam)
        {
        //    MSG m = MSG.Create(hWnd, msg, wParam, lParam);

        //    IntPtr result = IntPtr.Zero;

        //    if (hWnd == IntPtr.Zero)
        //    {
        //        result = User32.DefWindowProc(hWnd, msg, wParam, lParam);
        //        return result;
        //    }

        //    Window aWindow = null;
        //    if (!fWindowDictionary.TryGetValue(hWnd, out aWindow))
        //    {
        //        aWindow = this;
        //        fWindowDictionary.Add(hWnd, aWindow);
        //    }

        //    if ((msg == (int)WinMsg.WM_CREATE) || (msg == (int)WinMsg.WM_NCCREATE))
        //    {
        //        // If '0' is returned from here, the window creation
        //        // discontinues and the handle is deleted.
        //        result = User32.DefWindowProc(hWnd, msg, wParam, lParam);
        //        return result;
        //    }
        //    else
        //    {
        //        //DispatchObservable(m);
        //        //result = aWindow.OnMessage(aWindow, m);
        //    }

        //    return result;

            return IntPtr.Zero;
        }
        public virtual User32WindowClass CreateWindowClass()
        {
            CallbackDelegate = new WindowProc(this.Callback);

            User32WindowClass aClass = new User32WindowClass("NewTOAPIAWindow",
                (User32.CS_HREDRAW |
                User32.CS_VREDRAW |
                User32.CS_DBLCLKS |
                User32.CS_GLOBALCLASS |
                User32.CS_OWNDC),
                CallbackDelegate);

            return aClass;
        }

        public virtual WindowProfile CreateWindowProfile(int x, int y, int width, int height)
        {
            WindowProfile aProfile = new WindowProfile(fName,
                WindowStyle.OverlappedWindow, 
                ExtendedWindowStyle.AcceptFiles | ExtendedWindowStyle.OverLappedWindow,
                    x, y, width, height);

            return aProfile;
        }

        public virtual void Run()
        {
            fMessageSource.Run();
        }

        #region IMoveable
        public virtual IDisposable Subscribe(IObserver<PositionActivity> observer)
        {
            return fPositionObservable.Subscribe(observer);
        }

        /// <summary>
        /// Move the upper left corner of the window to the specified location.
        /// </summary>
        /// <param name="x">X Coordinate of upper left corner.</param>
        /// <param name="y">Y Coordinate of upper left corner.</param>
        /// <returns>true</returns>
        public virtual void MoveTo(int x, int y)
        {
            fPlatformWindow.MoveTo(x, y);
        }

        public virtual void MoveBy(int dx, int dy)
        {
            fPlatformWindow.MoveBy(dx, dy);
        }

        public virtual void OnMovedTo(int x, int y)
        {
        }

        public virtual void OnMovedBy(int dw, int dh)
        {
        }

        public virtual void OnMoving(int dw, int dh)
        {
        }
        #endregion

        #region IResizeable
        public virtual void ResizeBy(int dw, int dh)
        {
            fPlatformWindow.ResizeBy(dw, dh);
        }

        public virtual void ResizeTo(int width, int height)
        {
            fPlatformWindow.ResizeTo(width, height);
        }

        public virtual void OnMinimized()
        {
        }

        public virtual void OnMaximized(int width, int height)
        {
            OnResizedTo(width, height);
        }

        public virtual void OnResized()
        {
        }

        public virtual void OnResizedTo(int width, int height)
        {
        }

        /// <summary>
        /// This one is not actually used by the windowing interface.  Subclassers
        /// should implement OnResizedTo instead.
        /// </summary>
        /// <param name="dw"></param>
        /// <param name="dh"></param>
        public virtual void OnResizedBy(int dw, int dh)
        {
        }

        public virtual void OnResizing(int dw, int dh)
        {
        }

        #endregion

        #region IInteractor
        #region Keyboard Activity
        public virtual void OnNext(KeyboardActivityArgs ke)
        {
            switch (ke.AcitivityType)
            {
                case KeyActivityType.KeyDown:
                    OnKeyDown(ke);
                    break;

                case KeyActivityType.KeyUp:
                    OnKeyUp(ke);
                    break;

                case KeyActivityType.KeyChar:
                    OnKeyPress(ke);
                    break;
            }

        }

        public virtual void OnKeyDown(KeyboardActivityArgs ke)
        {
        }

        public virtual void OnKeyUp(KeyboardActivityArgs ke)
        {
        }

        public virtual void OnKeyPress(KeyboardActivityArgs ke)
        {
        }
        #endregion

        #region Mouse Activity
        // Reacting to the mouse
        public virtual IDisposable Subscribe(IObserver<MouseActivityArgs> observer)
        {
            return fMouseObservable.Subscribe(observer);
        }

        public virtual void OnNext(MouseActivityArgs e)
        {
            fMouseObservable.DispatchData(e);

            switch (e.ActivityType)
            {
                case MouseActivityType.MouseDown:
                    OnMouseDown(e);
                    break;

                case MouseActivityType.MouseMove:
                    OnMouseMove(e);
                    break;

                case MouseActivityType.MouseUp:
                    OnMouseUp(e);
                    break;

                case MouseActivityType.MouseEnter:
                    OnMouseEnter(e);
                    break;

                case MouseActivityType.MouseHover:
                    OnMouseHover(e);
                    break;

                case MouseActivityType.MouseLeave:
                    OnMouseLeave(e);
                    break;

                case MouseActivityType.MouseWheel:
                    OnMouseWheel(e);
                    break;
            }
        }

        public virtual void OnMouseDown(MouseActivityArgs e)
        {
        }

        /// <summary>
        /// OnMouseEnter
        /// 
        /// This gets called whenever the pointing device enters our frame.
        /// We want to do interesting things here like change the cursor shape
        /// to be whatever we require.
        /// </summary>
        /// <param name="e"></param>
        public virtual void OnMouseEnter(MouseActivityArgs e)
        {
        }

        public virtual void OnMouseHover(MouseActivityArgs e)
        {
        }

        public virtual void OnMouseLeave(MouseActivityArgs e)
        {
        }

        public virtual void OnMouseMove(MouseActivityArgs e)
        {
        }

        public virtual void OnMouseUp(MouseActivityArgs e)
        {
        }

        public virtual void OnMouseWheel(MouseActivityArgs e)
        {
        }
        #endregion

        public virtual void Focus()
        {
        }

        public virtual void LoseFocus()
        {
        }

        public bool Enabled
        {
            get { return fEnabled; }
            set { fEnabled = value; }
        }

        public bool Active
        {
            get { return fActive; }
            set { fActive = value; }
        }
        #endregion


        #region Device Contexts
        public GDIContext DeviceContextClientArea
        {
            get
            {
                return fClientAreaContext;

            }
        }

        public GDIContext DeviceContextWholeWindow
        {
            get
            {
                return fWholeWindowContext;
            }
        }
        #endregion

        public virtual void SwitchFullScreen()
        {
            MakeFullScreen(fIsWindowed);
        }

        public virtual void MakeFullScreen(bool enable)
        {
            if (enable)
            {
                // If it's already full screen, then just return
                if (!fIsWindowed)
                    return;

                int width = User32.GetSystemMetrics(SystemMetric.CXSCREEN);
                int height = User32.GetSystemMetrics(SystemMetric.CYSCREEN);

                User32.SetWindowLong(Handle, User32.GWL_STYLE, User32.WS_POPUP);

                User32.SetWindowPos(Handle, new IntPtr(User32.HWND_TOP), 0, 0, width, height,
                    User32.SWP_NOZORDER | User32.SWP_SHOWWINDOW);

                fIsWindowed = false;
            }
            else
            {
                if (fIsWindowed)
                    return;

                RECT R = new RECT(0, 0, 800, 600);
                User32.AdjustWindowRectEx(ref R, User32.WS_OVERLAPPEDWINDOW, false, 0);
                fIsWindowed = true;

                User32.SetWindowLong(Handle, User32.GWL_STYLE, User32.WS_OVERLAPPEDWINDOW);

                User32.SetWindowPos(Handle, new IntPtr(User32.HWND_TOP), 100, 100, R.Right, R.Bottom,
                    User32.SWP_NOZORDER | User32.SWP_SHOWWINDOW);
            }
        }

        public virtual IGraphPort ClientAreaGraphPort
        {
            get
            {
                if (null == fClientAreaRenderer)
                {
                    fClientAreaRenderer = new GDIRenderer(fClientAreaContext);
                }

                return fClientAreaRenderer;
            }
        }

        public virtual IGraphPort GraphPort
        {
            get
            {
                return ClientAreaGraphPort;
            }
        }

        // From IDrawable
        public virtual void OnPaint(DrawEvent devent)
        {
        }

        public virtual void OnControlCommand(IntPtr controlParam)
        {
        }

        public virtual void OnMenuItemSelected(int commandParam)
        {
        }

        /// <summary>
        /// Once the CreateWindow call returns, we have a valid Window
        /// handle.  We use this opportunity to do a couple of things.
        /// First, we add the window handle to the dictionary of windows
        /// so that the message processing loop can find the window and 
        /// dispatch messages back to it.
        /// 
        /// Then we create the client area device context and renderer
        /// and the Whole Window context and renderer. This allows us 
        /// to do rendering to the entire window or client area.
        /// </summary>
        public virtual void OnCreated()
        {
            //Console.WriteLine("Window.OnCreated : {0}", Handle);
            if (!fWindowDictionary.ContainsKey(Handle))
                fWindowDictionary.Add(Handle, this);

            // At this point, if we are using CS.OWNDC, the Device context
            // for the entire window, and for the client area are also valid.
            // So, we can setup those handles, and create the renderers as well.
            fClientAreaContext = GDIContext.CreateForWindowClientArea(Handle);
            fClientAreaRenderer = new GDIRenderer(fClientAreaContext);

            // Get device context for window title area
            // and create a GDIRenderer for it.
            fWholeWindowContext = GDIContext.CreateForWholeWindow(Handle);
            fWholeWindowRenderer = new GDIRenderer(fWholeWindowContext);
        }

        // from WM_DESTROY
        public virtual void OnDestroy()
        {
        }

        // This routine is called after the window has 
        // been destroyed.  It is time to clean up any
        // specific resources.
        public virtual void OnDestroyed()
        {
            if (IsApplicationWindow)
                User32.PostQuitMessage(0);
        }

        public virtual void OnEnable(bool enabled)
        {
        }

        // OnQuit()
        // This is called after all the fun has happened.  The
        // message loop has finished, and this is our last chance
        // to do something interesting.
        public virtual void OnQuit()
        {
        }

        public virtual bool OnCloseRequested()
        {
            return true;	// By default, it's OK to close	
        }

        public virtual void OnSetFocus()
        {
            //fCaret.FocusWindow = this;
            Win32Cursor.SetFocusWindow(PlatformWindow);
        }

        public virtual void OnKillFocus()
        {
            //fCaret.FocusWindow = null;
            Win32Cursor.SetFocusWindow(null);
        }

        public virtual void OnTimer()
        {
            if (TimerEvent != null)
                TimerEvent();
        }

        // Things we need to tell the platform specific window

        public virtual void Invalidate()
        {
            fPlatformWindow.Invalidate();
        }

        public virtual void Invalidate(RectangleI rect)
        {
            fPlatformWindow.Invalidate(rect);
        }

        public virtual void Validate()
        {
            fPlatformWindow.Validate();
        }

        public virtual void SetWindowRegion(GDIRegion aRegion)
        {
            fPlatformWindow.SetWindowRegion(aRegion);
        }

        public virtual void CaptureMouse() { }
        public virtual void ReleaseMouse() { }

        public virtual bool SetWindowAlpha(byte alpha)
        {
            return fPlatformWindow.SetWindowAlpha(alpha);
        }

        public virtual void Close()
        {
        }

        public virtual void Destroy()
        {
            fPlatformWindow.Destroy();
        }

        public virtual void Show(ShowHide hideCommand)
        {
            bool result;
            result = User32.ShowWindow(Handle, (int)hideCommand);
            User32.UpdateWindow(Handle);
        }

        public virtual void Show()
        {
            fPlatformWindow.Show();
        }

        public virtual void Hide()
        {
            fPlatformWindow.Hide();
        }

        public virtual void StartTimer(uint millis)
        {
            fPlatformWindow.StartTimer(millis);
        }

        public virtual void StopTimer()
        {
            fPlatformWindow.StopTimer();
        }



        public virtual IntPtr OnEraseBackground()
        {
            // Client area graphport, fill rectangle 
            // using background color.
            ClientAreaGraphPort.FillRectangle(fBackgroundBrush, ClientRectangle);

            return new IntPtr(1);
        }


        #region Static Construction Helpers
        public static Window Create(string title, RectangleI frame, User32WindowClass aClass, WindowProfile aProfile)
        {
            Window win = new Window(title, frame.Left, frame.Top, frame.Width, frame.Height);

            return win;
        }
        #endregion
    }
}