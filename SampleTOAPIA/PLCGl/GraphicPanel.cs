using System;
using System.Collections.Generic;

using NewTOAPIA.Drawing;
using NewTOAPIA.UI;
using TOAPI.Types;


public class GraphicPanel : ActiveArea
{
	ActiveArea fRootGraphic;
	Graphic fMouseTrackerGraphic = null;
	IGraphic fMouseTracker = null;
	float fZoomFactor;
	System.Drawing.Size fOrgSize;

	public GraphicPanel(string title, int x, int y, int width, int height)
		:base(title, x,y,width,height)
	{
		fOrgSize = new System.Drawing.Size(width,height);

		fZoomFactor = 1;
		fRootGraphic = new ActiveArea(title, 0, 0, width, height);
		//fRootGraphic.GraphicsUnit = GraphicsUnit.Inch;

		//fRootGraphic.Window = this;

		fMouseTracker = null;
		fMouseTrackerGraphic = null;
	}

	public float ZoomFactor
	{
		get {return fZoomFactor;}
		set 
		{
			fZoomFactor = value;
			//this.Scale(fZoomFactor);
			//this.Size = new Size((int)(fOrgSize.Width*fZoomFactor), (int)(fOrgSize.Height*fZoomFactor));

			Invalidate();
		}
	}



	public GraphicElement MouseTrackerAt(int x, int y)
	{
		System.Drawing.Point[] pts = new System.Drawing.Point[] { new System.Drawing.Point(x, y) };

		// We need to convert from the device space to the page space
		// The Graphics.TransformPoints() method does the trick.
        //Graphics grfx = this.CreateGraphics();	// Graphics on the screen
        //grfx.PageUnit = RootGraphic.GraphicsUnit;
        //grfx.PageScale = fZoomFactor;
        //grfx.TransformPoints(CoordinateSpace.Page, CoordinateSpace.Device, pts);
        //grfx.Dispose();
			

		// Since we are the window, and the client area receives
		// mouse points relative to an origin of 0,0, there's nothing
		// else to do but return this converted point.
		int localX = pts[0].X;
		int localY = pts[0].Y;


		// Now, we finally go searching for graphics.  We have
		// the hitpoint in the coordinate space and units of the 
		// root graphic, so away we go.
		Stack<IGraphic> graphicStack = new Stack<IGraphic>();
		GraphicsAt(localX,localY, ref graphicStack);

		foreach (GraphicElement g in graphicStack)
		{
			if (g.fGraphic is IInteractor)
				return g;
		}

		return null;
	}

    public override void OnMouseEnter(MouseActivityArgs e) 
	{
		//Console.WriteLine("GraphicWindow.OnMouseEnter - {0}", e.ToString());
		//Win32Cursor.Current = Win32Cursor.Arrow;
	}

	public override void OnMouseDown(MouseActivityArgs e) 
	{
		//Console.WriteLine("GraphicWindow.OnMouseDown - {0}", e.ToString());
		GraphicElement trackerElement = MouseTrackerAt(e.X, e.Y);
		IGraphic trackerGraphic = trackerElement.fGraphic;
		IGraphic tracker = (IGraphic)trackerGraphic;

		if (trackerGraphic != null)
		{
			fMouseTracker = tracker;
			fMouseTrackerGraphic = (Graphic)trackerGraphic;

			if (tracker != null)
			{
				//MouseEventArgs args = new MouseEventArgs(e.Button, e.Clicks,
				//	trackerElement.fLocation.X, trackerElement.fLocation.X, e.Delta);
				tracker.OnMouseDown(e); 
			}
		} 
		else
		{
			//Console.WriteLine("GraphicWindow.OnMouseDown - no tracker found");
			fMouseTracker = null;
			fMouseTrackerGraphic = null;
		}
	}

