using System;
using System.Runtime.InteropServices;

namespace TOAPI.Types
{
    [StructLayout(LayoutKind.Sequential)]
    public struct DIBSECTION
    {
        public BITMAP dsBm;
        public BITMAPINFOHEADER dsBmih;
        /// DWORD[3]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.U4)]
        public uint[] dsBitfields;
        public IntPtr dshSection;
        public uint dsOffset;

        public void Init()
        {
            dsBmih.Init();
            dsBitfields = new uint[3];
        }
    }
}