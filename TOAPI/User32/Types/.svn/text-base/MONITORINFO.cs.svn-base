using System;
using System.Text;
using System.Runtime.InteropServices;

using TOAPI.Types;

namespace TOAPI.User32
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MONITORINFO
    {
        public int cbSize;
        public RECT rcMonitor;
        public RECT rcWork;
        public uint dwFlags;

        public void Init()
        {
            cbSize = Marshal.SizeOf(this);
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct MONITORINFOEX
    {
        public int cbSize;
        public RECT rcMonitor;
        public RECT rcWork;
        public uint dwFlags;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string szDevice;

        public void Init()
        {
            cbSize = Marshal.SizeOf(this);
            szDevice = new string(' ', 32);
        }
    }
}
