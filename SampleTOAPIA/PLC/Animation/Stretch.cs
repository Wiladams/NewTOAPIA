using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

using NewTOAPIA.Drawing;
using NewTOAPIA.Drawing.GDI;

/// <summary>
/// </summary>
public class Stretch : ImageTransition
{
    int fDistance;
	int fDirection;
    int fRevealed;
    Point[] srcDstPoints;

    public Stretch(string name, GDIDIBSection srcPixelBuffer, Rectangle frame, double duration, int direction)
		: base(name, frame, null, srcPixelBuffer, duration)
	{
		// Calculate the increment in OnReset because it depends on the direction
		// And the direction will not have been set yet.
		fDirection = direction;

        switch (fDirection)
        {
            case RIGHT:
            case LEFT:
                fDistance = Frame.Width;
                break;

            case UP:
            case DOWN:
                fDistance = Frame.Height;
                break;
        }

    }

    public override void OnReset()
    {
        srcDstPoints = new Point[] {
			new Point(Frame.Left, Frame.Top),
			new Point(Frame.Right,Frame.Top),
			new Point(Frame.Left, Frame.Bottom)
		};

        switch (fDirection)
        {
            case RIGHT:
            case LEFT:
                fDistance = Frame.Width;
                break;

            case UP:
            case DOWN:
                fDistance = Frame.Height;
                break;
        }

        fRevealed = 0;
    }

    public override void UpdateGeometryState(double atTime)
    {
        fRevealed = (int)Math.Floor((fDistance * atTime) + 0.5); 
    }

	public virtual int Revealed
	{
        get
        {
            return fRevealed;
		}
	}

	public virtual Rectangle SourceRectangle()
	{
		Rectangle srcRect = Rectangle.Empty;
			
		switch(fDirection)
		{
			case DOWN:
				srcRect = new Rectangle(0, SourcePixelBuffer.Height - Revealed, SourcePixelBuffer.Width, Revealed);
				break;
			case UP:
				srcRect = new Rectangle(0, 0, SourcePixelBuffer.Width, Revealed);
				break;

			case RIGHT:
				srcRect = new Rectangle(SourcePixelBuffer.Width - Revealed, 0, Revealed, SourcePixelBuffer.Height);
				break;
			case LEFT:
				srcRect = new Rectangle(0, 0, Revealed, SourcePixelBuffer.Height);
				break;
		}

		return srcRect;
	}

    public override void Draw(NewTOAPIA.UI.DrawEvent devent)
    {
        devent.GraphPort.DrawImage(SourcePixelBuffer, srcDstPoints, SourceRectangle(), GraphicsUnit.Pixel);
    }
}

public class StretchRight : Stretch
{
	public StretchRight()
        : this(null, Rectangle.Empty, 1)
	{
	}

	public StretchRight(double duration)
        : this(null, Rectangle.Empty, duration)
	{
	}

    public StretchRight(GDIDIBSection srcPixelBuffer, Rectangle frame, double duration)
		: base("StretchRight", srcPixelBuffer, frame, duration, Stretch.RIGHT)
	{
	}
}
public class StretchLeft : Stretch
{
	public StretchLeft()
		: base("StretchLeft", null, Rectangle.Empty, 10, Stretch.LEFT)
	{
	}

	public StretchLeft(double duration)
		: base("StretchLeft", null, Rectangle.Empty, duration, Stretch.LEFT)
	{
	}

    public StretchLeft(GDIDIBSection srcPixelBuffer, Rectangle frame, double duration)
		: base("StretchLeft", srcPixelBuffer, frame, duration, Stretch.LEFT)
	{
	}
}

public class StretchUp : Stretch
{
	public StretchUp()
		: base("StretchUp", null, Rectangle.Empty, 1, Stretch.UP)
	{
	}

	public StretchUp(double duration)
		: base("StretchUp", null, Rectangle.Empty, duration, Stretch.UP)
	{
	}

    public StretchUp(GDIDIBSection srcPixelBuffer, Rectangle frame, double duration)
		: base("StretchUp", srcPixelBuffer, frame, duration, Stretch.UP)
	{
	}
}
public class StretchDown : Stretch
{
	public StretchDown()
		: base("StretchDown", null, Rectangle.Empty, 1, Stretch.DOWN)
	{
	}

	public StretchDown(double duration)
        : base("StretchDown", null, Rectangle.Empty, duration, Stretch.DOWN)
	{
	}

    public StretchDown(GDIDIBSection srcPixelBuffer, Rectangle frame, double duration)
        : base("StretchDown", srcPixelBuffer, frame, duration, Stretch.DOWN)
	{
	}
}
