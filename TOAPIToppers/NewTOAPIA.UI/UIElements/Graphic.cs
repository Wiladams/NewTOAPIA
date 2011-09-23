using System;

using NewTOAPIA;
using NewTOAPIA.Drawing;

namespace NewTOAPIA.UI 
{
    using NewTOAPIA.Drawing.GDI;
    using NewTOAPIA.Graphics;

    public class Graphic : IGraphic
    {
        #region Fields
        private bool fDebug;
        
        bool fEnabled;
        bool fActive;
        bool fVisible;
        bool fHasFocus;

        private string fName;

        protected Point3D fOrigin;
        protected Vector3D fDimension;
        protected RectangleI fFrame;
        
        //private GraphicsUnit fGraphicsUnit;
        private Transformation fWorldTransform;
        private Transformation fLocalTransform;

        private IGraphicGroup fContainer;
		private IWindow fWindow;
        #endregion

        #region Constructors
        public Graphic()
			: this(string.Empty, 0, 0, 0, 0)
		{
            // Do nothing in here.  It's just a convenience for the
            // more general call.
        }

		public Graphic(String name)
			: this(name, 0, 0, 0, 0)
		{
            // Do nothing in here.  It's just a convenience for the
            // more general call.
        }

        public Graphic(String name, RectangleI frame)
            : this(name, frame.Left, frame.Top, frame.Width, frame.Height)
        {
            // Do nothing in here.  It's just a convenience for the
            // more general call.
        }

		public Graphic(String name, int x, int y, int width, int height)
		{
            fName = name;

            fOrigin = new Point3D(0, 0);
            fDimension = new Vector3D(width, height);

            fFrame = new RectangleI(x, y, width, height);

            //fGraphicsUnit = GraphicsUnit.Display;
			fVisible = true;
            fActive = true;
            fEnabled = false;
        }
        #endregion

        #region Properties
        
        public bool Active
        {
            get { return fActive; }
            set { fActive = value; }
        }

        public bool Enabled
        {
            get { return fEnabled; }
            set { fEnabled = value; }
        }
        
        /// <summary>
        /// The Container tells you which GraphicGroup this graphic is a part of.
        /// When the container is set, there are a number of properties that the
        /// graphic gets from the container.
        /// </summary>
        public virtual IGraphicGroup Container
		{
			get { return fContainer; }
			set { 
				fContainer = value;

                OnSetContainer();
			}
		}

        protected virtual void OnSetContainer()
        {
            if (null != Container)
            {
                fWindow = Container.Window;
                //fGraphicsUnit = Container.GraphicsUnit;
            }
            else
            {
                fWindow = null;
            }

            UpdateGeometryState();
        }

		public virtual IWindow Window
		{
			get { 
				if (fWindow != null)
					return fWindow;

				if (Container != null)
					return Container.Window;

				return null;
			}
			set { fWindow = value; }
        }

        public virtual Transformation LocalTransform
        {
            get { return fLocalTransform; }
            set
            {
                fLocalTransform = value;
                OnSetLocalTransform();
            }
        }

        protected virtual void OnSetLocalTransform()
        {
        }


        public virtual Transformation WorldTransform
        {
            get { return fWorldTransform; }
            set
            {
                fWorldTransform = value;
                OnSetWorldTransform();
            }
        }

        protected virtual void OnSetWorldTransform()
        {
        }

		public virtual string Name
		{
			get { return fName; }
		}

        //public GraphicsUnit GraphicsUnit
        //{
        //    get { return fGraphicsUnit; }
        //    set { fGraphicsUnit = value; }
        //}

		public virtual bool IsVisible
		{
			get { return fVisible; }
			set { fVisible = value; }
		}

		public virtual bool Debug
		{
			get { return fDebug; }
			set { fDebug = value; }
		}

        public virtual RectangleI ClientRectangle
        {
            get 
            { 
                RectangleI rect = new RectangleI((int)Origin.x, (int)Origin.y, (int)Dimension.x, (int)Dimension.y);
                return rect;
            }
        }

        public virtual RectangleI Frame 
		{ 
			get{return fFrame;}
			set
			{
				fDimension = new Vector3D(value.Width, value.Height);
				fFrame = value;
                UpdateGeometryState();
			} 
		}


		public virtual Point3D Origin
		{
			get { return fOrigin; }
			set { 
                fOrigin = value;
 
            }
		}

		public virtual Vector3D Dimension
		{
			get { return fDimension; }
			set { 
                fDimension = value;
                ResizeTo((int)fDimension.x, (int)fDimension.y);
            }
        }
        #endregion

        #region IInteractor
        public virtual bool HasFocus
        {
            get { return fHasFocus; }
        }

        public virtual void Focus()
        {
            fHasFocus = true;
            
            OnGainedFocus();
        }

