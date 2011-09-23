using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.Drawing
{
    public class LineG : GPath
    {
        Point fStartPoint;
        Point fEndPoint;

        public LineG(Point startPoint, Point endPoint)
            :this(startPoint.X, startPoint.Y, endPoint.X, endPoint.Y)
        {
        }

        public LineG(int x1, int y1, int x2, int y2)
        {
            fStartPoint = new Point(x1, y1);
            fEndPoint = new Point(x2, y2);

            Begin();
            MoveTo(x1, y1, false);
            LineTo(x2, y2, true);
            End();
        }

        #region Properties
        public Point StartPoint
        {
            get { return fStartPoint; }
        }

        public Point EndPoint
        {
            get { return fEndPoint; }
        }
        #endregion
    }
}
