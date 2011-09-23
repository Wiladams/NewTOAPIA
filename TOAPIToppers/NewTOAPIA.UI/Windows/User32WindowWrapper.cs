using System;
using System.Collections.Generic;
using System.Text;

using TOAPI.User32;
using TOAPI.Types;


namespace NewTOAPIA.UI
{
    using NewTOAPIA.Drawing;
    using NewTOAPIA.Drawing.GDI;
    using NewTOAPIA.Graphics;

    /// <summary>
    /// Class: User32WindowWrapper
    /// 
    /// This class is meant to wrap a User32.dll based window.  It primarily makes it easy to access
    /// various attributes of an existing window.  It does not provide access to creating the 
    /// window in the first place.
    /// </summary>
    public class User32WindowWrapper : IPlatformWindow, IMoveable
    {
        IntPtr fTimerId;
        IntPtr fWindowHandle;
        GDIContext clientAreaContext;
        GDIContext windowAreaContext;

        public User32WindowWrapper(IntPtr hWnd)
        {
            fWindowHandle = hWnd;
        }

        public IntPtr Handle
        {
            get { return fWindowHandle; }
        }

        public RectangleI Frame
        {
            get
            {
                RECT aFrame = new RECT();
                User32.GetWindowRect(Handle, out aFrame);

                RectangleI frame = new RectangleI(new Point2I(aFrame.X, aFrame.Y), new Size2I(aFrame.Width, aFrame.Height));
                return frame;
            }
        }

        public RectangleI ClientRectangle
        {
            get
            {
                RECT aFrame = new RECT();
                User32.GetClientRect(Handle, out aFrame);

                RectangleI frame = new RectangleI(new Point2I(aFrame.X, aFrame.Y), new Size2I(aFrame.Width, aFrame.Height));
                return frame;                
            }
        }

        /// <summary>
        /// The ClientAreaDeviceContext is the device context that only covers
        /// the client area of the window.  It does not include the area of the
        /// title bar, borders, and the like.
        /// </summary>
        public virtual GDIContext DeviceContextClientArea
        {
            get
            {
                if (null == clientAreaContext)
                    clientAreaContext = GDIContext.CreateForWindowClientArea(Handle);

                return clientAreaContext;
            }
        }

        /// <summary>
        /// The WindowDeviceContext is the device context for the entire
        /// window.  Its origin is at the upper left corner of the window
        /// and includes title bar, border, and the like.
        /// </summary>
        public GDIContext DeviceContextWholeWindow
        {
            get
            {
                if (null == windowAreaContext)
                    windowAreaContext = GDIContext.CreateForWholeWindow(Handle);

                return windowAreaContext;
            }
        }

        public void SetTitle(string title)
        {
            User32.SetWindowTextW(Handle, title);
        }

        #region IPlatformWindowBase
        /// <summary>
        /// Invalidate the entire client area.
        /// </summary>
        public virtual void Invalidate()
        {
            RECT rect = new RECT(0, 0, 0, 0);

            User32.GetClientRect(Handle, out rect);
            User32.InvalidateRect(Handle, ref rect, true);
        }

        public virtual void Invalidate(RectangleI rect)
        {
            RECT r = new RECT(rect.Left, rect.Top, rect.Width, rect.Height);
            User32.InvalidateRect(Handle, ref r, true);
        }

        public virtual void Validate()
        {
            RECT rect = new RECT(0, 0, 0, 0);
            User32.GetClientRect(Handle, out rect);
            User32.ValidateRect(Handle, ref rect);
        }

        public virtual void SetWindowRegion(GDIRegion aRegion)
        {
            RegionCombineType retValue = (RegionCombineType)User32.SetWindowRgn(Handle, aRegion.DangerousGetHandle(), true);
        }

        // Frame resizing command
        public virtual void ResizeBy(int dw, int dh)
        {
            ResizeTo(Frame.Width + dw, Frame.Height + dh);
        }

        public virtual void ResizeTo(int width, int height)
        {
                RECT wFrame = new RECT();
                User32.GetWindowRect(Handle, out wFrame);

            RECT cRect = new RECT();
            User32.GetClientRect(Handle, out cRect);
            cRect.Width = width;
            cRect.Height = height;
            User32.AdjustWindowRectEx(ref cRect, User32.WS_OVERLAPPEDWINDOW, false, 0);
            User32.MoveWindow(Handle, wFrame.Left, wFrame.Top, cRect.Width, cRect.Height, true);
        }

        public virtual void CaptureMouse()
        {

        }

        public virtual void ReleaseMouse()
        {

        }

        public void Destroy()
        {
            bool result = User32.DestroyWindow(Handle);
        }

        public void Show()
        {
            bool result;
            result = User32.ShowWindow(Handle, (int)ShowHide.ShowNormal);
        }

        public void Hide()
        {
            bool result;
            result = User32.ShowWindow(Handle, (int)ShowHide.Hide);
        }

        public void StartTimer(uint millis)
        {
            fTimerId = User32.SetTimer(Handle, IntPtr.Zero, millis, null);
        }

        public void StopTimer()
        {
            User32.KillTimer(Handle, fTimerId);
        }

        #endregion

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

            // If we are here, then opacity is less than 255 (opaque), so
            // we need to first ensure the window is a layered window.
            User32.SetWindowLong(Handle, User32.GWL_EXSTYLE, res1 | User32.WS_EX_LAYERED);

            // Next we setup the alpha value.
            bool res2 = User32.SetLayeredWindowAttributes(Handle, 0, alpha, User32.LWA_ALPHA);

            return true;
        }

        #region IMoveable
        /// <summary>
        /// Move the window by a specied amount.
        /// </summary>
        /// <param name="dx">The distance to move in the x-axis.</param>
        /// <param name="dy">The number of pixels to move in the y-axis.</param>
        /// <returns>Always returns true.</returns>
        public virtual void MoveBy(int dx, int dy)
        {
            RectangleI frame = Frame;

            MoveTo(frame.Left + dx, frame.Top + dy);
        }

        /// <summary>
        /// Move the window to a specific location.
        /// </summary>
        /// <param name="x">The X coordinate of the window frame's upper left corner.</param>
        /// <param name="y">The Y coordinate of the window frame's upper left corner.</param>
        /// <returns>Always returns 'true'.</returns>
        public virtual void MoveTo(int x, int y)
        {
            RectangleI frame = Frame;
            bool retValue = User32.MoveWindow(Handle, x, y, frame.Width, frame.Height, true);
        }
        #endregion

        #region Static Helper Methods
        public static User32WindowWrapper GetTopLevelWindow(string className, string windowName)
        {
            IntPtr hWnd = User32.FindWindow(className, windowName);
            User32WindowWrapper aWindow = new User32WindowWrapper(hWnd);

            return aWindow;
        }
        #endregion

        /// <summary>
        /// </summary>
        // The message processing chain is like this:
        // First, in Start(), we're pulling messages off of the 
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

            int retValue;
            while ((retValue = User32.GetMessage(out msg, IntPtr.Zero, 0, 0)) != 0)
            {
                // If we get a return value == -1, then there was some type of error
                // So, we just throw an exception
                if (retValue == -1)
                    throw new Exception("GetMessage returned -1");

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
                /// that this code is dealing with.  
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

            }
        }
    }
}
