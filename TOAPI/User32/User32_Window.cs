using System;
using System.Text;  // For StringBuilder
using System.Runtime.InteropServices;

using TOAPI.Types;
using TOAPI.Kernel32;

namespace TOAPI.User32
{
    #region Type aliases

    using HWND = System.IntPtr;
    using HINSTANCE = System.IntPtr;
    using HMENU = System.IntPtr;
    using HICON = System.IntPtr;
    using HBRUSH = System.IntPtr;
    using HCURSOR = System.IntPtr;

    using LRESULT = System.IntPtr;
    using LPVOID = System.IntPtr;
    using LPCTSTR = System.String;

    using WPARAM = System.IntPtr;
    using LPARAM = System.IntPtr;
    using HANDLE = System.IntPtr;
    using HRAWINPUT = System.IntPtr;

    using BYTE = System.Byte;
    using SHORT = System.Int16;
    using USHORT = System.UInt16;
    using LONG = System.Int32;
    using ULONG = System.UInt32;
    using WORD = System.Int16;
    using DWORD = System.Int32;
    using BOOL = System.Boolean;
    using INT = System.Int32;
    using UINT = System.UInt32;
    using LONG_PTR = System.IntPtr;
    using ATOM = System.Int32;

    using COLORREF = System.Int32;
    using WNDPROC = System.IntPtr;

    using HRESULT = System.IntPtr;

    #endregion

    /// <summary>
    /// This is the delegate for a windows procedure
    /// </summary>
    /// 
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate IntPtr WindowProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

    partial class User32
    {

        // Dealing with transparent windows
        [DllImport("user32.dll", EntryPoint = "SetLayeredWindowAttributes")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

        [DllImport("user32.dll", SetLastError = true, EntryPoint = "UpdateLayeredWindow")]
        public static extern int UpdateLayeredWindow(IntPtr hWnd, 
            IntPtr hdcDst,
            ref POINT pptDst, 
            ref SIZE psize,
            IntPtr hdcSrc, 
            ref POINT pptSrc, 
            uint crKey, 
            ref BLENDFUNCTION pblend, 
            uint dwFlags);


        // Error handling
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetClientRect(IntPtr hWnd, out RECT rect);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowRect(IntPtr hWnd, out RECT rect);

        
        
        [DllImport("user32.dll", EntryPoint = "ClientToScreen")]
        public static extern int ClientToScreen(IntPtr hWnd, ref POINT lpPoint);

        [DllImport("user32.dll", EntryPoint = "ScreenToClient")]
        public static extern int ScreenToClient(IntPtr hWnd, ref POINT lpPoint);


        // Window properties and longs
        [DllImport("user32.dll")]
        public static extern int RemoveProp(IntPtr hWnd, int atom);

        [DllImport("user32.dll")]
        public static extern int SetProp(IntPtr hWnd, int atom, int data);

        [DllImport("user32.dll")]
        public static extern int GetProp(IntPtr hWnd, int atom);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetProp(IntPtr hWnd, string name);

        // SetWindowLong
        // SetWindowLongPtr does not exist on x86 platforms (it's a macro that resolves to SetWindowLong).
        // We need to detect if we are on x86 or x64 at runtime and call the correct function
        // (SetWindowLongPtr on x64 or SetWindowLong on x86). Fun!
        public static IntPtr SetWindowLong(IntPtr handle, int item, IntPtr newValue)
        {
            // SetWindowPos defines its error condition as an IntPtr.Zero retval and a non-0 GetLastError.
            // We need to SetLastError(0) to ensure we are not detecting on older error condition (from another function).

            IntPtr retval = IntPtr.Zero;
            TOAPI.Kernel32.Kernel32.SetLastError(0);

            if (IntPtr.Size == 4)
                retval = new IntPtr(SetWindowLong(handle, item, newValue.ToInt32()));
            else
                retval = SetWindowLongPtr(handle, item, newValue);

            if (retval == IntPtr.Zero)
            {
                int error = Marshal.GetLastWin32Error();
                if (error != 0)
                    throw new Exception(String.Format("Failed to modify window border. Error: {0}", error));
            }

            return retval;
        }

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        public static IntPtr SetWindowProc(IntPtr hWnd, WindowProc aProc)
        {
            IntPtr oldProc = SetWindowLongPtr(hWnd, User32.GWL_WNDPROC, Marshal.GetFunctionPointerForDelegate(aProc));
            return oldProc;
        }

        // GetWindowLong
        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);

        // GetWindowTextLength
        [DllImport("user32.dll")]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        // GetWindowText
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
        
