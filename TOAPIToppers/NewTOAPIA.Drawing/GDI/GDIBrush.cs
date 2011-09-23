using System;

using TOAPI.Types;
using TOAPI.GDI32;

namespace NewTOAPIA.Drawing
{
    /// <summary>
    /// GBrush styles
    /// BS_ constants from WinGDI.h
    /// </summary>
    public enum BrushStyle : int
    {
        Solid = 0,
        Hollow = 1,
        Hatched = 2,
        Pattern = 3,
        Indexed = 4,
        DIBPattern = 5,
        DIBPatternPT = 6,
        Pattern8x8 = 7,
        DIBPattern8X8 = 8,
        MonoPattern = 9
    }

    // From HS_ constants
    public enum HatchStyle
    {
        Horizontal = 0,       /* ----- */
        Vertical = 1,       /* ||||| */
        FDiagonal = 2,       /* \\\\\ */
        BDiagonal = 3,       /* ///// */
        Cross = 4,       /* +++++ */
        DiagCross = 5,       /* xxxxx */
    }


    /// <summary>
    /// Represent the default solid brush colors.
    /// </summary>
    public enum StockSolidBrush
    {
        White = 0,
        LightGray = 1,
        Gray = 2,
        DarkGray = 3,
        Black = 4,
        Hollow = 5,
        Null = 5,
        DC = 18
    }

    /// <summary>
    ///    Summary description for GDI32Brush.
    /// </summary>
    public class GDIBrush : GDIObject
    {
        #region Fields
        protected LOGBRUSH32 fLogBrush;

        UInt32 fColor;
        IntPtr fHatch;
        BrushStyle fBrushStyle;
        HatchStyle fHatchStyle;
        #endregion

        #region Constructors
        public GDIBrush()
            : this(RGBColor.Black)
        {
        }

        public GDIBrush(uint color)
            : this(BrushStyle.Solid, HatchStyle.Vertical, color, Guid.NewGuid())
        {
        }

        public GDIBrush(BrushStyle aStyle, HatchStyle hatchStyle, uint colorref, Guid uniqueID)
            : base(true, uniqueID)
        {
            fBrushStyle = aStyle;
            fHatchStyle = hatchStyle;
            fColor = colorref;

            fLogBrush = new LOGBRUSH32();

            // Make sure to mask off the high order byte
            // or GDI will draw in black
            fLogBrush.lbColor = colorref & 0x00ffffff;
            fLogBrush.lbHatch = (int)hatchStyle;
            fLogBrush.lbStyle = (int)aStyle;

            IntPtr brushHandle = GDI32.CreateBrushIndirect(ref fLogBrush);

            SetHandle(brushHandle);
        }
        #endregion

        #region Properties
        public BrushStyle Style
        {
            get { return fBrushStyle; }
        }

        public HatchStyle Hatching
        {
            get { return fHatchStyle; }
        }

        public uint Color
        {
            get { return fColor; }
        }

        public virtual LOGBRUSH32 LogicalBrush
        {
            get { return fLogBrush; }
        }
        #endregion
    }
}

