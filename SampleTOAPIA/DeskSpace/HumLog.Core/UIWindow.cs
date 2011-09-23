using System;
using System.Collections.Generic;
using TOAPI.Types;

using NewTOAPIA.Drawing;
using NewTOAPIA.UI;

namespace HumLog
{
    public class UIWindow : IUIWindow
    {
        //public event DrawEventHandler DrawEvent;

        public event MouseDownDelegate MouseDownEvent;
        public event MouseUpDelegate MouseUpEvent;
        public event MouseMoveDelegate MouseMoveEvent;
        public event MouseEnterDelegate MouseEnterEvent;
        public event MouseLeaveDelegate MouseLeaveEvent;
        public event MouseWheelDelegate MouseWheelEvent;

        //public event ValidateSurfaceEventHandler ValidateEvent;
        //public event InvalidateSurfaceRectEventHandler InvalidateEvent;

        //public event MoveToEventHandler MoveToEvent;
        //public event MoveByEventHandler MoveByEvent;

        UISurface fSurface;
        Guid fSurfaceID;

        string fTitle;
        RECT fFrame;
        RECT fBounds;
        GraphPortDelegate fGraphDelegate;
        bool fIsVisible;


        protected UIWindow(string title, int x, int y, int width, int height)
            :this(title, new RECT(x,y,width,height))
        {
        }

        protected UIWindow(string title, RECT frame)
        {
            fTitle = title;
            fFrame = frame;
            fBounds = new RECT(0, 0, frame.Width, frame.Height);
            fSurfaceID = MetaSpace.CreateSurface(title, frame, new OnSurfaceCreatedEventHandler(OnSurfaceCreated));
        }

        protected virtual void OnSurfaceCreated(Guid uniqueID)
        {
            fSurface = MetaSpace.GetSurface(uniqueID);

            fGraphDelegate = new GraphPortDelegate();
            fGraphDelegate.AddGraphPort(fSurface.GraphPort);

            AttachToUISurface();

            if (fIsVisible)
                fSurface.Show();
        }

        public virtual void AttachToUISurface()
        {
            AttachForKeyboard();
            AttachForMouse();
        }

        protected virtual void AttachForKeyboard()
        {

        }

        protected virtual void AttachForMouse()
        {
            // Basic mouse events
            fSurface.MouseDownEvent += new MouseDownDelegate(OnMouseDown);
            fSurface.MouseMoveEvent += new MouseMoveDelegate(OnMouseMove);
            fSurface.MouseUpEvent += new MouseUpDelegate(OnMouseUp);

            // Mouse wheel related
            fSurface.MouseWheelEvent += new MouseWheelDelegate(OnMouseWheel);

            // Mouse tracking events
            fSurface.MouseEnterEvent += new MouseEnterDelegate(OnMouseEnter);
            fSurface.MouseLeaveEvent += new MouseLeaveDelegate(OnMouseLeave);
        }

#region Properties
        public RECT Frame
        {
            get { return fFrame; }
        }

        //public virtual IGraphPort ImmediateGraphPort
        //{
        //    get { 
        //        return MetaSpace.GetImmediateGraphPort(); 
        //    }
        //}

        public GraphPortDelegate GraphPort
        {
            get { return fGraphDelegate; }
        }

        public virtual bool IsVisible
        {
            get { return fIsVisible; }
        }
#endregion

        public virtual void Show()
        {
            fIsVisible = true;
            if (fSurface != null)
                fSurface.Show();
        }

        public virtual void Hide()
        {
            fSurface.Hide();
        }


        public virtual void Draw(DrawEvent devent)
        {
            fSurface.Draw(devent);
        }

        public virtual void OnDraw(DrawEvent devent)
        {
        }


        #region  Drawing Validation
        public virtual void RefreshDisplay(RECT rect)
        {
            fSurface.RefreshDisplay(rect);
        }

        public virtual void Invalidate()
        {
            Invalidate(fBounds);
        }

        public virtual void Invalidate(RECT rect)
        {
            fSurface.Invalidate(rect);
        }

        public virtual void Validate()
        {
            fSurface.Validate();
        }

        #endregion

        #region IMouseTracker
        public virtual void OnMouseDown(MouseActivityArgs e)
        {
            if (null != MouseDownEvent)
                MouseDownEvent(e);
        }

        public virtual void OnMouseEnter(MouseActivityArgs e)
        {
            if (null != MouseEnterEvent)
                MouseEnterEvent(e);
        }

        public virtual void OnMouseHover(MouseActivityArgs e)
        {
        }

        public virtual void OnMouseLeave(MouseActivityArgs e)
        {
            if (null != MouseLeaveEvent)
                MouseLeaveEvent(e);
        }

        public virtual void OnMouseMove(MouseActivityArgs e)
        {
            if (null != MouseMoveEvent)
                MouseMoveEvent(e);
        }

        public virtual void OnMouseUp(MouseActivityArgs e)
        {
            if (null != MouseUpEvent)
                MouseUpEvent(e);
        }

        public virtual void OnMouseWheel(MouseActivityArgs e)
        {
            if (null != MouseWheelEvent)
                MouseWheelEvent(e);
        }
#endregion

        #region IMoveable
        public virtual void MoveTo(int x, int y)
        {
            fSurface.MoveTo(x,y);
        }

        public virtual void MoveBy(int dx, int dy)
        {
            fSurface.MoveBy(dx,dy);
        }

        public virtual void OnMovedTo(int x, int y)
        {
            fFrame.Location = new Point(x, y);
        }

        public virtual void OnMovedBy(int dx, int dy)
        {
            fFrame.Offset(dx, dy);
        }

        public virtual void OnMoving(int dx, int dy)
        {
        }

        #endregion
    }
   
}
