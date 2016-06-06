using System;
using System.Collections.Generic;


namespace NewTOAPIA.UI
{
    using NewTOAPIA.Drawing;
    using NewTOAPIA.Graphics;

	public class GraphicWindow : Window, IGraphicGroup
    {
        #region Fields
        Transformation fWorldTransform;
        Transformation fLocalTransform;

        IGraphic fActiveGraphic = null;
        private List<IGraphic> fChildList;
        private ILayoutManager fLayoutManager;
        

        protected bool fTracking;
        protected bool fIsDepressed;
        protected Point2I fLastMouse;
        #endregion

        #region Constructors
        public GraphicWindow(string title, int x, int y, int width, int height)
			:base(title,x,y,width,height)
		{
			fActiveGraphic = null;
			//fMouseTrackerGraphic = null;
            fChildList = new List<IGraphic>();
            fLayoutManager = LayoutManager.Empty;
        }
        #endregion

        public override void OnPaint(DrawEvent devent)
        {
            Draw(devent);
        }

        #region IDrawable
        public virtual void Draw(DrawEvent devent)
        {
            DrawChildren(devent);
        }
        #endregion

        #region IGraphic
        public string Name
        {
            get { return fName; }
        }

        //public GraphicsUnit GraphicsUnit
        //{
        //    get { return GraphicsUnit.Display; }
        //    set { ;}
        //}

        public bool IsVisible
        {
            get { return true; }
            set { ;}
        }

        public bool Contains(int x, int y)
        {
            return Frame.Contains(x, y);
        }

        public Point3D Origin
        {
            get { return new Point3D(Frame.Left, Frame.Top); }
            set {;}
        }

        public Vector3D Dimension
        {
            get { return new Vector3D(Frame.Width, Frame.Height); }
            set
            {
                RectangleI frame = new RectangleI(new Point2I(Frame.Left, Frame.Top), new Size2I((int)value.x, (int)value.y));
                Frame = frame;
            }
        }

        public int Bottom
        {
            get { return Frame.Bottom; }
            set
            {
                RectangleI frame = Frame;
                //frame.Bottom = value;
                Frame = frame;

            }
        }

        public int Left
        {
            get { return Frame.Left; }
            set
            {
                RectangleI frame = Frame;
                //frame.Left = value;
                Frame = frame;
            }
        }

        public int Right
        {
            get { return Frame.Right; }
            set
            {
                RectangleI frame = Frame;
                //frame.Right = value;
                Frame = frame;

            }
        }

        public int Top
        {
            get { return Frame.Top; }
            set
            {
                RectangleI frame = Frame;
                //frame.Top = value;
                Frame = frame;
            
            }
        }
        public int Width
        {
            get { return Frame.Width; }
            set
            {
                RectangleI frame = new RectangleI(new Point2I(Frame.Left, Frame.Top), new Size2I(value, Frame.Height));
                Frame = frame;

            }
        }
        public int Height
        {
            get { return Frame.Height; }
            set
            {
                RectangleI frame = new RectangleI(new Point2I(Frame.Left, Frame.Top), new Size2I(Frame.Width, value));
                Frame = frame;

            }
        }

        public Transformation LocalTransform
        {
            get { return fLocalTransform; }
            set
            {
                fLocalTransform = value;
            }
        }

        public Transformation WorldTransform
        {
            get { return fWorldTransform; }
            set
            {
                fWorldTransform = value;
            }
        }

        public IGraphicGroup Container
        {
            get { return null; }
            set { ;}
        }

        public IWindow Window
        {
            get { return null; }
            set { ;}
        }

        public virtual void UpdateBoundaryState()
        {
            foreach (IGraphic g in GraphicChildren)
            {
                g.UpdateBoundaryState();
            }
        }

        public virtual void UpdateGeometryState()
        {
            foreach (IGraphic g in GraphicChildren)
            {
                g.UpdateGeometryState();
            }
        }
        #endregion

        #region IGraphicGroup
        public virtual void DrawChildren(DrawEvent devent)
        {
            IGraphPort graphPort = devent.GraphPort;

            foreach (IGraphic g in GraphicChildren)
            {
                // Translate before we draw so all drawing within a child has
                // 0,0 in it's upper left corner	
                devent.GraphPort.SaveState();
                RectangleI f = g.Frame;
                f.Inflate(1, 1 );
                devent.GraphPort.SetClipRectangle(f);		// Make sure they don't draw outside their frame

                devent.GraphPort.TranslateTransform(g.Frame.Left, g.Frame.Top);

                g.Draw(devent);

                // Restore the state
                devent.GraphPort.ResetState();
            }
        }

