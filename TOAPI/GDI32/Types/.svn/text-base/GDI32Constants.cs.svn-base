using System;
using System.Collections.Generic;
using System.Text;

namespace TOAPI.GDI32
{
    public partial class GDI32
    {
        public const int
            AC_SRC_OVER = 0x00,
            AC_SRC_ALPHA = 0x01;

        public const int
            BI_RGB = 0,
            BI_RLE8 = 1,
            BI_RLE4 = 2,
            BI_BITFIELDS = 3,
            BI_JPEG = 4,
            BI_PNG = 5;

        // Brush Styles
        public const int
            BS_SOLID           = 0,
            BS_NULL            = 1,
            BS_HOLLOW          = BS_NULL,
            BS_HATCHED         = 2,
            BS_PATTERN         = 3,
            BS_INDEXED         = 4,
            BS_DIBPATTERN      = 5,
            BS_DIBPATTERNPT    = 6,
            BS_PATTERN8X8      = 7,
            BS_DIBPATTERN8X8   = 8,
            BS_MONOPATTERN     = 9;

        // ExtFloodFill style flags
        public const int
            FLOODFILLBORDER   = 0,
            FLOODFILLSURFACE  = 1;

                // Doing Gradient Fills
        public const int
            GRADIENT_FILL_RECT_H = 0x00000000,
            GRADIENT_FILL_RECT_V = 0x00000001,
            GRADIENT_FILL_TRIANGLE = 0x00000002,
            GRADIENT_FILL_OP_FLAG = 0x000000ff;

        // Hatch Styles
        public const int
            HS_HORIZONTAL      = 0,       /* ----- */
            HS_VERTICAL        = 1,       /* ||||| */
            HS_FDIAGONAL       = 2,       /* \\\\\ */
            HS_BDIAGONAL       = 3,       /* ///// */
            HS_CROSS           = 4,       /* +++++ */
            HS_DIAGCROSS       = 5;       /* xxxxx */

        // flags used with CreateDC()
        public const int
            DCB_RESET = 0x0001,
            DCB_ACCUMULATE = 0x0002,
            DCB_DIRTY = DCB_ACCUMULATE,
            DCB_SET = (DCB_RESET | DCB_ACCUMULATE),
            DCB_ENABLE = 0x0004,
            DCB_DISABLE = 0x0008;

        public const int
            DIB_RGB_COLORS = 0,
            DIB_PAL_COLORS = 1;


        /* Device Parameters for GetDeviceCaps() */
        public const int 
            DRIVERVERSION = 0,     /* Device driver version                    */
            TECHNOLOGY    = 2,     /* Device classification                    */
            HORZSIZE      = 4,     /* Horizontal size in millimeters           */
            VERTSIZE      = 6,     /* Vertical size in millimeters             */
            HORZRES       = 8,     /* Horizontal width in pixels               */
            VERTRES       = 10,    /* Vertical height in pixels                */
            BITSPIXEL     = 12,    /* Number of bits per pixel                 */
            PLANES        = 14,    /* Number of planes                         */
            NUMBRUSHES    = 16,    /* Number of brushes the device has         */
            NUMPENS       = 18,    /* Number of pens the device has            */
            NUMMARKERS    = 20,    /* Number of markers the device has         */
            NUMFONTS      = 22,    /* Number of fonts the device has           */
            NUMCOLORS     = 24,    /* Number of colors the device supports     */
            PDEVICESIZE   = 26,    /* Size required for device descriptor      */
            CURVECAPS     = 28,    /* Curve capabilities                       */
            LINECAPS      = 30,    /* Line capabilities                        */
            POLYGONALCAPS = 32,    /* Polygonal capabilities                   */
            TEXTCAPS      = 34,    /* Text capabilities                        */
            CLIPCAPS      = 36,    /* Clipping capabilities                    */
            RASTERCAPS    = 38,    /* Bitblt capabilities                      */
            ASPECTX       = 40,    /* Length of the X leg                      */
            ASPECTY       = 42,    /* Length of the Y leg                      */
            ASPECTXY      = 44,    /* Length of the hypotenuse                 */

