using System.Runtime.InteropServices;
using System;

namespace TOAPI.Types
{
    [Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public struct BLENDFUNCTION
	{
        public byte BlendOp;               // Blend operation - AC_SRC_OVER      
		public byte BlendFlags;            // Must be Zero 
		public byte SourceConstantAlpha;   // The alpha value to use if not pixel based alpha
		public byte AlphaFormat;           // Format of source alpha - AC_SRC_ALPHA

		public BLENDFUNCTION(byte op, byte flags, byte alpha, byte format)
		{
			BlendOp = op;
			BlendFlags = flags;
			SourceConstantAlpha = alpha;
			AlphaFormat = format;
		}
	}
}