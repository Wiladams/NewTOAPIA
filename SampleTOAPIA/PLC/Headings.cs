using System;

using NewTOAPIA.Drawing;

namespace PLC
{
    using NewTOAPIA;

	/// <summary>
	/// This graphic basically shows the phase title.
	/// It is clickable so that actions can occur once it is clicked on.
	/// </summary>

	public class GradientTextBar : TextBox
	{
		public GradientTextBar(string title, int x, int y, int width, int height, uint bkColor, uint borderColor, uint txtColor)
			: base(title, "Tahoma", 11, GDIFont.FontStyle.Bold, x, y, width, height, txtColor)
		{
            this.Background = new GradientRectangle(0, 0, width, height, RGBColor.RGB(255, 255, 255), bkColor, borderColor, 90);
            //Debug = true;
		}
	}
}