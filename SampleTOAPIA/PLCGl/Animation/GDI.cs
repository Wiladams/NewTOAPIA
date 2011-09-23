using System;
using System.Runtime.InteropServices;

/// <summary>
/// Summary description for Class1
/// </summary>
public class GDI
{
	[StructLayout(LayoutKind.Sequential)]
	public struct BLENDFUNCTION
	{
		internal byte BlendOp;
		internal byte BlendFlags;
		internal byte SourceConstantAlpha;
		internal byte AlphaFormat;

		public BLENDFUNCTION(byte op, byte flags, byte alpha, byte format)
		{
			BlendOp = op;
			BlendFlags = flags;
			SourceConstantAlpha = alpha;
			AlphaFormat = format;
		}
	}

	public const int
	AC_SRC_OVER = 0x00,
	AC_SRC_ALPHA = 0x01;

	public const uint
		SRCCOPY = 0x00CC0020,		/* dest = source                   */
		SRCPAINT = 0x00EE0086,		/* dest = source OR dest           */
		SRCAND = 0x008800C6,		/* dest = source AND dest          */
		SRCINVERT = 0x00660046,		/* dest = source XOR dest          */
		SRCERASE = 0x00440328,		/* dest = source AND (NOT dest )   */
		DSTINVERT = 0x00550009,		/* dest = (NOT dest)               */
		BLACKNESS = 0x00000042,		/* dest = BLACK                    */
		WHITENESS = 0x00FF0062,		/* dest = WHITE                    */
		MERGECOPY = 0x00C000CA,		/* dest = (source AND pattern)     */
		MERGEPAINT = 0x00BB0226,	/* dest = (NOT source) OR dest     */
		NOTSRCCOPY = 0x00330008,	/* dest = (NOT source)             */
		NOTSRCERASE = 0x001100A6,	/* dest = (NOT src) AND (NOT dest) */
		PATCOPY = 0x00F00021,		/* dest = pattern                  */
		PATPAINT = 0x00FB0A09,		/* dest = DPSnoo                   */
		PATINVERT = 0x005A0049,		/* dest = pattern XOR dest         */
		NOMIRRORBITMAP = 0x80000000, /* Do not Mirror the bitmap in this call */
		CAPTUREBLT = 0x40000000;	/* Include layered windows */

	[DllImport("gdi32.dll", EntryPoint = "GdiAlphaBlend")]
	public static extern bool AlphaBlend(IntPtr hdcDest, int nXOriginDest, int nYOriginDest,
		int nWidthDest, int nHeightDest,
		IntPtr hdcSrc, int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc,
		BLENDFUNCTION blendFunction);
	[DllImport("gdi32.dll")]
	public static extern bool StretchBlt(IntPtr hdcDest, int nXOriginDest, int nYOriginDest,
		int nWidthDest, int nHeightDest,
		IntPtr hdcSrc, int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc,
		uint dwRop);
}
