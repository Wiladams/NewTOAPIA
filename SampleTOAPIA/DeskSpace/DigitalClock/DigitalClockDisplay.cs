using System;

using TOAPI.Types;
using TOAPI.GDI32;
using TOAPI.Kernel32;

using NewTOAPIA.Drawing;
using NewTOAPIA.UI;

public class DigitalClockDisplay : IDrawable
{
	bool f24Hour;
	bool fSuppress;
	SegmentedNumber fSegmentedNumber;
    SYSTEMTIME fLastTime;
    SYSTEMTIME fThisTime;
    Point[][] ptColon;
    PolygonG[] fcolonPolies;
    Guid fPen;
    Guid fBrush;


	public DigitalClockDisplay(bool is24Hour, bool suppressExtra)
	{
        f24Hour = is24Hour;
		fSuppress = suppressExtra;
		fSegmentedNumber = new SegmentedNumber();

        ptColon = new Point[2][];
        ptColon[0] = new Point[] { new Point(2, 21), new Point(6, 17), new Point(10, 21), new Point(6, 25) };
        ptColon[1] = new Point[] { new Point(2, 51), new Point(6, 47), new Point(10, 51), new Point(6, 55) };

        fcolonPolies = new PolygonG[2];
        fcolonPolies[0] = new PolygonG(ptColon[0]);
        fcolonPolies[1] = new PolygonG(ptColon[1]);

        fLastTime = new SYSTEMTIME();
        fThisTime = new SYSTEMTIME();
    }
	
	void DisplayTwoDigits(IGraphPort graphPort, int iNumber, bool suppress)
	{
			if (!suppress || (iNumber / 10 != 0))
				SegmentedNumber.DisplayDigit(graphPort, iNumber / 10);

            graphPort.OffsetWindowOrigin(-42, 0);
            SegmentedNumber.DisplayDigit(graphPort, iNumber % 10);
            graphPort.OffsetWindowOrigin(-42, 0);	
	}

    void DisplayColon(IGraphPort graphPort)
    {
        graphPort.Polygon(fcolonPolies[0].Points);
        graphPort.Polygon(fcolonPolies[1].Points);

        graphPort.OffsetWindowOrigin(-12, 0);
    }

    void DrawHour(IGraphPort graphPort)
    {
        if (f24Hour)
            DisplayTwoDigits(graphPort, fThisTime.Hour, fSuppress);
        else
            DisplayTwoDigits(graphPort, ((fThisTime.Hour %= 12) > 0) ? (int)fThisTime.Hour : 12, fSuppress);

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

    public void Draw(DrawEvent devent)
    {
        OnDraw(devent);
    }

    public void OnDraw(DrawEvent devent)
	{
        IGraphPort graphPort = devent.GraphPort;


		Kernel32.GetLocalTime(out fThisTime);

        fPen = Guid.NewGuid();
        fBrush = Guid.NewGuid();
        graphPort.CreateCosmeticPen(PenStyle.Dash, RGBColor.Black, fPen);
        graphPort.CreateBrush(GDI32.BS_SOLID, 0, RGBColor.Red, fBrush);
        
        //graphPort.DrawingColor = RGBColor.Red;
        graphPort.UseDefaultBrush();
        graphPort.UseDefaultPen();
        graphPort.SetDefaultPenColor(RGBColor.Black);
        graphPort.SetDefaultBrushColor(RGBColor.Red);

        DrawHour(graphPort);

        DrawMinutes(graphPort);

        DrawSeconds(graphPort);

        // Copy the current time to being the last time
        fLastTime.Hour = fThisTime.Hour;
        fLastTime.Minute = fThisTime.Minute;
        fLastTime.Second = fThisTime.Second;

        // Restore the world transform so drawing
        // is normal for whomever is next
        //graphPort.SetWorldTransform(oldTransform);
    }

 
}
