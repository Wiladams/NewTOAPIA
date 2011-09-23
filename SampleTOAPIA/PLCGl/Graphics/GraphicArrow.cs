using System;
using System.Drawing;
using System.Xml;
using System.Xml.Serialization;

using NewTOAPIA;
using NewTOAPIA.Drawing;
using NewTOAPIA.UI;

using TOAPI.Types;

	public class GraphicArrow : Graphic
	{
		int fArrowDepth;
		int fDropMargin;
		uint fFillColor;
		uint fBorderColor;
        protected GDIPen fPen;
        protected GDIBrush fBrush;
        Point[] points;

        #region Constructors
        public GraphicArrow(string name, int left, int top, int width, int height)
            : this(name, left, top, width, height, RGBColor.RGB(238, 238, 238), RGBColor.RGB(204, 204, 204))
		{
		}

		public GraphicArrow(string name, int frameLeft, int frameTop, int width, int height,
			uint fillColor, uint borderColor)
            : base(name, frameLeft, frameTop, width, height)
		{
            fPen = new GDIPen(borderColor);
            fBrush = new GDIBrush(BrushStyle.Solid, HatchStyle.Vertical, fillColor, Guid.NewGuid());
            fFillColor = fillColor;
			fBorderColor = borderColor;

			UpdateGeometryState();
        }
        #endregion

        protected override void OnUpdateGeometryState()
        {
            // The arrow depth is 1/10th the overall height
            fArrowDepth = (int)(Frame.Height * 0.1f);
            fDropMargin = (int)(fArrowDepth / 2);

            int x = (int)Origin.X;
            int y = (int)Origin.Y;
            int bottom = y + Frame.Height;
            int right = x + Frame.Width;

            points = new Point[]{
					new Point(x, y + fDropMargin),
					new Point(right - fArrowDepth, y + fDropMargin),
					new Point(right - fArrowDepth, y),
					new Point(right, y + (Dimension.Height / 2)),
					new Point(right - fArrowDepth, bottom),
					new Point(right - fArrowDepth, bottom - fDropMargin),
					new Point(x, bottom - fDropMargin),
					new Point(x, y + fDropMargin)
			};
        }
        
        public Point[] Points
		{
			get { return points; }
		}

		public override Rectangle ClientRectangle
		{
			get
			{
				Rectangle rect = new Rectangle( (int)Origin.X+2, (int)Origin.Y+fDropMargin + 2,
					Dimension.Width - fArrowDepth - 4, Dimension.Height - fDropMargin * 2 - 4);

				return rect;
			}
		}

		public uint BorderColor
		{
			get { return fBorderColor; }
			set
			{
				fBorderColor = value;
				Invalidate();
			}
		}

		public uint FillColor
		{
			get { return fFillColor; }
			set { 
				fFillColor = value;
				Invalidate();
			}
		}

		public override void DrawBackground(DrawEvent devent)
		{
            IGraphPort graphPort = devent.GraphPort;

            graphPort.SetPen(fPen);
            graphPort.SetBrush(fBrush);

            graphPort.Polygon(points);
		}

		// XML Serialization

        //public override void WriteXmlAttributes(XmlWriter writer)
        //{
        //    base.WriteXmlAttributes(writer);

        //    writer.WriteAttributeString("arrowdepth", fArrowDepth.ToString());
        //    writer.WriteAttributeString("dropmargin", fDropMargin.ToString());
        //}

        //public override void WriteXmlBody(XmlWriter writer)
        //{
        //    // Write out the generic body first
        //    base.WriteXmlBody(writer);

        //    writer.WriteStartElement("polygon");

        //    writer.WriteStartElement("fillcolor");
        //    writer.WriteStartElement("color");
        //    writer.WriteAttributeString("r", RGBColor.R(fFillColor).ToString());
        //    writer.WriteAttributeString("g", RGBColor.G(fFillColor).ToString());
        //    writer.WriteAttributeString("b", RGBColor.B(fFillColor).ToString());
        //    //writer.WriteAttributeString("a", RGBColor..A(fFillColor).ToString());
        //    writer.WriteEndElement();
        //    writer.WriteEndElement();

        //    writer.WriteStartElement("bordercolor");
        //    writer.WriteStartElement("color");
        //    writer.WriteAttributeString("r", RGBColor.R(fBorderColor).ToString());
        //    writer.WriteAttributeString("g", RGBColor.G(fBorderColor).ToString());
        //    writer.WriteAttributeString("b", RGBColor.B(fBorderColor).ToString());
        //    //writer.WriteAttributeString("a", RGBColor.A(fBorderColor).ToString());
        //    writer.WriteEndElement();
        //    writer.WriteEndElement();
			
        //    writer.WriteStartElement("points");
        //    writer.WriteAttributeString("count", points.Length.ToString());
        //    foreach (PointF p in points)
        //    {
        //        // Write out a point
        //        writer.WriteStartElement("pointf");
        //        writer.WriteAttributeString("x", p.X.ToString());
        //        writer.WriteAttributeString("y", p.Y.ToString());
        //        writer.WriteEndElement();
        //    }
        //    writer.WriteEndElement();
        //    writer.WriteEndElement();
        //}

	}
