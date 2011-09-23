using System;
using System.Text;
using System.Drawing;
using System.Xml;

using NewTOAPIA;
using NewTOAPIA.Drawing;
using NewTOAPIA.UI;

using PLC;

/// <summary>
/// This graphic group contains all the little elements that make up a little IT 
/// phase.
/// </summary>
public class HardwarePhaseControl : GraphicGroup
{
	PLCColorScheme fCS;
	Rectangle fBaseFrame;
	Rectangle fExpandedFrame;
	bool fIsExpanded;
	GraphicArrow fBigArrow;

	public HardwarePhaseControl(string name, int phase, string phaseName, string deliverableName, string milestoneName, string checkpointName, int x, int y)
		:base(name,x,y,173,468)
	{
		fCS = new PLCColorScheme();

		switch (phase)
		{
			case 1:
			case 2:
			case 3:
			case 4:
				break;
			default:
				break;
		}

		fBaseFrame = new Rectangle(x, y, 173, 468);
		fExpandedFrame = new Rectangle(x, y, 400, 468);
		fIsExpanded = false;

		//GraphicsUnit = GraphicsUnit.Inch;

		// Big arrow
		fBigArrow=new GraphicArrow("bigarrow", 0, 0, 173, 468);
		AddGraphic(fBigArrow);

		// Heading
		HeadingButton heading = new HeadingButton(phaseName, "Tahoma", 11, GDIFont.FontStyle.Bold, 6, 36, 118, 35, StringAlignment.Center, StringAlignment.Center, fCS.HeaderText,
			new GradientRectangle(0, 0, 118, 35, RGBColor.White, fCS.HeaderBack, fCS.HeaderBorder, 90));
		heading.MouseUpEvent +=new EventHandler(HeadingClicked);
		AddGraphic(heading);

		// Production Phase Deliverables List
		// The deliverables, milestones, checkpoints
		AddGraphic(new DeliverablesList(deliverableName, 6, 85, 117, 152, fCS.ProductBack, fCS.ProductBorder, 45));
		AddGraphic(new DeliverablesList(milestoneName, 6, 255, 117, 119, fCS.MilestoneBack, fCS.MilestoneBorder, 45));
		AddGraphic(new DeliverablesList(checkpointName, 6, 391, 117, 40, fCS.ChecklistBack, fCS.ChecklistBorder, 90));
	}

    protected override void  OnUpdateGeometryState()
	{
		// Recalculate and reposition things according to our size
		if (fIsExpanded)
		{
			fBigArrow.FillColor = fCS.ArrowEditing;
			fBigArrow.BorderColor = fCS.ArrowEditingBorder;
			fBigArrow.Frame = new Rectangle(0, 0, fExpandedFrame.Width, fExpandedFrame.Height);
		}
		else
		{
			fBigArrow.FillColor = fCS.ArrowRegular;
			fBigArrow.BorderColor = fCS.ArrowRegularBorder;
			fBigArrow.Frame = new Rectangle(0, 0, fBaseFrame.Width, fBaseFrame.Height);
		}
	}


	public virtual void Shrink()
	{
		fIsExpanded = false;
		Frame = fBaseFrame;
	}

	public virtual Rectangle ExpansionFrame
	{
		get { return fExpandedFrame; }
		set { fExpandedFrame = value; }
	}

	public virtual void Expand()
	{
		fIsExpanded = true;
		Frame = fExpandedFrame;
	}

	public void HeadingClicked(object sender, EventArgs e)
	{
		//Console.WriteLine("ITPhaseControl.HeadingClicked: {0}",Name);
		//Container.MoveGraphicToFront(this);
	}
}
