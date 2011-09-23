using System;

using NewTOAPIA.UI;

namespace PLC
{
	/// <summary>
	/// Summary description for Class1
	/// </summary>
	public class PLCBaseControl : ActiveArea
	{
		public PLCBaseControl(string name, int x, int y, int width, int height)
			: base(name, x, y, width, height)
		{
		}


		public override void OnMouseEnter(MouseActivityArgs e)
		{
			Console.WriteLine("PLCBaseControl.MouseEnter");
		}

		public override void OnMouseLeave(MouseActivityArgs e)
		{
			Console.WriteLine("PLCBaseControl.MouseLeave");
		}

		// Graphic hierarchy
		public override void OnGraphicAdded(IGraphic aGraphic)
		{
			// Layout the graphic again
			LayoutHandler.AddToLayout(aGraphic);
		}
	}
}
