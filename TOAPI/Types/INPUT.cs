using System;
using System.Runtime.InteropServices;

namespace TOAPI.Types
{
    [StructLayout(LayoutKind.Explicit)]
    public struct INPUT
    {
        [FieldOffsetAttribute(0)]
        public int type;

        [FieldOffsetAttribute(4)]
        public MOUSEINPUT mi;

        [FieldOffsetAttribute(4)]
        public KEYBDINPUT ki;
        
        [FieldOffsetAttribute(4)]
        public HARDWAREINPUT hi;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct INPUT64
    {
        [FieldOffset(0)]
        public int type;

        [FieldOffset(8)]
        public MOUSEINPUT mi;

        [FieldOffset(8)]
        public KEYBDINPUT ki;

        [FieldOffset(8)]
        public HARDWAREINPUT hi;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct MOUSEINPUT
    {
        public int dx;
        public int dy;
        public int mouseData;
        public int dwFlags;
        public int time;
        public int dwExtraInfo;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct KEYBDINPUT
    {
        public short wVk;
        public short wScan;
        public int dwFlags;
        public int time;
        public int dwExtraInfo;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct HARDWAREINPUT
    {
        public int uMsg;
        public short wParamL;
        public short wParamH;
    }
}