        protected virtual void OnGainedFocus()
        {
        }

        public virtual void LoseFocus()
        {
            fHasFocus = false;

            OnLostFocus();

        }

        protected virtual void OnLostFocus()
        {
        }
        #endregion

        #region Public Methods
        public virtual void OnCompleted()
        {
        }

        public virtual void OnError(Exception excep)
        {
        }

        #region IReactToMouseActivity
        public virtual void OnNext(MouseActivityArgs e)
        {
            switch (e.ActivityType)
            {
                case MouseActivityType.MouseDown:
                    OnMouseDown(e);
                    break;

                case MouseActivityType.MouseMove:
                    OnMouseMove(e);
                    break;

                case MouseActivityType.MouseUp:
                    OnMouseUp(e);
                    break;

                case MouseActivityType.MouseEnter:
                    OnMouseEnter(e);
                    break;

                case MouseActivityType.MouseHover:
                    OnMouseHover(e);
                    break;

                case MouseActivityType.MouseLeave:
                    OnMouseLeave(e);
                    break;

                case MouseActivityType.MouseWheel:
                    OnMouseWheel(e);
                    break;
            }
        }
        #endregion

        #region IHandleMouseActivity
        public virtual void OnMouseDown(MouseActivityArgs e)
        {
        }

        public virtual void OnMouseMove(MouseActivityArgs e)
        {
        }

        public virtual void OnMouseUp(MouseActivityArgs e)
        {
        }

        public virtual void OnMouseWheel(MouseActivityArgs e)
        {
        }
        #endregion

        #region IReactToMouseMovement
        public virtual void OnMouseEnter(MouseActivityArgs e)
        {
        }

        public virtual void OnMouseHover(MouseActivityArgs e)
        {
        }

        public virtual void OnMouseLeave(MouseActivityArgs e)
        {
        }
        #endregion

        #region IReactToKeyboardActivity
        public virtual void OnNext(KeyboardActivityArgs ke)
        {
            switch (ke.AcitivityType)
            {
                case KeyActivityType.KeyDown:
                    OnKeyDown(ke);
                    break;

                case KeyActivityType.KeyUp:
                    OnKeyUp(ke);
                    break;

                case KeyActivityType.KeyChar:
                    OnKeyPress(ke);
                    break;
            }
        }
        #endregion

        #region IHandleKeyboardActivity
        public virtual void OnKeyDown(KeyboardActivityArgs ke)
        {
        }

        public virtual void OnKeyUp(KeyboardActivityArgs ke)
        {
        }

        public virtual void OnKeyPress(KeyboardActivityArgs kpe)
        {
        }
        #endregion

        /// <summary>
        /// Determine whether a given point is within our frame.  The point is given in the 
        /// coordinate system of the containing control, and so is measured against the frame
        /// and not against the ClientRectangle.
        /// </summary>
        /// <param name="x">The x-coordinate of the point of interest.</param>
        /// <param name="y">The y-coordinate of the point of interest.</param>
        /// <returns>Returns true if the point is contained within the bounding frame.</returns>
        public virtual bool Contains(int x, int y)
        {
            return Frame.Contains(x, y);
        }

        /// <summary>
        /// Invalidate a portion of the client rectangle.
        /// </summary>
        /// <param name="partialFrame"></param>
        public virtual void Invalidate(RectangleI portionOfClientRectangle)
        {
            // The portion to be invalidated is given in the coordinate space
            // of the client rectangle.  That portion needs to be converted
            // into the parent's space and sent to the parent for evaluation
            portionOfClientRectangle.Offset(Frame.Left, Frame.Top);

            if (null != Container)
            {
                Container.Invalidate(portionOfClientRectangle);
            }
            else if (null != Window)
                Window.Invalidate(portionOfClientRectangle);
        }

        /// <summary>
        /// Invalidate the entire client rectangle.
        /// </summary>
        public virtual void Invalidate()
        {
            Invalidate(ClientRectangle);
        }

        public virtual void UpdateBoundaryState()
        {
        }

        /// <summary>
        /// The Geometry state needs to be updated whenever there is a change to 
        /// the frame, or any other aspect of the geometry.  Here recalculations
        /// that are dependent on the geometry will occur.
        /// By default, the container is queried for its transform.
        /// </summary>
        public virtual void UpdateGeometryState()
        {
            // Since we are a part of a container, we will create
            // our own transform, starting with Identity
            Transformation myTransform = new Transformation();

            // 1. Query the container for the world transform
            if (null != Container)
            {
                //fGraphicsUnit = fContainer.GraphicsUnit;


                // If the container has a world transform, we want to start from 
                // that transform.
                Transformation parentTransform = fContainer.WorldTransform;

                if (null != parentTransform)
                {
                    myTransform = new Transformation(parentTransform);
                }
            }

            // Add the frame offset as part of the world transform.
            // 
            myTransform.Translate(Frame.Left, Frame.Top);

            WorldTransform = myTransform;


            // Let subclassers do the specifics they need to do
            OnUpdateGeometryState();
        }

