﻿namespace NewTOAPIA.Graphics
{
    public struct Point2I
    {
        public int x;
        public int y;

        public Point2I(int value)
        {
            x = value;
            y = value;
        }

        public Point2I(Point2I pt)
        {
            x = pt.x;
            y = pt.y;
        }

        public Point2I(int x_, int y_)
        {
            x = x_;
            y = y_;
        }

        public static Point2I operator *(Point2I pt, double value)
        {
            return new Point2I((int)(pt.x * value), (int)(pt.y * value));
        }
    }
}
