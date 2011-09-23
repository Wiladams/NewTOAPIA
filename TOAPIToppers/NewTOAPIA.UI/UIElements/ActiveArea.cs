using System;
using System.Drawing;

namespace NewTOAPIA.UI
{

/*
 * The idea behind a ActiveArea is that you may want to point out different areas
 * on the screen that are 'active', meaning they can respond to mouse clicks, but
 * they aren't part of a particular 'control'.  This might be the case where you have
 * a graphic displayed, like that of a map, and you want to be able to click on the 
 * various areas and have a message sent out in response.
 * 
 * To do that, you would setup a ActiveArea to the frame of the area you care to 
 * do mouse tracking with, and do whatever is necessary when the user clicks.
 * 
 * Another generalization is that a active area is a multi state control with 
 * no graphic representation per control state.
 * 
 */
	public class ActiveArea : GraphicGroup, IInteractor
    {
        #region Events
        public event MouseEventHandler MouseDownEvent;
        public event MouseEventHandler MouseUpEvent;
        public event MouseEventHandler MouseMoveEvent;
        #endregion

        #region Fields
        bool fIsDepressed;
		bool	fTracking;
		int	fLastMouseX;
        int fLastMouseY;

		ICursor fCursor;
        #endregion

        #region Constructors
        public ActiveArea(string name, Rectangle rect)
            : this(name, rect.Left, rect.Top, rect.Width, rect.Height)
        {
        }
        
        public ActiveArea(string name, int x, int y, int width, int height)
			: base(name, x, y, width, height)
		{
            Enabled = true;
			fIsDepressed = false;
			fTracking = false;
			fCursor = null ; // BUGBUG - Cursor.Arrow;
        }
        #endregion

        #region Properties
        protected bool Depressed
		{
			get {return fIsDepressed;}
			set {fIsDepressed = value;}
		}

		protected bool Tracking
		{
			get {return fTracking;}
			set {fTracking = value;}
		}

		protected int LastMouseX
		{
			get {return fLastMouseX;}
			set {fLastMouseX = value;}
		}

        protected int LastMouseY
		{
			get {return fLastMouseY;}
			set {fLastMouseY = value;}
		}

		public ICursor Cursor
		{
			get {return fCursor;}
			set 
			{
				fCursor = value;
				//BUGBUG
				// Cursor.Current = fCursor;
			}
        }
        #endregion

        protected virtual void SelectCursor()
		{
			//BUGBUG
			//Cursor.Current = Cursor.Arrow;
		}

		// Graphic hierarchy
		public override void OnGraphicAdded(IGraphic aGraphic)
		{
			// Layout the graphic again
			LayoutHandler.AddToLayout(aGraphic);
		}

		public override void OnMouseDown(MouseActivityArgs e)
		{
			int x = e.X;
			int y = e.Y;

			//Console.WriteLine("ActiveArea.OnMouseDown: {0} {1} - BEGIN \n", e.X, e.Y);
			Depressed = true;
			Tracking = true;

			fLastMouseX = x;
			fLastMouseY = y;

			// Draw myself Immediately
            Invalidate();
            //DrawInto(LastGraphDevice);

			if (MouseDownEvent != null)
				MouseDownEvent(this, e);

		}

		public override void OnMouseMove(MouseActivityArgs e)
		{
			//Console.WriteLine("ActiveArea.OnMouseMove: {0}", e.ToString());
			if (MouseMoveEvent != null)
				MouseMoveEvent(this, e);
		}

		public override void OnMouseUp(MouseActivityArgs e)
		{
			Depressed = false;
			Tracking = false;

			// Possibly Draw Myself Immediately
			Invalidate();
            //DrawInto(LastGraphDevice);

			if (MouseUpEvent != null)
				MouseUpEvent(this, e);

		}

		public override void OnMouseWheel(MouseActivityArgs e)
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
        public override void OnMouseEnter(MouseActivityArgs e)
        {
            //Console.WriteLine("ActiveArea.OnMouseEnter: {0}", e.ToString());
            fTracking = true;

            // Change the cursor to be the pointer
            SelectCursor();
        }

        public override void OnMouseHover(MouseActivityArgs e)
        {
            //Console.WriteLine("ActiveArea.OnMouseHover: {0}", e.ToString());
            //fTracking = true;

            // Change the cursor to be the pointer
            //SelectCursor();
        }

        public override void OnMouseLeave(MouseActivityArgs e)
        {
            //Console.WriteLine("ActiveArea.OnMouseLeave: {0}", e.ToString());
            fTracking = false;
            fIsDepressed = false;
        }
	}
}