	public override void OnMouseMove(MouseActivityArgs e) 
	{
		//Console.WriteLine("GraphicWindow.OnMouseDown - {0}", e.ToString());
		GraphicElement trackerElement = MouseTrackerAt(e.X, e.Y);
		IGraphic trackerGraphic = trackerElement.fGraphic;
		IGraphic tracker = (IGraphic)trackerGraphic;

		if (trackerGraphic != null)
		{
			fMouseTracker = tracker;
			fMouseTrackerGraphic = (Graphic)trackerGraphic;

			if (tracker != null)
			{
				//MouseEventArgs args = new MouseEventArgs(e.Button, e.Clicks,
				//	trackerElement.fLocation.X, trackerElement.fLocation.X, e.Delta);
				tracker.OnMouseMove(e);
			}
		}
		else
		{
			//Console.WriteLine("GraphicWindow.OnMouseDown - no tracker found");
			fMouseTracker = null;
			fMouseTrackerGraphic = null;
		}

		/*
			//Console.WriteLine("GraphicWindow.OnMouseMove - {0}", e.ToString());

			// There's a little bit of work to do here.
			// Find out who the current tracker is
			GraphicElement element = MouseTrackerAt(e.X, e.Y);
			Graphic trackerGraphic = (Graphic)element.fGraphic;
			IMouseTracker tracker = (IMouseTracker)trackerGraphic;

			// If the current tracker is the same as who we've
			// already been tracking
			// then we just send it a mousemove event.
			if ((tracker != null) && (tracker == fMouseTracker))
			{
				fMouseTracker.OnMouseMove(e);
				return ;
			}

			// If the tracker is different
			// Then the current tracker needs to receive the MouseExit event
			if (fMouseTracker != null)
			{
				fMouseTracker.OnMouseLeave(e);
			}

			// And the new tracker needs to receive the MouseEntered event
			fMouseTracker = tracker;
			fMouseTrackerGraphic = trackerGraphic;
			if (fMouseTracker != null)
			{
				fMouseTracker.OnMouseEnter(e);
			} 
			else
			{
				// Otherwise, no trackers have been hit, so we're just free floating
				// in the window.  Set the cursor to the generic arrow
				//Win32Cursor.Current = Win32Cursor.Arrow;
			}
			*/
	}


	public override void OnMouseUp(MouseActivityArgs e) 
	{
		//Console.WriteLine("GraphicWindow.OnMouseUp - {0}", e.ToString());

		if (fMouseTracker != null)
			fMouseTracker.OnMouseUp(e);
		else
		{
			//Console.WriteLine("GraphicWindow.OnMouseUp - no tracker found");
		}
	}


	// Graphic hierarchy
	// We override this because the default behavior of the GroupGraphic
	// is the adjust the child's coordinates.
	// We don't want that behavior, but we do want to adjust
	// based on using the layout handler.
	// Also, we don't want our frame to expand.
	public override void OnGraphicAdded(IGraphic aGraphic)
	{
		// Layout the graphic again
		LayoutHandler.AddToLayout(aGraphic);
	}

	public ActiveArea RootGraphic
	{
		get
		{
			return fRootGraphic;
		}
		set
		{
			fRootGraphic = value;
			if (fRootGraphic!=null)
			{
				fRootGraphic.Window = Window;
			}
			Invalidate();
		}
	}

	public override void Draw(DrawEvent de)
	{
		//grfx.PageUnit = RootGraphic.GraphicsUnit;
		//grfx.PageScale = fZoomFactor;
		//grfx.ScaleTransform(fZoomFactor,fZoomFactor);

		RootGraphic.Draw(de);
	}


	// Graphic hierarchy
	public override List<IGraphic> GraphicChildren
	{
		get
		{
			return RootGraphic.GraphicChildren;
		}
	}


	public override void AddGraphic(IGraphic aGraphic)
	{
		this.AddGraphic(aGraphic, null);
	}

	public override void AddGraphic(IGraphic aGraphic, IGraphic before)
	{
		RootGraphic.AddGraphic(aGraphic, before);
	}

    public override void AddGraphicAfter(IGraphic aGraphic, IGraphic after)
	{
		RootGraphic.AddGraphicAfter(aGraphic, after);
	}

	public override void MoveGraphicToFront(IGraphic aDrawable)
	{
		RootGraphic.MoveGraphicToFront(aDrawable);
	}

	public override bool RemoveGraphic(IGraphic aGraphic)
	{
		return RootGraphic.RemoveGraphic(aGraphic);
	}

	public override void RemoveAllGraphics()
	{
		RootGraphic.RemoveAllGraphics();
	}

	public override int CountGraphics()
	{
		return RootGraphic.CountGraphics();
	}

	public override bool Contains(int x, int y)
	{
		return Frame.Contains((int)x, (int)y);
	}

	// Find which graphic is in the group at a particular
	// location.
	public override IGraphic GraphicAt(int index)
	{
		return RootGraphic.GraphicAt(index);
	}

	// return all the graphics in the hierarchy that are hit by the point
	public override void GraphicsAt(int x, int y, ref Stack<IGraphic> coll)
	{
		RootGraphic.GraphicsAt(x, y, ref coll);
	}

	public override IGraphic GraphicNamed(string graphicName)
	{
		return RootGraphic.GraphicNamed(graphicName);
	}

    public override IGraphic GraphicNamedRecurse(string graphicName)
	{
		return RootGraphic.GraphicNamedRecurse(graphicName);
	}
	// Graphic hierarchy

    public override ILayoutManager LayoutHandler
	{
		get { return RootGraphic.LayoutHandler; }
		set
		{
			RootGraphic.LayoutHandler = value;
			value.Layout(RootGraphic);
		}
	}
}

