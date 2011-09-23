using System;
using System.Drawing;
using System.Threading;

using TOAPI.Kernel32;
using TOAPI.Types;
using TOAPI.User32;

using NewTOAPIA.Drawing;

namespace NewTOAPIA.UI
{
    /// <summary>
    /// Class: Win32Window
    /// 
    /// </summary>
    //public class Win32Window : IHandle, IPlatformWindow, IMoveableReaction, IResizeReaction
    //{
    //    #region Fields
    //    protected static string gWindowClassName = "Win32Window";
    //    protected static bool gClassRegistered = false;

    //    // This set of fields are used to initialize and register
    //    // the window class.
    //    private ushort fClassAtom;		// atom representing class registration
    //    private WNDCLASSEXW fWndClass;
    //    private string fClassName;
    //    private uint fClassStyle;
    //    private MessageProc fWindowProc;
    //    private IntPtr fAppInstance;	// Application Instance

    //    // Properties of the created window
    //    protected IntPtr fHandle;			// Handle to Win32 window
    //    private Rectangle fFrame;
    //    private string fTitle;

    //    protected IWindow fDelegateWindow;


    //    protected GDIContext fDeviceContextClientArea;	// Device context for client area
    //    protected GDIContext fWindowDeviceContext;  // Device context for whole window
    //    #endregion

    //    #region Constructors
    //    public Win32Window(string title, int x, int y, int width, int height)
    //    {
    //        fClassName = gWindowClassName;
    //        fClassStyle = (User32.CS_HREDRAW | User32.CS_VREDRAW | User32.CS_DBLCLKS | User32.CS_GLOBALCLASS | User32.CS_OWNDC);
    //        fWindowProc = new MessageProc(this.Callback);

    //        // Instance variables
    //        fFrame = new Rectangle(x, y, width, height);
    //        fTitle = title;

    //        // Register the window
    //        RegisterWindowClass();
    //    }

    //    public Win32Window(string className, uint classStyle, MessageProc wndProc)
    //    {
    //        fClassName = className;
    //        fClassStyle = classStyle;
    //        fWindowProc = wndProc;

    //        RegisterWindowClass();
    //    }
    //    #endregion

    //    #region Properties
    //    public IWindow DelegateWindow
    //    {
    //        get { return fDelegateWindow; }
    //        set
    //        {
    //            fDelegateWindow = value;
    //        }
    //    }

    //    #region Device Contexts
    //    public GDIContext DeviceContextClientArea
    //    {
    //        get
    //        {
    //            return fDeviceContextClientArea;

    //        }
    //    }

    //    public GDIContext DeviceContextWholeWindow
    //    {
    //        get
    //        {
    //            return fWindowDeviceContext;
    //        }
    //    }
    //    #endregion
        

    //    /// <summary>
    //    /// The frame of the window.  This is the space in pixels that the windows
    //    /// takes on the desktop.  If the window has not actually been created
    //    /// as yet, the value returned is the space the frame will take.
    //    /// If the window has been created, this returns the actual space taken
    //    /// by the window.
    //    /// </summary>
    //    public Rectangle Frame
    //    {
    //        get
    //        {
    //            if (IntPtr.Zero != fHandle)
    //            {
    //                RECT aFrame = new RECT();
    //                User32.GetWindowRect(Handle, out aFrame);

    //                return new Rectangle(aFrame.X, aFrame.Y, aFrame.Width, aFrame.Height) ;
    //            }
    //            else
    //                return fFrame;
    //        }

    //        set
    //        {
    //            fFrame = value;
    //            if (IntPtr.Zero != fHandle)
    //            {
    //                User32.SetWindowPos(Handle, IntPtr.Zero, fFrame.X, fFrame.Y, fFrame.Width, fFrame.Height, 0);
    //            }
    //        }
    //    }

    //    public Rectangle ClientRectangle
    //    {
    //        get
    //        {
    //            RECT aFrame = new RECT();
    //            User32.GetClientRect(Handle, out aFrame);

