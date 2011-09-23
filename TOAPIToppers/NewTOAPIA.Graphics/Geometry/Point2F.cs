namespace NewTOAPIA.Graphics
{
    using NewTOAPIA;

    /*
     * PointF is a 2 dimensional representation of a point using
     * floating point numbers.
     */

    /// <summary>
    /// A Point based on floating point values.
    /// </summary>
    public struct Point2F
    {
        public static readonly Point2F Empty = new Point2F();

        public float x;
        public float y;


        /// <summary>
        /// Constructs a point from x and y values.
        /// </summary>
        /// <param name="x">X coordinate of point</param>
        /// <param name="y">Y coordinate of point</param>
        public Point2F(float x_, float y_)
        {
            x = x_;
            y = y_;
        }

        /*
         * Construct a point from a single 32-bit integer.
         * The Loword contains the x portion
         * The Hiword contains the y portion
         */
        public Point2F(int dw)
        {
            this.x = (float)BitUtils.Loword(dw);
            this.y = (float)BitUtils.Hiword(dw);
        }


        // Translate the point by the specified amount
        public void Offset(float dx, float dy)
        {
            x += dx;
            y += dy;
        }

        /*
                // Translate a point by the given size
                public static PointF operator +(PointF pt, Size2DF sz)
                {
                    return new Point2DF(pt.X+sz.Width, pt.Y+sz.Height);
                }

                // Translate a point by the given size
                public static Point2DF operator -(Point2DF pt, Size2DF sz)
                {
                    return new Point2DF(pt.X-sz.Width, pt.Y-sz.Height);
                }
        */
        public static bool operator ==(Point2F left, Point2F right)
        {
            return left.x == right.x && left.y == right.y;
        }

        public static bool operator !=(Point2F left, Point2F right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            // TODO: fill this part in;
            return -1;
        }

        // Check Point for equivalency against another generic object
        // This is a very expensive operation, and the typecast may
        // fail and throw a exception.  So, the additional GetType
        // on the return is not really needed, but a try/catch should 
        // be placed around the explicit typecast.
        public override bool Equals(object obj)
        {
            if (!(obj is Point2F))
            {
                return false;
            }

            Point2F comp = (Point2F)obj;

            return comp.x == this.x && comp.y == this.y;
        }

        public override string ToString()
        {
            return "<PointF X='" + x.ToString() +
                "' Y='" + y.ToString() + "' />";
        }
    }
}