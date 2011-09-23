namespace TOAPI.Types
{
    using System;
    using System.Runtime.InteropServices;

    using UINT = System.UInt32;
    using BYTE = System.Byte;
    using PSTR = System.Int32;

    [StructLayout(LayoutKind.Sequential, Pack=4, CharSet = CharSet.Auto)]
    public struct OUTLINETEXTMETRIC
    {
        public UINT otmSize;
        public TEXTMETRIC otmTextMetrics;
        public BYTE otmFiller;
        public PANOSE otmPanoseNumber;
        public UINT otmfsSelection;
        public UINT otmfsType;
        public int otmsCharSlopeRise;
        public int otmsCharSlopeRun;
        public int otmItalicAngle;
        public UINT otmEMSquare;
        public int otmAscent;
        public int otmDescent;
        public UINT otmLineGap;
        public UINT otmsCapEmHeight;
        public UINT otmsXHeight;
        public RECT otmrcFontBox;
        public int otmMacAscent;
        public int otmMacDescent;
        public UINT otmMacLineGap;
        public UINT otmusMinimumPPEM;
        public POINT otmptSubscriptSize;
        public POINT otmptSubscriptOffset;
        public POINT otmptSuperscriptSize;
        public POINT otmptSuperscriptOffset;
        public UINT otmsStrikeoutSize;
        public int otmsStrikeoutPosition;
        public int otmsUnderscoreSize;
        public int otmsUnderscorePosition;
        public PSTR otmpFamilyName;
        public PSTR otmpFaceName;
        public PSTR otmpStyleName;
        public PSTR otmpFullName; 
    }
}
