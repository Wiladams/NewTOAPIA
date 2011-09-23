using System;
using System.Runtime.InteropServices;
using System.Text;

namespace TOAPI.Kernel32
{
    [StructLayout(LayoutKind.Sequential)]
    public struct INPUT_RECORD
    {
        public EVENT_TYPE EventType;
        public Anonymous_2ae3b958_7751_4e29_8f2c_6b953571cfc8 Event;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct Anonymous_2ae3b958_7751_4e29_8f2c_6b953571cfc8
    {
        [FieldOffset(0)]
        public KEY_EVENT_RECORD KeyEvent;

        [FieldOffset(0)]
        public MOUSE_EVENT_RECORD MouseEvent;

        [FieldOffset(0)]
        public WINDOW_BUFFER_SIZE_RECORD WindowBufferSizeEvent;

        [FieldOffset(0)]
        public MENU_EVENT_RECORD MenuEvent;

        [FieldOffset(0)]
        public FOCUS_EVENT_RECORD FocusEvent;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct KEY_EVENT_RECORD
    {
        [MarshalAs(UnmanagedType.Bool)]
        public bool bKeyDown;
        public ushort wRepeatCount;
        public ushort wVirtualKeyCode;
        public ushort wVirtualScanCode;
        public UniChar uChar;
        public uint dwControlKeyState;
    }


    [StructLayout(LayoutKind.Sequential)]
    public struct MOUSE_EVENT_RECORD
    {
        public COORD dwMousePosition;
        public uint dwButtonState;
        public uint dwControlKeyState;
        public uint dwEventFlags;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct WINDOW_BUFFER_SIZE_RECORD
    {
        public COORD dwSize;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct FOCUS_EVENT_RECORD
    {
        [MarshalAs(UnmanagedType.Bool)]
        public bool bSetFocus;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MENU_EVENT_RECORD
    {
        public uint dwCommandId;
    }

}
