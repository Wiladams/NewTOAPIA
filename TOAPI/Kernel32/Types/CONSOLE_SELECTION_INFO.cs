using System;
using System.Runtime.InteropServices;

namespace TOAPI.Kernel32
{
    [StructLayout(LayoutKind.Sequential)]
    public struct CONSOLE_SELECTION_INFO
    {
        public uint dwFlags;
        public COORD dwSelectionAnchor;
        public SMALL_RECT srSelection;
    }
}
