namespace NewTOAPIA.Graphics
{
    using System;

    public struct RectangleI
    {
        static public RectangleI Empty = new RectangleI();

        public int x1;
        public int y1;
        public int x2;
        public int y2;

        public RectangleI(Point2I origin, Size2I size)
        {
            x1 = origin.x;
            y1 = origin.y;

            x2 = x1 + size.width;
            y2 = y1 + size.height;
        }

        public RectangleI(int x1_, int y1_, int x2_, int y2_)
        {
            x1 = x1_;
            y1 = y1_;
            x2 = x2_;
            y2 = y2_;

            Normalize();
        }

        #region Properties
        public bool IsEmpty
        {
            get
            {
                return (x1 == x2) && (y1 == y2);
            }
        }

        public int Left
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

        public int Right
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

        public int Bottom
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

        public int Top
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

        public int Width
        {
            get
            {
                return Right - Left;
            }
        }

        public int Height
        {
            get
            {
                return Bottom - Top;
            }
        }
        #endregion

        public void SetRect(int x1_, int y1_, int x2_, int y2_)
        {
            x1 = x1_;
            y1 = y1_;
            x2 = x2_;
            y2 = y2_;

            Normalize();
        }

        public RectangleI Normalize()
        {
            int t;
            if (x1 > x2) { t = x1; x1 = x2; x2 = t; }
            if (y1 > y2) { t = y1; y1 = y2; y2 = t; }

            return this;
        }

        public bool Clip(RectangleI r)
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

        public bool Contains(int x, int y)
        {
            return ((x >= Left) && (x <= Right) && (y >= Bottom) && (y <= Top));
        }

        // Change the shape of this rectangle by intersecting
        // it with another one.
        public void Intersect(RectangleI rect)
        {
            RectangleI result = RectangleI.Intersect(rect, this);
            this.x1 = result.Left;
            this.y1 = result.Bottom;
            this.x2 = result.x2;
            this.y2 = result.y2;
        }

        // Generic routine to create the intersection between
        // two rectangles.
        //
        public static RectangleI Intersect(RectangleI left, RectangleI right)
        {
            int x1 = Math.Max(left.x1, right.x1);
            int x2 = Math.Min(left.x2, right.x2);
            int y1 = Math.Max(left.y1, right.y1);
            int y2 = Math.Min(left.y2, right.y2);

            if (x2 >= x1 && y2 >= y1)
            {
                return new RectangleI(x1, y1, x2, y2);
            }

            return RectangleI.Empty;
        }

        public static RectangleI Union(RectangleI left, RectangleI right)
        {
            int x1 = Math.Min(left.Left, right.Left);
            int x2 = Math.Max(left.Right, right.Right);
            int y1 = Math.Min(left.Bottom, right.Bottom);
            int y2 = Math.Max(left.Top, right.Top);

            return new RectangleI(x1, y1, x2, y2);
        }

        public void Inflate(int inflateSize)
        {
            x1 = x1 - inflateSize;
            y1 = y1 - inflateSize;
            x2 = x2 + inflateSize;
            y2 = y2 + inflateSize;

            Normalize();
        }

        public void Inflate(int dx, int dy)
        {
            x1 = x1 - dx;
            y1 = y1 - dy;
            x2 = x2 + dx;
            y2 = y2 + dy;

            Normalize();
        }

        public void Offset(int x, int y)
        {
            x1 = x1 + x;
            y1 = y1 + y;
            x2 = x2 + x;
            y2 = y2 + y;
        }

    }
}