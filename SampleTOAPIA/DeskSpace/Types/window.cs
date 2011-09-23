using System;
using System.Collections;

using TOAPI.Types;
using TOAPI.User32;

	public class Window :  IWindow
	{
		Win32Window fPlatformWindow;
        uint fBackgroundColor;
        Rectangle fWindowFrame;
        Point fOrigin;
        Size fDimension;
        string fWindowTitle;
        bool fEnabled;
        bool fActive;
        bool fMouseCaptured;

		// Default constructor, just a shell of a window
		public Window()
			: this(string.Empty, 0, 0, 0, 0)
		{
            fWindowTitle = "Papyrus Window";
            fWindowFrame = new Rectangle();
            fDimension = new Size(0, 0);
            fOrigin = new Point(0,0);
            fMouseCaptured = false;
        }

		public Window(string title, int x, int y, int width, int height)
		{
            fWindowTitle = title;
            fWindowFrame = new Rectangle(x, y, width, height);
            fOrigin = new Point(0, 0);
            fDimension = new Size(width, height);
            fMouseCaptured = false;
			//this.Text = title;
			//this.Size = new Size(width, height);
			//this.Location = new Point(x, y);
            fBackgroundColor = RGBColor.LtGray;

		    fPlatformWindow = new Win32Window(title, x, y, width, height, this);
		}


        public Win32Window PlatformWindow 
        {
            get {return fPlatformWindow;}
        }


        public IntPtr Handle
        {
            get
            {
                return fPlatformWindow.Handle;
            }
        }

        public Rectangle Frame
        {
            get { return fPlatformWindow.Frame; }
        }

        public Rectangle Bounds
        {
            get { return fPlatformWindow.Bounds; }
        }

        public bool Capture
        {
            get { return fMouseCaptured; }
            set
            {
                if (value == true)
                {
                    fMouseCaptured = true;
                    User32.SetCapture(Handle);
                }
                else
                {
                    fMouseCaptured = false;
                    User32.SetCapture(IntPtr.Zero);
                }
            }
        }

        // IMoveable
        /// <summary>
        /// Move the upper left corner of the window to the specified location.
        /// </summary>
        /// <param name="x">X Coordinate of upper left corner.</param>
        /// <param name="y">Y Coordinate of upper left corner.</param>
        /// <returns>true</returns>
        public virtual bool MoveTo(int x, int y)
        {
            return fPlatformWindow.MoveTo(x, y);
        }

        public virtual bool MoveBy(int dx, int dy)
        {
            return fPlatformWindow.MoveBy(dx, dy);
        }

        public virtual void ResizeBy(int dw, int dh)
        {
            fPlatformWindow.ResizeBy(dw, dh);
        }

        public virtual void ResizeTo(int width, int height)
        {
            fPlatformWindow.ResizeTo(width, height);
        }

        // ITransformEvents
        public virtual void OnMovedTo(int x, int y)
        {
            fWindowFrame.Left = x;
            fWindowFrame.Top = y;
        }

        public virtual void OnMovedBy(int dw, int dh)
        {
        }

        public virtual void OnMoving(int dw, int dh)
        {
        }

        public virtual void OnResized(int dw, int dh)
        {
            fWindowFrame.Width = dw;
            fWindowFrame.Height = dh;
            fDimension.Width = fWindowFrame.Width;
            fDimension.Height = fWindowFrame.Height;

            //Console.WriteLine("Window.OnResized: {0} {1}", dw, dh);
            //Console.WriteLine("Window.OnResized: Width - {0}, Height - {1}", Frame.Width, Frame.Height);
        }

        public virtual void OnResizing(int dw, int dh)
        {
        }

        // Reacting to the keyboard
        public virtual void OnKeyDown(KeyEventArgs ke)
        {
        }

        public virtual void OnKeyUp(KeyEventArgs ke)
        {
        }

        public virtual void OnKeyPress(KeyCharEventArgs kpe)
        {
        }

        // Reacting to the mouse
        public virtual void OnMouseDown(MouseEventArgs e)
        {
        }

        /// <summary>
        /// OnMouseEnter
        /// 
        /// This gets called whenever the pointing device enters our frame.
        /// We want to do interesting things here like change the cursor shape
        /// to be whatever we require.
        /// </summary>
        /// <param name="e"></param>
        public virtual void OnMouseEnter(EventArgs e)
        {
        }

        public virtual void OnMouseHover(EventArgs e)
        {
        }

        public virtual void OnMouseLeave(EventArgs e)
        {
        }

        public virtual void OnMouseMove(MouseEventArgs e)
        {
        }

        public virtual void OnMouseUp(MouseEventArgs e)
        {
        }

        public virtual void OnMouseWheel(MouseEventArgs e)
        {
        }

        //
        public bool Enabled 
        {
            get { return fEnabled; }
            set { fEnabled = value; } 
        }
        
        public bool Active 
        {
            get { return fActive; }
            set { fActive = value; } 
        }

        // From IDrawable
        public virtual IGraphPort GraphPort
        {
            get
            {
                if (null != fPlatformWindow)
                    return fPlatformWindow.GraphPort;
                else
                    return null;
            }
        }

        public virtual void CreateComponents()
        {
        }

        public virtual void DrawInto(IGraphPort graphPort)
        {
        }

        public virtual void OnPaint(DrawEventArgs pea)
		{
            DrawInto(pea.GraphDevice);
        }


		public virtual void OnCommand(int cmd)
		{
		}

		// This gets called from the platform window once
		// the window has been created and is ready to go.
        // This may happen before this object's constructor
        // has finished though.
		public virtual void OnCreated()
		{
		}

		// This routine is called after the window has 
		// been destroyed.  It is time to clean up any
		// specific resources.
        public virtual void OnDestroyed()
        {
        }

		// OnQuit()
		// This is called after all the fun has happened.  The
		// message loop has finished, and this is our last chance
		// to do something interesting.
		public virtual void OnQuit()
		{
		}
		
		public virtual bool OnCloseRequested()
		{
			return true;	// By default, it's OK to close	
		}

		public virtual void OnSetFocus()
		{
		}

		public virtual void OnKillFocus()
		{
		}

		public virtual void OnTimer()
		{
		}

		// Things we need to tell the platform specific window

        public virtual void Invalidate()
        {
            fPlatformWindow.Invalidate();
        }

        public virtual void Invalidate(Rectangle rect)
        {
            fPlatformWindow.Invalidate(rect);
        }

        public virtual void Validate()
        {
            fPlatformWindow.Validate();
        }

        public virtual void CaptureMouse() { }
        public virtual void ReleaseMouse() { }

        public virtual bool SetWindowAlpha(byte alpha)
        {
            return fPlatformWindow.SetWindowAlpha(alpha);
        }

        public virtual void Show()
        {
            fPlatformWindow.Show();
        }

        public virtual void Hide()
        {
            fPlatformWindow.Hide();
        }

        public virtual void StartTimer(uint millis)
        {
            fPlatformWindow.StartTimer(millis);
        }

        public virtual void StopTimer()
        {
            fPlatformWindow.StopTimer();
        }


        /// <summary>
        /// Drawing the background of the window
        /// </summary>
        /// <param name="graphPort"></param>
        /// 
        public uint BackgroundColor
        {
            get { return fBackgroundColor; }
            set { fBackgroundColor = value; }
        }

        public virtual int OnEraseBackground(IGraphPort aPort)
        {
            //GraphPort.DrawingColor = BackgroundColor;
            //GraphPort.FillRectangle(fOrigin.X, fOrigin.Y, fDimension.Width, fDimension.Height);

            return 1;
        }

    }