    //            return new Rectangle(aFrame.X, aFrame.Y, aFrame.Width, aFrame.Height);
    //        }
    //    }

    //    public IntPtr Handle
    //    {
    //        get { return fHandle; }
    //        set { fHandle = value; }
    //    }



    //    public string Title
    //    {
    //        get { return fTitle; }
    //        set { fTitle = value; }
    //    }

    //    #endregion


    //    protected virtual void RegisterWindowClass()
    //    {
    //        if (gClassRegistered)
    //            return;

    //        fAppInstance = Kernel32.GetModuleHandle(null);
    //        fWndClass = new WNDCLASSEXW();
    //        fWndClass.Init();

    //        fWndClass.style = fClassStyle;
    //        fWndClass.lpfnWndProc = fWindowProc;
    //        fWndClass.cbClsExtra = 0;
    //        fWndClass.cbWndExtra = 0;
    //        fWndClass.hInstance = fAppInstance;
    //        fWndClass.hIcon = IntPtr.Zero;
    //        fWndClass.hCursor = IntPtr.Zero;
    //        fWndClass.hbrBackground = (IntPtr)(User32.COLOR_BACKGROUND + 1);
    //        fWndClass.lpszMenuName = null;
    //        fWndClass.lpszClassName = fClassName;
    //        fWndClass.hIconSm = IntPtr.Zero;

    //        // Register the window class. 
    //        fClassAtom = User32.RegisterClassExW(ref fWndClass);
    //        uint error = Kernel32.GetLastError();

    //        if (fClassAtom == 0)
    //        {
    //            fWindowProc = null;
    //            //throw new Win32Exception();
    //        }

    //        gClassRegistered = true;
    //    }

    //    public virtual void CreateWindow()
    //    {
    //        fHandle = User32.CreateWindowExW(
    //            User32.WS_EX_ACCEPTFILES | User32.WS_EX_OVERLAPPEDWINDOW,
    //            fClassName,
    //            this.fTitle,
    //            User32.WS_OVERLAPPEDWINDOW,
    //            fFrame.Left,
    //            fFrame.Top,
    //            fFrame.Width,
    //            fFrame.Height,
    //            IntPtr.Zero,
    //            IntPtr.Zero,
    //            fAppInstance,
    //            IntPtr.Zero);


    //    }



    //    /// <summary>
    //    /// </summary>
    //    // The message processing chain is like this:
    //    // First, in Start(), we're pulling messages off of the 
    //    // message queue using GetMessage().  
    //    // 2) Translate that message into a proper keyboard 
    //    //    message using TranslateMessage()
    //    // 3) Dispatch the message using the WindowProc for 
    //    //    the window, namely, fWindowProc.
    //    // 4) fWindowProc being this.Callback(), Callback() then
    //    //    packages up the message into a Message object, and
    //    //    Calls this.DispatchMessage()
    //    // 5) If DispatchMessage() can deal with it alone, it
    //    //    will deal with it.  If it can't, then it then
    //    //    calls the default window dispatch function
    //    // 6) DefWindowProc
    //    //
    //    // And that's about it.
    //    public virtual void Start()
    //    {
    //        MSG msg = new MSG();

    //        while (User32.GetMessage(out msg, IntPtr.Zero, 0, 0))
    //        {
    //            /// Messages that come in here are typically keyboard
    //            /// mouse, and timer events.
    //            /// For the keyboard ones, there may be Hot-Key combinations
    //            /// that need to be translated into regular characters.  So,
    //            /// we first call the TranslateMessage() function to do that.
    //            User32.TranslateMessage(ref msg);

