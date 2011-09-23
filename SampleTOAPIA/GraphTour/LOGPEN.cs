using System;
using System.Runtime.InteropServices;

namespace Papyrus.Types
{
	[StructLayout(LayoutKind.Sequential)]
	public struct LOGPEN
	{
		public uint lopnStyle;
        public PointG lopnWidth;
		public uint lopnColor;
	}
}