using System;
using System.Runtime.InteropServices;

namespace TOAPI.User32
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RAWINPUTDEVICELIST
    {
        public IntPtr hDevice;
        public uint dwType;
    }
}
