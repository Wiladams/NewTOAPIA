using System;
using System.Runtime.InteropServices;

namespace Papyrus.Types
{
	[StructLayout(LayoutKind.Sequential)]
	public struct LOGBRUSH
	{
		public int lbStyle;
		public int lbColor;
		public IntPtr lbHatch;
	}
}