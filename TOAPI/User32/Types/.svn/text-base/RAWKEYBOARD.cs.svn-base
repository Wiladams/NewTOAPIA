using System;
using System.Runtime.InteropServices;

namespace TOAPI.User32
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RAWKEYBOARD
    {
        public ushort MakeCode;     // Manufacturer's scan code from keyboard activity.
        public ushort Flags;        // RI_KEY... flags for activity
        public ushort Reserved;     // Reserved, must be zero
        public ushort VKey;         // Microsoft Windows message compatible virtual-key code.   
        public uint Message;        // Corresponding window message, for example WM_KEYDOWN, WM_SYSKEYDOWN, and so forth. 
        public uint ExtraInformation;
    }
}