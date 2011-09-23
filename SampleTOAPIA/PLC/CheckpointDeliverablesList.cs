using System;
using Papyrus;
using System.Drawing;

namespace PLC
{
	/// <summary>
	/// This control is meant to hold a list of deliverables.
	/// It is simply a box with some specialized text pushbuttons in it.
	/// </summary>
	public class CheckpointDeliverablesList : PLCBaseControl
	{
		DrawableGradientRectangle fBackgroundRect;

		public CheckpointDeliverablesList(string name, int x, int y, int width, int height)
			: base(name, x, y, width, height)
		{
			fBackgroundRect = new DrawableGradientRectangle(x, y, width, height, 
				Color.White, Color.FromArgb(221,255,204) ,Color.FromArgb(102, 204, 51), 90);

			LayoutHandler = new LinearLayout(this, 4, 2, Orientation.Vertical);
		}

		public void AddDeliverable(string deliverable)
		{
			AddGraphic(new StringLabel(deliverable, "Tahoma", 12, 0, 0));
		}

		public override void OnResized(float dw, float dh)
		{
			base.OnResized(dw, dh);
			fBackgroundRect.Dimension = new SizeF(dw, dh);
		}

		// We override this one because we want to be able to draw the
		// pretty background
		public override void DrawBackground(Graphics graphPort)
		{
			fBackgroundRect.DrawInto(graphPort);
			//graphPort.DrawRectangle(new Pen(Color.FromArgb(102, 204, 51)), Left, Top, Width, Height);
		}
	}
}
