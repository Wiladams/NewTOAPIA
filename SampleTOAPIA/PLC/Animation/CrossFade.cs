using System;
using System.Drawing;
using System.Runtime.InteropServices;

using TOAPI.Types;

using NewTOAPIA.Drawing;
using NewTOAPIA.Drawing.GDI;
using NewTOAPIA.UI;

/// <summary>
/// CrossFade is an animation that does a progressive cross fade
/// from the dstPixelBuffer to the srcPixelBuffer.  In the end, the Graphics
/// port will be displaying the srcPixelBuffer in full.
/// </summary>
public class CrossFade : ImageTransition
{
    GDIDIBSection workPixmap;
    byte fContribution;

	public CrossFade()
		:this(null,null,Rectangle.Empty,1)
	{
	}

    public CrossFade(GDIDIBSection dstPixelBuffer, GDIDIBSection srcPixelBuffer, Rectangle frame, double duration)
		:base("CrossFade", frame, dstPixelBuffer, srcPixelBuffer, duration)
	{
	}

    public override void OnReset()
    {
        fContribution = 0;
    }

    public override void OnSetDestinationPixelBuffer()
    {
        if (null != DestinationPixelBuffer)
        {
            workPixmap = (GDIDIBSection)this.DestinationPixelBuffer.Clone();
            workPixmap.DeviceContext.ClearToWhite();
        }
        else
        {
            workPixmap = null;
        }
    }
    
    public override void UpdateGeometryState(double atTime)
    {
        byte opacity = (byte)Math.Floor((atTime * 255) + 0.5);
        //fContribution = (byte)Math.Floor(((1.0 - CompletionPercentage) * opacity) + 0.5);
        fContribution = opacity;
        //Console.WriteLine("Opacity: {0}", fContribution);
    }

    public override void Draw(DrawEvent devent)
    {
        // For convenience, create rectangles to represent the frames of each 
        // of the pixmaps
        Rectangle srcRect = new Rectangle(0, 0, SourcePixelBuffer.Width, SourcePixelBuffer.Height);
        Rectangle dstRect = new Rectangle(0, 0, DestinationPixelBuffer.Width, DestinationPixelBuffer.Height);

        //devent.GraphPort.AlphaBlend(0, 0, DestinationPixelBuffer.Width, DestinationPixelBuffer.Height,
        //    SourcePixelBuffer, 0, 0, SourcePixelBuffer.Width, SourcePixelBuffer.Height, fContribution);

        // First blast the destination bitmap to the working buffer
        workPixmap.DeviceContext.BitBlt(DestinationPixelBuffer.DeviceContext, new Point(0, 0), dstRect, TernaryRasterOps.SRCCOPY);

        // Alpha blend the source into the destination
        if (!workPixmap.DeviceContext.AlphaBlend(SourcePixelBuffer.DeviceContext, srcRect, dstRect, fContribution))
            return ;

        // Blit the destination onto the final graphport
        GraphPort.PixBlt(workPixmap, 0, 0);
	}
}