        // IGraphicHierarchy related
        public virtual ILayoutManager LayoutHandler
        {
            get { return fLayoutManager; }
            set
            {
                fLayoutManager = value;
                if (null != fLayoutManager)
                    fLayoutManager.Layout(this);
            }
        }


        public virtual void AddGraphic(IGraphic aGraphic)
        {
            this.AddGraphic(aGraphic, null);
        }

        public virtual void AddGraphic(IGraphic aGraphic, IGraphic before)
        {
            if (aGraphic == null)
            {
                return;
            }


            lock (this)
            {
                GraphicChildren.Add(aGraphic);
            }

            aGraphic.Container = this;
            aGraphic.Window = this;

            OnGraphicAdded(aGraphic);
        }

        public virtual void AddGraphicAfter(IGraphic aGraphic, IGraphic after)
        {
            if (aGraphic == null)
            {
                return;
            }

            lock (this)
            {
                GraphicChildren.Add(aGraphic);
            }
            aGraphic.Container = null;
            aGraphic.Window = this;

            OnGraphicAdded(aGraphic);
        }

        // Graphic hierarchy
        // We override this because want to adjust the children's
        // positions and sizes based on using the layout handler.
        // Also, we don't want our frame to expand.
        public virtual void OnGraphicAdded(IGraphic aGraphic)
        {
            // Layout the graphic again
            LayoutHandler.AddToLayout(aGraphic);
        }

        public virtual bool RemoveGraphic(IGraphic aGraphic)
        {
            lock (this)
            {
                GraphicChildren.Remove(aGraphic);
            }

            OnGraphicRemoved(aGraphic);

            return false;
        }

        public virtual void RemoveAllGraphics()
        {
            foreach (IGraphic g in GraphicChildren)
                RemoveGraphic(g);
        }

        protected virtual void OnGraphicRemoved(IGraphic graphic)
        {
            graphic.Container = null;
            graphic.Window = this;

            // Layout the graphic again
            LayoutHandler.Layout(this);
        }

        public virtual void MoveGraphicToFront(IGraphic graphic)
        {
            if (null == graphic)
                return;

            RemoveGraphic(graphic);

            // Move the new graphic to the front in the hierarchy
            AddGraphic(graphic);
        }

        public virtual List<IGraphic> GraphicChildren
        {
            get
            {
                return fChildList;
            }
        }

        public virtual int CountGraphics()
        {
            lock (this)
            {
                return GraphicChildren.Count;
            }
        }

        public virtual IGraphic GraphicAt(int index)
        {
            return (IGraphic)fChildList[index];
        }

        // return all the graphics in the hierarchy that are hit by the point
        /// <summary>
        /// Return a stack of graphics that are located under the point.
        /// The graphic hierarchy is traversed from the parent most, to the
        /// child most.  The child that is the closest to the top of the visual
        /// stack will be the first in the stack.  The parent parent will
        /// be at the bottom of the stack.
        /// 
        /// This allows events such as mouse tracking to hit the closest graphic
        /// first, and purcolate up to the parent if none of the children do 
        /// anything with it.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="coll"></param>
        public virtual void GraphicsAt(int x, int y, ref Stack<IGraphic> coll)
        {
            foreach (IGraphic g in fChildList)
            {
                if (g.Frame.Contains(x, y))
                {
                    coll.Push(g);

                    if (g is IGraphicGroup)
                    {
                        // Transform the point so the graphic we are checking
                        // uses a point that is within its frame, not the parent's frame
                        int newX = x - g.Frame.Left;
                        int newY = y - g.Frame.Top;
                        ((IGraphicGroup)g).GraphicsAt(newX, newY, ref coll);
                    }
                }
            }
        }

        // Just check immediate children for a graphic of the given name
        public virtual IGraphic GraphicNamed(string graphicName)
        {
            foreach (IGraphic g in fChildList)
            {
                if (g.Name.CompareTo(graphicName) == 0)
                    return g;
            }

            return null;
        }

        // Check the whole hierarchy for a graphic of the given name
        public virtual IGraphic GraphicNamedRecurse(string graphicName)
        {
            foreach (IGraphic g in fChildList)
            {
                // We found a graphic of the given name, so return
                if (g.Name.CompareTo(graphicName) == 0)
                {
                    return g;

                }
                else if (g is IGraphicHierarchy)
                {
                    IGraphic aGraphic = ((IGraphicHierarchy)g).GraphicNamedRecurse(graphicName);
                    if (aGraphic != null)
                        return aGraphic;
                }
            }

            return null;
        }
        #endregion

