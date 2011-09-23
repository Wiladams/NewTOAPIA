using System;

using TOAPI.Types;
using TOAPI.GDI32;

namespace NewTOAPIA.Drawing.GDI
{
    using NewTOAPIA.Drawing;
    using NewTOAPIA.Graphics;

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
    public class GDIBrush : GDIObject, IBrush
    {
        #region Fields
        protected LOGBRUSH32 fLogBrush;

        Colorref fColor;
        //IntPtr fHatch;
        BrushStyle fBrushStyle;
        HatchStyle fHatchStyle;
        #endregion

        #region Constructors
        public GDIBrush()
            : this((Colorref)Colorrefs.Black)
        {
        }

        public GDIBrush(Colorref color)
            : this(BrushStyle.Solid, HatchStyle.Vertical, color, Guid.NewGuid())
        {
        }

        public GDIBrush(BrushStyle aStyle, HatchStyle hatchStyle, Colorref colorref, Guid uniqueID)
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
        public BrushStyle BrushStyle
        {
            get { return fBrushStyle; }
        }

        public HatchStyle HatchStyle
        {
            get { return fHatchStyle; }
        }

        public uint InternalColor
        {
            get { return fColor; }
        }

        public Colorref Color
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

