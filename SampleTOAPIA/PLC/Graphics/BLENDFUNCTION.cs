using System;
using System.Runtime.InteropServices;

namespace Papyri
{
	[StructLayout(LayoutKind.Sequential)]
	public struct BLENDFUNCTION
	{
		byte BlendOp;
		byte BlendFlags;
		byte SourceConstantAlpha;
		byte AlphaFormat;

		public BLENDFUNCTION(byte op, byte flags, byte alpha, byte format)
		{
			BlendOp = op;
			BlendFlags = flags;
			SourceConstantAlpha = alpha;
			AlphaFormat = format;
		}
	}
}