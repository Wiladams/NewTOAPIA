using System;

using TOAPI.Types;
using TOAPI.GDI32;

namespace NewTOAPIA.Drawing
{
    // Type of pen
    //public enum PenType : int
    //{
    //    Cosmetic = 0x00000000,
    //    Geometric = 0x00010000,
    //}

    //// Pen Styles
    //public enum PenStyle : int
    //{
    //    Solid = 0,
    //    Dash = 1,       /* -------  */
    //    Dot = 2,       /* .......  */
    //    DashDot = 3,       /* _._._._  */
    //    DashDotDot = 4,       /* _.._.._  */
    //    Null = 5,
    //    InsideFrame = 6,
    //    UserStyle = 7,
    //    Alternate = 8,
    //    StyleMask = 0x0000000F
    //}

    //// Pen End Cap
    //public enum PenEndCap : int
    //{
    //    Round = 0x00000000,
    //    Square = 0x00000100,
    //    Flat = 0x00000200,
    //    EndcapMask = 0x00000F00
    //}

    //// Style of joining lines
    //public enum PenJoinStyle : int
    //{
    //    Round = 0x00000000,
    //    Bevel = 0x00001000,
    //    Miter = 0x00002000,
    //    JoinMask = 0x0000F000
    //}


    /// <summary>
    /// StockSolidPen
    /// </summary>
    //public enum PenStockSolid
    //{
    //    Black = 7,
    //    Null = 8,
    //    White = 6,
    //    DC = 19		// From WinGdi.h
    //}

    [Serializable]
    public class GDIPen : GDIObject, IPen
    {
        LOGBRUSH fLogBrush;

        public PenType TypeOfPen { get; set; }
        public PenStyle Style { get; set; }
        public PenJoinStyle JoinStyle { get; set; }
        public PenEndCap EndCap { get; set; }
        public int Width { get; set; }
        public uint Color { get; set; }

        #region Constructors
        public GDIPen(uint colorref)
            : this(PenType.Cosmetic, PenStyle.Solid, PenJoinStyle.Round, PenEndCap.Round, colorref, 1, Guid.NewGuid())
        {
        }

        public GDIPen(PenType aType, PenStyle aStyle, PenJoinStyle aJoinStyle, PenEndCap aEndCap, uint colorref, int width, Guid uniqueID)
            : base(true,uniqueID)
        {
            TypeOfPen = aType;
            Style = aStyle;
            JoinStyle = aJoinStyle;
            EndCap = aEndCap;
            Width = width;
            Color = colorref;


            int combinedStyle = (int)aStyle | (int)aType | (int)aJoinStyle | (int)aEndCap;
            fLogBrush = new LOGBRUSH();
            fLogBrush.lbColor = colorref;
            fLogBrush.lbHatch = IntPtr.Zero;
            fLogBrush.lbStyle = (int)BrushStyle.Solid;

            if (PenType.Cosmetic == aType)
            {
                // If it's cosmetic, the width must be 1
                width = 1;

                // The color must be in the brush structure
                // Must mask off the alpha, or we'll get black
                fLogBrush.lbColor = colorref & 0x00ffffff;

                // The brush style must be solid
                fLogBrush.lbStyle = (int)BrushStyle.Solid;
            }


            IntPtr penHandle = GDI32.ExtCreatePen((uint)combinedStyle, (uint)width, ref fLogBrush, 0, IntPtr.Zero);

            SetHandle(penHandle);
        }
        #endregion


        //public static explicit operator GPen(GDIPen aPen)
        //{
        //    GPen aNewPen = new GPen(aPen.Color, aPen.TypeOfPen, aPen.Style, aPen.JoinStyle, aPen.EndCap, aPen.Width, aPen.UniqueID);

        //    return aNewPen;
        //}

    }
}