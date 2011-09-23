using System;
//using System.Runtime.InteropServices;
using NewTOAPIA;

using TOAPI.Types;
    
    /// <summary>
    /// This class represents a Region Handle in GDI
    /// </summary>
    public class Region : IHandle
    {
        IntPtr fRegionHandle;


        public enum CombineRgnType : int
        {
            NULLREGION,
            SIMPLEREGION,
            COMPLEXREGION,
            ERROR
        }

        public Region()
        {
            fRegionHandle = IntPtr.Zero;
        }

        public IntPtr Handle
        {
            get { return fRegionHandle; }
            set { }
        }
        
        // GDI Interop methods for regions
		// Only the calls related to the creation of, and
		// manipulation of regions are in here.
		// The calls related to drawing regions are in the GDI32Device class.
		// Lots of different ways to create regions
        // [DllImport("gdi32.dll")]
        // static extern IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect,
        //  int nBottomRect);
        // [DllImport("gdi32.dll")]
        // static extern IntPtr CreateRectRgnIndirect([In] ref Rectangle lprc);
        // [DllImport("gdi32.dll")]
        // static extern IntPtr CreateRoundRectRgn(int x1, int y1, int x2, int y2,
        //  int cx, int cy);
        // [DllImport("gdi32.dll")]
        // static extern IntPtr CreateEllipticRgn(int nLeftRect, int nTopRect,
        //  int nRightRect, int nBottomRect);
        // [DllImport("gdi32.dll")]
        // static extern IntPtr CreateEllipticRgnIndirect([In] ref Rectangle lprc);
        // [DllImport("gdi32.dll")]
        // static extern IntPtr CreatePolygonRgn(Point[] lppt, int cPoints,
        //  int fnPolyFillMode);
        // [DllImport("gdi32.dll")]
        // static extern IntPtr CreatePolyPolygonRgn(Point[] lppt, int[] lpPolyCounts,
        //  int nCount, int fnPolyFillMode);
        // [DllImport("gdi32.dll")]
        // static extern IntPtr PathToRegion(IntPtr hdc);
        // [DllImport("gdi32.dll")]
        // static extern IntPtr ExtCreateRegion(IntPtr lpXform, uint nCount,
        //  [In] ref RGNDATA lpRgnData);

        //// Manipulating regions
        // [DllImport("gdi32.dll")]
        // static extern int CombineRgn(IntPtr hrgnDest, IntPtr hrgnSrc1,
        //  IntPtr hrgnSrc2, int fnCombineMode);
        // [DllImport("gdi32.dll")]
        // static extern int OffsetRgn(IntPtr hrgn, int nXOffset, int nYOffset);


        //// Hit testing a region
        // [DllImport("gdi32.dll")]
        // static extern bool EqualRgn(IntPtr hSrcRgn1, IntPtr hSrcRgn2);
        // [DllImport("gdi32.dll")]
        // static extern int GetRgnBox(IntPtr hrgn, out Rectangle lprc);
        // [DllImport("gdi32.dll")]
        // static extern int GetRandomRgn(IntPtr hdc, IntPtr hrgn, int iNum);
        // [DllImport("gdi32.dll")]
        // static extern uint GetRegionData(IntPtr hRgn, uint dwCount, out RGNDATA lpRgnData);
        // [DllImport("gdi32.dll")]
        // static extern bool PtInRegion(IntPtr hrgn, int X, int Y);
        // [DllImport("gdi32.dll")]
        // static extern bool RectInRegion(IntPtr hrgn, [In] ref Rectangle lprc);
    }