        [DllImport("user32.dll", EntryPoint = "SetWindowTextW")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowTextW([In] IntPtr hWnd, [In] [MarshalAs(UnmanagedType.LPWStr)] string lpString);

        // IsChild
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public extern static bool IsChild(int parent, int child);

        // IsWindowEnabled
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindowEnabled(IntPtr hWnd);

        // IsWindowUnicode
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindowUnicode(IntPtr hWnd);

        // IsWindowVisible
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindowVisible(IntPtr hWnd);



        // AdjustWindowRectEx
        [DllImport("user32.dll")]
        public static extern int AdjustWindowRectEx(ref RECT lpRect, int dwStyle, bool bMenu, int dwExStyle);

        // MoveWindow
        [DllImport("user32.dll", ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool MoveWindow(IntPtr hWnd,
            int X, int Y,
            int nWidth, int nHeight,
            [MarshalAs(UnmanagedType.Bool)]bool bRepaint);

        // SetWindowPos
        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);


        // EnableWindow
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnableWindow(IntPtr hWnd, [MarshalAs(UnmanagedType.Bool)] bool bEnable);

        // FindWindow
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string className, string windowName);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindow(IntPtr hWnd, int param);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        // SetActiveWindow
        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern IntPtr SetActiveWindow(IntPtr hWnd);

        // SetForegroundWindow
        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        // ShowWindow
        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        // ShowWindowAsync
        [DllImport("user32.dll", ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        // ScrollWindow
        [DllImport("user32.dll")]
        public static extern int ScrollWindow(IntPtr hWnd, int nXAmount, int nYAmount, ref RECT rectScrollRegion, ref RECT rectClip);

        // ScrollWindowEx
        [DllImport("user32.dll")]
        public static extern int ScrollWindowEx(IntPtr hWnd, int nXAmount, int nYAmount, RECT rectScrollRegion, ref RECT rectClip, int hrgnUpdate, ref RECT prcUpdate, int flags);


        // Creating and getting a hold of window handles
        // Class registration
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short UnregisterClass([MarshalAs(UnmanagedType.LPTStr)] LPCTSTR className, IntPtr instance);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern short UnregisterClass(IntPtr className, IntPtr instance);


        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern ushort RegisterClass(ref WNDCLASS wc);

        // RegisterClassEx
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern ushort RegisterClassEx(ref WNDCLASSEX wc);
        
        //[DllImport("user32.dll", CharSet=CharSet.Ansi, EntryPoint = "RegisterClassExA")]
        //public static extern ushort RegisterClassExA(ref WNDCLASSEXA wc);

        //[DllImport("user32.dll", CharSet=CharSet.Unicode, EntryPoint = "RegisterClassExW")]
        //public static extern ushort RegisterClassExW(ref WNDCLASSEXW wc);

        // GetClassInfo
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassInfo(IntPtr hInst, [In] string lpszClass, [Out] out WNDCLASS wc);



        // CreateWindowEx
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CreateWindowEx(
            int dwExStyle,
            IntPtr ClassName,
            IntPtr WindowName,
            int style,
            int x, int y,
            int width, int height,
            IntPtr hWndParent,
            IntPtr hMenu,
            IntPtr hInst,
            [MarshalAs(UnmanagedType.AsAny)] object pvParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CreateWindowEx(
            int dwExStyle,
            [MarshalAs(UnmanagedType.LPTStr)] string ClassName,
            [MarshalAs(UnmanagedType.LPTStr)] string WindowName,
            int style,
            int x, int y, 
            int width, int height,
            IntPtr hWndParent,
            IntPtr hMenu,
            IntPtr hInst,
            [MarshalAs(UnmanagedType.AsAny)] object pvParam);

        //[DllImport("user32.dll", CharSet=CharSet.Ansi, EntryPoint = "CreateWindowExA")]
        //public static extern IntPtr CreateWindowExA(uint dwExStyle, 
        //    [MarshalAs(UnmanagedType.LPStr)] string lpClassName, 
        //    [MarshalAs(UnmanagedType.LPStr)] string lpWindowName, 
        //    uint dwStyle, 
        //    int X, int Y, int nWidth, int nHeight, 
        //    System.IntPtr hWndParent, 
        //    System.IntPtr hMenu, 
        //    System.IntPtr hInstance, 
        //    System.IntPtr lpParam);

        //[DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "CreateWindowExW")]
        //public static extern IntPtr CreateWindowExW(uint dwExStyle, 
        //    [MarshalAs(UnmanagedType.LPWStr)] string lpClassName, 
        //    [MarshalAs(UnmanagedType.LPWStr)] string lpWindowName, 
        //    uint dwStyle, 
        //    int X, int Y, int nWidth, int nHeight, 
        //    IntPtr hWndParent, 
        //    IntPtr hMenu, 
        //    IntPtr hInstance, 
        //    IntPtr lpParam);


        // GetDesktopWindow
        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();

        // WindowFromPoint
        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(int x, int y);

        // DestroyWindow
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DestroyWindow(IntPtr hWnd);


        // UpdateWindow
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UpdateWindow(IntPtr hWnd);

        // DrawMenuBar
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DrawMenuBar(IntPtr hWnd);

        // LockWindowUpdate
        [DllImport("user32.dll", EntryPoint = "LockWindowUpdate")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool LockWindowUpdate(IntPtr hWndLock);

        // RedrawWindow
        [DllImport("user32.dll", EntryPoint = "RedrawWindow")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool RedrawWindow(IntPtr hWnd, [In] ref RECT updateRect, [In] IntPtr hrgnUpdate, uint flags);

        // SetWindowRgn
        [DllImport("user32.dll", EntryPoint = "SetWindowRgn")]
        public static extern int SetWindowRgn(IntPtr hWnd, [In] IntPtr hRgn, [MarshalAs(UnmanagedType.Bool)] bool bRedraw);

        // WindowFromDC
        [DllImport("user32.dll", EntryPoint = "WindowFromDC")]
        public static extern IntPtr WindowFromDC(IntPtr hDC);

        // GetWindowRgn
        [DllImport("user32.dll", EntryPoint = "GetWindowRgn")]
        public static extern int GetWindowRgn(IntPtr hWnd, [In] IntPtr hRgn);

        // GetWindowRgnBox
        [DllImport("user32.dll", EntryPoint = "GetWindowRgnBox")]
        public static extern int GetWindowRgnBox(IntPtr hWnd, [Out] out RECT lprc);


    }
}
