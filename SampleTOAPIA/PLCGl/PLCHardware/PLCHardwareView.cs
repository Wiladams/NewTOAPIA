using System;
using System.Drawing;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Threading;

using NewTOAPIA;
using NewTOAPIA.Drawing;
using NewTOAPIA.UI;

namespace PLC
{
	/// <summary>
	/// This view is meant to be a view that covers an entire window
	/// It doesn't do much more than exist the size of the window
	/// And has the opportunity to act as the containing view for 
	/// a bunch of drawing.
	/// </summary>
	[XmlRoot("UserInterface")]
	public class PLCHardwareView : PLCView
	{
			HardwareDataBinder fMyDataBinder;

		public PLCHardwareView()
			: this("PLCHardwareView", 0, 0, 1000, 750)
		{ }

		public PLCHardwareView(string name, int x, int y, int width, int height)
			: base(name, "Product Life Cycle - Hardware", x, y, width, height)
		{
			fMyDataBinder = new HardwareDataBinder(this);
			Legend = new HardwareLegend();
			//PreferredTransition = new UnCoverDown(this.BackingImage, this.BackingImage, System.Drawing.Rectangle.Empty, 2);
            PreferredTransition = new CrossFade(null, null, Rectangle.Empty, 0.5);
            //PreferredTransition = new PushDown(null, null, Rectangle.Empty, 0.5);
            //PreferredTransition = new CoverDown(null, null, Rectangle.Empty, 1.0);
            //PreferredTransition = new WipeLeft(null, null, Rectangle.Empty, 1.0);
            //PreferredTransition = new WipeDown(null, null, Rectangle.Empty, 1.0);
            //PreferredTransition = new WipeRight(null, null, Rectangle.Empty, 0.5);
        }


		public override PLCDataBinder DataBinder
		{
			get
			{
				return fMyDataBinder;
			}
		}

		public override void AddPhaseHeadings()
		{
			AddGraphic(new GradientTextBar("Front End", 19, 157, 218, 34, CS.HeaderBack, CS.HeaderBorder, CS.HeaderText));
			AddGraphic(new GradientTextBar("Product Development", 259, 157, 291, 34, CS.HeaderBack, CS.HeaderBorder, CS.HeaderText));
			AddGraphic(new GradientTextBar("Production", 576, 157, 143, 34, CS.HeaderBack, CS.HeaderBorder, CS.HeaderText));
			AddGraphic(new GradientTextBar("Product Retirement", 739, 157, 218, 34, CS.HeaderBack, CS.HeaderBorder, CS.HeaderText));
		}


		public override void AddStageHeadings()
		{
			//Color txtColor = RGBColor.RGB(102,17,0);
            uint txtColor = PLCColorScheme.Default.StageHeaderText;

			// Ideation, Roadmap Development, Product Research
            IPixelArray gImage = PixelBufferHelper.CreateFromResource("Resources.stagebar3.png");
			AddGraphic(new GraphicImage("FrontEndStageBar", 19, 202, 218, 34, gImage));
            AddGraphic(new TextBox("Ideation", "Tahoma", 7, GDIFont.FontStyle.Regular, 19, 207, 70, 24, txtColor));
            AddGraphic(new TextBox("Roadmap Development", "Tahoma", 7, GDIFont.FontStyle.Regular, 94, 207, 70, 24, txtColor));
            AddGraphic(new TextBox("Product Research", "Tahoma", 7, GDIFont.FontStyle.Regular, 167, 207, 70, 24, txtColor));

			// Preliminary Design, Product Engineering, Design Validation, Production Readiness
            gImage = PixelBufferHelper.CreateFromResource("Resources.stagebar4.png");
            AddGraphic(new GraphicImage("ProductDevelopmentStageBar", 260, 202, 291, 34, gImage));
            AddGraphic(new TextBox("Preliminary Design", "Tahoma", 7, GDIFont.FontStyle.Regular, 260, 207, 70, 24, txtColor));
            AddGraphic(new TextBox("Product Engineering", "Tahoma", 7, GDIFont.FontStyle.Regular, 333, 207, 70, 24, txtColor));
            AddGraphic(new TextBox("Design Validation", "Tahoma", 7, GDIFont.FontStyle.Regular, 408, 207, 70, 24, txtColor));
            AddGraphic(new TextBox("Production Readiness", "Tahoma", 7, GDIFont.FontStyle.Regular, 482, 207, 70, 24, txtColor));

			// Production Ramp Up, Production Sustaining
            gImage = PixelBufferHelper.CreateFromResource("Resources.stagebar2.png");
            AddGraphic(new GraphicImage("ProductionStageBar", 576, 202, 143, 34, gImage));
            AddGraphic(new TextBox("Production Ramp Up", "Tahoma", 7, GDIFont.FontStyle.Regular, 576, 207, 70, 24, txtColor));
            AddGraphic(new TextBox("Production Sustaining", "Tahoma", 7, GDIFont.FontStyle.Regular, 650, 207, 70, 24, txtColor));

			// SKU Management, Production Shutdown, Termination
            gImage = PixelBufferHelper.CreateFromResource("Resources.stagebar3.png");
            AddGraphic(new GraphicImage("ProductRetirementStageBar", 739, 202, 218, 34, gImage));
            AddGraphic(new TextBox("SKU Management", "Tahoma", 7, GDIFont.FontStyle.Regular, 740, 207, 70, 24, txtColor));
            AddGraphic(new TextBox("Production Shutdown", "Tahoma", 7, GDIFont.FontStyle.Regular, 814, 207, 70, 24, txtColor));
			AddGraphic(new TextBox("Termination", "Tahoma", 7, GDIFont.FontStyle.Regular, 888, 207, 70, 24, txtColor));

		}

