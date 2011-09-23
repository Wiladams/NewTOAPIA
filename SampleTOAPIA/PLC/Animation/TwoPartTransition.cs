using System;
using System.Drawing;

using TOAPI.Types;

using NewTOAPIA.Drawing;
using NewTOAPIA.Drawing.GDI;
using NewTOAPIA.UI;

/// <summary>
/// </summary>
public class TwoPartTransition : ImageTransition
{
    //Rectangle fSourceFrame;
    //Rectangle fSourceBoundary;

    int fDistance;
    int fDirection;
    int fRevealed;

    public TwoPartTransition(string name, GDIDIBSection dstPixelBuffer, GDIDIBSection srcPixelBuffer, Rectangle frame, double duration, int direction)
		: base(name, frame, dstPixelBuffer, srcPixelBuffer, duration)
	{
		fDirection = direction;
        fRevealed = 0;

		switch (direction)
		{
			case RIGHT:
			case LEFT:
				fDistance = frame.Width;
				break;

			case UP:
			case DOWN:
				fDistance = frame.Height;
				break;
		}


    }

    #region Properties
    public int Direction
	{
		get {return fDirection;}
		set {fDirection = value;}
    }

    public virtual int Revealed
    {
        get { return fRevealed; }
    }
    #endregion

    public override void UpdateGeometryState(double atTime)
    {
        fRevealed = (int)Math.Floor((fDistance * atTime) + 0.5);
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
				srcRect = new Rectangle(0, Revealed, DestinationPixelBuffer.Width, DestinationPixelBuffer.Height - Revealed);
				break;
			case UP:
				srcRect = new Rectangle(0, 0, DestinationPixelBuffer.Width, DestinationPixelBuffer.Height - Revealed);
				break;

			case RIGHT:
				srcRect = new Rectangle(Revealed, 0, DestinationPixelBuffer.Width - Revealed, DestinationPixelBuffer.Height);
				break;
			case LEFT:
				srcRect = new Rectangle(0, 0, DestinationPixelBuffer.Width - Revealed, DestinationPixelBuffer.Height);
				break;
		}
		return srcRect;
	}

	public virtual Point[] DestinationOfSrcRect()
	{
		Point[] srcPoints = null;

		switch (fDirection)
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
		devent.GraphPort.DrawImage(DestinationPixelBuffer, DestinationOfDstRect(), DestinationRectangle(), GraphicsUnit.Pixel);

		// Draw a portion of the source bitmap
		devent.GraphPort.DrawImage(SourcePixelBuffer, DestinationOfSrcRect(), SourceRectangle(), GraphicsUnit.Pixel);
	}

}

