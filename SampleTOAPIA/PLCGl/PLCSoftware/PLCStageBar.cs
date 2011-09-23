using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

using NewTOAPIA.Drawing;
using NewTOAPIA.UI;

namespace PLC
{
    using NewTOAPIA;

	/// <summary>
	/// Summary description for Class1
	/// </summary>
	public class PLCStageBar :PLCBaseControl 
	{
        GDIPixmap fImage;
		GDIBrush fBrush;

		public PLCStageBar(string name, int x, int y, int width, int height)
			:base(name, x, y, width, height)
		{
            fBrush = new GDIBrush(BrushStyle.Solid, HatchStyle.Vertical, RGBColor.RGB(238, 238, 238), Guid.NewGuid());

			//fPixmap = Image.FromFile("stagehead.png");
            
            //fPixmap = new PixelBuffer(this.GetType(), "Resources.stagehead.png");
			//fPixmap = new PixelBuffer(this.GetType(), "Resources.stagebar5.png");
			//fPixmap = new PixelBuffer(this.GetType(), "Resources.stageheademf.emf");
        }

		public override void DrawBackground(DrawEvent devent)
		{
            IGraphPort graphPort = devent.GraphPort;
			//graphPort.FillRectangle(fBrush,Frame.Left, Frame.Top, Width, Height);
            graphPort.FillRectangle(fBrush, Frame);

            //PointF[] destPara = new PointF[] {
            //    new PointF(Origin.X, Origin.Y+0.10F),
            //    new PointF(Origin.X+Dimension.Width, Origin.Y+0.10F),
            //    new PointF(Origin.X, Origin.Y+Dimension.Height-0.30F)};
   
			System.Drawing.Rectangle f = new System.Drawing.Rectangle(Frame.Left,Frame.Top+10,Dimension.Width,Dimension.Height-30);
			graphPort.ScaleBitmap(fImage,f);
			
			// Draw a transparent gradient over the top
			//graphPort.DrawRectangle(new Pen(RGBColor.RGB(204, 204, 204), 0.5F /72), f);
		}

	}
}