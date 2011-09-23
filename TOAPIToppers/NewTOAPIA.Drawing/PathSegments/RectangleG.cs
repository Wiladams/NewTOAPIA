using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.Drawing
{
    public class RectangleG : GPath
    {
        Rectangle fRectangle;

        public RectangleG(int x, int y, int width, int height)
        {
            fRectangle = new Rectangle(x, y, width, height);

            Begin();
            MoveTo(x, y, false);
            LineTo(x, y + height, false);
            LineTo(x + width, y + height, false);
            LineTo(x + width, y, false);
            LineTo(x, y, true);
            End();
        }

        public Rectangle Extent
        {
            get { return fRectangle; }
        }
    }
}
