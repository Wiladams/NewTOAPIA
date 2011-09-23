using System;
using System.Runtime.InteropServices;

namespace TOAPI.Types
{

	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
	public class TEXTMETRIC
	{
		public int tmHeight;
		public int tmAscent;
		public int tmDescent;
		public int tmInternalLeading;
		public int tmExternalLeading;
		public int tmAveCharWidth;
		public int tmMaxCharWidth;
		public int tmWeight;
		public int tmOverhang;
		public int tmDigitizedAspectX;
		public int tmDigitizedAspectY;
		public char tmFirstChar;
		public char tmLastChar;
		public char tmDefaultChar;
		public char tmBreakChar;
		public byte tmItalic;
		public byte tmUnderlined;
		public byte tmStruckOut;
		public byte tmPitchAndFamily;
		public byte tmCharSet;
	}

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	public class TEXTMETRICA
	{
		public int tmHeight;
		public int tmAscent;
		public int tmDescent;
		public int tmInternalLeading;
		public int tmExternalLeading;
		public int tmAveCharWidth;
		public int tmMaxCharWidth;
		public int tmWeight;
		public int tmOverhang;
		public int tmDigitizedAspectX;
		public int tmDigitizedAspectY;
		public byte tmFirstChar;
		public byte tmLastChar;
		public byte tmDefaultChar;
		public byte tmBreakChar;
		public byte tmItalic;
		public byte tmUnderlined;
		public byte tmStruckOut;
		public byte tmPitchAndFamily;
		public byte tmCharSet;
	}

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet= CharSet.Unicode)]
    public struct TEXTMETRICW
    {
        public int tmHeight;
        public int tmAscent;
        public int tmDescent;
        public int tmInternalLeading;
        public int tmExternalLeading;
        public int tmAveCharWidth;
        public int tmMaxCharWidth;
        public int tmWeight;
        public int tmOverhang;
        public int tmDigitizedAspectX;
        public int tmDigitizedAspectY;
        public ushort tmFirstChar;      /// WCHAR->wchar_t->unsigned short
        public ushort tmLastChar;       /// WCHAR->wchar_t->unsigned short
        public ushort tmDefaultChar;    /// WCHAR->wchar_t->unsigned short
        public ushort tmBreakChar;      /// WCHAR->wchar_t->unsigned short
        public byte tmItalic;
        public byte tmUnderlined;
        public byte tmStruckOut;
        public byte tmPitchAndFamily;
        public byte tmCharSet;
    }

}