using System;
using Papyrus;
using System.Drawing;

namespace PLC
{
	/// <summary>
	/// This graphic basically shows the phase title.
	/// It is clickable so that actions can occur once it is clicked on.
	/// </summary>
	public class PLCDeliverablesHeading : TextBox
	{
		public PLCDeliverablesHeading(string title, float x, float y, float width, float height, Color bkColor,Color txtColor)
			: base(title, "Tahoma", 11, x, y, width, height, new DrawableGradientRectangle(x, y, width, height,
				Color.FromArgb(255, 255, 255), bkColor, Color.FromArgb(119, 153, 238), 90))
		{
		}
	}
}