            LOGPIXELSX    = 88,    /* Logical pixels/inch in X                 */
            LOGPIXELSY    = 90,    /* Logical pixels/inch in Y                 */

            SIZEPALETTE  = 104,    /* Number of entries in physical palette    */
            NUMRESERVED  = 106,    /* Number of reserved entries in palette    */
            COLORRES     = 108,    /* Actual color resolution                  */

            // Printing related DeviceCaps. These replace the appropriate Escapes

            PHYSICALWIDTH   = 110, /* Physical Width in device units           */
            PHYSICALHEIGHT  = 111, /* Physical Height in device units          */
            PHYSICALOFFSETX = 112, /* Physical Printable Area x margin         */
            PHYSICALOFFSETY = 113, /* Physical Printable Area y margin         */
            SCALINGFACTORX  = 114, /* Scaling factor x                         */
            SCALINGFACTORY  = 115, /* Scaling factor y                         */

            // Display driver specific

            VREFRESH        = 116,  /* Current vertical refresh rate of the    */
                                    /* display device (for displays only) in Hz*/
            DESKTOPVERTRES = 117,   /* Vertical height of entire desktop in pixels   */
            DESKTOPHORZRES = 118,   /* Horizontal width of entire desktop in  pixels  */
            BLTALIGNMENT    = 119,  /* Preferred blt alignment                 */

            SHADEBLENDCAPS  = 120,  /* Shading and blending caps               */
            COLORMGMTCAPS   = 121;  /* Color Management caps                   */



        // Changing Graphics mode
        public const int
            GM_COMPATIBLE   = 1,
            GM_ADVANCED     = 2;

        public const int 
            LAYOUT_RTL                          = 0x00000001, // Right to left
            LAYOUT_BTT                          = 0x00000002, // Bottom to top
            LAYOUT_VBH                          = 0x00000004, // Vertical before horizontal
            LAYOUT_ORIENTATIONMASK              = (LAYOUT_RTL | LAYOUT_BTT | LAYOUT_VBH),
            LAYOUT_BITMAPORIENTATIONPRESERVED   = 0x00000008;   // Disables any reflection during BitBlt and StretchBlt operations. 


        // Mapping Modes - Used with SetMapMode()
        public const int
            MM_TEXT = 1,
            MM_LOMETRIC = 2,
            MM_HIMETRIC = 3,
            MM_LOENGLISH = 4,
            MM_HIENGLISH = 5,
            MM_TWIPS = 6,
            MM_ISOTROPIC = 7,
            MM_ANISOTROPIC = 8;

        // Object Definitions for EnumObjects() and GetCurrentObject
        public const int
            OBJ_PEN         = 1,
            OBJ_BRUSH       = 2,
            OBJ_DC          = 3,
            OBJ_METADC      = 4,
            OBJ_PAL         = 5,
            OBJ_FONT        = 6,
            OBJ_BITMAP      = 7,
            OBJ_REGION      = 8,
            OBJ_METAFILE    = 9,
            OBJ_MEMDC       = 10,
            OBJ_EXTPEN      = 11,
            OBJ_ENHMETADC   = 12,
            OBJ_ENHMETAFILE = 13,
            OBJ_COLORSPACE  = 14;


