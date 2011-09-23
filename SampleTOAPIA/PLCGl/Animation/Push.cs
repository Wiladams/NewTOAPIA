using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

using NewTOAPIA.Drawing;
using NewTOAPIA.UI;

/// <summary>
/// </summary>
public class Push : ImageTransition
{
	int fDistance;
	int fDirection;
    int fRevealed;

    public Push(string name, GDIDIBSection dstPixelBuffer, GDIDIBSection srcPixelBuffer, Rectangle frame, double duration, int direction)
		: base(name, frame, dstPixelBuffer, srcPixelBuffer, duration)
	{
		fDirection = direction;
	}

    public override void OnReset()
    {
        fRevealed = 0;

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

    public override void UpdateGeometryState(double atTime)
    {
        fRevealed = (int)Math.Floor((fDistance * atTime) + 0.5);
    }

	public virtual int Revealed
	{
		get
		{
			return fRevealed ;
		}
	}

	public virtual Point[] DestinationOfDstRect()
	{
		Point[] dstPoints = null;

		switch (fDirection)
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

	public virtual Rectangle DestinationRectangle()
	{
		Rectangle srcRect = Rectangle.Empty;
		switch (fDirection)
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

	public virtual Point[] DestinationOfSrcRect()
	{
		Point[] srcPoints = null;

		switch(fDirection)
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

	public virtual Rectangle SourceRectangle()
	{
		Rectangle srcRect = Rectangle.Empty;

		switch (fDirection)
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

	public override void Draw(DrawEvent devent)
	{
		// Draw a portion of the destination bitmap
		GraphPort.DrawImage(DestinationPixelBuffer, DestinationOfDstRect(), DestinationRectangle(), GraphicsUnit.Pixel);

		// Draw a portion of the source bitmap
		GraphPort.DrawImage(SourcePixelBuffer, DestinationOfSrcRect(), SourceRectangle(), GraphicsUnit.Pixel);
	}

    //protected override void OnFinalCell()
    //{
    //    GraphPort.ScaleBitmap(SourcePixelBuffer, Frame);
    //}


}

public class PushRight : Push
{
    public PushRight(GDIDIBSection dstPixelBuffer, GDIDIBSection srcPixelBuffer, Rectangle frame, double duration)
        : base("PushRight", dstPixelBuffer, srcPixelBuffer, frame, duration, Push.RIGHT)
	{
	}
}
public class PushLeft : Push
{
    public PushLeft(GDIDIBSection dstPixelBuffer, GDIDIBSection srcPixelBuffer, Rectangle frame, double duration)
        : base("PushLeft", dstPixelBuffer, srcPixelBuffer, frame, duration, Push.LEFT)
	{
	}
}
public class PushUp : Push
{
    public PushUp(GDIDIBSection dstPixelBuffer, GDIDIBSection srcPixelBuffer, Rectangle frame, double duration)
        : base("PushUp", dstPixelBuffer, srcPixelBuffer, frame, duration, Push.UP)
	{
	}
}
public class PushDown : Push
{
    public PushDown(GDIDIBSection dstPixelBuffer, GDIDIBSection srcPixelBuffer, Rectangle frame, double duration)
        : base("PushDown", dstPixelBuffer, srcPixelBuffer, frame, duration, Push.DOWN)
	{
	}
}
