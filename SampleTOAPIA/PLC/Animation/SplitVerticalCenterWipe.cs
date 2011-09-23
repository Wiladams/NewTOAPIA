using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

using NewTOAPIA;
using NewTOAPIA.UI;
using NewTOAPIA.Drawing;
using NewTOAPIA.Drawing.GDI;
/// <summary>
/// </summary>
public class SplitVerticalOut : ImageTransition
{
	int fDistance;
    int fRevealed;

	int fMidX;
	Rectangle fSrcFrame;

    public SplitVerticalOut(GDIDIBSection srcPixelBuffer, Rectangle frame, double duration)
		: base("SplitVerticalOut", frame, null, srcPixelBuffer, duration)
	{
       
	}

	public override void OnFrameChanged()
	{
		Reset();
	}

	public override void OnReset()
	{
		fMidX = Frame.Left + Frame.Width / 2;
        fDistance = Frame.Width / 2;
        fRevealed = 0;
        fSrcFrame = new Rectangle(Frame.Width / 2, 0, 0, Frame.Height);
	}

    int Revealed
    {
        get
        {
            return fRevealed;
        }
    }

    public override void UpdateGeometryState(double atTime)
    {
        fRevealed = (int)Math.Floor((fDistance * atTime) + 0.5);
    }

	public override void Draw(DrawEvent devent)
	{
		// Calculate the srcPixelBuffer frame
		int srcMidX = SourcePixelBuffer.Width / 2;


		Rectangle srcRect = new Rectangle(srcMidX-Revealed/2,0,
			Revealed,SourcePixelBuffer.Height);
		Point[] srcDstPoints = new Point[] {
			new Point(fMidX-Revealed/2,Frame.Top),
			new Point(fMidX+Revealed/2,Frame.Top),
			new Point(fMidX-Revealed/2,Frame.Bottom)
		};

		GraphPort.DrawImage(SourcePixelBuffer, srcDstPoints,srcRect,GraphicsUnit.Pixel);
	}
}

