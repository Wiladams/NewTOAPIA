using System;

namespace TOAPI.User32
{
    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct RAWINPUTHEADER
    {
        public uint dwType;     // RIM_TYPE(KEYBOARD,MOUSE,HID)
        public uint dwSize;     // Size, in bytes, of the entire input packet of data.
        public IntPtr hDevice;  // Handle to the device generating the raw input data.
        public uint wParam;     // Value passed in the wParam parameter of the WM_INPUT message.
    }
}
