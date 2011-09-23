using System.Runtime.InteropServices;

namespace TOAPI.User32
{
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct RAWHID
    {
        public uint dwSizeHid;  // Size, in bytes, of each HID input in bRawData.
        public uint dwCount;    // Number of HID inputs in bRawData.

        /// BYTE[1]
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 1, ArraySubType = UnmanagedType.I1)]
        public byte[] bRawData; // Raw input data as an array of bytes.
    }
}
