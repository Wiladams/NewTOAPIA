using System;
using System.Drawing;
using NewTOAPIA.Drawing;
using NewTOAPIA.UI;

namespace PLC
{
    using NewTOAPIA;

	/// <summary>
	/// Summary description for HardwareLegend.
	/// </summary>
	public class ITLegend : ActiveArea
	{
		public ITLegend()
			:base("legendbox",630, 590, 135, 83)
		{
			PLCColorScheme CS = new PLCColorScheme();

			GraphicsUnit = GraphicsUnit.Inch;

			AddGraphic(new GradientRectangle(0, 0, 12, 12, RGBColor.White, CS.HeaderBack, CS.HeaderBorder, 90));
            AddGraphic(new GradientRectangle(0, 21, 12, 12, RGBColor.White, CS.ProductBack, CS.ProductBorder, 90));
            AddGraphic(new GradientRectangle(0, 41, 12, 12, RGBColor.White, CS.MilestoneBack, CS.MilestoneBorder, 45));
            AddGraphic(new GradientRectangle(0, 61, 12, 12, RGBColor.White, CS.ChecklistBack, CS.ChecklistBorder, 45));

			AddGraphic(new TextBox("Phases", "Tahoma", 8, GDIFont.FontStyle.Regular, 21, 0, 115, 12, StringAlignment.Near, StringAlignment.Center, CS.HeaderText, null));
			AddGraphic(new TextBox("Deliverables", "Tahoma", 8, GDIFont.FontStyle.Regular, 21, 21, 115, 12, StringAlignment.Near, StringAlignment.Center, CS.ProductText, null));
			AddGraphic(new TextBox("Interim Milestones", "Tahoma", 8, GDIFont.FontStyle.Regular, 21, 41, 115, 12, StringAlignment.Near, StringAlignment.Center, CS.MilestoneText, null));
			AddGraphic(new TextBox("Checkpoints", "Tahoma", 8, GDIFont.FontStyle.Regular, 21, 61, 115, 12, StringAlignment.Near, StringAlignment.Center, CS.ChecklistText, null));
		}
	}
}
