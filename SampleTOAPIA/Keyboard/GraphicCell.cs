using System;
using System.Drawing;

using NewTOAPIA.UI;


    public class GraphicCell : LayoutManager
    {
		private IGraphic fWrappedGraphic;
		private Position fPosition;
		private int fMargin;

        public GraphicCell(Position aPosition, int margin)
            : base(null, Rectangle.Empty)
        {
            fPosition = aPosition;
            fMargin = margin;
            fWrappedGraphic = null;
        }

        public GraphicCell(Position aPosition, int margin, Rectangle frame)
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
					xpos = fMargin + midx-graphicwidth/2;
					ypos = fMargin + midy-graphicheight/2;
				break;
		
				case Position.Left:
					xpos = fMargin;
					ypos = midy-graphicheight/2;
				break;
		
				case Position.Top:
					xpos = midx-graphicwidth/2;
					ypos = fMargin;
				break;
		
				case Position.Right:
					xpos = Frame.Width-graphicwidth - fMargin;
					ypos = midy-graphicheight/2;
				break;
		
				case Position.Bottom:
					xpos = midx-graphicwidth/2;
					ypos = Frame.Height-graphicheight - fMargin;
				break;
		
				case Position.TopLeft:
					xpos = fMargin;
					ypos = fMargin;
				break;
		
				case Position.TopRight:
					xpos = Frame.Width-graphicwidth - fMargin;
					ypos = fMargin;
				break;
		
				case Position.BottomLeft:
					xpos = fMargin;
					ypos = Frame.Height-graphicheight - fMargin;
				break;
		
				case Position.BottomRight:
					xpos = Frame.Width-graphicwidth - fMargin;
					ypos = Frame.Height-graphicheight -fMargin;
				break;
			}

			fWrappedGraphic.MoveTo(xpos, ypos);
		}
    }

