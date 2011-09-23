using System;
using System.Drawing;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

using NewTOAPIA.Drawing;
using NewTOAPIA.UI;
using TOAPI.Types;

/// <summary>
/// The GRectangle class encapsulates the properties of a border.  Basically a rectangle.
/// It most closely matches the Border property of a Box in the CSS system.
/// </summary>
public class GRectangle : Graphic
{
	uint fBackColor;
	uint fBorderColor;
	GDIPen fPen;
    RectangleG fRectangle;

    public GRectangle(string name, int x, int y, int width, int height, uint bkColor, uint borderColor, float weight)
        :base(name, x, y, width, height)
	{
        fRectangle = new RectangleG(0, 0, width, height);
		fBackColor = bkColor;
		fBorderColor = borderColor;

        fPen = new GDIPen(fBorderColor);
	}

	public virtual Rectangle Extent
	{
		get {return fRectangle.Extent;}
	}

    public override void DrawSelf(DrawEvent e)
    {
        e.GraphPort.DrawRectangle(fPen, Extent);
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