        protected virtual void OnUpdateGeometryState()
        {
        }


        #region Drawing
        // Drawable things
        public virtual void Draw(DrawEvent devent)
        {
            if (null != WorldTransform)
            {
                // Save the current graph port state
                //devent.GraphPort.SaveState();

                // Set our world transform if we have one
                //devent.GraphPort.SetWorldTransform(WorldTransform);
            }

            // Do the drawing
            OnDraw(devent);

            if (null != WorldTransform)
            {
                // Restore the graphics state
                //devent.GraphPort.ResetState();
            }
        }


        public virtual void DrawBackground(DrawEvent devent)
		{
		}

        public virtual void DrawSelf(DrawEvent devent)
		{
		}

        public virtual void DrawForeground(DrawEvent devent)
		{
		}

        public virtual void OnDraw(DrawEvent devent)
        {
            if (!IsVisible)
            {
                return;
            }

            DrawBackground(devent);
            DrawSelf(devent);
            DrawForeground(devent);

            // If we're debugging, we want to see the outline
            // 
            if (Debug)
            {
                OnDebug(devent);
            }
        }
        
        // By default, debugging will just draw a red rectangle around the frame
        protected virtual void OnDebug(DrawEvent devent)
        {
            RectangleI aRect = new RectangleI((int)Origin.x, (int)Origin.y, (int)Dimension.x, (int)Dimension.y);
            devent.GraphPort.DrawRectangle(GDICosmeticPen.Red, aRect);
        }
        #endregion

        #region Moving
        public virtual void MoveTo(int x, int y)
        {
			int dx = x - fFrame.Left;
            int dy = y - fFrame.Top;

            MoveBy(dx, dy);
        }

        public virtual void MoveBy(int dx, int dy)
        {
			fFrame.Offset(dx, dy);

            OnMoving(dx, dy);
            
            UpdateGeometryState();
		}

		protected virtual void OnMoving(int dw, int dh)
		{
            if (null != Window)
                Window.Invalidate(Frame);
        }
        #endregion

        #region Resizing
        public virtual void ResizeTo(int width, int height)
        {
            fFrame = new RectangleI(new Point2I(fFrame.Left, fFrame.Top), new Size2I(width, height));
            fDimension.x = width;
            fDimension.y = height;

            UpdateGeometryState();
        }

        public virtual void ResizeBy(int dw, int dh)
        {
            fFrame = new RectangleI(new Point2I(fFrame.Left, fFrame.Top), 
                new Size2I(Frame.Width+dw, Frame.Height+dh));
            fDimension.x = fFrame.Width;
            fDimension.y = fFrame.Height;

            UpdateGeometryState();
        }

        #endregion

        #endregion


        // This transforms screen coordinates to local coordinates
        // If the parent is null, then this is a top level graphic
        // and it becomes important to utilize the graphicsUnits
        // in the calculation.
        // Assume screen coordinates are in GraphicsUnit.Display (96 dpi)
        //public virtual GraphicsUnit ScreenToLocal(ref float x, ref float y)
        //{
        //    GraphicsUnit containerUnits=GraphicsUnit.Pixel;

        //    POINTF[] pts = new POINTF[] { new POINTF(x, y) };

        //    // First give the container a chance at converting the point
        //    if (Container != null)
        //    {
        //        containerUnits = Container.ScreenToLocal(ref x, ref y);
        //    }
        //    else
        //    {
        //        Graphics grfx = Graphics.FromHwnd(IntPtr.Zero);	// Graphics on the screen
        //        grfx.PageUnit = GraphicsUnit;
        //        grfx.TransformPOINTs(CoordinateSpace.Page, CoordinateSpace.Device,pts);
        //        grfx.Dispose();
        //    }

        //    // Now subtract off our own part
        //    x = pts[0].X - Frame.Left;
        //    y = pts[0].Y - Frame.Top;

        //    return containerUnits;
        //}

        //public virtual void LocalToScreen(ref float x, ref float y)
        //{
        //    // Now add on our own part
        //    x += Frame.Left;
        //    y += Frame.Top;


        //    // First give the container a chance at converting the point
        //    if (Container != null)
        //    {
        //        Container.LocalToScreen(ref x, ref y);
        //    }
        //    else
        //    {
        //        POINTF[] pts = new POINTF[] { new POINTF(x, y) };
        //        Graphics grfx = Graphics.FromHwnd(IntPtr.Zero);	// Graphics on the screen
        //        grfx.PageUnit = GraphicsUnit;
        //        grfx.TransformPOINTs(CoordinateSpace.Device, CoordinateSpace.Page, pts);
        //        grfx.Dispose();
        //        x = pts[0].X;
        //        y = pts[0].Y;
        //    }
        //}