        /* Ternary raster operations - Used in BITBLT, PatBlt, etc*/
        public const int
        SRCCOPY        =     0x00CC0020, /* dest = source                   */
        SRCPAINT       =     0x00EE0086, /* dest = source OR dest           */
        SRCAND         =     0x008800C6, /* dest = source AND dest          */
        SRCINVERT      =     0x00660046, /* dest = source XOR dest          */
        SRCERASE       =     0x00440328, /* dest = source AND (NOT dest )   */
        NOTSRCCOPY     =     0x00330008, /* dest = (NOT source)             */
        NOTSRCERASE    =     0x001100A6, /* dest = (NOT src) AND (NOT dest) */
        MERGECOPY      =     0x00C000CA, /* dest = (source AND pattern)     */
        MERGEPAINT     =     0x00BB0226, /* dest = (NOT source) OR dest     */
        PATCOPY        =     0x00F00021, /* dest = pattern                  */
        PATPAINT       =     0x00FB0A09, /* dest = DPSnoo                   */
        PATINVERT      =     0x005A0049, /* dest = pattern XOR dest         */
        DSTINVERT      =     0x00550009, /* dest = (NOT dest)               */
        BLACKNESS      =     0x00000042, /* dest = BLACK                    */
        WHITENESS      =     0x00FF0062; /* dest = WHITE                    */

        public const int
            CAPTUREBLT = 0x40000000; /* Include layered windows */

        public const uint
            NOMIRRORBITMAP =     0x80000000; /* Do not Mirror the bitmap in this call */


        // For SetPolyFillMode()
        public const int
            ALTERNATE   = 1,
            WINDING     = 2;

        // Various constants for fonts
        public static int
            FF_DONTCARE     = (0 << 4),
            FF_ROMAN        = (1 << 4),
            FF_SWISS        = (2 << 4),
            FF_MODERN       = (3 << 4),
            FF_SCRIPT       = (4 << 4),
            FF_DECORATIVE   = (5 << 4);

        /* Font Weights */
        public const int
            FW_DONTCARE     =    0,
            FW_THIN         =    100,
            FW_EXTRALIGHT   =    200,
            FW_LIGHT        =    300,
            FW_NORMAL       =    400,
            FW_MEDIUM       =    500,
            FW_SEMIBOLD     =    600,
            FW_BOLD         =    700,
            FW_EXTRABOLD    =    800,
            FW_HEAVY        =    900;

        public const int
            FW_ULTRALIGHT   =    FW_EXTRALIGHT,
            FW_REGULAR      =    FW_NORMAL,
            FW_DEMIBOLD     =    FW_SEMIBOLD,
            FW_ULTRABOLD    =    FW_EXTRABOLD,
            FW_BLACK        =    FW_HEAVY;

        // Font Quality
        public const int
            DEFAULT_QUALITY         =0,
            DRAFT_QUALITY           =1,
            PROOF_QUALITY           =2,
            NONANTIALIASED_QUALITY  =3,
            ANTIALIASED_QUALITY     =4;

        // Windows Vista and higher
        public const int
            CLEARTYPE_QUALITY       = 5,
            CLEARTYPE_NATURAL_QUALITY =      6;

        public const int
            DEFAULT_PITCH          = 0,
            FIXED_PITCH            = 1,
            VARIABLE_PITCH         = 2,
            MONO_FONT              = 8;

        public static int
            AnsiFixedFont       = 11,
            AnsiVarFont         = 12,
            DeviceDefaultFont   = 14,
            OEMFixedFont        = 10,
            SystemFont          = 13,
            SystemFixedFont     = 16;

        public const int
            OPAQUE      = 2,
            TRANSPARENT = 1;

        // PRECIS constants used with LOGFONT
        public const int
            OUT_DEFAULT_PRECIS      = 0,
            OUT_STRING_PRECIS       = 1,
            OUT_CHARACTER_PRECIS    = 2,
            OUT_STROKE_PRECIS       = 3,
            OUT_TT_PRECIS           = 4,
            OUT_DEVICE_PRECIS       = 5,
            OUT_RASTER_PRECIS       = 6,
            OUT_TT_ONLY_PRECIS      = 7,
            OUT_OUTLINE_PRECIS      = 8,
            OUT_SCREEN_OUTLINE_PRECIS = 9,
            OUT_PS_ONLY_PRECIS      = 10;

