using System;
using System.Drawing;

using NewTOAPIA;
using NewTOAPIA.Drawing;
using NewTOAPIA.Drawing.GDI;
using NewTOAPIA.UI;

/// <summary>
/// The HorizontalCenterExpand expands out from the center along
/// a vertical line.
/// </summary>
public class ExpandVerticalOut : ImageTransition
{
    PixmapShard fShard;
    Rectangle fInitialFrame;
    Rectangle fFinalFrame;
    Rectangle fCurrentFrame;

    public ExpandVerticalOut(GDIDIBSection srcPixelBuffer, Rectangle frame, double duration)
        : base("ExpandVerticalOut", frame, null, srcPixelBuffer, duration)
	{
        fFinalFrame = frame;
        fInitialFrame = new Rectangle(Frame.Left + Frame.Width / 2, Frame.Top, 2, Frame.Height);
    }

    public override void OnReset()
    {
        fFinalFrame = Frame;
        fInitialFrame = new Rectangle(Frame.Left + Frame.Width / 2, Frame.Top, 2, Frame.Height);
        fCurrentFrame = fInitialFrame;

        if (null != SourcePixelBuffer)
        {
            Rectangle boundary = new Rectangle(0, 0, SourcePixelBuffer.Width, SourcePixelBuffer.Height);
            fShard = new PixmapShard(SourcePixelBuffer, boundary, fInitialFrame);
        }
        else
            fShard = null;
    }

    public override void UpdateGeometryState(double atTime)
    {
        int newX = Interpolator.LinearInterpolation(fInitialFrame.X, fFinalFrame.X, atTime);
        int newY = Interpolator.LinearInterpolation(fInitialFrame.Y, fFinalFrame.Y, atTime);
        int newWidth = Interpolator.LinearInterpolation(fInitialFrame.Width, fFinalFrame.Width, atTime);
        int newHeight = Interpolator.LinearInterpolation(fInitialFrame.Height, fFinalFrame.Height, atTime);

        Rectangle currentFrame = new Rectangle(newX, newY, newWidth, newHeight);
        fShard.Frame = currentFrame;
    }

    public override void Draw(DrawEvent devent)
    {
        if (null != fShard)
        {
            fShard.Draw(devent);
        }
    }
}