        //// Xml Serialization Infrastructure
        //public XmlSchema GetSchema()
        //{
        //    return (null);
        //}

        //public virtual void WriteXmlHead(XmlWriter writer)
        //{
        //    writer.WriteStartElement("Graphic");
        //}

        //public virtual void WriteXmlAttributes(XmlWriter writer)
        //{
        //    writer.WriteAttributeString("name", fName);
        //    writer.WriteAttributeString("visible", fVisible.ToString());
        //    writer.WriteAttributeString("debug", fDebug.ToString());
        //    writer.WriteAttributeString("unit", fGraphicsUnit.ToString());
        //}

        //public virtual void WriteXmlFrame(XmlWriter writer)
        //{
        //    writer.WriteStartElement("frame");
        //    writer.WriteStartElement("rectangle");
        //    writer.WriteAttributeString("x", fFrame.X.ToString());
        //    writer.WriteAttributeString("y", fFrame.Y.ToString());
        //    writer.WriteAttributeString("width", fFrame.Width.ToString());
        //    writer.WriteAttributeString("height", fFrame.Height.ToString());
        //    writer.WriteEndElement();
        //    writer.WriteEndElement();
        //}

        //public virtual void WriteXmlBody(XmlWriter writer)
        //{
        //    // Write out the frame
        //    WriteXmlFrame(writer);

        //    // Write out the Origin
        //    writer.WriteStartElement("origin");
        //    writer.WriteAttributeString("x", fOrigin.x.ToString());
        //    writer.WriteAttributeString("y", fOrigin.x.ToString());
        //    writer.WriteEndElement();

        //    // Write out the Dimension
        //    writer.WriteStartElement("size");
        //    writer.WriteAttributeString("width", fDimension.Width.ToString());
        //    writer.WriteAttributeString("height", fDimension.Height.ToString());
        //    writer.WriteEndElement();
        //}

        //public virtual void WriteXmlEnd(XmlWriter writer)
        //{
        //    writer.WriteEndElement();
        //}

        //public virtual void WriteXml(XmlWriter writer)
        //{
        //    WriteXmlHead(writer);
        //    WriteXmlAttributes(writer);
        //    WriteXmlBody(writer);
        //    WriteXmlEnd(writer);
			
        //}




        //// Reading XML
        //public virtual void ReadXml(XmlReader reader)
        //{
        //    ReadXmlHead(reader);
        //    ReadXmlAttributes(reader);
        //    ReadXmlBody(reader);
        //    ReadXmlEnd(reader);
        //}

        //public virtual void ReadXmlHead(XmlReader reader)
        //{
        //    reader.Read();
        //}

        //public virtual void ReadXmlAttributes(XmlReader reader)
        //{
        //    fName = reader.GetAttribute("name");
        //    fVisible = Convert.ToBoolean(reader.GetAttribute("visible"));
        //    fDebug = Convert.ToBoolean(reader.GetAttribute("debug"));

        //    switch (reader.GetAttribute("unit"))
        //    {
        //        case "Inch":
        //            this.GraphicsUnit = GraphicsUnit.Inch;
        //            break;

        //        case "Display":
        //            this.GraphicsUnit = GraphicsUnit.Display;
        //            break;
        //    }
        //}

        //public virtual void ReadXmlFrame(XmlReader reader)
        //{
        //    float x;
        //    float y;
        //    float width;
        //    float height;

        //    reader.Read();
        //    if (reader.IsStartElement("frame"))
        //    {
        //        reader.Read();
        //        x = Convert.ToSingle(reader.GetAttribute("x"));
        //        y = Convert.ToSingle(reader.GetAttribute("y"));
        //        width = Convert.ToSingle(reader.GetAttribute("width"));
        //        height = Convert.ToSingle(reader.GetAttribute("height"));
        //    }
        //}

        //public virtual void ReadXmlBody(XmlReader reader)
        //{
        //    // Read in the frame
        //    ReadXmlFrame(reader);

        //    // Read in the Origin
        //    reader.Read();	// Now we should be on the origin element
        //    int x = Convert.ToInt32(reader.GetAttribute("x"));
        //    int y = Convert.ToInt32(reader.GetAttribute("y"));
        //    this.Origin = new POINT(x,y);


        //    // Read in the Dimension
        //    reader.Read();	// Now we should be on the size element
        //    int width = Convert.ToInt32(reader.GetAttribute("width"));
        //    int height = Convert.ToInt32(reader.GetAttribute("height"));
        //    this.Dimension = new Size(x,y);
        //}

        //public virtual void ReadXmlEnd(XmlReader reader)
        //{
        //}
    }
}

