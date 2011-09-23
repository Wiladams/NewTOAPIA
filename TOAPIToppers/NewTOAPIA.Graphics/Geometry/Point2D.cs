namespace NewTOAPIA.Graphics
{
    public struct Point2D
    {
        public double x;
        public double y;

        public Point2D(double value)
        {
            x = value;
            y = value;
        }

        public Point2D(Point2D pt)
        {
            x = pt.x;
            y = pt.y;
        }

        public Point2D(double x_, double y_)
        {
            x = x_;
            y = y_;
        }

        public static Point2D operator * (Point2D pt, double value)
        {
            return new Point2D(pt.x * value, pt.y * value);
        }
    }
}
