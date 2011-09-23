using System;
using Papyri;

namespace PLC
{
	/// <summary>
	/// The PLCPhaseControl graphic is meant to represent an entire
	/// phase.  It is really the base class to represent the various
	/// phase subclasses that may exist.
	/// 
	/// This class knows how to draw itself as that big arrow with the 
	/// fancy title at the top.
	/// </summary>
	public class PLCPhaseControl : ActiveArea
	{
		string fTitle;
		PLCPhaseHeading fPhaseHeading;
		PLCStageControl fStageControl;

		public PLCPhaseControl(string title, string stageTitle, int width, int height, bool swappable)
			:base("PLCPhaseControl",0,0,width,height)
		{
			fTitle = title;

			LayoutHandler = new LinearLayout(this, 10, 10, Orientation.Vertical);

			// Title for the Phase
			fPhaseHeading = new PLCPhaseHeading(title, 0, 0, width,180);

			fStageControl = new PLCStageControl(stageTitle, 0, 0, width, height,swappable);


			// Add the controls to the window.  They will be layed out automatically
			AddChild(fPhaseHeading);
			AddChild(fStageControl);
		}

		public void AddStage(string stageName)
		{
			fStageControl.AddStage(stageName);
		}

		public void AddProductDeliverable(string deliverable)
		{
			fStageControl.AddProductDeliverable(deliverable);
		}

		public void AddFeatureDeliverable(string deliverable)
		{
			fStageControl.AddFeatureDeliverable(deliverable);
		}
	}
	
}
