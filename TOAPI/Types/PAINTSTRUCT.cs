using System;
using System.Runtime.InteropServices;

namespace TOAPI.Types
{
	[StructLayout(LayoutKind.Sequential)]
	public struct PAINTSTRUCT
	{
		public SafeHandle hdc;

        public int fErase;
		public RECT rcPaint;
        public int fRestore;        
        public int fIncUpdate;
        
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
		public byte[] rgbReserved;

        public void Init()
        {
            rgbReserved = new byte[32];
        }
	}
}