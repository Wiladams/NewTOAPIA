using System;
//using System.Drawing;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

using TOAPI.Types;

using NewTOAPIA;
using NewTOAPIA.Drawing;
using NewTOAPIA.Drawing.GDI;
using NewTOAPIA.Graphics;
using NewTOAPIA.UI;

public enum StringAlignment
{
    Left,
    Center,
    Right,
    Near = Left,
}
	public class TextBox : Graphic
    {
        #region Fields
        private GDIFont fFont;
        private string fFontName;
		private int fPointSize;
		private FontStyle fFontStyle;


		private String fString;
		private uint fTextColor;
		private uint fBackColor;
        Point fTextOrigin;

        StringAlignment fHAlignment;
        StringAlignment fVAlignment;

		private IGraphic fBackground;
        #endregion

        #region Constructors
        public TextBox()
			: this(string.Empty, "Courier",8,GDIFont.FontStyle.Regular,0,0,1,1,RGBColor.Black)
		{
		}

        public TextBox(string aString, string aFontName, int pointSize, FontStyle style, int x, int y, int width, int height, uint txtColor)
			:this(aString,aFontName,pointSize,style,x,y,width,height,StringAlignment.Center,StringAlignment.Center,txtColor,null)
		{
		}

        public TextBox(string aString, string aFontName, int pointSize, FontStyle style, int x, int y, int width, int height, Graphic background)
			: this(aString, aFontName, pointSize, style, x, y, width, height, StringAlignment.Center, StringAlignment.Center, RGBColor.Black, background)
		{
		}

        public TextBox(string aString, string aFontName, int pointSize, FontStyle style, 
            int x, int y, int width, int height, 
            StringAlignment align, StringAlignment lineAlign, uint txtColor, Graphic background)
			: base("TextBox", x, y, width, height)
		{
			fBackground = background;
			fString = aString;
			fFontName = aFontName;
			fPointSize = pointSize;
			fFontStyle = style;
            fFont = new GDIFont(fFontName, fPointSize);

			fTextColor = txtColor;
			fBackColor = RGBColor.TRANSPARENT;
            fHAlignment = align;
            fVAlignment = lineAlign;

            CalculateTextOrigin();

            //fStringFormat = new StringFormat();
            //fStringFormat.Alignment = align;
            //fStringFormat.LineAlignment = lineAlign;
        }
        #endregion

        //protected override void OnUpdateGeometryState()
        //{
        //    fBackground.Frame = Frame;
        //    CalculateTextOrigin();
        //}

        void CalculateTextOrigin()
        {
            Size stringSize = fFont.MeasureString(fString);
            int xCenter = Origin.X + Frame.Width / 2;
            int yCenter = Origin.Y + Frame.Height / 2;

            switch (fHAlignment)
            {
                case StringAlignment.Left:
                    fTextOrigin.X = Origin.X;
                    break;

                case StringAlignment.Center:
                    fTextOrigin.X = xCenter - stringSize.Width / 2;
                    break;

                case StringAlignment.Right:
                    fTextOrigin.X = Origin.X + Dimension.Width - stringSize.Width;
                    break;
            }

            switch (fVAlignment)
            {
                case StringAlignment.Left:
                    fTextOrigin.Y = Origin.X;
                    break;

                case StringAlignment.Center:
                    fTextOrigin.Y = yCenter - stringSize.Height / 2;
                    break;

                case StringAlignment.Right:
                    fTextOrigin.Y = Origin.Y + Dimension.Height - stringSize.Height;
                    break;
            }


        }

		// Graphic Overrides
        public override void DrawBackground(DrawEvent devent)
		{
			if (fBackground != null)
			{
				fBackground.Draw(devent);
			}
		}

		public override void DrawSelf(DrawEvent devent)
		{
            devent.GraphPort.SetTextColor(TextColor);
            devent.GraphPort.SetBkColor(RGBColor.TRANSPARENT);
            devent.GraphPort.SetBkMode(RGBColor.TRANSPARENT);
            devent.GraphPort.SetFont(fFont);
			devent.GraphPort.DrawString(fTextOrigin.X, fTextOrigin.Y, fString);
        }

        #region Properties
        public string Text
		{
			get
			{
				return fString;
			}

			set
			{
				fString = value;
			}
		}

        public IGraphic Background
        {
            get { return fBackground; }
            set
            {
                fBackground = value;
            }
        }

		public uint BackgroundColor
		{
			get { return fBackColor; }
			set { fBackColor = value; }
		}

		public uint TextColor
		{
			get { return fTextColor; }
			set { 
				fTextColor = value;
			}
		}

		public string FontName
		{
			get
			{
				return fFontName;
			}

			set
			{
				fFontName = value;
			}
		}

		public int PointSize
		{
			get
			{
				return fPointSize;
			}

			set
			{
				fPointSize = value;
			}
		}

		public Size2I PreferredSize
		{
			get {
                //float width = Width;
                //float height = 0;
                //Graphics grfx = Graphics.FromHwnd(IntPtr.Zero);	// GraphPort of desktop
                //grfx.PageUnit = this.GraphicsUnit;
                //SizeF aSize = SizeF.Empty;

                //if (grfx != null)
                //{
                //    StringFormat strFormat = new StringFormat(StringFormat);
                //    //strFormat.Trimming = StringTrimming.Character;
                //    //aSize = grfx.MeasureString(fString, fFont, width, strFormat);
                //    int tmpWidth = (int)(width*grfx.DpiX);
                //    aSize = grfx.MeasureString(fString,fFont, new SizeF(width,Height),strFormat);
                //    //aSize = grfx.MeasureString(fString, fFont, tmpWidth,strFormat);
                //    // Get rid of the graphics object we created
                //    grfx.Dispose();
                //}

				// Calculate the height using MeasureString
                return new Size2I(Frame.Width, Frame.Height) ;
			}
        }
        #endregion


        // XML Serialization

    //    public override void WriteXmlAttributes(XmlWriter writer)
    //    {
    //        base.WriteXmlAttributes(writer);


    //        /*
    //        private uint fTextColor;
    //        private uint fBackColor;

    //        private Brush fBrush;
    //        private StringFormat fStringFormat;
    //*/
    //    }

        //public override void WriteXmlBody(XmlWriter writer)
        //{
        //    // Write out the generic body first
        //    base.WriteXmlBody(writer);

        //    // Write out the text
        //    writer.WriteStartElement("text");
        //    writer.WriteString(fString);
        //    writer.WriteEndElement();
			
        //    // Write out font information
        //    writer.WriteStartElement("font");
        //    writer.WriteAttributeString("name", fFontName);
        //    writer.WriteAttributeString("points", fPointSize.ToString());
        //    writer.WriteAttributeString("style", fFontStyle.ToString());
        //    writer.WriteEndElement();

        //    // Write out the background graphic
        //    if (fBackground != null)
        //    {
        //        writer.WriteStartElement("background");
        //        fBackground.WriteXml(writer);
        //        writer.WriteEndElement();
        //    }
        //}

	}