    //            /// Now, we want to actually handle the message.  
    //            /// There are three different cases where we will receive
    //            /// messages.
    //            /// 1) Thread messages - These messages have a msg.hWnd == 0
    //            /// Right now we will do nothing with those.
    //            /// 2) Messages with a msg.hWnd == Handle
    //            /// These are messages that are meant for the specific window
    //            /// that this code is dealing with.  We will dispatch these directly
    //            /// because we know which window they are meant for, and we don't need 
    //            /// the system to figure that out for us.
    //            /// 3) Messages with a msg.hWnd != 0 && msg.hWnd != Handle
    //            /// These are messages that are meant for a child window of our managed
    //            /// windows.  This would be a User32 Window that is a child window
    //            /// of the one we are handling.  These we allow the system to dispatch
    //            /// as it normally would.
    //            /// By covering these three cases, we're all set.

    //            //if (msg.hWnd == Handle)
    //            //	OnMessage(this, msg);
    //            //else if (msg.hWnd != IntPtr.Zero)
    //            User32.DispatchMessage(ref msg);
    //            //else
    //            // It's a thread message and we need to do something about that

    //        }

    //        Dispose();

    //        // We must have received the WM_QUIT message
    //        // So, tell the delegate window it's time to quit before
    //        // we terminate the thread.
    //        if (null != fDelegateWindow)
    //            fDelegateWindow.OnQuit();
    //    }

    //    /// <summary>
    //    /// This is where all the messages from the system come to.
    //    /// We instantly turn them into Message objects so that we can
    //    /// dispatch them throughout the rest of the system.
    //    /// </summary>
    //    /// <param name="hWnd"> </param>
    //    /// <param name="msg"> </param>
    //    /// <param name="wParam"> </param>
    //    /// <param name="lParam"> </param>
    //    public virtual int Callback(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
    //    {
    //        MSG m = MSG.Create(hWnd, msg, (uint)wParam.ToInt32(), lParam.ToInt32());
    //        //Console.WriteLine("Win32Window.Callback - {0}", MessageDecoder.MsgToString(msg));

    //        int retValue = OnMessage(this, m);

    //        return retValue;
    //    }

    //    protected virtual int OnMessage(Object sender, MSG msg)
    //    {
    //        //Console.WriteLine("Win32Window.OnMessage(default) - {0}", MessageDecoder.MsgToString(msg.message));

    //        int result = 0;

    //        switch (msg.message)
    //        {
    //            //
    //            // KeyboardDevice messages
    //            //
    //            //case MSG.WM_SYSKEYDOWN:
    //            //case MSG.WM_SYSKEYUP:

    //            case MSG.WM_KEYDOWN:
    //                WmKeyDown(ref msg);
    //                break;

    //            case MSG.WM_KEYUP:
    //                WmKeyUp(ref msg);
    //                break;

    //            //
    //            // These messages are sent to the windowproc directly.
    //            // They do not come from the posted message queue.
    //            //
    //            //
    //            // These character events are the ones generated from
    //            // the TranslateMessage() function, after a WM_KEYDOWN
    //            // message.
    //            //case MSG.WM_SYSCHAR:
    //            case MSG.WM_CHAR:
    //                WmKeyChar(ref msg);
    //                break;

    //            //case MSG.WM_COMMAND:
    //            //	fDelegateWindow.OnCommand(Utils.Loword(msg.wParam));
    //            //	break;
    //            // 
    //            // MouseDevice processing messages
    //            //
    //            // Left button processing
    //            case MSG.WM_LBUTTONDBLCLK:
    //                WmMouseActivity(msg.lParam, MouseButtonActivity.LeftButtonDown, 0, 2);
    //                break;
    //            case MSG.WM_LBUTTONDOWN:
    //                WmMouseActivity(msg.lParam, MouseButtonActivity.LeftButtonDown, 0, 1);
    //                break;
    //            case MSG.WM_LBUTTONUP:
    //                WmMouseActivity(msg.lParam, MouseButtonActivity.LeftButtonUp, 0, 1);
    //                break;

