using System;
using System.Runtime.InteropServices;
using System.Text;

namespace TOAPI.User32
{

    [StructLayout(LayoutKind.Sequential)]
    public class RID_DEVICE_INFO
    {
        public int cbSize;
        public uint dwType;

        public Anonymous_7e5cad77_be70_4bb7_aab7_d011f0dd77b9 Union1;

        public void Init()
        {
            cbSize = Marshal.SizeOf(this);
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct Anonymous_7e5cad77_be70_4bb7_aab7_d011f0dd77b9
    {
        [FieldOffset(0)]
        public RID_DEVICE_INFO_MOUSE mouse;

        [FieldOffset(0)]
        public RID_DEVICE_INFO_KEYBOARD keyboard;

        [FieldOffset(0)]
        public RID_DEVICE_INFO_HID hid;
    }
}
