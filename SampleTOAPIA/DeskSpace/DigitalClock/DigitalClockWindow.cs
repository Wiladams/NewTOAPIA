using System;
using System.Runtime.InteropServices;

using TOAPI.Types;
using NewTOAPIA.Drawing;
using NewTOAPIA.UI;

using HamSketch;
using HumLog;

public class ClockWindow  //: UISurface
{
	DigitalClockDisplay fClockDisplay;
    UISurface fSurface;
    Guid fSurfaceID;
    RECT fFrame;
    RECT fBounds;

	public ClockWindow(int x, int y, int width, int height)
		//:base("Digital Clock", x,y,width, height,Guid.NewGuid())
	{
        fFrame = new RECT(x, y, width, height);
        fBounds = new RECT(0, 0, width, height);
        fClockDisplay = new DigitalClockDisplay(false, true);

        fSurfaceID = MetaSpace.CreateSurface("Digital Clock", fFrame, 
            new OnSurfaceCreatedEventHandler(OnSurfaceCreated));
	}

    //public override void OnTimer()
    //{
    //    Invalidate();
    //}

    void OnSurfaceCreated(Guid uniqueID)
    {
        fSurface = MetaSpace.GetSurface(uniqueID);

        DrawOnSurface(fSurface);

        //BackgroundColor = RGBColor.LtGray;

        //StartTimer(1000);
    }

    public virtual void DrawOnSurface(UISurface aSurface)
    {
        OnDraw(new DrawEvent(aSurface.GraphPort, fBounds));

        fSurface.Invalidate();
    }

    public virtual void DrawBackground(IGraphPort graphPort)
    {
        // Clear the backing buffer
        //GraphPort.DrawingColor = RGBColor.LtGray;
        graphPort.UseDefaultBrush();
        graphPort.UseDefaultPen();
        graphPort.SetDefaultBrushColor(RGBColor.LtGray);
        graphPort.SetDefaultPenColor(RGBColor.LtGray);

        graphPort.Rectangle(0, 0, fBounds.Width, fBounds.Height);
    }


    public virtual void OnDraw(DrawEvent devent)
    {
        IGraphPort gPort = devent.GraphPort;

        DrawBackground(gPort);

        gPort.SaveState();

        // Set the view and window for proper scaling
        gPort.SetMappingMode(MappingModes.ISOTROPIC);
        gPort.SetWindowExtent(276, 72);
        gPort.SetViewportExtent(fFrame.Width, fFrame.Height);
        gPort.SetWindowOrigin(138, 36);
        gPort.SetViewportOrigin(fFrame.Width / 2, fFrame.Height / 2);

        fClockDisplay.Draw(new DrawEvent(gPort, fBounds));

        gPort.ResetState();

        //base.OnDraw(devent);
    }
}