    //            // Middle button processing
    //            case MSG.WM_MBUTTONDBLCLK:
    //                WmMouseActivity(msg.lParam, MouseButtonActivity.MiddleButtonDown, 0, 2);
    //                break;
    //            case MSG.WM_MBUTTONDOWN:
    //                WmMouseActivity(msg.lParam, MouseButtonActivity.MiddleButtonDown, 0, 1);
    //                break;
    //            case MSG.WM_MBUTTONUP:
    //                WmMouseActivity(msg.lParam, MouseButtonActivity.MiddleButtonUp, 0, 1);
    //                break;

    //            // Right button processing
    //            case MSG.WM_RBUTTONDBLCLK:
    //                WmMouseActivity(msg.lParam, MouseButtonActivity.RightButtonDown, 0, 2);
    //                break;
    //            case MSG.WM_RBUTTONDOWN:
    //                WmMouseActivity(msg.lParam, MouseButtonActivity.RightButtonDown, 0, 1);
    //                break;
    //            case MSG.WM_RBUTTONUP:
    //                WmMouseActivity(msg.lParam, MouseButtonActivity.RightButtonUp, 0, 1);
    //                break;


    //            case MSG.WM_MOUSEHOVER:
    //                //WmMouseHover(ref msg);
    //                break;
    //            case MSG.WM_MOUSELEAVE:
    //                //WmMouseLeave(ref msg);
    //                break;
    //            case MSG.WM_MOUSEMOVE:
    //                WmMouseActivity(msg.lParam, MouseButtonActivity.None, 0, 0);
    //                break;
    //            case MSG.WM_MOUSEWHEEL:
    //                WmMouseActivity(msg.lParam, MouseButtonActivity.MouseWheel, 0, 0);
    //                break;

    //            // Then paint events
    //            case MSG.WM_PAINT:
    //                WmPaint();
    //                break;


    //            case MSG.WM_CLOSE:
    //                // we give the delegate window a chance to say something about whether
    //                // the window should close or not.  If OnCloseRequested returns 'true'
    //                // the delegate is saying "it's ok to close", so we then call the default
    //                // window procedure to proceed.
    //                // If the delegate window returns 'false', then the message is essentially 
    //                // swallowed without any action.
    //                if (null != fDelegateWindow)
    //                {
    //                    if (fDelegateWindow.OnCloseRequested())
    //                        result = User32.DefWindowProc(msg.hWnd, msg.message, new IntPtr((int)msg.wParam), new IntPtr(msg.lParam));
    //                } else
    //                    result = User32.DefWindowProc(msg.hWnd, msg.message, new IntPtr((int)msg.wParam), new IntPtr(msg.lParam));
    //                break;


    //            //case MSG.WM_ERASEBKGND:
    //            //    // the msg.wparam is a pointer to a device context
    //            //    // So, we create a renderer to it, and pass it along.
    //            //    //GDIRenderer rend = new GDIRenderer(new IntPtr(msg.wParam));
    //            //    result = fDelegateWindow.OnEraseBackground(null);
    //            //break;

    //            case MSG.WM_MOVE:
    //                OnMovedTo(BitUtils.Loword((int)msg.lParam), BitUtils.Hiword((int)msg.lParam));
    //                break;

    //            case MSG.WM_MOVING:
    //                if (null != fDelegateWindow)
    //                    fDelegateWindow.OnMoving(BitUtils.Loword((int)msg.lParam), BitUtils.Hiword((int)msg.lParam));
    //            break;

    //            case MSG.WM_SIZE:
    //                OnResizedBy(BitUtils.Loword((int)msg.lParam), BitUtils.Hiword((int)msg.lParam));
    //                break;

    //            //case MSG.WM_SIZING:
    //            //	fDelegateWindow.OnResizing(Utils.Loword(msg.lParam), Utils.Hiword(msg.lParam));
    //            //break;

