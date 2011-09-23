using System;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Threading;

using NewTOAPIA.Drawing;
using NewTOAPIA.UI;

namespace PLC
{
    using NewTOAPIA;

	/// <summary>
	/// This view is meant to be a view that covers an entire window
	/// It doesn't do much more than exist the size of the window
	/// And has the opportunity to act as the containing view for 
	/// a bunch of drawing.
	/// </summary>
	[XmlRoot("UserInterface")]
	public class PLCSoftwareView : PLCView
    {
        #region Fields
        GearBox fGearBox;
		SoftwareDataBinder fMyDataBinder;
        #endregion

        #region Constructors
        public PLCSoftwareView()
			:this("PLCSoftwareView",0,0,1000,750)
		{ }

		public PLCSoftwareView(string name, int x, int y, int width, int height)
			: base(name, "Product Life Cycle - Software", x, y, width, height)
		{
			fMyDataBinder = new SoftwareDataBinder(this);

			//PreferredTransition = new SplitVerticalOut(this.BackingImage, System.Drawing.Rectangle.Empty, 0.5);
			PreferredTransition = new ExpandVerticalOut(this.BackingImage, System.Drawing.Rectangle.Empty, 0.5);
        }
        #endregion

        public override PLCDataBinder DataBinder
		{
			get
			{
				return fMyDataBinder;
			}
		}

		// Do the spinning wheel animation
		public override void Animate()
		{
            if ((null != fGearBox) && (Window != null))
			    fGearBox.Animate(Window.GraphPort);
		}

		public void diagramHeading_MouseDown(object sender, EventArgs e)
		{
			Animate();
		}

		public override void AddPhaseHeadings()
		{
			uint bkColor = RGBColor.RGB(255, 221, 102);
			uint txtColor = RGBColor.RGB(153, 102, 0);
			uint borderColor = RGBColor.RGB(255,204,0);

			GradientTextBar phaseHeading = null;

			phaseHeading = new GradientTextBar("Product Definition", 61, 115, 176, 33, bkColor, borderColor, txtColor);
			AddGraphic(phaseHeading);

			phaseHeading = new GradientTextBar("Product Development", 282, 115, 460, 33, bkColor, borderColor, txtColor);
			AddGraphic(phaseHeading);

			phaseHeading = new GradientTextBar("Servicing", 798, 115, 176, 33, bkColor, borderColor, txtColor);
			AddGraphic(phaseHeading);
		}

        public override void AddStageHeadings()
        {
            uint txtColor = RGBColor.RGB(102, 17, 0);

            PLCStageBar bar = new PLCStageBar("stagebar", 282, 164, 464, 66);
            AddGraphic(bar);

            TextBox box = null;
            box = new TextBox("Requirements", "Tahoma", 8, GDIFont.FontStyle.Regular, 287, 179, 85, 23, txtColor);
            AddGraphic(box);

            box = new TextBox("Design", "Tahoma", 8, GDIFont.FontStyle.Regular, 381, 179, 85, 23, txtColor);
            AddGraphic(box);

            box = new TextBox("Implementation", "Tahoma", 8, GDIFont.FontStyle.Regular, 473, 179, 85, 23, txtColor);
            AddGraphic(box);

            box = new TextBox("Verification", "Tahoma", 8, GDIFont.FontStyle.Regular, 564, 179, 85, 23, txtColor);
            AddGraphic(box);

            box = new TextBox("Release", "Tahoma", 8, GDIFont.FontStyle.Regular, 657, 179, 85, 23, txtColor);
            AddGraphic(box);

        }

        public override void AddPhaseArrows()
        {
            GraphicArrow bigArrow = null;
            bigArrow = new GraphicArrow("graphicArrow", -20, 236, 51, 224);
            AddGraphic(bigArrow);

            bigArrow = new GraphicArrow("phase1Arrow", 61, 236, 205, 224);
            AddGraphic(bigArrow);

            bigArrow = new GraphicArrow("phase2Arrow", 282, 236, 498, 224);
            AddGraphic(bigArrow);

            bigArrow = new GraphicArrow("phase3Arrow", 793, 236, 205, 224);
            AddGraphic(bigArrow);

        }

