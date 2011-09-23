using System;
using System.Runtime.InteropServices;

namespace TOAPI.Types
{
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct TRACKMOUSEEVENT
    {
        public uint cbSize;         /// DWORD->unsigned int
        public uint dwFlags;        /// DWORD->unsigned int
        public IntPtr hwndTrack;    /// HWND->HWND__*
        public uint dwHoverTime;    /// DWORD->unsigned int
    }
}
