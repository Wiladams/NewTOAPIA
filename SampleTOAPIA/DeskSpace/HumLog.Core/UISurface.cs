using System;
using System.Collections.Generic;
using System.Text;

using TOAPI.Types;

using NewTOAPIA.Drawing;
using NewTOAPIA.UI;

namespace HumLog
{

    // A UISurface is an ActiveArea with a backing store.  It exposes the IGraphPort for the backing
    // store so that drawing into the area can occur directly.  This class is the basic formation
    // of a "Window" that can be remotely displayed and interacted with.
    public class UISurface : IDrawable, IMouseTracker, IMoveable
    {
        public event DrawEventHandler DrawEvent;

        public event MouseDownDelegate MouseDownEvent;
        public event MouseUpDelegate MouseUpEvent;
        public event MouseMoveDelegate MouseMoveEvent;
        public event MouseEnterDelegate MouseEnterEvent;
        public event MouseLeaveDelegate MouseLeaveEvent;
        public event MouseWheelDelegate MouseWheelEvent;

        //public event ValidateSurfaceEventHandler ValidateSurfaceEvent;
        //public event InvalidateSurfaceRectEventHandler InvalidateSurfaceRectEvent;

        public event MoveToEventHandler MoveToEvent;
        public event MoveByEventHandler MoveByEvent;

        Guid fUniqueID;
        string fTitle;
        RECT fFrame;
        RECT fBounds;
        PixelBuffer fBackingBuffer;
        GraphPortDelegate fGraphDelegate;
        bool fIsVisible;

        public static Dictionary<Guid, UISurface> gSurfaces;

        static UISurface()
        {
            // Initialize the dictionary of graphic surfaces
            gSurfaces = new Dictionary<Guid, UISurface>();

        }

        public static UISurface CreateSurface(string title, RECT frame, Guid uniqueID)
        {
            UISurface newSurface = new UISurface(title, frame, uniqueID);
            newSurface.ClearToColor(RGBColor.LtGray);

            return newSurface;
        }

        protected UISurface(string title, RECT frame, Guid uniqueID)
            : this(title, frame.Left, frame.Top, frame.Width, frame.Height, uniqueID)
        {
        }

        protected UISurface(string title, int x, int y, int width, int height, Guid uniqueID)
        {
            fTitle = title;
            fFrame = new RECT(x, y, width, height);
            fBounds = new RECT(0, 0, width, height);
            fBackingBuffer = new PixelBuffer(width, height);
            fGraphDelegate = new GraphPortDelegate();
            fGraphDelegate.AddGraphPort(fBackingBuffer.GraphPort);

            fUniqueID = uniqueID;
            fIsVisible = false;

            UISurface.gSurfaces.Add(uniqueID, this);
        }

        public RECT Frame
        {
            get { return fFrame; }
        }

        public GraphPortDelegate GraphPort
        {
            get { return fGraphDelegate; }
        }

        public virtual bool IsVisible
        {
            get { return fIsVisible; }
        }

        public virtual PixelBuffer PixelBuffer
        {
            get { return fBackingBuffer; }
        }

        public virtual void Show()
        {
            fIsVisible = true;
            Invalidate();
        }

        public virtual void Hide()
        {
            fIsVisible = false;
            Invalidate();
        }

        public Guid UniqueID
        {
            get { return fUniqueID; }
        }

        public virtual void Draw(DrawEvent devent)
        {
            if (null != DrawEvent)
            {
                DrawEvent(this, devent);
            }
            else
            {
                OnDraw(devent);
            }
        }

        public virtual void OnDraw(DrawEvent devent)
        {
            IGraphPort graphPort = devent.GraphPort;
            RECT clipRect = devent.ClipRect;
            int dstX = fFrame.Left + clipRect.Left;
            int dstY = fFrame.Top + clipRect.Top;
            int dstWidth = clipRect.Width;
            int dstHeight = clipRect.Height;

            int srcX = clipRect.Left;
            int srcY = clipRect.Top;
            int srcWidth = clipRect.Width;
            int srcHeight = clipRect.Height;

            //graphPort.AlphaBlend(dstX, dstY, dstWidth, dstHeight,
            //    fBackingBuffer, srcX, srcY, srcWidth, srcHeight, 127);
            graphPort.ScaleBitmap(fBackingBuffer, fFrame);
            graphPort.Flush();
        }

        public virtual void RefreshDisplay(RECT rect)
        {
            MetaSpace.RefreshSurface(UniqueID, rect);
        }

        public virtual void ClearToColor(uint colorref)
        {
            GraphPort.UseDefaultPen();
            GraphPort.UseDefaultBrush();
            GraphPort.SetDefaultBrushColor(colorref);
            GraphPort.SetDefaultPenColor(colorref);
            GraphPort.Rectangle(0, 0, fFrame.Width - 1, fFrame.Height - 1);

            //Invalidate();
        }

        #region  Drawing Validation
        public virtual void Invalidate()
        {
            Invalidate(fBounds);
        }

        public virtual void Invalidate(RECT rect)
        {
            MetaSpace.InvalidateSurface(this.UniqueID, rect);
        }

        public virtual void Validate()
        {
            //MetaSpace.ValidateSurface(this.UniqueID);
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
            if (null != MoveToEvent)
                MoveToEvent(this, x, y);
        }

        public virtual void MoveBy(int dx, int dy)
        {
            if (null != MoveByEvent)
                MoveByEvent(this, dx, dy);
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
