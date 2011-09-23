using System;
using Papyri;

namespace PLC
{
	/// <summary>
	/// This control is meant to hold a list of deliverables.
	/// It is simply a box with some specialized text pushbuttons in it.
	/// </summary>
	public class DeliverableControl : PLCBaseControl
	{
		DeliverablesList fDeliverablesList;
		CheckpointDeliverablesList fCheckpointList;

		public DeliverableControl(string name, int x, int y, int width, int height,uint bkColor)
			: base(name, x, y, width, height)
		{
			LayoutHandler = new VFlowLayout(this, 4, 2);
			//Debug = true;

			fDeliverablesList = new DeliverablesList("deliverables", 0, 0, width, 250, bkColor,45);
			fCheckpointList = new CheckpointDeliverablesList("checkpoints", 0, 204, width, 50);

			AddChild(fDeliverablesList);
			AddChild(fCheckpointList);
		}

		public void AddDeliverable(string deliverable)
		{
			fDeliverablesList.AddDeliverable(deliverable);
		}

		public void AddCheckPoint(string checkpoint)
		{
			fCheckpointList.AddDeliverable(checkpoint);
		}
	}
}