        public override void AddDeliverablesHeadings()
        {
            AddGraphic(new GradientTextBar(string.Empty, 65, 258, 167, 24, CS.ProductBack, CS.ProductBorder, CS.ProductText));
            AddGraphic(new GradientTextBar("Product Activities", 286, 258, 455, 24, CS.ProductBack, CS.ProductBorder, CS.ProductText));
            AddGraphic(new GradientTextBar(string.Empty, 797, 258, 167, 24, CS.ProductBack, CS.ProductBorder, CS.ProductText));
        }

        void AddProductDeliveryContainers()
        {
            uint bkColor = RGBColor.RGB(204, 221, 255);
            uint grnColor = RGBColor.RGB(221, 255, 204);
            uint greenBorder = RGBColor.RGB(102, 204, 51);

            DeliverablesList deliveryList = null;


            // First Phase Deliverables List
            // The deliverables list
            AddGraphic(new DeliverablesList("p1s1dlist", 65, 282, 167, 121, bkColor, 45));
            // The matching Checklist 
            AddGraphic(new DeliverablesList("p1s1clist", 65, 408, 167, 29, grnColor, greenBorder)); // 90


            // The Product Development Stages
            // The deliverables list
            deliveryList = new DeliverablesList("p2s1dlist", 286, 282, 86, 121, bkColor); // 45
            AddGraphic(deliveryList);
            // The matching Checklist 
            deliveryList = new DeliverablesList("p2s1clist", 286, 408, 86, 29, grnColor, greenBorder); // 90
            AddGraphic(deliveryList);
            // The deliverables list
            deliveryList = new DeliverablesList("p2s2dlist", 378, 282, 86, 121, bkColor); // 45
            AddGraphic(deliveryList);
            // The matching Checklist 
            deliveryList = new DeliverablesList("p2s2clist", 378, 408, 86, 29, grnColor, greenBorder);  // 90
            AddGraphic(deliveryList);
            // The deliverables list
            deliveryList = new DeliverablesList("p2s3dlist", 470, 282, 86, 121, bkColor);  // 45
            AddGraphic(deliveryList);
            // The matching Checklist 
            deliveryList = new DeliverablesList("p2s3clist", 470, 408, 86, 29, grnColor, greenBorder);  // 90
            AddGraphic(deliveryList);
            // The deliverables list
            deliveryList = new DeliverablesList("p2s4dlist", 562, 282, 86, 121, bkColor); // 45
            AddGraphic(deliveryList);
            // The matching Checklist 
            deliveryList = new DeliverablesList("p2s4clist", 562, 408, 86, 29, grnColor, greenBorder);  // 90
            AddGraphic(deliveryList);
            // The deliverables list
            deliveryList = new DeliverablesList("p2s5dlist", 655, 282, 86, 121, bkColor);   // 45
            AddGraphic(deliveryList);
            // The matching Checklist 
            deliveryList = new DeliverablesList("p2s5clist", 655, 408, 86, 29, grnColor, greenBorder);  // 90
            AddGraphic(deliveryList);

            // Last Stage
            // The deliverables list
            deliveryList = new DeliverablesList("p3s1dlist", 797, 282, 167, 121, bkColor);  // 45
            AddGraphic(deliveryList);
            // The matching Checklist 
            deliveryList = new DeliverablesList("p3s1clist", 797, 408, 167, 29, grnColor, greenBorder); // 90
            AddGraphic(deliveryList);
        }

