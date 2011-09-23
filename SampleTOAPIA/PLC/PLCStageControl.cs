using System;
using Papyri;

//
// The stage control does not display anything by default.  Individual 
// stages need to be added to it before anything is displayed.
//
namespace PLC
{
	/// <summary>
	/// Summary description for Class1
	/// </summary>
	public class PLCStageControl : PLCBaseControl
	{
		PLCStageDeliveryControl fStageDeliveryControl;
		PLCStageBar fStageBar;
		
		public PLCStageControl(string name, int x, int y, int width, int height, bool swappable)
			:base("PLCStageControl", x, y, width, height)
		{
			LayoutHandler = new LinearLayout(this, 10, 2, Orientation.Vertical);

			fStageDeliveryControl = new PLCStageDeliveryControl(name, 0, 0, width, 410,swappable);
			RectangleI rect = fStageDeliveryControl.ClientArea;

			fStageBar = new PLCStageBar("stagebar", 0, 0, rect.Width, 80);


			AddChild(fStageBar);
			AddChild(fStageDeliveryControl);
		}

		public void AddStage(string name)
		{
			fStageDeliveryControl.AddStage(name);
		}

		public void AddProductDeliverable(string deliverable)
		{
			fStageDeliveryControl.AddProductDeliverable(deliverable);
		}

		public void AddFeatureDeliverable(string deliverable)
		{
			fStageDeliveryControl.AddFeatureDeliverable(deliverable);
		}

	}
}