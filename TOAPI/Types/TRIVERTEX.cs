using System;
using System.Runtime.InteropServices;

namespace TOAPI.Types
{
    [Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public struct TRIVERTEX
	{
		public int x;
		public int y;
		public ushort Red;
		public ushort Green;
		public ushort Blue;
		public ushort Alpha;

		public TRIVERTEX(int aX, int aY, ushort red, ushort green, ushort blue, ushort alpha)
		{
			x = aX;
			y = aY;
			Red = red;
			Green = green;
			Blue = blue;
			Alpha = alpha;
		}
	}
}