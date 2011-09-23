
namespace NewTOAPIA.Drawing
{
    using NewTOAPIA.Graphics;

    public class LineSegment : GPath
    {
        public Point3D StartPoint { get; private set; }
        public Point3D EndPoint { get; private set; }

        public LineSegment(Point3D startPoint, Point3D endPoint)
            :this(startPoint.x, startPoint.y, endPoint.x, endPoint.y)
        {
        }

        public LineSegment(double x1, double y1, double x2, double y2)
        {
            StartPoint = new Point3D(x1, y1);
            EndPoint = new Point3D(x2, y2);

            Begin();
            MoveTo(x1, y1, false);
            LineTo(x2, y2, true);
            End();
        }
    }
}
