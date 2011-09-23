using System;
using System.Runtime.InteropServices;

namespace Papyrus.Types
{

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	public class LOGFONT
	{
        public const int LogFontCharSetOffset = 23;
        public const int LogFontNameOffset = 28;
        
        public int lfHeight;
		public int lfWidth;
		public int lfEscapement;
		public int lfOrientation;
		public int lfWeight;
		public byte lfItalic;
		public byte lfUnderline;
		public byte lfStrikeOut;
		public byte lfCharSet;
		public byte lfOutPrecision;
		public byte lfClipPrecision;
		public byte lfQuality;
		public byte lfPitchAndFamily;
        /// CHAR[32]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		public string lfFaceName;
    
    }


}