    //            case MSG.WM_KILLFOCUS:
    //                //fCaret.FocusWindow = null;
    //                Win32Cursor.Current.FocusWindow = null;
    //                if (null != fDelegateWindow)
    //                    fDelegateWindow.OnKillFocus();
    //                break;

    //            case MSG.WM_SETFOCUS:
    //                //fCaret.FocusWindow = this;
    //                Win32Cursor.Current.FocusWindow = this;
    //                if (null != fDelegateWindow)
    //                    fDelegateWindow.OnSetFocus();
    //                break;

    //            case MSG.WM_SETCURSOR:
    //                Win32Cursor.Current = Win32Cursor.Arrow;

    //                //    // return '1' to halt further processing
    //                //    // otherwise return '0'
    //                result = 1;
    //                break;

    //            case MSG.WM_NCCREATE:
    //                // We receive this message during the 'CreateWindowEx' function call.
    //                // The window handle is valid, so we can do some setup based on
    //                // that, like setting our handle.  
    //                fHandle = msg.hWnd;

    //                // At this point, if we are using CS.OWNDC, the Device context
    //                // for the entire window, and for the client area are also valid.
    //                // So, we can setup those handles, and create the renderers as well.
    //                fDeviceContextClientArea = GDIContext.CreateForWindowClientArea(msg.hWnd);

    //                // Get device context for the entire window, including the title and trim
    //                fWindowDeviceContext = GDIContext.CreateForWholeWindow(msg.hWnd);


    //                // If '0' is returned from here, the window creation
    //                // discontinues and the handle is deleted.
    //                result = User32.DefWindowProc(msg.hWnd, msg.message, new IntPtr((int)msg.wParam), new IntPtr(msg.lParam));
    //                break;

    //            // By the time we get this message, the window handle has already
    //            // been destroyed.  Posting the WM_QUIT message will clean out the 
    //            // thread's message queue, and some other stuff.
    //            // This is called after WM_DESTROY, and after all child windows
    //            // have already been destroyed.
    //            case MSG.WM_NCDESTROY:
    //                User32.PostQuitMessage(0);
    //                break;


    //            // And finally timing events
    //            case MSG.WM_TIMER:
    //                WmTimer(ref msg);
    //                break;

    //            default:
    //                // Do what should be done by default if we don't handle it specifically
    //                //Console.WriteLine("Window.DispatchMessage(default) - {0}", MessageDecoder.MsgToString(msg.message));
    //                result = User32.DefWindowProc(msg.hWnd, msg.message, new IntPtr((int)msg.wParam), new IntPtr(msg.lParam));
    //                break;
    //        }

    //        return result;
    //    }

    //    /// <summary>
    //    /// These various 'WmXXX' method calls are used to deal with the various events from the 
    //    /// message loop.  In most cases, they pass parameters along to the appropriate 'OnXXX' 
    //    /// method call in the delegate.
    //    /// </summary>

    //    public void WmPaint()
    //    {
    //        PAINTSTRUCT ps = new PAINTSTRUCT();
    //        User32.BeginPaint(Handle, out ps);

    //        OnPaint(ref ps);

    //        // If "BeginPaint/EndPaint", are not used, we have to 
    //        // explicitly call "Validate()".  If we don't, Windows will
    //        // keep sending an enless stream of WM_PAINT messages until we do
    //        //Validate();
    //        User32.EndPaint(Handle, ref ps);
    //    }

    //    public virtual void OnPaint(ref PAINTSTRUCT aStruct)
    //    {
    //    }

    //    /// <summary>
    //    /// Called to handle WM_KEYCHAR messages.
    //    /// </summary>
    //    /// <param name="m"></param>
    //    /// <returns></returns>
    //    private bool WmKeyChar(ref MSG m)
    //    {
    //        KeyboardActivityArgs ke = new KeyboardActivityArgs(null,KeyActivityType.KeyChar, 0, m.lParam, (char)m.wParam);

    //        OnKeyboardActivity(ke);

