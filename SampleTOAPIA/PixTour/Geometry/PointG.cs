


namespace Papyrus.Types
{
    using System;
    using System.Runtime.InteropServices;
    
    /// <summary>
    /// PointG
    /// 
    /// Basic geometric representation of a 2 dimensional point in the graphics system.
    /// This point is represented with two integer values.
    /// It is an equivalent type for Win32's POINT structure, and can be used in all 
    /// system calls requiring that structure.
    /// </summary>
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct PointG
	{
		public int X;
		public int Y;

		public static readonly PointG Empty = new PointG();

		public PointG(int x, int y)
		{
			X = x;
			Y = y;
		}

        public PointG(SizeG size)
		{
			X = size.Width;
			Y = size.Height;
		}

		/*
		 * Construct a point from a single 32-bit integer.
		 * The LOWORD contains the x portion
		 * The HIWORD contains the y portion
		 */
		public PointG(int dw)
		{
			X = (short)BitUtils.LOWORD(dw);
			Y = (short)BitUtils.HIWORD(dw);
		}

        //// Return a Point2DF object from a PointG
        ////public static implicit operator Point2DF(PointG pt)
        ////{
        ////	return new Point2DF(pt.X, pt.Y);
        ////}

        /// <summary>
        /// Return a Size object from a PointG
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public static explicit operator SizeG(PointG pt)
		{
			return new SizeG(pt.X, pt.Y);
		}

		// Translate the point by the specified amount
		public void Offset(int dx, int dy)
		{
			X += dx;
			Y += dy;
		}

		// Translate a point by the given size
        public static PointG operator +(PointG pt, SizeG sz)
		{
			return new PointG(pt.X + sz.Width, pt.Y + sz.Height);
		}

		// Translate a point by the given size
        public static PointG operator -(PointG pt, SizeG sz)
		{
			return new PointG(pt.X - sz.Width, pt.Y - sz.Height);
		}

		public static bool operator ==(PointG left, PointG right)
		{
			return (left.X == right.X) && (left.Y == right.Y);
		}

		public static bool operator != (PointG left, PointG right)
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
            if (!(obj is PointG))
            {
                return false;
            }

            PointG comp = (PointG)obj;

            return comp.X == this.X && comp.Y == this.Y;
        }

        public override string ToString()
        {
            return "<PointG X='" + X.ToString() +
                "' Y='" + Y.ToString() + " />";
        }

    }
}