        public IGraphic ActiveGraphic
        {
            get { return fActiveGraphic; }
            protected set
            {
                // If there is currently an active graphic,
                // and it's different than the one that wants to become
                // active, then call LoseFocus on the existing active 
                // graphic so it can do whatever it wants to do owhen it 
                // loses focus.
                if (null != fActiveGraphic && (fActiveGraphic != value))
                {
                    fActiveGraphic.LoseFocus();
                }

                fActiveGraphic = value;

                // Now, if the currently active graphic is not null, then 
                // call the Focus() method on it so that it can perform any 
                // actions related to gaining focus.
                if (null != fActiveGraphic)
                    fActiveGraphic.Focus();
            }
        }

        public IGraphic MouseTrackerAt(int x, int y)
		{
            // The coordinates are relative to the upper left hand
            // corner of the ClientArea, and are in units of pixels.
            Stack<IGraphic> graphicStack = new Stack<IGraphic>();
			GraphicsAt(x,y, ref graphicStack);

			foreach (IGraphic g in graphicStack)
			{
                if (true == g.Enabled)
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
            int x = e.X;
            int y = e.Y;
            IGraphic trackerGraphic = MouseTrackerAt(x, y);

            ActiveGraphic = trackerGraphic;
            
            if (null != trackerGraphic)
			{
                trackerGraphic.OnMouseDown(e);
			} 
		}

        public override void OnMouseMove(MouseActivityArgs e) 
		{
            //Console.WriteLine("GraphicWindow.OnMouseMove - {0}", e.ToString());
            IGraphic trackerGraphic = MouseTrackerAt(e.X, e.Y);

            // The first case is where we have a current mouse tracker, and
            // the mouse is still moving through that tracker.
            // We give the current tracker more mouse move events and just return.
            if ((null != fActiveGraphic) && (trackerGraphic == fActiveGraphic))
            {
                fActiveGraphic.OnMouseMove(e);
                return;
            }

            // If we're here, the mouse tracker that we just found differs from
            // the one that may currently be active.
            // There are two possibilities.
            // 1. We are now in empty space with no tracker under the mouse cursor
            // 2. We are now positioned on a new tracker.
            //
            // In either case, we need to tell the old mouse tracker, if it is not null
            // that the mouse left it.
            if (null != fActiveGraphic)
            {
                fActiveGraphic.OnMouseLeave(e);
            }

            // Now, if we are in empty space, we can set the tracker to null.
            // We also set the cursor to the arrow cursor, or whatever is the
            // default for the window so it doesn't get stuck in the wrong state.
            if (null == trackerGraphic)
            {
                //Console.WriteLine("GraphicWindow.OnMouseMove - no tracker");
                //fActiveGraphic = null;

                // Set the cursor to the generic arrow
                Win32Cursor.SetCurrentCursor(Win32Cursor.Arrow);

                return;
            }


            // Lastly, the mouse has entered a new tracker.  We want to inform
            // that tracker that the mouse has entered, and set it as the new
            // tracker.
			//fActiveGraphic = trackerGraphic;

            trackerGraphic.OnMouseEnter(e);
		}

        public override void OnMouseUp(MouseActivityArgs e) 
		{
			//Console.WriteLine("GraphicWindow.OnMouseUp - {0}", e.ToString());

			if (fActiveGraphic != null)
					fActiveGraphic.OnNext(e);
			else
			{
				//Console.WriteLine("GraphicWindow.OnMouseUp - no tracker found");
			}
        }

        #region Keyboard Activity
        public override void OnKeyDown(KeyboardActivityArgs ke)
        {
            if (null != ActiveGraphic)
                ActiveGraphic.OnKeyDown(ke);
            else
                base.OnKeyDown(ke);
        }

        public override void OnKeyUp(KeyboardActivityArgs ke)
        {
            if (null != ActiveGraphic)
                ActiveGraphic.OnKeyUp(ke);
            else
                base.OnKeyUp(ke);
        }

        public override void OnKeyPress(KeyboardActivityArgs ke)
        {
            if (null != ActiveGraphic)
                ActiveGraphic.OnKeyPress(ke);
            else
                base.OnKeyPress(ke);
        }

        #endregion
    }
}