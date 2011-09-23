using System;

using NewTOAPIA.UI;

public class HFlowLayout : LayoutManager
{
	int fGap;
	int fMargin;
	int fCurrentX;

	public HFlowLayout(GraphicGroup gg, int gap, int margin)
		: base(gg,gg.Frame)
	{
		fGap = gap;
		fMargin = margin;
		fCurrentX = fMargin;
	}

	public override void AddToLayout(IGraphic trans)
	{
		int cWidth = Frame.Width;
		int cHeight = Frame.Height;
		int top = Frame.Top;
		int fixedWidth;
		int numElements = Container.CountGraphics();

		fixedWidth = (cWidth - (fMargin * 2) - ((numElements-1) * fGap)) / (numElements);

		fCurrentX = fMargin;

		foreach (IGraphic g in Container.GraphicChildren)
		{
			g.ResizeTo(fixedWidth, g.Frame.Height);
			g.MoveTo(fCurrentX, top);

			fCurrentX = g.Frame.Right + fGap+1;
		}
	}
}
