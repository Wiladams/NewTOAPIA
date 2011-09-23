using System;
using System.Xml;

using NewTOAPIA;
using NewTOAPIA.UI;
using NewTOAPIA.Drawing;

namespace PLC
{
	/// <summary>
	/// This graphic basically shows the phase title.
	/// It is clickable so that actions can occur once it is clicked on.
	/// </summary>
	public class GradientRectangle : Graphic
	{
		uint fColorBegin;
		uint fColorEnd;
		uint fColorBorder;
		float fAngle;
        GradientRect fGradient;
        System.Drawing.Rectangle fBorder;

        GDIPen fBorderPen;

		public GradientRectangle()
			: this(0, 0, 1, 1, RGBColor.Black, RGBColor.White, RGBColor.Black, 90)
		{
		}

		public GradientRectangle(int x, int y, int width, int height, uint colorBegin, uint colorEnd, uint colorBorder, float angle)
			:base("GradientRectangle",x,y,width,height)
		{
			fColorBegin = colorBegin;
			fColorEnd = colorEnd;
			fColorBorder = colorBorder;
			fAngle = angle;
            fBorderPen = new GDIPen(PenType.Cosmetic, PenStyle.Solid, PenJoinStyle.Round, PenEndCap.Round, colorBorder, 1, Guid.NewGuid());

            fGradient = new GradientRect(0, 0, width, height, colorBegin, colorEnd, GradientRectDirection.Vertical);
            fBorder = new System.Drawing.Rectangle(0, 0, width, height);
        }

		public override void DrawSelf(DrawEvent de)
		{
            IGraphPort graphPort = de.GraphPort;

            graphPort.DrawGradientRectangle(fGradient);
            graphPort.DrawRectangle(fBorderPen, fBorder);
		}

        protected override void OnUpdateGeometryState()
        {
            base.OnUpdateGeometryState();

            fGradient.SetVertices((int)Origin.X, (int)Origin.Y, Frame.Width, Frame.Height);
            fBorder = new System.Drawing.Rectangle((int)Origin.X, (int)Origin.Y, Frame.Width, Frame.Height);
        }

		// XML Serialization
        //public override void WriteXmlAttributes(XmlWriter writer)
        //{
        //    base.WriteXmlAttributes(writer);
        //    writer.WriteAttributeString("angle", fAngle.ToString());
        //}

        //public override void WriteXmlBody(XmlWriter writer)
        //{
        //    base.WriteXmlBody(writer);

        //    // Color Begin
        //    writer.WriteStartElement("startcolor");
        //    writer.WriteStartElement("color");
        //    writer.WriteAttributeString("r", fColorBegin.R.ToString());
        //    writer.WriteAttributeString("g", fColorBegin.G.ToString());
        //    writer.WriteAttributeString("b", fColorBegin.B.ToString());
        //    writer.WriteAttributeString("a", fColorBegin.A.ToString());
        //    writer.WriteEndElement();
        //    writer.WriteEndElement();

        //    // Color End
        //    writer.WriteStartElement("endcolor");
        //    writer.WriteStartElement("color");
        //    writer.WriteAttributeString("r", fColorEnd.R.ToString());
        //    writer.WriteAttributeString("g", fColorEnd.G.ToString());
        //    writer.WriteAttributeString("b", fColorEnd.B.ToString());
        //    writer.WriteAttributeString("a", fColorEnd.A.ToString());
        //    writer.WriteEndElement();
        //    writer.WriteEndElement();

        //    // Color Border
        //    writer.WriteStartElement("bordercolor");
        //    writer.WriteStartElement("color");
        //    writer.WriteAttributeString("r", fColorBorder.R.ToString());
        //    writer.WriteAttributeString("g", fColorBorder.G.ToString());
        //    writer.WriteAttributeString("b", fColorBorder.B.ToString());
        //    writer.WriteAttributeString("a", fColorBorder.A.ToString());
        //    writer.WriteEndElement();
        //    writer.WriteEndElement();

        //}
	}
}

