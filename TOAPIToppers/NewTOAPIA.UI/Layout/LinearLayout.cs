
namespace NewTOAPIA.UI
{
	public class LinearLayout :LayoutManager
	{
        int fGap;
        int fMargin;
        int fTallest;
        int fWidest;
		Orientation	fOrientation;

		protected int fCurrentX;
        protected int fCurrentY;
        protected int fNumElements;

		public LinearLayout(IGraphicGroup gg, int gap, int margin, Orientation orient)
			: base(gg,gg.ClientRectangle)
		{
			fGap = gap;
			fMargin = margin;
			fOrientation = orient;

			fCurrentX = Frame.Left;
			fCurrentY = Frame.Top;
		}

		public override void ResetLayout()
		{
			// Reset the layout for incremental adding
			fCurrentX = Frame.Left;
			fCurrentY = Frame.Top;
			fTallest = 0;
			fWidest = 0;
			fNumElements = 0;
		}

		public override void AddToLayout(IGraphic trans)
		{
            int size;
			int height;
            int width;

			height = trans.Frame.Height;
			width = trans.Frame.Width;

			switch (fOrientation)
			{
				case Orientation.Horizontal:
					if (fNumElements == 0)
						fCurrentX = fCurrentX + fMargin;
					size = width;
					if (height > fTallest)
						fTallest = height;
					//trans.MoveTo(fCurrentX,fCurrentY);
					trans.MoveTo(fCurrentX,trans.Frame.Top);
					
					// Adjust widest, tallest, and new position
					fWidest = fCurrentX+size+1;	
					fCurrentX += size+1 + fGap;
				break;

				case Orientation.Vertical:
					if (fNumElements == 0)
						fCurrentY = fCurrentY + fMargin;
					size = height;
					if (width > fWidest)
						fWidest = width;
					trans.MoveTo(fCurrentX,fCurrentY);
					fTallest = fCurrentY + size+1;
					fCurrentY += size+1 + fGap;
				break;
			}

			fNumElements++;		
		}
	}

/*
	public class HFlowLayout :LayoutManager
	{
		public HFlowLayout(GraphicGroup gg, int gap, int margin, Orientation orient)
			: base(gg, gap, margin, orient)
		{
		}

		public override void AddToLayout(IGraphic trans)
		{
			if (fNumElements == 0)
				fCurrentX = fMargin;

			int size = trans.Frame.Width;
			int height = trans.Frame.Height;
	
			trans.MoveTo(fCurrentX,0);

			// Adjust widest, tallest, and new position
			if (height > fTallest)
				fTallest = height;
			fWidest = fCurrentX+size+1;	
			fCurrentX += size+1 + fGap;

			fElements++;		
		}
	}

	public class VFlowLayout :LayoutManager
	{
		public VFlowLayout(GraphicGroup gg, int gap, int margin, Orientation orient)
			: base(gg, gap, margin, orient)
		{
		}

		public override void AddToLayout(IGraphic trans)
		{
			if (fElements == 0)
				fCurrentY = fMargin;

			int size = trans.Frame.Height;
			int width = trans.Frame.Width;

			trans.MoveTo(fCurrentX,fCurrentY);
			if (width > fWidest)
				fWidest = width;
			fTallest = fCurrentY + size+1;
			fCurrentY += size+1 + fGap;
		
			fElements++;
		}
	}
*/
}