        // Pen Styles
        public const int
            PS_SOLID            = 0,
            PS_DASH             = 1,       /* -------  */
            PS_DOT              = 2,       /* .......  */
            PS_DASHDOT          = 3,       /* _._._._  */
            PS_DASHDOTDOT       = 4,       /* _.._.._  */
            PS_NULL             = 5,
            PS_INSIDEFRAME      = 6,
            PS_USERSTYLE        = 7,
            PS_ALTERNATE        = 8,
            PS_STYLE_MASK       = 0x0000000F;

        public const int
            PS_ENDCAP_ROUND     = 0x00000000,
            PS_ENDCAP_SQUARE    = 0x00000100,
            PS_ENDCAP_FLAT      = 0x00000200,
            PS_ENDCAP_MASK      = 0x00000F00;

        public const int
            PS_JOIN_ROUND       = 0x00000000,
            PS_JOIN_BEVEL       = 0x00001000,
            PS_JOIN_MITER       = 0x00002000,
            PS_JOIN_MASK        = 0x0000F000;

        public const int
            PS_COSMETIC         = 0x00000000,
            PS_GEOMETRIC        = 0x00010000,
            PS_TYPE_MASK        = 0x000F0000;

        // PolyDraw and GetPath point types
        public const int
            PT_CLOSEFIGURE = 0x01,
            PT_LINETO = 0x02,
            PT_BEZIERTO = 0x04,
            PT_MOVETO = 0x06;

        public const int
            AD_COUNTERCLOCKWISE = 1,
            AD_CLOCKWISE = 2;

        public const int
            CLIP_DEFAULT_PRECIS = 0,
            CLIP_CHARACTER_PRECIS = 1,
            CLIP_STROKE_PRECIS = 2,
            CLIP_MASK = 0xf,
            CLIP_LH_ANGLES = (1 << 4),
            CLIP_TT_ALWAYS = (2 << 4),
            CLIP_DFA_DISABLE = (4 << 4),
            CLIP_EMBEDDED = (8 << 4);

        // Charset constants for LOGFONT
        public const int
            ANSI_CHARSET = 0,
            DEFAULT_CHARSET = 1,
            SYMBOL_CHARSET = 2,
            SHIFTJIS_CHARSET = 128,
            HANGEUL_CHARSET = 129,
            HANGUL_CHARSET = 129,
            GB2312_CHARSET = 134,
            CHINESEBIG5_CHARSET = 136,
            OEM_CHARSET = 255,
            JOHAB_CHARSET = 130,
            HEBREW_CHARSET = 177,
            ARABIC_CHARSET = 178,
            GREEK_CHARSET = 161,
            TURKISH_CHARSET = 162,
            VIETNAMESE_CHARSET = 163,
            THAI_CHARSET = 222,
            EASTEUROPE_CHARSET = 238,
            RUSSIAN_CHARSET = 204,
            MAC_CHARSET = 77,
            BALTIC_CHARSET = 186;
        /*
        FS_LATIN1               0x00000001L
        FS_LATIN2               0x00000002L
        FS_CYRILLIC             0x00000004L
        FS_GREEK                0x00000008L
        FS_TURKISH              0x00000010L
        FS_HEBREW               0x00000020L
        FS_ARABIC               0x00000040L
        FS_BALTIC               0x00000080L
        FS_VIETNAMESE           0x00000100L
        FS_THAI                 0x00010000L
        FS_JISJAPAN             0x00020000L
        FS_CHINESESIMP          0x00040000L
        FS_WANSUNG              0x00080000L
        FS_CHINESETRAD          0x00100000L
        FS_JOHAB                0x00200000L
        FS_SYMBOL               0x80000000L
        */


