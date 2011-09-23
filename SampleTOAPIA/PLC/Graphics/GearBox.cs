using System;

using NewTOAPIA.Drawing;
using NewTOAPIA.Drawing.GDI;
using NewTOAPIA.UI;

using PLC;

/// <summary>
/// Summary description for Class1
/// </summary>
public class GearBox : ActiveArea
{
	GraphicImage fProductWheel;
	GraphicImage fFeatureWheel;
    GDIPixmap fBacking;
	IGraphPort fGraphPort;

	public GearBox(string name, int x, int y, int width, int height)
		: base(name, x, y, width, height)
	{
        fBacking = new GDIDIBSection(Convert.ToInt32(width * 96), Convert.ToInt32(height * 96));
        fGraphPort = fBacking.GraphPort;
		//fGraphPort.PageUnit = GraphicsUnit.Inch;

		AddImages();
	}

	void AddImages()
	{
        fProductWheel = GraphicImage.CreateFromResource("Resources.ProductWheel.png", 0, 0, 115, 114);
		//AddGraphic(fProductWheel);
        fFeatureWheel = GraphicImage.CreateFromResource("Resources.FeatureWheel.png", 110, 59, 56, 56);
		//AddGraphic(fFeatureWheel);

        if (fProductWheel != null)
            fGraphPort.PixBlt(fProductWheel.Image, fProductWheel.Frame.Left, fProductWheel.Frame.Top);

        if (fFeatureWheel != null)
            fGraphPort.PixBlt(fFeatureWheel.Image, fFeatureWheel.Frame.Left, fFeatureWheel.Frame.Top);
    }

    public override void DrawSelf(DrawEvent devent)
	{
		//fGraphPort.Clear(Color.White);
		//fGraphPort.DrawImage(fProductWheel.Image, fProductWheel.Frame);
		//fGraphPort.DrawImage(fFeatureWheel.Image, fFeatureWheel.Frame);

		//graphPort.DrawImage(fBacking,new RectangleF(0,0,Width,Height));
	}
   
    public override void OnMouseMove(MouseActivityArgs e)
	{
		// WAA - temporary disable due to scaling issues
		return ;

        //if (this.Window != null)
        //{
        //    Graphics grfx = this.Window.CreateGraphics();
        //    grfx.PageUnit = GraphicsUnit;

        //    Animate(grfx);
        //    grfx.Dispose();
        //}
	}

	public void Animate(IGraphPort graphPort)
	{
        //graphPort.PageUnit = GraphicsUnit.Inch;
        //graphPort.TranslateTransform(Left, Top);

        //RectangleF f1 = fProductWheel.Frame;
        //RectangleF f2 = fFeatureWheel.Frame;

        //float centerX = f1.Left + f1.Width / 2;
        //float centerY = f1.Top + f1.Height / 2;
        //PointF productCenter = new PointF(centerX, centerY);
        //PointF[] points = new PointF[] { 
        //        new PointF( f1.Left, f1.Top ), 
        //        new PointF( f1.Right, f1.Top ),
        //        new PointF( f1.Left, f1.Bottom )};

        //float featureCenterX = f2.Left + f2.Width / 2;
        //float featureCenterY = f2.Top + f2.Height / 2;
        //PointF featureCenter = new PointF(featureCenterX, featureCenterY);
        //PointF[] featurePoints = new PointF[] { 
        //        new PointF( f2.Left, f2.Top ), 
        //        new PointF( f2.Right, f2.Top ),
        //        new PointF( f2.Left, f2.Bottom )};

        //Matrix PX = new Matrix();
        //Matrix FX = new Matrix();

		
		
        //for (int angle = 0; angle <= 180; angle += 18)
        //{
        //    PX.RotateAt((float)10 / 2, productCenter);
        //    PX.TransformPoints(points);

        //    fGraphPort.Clear(Color.White);
        //    fGraphPort.DrawImage(fProductWheel.Image, points);

        //    // Do the counter clockwise rotation on the feature wheel
        //    FX.RotateAt(-(float)10, featureCenter);
        //    FX.TransformPoints(featurePoints);

        //    fGraphPort.DrawImage(fFeatureWheel.Image, featurePoints);
			
			
        //    graphPort.DrawImage(fBacking, new RectangleF(0, 0, Width, Height));

        //    Thread.Sleep(100);
        //}

        //// Draw one final time so we get the transparency back
        //DrawInto(graphPort);


        //graphPort.Dispose();
	}

    //public override void WriteXmlBody(System.Xml.XmlWriter writer)
    //{
    //    base.WriteXmlBody(writer);

    //    fProductWheel.WriteXml(writer);
    //    fFeatureWheel.WriteXml(writer);
    //}
}
