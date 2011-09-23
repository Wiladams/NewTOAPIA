using System;

using NewTOAPIA.Drawing;
using NewTOAPIA.UI;

/// <summary>
/// The Drawable polygon is meant to take in an array of points that
/// are in unit measurements (floating point between 0.0 and 1.0
/// When it's time to draw, the points are scaled up to the specified
/// size and the polygon is drawn.
/// </summary>
public class GPolygon : Graphic
{
	protected uint fFillColor;
	protected uint fBorderColor;
	protected System.Drawing.Point[] fPoints;
    protected GDIPen fPen;
    protected GDIBrush fBrush;

	public GPolygon(System.Drawing.Point[] points, int x, int y, int width, int height, uint fillColor, uint borderColor)
	{
		fPoints = points;
		fFillColor = fillColor;
		fBorderColor = borderColor;

        fPen = new GDIPen(borderColor);
        fBrush = new GDIBrush(BrushStyle.Solid, HatchStyle.Vertical, fillColor, Guid.NewGuid());
    }

	public uint FillColor
	{
		get { return fFillColor; }
		set { fFillColor = value; }
	}

	public uint BorderColor
	{
		get { return fBorderColor; }
		set { fBorderColor = value; }
	}

	public System.Drawing.Point[] Points
	{
		get { return fPoints; }
	}

	public override void DrawBackground(DrawEvent devent)
	{
        IGraphPort graphPort = devent.GraphPort;

        graphPort.SetPen(fPen);
        graphPort.SetBrush(fBrush);
        graphPort.Polygon(Points);
	}
}