    //        return true;
    //    }

    //    private bool WmKeyDown(ref MSG m)
    //    {
    //        KeyboardActivityArgs kbde = new KeyboardActivityArgs(null,KeyActivityType.KeyDown, (VirtualKeyCodes)(((int)m.wParam) | (int)ModifierKeys), m.lParam, '\0');

    //        OnKeyboardActivity(kbde);

    //        return true;
    //    }

    //    private bool WmKeyUp(ref MSG m)
    //    {
    //        //KeyEventArgs ke = null;

    //        //ke = new KeyEventArgs((VirtualKeyCodes)(((int)m.wParam) | (int)ModifierKeys),
    //        //    m.lParam);

    //        KeyboardActivityArgs kbde = new KeyboardActivityArgs(null,KeyActivityType.KeyUp, (VirtualKeyCodes)(((int)m.wParam) | (int)ModifierKeys), m.lParam, '\0');

    //        OnKeyboardActivity(kbde);

    //        return true;
    //    }

    //    protected virtual void OnKeyboardActivity(KeyboardActivityArgs args)
    //    {
    //        if (null != fDelegateWindow)
    //            fDelegateWindow.OnKeyboardActivity(this, args);
    //    }

    //    void WmMouseActivity(int lParam, MouseButtonActivity buttonActivity, int delta, int clicks)
    //    {
    //        short x = (short)BitUtils.Loword(lParam);
    //        short y = (short)BitUtils.Hiword(lParam);

    //        MouseActivityArgs args = new MouseActivityArgs(null, RawMouseEventType.MoveRelative, buttonActivity, x, y, delta, clicks);

    //        OnMouseActivity(args);
    //    }

    //    protected virtual void OnMouseActivity(MouseActivityArgs ma)
    //    {
    //        if (null != fDelegateWindow)
    //            fDelegateWindow.OnMouseActivity(this, ma);
    //    }

    //    private void WmTimer(ref MSG m)
    //    {
    //        //MSG msg = new MSG();
    //        //msg.hWnd = m.hWnd;
    //        //msg.lParam = m.lParam;
    //        //msg.message = m.message;
    //        //msg.wParam = m.wParam;

    //        if (null != fDelegateWindow)
    //            fDelegateWindow.OnTimer();
    //    }

    //    public virtual bool SetWindowAlpha(byte alpha)
    //    {
    //        int res1;

    //        res1 = User32.GetWindowLong(Handle, User32.GWL_EXSTYLE);

    //        // If the alpha is == 255, then turn off layered window
    //        if (255 == alpha)
    //        {
    //            User32.SetWindowLong(Handle, User32.GWL_EXSTYLE, res1 & ~User32.WS_EX_LAYERED);

    //            return true;
    //        }

    //        // If we are here, then opacity is less than 255 (opaque), so
    //        // we need to first ensure the window is a layered window.
    //        User32.SetWindowLong(Handle, User32.GWL_EXSTYLE, res1 | User32.WS_EX_LAYERED);

    //        // Next we setup the alpha value.
    //        bool res2 = User32.SetLayeredWindowAttributes(Handle, 0, alpha, User32.LWA_ALPHA);

    //        return true;
    //    }

    //    #region IPlatformWindowBase
    //    /// <summary>
    //    /// Invalidate the entire client area.
    //    /// </summary>
    //    public virtual void Invalidate()
    //    {
    //        RECT rect = new RECT(0, 0, 0, 0);

    //        User32.GetClientRect(Handle, out rect);
    //        User32.InvalidateRect(Handle, ref rect, true);
    //    }

    //    public virtual void Invalidate(Rectangle rect)
    //    {
    //        RECT r = new RECT(rect.X, rect.Y, rect.Width, rect.Height);
    //        User32.InvalidateRect(Handle, ref r, true);
    //    }

