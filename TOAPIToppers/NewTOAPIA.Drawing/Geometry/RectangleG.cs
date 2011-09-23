
namespace NewTOAPIA.Drawing
{
    using NewTOAPIA.Graphics;

    public class RectangleG : GPath
    {
        RectangleI fRectangle;

        public RectangleG(int x, int y, int width, int height)
        {
            fRectangle = new RectangleI(x, y, width, height);

            Begin();
            MoveTo(x, y, false);
            LineTo(x, y + height, false);
            LineTo(x + width, y + height, false);
            LineTo(x + width, y, false);
            LineTo(x, y, true);
            End();
        }

        public RectangleI Extent
        {
            get { return fRectangle; }
        }
    }
}
