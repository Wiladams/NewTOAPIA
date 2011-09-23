using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace TOAPI.User32
{
    /// <summary>
    /// DISPLAY_DEVICE
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct DISPLAY_DEVICE
    {
        public int cb;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string DeviceName;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string DeviceString;

        public uint StateFlags;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string DeviceID;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string DeviceKey;

        public void Init()
        {
            cb = Marshal.SizeOf(this);
            DeviceName = new string(' ', 32);
            DeviceString = new string(' ', 128);
            DeviceID = new string(' ', 128);
            DeviceKey = new string(' ', 128);
        }
    }
}