    //    public virtual void Validate()
    //    {
    //        RECT rect = new RECT(0, 0, 0, 0);
    //        User32.GetClientRect(Handle, out rect);
    //        User32.ValidateRect(Handle, ref rect);
    //    }

    //    public virtual void SetWindowRegion(GDIRegion aRegion)
    //    {
    //        RegionCombineType retValue = (RegionCombineType)User32.SetWindowRgn(Handle, aRegion.Handle, true);
    //    }

    //    /// <summary>
    //    /// Move the window to a specific location.
    //    /// </summary>
    //    /// <param name="x">The X coordinate of the window frame's upper left corner.</param>
    //    /// <param name="y">The Y coordinate of the window frame's upper left corner.</param>
    //    /// <returns>Always returns 'true'.</returns>
    //    public virtual void MoveTo(int x, int y)
    //    {
    //        bool retValue = User32.MoveWindow(Handle, x, y, fFrame.Width, fFrame.Height, true);
    //    }

    //    public virtual void OnMovedTo(int x, int y)
    //    {
    //        if (null != fDelegateWindow)
    //            fDelegateWindow.OnMovedTo(x, y);
    //    }

    //    public virtual void OnMoving(int x, int y)
    //    {
    //    }

    //    /// <summary>
    //    /// Move the window by a specied amount.
    //    /// </summary>
    //    /// <param name="dx">The distance to move in the x-axis.</param>
    //    /// <param name="dy">The number of pixels to move in the y-axis.</param>
    //    /// <returns>Always returns true.</returns>
    //    public virtual void MoveBy(int dx, int dy)
    //    {
    //        MoveTo(Frame.Left + dx, Frame.Top + dy);
    //    }

    //    public virtual void OnMovedBy(int dx, int dy)
    //    {
    //    }

    //    // Frame resizing command
    //    public virtual void ResizeBy(int dw, int dh)
    //    {
    //        ResizeTo(Frame.Width + dw, Frame.Height + dh);
    //    }

    //    public virtual void OnResizedBy(int dw, int dh)
    //    {
    //        if (null != fDelegateWindow)
    //            fDelegateWindow.OnResizedBy(dw, dh);
    //    }

    //    public virtual void OnResizing(int dw, int dy)
    //    {
    //    }

    //    public virtual void ResizeTo(int width, int height)
    //    {
    //        Rectangle frame = Frame;
    //        User32.MoveWindow(Handle, frame.Left, frame.Top, width, height, true);
    //    }

    //    public virtual void OnResizedTo(int width, int height)
    //    {
    //        if (null != fDelegateWindow)
    //            fDelegateWindow.OnResizedTo(width, height);
    //    }

    //    public virtual void CaptureMouse()
    //    {

    //    }

    //    public virtual void ReleaseMouse()
    //    {

    //    }

    //    public void Destroy()
    //    {
    //        bool result = User32.DestroyWindow(Handle);
    //    }

    //    public void Show()
    //    {
    //        bool result;
    //        result = User32.ShowWindow(Handle, (int)ShowHide.ShowNormal);
    //    }

    //    public void Hide()
    //    {
    //        bool result;
    //        result = User32.ShowWindow(Handle, (int)ShowHide.Hide);
    //    }

    //    public void StartTimer(uint millis)
    //    {
    //        IntPtr timer = User32.SetTimer(Handle, IntPtr.Zero, millis, null);
    //    }

    //    public void StopTimer()
    //    {
    //    }

    //    #endregion

    //    #region Static Helpers
    //    public static KeyMasks ModifierKeys
    //    {
    //        get
    //        {
    //            KeyMasks modifiers = KeyMasks.None;


    //            if (KeyboardDevice.KeyState((int)VirtualKeyCodes.ShiftKey) < 0)
    //                modifiers |= KeyMasks.Shift;
    //            if (KeyboardDevice.KeyState((int)VirtualKeyCodes.ControlKey) < 0)
    //                modifiers |= KeyMasks.Control;
    //            if (KeyboardDevice.KeyState((int)VirtualKeyCodes.Menu) < 0)
    //                modifiers |= KeyMasks.Alt;

