using System;

namespace NewTOAPIA.Graphics
{
    /// <summary>
    /// Enum to determine how pixels are combined with the background for 
    /// some drawing calls.  This is used for text drawing, as well as
    /// some of the bitmap drawing functions.
    /// </summary>
    public enum BackgroundMixMode : int
    {
        Opaque = 2,         // GDI32.OPAQUE,
        Transparent = 1,    // GDI32.TRANSPARENT
    };

    public enum EDirection
    {
        ClockWise,
        CounterClockWise,
    }

    public enum FloodFillType
    {
        Border = 0,     // GDI32.FLOODFILLBORDER,
        Surface = 1,    // GDI32.FLOODFILLSURFACE,
    }

    // Constants for the symbolic names of well known raster operations
    // These are used with the SetROP2 call to change the drawing mode.
    /* Binary raster operations */
    public enum BinaryRasterOps : int
    {
        Black           = 1,   /*  0       */
        NOTMergePen     = 2,   /* DPon     */
        MaskNOTPen      = 3,   /* DPna     */
        NOTCppyPen      = 4,   /* PN       */
        MaskPenNOT      = 5,   /* PDna     */
        NOT             = 6,   /* Dn       */
        XORPen          = 7,   /* DPx      */
        NOTMaskPen      = 8,   /* DPan     */
        MASKPEN         = 9,   /* DPa      */
        NOTXORPEN       = 10,  /* DPxn     */
        NOP             = 11,  /* D        */
        MERGENOTPEN     = 12,  /* DPno     */
        COPYPEN         = 13,  /* P        */
        MERGEPENNOT     = 14,  /* PDno     */
        MERGEPEN        = 15,  /* DPo      */
        WHITE           = 16   /*  1       */
    }

    // Ternary raster operations
    public enum TernaryRasterOps : uint
    {
        BLACKNESS = 0x00000042,		// dest = BLACK
        SRCCOPY = 0x00CC0020,		// dest = source
        SRCPAINT = 0x00EE0086,		// dest = source OR dest
        SRCAND = 0x008800C6,		// dest = source AND dest
        SRCINVERT = 0x00660046,		// dest = source XOR dest
        SRCERASE = 0x00440328,		// dest = source AND (NOT dest )
        DSTINVERT = 0x00550009,		// dest = (NOT dest)
        MERGECOPY = 0x00C000CA,		// dest = (source AND pattern)
        MERGEPAINT = 0x00BB0226,       // dest = (NOT source) OR dest
        NOTSRCCOPY = 0x00330008,       // dest = (NOT source)
        NOTSRCERASE = 0x001100A6,       // dest = (NOT src) AND (NOT dest)
        PATCOPY = 0x00F00021,		// dest = pattern
        PATPAINT = 0x00FB0A09,		// dest = DPSnoo
        PATINVERT = 0x005A0049,		// dest = pattern XOR dest
        NOMIRRORBITMAP = 0x80000000,    // Do not Mirror the bitmap in this call
        CAPTUREBLT = 0x40000000,       // Include layered windows
        WHITENESS = 0x00FF0062,       // dest = WHITE
    }

    // For PatBlt
    public enum PatternBlitOps : uint
    {
        PATCOPY = 0x00F00021,		// dest = pattern
        PATINVERT = 0x005A0049,		// dest = pattern XOR dest
        DSTINVERT = 0x00550009,		// dest = (NOT dest)
        BLACKNESS = 0x00000042,		// dest = BLACK
        WHITENESS = 0x00FF0062,       // dest = WHITE
    }

    public enum PolygonFillMode : int
    {
        Alternate = 1,  // GDI32.ALTERNATE,
        Winding = 2,    // GDI32.WINDING,
    }

    // Used for the CombineRgn call
    public enum RegionCombineStyles : int
    {
        AND = 1,    // GDI32.RGN_AND,
        OR = 2,     // GDI32.RGN_OR,
        XOR = 3,    // GDI32.RGN_XOR,
        Diff = 4,   // GDI32.RGN_DIFF,
        Copy = 5,   // GDI32.RGN_COPY,
    }

    public enum RegionCombineType : int
    {
        Error = 0,      // GDI32.RGN_ERROR,
        Null = 1,       // GDI32.NULLREGION,
        Simple = 2,     // GDI32.SIMPLEREGION,
        Complex = 3,    // GDI32.COMPLEXREGION,
    }

    // Mapping Modes - Used with SetMapMode
   public enum MappingModes
    {
        Text = 1,           // GDI32.MM_TEXT,
        LoMetric = 2,       // GDI32.MM_LOMETRIC,
        HiMetric = 3,       // GDI32.MM_HIMETRIC,
        LoEnglish = 4,      // GDI32.MM_LOENGLISH,
        HiEnglish = 5,      // GDI32.MM_HIENGLISH,
        Twips = 6,          // GDI32.MM_TWIPS,
        Isotropic = 7,      // GDI32.MM_ISOTROPIC,
        Anisotropic = 8,    // GDI32.MM_ANISOTROPIC,
        MaxFixedScale = Twips
    }

    [Flags]
    public enum GDIPathCommand
    {
        CloseFigure = 0x01,     // GDI32.PT_CLOSEFIGURE,
        LineTo = 0x02,          // GDI32.PT_LINETO,
        BezierTo = 0x04,        // GDI32.PT_BEZIERTO,
        MoveTo = 0x06,          // GDI32.PT_MOVETO,
    }
}
