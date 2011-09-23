using System;
using System.Drawing;
using System.Xml;

using NewTOAPIA.Drawing;
using NewTOAPIA.UI;

namespace PLC
{
    using NewTOAPIA;
    
    ///
	/// <summary>
	/// This control is meant to hold a list of deliverables.
	/// It is simply a box with some specialized text pushbuttons in it.
	/// </summary>
	public class DeliverablesList : PLCBaseControl
    {
        #region Fields
        GradientRectangle fBackground;
        #endregion

        #region Constructors
        public DeliverablesList(string name, int x, int y, int width, int height, uint bkColor)
			: this(name, x, y, width, height, bkColor, RGBColor.RGB(119, 153, 238))
		{
		}

		public DeliverablesList(string name, int x, int y, int width, int height, uint bkColor, uint borderColor)
			: this(name, x, y, width, height, bkColor, borderColor, 90)
		{
		}

        public DeliverablesList(string name, int x, int y, int width, int height, uint bkColor, uint borderColor, int angle)
            : base(name, x, y, width, height)
        {
            fBackground = new GradientRectangle(0, 0, width, height, RGBColor.White, bkColor, borderColor, angle);

            //LayoutHandler = new VEqualLayout(this, 0, 0);
            LayoutHandler = new VTextLayout(this, 3, 0);
        }
        #endregion

        public void AddDeliverable(string deliverable)
		{
            GDIFont font = new GDIFont("Tahoma", 6, Guid.NewGuid());
			TextBox tBox = new TextBox(deliverable, "Tahoma", 6, GDIFont.FontStyle.Regular, 0, 0, Frame.Width - 6, (int)((font.Height*2)/72F), 0);

            //StringFormat aFormat = new StringFormat();
            //aFormat.Alignment = StringAlignment.Near;
            //aFormat.LineAlignment = StringAlignment.Near;
			//aFormat.Trimming = StringTrimming.Word;
			//aFormat.FormatFlags |= StringFormatFlags.NoClip;
			//aFormat.FormatFlags |= StringFormatFlags.NoWrap;

			//tBox.StringFormat = aFormat;


			AddGraphic(tBox);

		}

        protected override void  OnUpdateGeometryState()
		{
            fBackground.Frame = new Rectangle((int)Origin.X, (int)Origin.Y, Frame.Width, Frame.Height);
		}

		// We override this one because we want to be able to draw the
		// pretty background
        public override void DrawBackground(DrawEvent devent)
        {
            fBackground.Draw(devent);
        }

		public void Clear()
		{
			this.RemoveAllGraphics();
		}


		// XML Serialization
        //public override void WriteXmlBody(XmlWriter writer)
        //{
        //    // Write out the generic body first
        //    base.WriteXmlBody(writer);


        //    // Write out the background graphic
        //    if (fBackground != null)
        //    {
        //        writer.WriteStartElement("background");
        //        fBackground.WriteXml(writer);
        //        writer.WriteEndElement();
        //    }
        //}
	}
}

