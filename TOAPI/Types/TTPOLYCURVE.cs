using System;
using System.Runtime.InteropServices;

namespace TOAPI.Types
{
    [StructLayout(LayoutKind.Sequential)]
    public struct TTPOLYCURVE
    {
        public short wType;
        public ushort cpfx;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1, ArraySubType = UnmanagedType.Struct)]
        public POINTFX[] apfx;  /// POINTFX[1]
    }
}
