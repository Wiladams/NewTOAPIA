/*
 * PointF is a 2 dimensional representation of a point using
 * floating point numbers.
 */

    /// <summary>
    /// A Point based on floating point values.
    /// </summary>
	public struct PointF
	{
		private float fX;
		private float fY;

		public static readonly PointF Empty = new PointF();

        /// <summary>
        /// Constructs a point from x and y values.
        /// </summary>
        /// <param name="x">X coordinate of point</param>
        /// <param name="y">Y coordinate of point</param>
		public PointF(float x, float y)
		{
			fX = x;
			fY = y;
		}

/*
		public PointF(Size2DF size)
		{
			fX = size.Width;
			fY = size.Height;
		}
*/
		/*
		 * Construct a point from a single 32-bit integer.
		 * The LOWORD contains the x portion
		 * The HIWORD contains the y portion
		 */
		public PointF(int dw)
		{
			this.fX = (float)BitUtils.LOWORD(dw);
            this.fY = (float)BitUtils.HIWORD(dw);
		}

		public bool IsEmpty
		{
			get 
			{
				return (fX == 0) && (fY == 0);
			}
		}

		public float X
		{
			get 
			{
				return fX;
			}

			set 
			{
				fX = value;
			}
		}

		public float Y
		{
			get 
			{
				return fY;
			}

			set 
			{
				fY = value;
			}
		}

		// Translate the point by the specified amount
		public void Offset(float dx, float dy)
		{
			X += dx;
			Y += dy;
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
		public static bool operator ==(PointF left, PointF right)
		{
			return left.X == right.X && left.Y == right.Y;
		}

		public static bool operator !=(PointF left, PointF right)
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
			if (!(obj is PointF))
			{
				return false;
			}

			PointF comp = (PointF)obj;

			return comp.X == this.X && comp.Y == this.Y;
		}
		
		public override string ToString()
		{
			return "<PointF X='" + X.ToString() +
				"' Y='" + Y.ToString() + "' />";
		}
	}