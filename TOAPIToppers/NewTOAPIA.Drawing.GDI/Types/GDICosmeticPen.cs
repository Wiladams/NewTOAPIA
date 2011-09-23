using System;

using TOAPI.Types;
using TOAPI.GDI32;

namespace NewTOAPIA.Drawing.GDI
{
    using NewTOAPIA.Drawing;
    using NewTOAPIA.Graphics;

    public class GDICosmeticPen : GDIPen
    {
        public static GDIPen Black;
        public static GDIPen Red;
        public static GDIPen Blue;
        public static GDIPen Green;

        static GDICosmeticPen()
        {
            Black = new GDICosmeticPen((Colorref)Colorrefs.Black);
            Red = new GDICosmeticPen((Colorref)Colorrefs.Red);
            Green = new GDICosmeticPen((Colorref)Colorrefs.Green);
            Blue = new GDICosmeticPen((Colorref)Colorrefs.Blue);
        }

        public GDICosmeticPen(Colorref colorref)
            : this(PenStyle.Solid, colorref, Guid.NewGuid())
        {
        }

        public GDICosmeticPen(PenStyle penStyle, Colorref colorref, Guid uniqueID)
            : base(PenType.Cosmetic, penStyle, PenJoinStyle.Round, PenEndCap.Round, colorref, 1, uniqueID)
        {
        }
    }
}