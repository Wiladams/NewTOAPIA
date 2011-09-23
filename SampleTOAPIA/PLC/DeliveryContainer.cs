using System;
using Papyrus;
using System.Drawing;

namespace PLC
{
	public class DeliveryContainer : GraphicContainer
	{
		TextBar fHeading;
		GraphicContainer fStageContainer;
		Color fBkColor;
		Color fTxtColor;

		public DeliveryContainer(string name, int x, int y, int width, int height, Color bkColor, Color txtColor)
			: base(name, x, y, width, height)
		{
			fBkColor = bkColor;
			fTxtColor = txtColor;

			LayoutHandler = new LinearLayout(this, 0, 0, Orientation.Vertical);

			// Add a deliveries heading to the container
			fHeading = new TextBar(name, 0, 0, width, 30, bkColor, txtColor);
			//heading.Debug = true;

			AddGraphic(fHeading);

			// This is the container that holds the individuals delivery lists
			// for the stages.
			fStageContainer = new GraphicContainer("stages", 0, 0, width, height - 34);
			fStageContainer.LayoutHandler = new HFlowLayout(fStageContainer, 10, 2);
			//fStageContainer.Debug = true;

			AddGraphic(fStageContainer);
		}

		public GraphicContainer StageContainer
		{
			get { return fStageContainer; }
		}
	}
}