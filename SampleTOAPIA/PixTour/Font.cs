using System;

namespace Papyrus.Types
{
    public class Font : IHandle
    {
        public int fHeight;
        public int fWidth;
        public int fEscapement;
        public int fOrientation;
        public Font.FontWeight fWeight;
        public bool fItalic;
        public bool fUnderline;
        public bool fStrikeOut;
        public byte fCharSet;
        public byte fOutPrecision;
        public byte fClipPrecision;
        public Font.Quality fQuality;
        public byte fPitchAndFamily;
        public string fFaceName;


        public virtual IntPtr Handle
        {
            get { return IntPtr.Zero; }
        }




        [Flags]
        public enum Style : int
        {
            Regular = 0x00,
            Bold = 0x01,
            Italic = 0x02,
            Underline = 0x04,
            Strikethrough = 0x10
        }

        // Various constants for fonts
        public static int
            FF_DONTCARE = (0 << 4),
            FF_ROMAN = (1 << 4),
            FF_SWISS = (2 << 4),
            FF_MODERN = (3 << 4),
            FF_SCRIPT = (4 << 4),
            FF_DECORATIVE = (5 << 4);

        public enum FontWeight : short
        {
            DONTCARE = 0,
            THIN = 100,
            EXTRALIGHT = 200,
            ULTRALIGHT = 200,
            LIGHT = 300,
            NORMAL = 400,
            REGULAR = 400,
            MEDIUM = 500,
            SEMIBOLD = 600,
            DEMIBOLD = 600,
            BOLD = 700,
            EXTRABOLD = 800,
            ULTRABOLD = 800,
            HEAVY = 900,
        }

        public static int
            AnsiFixedFont = 11,
            AnsiVarFont = 12,
            DeviceDefaultFont = 14,
            OEMFixedFont = 10,
            SystemFont = 13,
            SystemFixedFont = 16;

        // font quality constants used for LOGFONT quality
        public enum Quality : byte
        {
            DEFAULT = 0,
            DRAFT = 1,
            PROOF = 2,
            NONANTIALIASED = 3,
            ANTIALIASED = 4,
            CLEARTYPE = 5,
            CLEARTYPE_NATURAL = 6
        }

        // PRECIS constants used with LOGFONT
        public const int
            OUT_DEFAULT_PRECIS = 0,
            OUT_STRING_PRECIS = 1,
            OUT_CHARACTER_PRECIS = 2,
            OUT_STROKE_PRECIS = 3,
            OUT_TT_PRECIS = 4,
            OUT_DEVICE_PRECIS = 5,
            OUT_RASTER_PRECIS = 6,
            OUT_TT_ONLY_PRECIS = 7,
            OUT_OUTLINE_PRECIS = 8,
            OUT_SCREEN_OUTLINE_PRECIS = 9,
            OUT_PS_ONLY_PRECIS = 10;


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
        #define FS_LATIN1               0x00000001L
        #define FS_LATIN2               0x00000002L
        #define FS_CYRILLIC             0x00000004L
        #define FS_GREEK                0x00000008L
        #define FS_TURKISH              0x00000010L
        #define FS_HEBREW               0x00000020L
        #define FS_ARABIC               0x00000040L
        #define FS_BALTIC               0x00000080L
        #define FS_VIETNAMESE           0x00000100L
        #define FS_THAI                 0x00010000L
        #define FS_JISJAPAN             0x00020000L
        #define FS_CHINESESIMP          0x00040000L
        #define FS_WANSUNG              0x00080000L
        #define FS_CHINESETRAD          0x00100000L
        #define FS_JOHAB                0x00200000L
        #define FS_SYMBOL               0x80000000L
        */

        // Pitch constants for LOGFONT
        public enum Pitch : int
        {
            Default = 0,
            Fixed = 1,
            Variable = 2,
            Mono = 8
        }
    }
}
