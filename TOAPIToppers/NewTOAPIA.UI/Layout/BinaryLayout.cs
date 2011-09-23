using System;

namespace NewTOAPIA.UI
{
    using NewTOAPIA.Graphics;

    public class BinaryLayout : LayoutManager
    {
		IGraphic fPrimaryGraphic;
		IGraphic fSecondaryGraphic;
		int fMargin;
		Position fPosition;

        public BinaryLayout(Position aPos, int gap)
		{
			fPrimaryGraphic = null;
			fSecondaryGraphic = null;
			fMargin = gap;
			fPosition = aPos;
        }

		public override void ResetLayout()
		{
			fPrimaryGraphic = null;
			fSecondaryGraphic = null;
		}

		public override void AddToLayout(IGraphic g)
		{
			if (fSecondaryGraphic == null)
			{
				if (fPrimaryGraphic == null)
					fPrimaryGraphic = g;
				else
					fSecondaryGraphic = g;
			}

			// If we still don't have a secondary graphic,
			// then just return without doing anything.
			if (fSecondaryGraphic == null)
				return ;
		
			RectangleI frame = fPrimaryGraphic.Frame;

			int left = frame.Left;
            int right = frame.Right;
            int top = frame.Top;
            int bottom = frame.Bottom;
            int midx = (right - left + 1) / 2;
            int midy = (bottom - top + 1) / 2;
            int graphicwidth = fSecondaryGraphic.Frame.Width;
            int graphicheight = fSecondaryGraphic.Frame.Height;
            int xpos = 0;
            int ypos = 0;
	
			switch (fPosition)
			{
				case Position.Center:
					xpos = midx-graphicwidth/2;
					ypos = midy-graphicheight/2;
				break;
		
				case Position.Left:
					xpos = left - graphicwidth - fMargin;
					ypos = midy-graphicheight/2;
				break;
		
				case Position.Top:
					xpos = midx-graphicwidth/2;
					ypos = top - graphicheight - fMargin;
				break;
		
				case Position.Right:
					xpos = right + fMargin;
					ypos = midy-graphicheight/2;
				break;
		
				case Position.Bottom:
					xpos = midx-graphicwidth/2;
					ypos = bottom + fMargin;
				break;
		
				case Position.TopLeft:
					xpos = left - graphicwidth - fMargin;
					ypos = top - graphicheight - fMargin;
				break;
		
				case Position.TopRight:
					xpos = right + fMargin;
					ypos = top - graphicheight - fMargin;
				break;
		
				case Position.BottomLeft:
					xpos = left - graphicwidth - fMargin;
					ypos = bottom + fMargin;
				break;
		
				case Position.BottomRight:
					xpos = right + fMargin;
					ypos = bottom + fMargin;
				break;
			}
	
			// Move the secondary graphic to its new position
			fSecondaryGraphic.MoveTo(xpos, ypos);
		}
    }
}
