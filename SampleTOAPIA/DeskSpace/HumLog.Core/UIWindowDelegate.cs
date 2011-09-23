using System;
using System.Collections.Generic;
using TOAPI.Types;

using NewTOAPIA.Drawing;
using NewTOAPIA.UI;

namespace HumLog
{
    public class UIWindowDelegate : IUIWindow
    {
        public event DrawEventHandler DrawEvent;

        public event MouseDownDelegate MouseDownEvent;
        public event MouseUpDelegate MouseUpEvent;
        public event MouseMoveDelegate MouseMoveEvent;
        public event MouseEnterDelegate MouseEnterEvent;
        public event MouseLeaveDelegate MouseLeaveEvent;
        public event MouseWheelDelegate MouseWheelEvent;

        public event ValidateSurfaceEventHandler ValidateEvent;
        public event InvalidateSurfaceEventHandler InvalidateEvent;

        public event MoveToEventHandler MoveToEvent;
        public event MoveByEventHandler MoveByEvent;

        protected UIWindowDelegate()
        {
        }


        //public virtual void Show()
        //{
        //    fIsVisible = true;
        //    if (fSurface != null)
        //        fSurface.Show();
        //}

        //public virtual void Hide()
        //{
        //    fSurface.Hide();
        //}


        public virtual void Draw(DrawEvent devent)
        {
            if (null != DrawEvent)
                DrawEvent(this,devent);
        }

        public virtual void OnDraw(DrawEvent devent)
        {
        }


        #region  Drawing Validation
        public virtual void Invalidate()
        {
            Invalidate(Frame);
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
        public virtual void OnMouseDown(MouseEventArgs e)
        {
            if (null != MouseDownEvent)
                MouseDownEvent(e);
        }

        public virtual void OnMouseEnter(MouseEventArgs e)
        {
            if (null != MouseEnterEvent)
                MouseEnterEvent(e);
        }

        public virtual void OnMouseHover(MouseEventArgs e)
        {
        }

        public virtual void OnMouseLeave(MouseEventArgs e)
        {
            if (null != MouseLeaveEvent)
                MouseLeaveEvent(e);
        }

        public virtual void OnMouseMove(MouseEventArgs e)
        {
            if (null != MouseMoveEvent)
                MouseMoveEvent(e);
        }

        public virtual void OnMouseUp(MouseEventArgs e)
        {
            if (null != MouseUpEvent)
                MouseUpEvent(e);
        }

        public virtual void OnMouseWheel(MouseEventArgs e)
        {
            if (null != MouseWheelEvent)
                MouseWheelEvent(e);
        }
        #endregion

        #region IMoveable
        public virtual void MoveTo(int x, int y)
        {
            fSurface.MoveTo(x, y);
        }

        public virtual void MoveBy(int dx, int dy)
        {
            fSurface.MoveBy(dx, dy);
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
