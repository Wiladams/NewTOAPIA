using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

using TOAPI.Types;

// GdiGetBatchLimit - gdi32
// GdiSetBatchLimit - gdi32

namespace TOAPI.GDI32
{
	public partial class GDI32
	{
        // Use the GM_ constants
        [DllImport("gdi32.dll")]
        public static extern int GetGraphicsMode([In] SafeHandle hdc);
		
        // SetGraphicsMode
        // Use the GM_ constants
        [DllImport("gdi32.dll")]
        public static extern int SetGraphicsMode([In] SafeHandle hdc, int iMode);


		// Brush management
		[DllImport("gdi32.dll")]
		public static extern IntPtr CreateSolidBrush(int color);


       


        // Dealing with pens
        //         74   49 00010911 CreatePen
        [DllImport("gdi32.dll", EntryPoint = "CreatePen")]
        public static extern IntPtr CreatePen(int style, int width, uint color);
        
        // CreatePenIndirect
        [DllImport("gdi32.dll", EntryPoint = "CreatePenIndirect")]
        public static extern IntPtr CreatePenIndirect([In] ref LOGPEN plpen);

        //        286  11D 00012374 ExtCreatePen
        [DllImport("gdi32.dll", EntryPoint = "ExtCreatePen")]
        public static extern IntPtr ExtCreatePen(uint iPenStyle, uint cWidth, [In] ref LOGBRUSH plbrush, uint cStyle, [In] IntPtr pstyle);



        // Region Management
        [DllImport("gdi32.dll", EntryPoint = "CreateRectRgn")]
        public static extern IntPtr CreateRectRgn(int x1, int y1, int x2, int y2);

        [DllImport("gdi32.dll", EntryPoint = "CreateRectRgnIndirect")]
        public static extern IntPtr CreateRectRgnIndirect([In] ref RECT lprect);

        [DllImport("gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        public static extern IntPtr CreateRoundRectRgn(int x1, int y1, int x2, int y2, int w, int h);

        [DllImport("gdi32.dll", EntryPoint = "CreateEllipticRgn")]
        public static extern IntPtr CreateEllipticRgn(int x1, int y1, int x2, int y2);

        [DllImport("gdi32.dll", EntryPoint = "CreateEllipticRgnIndirect")]
        public static extern IntPtr CreateEllipticRgnIndirect([In] ref RECT lprect);

        [DllImport("gdi32.dll", EntryPoint = "CreatePolygonRgn")]
        public static extern IntPtr CreatePolygonRgn([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Struct, SizeParamIndex = 1)] POINT[] pptl, int cPoint, int iMode);

        [DllImport("gdi32.dll", EntryPoint = "CreatePolyPolygonRgn")]
        public static extern IntPtr CreatePolyPolygonRgn([In] ref POINT pptl, [In] ref int pc, int cPoly, int iMode);

        [DllImport("gdi32.dll", EntryPoint = "CombineRgn")]
        public static extern int CombineRgn([In] IntPtr hrgnDst, [In] IntPtr hrgnSrc1, [In] IntPtr hrgnSrc2, int iMode);

        [DllImport("gdi32.dll", EntryPoint = "ExtCreateRegion")]
        public static extern IntPtr ExtCreateRegion([In] IntPtr lpx, uint nCount, [In] IntPtr lpData);

        [DllImport("gdi32.dll", EntryPoint = "GetRgnBox")]
        public static extern int GetRgnBox([In] IntPtr hrgn, [Out] out RECT lprc);

        [DllImport("gdi32.dll", EntryPoint = "GetRegionData")]
        public static extern uint GetRegionData([In] IntPtr hrgn, uint nCount, IntPtr lpRgnData);

        [DllImport("gdi32.dll", EntryPoint = "OffsetRgn")]
        public static extern int OffsetRgn([In] IntPtr hrgn, int x, int y);

        [DllImport("gdi32.dll", EntryPoint = "EqualRgn")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EqualRgn([In] IntPtr hrgn1, [In] IntPtr hrgn2);

        [DllImport("gdi32.dll", EntryPoint = "PtInRegion")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PtInRegion([In] IntPtr hrgn, int x, int y);

        [DllImport("gdi32.dll", EntryPoint = "RectInRegion")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool RectInRegion([In] IntPtr hrgn, [In] ref RECT lprect);

        [DllImport("gdi32.dll", EntryPoint = "SetRectRgn")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetRectRgn([In] IntPtr hrgn, int left, int top, int right, int bottom);



    }
}

