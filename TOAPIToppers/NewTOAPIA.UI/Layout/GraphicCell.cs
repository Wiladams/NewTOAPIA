using System;

namespace NewTOAPIA.UI
{
    using NewTOAPIA.Graphics;

    public class GraphicCell : LayoutManager
    {
		private IGraphic fWrappedGraphic;
		private Position fPosition;
		private int fMargin;

        public GraphicCell(Position aPosition, int margin)
            : base(null, RectangleI.Empty)
        {
            fPosition = aPosition;
            fMargin = margin;
            fWrappedGraphic = null;
        }

        public GraphicCell(Position aPosition, int margin, RectangleI frame)
			:base(null,frame)
		{
			fPosition = aPosition;
			fMargin = margin;
			fWrappedGraphic = null;
        }

		// From ILayoutManager
		public override void ResetLayout()
		{
			fWrappedGraphic = null;
		}

		public override void AddToLayout(IGraphic g)
		{
			fWrappedGraphic = g;
			if (fWrappedGraphic == null)
			{
				return ;
			}

			int xpos = Frame.Left;
            int ypos = Frame.Top;
            int width = Frame.Width;
            int height = Frame.Height;
            int midx = Frame.Left + width / 2;
            int midy = Frame.Top + height / 2;

            int graphicwidth = fWrappedGraphic.Frame.Width;
            int graphicheight = fWrappedGraphic.Frame.Height;

			switch (fPosition)
			{
				case Position.Center:
					xpos = midx-graphicwidth/2;
					ypos = midy-graphicheight/2;
				break;
		
				case Position.Left:
					xpos = fMargin + Frame.Left;
					ypos = midy-graphicheight/2;
				break;
		
				case Position.Top:
					xpos = midx-graphicwidth/2;
					ypos = fMargin + Frame.Top;
				break;
		
				case Position.Right:
					xpos = Frame.Right-graphicwidth - fMargin;
					ypos = midy-graphicheight/2;
				break;
		
				case Position.Bottom:
					xpos = midx-graphicwidth/2;
					ypos = Frame.Bottom-graphicheight - fMargin;
				break;
		
				case Position.TopLeft:
					xpos = fMargin + Frame.Left;
					ypos = fMargin + Frame.Top;
				break;
		
				case Position.TopRight:
					xpos = Frame.Right-graphicwidth - fMargin;
					ypos = Frame.Top + fMargin;
				break;
		
				case Position.BottomLeft:
					xpos = fMargin + Frame.Left;
					ypos = Frame.Bottom-graphicheight - fMargin;
				break;
		
				case Position.BottomRight:
					xpos = Frame.Right-graphicwidth - fMargin;
					ypos = Frame.Bottom-graphicheight -fMargin;
				break;
			}

			fWrappedGraphic.MoveTo(xpos, ypos);
		}
    }
}
