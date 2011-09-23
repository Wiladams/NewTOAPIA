using System;
using System.Drawing;
using System.Drawing.Drawing2D;

using NewTOAPIA.Drawing;
using NewTOAPIA.UI;

/// <summary>
/// </summary>
public class Wipe : ImageTransition
{
    Rectangle fSourceFrame;
    Rectangle fSourceBoundary;
    int fDistance;
    int fDirection;
    int fRevealed;

    #region Constructors
    public Wipe(string name, GDIDIBSection dstBitmap, GDIDIBSection srcBitmap, Rectangle frame, double duration, int direction)
		: base(name, frame, dstBitmap, srcBitmap, duration)
	{
        fDirection = direction;


    }
    #endregion

    public override void OnFrameChanged()
    {
        switch (Direction)
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

    #region Properties
    
    int Direction
    {
        get { return fDirection; }
    }

    public virtual int Revealed
    {
        get
        {
            return fRevealed;
        }
    }

    Rectangle SourceFrame
    {
        get { return fSourceFrame; }
    }

    Rectangle SourceBoundary
    {
        get { return fSourceBoundary; }
    }
    #endregion

    public override void UpdateGeometryState(double atTime)
    {
        fRevealed = (int)Math.Floor((double)fDistance * atTime);

        switch (fDirection)
        {
            case DOWN:
                fSourceBoundary = new Rectangle(0, 0, SourcePixelBuffer.Width, Revealed);
                fSourceFrame = Rectangle.FromLTRB(Frame.Left, Frame.Top, Frame.Right, Revealed);
                break;
            case UP:
                fSourceFrame = Rectangle.FromLTRB(Frame.Left, Frame.Bottom - Revealed, Frame.Right, Frame.Bottom - Revealed);
                fSourceBoundary = new Rectangle(0, SourcePixelBuffer.Height - Revealed, SourcePixelBuffer.Width, Revealed);
                break;
            case LEFT:
                fSourceFrame = Rectangle.FromLTRB(Frame.Right - Revealed, Frame.Top, Frame.Right, Frame.Bottom);
                fSourceBoundary = new Rectangle(SourcePixelBuffer.Width - Revealed, 0, Revealed, SourcePixelBuffer.Height);
                break;
            case RIGHT:
                fSourceFrame = Rectangle.FromLTRB(Frame.Left, Frame.Top, Frame.Left + Revealed, Frame.Bottom);
                fSourceBoundary = new Rectangle(0, 0, Revealed, SourcePixelBuffer.Height);
                break;
        }
    }

	public override void Draw(DrawEvent devent)
	{
        //devent.GraphPort.PixmapShardBlt(SourcePixelBuffer, SourceBoundary, SourceFrame);
        devent.GraphPort.AlphaBlend(SourceFrame.X, SourceFrame.Y, SourceFrame.Width, SourceFrame.Height,
            SourcePixelBuffer,
            SourceBoundary.X, SourceBoundary.Y, SourceBoundary.Width, SourceBoundary.Height,
            255);
    }
}

public class WipeRight : Wipe
{
    public WipeRight(GDIDIBSection dstBitmap, GDIDIBSection srcBitmap, Rectangle frame, double steps)
		: base("WipeRight", dstBitmap, srcBitmap, frame, steps, TwoPartTransition.RIGHT)
	{
	}
}
public class WipeLeft : Wipe
{
    public WipeLeft(GDIDIBSection dstBitmap, GDIDIBSection srcBitmap, Rectangle frame, double steps)
		: base("WipeLeft", dstBitmap, srcBitmap, frame, steps, TwoPartTransition.LEFT)
	{
	}
}
public class WipeUp : Wipe
{
    public WipeUp(GDIDIBSection dstBitmap, GDIDIBSection srcBitmap, Rectangle frame, double steps)
		: base("WipeUp", dstBitmap, srcBitmap, frame, steps, TwoPartTransition.UP)
	{
	}
}

public class WipeDown : Wipe
{
    public WipeDown(GDIDIBSection dstBitmap, GDIDIBSection srcBitmap, Rectangle frame, double steps)
		: base("WipeDown", dstBitmap, srcBitmap, frame, steps, TwoPartTransition.DOWN)
	{
	}
}