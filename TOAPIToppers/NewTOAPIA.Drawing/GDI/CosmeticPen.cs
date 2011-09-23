using System;

using TOAPI.Types;
using TOAPI.GDI32;

namespace NewTOAPIA.Drawing
{

    public class GDICosmeticPen : GDIPen
    {
        public static GDIPen Black;
        public static GDIPen Red;
        public static GDIPen Blue;
        public static GDIPen Green;

        static GDICosmeticPen()
        {
            Black = new GDICosmeticPen(RGBColor.Black);
            Red = new GDICosmeticPen(RGBColor.Red);
            Green = new GDICosmeticPen(RGBColor.Green);
            Blue = new GDICosmeticPen(RGBColor.Blue);
        }

        public GDICosmeticPen(uint colorref)
            : this(PenStyle.Solid, colorref, Guid.NewGuid())
        {
        }

        public GDICosmeticPen(PenStyle penStyle, uint colorref, Guid uniqueID)
            : base(PenType.Cosmetic, penStyle, PenJoinStyle.Round, PenEndCap.Round, colorref, 1, uniqueID)
        {
        }
    }
}