		public override void AddPhaseArrows()
		{
			AddGraphic(new GraphicArrow("leadingArrow", 0, 116, 45, 468));
			AddGraphic(new GraphicArrow("FrontEndArrow", 17, 116, 270, 468));
			AddGraphic(new GraphicArrow("ProductDevelopmentArrow", 257, 116, 342, 468));
			AddGraphic(new GraphicArrow("ProductionArrow", 570, 116, 194, 468));
			AddGraphic(new GraphicArrow("ProductRetirementArrow", 735, 116, 264, 468));
		}

		public override void AddDeliveryContainers()
		{

			uint blueColor = RGBColor.RGB(204, 221, 255);
            uint blueBorder = RGBColor.RGB(119, 153, 238);
            uint greenColor = RGBColor.RGB(221, 255, 204);
            uint greenBorder = RGBColor.RGB(102, 204, 51);
			 


			// Front End Phase Deliverables List
			// The deliverables lists
			AddGraphic(new DeliverablesList("p1s1dlist", 19, 248, 69, 167, blueColor, blueBorder, 45));
			AddGraphic(new DeliverablesList("p1s2dlist", 94, 248, 69, 167, blueColor, blueBorder, 45));
			AddGraphic(new DeliverablesList("p1s3dlist", 167, 248, 69, 167, blueColor, blueBorder, 45));

            // The Checklist s
            AddGraphic(new DeliverablesList("p1s1clist", 19, 429, 69, 114, greenColor, greenBorder, 45));
            AddGraphic(new DeliverablesList("p1s2clist", 94, 429, 69, 114, greenColor, greenBorder, 45));
            AddGraphic(new DeliverablesList("p1s3clist", 167, 429, 69, 114, greenColor, greenBorder, 45));



            // Product Development Phase Deliverables List
            // The deliverables lists
            AddGraphic(new DeliverablesList("p2s1dlist", 259, 248, 69, 167, blueColor, blueBorder, 45));
            AddGraphic(new DeliverablesList("p2s2dlist", 334, 248, 69, 167, blueColor, blueBorder, 45));
            AddGraphic(new DeliverablesList("p2s3dlist", 407, 248, 69, 167, blueColor, blueBorder, 45));
            AddGraphic(new DeliverablesList("p2s4dlist", 481, 248, 69, 167, blueColor, blueBorder, 45));

            // The Checklist s
            AddGraphic(new DeliverablesList("p2s1clist", 259, 429, 69, 114, greenColor, greenBorder, 45));
            AddGraphic(new DeliverablesList("p2s2clist", 334, 429, 69, 114, greenColor, greenBorder, 45));
            AddGraphic(new DeliverablesList("p2s3clist", 407, 429, 69, 114, greenColor, greenBorder, 45));
            AddGraphic(new DeliverablesList("p2s4clist", 481, 429, 69, 114, greenColor, greenBorder, 45));


            // Production Phase Deliverables List
            // The deliverables lists
            AddGraphic(new DeliverablesList("p3s1dlist", 575, 248, 69, 167, blueColor, blueBorder, 45));
            AddGraphic(new DeliverablesList("p3s2dlist", 649, 248, 69, 167, blueColor, blueBorder, 45));

            // The Checklist s
            AddGraphic(new DeliverablesList("p3s1clist", 575, 429, 69, 114, greenColor, greenBorder, 45));
            AddGraphic(new DeliverablesList("p3s2clist", 649, 429, 69, 114, greenColor, greenBorder, 45));


            // Product Retirement Phase Deliverables List
            // The deliverables lists
            AddGraphic(new DeliverablesList("p4s1dlist", 740, 248, 69, 167, blueColor, blueBorder, 45));
            AddGraphic(new DeliverablesList("p4s2dlist", 814, 248, 69, 167, blueColor, blueBorder, 45));
            AddGraphic(new DeliverablesList("p4s3dlist", 888, 248, 69, 167, blueColor, blueBorder, 45));

            // The Checklist s
            AddGraphic(new DeliverablesList("p4s1clist", 740, 429, 69, 114, greenColor, greenBorder, 45));
            AddGraphic(new DeliverablesList("p4s2clist", 814, 429, 69, 114, greenColor, greenBorder, 45));
            AddGraphic(new DeliverablesList("p4s3clist", 888, 429, 69, 114, greenColor, greenBorder, 45));
		}

	}
}