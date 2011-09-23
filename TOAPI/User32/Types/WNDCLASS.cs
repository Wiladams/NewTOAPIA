using System;
using System.Runtime.InteropServices;

using TOAPI.Types;

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
    //using LPDEVMODE = DeviceMode;

    using HRESULT = System.IntPtr;

    #endregion

    #region WNDCLASS
    /// <summary>
    /// An definition of the Basic Window Class structure that is
    /// used for CreateWindow
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct WNDCLASS
    {
        public uint style;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public WindowProc lpfnWndProc;
        public int cbClsExtra;
        public int cbWndExtra;
        public IntPtr hInstance;
        public IntPtr hIcon;
        public IntPtr hCursor;
        public IntPtr hbrBackground;
        public IntPtr lpszMenuName;
        public IntPtr lpszClassName;
    }
    #endregion

    #region WNDCLASSEX
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct WNDCLASSEX
    {
        public static uint SizeInBytes = (uint)Marshal.SizeOf(default(WNDCLASSEX));

        public int cbSize;
        public int style;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public WindowProc lpfnWndProc;
        public int cbClsExtra;
        public int cbWndExtra;
        public HINSTANCE hInstance;
        public HICON hIcon;
        public HCURSOR hCursor;
        public HBRUSH hbrBackground;
        public IntPtr lpszMenuName;
        public IntPtr lpszClassName;
        public HICON hIconSm;

        public void Init()
        {
            cbSize = (int)SizeInBytes;
        }
    }

    //[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    //public struct WNDCLASSEXA
    //{
    //    public uint cbSize;
    //    public uint style;
    //    public MessageProc lpfnWndProc;
    //    public int cbClsExtra;
    //    public int cbWndExtra;
    //    public IntPtr hInstance;
    //    public IntPtr hIcon;
    //    public IntPtr hCursor;
    //    public IntPtr hbrBackground;
    //    public string lpszMenuName;
    //    public string lpszClassName;
    //    public IntPtr hIconSm;

    //    public void Init()
    //    {
    //        cbSize = (uint)Marshal.SizeOf(this);
    //    }
    //}


    //[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    //public struct WNDCLASSEXW
    //{
    //    public uint cbSize;
    //    public uint style;
    //    public MessageProc lpfnWndProc;
    //    public int cbClsExtra;
    //    public int cbWndExtra;
    //    public System.IntPtr hInstance;
    //    public System.IntPtr hIcon;
    //    public System.IntPtr hCursor;
    //    public System.IntPtr hbrBackground;
    //    public string lpszMenuName;
    //    public string lpszClassName;
    //    public System.IntPtr hIconSm;

    //    public void Init()
    //    {
    //        cbSize = (uint)Marshal.SizeOf(this);
    //    }

    //}
    #endregion
}
