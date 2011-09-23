using System;

using NewTOAPIA.Drawing;

namespace NewTOAPIA.UI
{

	public class GrayTrack : Graphic
	{
		public GrayTrack(string name, int x, int y, int width, int height)
			: base(name, x,y,width,height)
		{
		}

		// We override this one because we want to be able to draw the
		// background, raised or sunken, depending on our state
		public override void DrawBackground(DrawEvent devent)
		{
			GUIStyle.Default.DrawSunkenRect(devent.GraphPort, (int)Origin.x, (int)Origin.y, Frame.Width, Frame.Height);
		}
	}
}