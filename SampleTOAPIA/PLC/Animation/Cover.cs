using System;
using System.Drawing;

using NewTOAPIA.Drawing;
using NewTOAPIA.Drawing.GDI;
using NewTOAPIA.UI;

public class Cover : ImageTransition
{
    #region Fields
    int fDirection;
    int fDistance;
    int fRevealed;
    Rectangle fSourceFrame;
    Rectangle fSourceBoundary;
    #endregion

    #region Constructors
    public Cover(string name, GDIDIBSection dstPixelBuffer, GDIDIBSection srcPixelBuffer, Rectangle frame, double duration, int direction)
		: base(name, frame, dstPixelBuffer, srcPixelBuffer, duration)
	{
		fDirection = direction;


    }
    #endregion

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

        switch (fDirection)
        {
            case DOWN:
                fSourceFrame = Rectangle.FromLTRB(Frame.Left, Frame.Top, Frame.Right, Revealed);
				fSourceBoundary = new Rectangle(0, SourcePixelBuffer.Height - Revealed, SourcePixelBuffer.Width, Revealed);
                break;
            case UP:
                fSourceFrame = Rectangle.FromLTRB(Frame.Left, Frame.Bottom - Revealed, Frame.Right, Frame.Bottom - Revealed);
                fSourceBoundary = new Rectangle(0, 0, SourcePixelBuffer.Width, Revealed);
                break;
            case LEFT:
                fSourceFrame = Rectangle.FromLTRB(Frame.Right - Revealed, Frame.Top, Frame.Right, Frame.Bottom);
                fSourceBoundary = new Rectangle(0, 0, Revealed, SourcePixelBuffer.Height);
               break;
            case RIGHT:
                fSourceFrame = Rectangle.FromLTRB(Frame.Left, Frame.Top, Frame.Left + Revealed, Frame.Bottom);
                fSourceBoundary = new Rectangle(SourcePixelBuffer.Width - Revealed, 0, Revealed, SourcePixelBuffer.Height);
                break;
        }


    }

    #region Properties
    public virtual int Revealed
	{
		get
		{
            return fRevealed ;
		}
	}

    Rectangle SourceFrame
	{
        get {return fSourceFrame;}
	}

    Rectangle SourceBoundary
    {
        get { return fSourceBoundary; }
    }
    #endregion

    #region IDrawable
    public override void Draw(DrawEvent devent)
	{
		// Draw a portion of the source bitmap
        //devent.GraphPort.PixmapShardBlt(SourcePixelBuffer, SourceBoundary, SourceFrame);
        //Console.WriteLine("Boundary: {0}   Frame: {1}", SourceBoundary.ToString(), SourceFrame.ToString());
        devent.GraphPort.AlphaBlend(SourceFrame.X, SourceFrame.Y, SourceFrame.Width, SourceFrame.Height,
            SourcePixelBuffer,
            SourceBoundary.X, SourceBoundary.Y, SourceBoundary.Width, SourceBoundary.Height,
            255);
    }
    #endregion

    #region Informational
    //Point[] SourceFrameParallelogram()
    //{
    //    Point[] srcPoints = null;

    //    switch (fDirection)
    //    {
    //        case DOWN:
    //            srcPoints = new Point[] {
    //                new Point(Frame.Left, Frame.Top),
    //                new Point(Frame.Right,Frame.Top),
    //                new Point(Frame.Left, Revealed)};
    //            break;
    //        case UP:
    //            srcPoints = new Point[] {
    //                new Point(Frame.Left, Frame.Bottom-Revealed),
    //                new Point(Frame.Right,Frame.Bottom-Revealed),
    //                new Point(Frame.Left, Frame.Bottom)};
    //            break;
    //        case LEFT:
    //            srcPoints = new Point[] {
    //                new Point(Frame.Right-Revealed, Frame.Top),
    //                new Point(Frame.Right,Frame.Top),
    //                new Point(Frame.Right-Revealed, Frame.Bottom)};
    //            break;
    //        case RIGHT:
    //            srcPoints = new Point[] {
    //                new Point(Frame.Left, Frame.Top),
    //                new Point(Frame.Left+Revealed,Frame.Top),
    //                new Point(Frame.Left, Frame.Bottom)};
    //            break;
    //    }

    //    return srcPoints;
    //}
    #endregion

}

public class CoverRight : Cover
{
    public CoverRight(GDIDIBSection dstPixelBuffer, GDIDIBSection srcPixelBuffer, Rectangle frame, double duration)
        : base("CoverRight", dstPixelBuffer, srcPixelBuffer, frame, duration, TwoPartTransition.RIGHT)
	{
	}
}

public class CoverLeft : Cover
{
    public CoverLeft(GDIDIBSection dstPixelBuffer, GDIDIBSection srcPixelBuffer, Rectangle frame, double duration)
        : base("CoverLeft", dstPixelBuffer, srcPixelBuffer, frame, duration, TwoPartTransition.LEFT)
	{
	}
}

public class CoverUp : Cover
{
    public CoverUp(GDIDIBSection dstPixelBuffer, GDIDIBSection srcPixelBuffer, Rectangle frame, double duration)
        : base("CoverUp", dstPixelBuffer, srcPixelBuffer, frame, duration, TwoPartTransition.UP)
	{
	}
}

public class CoverDown : Cover
{
    public CoverDown(GDIDIBSection dstPixelBuffer, GDIDIBSection srcPixelBuffer, Rectangle frame, double duration)
		: base("CoverDown", dstPixelBuffer, srcPixelBuffer, frame, duration, TwoPartTransition.DOWN)
	{
	}
}
