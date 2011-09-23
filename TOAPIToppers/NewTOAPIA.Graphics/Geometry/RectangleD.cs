namespace NewTOAPIA.Graphics
{
    using System;

    public struct RectangleD
    {
        static public RectangleD Empty = new RectangleD();

        public double x1;
        public double y1;
        public double x2;
        public double y2;

        public RectangleD(double x1_, double y1_, double x2_, double y2_)
        {
            x1 = x1_;
            y1 = y1_;
            x2 = x2_;
            y2 = y2_;

            Normalize();
        }

        #region Properties
        public double Left
        {
            get
            {
                return x1;
            }

            set
            {
                x1 = value;
                Normalize();
            }
        }

        public double Right
        {
            get
            {
                return x2 > x1 ? x2 : x1;
            }

            set
            {
                x2 = value;
                Normalize();
            }
        }

        public double Bottom
        {
            get
            {
                return y1;
            }

            set
            {
                y1 = value;
                Normalize();
            }
        }

        public double Top
        {
            get
            {
                return y2;
            }

            set
            {
                y2 = value;
                Normalize();
            }
        }

        public double Width
        {
            get
            {
                return Right - Left;
            }
        }

        public double Height
        {
            get
            {
                return Top - Bottom;
            }
        }
        #endregion

        public void SetRect(double x1_, double y1_, double x2_, double y2_)
        {
            x1 = x1_;
            y1 = y1_;
            x2 = x2_;
            y2 = y2_;

            Normalize();
        }

        RectangleD Normalize()
        {
            double t;
            if (x1 > x2) { t = x1; x1 = x2; x2 = t; }
            if (y1 > y2) { t = y1; y1 = y2; y2 = t; }

            return this;
        }

        public bool Clip(RectangleD r)
        {
            if (x2 > r.x2) x2 = r.x2;
            if (y2 > r.y2) y2 = r.y2;
            if (x1 < r.x1) x1 = r.x1;
            if (y1 < r.y1) y1 = r.y1;

            return IsValid();
        }

        public bool IsValid()
        {
            return x1 <= x2 && y1 <= y2;
        }

        public bool Contains(double x, double y)
        {
            return ((x >= Left) && (x <= Right) && (y >= Bottom) && (y <= Top));
        }

        // Change the shape of this rectangle by intersecting
        // it with another one.
        public void Intersect(RectangleD rect)
        {
            RectangleD result = RectangleD.Intersect(rect, this);
            this.x1 = result.Left;
            this.y1 = result.Bottom;
            this.x2 = result.x2;
            this.y2 = result.y2;
        }

        // Generic routine to create the intersection between
        // two rectangles.
        //
        public static RectangleD Intersect(RectangleD left, RectangleD right)
        {
            double x1 = Math.Max(left.x1, right.x1);
            double x2 = Math.Min(left.x2, right.x2);
            double y1 = Math.Max(left.y1, right.y1);
            double y2 = Math.Min(left.y2, right.y2);

            if (x2 >= x1 && y2 >= y1)
            {
                return new RectangleD(x1, y1, x2, y2);
            }

            return RectangleD.Empty;
        }

        public static RectangleD Union(RectangleD left, RectangleD right)
        {
            double x1 = Math.Min(left.Left, right.Left);
            double x2 = Math.Max(left.Right, right.Right);
            double y1 = Math.Min(left.Bottom, right.Bottom);
            double y2 = Math.Max(left.Top, right.Top);

            return new RectangleD(x1, y1, x2, y2);
        }

        public void Inflate(double inflateSize)
        {
            x1 = x1 - inflateSize;
            y1 = y1 - inflateSize;
            x2 = x2 + inflateSize;
            y2 = y2 + inflateSize;

            Normalize();
        }

        public void Offset(double x, double y)
        {
            x1 = x1 + x;
            y1 = y1 + y;
            x2 = x2 + x;
            y2 = y2 + y;
        }

    }
}