        void AddFeatureDeliveryContainers()
        {
            uint greenColor = RGBColor.RGB(221, 255, 204);
            uint greenBorder = RGBColor.RGB(102, 204, 51);



            // This is the purple rectangle that encompasses the feature deliverables
            AddGraphic(new GRectangle("grect", 270, 497, 496, 181, RGBColor.TRANSPARENT, CS.FeatureBorder, 1));

            // The heading
            AddGraphic(new TextBox("Feature Activities", "Tahoma", 12, GDIFont.FontStyle.Bold, 370, 475, 271, 15, CS.FeatureText));


            // First Stage Deliverables List
            AddGraphic(new DeliverablesList("p2s1fdlist", 286, 513, 86, 115, CS.FeatureBack, CS.FeatureBorder));    // 45
            AddGraphic(new DeliverablesList("p2s1fclist", 286, 634, 86, 29, greenColor, greenBorder));          // 90

            // Second Stage Deliverables List
            AddGraphic(new DeliverablesList("p2s2fdlist", 378, 513, 86, 115, CS.FeatureBack, CS.FeatureBorder));   // 45
            AddGraphic(new DeliverablesList("p2s2fclist", 378, 634, 86, 29, greenColor, greenBorder));          // 90

            // Third Stage Deliverables List
            AddGraphic(new DeliverablesList("p2s2fdlist", 470, 513, 86, 115, CS.FeatureBack, CS.FeatureBorder));   // 45
            AddGraphic(new DeliverablesList("p2s2fclist", 470, 634, 86, 29, greenColor, greenBorder));          // 90

            // Fourth Stage Deliverables List
            AddGraphic(new DeliverablesList("p2s2fdlist", 562, 513, 86, 115, CS.FeatureBack, CS.FeatureBorder));   // 45
            AddGraphic(new DeliverablesList("p2s2fclist", 562, 634, 86, 29, greenColor, greenBorder));          // 90

            // Fifth Stage Deliverables List
            AddGraphic(new DeliverablesList("p2s2fdlist", 655, 513, 86, 115, CS.FeatureBack, CS.FeatureBorder));   // 45
            AddGraphic(new DeliverablesList("p2s2fclist", 655, 634, 86, 29, greenColor, greenBorder));          // 90
        }

        public override void AddDeliveryContainers()
        {
            AddDeliverablesHeadings();
            AddProductDeliveryContainers();
            //AddFeatureDeliveryContainers();
        }

        public override void AddImages()
        {
            base.AddImages();

            fGearBox = new GearBox("gearbox", 49, 593, 166, 115);
            AddGraphic(fGearBox);
        }

        // Here we'll add the legend and various other misfit graphics
        public override void AddLegend()
        {
            uint blueColor = RGBColor.RGB(204, 221, 255);
            uint blueBorder = RGBColor.RGB(119, 153, 238);
            uint blueText = RGBColor.RGB(51, 102, 204);

            uint orangeColor = RGBColor.RGB(255, 205, 170);
            uint orangeBorder = RGBColor.RGB(204, 51, 0);
            uint orangeText = RGBColor.RGB(153, 51, 0);


            uint greenColor = RGBColor.RGB(221, 255, 204);
            uint greenBorder = RGBColor.RGB(102, 204, 51);
            uint greenText = RGBColor.RGB(17, 102, 51);

            uint yellowColor = RGBColor.RGB(255, 221, 102);
            uint yellowBorder = RGBColor.RGB(255, 204, 0);
            uint yellowText = RGBColor.RGB(204, 153, 0);

            GraphicGroup legendBox = new GraphicGroup("legendbox", 63, 528, 135, 41);
            legendBox.GraphicsUnit = GraphicsUnit;

            legendBox.AddGraphic(new GradientRectangle(0, 0, 12, 12, RGBColor.White, yellowColor, yellowBorder, 90));
            legendBox.AddGraphic(new GradientRectangle(0, 21, 12, 12, RGBColor.White, orangeColor, orangeBorder, 90));
            legendBox.AddGraphic(new GradientRectangle(0, 41, 12, 12, RGBColor.White, greenColor, greenBorder, 45));

            legendBox.AddGraphic(new TextBox("Phases", "Tahoma", 8, GDIFont.FontStyle.Regular, 23, 0, 115, 12, StringAlignment.Left, StringAlignment.Center, yellowText, null));
            legendBox.AddGraphic(new TextBox("Stages", "Tahoma", 8, GDIFont.FontStyle.Regular, 23, 21, 115, 12, StringAlignment.Left, StringAlignment.Center, orangeText, null));
            legendBox.AddGraphic(new TextBox("Checkpoints", "Tahoma", 8, GDIFont.FontStyle.Regular, 23, 41, 115, 12, StringAlignment.Left, StringAlignment.Center, greenText, null));

            AddGraphic(legendBox);

            // Add the various accent lines
            //AddGraphic(new GraphicLine("gline", 65, 466, 235, 466, yellowBorder, 2));
            //AddGraphic(new GraphicLine("gline", 285, 699, 740, 699, yellowBorder, 2));
            //AddGraphic(new GraphicLine("gline", 797, 466, 964, 466, yellowBorder, 2));


        }

		
	}
}