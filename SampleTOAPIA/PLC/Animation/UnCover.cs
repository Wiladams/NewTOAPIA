using System;
using System.Drawing;

using TOAPI.Types;

using NewTOAPIA;
using NewTOAPIA.Drawing;
using NewTOAPIA.Drawing.GDI;
using NewTOAPIA.Graphics;

/// <summary>
/// </summary>
public class UnCover : TwoPartTransition
{
    public UnCover(string name, GDIDIBSection dstPixelBuffer, GDIDIBSection srcPixelBuffer, Rectangle frame, double duration, int direction)
		: base(name, dstPixelBuffer, srcPixelBuffer, frame, duration, direction)
	{
	}

	public override Point[] DestinationOfDstRect()
	{
		Point[] dstPoints = null;

		switch (Direction)
		{
			case DOWN:
				dstPoints = new Point[] {
					new Point(Frame.Left, Frame.Top+Revealed),
					new Point(Frame.Right,Frame.Top+Revealed),
					new Point(Frame.Left, Frame.Bottom)};
				break;

			case UP:
				dstPoints = new Point[] {
					new Point(Frame.Left, Frame.Top),
					new Point(Frame.Right,Frame.Top),
					new Point(Frame.Left, Frame.Bottom-Revealed)};
				break;
			case RIGHT:
				dstPoints = new Point[] {
					new Point(Frame.Left+Revealed, Frame.Top),
					new Point(Frame.Right,Frame.Top),
					new Point(Frame.Left+Revealed, Frame.Bottom)};
				break;
			case LEFT:
				dstPoints = new Point[] {
					new Point(Frame.Left, Frame.Top),
					new Point(Frame.Right-Revealed,Frame.Top),
					new Point(Frame.Left, Frame.Bottom)};
				break;

		}
		return dstPoints;
	}

	public override Rectangle DestinationRectangle()
	{
		Rectangle srcRect = Rectangle.Empty;

		switch (Direction)
		{
			case DOWN:
				srcRect = new Rectangle(0, 0, DestinationPixelBuffer.Width, DestinationPixelBuffer.Height - Revealed);
				break;
			case UP:
				srcRect = new Rectangle(0, Revealed, DestinationPixelBuffer.Width, DestinationPixelBuffer.Height - Revealed);
				break;

			case RIGHT:
				srcRect = new Rectangle(0, 0, DestinationPixelBuffer.Width - Revealed, DestinationPixelBuffer.Height);
				break;
			case LEFT:
				srcRect = new Rectangle(Revealed, 0, DestinationPixelBuffer.Width - Revealed, DestinationPixelBuffer.Height);
				break;
		}
		return srcRect;
	}

	public override Point[] DestinationOfSrcRect()
	{
		Point[] srcPoints = null;

		switch (Direction)
		{
			case DOWN:
				srcPoints = new Point[] {
					new Point(Frame.Left, Frame.Top),
					new Point(Frame.Right,Frame.Top),
					new Point(Frame.Left, Revealed)};
				break;
			case UP:
				srcPoints = new Point[] {
					new Point(Frame.Left, Frame.Bottom-Revealed),
					new Point(Frame.Right,Frame.Bottom-Revealed),
					new Point(Frame.Left, Frame.Bottom)};
				break;
			case LEFT:
				srcPoints = new Point[] {
					new Point(Frame.Right-Revealed, Frame.Top),
					new Point(Frame.Right,Frame.Top),
					new Point(Frame.Right-Revealed, Frame.Bottom)};
				break;
			case RIGHT:
				srcPoints = new Point[] {
					new Point(Frame.Left, Frame.Top),
					new Point(Frame.Left+Revealed,Frame.Top),
					new Point(Frame.Left, Frame.Bottom)};
				break;
		}

		return srcPoints;
	}

	public override Rectangle SourceRectangle()
	{
		Rectangle srcRect = Rectangle.Empty;

		switch (Direction)
		{
			case DOWN:
				srcRect = new Rectangle(0, 0, SourcePixelBuffer.Width, Revealed);
				break;
			case UP:
				srcRect = new Rectangle(0, SourcePixelBuffer.Height - Revealed, SourcePixelBuffer.Width, Revealed);
				break;

			case RIGHT:
				srcRect = new Rectangle(0, 0, Revealed, SourcePixelBuffer.Height);
				break;
			case LEFT:
				srcRect = new Rectangle(SourcePixelBuffer.Width - Revealed, 0, Revealed, SourcePixelBuffer.Height);
				break;
		}

		return srcRect;
	}
}

public class UnCoverRight : UnCover
{
    public UnCoverRight(GDIDIBSection dstPixelBuffer, GDIDIBSection srcPixelBuffer, Rectangle frame, int steps)
		: base("UnCoverRight", dstPixelBuffer, srcPixelBuffer, frame, steps, TwoPartTransition.RIGHT)
	{
	}
}
public class UnCoverLeft : UnCover
{
    public UnCoverLeft(GDIDIBSection dstPixelBuffer, GDIDIBSection srcPixelBuffer, Rectangle frame, int steps)
		: base("UnCoverLeft", dstPixelBuffer, srcPixelBuffer, frame, steps, TwoPartTransition.LEFT)
	{
	}
}
public class UnCoverUp : UnCover
{
    public UnCoverUp(GDIDIBSection dstPixelBuffer, GDIDIBSection srcPixelBuffer, Rectangle frame, double duration)
		: base("UnCoverUp", dstPixelBuffer, srcPixelBuffer, frame, duration, TwoPartTransition.UP)
	{
	}
}

public class UnCoverDown : UnCover
{
    public UnCoverDown(GDIDIBSection dstPixelBuffer, GDIDIBSection srcPixelBuffer, Rectangle frame, double duration)
		: base("UnCoverDown", dstPixelBuffer, srcPixelBuffer, frame, duration, TwoPartTransition.DOWN)
	{
	}
}
