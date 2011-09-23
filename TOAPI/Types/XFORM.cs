using System;
using System.Runtime.InteropServices;

namespace TOAPI.Types
{
    [Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public struct XFORM
	{
		public float eM11;
		public float eM12;
		public float eM21;
		public float eM22;
		public float eDx;
		public float eDy;

		public XFORM(XFORM a)
		{
			eM11 = a.eM11;
			eM12 = a.eM12;
			eM21 = a.eM21;
			eM22 = a.eM22;
			eDx = a.eDx;
			eDy = a.eDy;
		}
	};
}