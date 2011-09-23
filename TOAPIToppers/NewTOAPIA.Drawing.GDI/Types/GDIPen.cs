using System;

using TOAPI.Types;
using TOAPI.GDI32;

namespace NewTOAPIA.Drawing.GDI
{
    using NewTOAPIA.Drawing;
    using NewTOAPIA.Graphics;

    [Serializable]
    public class GDIPen : GDIObject, IPen
    {
        LOGBRUSH fLogBrush;

        public PenType TypeOfPen { get; set; }
        public PenStyle Style { get; set; }
        public PenJoinStyle JoinStyle { get; set; }
        public PenEndCap EndCap { get; set; }
        public int Width { get; set; }
        public Colorref Color { get; set; }

        #region Constructors
        public GDIPen(Colorref colorref)
            : this(PenType.Cosmetic, PenStyle.Solid, PenJoinStyle.Round, PenEndCap.Round, colorref, 1, Guid.NewGuid())
        {
        }

        public GDIPen(PenType aType, PenStyle aStyle, PenJoinStyle aJoinStyle, PenEndCap aEndCap, Colorref colorref, int width, Guid uniqueID)
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