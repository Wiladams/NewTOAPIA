using System.Runtime.InteropServices;

namespace TOAPI.User32
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RID_DEVICE_INFO_MOUSE
    {
        public uint dwId;
        public uint dwNumberOfButtons;
        public uint dwSampleRate;
    }
}