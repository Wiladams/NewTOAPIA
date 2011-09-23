using System;
using System.Runtime.InteropServices;

using TOAPI.Types;
using NewTOAPIA;
using NewTOAPIA.Drawing;
using NewTOAPIA.Drawing.GDI;
using NewTOAPIA.Graphics;
using NewTOAPIA.UI;



public class ClockWindow  : Window
{
	DigitalClockDisplay fClockDisplay;
    bool fIsTracking;
    GDIBrush fBackgroundBrush;
    GDIPen fBlackPen;

    // Scale management
    Point2I WOrg = new Point2I(0, 0);
    Point2I WExt = new Point2I(320, 110);
    Point2I VOrg = new Point2I(0,0);
    Point2I VExt = new Point2I(320, 110);

	public ClockWindow()
		:base("Digital Clock",10,10,320,110)
	{
        //BackgroundColor = RGBColor.LtGray;

        fBlackPen = new GDICosmeticPen(PenStyle.Solid, Colorrefs.Black, Guid.NewGuid());
        fBackgroundBrush = new GDIBrush(BrushStyle.Solid, HatchStyle.Vertical, Colorrefs.Black, Guid.NewGuid());

        fIsTracking = false;
        fClockDisplay = new DigitalClockDisplay(true, false);
        //SetWindowAlpha(128);

        StartTimer(1000);
	}

	public override void OnTimer()
	{
		Invalidate();
	}

    void DrawBackground(IGraphPort gPort)
    {
        gPort.FillRectangle(fBackgroundBrush, ClientRectangle);
    }

    public override void OnPaint(DrawEvent pea)
    {
        IGraphPort gPort = pea.GraphPort;

        // Draw the background before we do any scaling
        DrawBackground(gPort);

        
        gPort.SaveState();

        //// Set the graphport scaling appropriately
        DeviceContextClientArea.SetMappingMode(MappingModes.Anisotropic);
        DeviceContextClientArea.SetWindowOrigin(WOrg.x, WOrg.y);
        DeviceContextClientArea.SetWindowExtent(WExt.x, WExt.y);
        DeviceContextClientArea.SetViewportOrigin(VOrg.x, VOrg.y);
        DeviceContextClientArea.SetViewportExtent(VExt.x, VExt.y);

        fClockDisplay.Draw(pea);

        gPort.ResetState();
    }

    public override void OnResizing(int dw, int dh)
    {
        VExt.x = ClientRectangle.Width;
        VExt.y = ClientRectangle.Height;
    }

    public override void OnMouseHover(MouseActivityArgs e)
    {
        //Console.WriteLine("OnMouseHover");

        // Make the window fully opaque
        SetWindowAlpha(255);
    }

    public override void OnMouseLeave(MouseActivityArgs e)
    {
        //Console.WriteLine("OnMouseLeave");
        fIsTracking = false;
        SetWindowAlpha(128);
    }

    //public override void  OnMouseMove(MouseActivityArgs e)
    //{
    //    //Console.WriteLine("OnMouseMove");
    //    if (false == fIsTracking)
    //    {
    //        fIsTracking = true;
    //        TRACKMOUSEEVENT ev = new TRACKMOUSEEVENT();
    //        ev.cbSize = (uint)Marshal.SizeOf(typeof(TRACKMOUSEEVENT));

    //        // Now set what we want
    //        ev.dwFlags = User32.TME_HOVER | User32.TME_LEAVE;
    //        ev.dwHoverTime = User32.HOVER_DEFAULT;
    //        ev.hwndTrack = Handle;

    //        User32.TrackMouseEvent(ref ev);

    //        Console.WriteLine("Tracking Set");
    //    }
    //}

    public override void OnSetFocus()
    {
        SetWindowAlpha(255);
    }

    public override void OnKillFocus()
    {
        SetWindowAlpha(128);
    }
}
