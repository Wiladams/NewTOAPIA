using System;

using NewTOAPIA.UI;

public class VFlowLayout : LayoutManager
{
	int fGap;
	int fMargin;

	public VFlowLayout(GraphicGroup gg, int gap, int margin)
		: base(gg,gg.Frame)
	{
		fGap = gap;
		fMargin = margin;
	}


	public override void AddToLayout(IGraphic trans)
	{
		int cWidth = Frame.Width;
		int cHeight = Frame.Height;
		int left = Frame.Left;
		int numElements = Container.CountGraphics();


		int currentY = Frame.Top+fMargin;

		foreach (IGraphic g in Container.GraphicChildren)
		{
			g.ResizeTo(cWidth, g.Frame.Height);
			g.MoveTo(left, currentY);

			currentY = g.Frame.Bottom + fGap;
		}
	}
}

public class VEqualLayout : LayoutManager
{
	int fGap;
	int fMargin;
	bool fForceNormalization;

	public VEqualLayout(GraphicGroup gg, int gap, int margin)
		: base(gg,gg.Frame)
	{
		fForceNormalization = false;
		fGap = gap;
		fMargin = margin;
	}

	public bool ForcedNormalization
	{
		get { return fForceNormalization; }
		set { fForceNormalization = value; }
	}

	public override void AddToLayout(IGraphic trans)
	{
		int cWidth = Frame.Width;
		int cHeight = Frame.Height;
		int left = 0;	// WAA - Container.Origin.X;
		int numElements = Container.CountGraphics();
		int fixedHeight = (cHeight - ((numElements - 1) * fGap)) / (numElements);
		int collectiveHeight=0;
		bool normalize = false;

		// First figure out the collective height
		foreach (IGraphic g in Container.GraphicChildren)
			collectiveHeight += g.Frame.Height;
		collectiveHeight += fGap * (numElements - 1);

		if (collectiveHeight > cHeight)
			normalize = true;

		int currentY = 0; // WAA - Container.Origin.Y;

		foreach (IGraphic g in Container.GraphicChildren)
		{
			if (normalize)
			{
				g.ResizeTo(cWidth, fixedHeight);
			} else
			{
				g.ResizeTo(cWidth, g.Frame.Height);
			}

			g.MoveTo(left + fMargin, currentY);
			currentY = g.Frame.Bottom + fGap;
		}
	}
}



public class VTextLayout : LayoutManager
{
	int fGap;
	int fMargin;
	bool fForceNormalization;

	public VTextLayout(GraphicGroup gg, int gap, int margin)
		: base(gg,gg.Frame)
	{
		fForceNormalization = false;
		fGap = gap;
		fMargin = margin;
	}

	public bool ForcedNormalization
	{
		get { return fForceNormalization; }
		set { fForceNormalization = value; }
	}

	public override void AddToLayout(IGraphic trans)
	{
		int cWidth = Frame.Width;
		int cHeight = Frame.Height;
		int left = 0;	// WAA - Container.Origin.X;
		int numElements = Container.CountGraphics();
		int fixedHeight = (cHeight - ((numElements - 1) * fGap)) / (numElements);
		int collectiveHeight=0;
		bool normalize;

		// First figure out the collective height
		foreach (IGraphic g in Container.GraphicChildren)
			collectiveHeight += g.Frame.Height;
		collectiveHeight += fGap * (numElements - 1);

		if (collectiveHeight > cHeight)
			normalize = true;

		int currentY = 0; // WAA - Container.Origin.Y;

		foreach (IGraphic g in Container.GraphicChildren)
		{
			TextBox tBox = (TextBox)g;
			tBox.ResizeTo(cWidth, cHeight-currentY);
			
			// Get the preferred size
			System.Drawing.Size pSize = tBox.PreferredSize;
			g.ResizeTo(cWidth, pSize.Height);
			// Layout based on that
			/*
			if (normalize)
			{
				g.ResizeTo(cWidth, fixedHeight);
			} 
			else
			{
				g.ResizeTo(cWidth, g.Height);
			}
*/
			g.MoveTo(left + fMargin, currentY);
			currentY = g.Frame.Bottom + fGap;
		}
	}
}