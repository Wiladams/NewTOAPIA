using System;
using System.Runtime.InteropServices;

namespace TOAPI.Types
{
    [StructLayout(LayoutKind.Sequential)]
    public struct CURSORINFO
    {
        public int cbSize;
        public int flags;
        public IntPtr hCursor;
        public POINT ptScreenPos;

        public void Init()
        {
            cbSize = Marshal.SizeOf(typeof(CURSORINFO));
        }
    }
}
