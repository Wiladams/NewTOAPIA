using System;
using Papyri;

namespace PLC
{
	/// <summary>
	/// Summary description for Class1
	/// </summary>
	public class FeatureDeliverabelsControl : PLCBaseControl
	{
		BigArrow fArrowGraphic;

		public FeatureDeliverabelsControl(string name, int x, int y, int width, int height, Window window)
			: base(name, x, y, width, height, window)
		{

			// We want the arrow centered, then other controls will be added
			// to the arrow control.
			LayoutHandler = new GraphicCell(Position.Center, 2);

			// Create the big arrow
			fArrowGraphic = new BigArrow(name, x, y, width, height, window);

			// Add some deliveries to the big arrow
			RectangleI r = fArrowGraphic.ClientArea;

			DeliverableControl deliverable = new DeliverableControl("deliverable", 0, 0, r.Width, 250, window);
			deliverable.AddDeliverable("Value Proposition Vision");
			//deliverable.AddDeliverable("Value Proposition Vision Document");
			deliverable.AddDeliverable("Integrated Comm. Plan");
			//deliverable.AddDeliverable("Engineering Release Vision Memo");
			//deliverable.AddDeliverable("Release Requirements Document");
			deliverable.AddCheckPoint("Requirements");
			deliverable.AddCheckPoint("Sign-off");

			fArrowGraphic.AddDeliverables(deliverable);

			// Add the arrow to this graphic
			AddChild(fArrowGraphic);
		}

		public RectangleI ClientArea
		{
			get
			{
				return fArrowGraphic.ClientArea;
			}
		}
	}
}