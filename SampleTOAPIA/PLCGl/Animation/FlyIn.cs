using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Threading;

using NewTOAPIA.Drawing;
using NewTOAPIA.UI;

/// <summary>
/// </summary>
public class FlyIn : ImageTransition
{
	Rectangle fStartFrame;
	Rectangle fEndFrame;
	Rectangle fLastFrame;
	Rectangle fCurrentFrame;

	int fXDistance;
	int fYDistance;

    GDIPixmap bitmapBuffer;
	IGraphPort bitmapGraphics;

	public FlyIn(Rectangle startFrame, Rectangle endFrame, double duration)
		: base("FlyIn", 0)
	{
		fStartFrame = startFrame;
		fCurrentFrame = fStartFrame;
		fEndFrame = endFrame;
		fLastFrame = Rectangle.Empty;

		fXDistance = (fEndFrame.Left - fStartFrame.Left);
		fYDistance = (fEndFrame.Top - fStartFrame.Top);
	}

	public override void OnSetDestinationPixelBuffer()
	{
        if (null == DestinationPixelBuffer)
            return;

        bitmapBuffer = (GDIDIBSection)this.DestinationPixelBuffer.Clone();
        bitmapGraphics = bitmapBuffer.GraphPort;
        bitmapBuffer.DeviceContext.ClearToWhite();
	}

    protected override void OnFirstCell()
    {
        // If this is the first step, set the currentFrame
        // to the start frame, and leave it at that.
        fCurrentFrame = fStartFrame;
    }

	public override void Draw(DrawEvent devent)
	{
		Point[] dstPoints=null;
		
		dstPoints = new Point[] {
			new Point(fLastFrame.Left, fLastFrame.Top),
			new Point(fLastFrame.Right,fLastFrame.Top),
			new Point(fLastFrame.Left, fLastFrame.Bottom)};

		// Draw the little piece of the destination bitmap to refresh where the graphis has been
		bitmapGraphics.DrawImage(DestinationPixelBuffer, dstPoints, fLastFrame, GraphicsUnit.Pixel);

		// Now draw the graphic in it's new position
        int left = fStartFrame.Left + (int)Math.Floor((fXDistance * CompletionPercentage)+0.5);
        int top = fStartFrame.Top + (int)Math.Floor((fYDistance * CompletionPercentage) + 0.5);

		fCurrentFrame = new Rectangle(left, top, fStartFrame.Width,fStartFrame.Height);
		bitmapGraphics.ScaleBitmap(SourcePixelBuffer, fCurrentFrame);


		Point [] srcPoints = new Point[] {
				new Point(fCurrentFrame.Left, fCurrentFrame.Top),
				new Point(fCurrentFrame.Right,fCurrentFrame.Top),
				new Point(fCurrentFrame.Left, fCurrentFrame.Bottom)};
		
		if (dstPoints!=null)
			GraphPort.DrawImage(bitmapBuffer, dstPoints, fLastFrame, GraphicsUnit.Pixel);
		
        GraphPort.DrawImage(bitmapBuffer, srcPoints, fCurrentFrame, GraphicsUnit.Pixel);

		fLastFrame = fCurrentFrame;
	}

}

