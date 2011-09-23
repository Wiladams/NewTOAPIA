using System;
using System.Drawing;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

using NewTOAPIA;
using NewTOAPIA.Drawing;
using NewTOAPIA.UI;
using TOAPI.Types;

public class GraphicLine : Graphic
{
    LineG fLine;
	float fWidth;
	uint fColor;
	GDIPen fPen;

	public GraphicLine()
		:this(string.Empty,0,0,1,1,RGBColor.Black,1/72)
	{
	}

    public GraphicLine(string name, int x1, int y1, int x2, int y2, uint color, int width)
        :base(name, x1,y1, x2-x1, y2-y1)
	{
        fLine = new LineG(new Point(x1, y1), new Point(x2, y2));

		fWidth = width;
		fColor = color;

		// The weight is in points, so we need to convert to local units.
		// In this case, we'll go with inches.
        fPen = new GDIPen(PenType.Geometric, PenStyle.Solid, PenJoinStyle.Round, PenEndCap.Round, color, Frame.Width, Guid.NewGuid());
	}

	public virtual System.Drawing.Rectangle Extent
	{
		get {
            return new Rectangle(fLine.StartPoint.X,fLine.StartPoint.Y,
            Math.Abs(fLine.EndPoint.X-fLine.StartPoint.X),Math.Abs(fLine.EndPoint.Y-fLine.EndPoint.Y));
        }
	}

	public override void DrawSelf(DrawEvent devent)
	{
        IGraphPort graphPort = devent.GraphPort;

        graphPort.DrawLine(fPen, fLine.StartPoint, fLine.EndPoint);
	}


	// XML Serialization
	public virtual XmlSchema GetSchema()
	{
		return (null);
	}

	public virtual void ReadXml(XmlReader reader)
	{
	}

	public virtual void WriteXml(XmlWriter writer)
	{
	}

}
