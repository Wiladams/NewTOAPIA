using System;
using System.Runtime.InteropServices;

using TOAPI.Types;

namespace TOAPI.GDI32
{
    public partial class GDI32
    {
        //        458  1C9 0002B4BF GetGlyphOutline
        //        459  1CA 0002B4BF GetGlyphOutlineA
        //        461  1CC 0002B4E9 GetGlyphOutlineWow
        [DllImport("gdi32.dll", CharSet=CharSet.Auto)]
        public static extern int GetGlyphOutline([In] SafeHandle hDC,		// handle to DC
            uint uChar,		// character to query
            uint uFormat,    // data format
            IntPtr lpgm,		// glyph metrics
            int cbBuffer,	// size of data buffer
            IntPtr lpvBuffer,	// data buffer
            IntPtr lpmat2		// transformation matrix
            );

        //        460  1CB 0001FFFF GetGlyphOutlineW
        [DllImport("gdi32.dll", EntryPoint = "GetGlyphOutlineW")]
        public static extern uint GetGlyphOutlineW(
            [In] SafeHandle hDC, 
            uint uChar, 
            uint fuFormat, 
            [Out] out GLYPHMETRICS lpgm, 
            uint cjBuffer, 
            IntPtr pvBuffer, 
            [In] ref MAT2 lpmat2);

        //486  1E5 0000D743 GetOutlineTextMetricsA
        //487  1E6 0000C7F9 GetOutlineTextMetricsW
        [DllImport("gdi32.dll", CharSet=CharSet.Auto)]
        public static extern uint GetOutlineTextMetrics([In] SafeHandle hDC, uint cbDtat, IntPtr lpOTM);

        [DllImport("gdi32.dll")]
        public static extern uint GetTextMetrics([In] SafeHandle hDC, IntPtr lptm);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetTextMetrics([In] SafeHandle hDC, [Out] out TEXTMETRIC lptm);

        [DllImport("gdi32.dll", EntryPoint = "GetTextMetricsW")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetTextMetricsW([In] SafeHandle hDC, [Out] out TEXTMETRICW lptm);

        [DllImport("gdi32.dll", EntryPoint = "CreateFontA")]
        public static extern IntPtr CreateFontA(
            int cHeight, 
            int cWidth, 
            int cEscapement, 
            int cOrientation, 
            int cWeight, 
            uint bItalic, 
            uint bUnderline, 
            uint bStrikeOut, 
            uint iCharSet, 
            uint iOutPrecision, 
            uint iClipPrecision, 
            uint iQuality, 
            uint iPitchAndFamily, 
            [In] [MarshalAs(UnmanagedType.LPStr)] string pszFaceName);
        
        [DllImport("gdi32.dll", EntryPoint = "CreateFontW")]
        public static extern IntPtr CreateFontW(
            int cHeight, 
            int cWidth, 
            int cEscapement, 
            int cOrientation, 
            int cWeight, 
            uint bItalic, 
            uint bUnderline, 
            uint bStrikeOut, 
            uint iCharSet, 
            uint iOutPrecision, 
            uint iClipPrecision, 
            uint iQuality, 
            uint iPitchAndFamily, 
            [In] [MarshalAs(UnmanagedType.LPWStr)] string pszFaceName);
        
        [DllImport("gdi32.dll", CharSet=CharSet.Auto)]
        public static extern IntPtr CreateFont(
            short nHeight,               // height of font
            short nWidth,                // average character width
            short nEscapement,           // angle of escapement
            short nOrientation,          // base-line orientation angle
            short fnWeight,              // font weight
            int fdwItalic,           // italic attribute option
            int fdwUnderline,        // underline attribute option
            int fdwStrikeOut,        // strikeout attribute option
            int fdwCharSet,          // character set identifier
            int fdwOutputPrecision,  // output precision
            int fdwClipPrecision,    // clipping precision
            int fdwQuality,          // output quality
            int fdwPitchAndFamily,   // pitch and family
            string lpszFace           // typeface name
            );

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateFontIndirect(ref LOGFONT lplf);

        // Text Drawing
        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool TextOut([In] SafeHandle hDC, 
            int nXStart, int nYStart,
           string lpString, int cbString);

        // ExtTextOut
        [DllImport("gdi32.dll", CharSet=CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ExtTextOut([In] SafeHandle hDC, 
            int x, int y, 
            int nOptions, ref RECT lpRect, 
            string s, int strLength, 
            int[] lpDx);

        // Measuring text
        // GetCharWidth32
        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCharWidth32([In] SafeHandle hDC, 
            uint iFirstChar, uint iLastChar, out int[] lpBuffer);

        // GetTextExtentPoint32
        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetTextExtentPoint32([In] SafeHandle hDC, 
            [In] string lpString,
            int cbString, out SIZE lpSize);


        // Text Attributes
        //        652  28B 00007E09 SetTextAlign
        [DllImport("gdi32.dll")]
        public static extern uint SetTextAlign([In] SafeHandle hDC, uint fMode);

        //        654  28D 000064BF SetTextColor
        [DllImport("gdi32.dll")]
        public static extern int SetTextColor([In] SafeHandle hDC, uint textColor);

    }
}
