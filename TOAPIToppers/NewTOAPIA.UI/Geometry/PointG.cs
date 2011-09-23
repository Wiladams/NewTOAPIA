


namespace Papyrus.Types
{
    using System;
    using System.Runtime.InteropServices;
    
    /// <summary>
    /// Point
    /// 
    /// Basic geometric representation of a 2 dimensional point in the graphics system.
    /// This point is represented with two integer values.
    /// It is an equivalent type for Win32's POINT structure, and can be used in all 
    /// system calls requiring that structure.
    /// </summary>
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct Point
	{
		public int X;
		public int Y;

		public static readonly Point Empty = new Point();

		public Point(int x, int y)
		{
			X = x;
			Y = y;
		}

        public Point(Size size)
		{
			X = size.Width;
			Y = size.Height;
		}

		/*
		 * Construct a point from a single 32-bit integer.
		 * The LOWORD contains the x portion
		 * The HIWORD contains the y portion
		 */
		public Point(int dw)
		{
			X = (short)BitUtils.LOWORD(dw);
			Y = (short)BitUtils.HIWORD(dw);
		}

        //// Return a Point2DF object from a Point
        ////public static implicit operator Point2DF(Point pt)
        ////{
        ////	return new Point2DF(pt.X, pt.Y);
        ////}

        /// <summary>
        /// Return a Size object from a Point
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public static explicit operator Size(Point pt)
		{
			return new Size(pt.X, pt.Y);
		}

		// Translate the point by the specified amount
		public void Offset(int dx, int dy)
		{
			X += dx;
			Y += dy;
		}

		// Translate a point by the given size
        public static Point operator +(Point pt, Size sz)
		{
			return new Point(pt.X + sz.Width, pt.Y + sz.Height);
		}

		// Translate a point by the given size
        public static Point operator -(Point pt, Size sz)
		{
			return new Point(pt.X - sz.Width, pt.Y - sz.Height);
		}

		public static bool operator ==(Point left, Point right)
		{
			return (left.X == right.X) && (left.Y == right.Y);
		}

		public static bool operator != (Point left, Point right)
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
            if (!(obj is Point))
            {
                return false;
            }

            Point comp = (Point)obj;

            return comp.X == this.X && comp.Y == this.Y;
        }

        public override string ToString()
        {
            return "<Point X='" + X.ToString() +
                "' Y='" + Y.ToString() + " />";
        }

    }
}