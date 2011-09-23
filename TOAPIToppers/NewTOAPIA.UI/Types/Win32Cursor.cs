using System;

namespace NewTOAPIA.UI
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.InteropServices;

    using TOAPI.Types;
    using TOAPI.User32;

    public class Win32Cursor : ICursor, IHandle
    {
        int fIdentifier;
        IntPtr fHandle;
        int fShowCount;
        Point fLocation;
        IPlatformWindowBase fWindow;

        // Default system cursors
        public static Win32Cursor Arrow = new Win32Cursor(User32.IDC_ARROW);
        public static Win32Cursor IBeam = new Win32Cursor(User32.IDC_IBEAM);
        public static Win32Cursor Wait = new Win32Cursor(User32.IDC_WAIT);
        public static Win32Cursor Cross = new Win32Cursor(User32.IDC_CROSS);
        public static Win32Cursor UpArrow = new Win32Cursor(User32.IDC_UPARROW);
        public static Win32Cursor Size = new Win32Cursor(User32.IDC_SIZE);
        public static Win32Cursor Icon = new Win32Cursor(User32.IDC_ICON);
        public static Win32Cursor SizeNWSE = new Win32Cursor(User32.IDC_SIZENWSE);
        public static Win32Cursor SizeNESW = new Win32Cursor(User32.IDC_SIZENESW);
        public static Win32Cursor SizeWE = new Win32Cursor(User32.IDC_SIZEWE);
        public static Win32Cursor SizeNS = new Win32Cursor(User32.IDC_SIZENS);
        public static Win32Cursor SizeAll = new Win32Cursor(User32.IDC_SIZEALL);
        public static Win32Cursor None = new Win32Cursor(User32.IDC_NO);
        public static Win32Cursor AppStarting = new Win32Cursor(User32.IDC_APPSTARTING);
        public static Win32Cursor Help = new Win32Cursor(User32.IDC_HELP);


        #region Static Helpers
        public static IPlatformWindowBase SetFocusWindow(IPlatformWindowBase aWindow)
        {
            Win32Cursor currentCursor = GetCurrentCursor();
            IPlatformWindowBase oldWindow = null;

            if (currentCursor != null)
            {
                oldWindow = currentCursor.FocusWindow;
                currentCursor.FocusWindow = aWindow;
            }

            return oldWindow;
        }

        public static Win32Cursor GetCurrentCursor()
        {
            // Call GetCursorInfo, to see which cursor
            // is currently the active cursor
            CURSORINFO curseInfo = new CURSORINFO();
            bool success = User32.GetCursorInfo(ref curseInfo) != 0;

            if (!success)
                return null;

            // hand out a cursor object based on the handle retrieved
            // from the current global cursor.
            return new Win32Cursor(curseInfo.hCursor, curseInfo.ptScreenPos.X, curseInfo.ptScreenPos.Y);
        }

        public static void SetCurrentCursor(Win32Cursor newCursor)
        {
            //Win32Cursor.gCurrentCursor = value;
            User32.SetCursor(newCursor.Handle);
        }
        #endregion

        #region Constructors
        public Win32Cursor(int ident)
        {
            fIdentifier = ident;
            fHandle = User32.LoadCursor(IntPtr.Zero, ident);
            fShowCount = 0;
            fWindow = null;
        }

        public Win32Cursor(IntPtr handle)
            : this(handle, 0, 0)
        {
        }

        public Win32Cursor(IntPtr handle, int x, int y)
        {
            fHandle = handle;
            fLocation = new Point(x, y);
            fWindow = null;
        }

        #endregion

        public IntPtr Handle
        {
            get { return fHandle; }
        }

        /// <summary>
        /// FocusWindow
        /// 
        /// This method is called from places like a Win32Window's Message Dispatch
        /// loop.  When the window gains focus, it sets itself as the FocusWindow.  
        /// When it loses focus, it sets null as the FocusWindow.
        /// All we do here is create the cursor and destroy it as appropriate.  This
        /// does not in and of itself make the caret show itself.  A control that is
        /// doing input would deal with that.
        /// </summary>
        private IPlatformWindowBase FocusWindow
        {
            get { return fWindow; }
            set
            {
                fWindow = value;
            }
        }


        public virtual Point Location
        {
            get
            {
                POINT aPoint;
                User32.GetCursorPos(out aPoint);
                
                return new Point(aPoint.X, aPoint.Y);
            }
        }

        /// <summary>
        /// MoveTo
        /// 
        /// Moves the Cursor to the specified location, in the current focuswindow's
        /// client coordinates.  If the cursor is hidden, it will still move, but
        /// it won't get automatically displayed.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>Returns true if it successfully moved the cursor.</returns>
        public virtual void MoveTo(int x, int y)
        {
            bool result = User32.SetCursorPos(x, y) != 0;
        }

        public virtual void OnMovedTo(int x, int y)
        {
        }

        public virtual void OnMoving(int x, int y)
        {
        }

        public virtual void MoveBy(int dx, int dy)
        {
            POINT aPoint;

            User32.GetCursorPos(out aPoint);
            aPoint.X += dx;
            aPoint.Y += dy;

            MoveTo(aPoint.X, aPoint.Y);
        }

        public virtual void OnMovedBy(int dx, int dy)
        {
        }

        //
        // MakeVisible will call the Show() method until the Cursor 
        // is actually visible.  You would use this method if you are
        // not keeping track of how many times Hide() gets called, but
        // you just want the Cursor to be visible.
        public virtual void MakeVisible()
        {
            while ((fWindow != null) && (fShowCount < 1))
            {
                Show();
            }
        }

        public virtual void Show()
        {
            if (fWindow == null)
                return;

            fShowCount = User32.ShowCursor(true);
        }

        public virtual void Hide()
        {
            if (fWindow == null)
                return;

            fShowCount = User32.ShowCursor(false);
        }
    }
}