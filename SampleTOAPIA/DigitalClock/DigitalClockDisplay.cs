using System;
using System.Runtime.InteropServices;   // GetLocalTime()

using TOAPI.Types;

using NewTOAPIA;
using NewTOAPIA.Drawing;
using NewTOAPIA.Drawing.GDI;
using NewTOAPIA.Graphics;
using NewTOAPIA.UI;

/// <summary>
/// Summary description for Class1
/// </summary>
public class DigitalClockDisplay : Graphic
{
	bool f24Hour;
	bool fSuppress;
	SegmentedNumber fSegmentedNumber;
    DateTime fLastTime;
    DateTime fThisTime;
    Point2I[][] ptColon;
    Point2I[] fcolonPoly1;
    Point2I[] fcolonPoly2;
    Affine fTransform;

    GDIPen fPen;
    GDIBrush fBrush;

    /// <summary>
    /// This is the most basic constructor
    /// </summary>
    /// <param name="is24Hour">this is the 24 hour flag</param>
    /// <param name="suppressExtra"></param>
	public DigitalClockDisplay(bool is24Hour, bool suppressExtra)
	{
        fTransform = new Affine();

        f24Hour = is24Hour;
		fSuppress = suppressExtra;
		fSegmentedNumber = new SegmentedNumber();

        fcolonPoly1 = new Point2I[] { new Point2I(2, 21), new Point2I(6, 17), new Point2I(10, 21), new Point2I(6, 25) };
        fcolonPoly2 = new Point2I[] { new Point2I(2, 51), new Point2I(6, 47), new Point2I(10, 51), new Point2I(6, 55) };


        fPen = new GDIPen(Colorrefs.Black);
        fBrush = new GDIBrush(BrushStyle.Solid, HatchStyle.Vertical, Colorrefs.Red, Guid.NewGuid());
    }
	
	void DisplayTwoDigits(IGraphPort graphPort, int iNumber, bool suppress)
	{
		if (!suppress || (iNumber / 10 != 0))
			SegmentedNumber.DisplayDigit(graphPort, iNumber / 10);

        graphPort.TranslateTransform(42,0);
        SegmentedNumber.DisplayDigit(graphPort, iNumber % 10);

        graphPort.TranslateTransform(42, 0);
    }

	void DisplayColon(IGraphPort graphPort)
	{
        graphPort.Polygon(fcolonPoly1);
        graphPort.Polygon(fcolonPoly2);
        graphPort.TranslateTransform(12, 0);
    }

    void DrawHour(IGraphPort graphPort)
    {
        int hour = fThisTime.Hour % 12;

            if (f24Hour)
                DisplayTwoDigits(graphPort, fThisTime.Hour, fSuppress);
            else
                DisplayTwoDigits(graphPort, ((hour) > 0) ? (int)hour : 12, fSuppress);
        
        DisplayColon(graphPort);

    }

    void DrawMinutes(IGraphPort graphPort)
    {
        DisplayTwoDigits(graphPort, fThisTime.Minute, false);
 
        DisplayColon(graphPort);
    }

    void DrawSeconds(IGraphPort graphPort)
    {
        DisplayTwoDigits(graphPort, fThisTime.Second, false);
    }


    public override void DrawSelf(DrawEvent devent)
	{
        IGraphPort graphPort = devent.GraphPort;

        fThisTime = DateTime.Now;


        // Set the pen and brush that will be used
        graphPort.SetPen(fPen);
        graphPort.SetBrush(fBrush);

        // Start out with an identity transform
        fTransform.Identity();
        DrawHour(graphPort);

        DrawMinutes(graphPort);

        DrawSeconds(graphPort);

        // Copy the current time to being the last time
        fLastTime = fThisTime;
    }

    public override void Draw(DrawEvent devent)
    {
        base.Draw(devent);
        // Set the view and window for proper scaling
        //gPort.SetMappingMode(MappingModes.Isotropic);
        //Transform2D transform = new Transform2D();
        //float sx = Frame.Width / 276;
        //float sy = Frame.Height / 72;
        //transform.Scale(sx, sy);
        //transform.Translate(138, 36);
        //gPort.SetWorldTransform(transform);

        //gPort.SetWindowExtent(276, 72);
        //gPort.SetViewportExtent(Frame.Width, Frame.Height);
        //gPort.SetWindowOrigin(138, 36);
        //gPort.SetViewportOrigin(Frame.Width / 2, Frame.Height / 2);

    }

    public override void DrawBackground(DrawEvent devent)
    {
        //devent.GraphPort.FillRectangle(fBrush, new Rectangle(Origin.X, Origin.Y, Width, Height));
    }
}
