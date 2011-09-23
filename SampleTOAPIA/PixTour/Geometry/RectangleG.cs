using System;
using System.Runtime.InteropServices;

/// <summary>
/// RectangleG
///
/// This is the basic geometry of a rectangle for the system.
/// This structure is compatible with the win32 RECT structure,
/// and can be used in all system calls that require RECT.  In addition
/// the methods in this class implement the various system provided Rectangle
/// manipulation functions: 
/// CopyRect
/// EqualRect
/// InflateRect
/// IntersectRect
/// IsRectEmpty
/// OffsetRect
/// PtInRect
/// SetRect
/// SetRectEmpty
/// SubtractRect
/// UnionRect
/// 
/// This is also a replacement for the Win32 supplied RECTL function which 
/// is used when reading graphics metafiles.
/// </summary>
[ComVisible(true)]
[StructLayout(LayoutKind.Sequential, Pack=1)]
public struct Rectangle
{
	private Int32 fLeft;
    private Int32 fTop;
    private Int32 fRight;
    private Int32 fBottom;

	public static readonly RectangleG Empty = new RectangleG();

	public RectangleG(int x, int y, int width, int height)
	{
		fLeft = x;
		fTop = y;
		fRight = fLeft + width;
		fBottom = fTop + height;
	}

	public RectangleG(Point origin, Size size)
		: this(origin.X, origin.Y, size.Width, size.Height)
	{
	}

	// Some properties
    public PointG Location
	{
		get 
		{
            return new PointG(X, Y);
		}

		set
		{
			X = value.X;
			Y = value.Y;
		}
	}

    public SizeG Size
	{
		get 
		{
            return new SizeG(Width, Height);
		}

		set 
		{
			Width = value.Width;
			Height = value.Height;
		}
	}

	public int X 
	{
		get 
		{
			return fLeft;
		}

		set 
		{
			fLeft = value;
		}
	}

	public int Y
	{
		get 
		{
			return fTop;
		}

		set 
		{
			fTop = value;
		}
	}

	public int Width 
	{
		get 
		{
			return fRight - fLeft;
		}

		set 
		{
			fRight = fLeft + value;
		}
	}

	public int Height 
	{
		get 
		{
			return fBottom - fTop;
		}

		set 
		{
			fBottom = fTop + value;
		}
	}

    public int Left 
    {
        get { return fLeft; }
        set{ fLeft = value; }
    }

	public int Top
	{
		get 
		{
			return fTop;
		}

		set 
		{
			fTop = value;
		}
	}

	public int Right
	{
		get
		{
			return fRight;
		}
		set { fRight = value; }
	}

	public int Bottom 
	{
		get 
		{
			return fBottom;
		}
		set { fBottom = value; }
	}

	/// <summary>
	/// Contains
    /// 
    /// Contains on rectangles is always tricky.
    /// The geometry here favors the upper left edge of a 
    /// pixel.  So, if you have a rectangle with origin
    /// 0,0, and size: 5,5.  The point 5,5 will NOT be Contained
    /// within the rectangle.  As long as everyone believes
    /// in this geometry, everything works out just fine.
    /// </summary>
	/// <param name="x">The X coordinate of the point to be tested.</param>
	/// <param name="y">The Y coordinate of the point to be tested.</param>
	/// <returns></returns>
    public bool Contains(int x, int y)
	{
		return (this.X <= x) && (x < this.Right) &&
			(this.Y <= y) && (y < this.Bottom);
	}

    public bool Contains(PointG pt)
	{
		return Contains(pt.X, pt.Y);
	}

	public bool Contains(RectangleG rect)
	{
		return (this.X <= rect.X) &&
			((rect.Right) <= (this.Right)) &&
			(this.Y <= rect.Y) &&
			((rect.Bottom) <= (this.Bottom));
	}

    /// <summary>
    /// Offset
    /// Displace the rectangle by the specified amount in both the
    /// x, and y axis.
    /// </summary>
    /// <param name="dx">The amount to displace the rectangle in the x axis.</param>
    /// <param name="dy">The amount to displace the rectangle in the y axis.</param>
	public void Offset(int dx, int dy)
	{
		fLeft += dx;
		fTop += dy;
		fRight += dx;
		fBottom += dy;
	}

    public void Offset(SizeG sz)
	{
		Offset(sz.Width, sz.Height);	
	}

	public void Resize(int dw, int dh)
	{
		fRight += dw;
		fBottom += dh;
	}

	public void Inset(int dw, int dh) 
	{
		Width = Width - dw;
		Height = Height - dh;

		X = X + (dw / 2);
		Y = Y + (dh / 2);
	}

	
	// Change the shape of this rectangle by intersecting
	// it with another one.
	public void Intersect(RectangleG rect)
	{
		RectangleG result = RectangleG.Intersect(rect, this);
		this.X = result.X;
		this.Y = result.Y;
		this.Width = result.Width;
		this.Height = result.Height;
	}

	// Generic routine to create the intersection between
	// two rectangles.
	//
	public static RectangleG Intersect(RectangleG left, RectangleG right)
	{
		int x1 = Math.Max(left.X, right.X);
		int x2 = Math.Min(left.Right, right.Right);
		int y1 = Math.Max(left.Y, right.Y);
		int y2 = Math.Min(left.Bottom, right.Bottom);

		if (x2 >= x1 && y2 >= y1)
		{
			return new RectangleG(x1, y1, x2 - x1, y2 - y1);
		}

		return RectangleG.Empty;
	}

	// Does this rectangle intersect with the one specified?
	public bool IntersectsWith(RectangleG rect)
	{
		return (rect.X < Right) &&
			(this.X < rect.Right) &&
			(rect.Y < this.Bottom) &&
			(this.Y < rect.Bottom);
	}

	public void Union(RectangleG rect)
	{
		RectangleG result = RectangleG.Union(this, rect);

		this.X = result.X;
		this.Y = result.Y;
		this.Width = result.Width;
		this.Height = result.Height;
	}

	public static RectangleG Union(RectangleG left, RectangleG right)
	{
		int x1 = Math.Min(left.X, right.X);
		int x2 = Math.Max(left.Right, right.Right);
		int y1 = Math.Min(left.Y, right.Y);
		int y2 = Math.Max(left.Bottom, right.Bottom);
		
		return new RectangleG(x1, y1, x2 - x1, y2 - y1);
	}

	// Override from Object
	public override bool Equals(object obj)
	{
        if (!(obj is RectangleG))
        {
            return false;
        }

		RectangleG comp = (RectangleG)obj;
	
		return (comp.X == this.X) &&
			(comp.Y == this.Y) &&
			(comp.Width == this.Width) &&
			(comp.Height == this.Height);
	}

	public static bool operator == (RectangleG left, RectangleG right)
	{
		return (left.X == right.X) &&
			(left.Y == right.Y) &&
			(left.Width == right.Width) &&
			(left.Height == right.Height);
	}

	public static bool operator !=(RectangleG left, RectangleG right)
	{
		return !(left == right);
	}

    public override int GetHashCode()
    {
        // TODO: fill this part in;
        return -1;
    }

    public override string ToString()
    {
        return "<RectangleG X='" + X.ToString() +
            "' Y='" + Y.ToString() +
            "' Width = '" + Width.ToString() +
            "' Height = '" + Height.ToString() + " />";
    }
}