        /* Stock Logical Objects */
        // These get used with calls to GetStockObject
        public const int
            WHITE_BRUSH        = 0,
            LTGRAY_BRUSH        = 1,
            GRAY_BRUSH          = 2,
            DKGRAY_BRUSH        = 3,
            BLACK_BRUSH         = 4,
            NULL_BRUSH          = 5,
            HOLLOW_BRUSH        = NULL_BRUSH,
            WHITE_PEN           = 6,
            BLACK_PEN           = 7,
            NULL_PEN            = 8,
            OEM_FIXED_FONT      = 10,
            ANSI_FIXED_FONT     = 11,
            ANSI_VAR_FONT       = 12,
            SYSTEM_FONT         = 13,
            DEVICE_DEFAULT_FONT = 14,
            DEFAULT_PALETTE     = 15,
            SYSTEM_FIXED_FONT   = 16,
            DEFAULT_GUI_FONT    = 17,
            DC_BRUSH            = 18,
            DC_PEN              = 19;

        // Device Technologies
        // Used with GetDeviceCaps(Handle, TECHNOLOGY)
        public const int
            DT_PLOTTER         = 0,   /* Vector plotter                   */
            DT_RASDISPLAY      = 1,   /* Raster display                   */
            DT_RASPRINTER      = 2,   /* Raster printer                   */
            DT_RASCAMERA       = 3,   /* Raster camera                    */
            DT_CHARSTREAM      = 4,   /* Character-stream, PLP            */
            DT_METAFILE        = 5,   /* Metafile, VDM                    */
            DT_DISPFILE        = 6;   /* Display-file                     */

        /* Curve Capabilities */
        public const int
            CC_NONE            = 0,   /* Curves not supported             */
            CC_CIRCLES         = 1,   /* Can do circles                   */
            CC_PIE             = 2,   /* Can do pie wedges                */
            CC_CHORD           = 4,   /* Can do chord arcs                */
            CC_ELLIPSES        = 8,   /* Can do ellipese                  */
            CC_WIDE            = 16,  /* Can do wide lines                */
            CC_STYLED          = 32,  /* Can do styled lines              */
            CC_WIDESTYLED      = 64,  /* Can do wide styled lines         */
            CC_INTERIORS       = 128, /* Can do interiors                 */
            CC_ROUNDRECT       = 256; /*                                  */

        /* Line Capabilities */
        public const int
            LC_NONE            = 0,   /* Lines not supported              */
            LC_POLYLINE        = 2,   /* Can do polylines                 */
            LC_MARKER          = 4,   /* Can do markers                   */
            LC_POLYMARKER      = 8,   /* Can do polymarkers               */
            LC_WIDE            = 16,  /* Can do wide lines                */
            LC_STYLED          = 32,  /* Can do styled lines              */
            LC_WIDESTYLED      = 64,  /* Can do wide styled lines         */
            LC_INTERIORS       = 128; /* Can do interiors                 */

        /* Polygonal Capabilities */
        public const int 
            PC_NONE            = 0,   /* Polygonals not supported         */
            PC_POLYGON         = 1,   /* Can do polygons                  */
            PC_RECTANGLE       = 2,   /* Can do rectangles                */
            PC_WINDPOLYGON     = 4,   /* Can do winding polygons          */
            PC_TRAPEZOID       = 4,   /* Can do trapezoids                */
            PC_SCANLINE        = 8,   /* Can do scanlines                 */
            PC_WIDE            = 16,  /* Can do wide borders              */
            PC_STYLED          = 32,  /* Can do styled borders            */
            PC_WIDESTYLED      = 64,  /* Can do wide styled borders       */
            PC_INTERIORS       = 128, /* Can do interiors                 */
            PC_POLYPOLYGON     = 256, /* Can do polypolygons              */
            PC_PATHS           = 512; /* Can do paths                     */
        