    //            // Toggle keys
    //            if (KeyboardDevice.KeyState((int)VirtualKeyCodes.Capital) < 0)
    //                modifiers |= KeyMasks.CapsLock;
    //            if (KeyboardDevice.KeyState((int)VirtualKeyCodes.Scroll) < 0)
    //                modifiers |= KeyMasks.ScrollLock;
    //            if (KeyboardDevice.KeyState((int)VirtualKeyCodes.NumLock) < 0)
    //                modifiers |= KeyMasks.NumLock;

    //            return modifiers;
    //        }
    //    }

    //    //public static MouseDevice.ButtonIdentifiers MouseButtons
    //    //{
    //    //    get
    //    //    {
    //    //        MouseDevice.ButtonIdentifiers buttons = MouseButtons.None;


    //    //        if (KeyboardDevice.KeyState((int)VirtualKeyCodes.LButton) < 0)
    //    //            buttons |= MouseButtons.Left;
    //    //        if (KeyboardDevice.KeyState((int)VirtualKeyCodes.RButton) < 0)
    //    //            buttons |= MouseButtons.Right;
    //    //        if (KeyboardDevice.KeyState((int)VirtualKeyCodes.MButton) < 0)
    //    //            buttons |= MouseButtons.Middle;
    //    //        if (KeyboardDevice.KeyState((int)VirtualKeyCodes.XButton1) < 0)
    //    //            buttons |= MouseButtons.XButton1;
    //    //        if (KeyboardDevice.KeyState((int)VirtualKeyCodes.XButton2) < 0)
    //    //            buttons |= MouseButtons.XButton2;

    //    //        return buttons;
    //    //    }
    //    //}
    //    #endregion

    //    #region IDisposable
    //    ~Win32Window()
    //    {
    //        Dispose(false);
    //    }

    //    public void Dispose()
    //    {
    //        Dispose(true);
    //        // Take yourself off the Finalization queue 
    //        // to prevent finalization code for this object
    //        // from executing a second time.
    //        GC.SuppressFinalize(this);
    //    }

    //    /// <summary>
    //    ///    <para>
    //    ///       Releases the handle associated with this window.
    //    ///    </para>
    //    /// </summary>
    //    /// <remarks>
    //    ///    <para>
    //    ///       This method
    //    ///       does not destroy the window's handle. Instead, it sets the handle's window
    //    ///       procedure to the default window
    //    ///       procedure. It sets the <see cref='System.WinForms.Win32Window.Handle'/> property to zero and calls <see cref='System.WinForms.Win32Window.OnHandleChange'/>
    //    ///       to reflect the
    //    ///       change.
    //    ///    </para>
    //    ///    <para>
    //    ///       In addition, a window automatically calls this method if it receives a <see cref='Microsoft.Win32.Interop.win.WM_NCDESTROY'/> window message, indicating that
    //    ///       Windows has destroyed the handle.
    //    ///    </para>
    //    protected virtual void Dispose(bool disposing)
    //    {
    //        lock (this)
    //        {

    //            /*
    //             *                 if (Handle != 0) {
    //                                SafeWin32Methods.RemoveProp(handle, windowAtom);
    //                                handleCount--;
    //                                if (handleCount == 0) {
    //                                    SafeWin32Methods.GlobalDeleteAtom((short)windowAtom);
    //                                    windowAtom = 0;
    //                                }
    //                                SafeWin32Methods.SetWindowLong(handle, NativeMethods.GWL_WNDPROC, defWindowProc);

    //                                defWindowProc = 0;
    //                                if (root.IsAllocated) {
    //                                    root.Free();
    //                                }
    //                                windowProc = null;
    //                                handle = 0;
    //                                OnHandleChange();
    //            */
    //        }
    //    }
    //    #endregion
    //}
}