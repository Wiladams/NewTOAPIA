using System;

using NewTOAPIA.Drawing;

namespace NewTOAPIA.UI
{
    using NewTOAPIA.Graphics;

	public class PushButton : ActiveArea
	{
		IGraphic fLabel;

        public PushButton(string name, RectangleI frame, IGraphic label)
            : this(name, frame.Left, frame.Top, frame.Width, frame.Height, label)
        {
        }
        
        public PushButton(string name, int x, int y, int width, int height, IGraphic label)
			: base(name, x,y,width,height)
		{
            Enabled = true;
            GraphicCell layout = new GraphicCell(Position.Center, 2);
            layout.Frame = new RectangleI(0, 0, width, height);
            LayoutHandler = layout;

			fLabel = label;
			AddGraphic(fLabel,null);
		}


		// We override this one because we want to be able to draw the
		// background, raised or sunken, depending on our state
        public override void DrawBackground(DrawEvent devent)
		{
			if (Depressed && Tracking)
				GUIStyle.Default.DrawSunkenRect(devent.GraphPort, (int)Origin.x, (int)Origin.y, Frame.Width, Frame.Height);
			else
				GUIStyle.Default.DrawRaisedRect(devent.GraphPort, (int)Origin.x, (int)Origin.y, Frame.Width, Frame.Height);
		}
	}
}