        /* Raster Capabilities */
        public const int
            //RC_NONE
            RC_BITBLT          = 1,       /* Can do standard BLT.             */
            RC_BANDING         = 2,       /* Device requires banding support  */
            RC_SCALING         = 4,       /* Device requires scaling support  */
            RC_BITMAP64        = 8,       /* Device can support >64K bitmap   */
            RC_GDI20_OUTPUT    = 0x0010,      /* has 2.0 output calls         */
            RC_GDI20_STATE     = 0x0020,
            RC_SAVEBITMAP      = 0x0040,
            RC_DI_BITMAP       = 0x0080,      /* supports DIB to memory       */
            RC_PALETTE         = 0x0100,      /* supports a palette           */
            RC_DIBTODEV        = 0x0200,      /* supports DIBitsToDevice      */
            RC_BIGFONT         = 0x0400,      /* supports >64K fonts          */
            RC_STRETCHBLT      = 0x0800,      /* supports StretchBlt          */
            RC_FLOODFILL       = 0x1000,      /* supports FloodFill           */
            RC_STRETCHDIB      = 0x2000,      /* supports StretchDIBits       */
            RC_OP_DX_OUTPUT    = 0x4000,
            RC_DEVBITS         = 0x8000;

        // Text Capabilities
        // When TEXTCAPS is used with GetDeviceCaps
        // The value returned is a combination of these flags
        public const int
            TC_OP_CHARACTER    = 0x00000001,  /* Can do OutputPrecision   CHARACTER      */
            TC_OP_STROKE       = 0x00000002,  /* Can do OutputPrecision   STROKE         */
            TC_CP_STROKE       = 0x00000004,  /* Can do ClipPrecision     STROKE         */
            TC_CR_90           = 0x00000008,  /* Can do CharRotAbility    90             */
            TC_CR_ANY          = 0x00000010,  /* Can do CharRotAbility    ANY            */
            TC_SF_X_YINDEP     = 0x00000020,  /* Can do ScaleFreedom      X_YINDEPENDENT */
            TC_SA_DOUBLE       = 0x00000040,  /* Can do ScaleAbility      DOUBLE         */
            TC_SA_INTEGER      = 0x00000080,  /* Can do ScaleAbility      INTEGER        */
            TC_SA_CONTIN       = 0x00000100,  /* Can do ScaleAbility      CONTINUOUS     */
            TC_EA_DOUBLE       = 0x00000200,  /* Can do EmboldenAbility   DOUBLE         */
            TC_IA_ABLE         = 0x00000400,  /* Can do ItalisizeAbility  ABLE           */
            TC_UA_ABLE         = 0x00000800,  /* Can do UnderlineAbility  ABLE           */
            TC_SO_ABLE         = 0x00001000,  /* Can do StrikeOutAbility  ABLE           */
            TC_RA_ABLE         = 0x00002000,  /* Can do RasterFontAble    ABLE           */
            TC_VA_ABLE         = 0x00004000, /* Can do VectorFontAble    ABLE           */
            TC_RESERVED        = 0x00008000,
            TC_SCROLLBLT       = 0x00010000;  /* Don't do text scroll with blt           */

        // Used with GetGlyphOutline
        public const int
            TT_PRIM_LINE        = 1,            // Curve is a polyline. 
            TT_PRIM_QSPLINE     = 2,            // Curve is a quadratic Bézier spline. 
            TT_PRIM_CSPLINE     = 3;            // Curve is a cubic Bézier spline. 

        public const int
            TT_POLYGON_TYPE     = 24;

        public const int
            GGO_METRICS         = 0,
            GGO_BITMAP          = 1,
            GGO_NATIVE          = 2,
            GGO_BEZIER          = 3;

        public const int
            GGO_GRAY2_BITMAP    = 4,
            GGO_GRAY4_BITMAP    = 5,
            GGO_GRAY8_BITMAP    = 6,
            GGO_GLYPH_INDEX     = 0x0080;

        public const int
            GGO_UNHINTED        = 0x0100;

        public const int
            RGN_AND = 1,
            RGN_OR = 2,
            RGN_XOR = 3,
            RGN_DIFF = 4,
            RGN_COPY = 5,
            RGN_MIN = RGN_AND,
            RGN_MAX = RGN_COPY;

        public const int
            RGN_ERROR = 0,
            NULLREGION = 1,
            SIMPLEREGION = 2,
            COMPLEXREGION = 3;


    }
}
