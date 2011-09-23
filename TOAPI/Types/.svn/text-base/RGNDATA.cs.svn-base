using System;
using System.Runtime.InteropServices;

namespace TOAPI.Types
{
    /// <summary>
    /// RGNDATAHEADER
    /// 
    /// This is the managed code implementation of the RGNDATAHEADER structure.
    /// This structure is referred to by RGNDATA, which is part of the GDI
    /// region drawing facility.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RGNDATAHEADER
    {
        public int dwSize;
        public uint iType;
        public uint nCount;
        public uint nRgnSize;
        public RECT rcBound;

        public void Init()
        {
            dwSize = Marshal.SizeOf(this);
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	public struct RGNDATA 
	{ 
		public RGNDATAHEADER    rdh;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public byte[] Buffer; 
	}  

}