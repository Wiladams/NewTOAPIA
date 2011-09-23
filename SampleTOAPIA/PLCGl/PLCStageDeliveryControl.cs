using System;
using Papyri;

namespace PLC
{
	/// <summary>
	/// Summary description for Class1
	/// </summary>
	public class PLCStageDeliveryControl : PLCBaseControl
	{
		DeliveryContainer fProductContainer;
		DeliveryContainer fFeatureContainer;
		DeliveryContainer fDisplayedContainer;
		DeliveryContainer fNonDisplayedContainer;

		GraphicArrow fBigArrow;
		DeliverableControl fCurrentProdCtl;
		DeliverableControl fCurrentFeatureCtl;
		bool fSwappable;

		public PLCStageDeliveryControl(string name, int x, int y, int width, int height, bool swappable)
			:base(name, x, y, width, height)
		{
			//Debug = true;

			fSwappable = swappable;

			// Create the big arrow
			fBigArrow = new GraphicArrow("graphicArrow", 0, 0, width, height);

			RectangleI ca = ClientArea;

			// Create the container that holds all the stage stuff
			fProductContainer = new DeliveryContainer(name, ca.Left, ca.Top, ca.Width, ca.Height, RGBColor.RGB(204, 221, 255), RGBColor.RGB(51, 102, 204));
			fFeatureContainer = new DeliveryContainer("Feature Deliverables", ca.Left, ca.Top, ca.Width, ca.Height, RGBColor.RGB(238, 221, 255), RGBColor.RGB(136, 102, 153));
			fFeatureContainer.IsVisible = false;

			this.MouseUpEvent += new System.EventHandler(this.MouseActivity);
			//fProductContainer.MouseUpEvent += new System.EventHandler(this.MouseActivity);
			//fFeatureContainer.MouseUpEvent += new System.EventHandler(this.MouseActivity);

			// Finally, add the container to this graphic
			fDisplayedContainer = fProductContainer;
			fNonDisplayedContainer = fFeatureContainer;

			AddChild(fProductContainer);
			AddChild(fFeatureContainer);
		}


		private void SwapContainers()
		{
			// Create the two bitmaps to play with
			GDIBitmap srcBitmap = new GDIBitmap(fNonDisplayedContainer.Width, fNonDisplayedContainer.Height);
			GDIBitmap dstBitmap = new GDIBitmap(fDisplayedContainer.Width, fDisplayedContainer.Height);

			// Fill both bitmaps with the background gray color of the arrow so there
			// won't be a disconcerting flicker if the backgroud bitmap background
			// starts out as black or white.
			srcBitmap.ClearToColor(RGBColor.RGB(238, 238, 238));
			dstBitmap.ClearToColor(RGBColor.RGB(238, 238, 238));

			// Set the origin to be the Frame(topleft) of the graphic
			Point oldOrigin = new Point(fDisplayedContainer.Frame.Left, fDisplayedContainer.Frame.Top);
			fNonDisplayedContainer.MoveTo(0, 0);
			fDisplayedContainer.MoveTo(0, 0);

			// Draw the currently displayed thing into the destination bitmap
			fDisplayedContainer.DrawAt(dstBitmap.GraphDevice, 0, 0, fDisplayedContainer.Frame);


			// Draw the invisible one into its bitmap
			fNonDisplayedContainer.IsVisible = true;
			fNonDisplayedContainer.DrawAt(srcBitmap.GraphDevice, 0, 0, fNonDisplayedContainer.Frame);
			
			// At this point we have two bitmaps that hold the images of the 
			// two graphics we want to swap.

			// Move both graphics back into position back to where it was
			fNonDisplayedContainer.MoveTo(oldOrigin.X, oldOrigin.Y);
			fDisplayedContainer.MoveTo(oldOrigin.X, oldOrigin.y);

			// Fade the images into place bitmap into place
			AlphaFade.Run(LastGraphDevice, dstBitmap, srcBitmap,fDisplayedContainer.Frame, 50);

			// Mark the currently displayed container as invisible
			fDisplayedContainer.IsVisible = false;


			// swap the references
			DeliveryContainer tmpContainer;
			tmpContainer = fNonDisplayedContainer;
			fNonDisplayedContainer = fDisplayedContainer;
			fDisplayedContainer = tmpContainer;
			fDisplayedContainer.IsVisible = true;

			// Cleanup
			srcBitmap.Dispose();
			dstBitmap.Dispose();
		}

		private void MouseActivity(object sender, EventArgs e)
		{
			Console.WriteLine("StageDeliveryControl.MouseActivity: {0}", e.ToString());

			if (fSwappable)
				SwapContainers();
		}

		public RectangleI ClientArea
		{
			get
			{
				return fBigArrow.ClientArea;
			}
		}

		public void AddStage(string name)
		{
			fCurrentProdCtl = new DeliverableControl(name, 0, 0, 200, 250, RGBColor.RGB(204, 221, 255));
			fProductContainer.StageContainer.AddChild(fCurrentProdCtl);

			fCurrentFeatureCtl = new DeliverableControl(name, 0, 0, 200, 250, RGBColor.RGB(238, 221, 255));
			fFeatureContainer.StageContainer.AddChild(fCurrentFeatureCtl);
		}

		public void AddProductDeliverable(string deliverable)
		{
			fCurrentProdCtl.AddDeliverable(deliverable);
		}

		public void AddFeatureDeliverable(string deliverable)
		{
			fCurrentFeatureCtl.AddDeliverable(deliverable);
		}

		public override void DrawBackgroundAt(I2DGraphDevice graphPort, int x, int y, RectangleI updateRect)
		{
			fBigArrow.DrawAt(graphPort, Frame.Left, Frame.Top, updateRect);
		}
	}
}