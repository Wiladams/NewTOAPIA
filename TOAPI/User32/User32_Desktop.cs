using System;
using System.Text;  // For StringBuilder
using System.Runtime.InteropServices;

using TOAPI.Types;


namespace TOAPI.User32
{
    /// <summary>
    /// For User32.EnumDesktops call
    /// </summary>
    /// <param name="desktop"></param>
    /// <param name="lParam"></param>
    /// <returns></returns>
    public delegate int EnumDesktopsDelegate(string desktop, IntPtr lParam);

    partial class User32
    {
        // Desktop Routines
        // CloseDesktop
        [DllImport("user32.dll", EntryPoint = "CloseDesktop")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseDesktop(IntPtr hDesktop);

        // CreateDesktop
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CreateDesktop([In] string lpszDesktop, IntPtr lpszDevice, IntPtr pDevmode, uint dwFlags, int dwDesiredAccess, ref SECURITY_ATTRIBUTES lpsa);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CreateDesktop([In] string lpszDesktop, IntPtr lpszDevice, IntPtr pDevmode, uint dwFlags, int dwDesiredAccess, IntPtr lpsa);

        public static IntPtr CreateDesktop([In] string desktopName, uint dwFlags, int dwAccess, IntPtr lpsa)
        {
            return CreateDesktop(desktopName, IntPtr.Zero, IntPtr.Zero, dwFlags, dwAccess, lpsa);
        }

        // EnumDesktops
        [DllImport("user32.dll")]
        public static extern int EnumDesktops(IntPtr hwinsta, EnumDesktopsDelegate lpEnumFunc, IntPtr lParam);

        // EnumDesktopWindows
        [DllImport("user32.dll", EntryPoint = "EnumDesktopWindows")]
        public static extern int EnumDesktopWindows(IntPtr hDesktop, EnumWindowsProc lpfn, IntPtr lParam);

        // EnumWindows
        [DllImport("user32.dll", EntryPoint = "EnumWindows")]
        public static extern int EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        // GetThreadDesktop
        [DllImport("user32.dll")]
        public static extern IntPtr GetThreadDesktop(uint dwThreadId);

        // OpenDesktop
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr OpenDesktop([In] string lpszDesktop, uint dwFlags,
            [MarshalAs(UnmanagedType.Bool)] bool fInherit,
            uint dwDesiredAccess);

        // OpenInputDesktop
        [DllImport("user32.dll", EntryPoint = "OpenInputDesktop", SetLastError = true)]
        public static extern IntPtr OpenInputDesktop(uint dwFlags, [MarshalAs(UnmanagedType.Bool)] bool fInherit, uint dwDesiredAccess);

        // SetThreadDesktop
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetThreadDesktop(IntPtr hDesktop);

        // SwitchDesktop
        [DllImport("user32.dll", EntryPoint = "SwitchDesktop")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SwitchDesktop(IntPtr hDesktop);


        // PaintDesktop
        [DllImport("user32.dll", EntryPoint = "PaintDesktop")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PaintDesktop([In] IntPtr hdc);
    }
}
