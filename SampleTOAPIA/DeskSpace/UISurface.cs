using System;
using System.Collections.Generic;
using System.Text;

using TOAPI.Types;

using NewTOAPIA.Drawing;
using NewTOAPIA.UI;

namespace HamSketch
{
    public delegate void ValidateDelegate(Guid aSurface);
    public delegate void InvalidateDelegate(Guid aSurface, RECT aRect);

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

        public event ValidateDelegate ValidateEvent;
        public event InvalidateDelegate InvalidateEvent;

        public event MoveToEventHandler MoveToEvent;
        public event MoveByEventHandler MoveByEvent;

        Guid fUniqueID;
        string fTitle;
        RECT fFrame;
        PixelBuffer fBackingBuffer;
        GraphPortDelegate fGraphDelegate;

        public static Dictionary<Guid, UISurface> gSurfaces;

        static UISurface()
        {
            // Initialize the dictionary of graphic surfaces
            gSurfaces = new Dictionary<Guid, UISurface>();

        }

        public static UISurface CreateSurface(string title, RECT frame)
        {
            UISurface newSurface = new UISurface(title, frame);

            return newSurface;
        }

        protected UISurface(string title, RECT frame)
            : this(title, frame.Left, frame.Top, frame.Width, frame.Height, Guid.NewGuid())
        {
        }

        protected UISurface(string title, int x, int y, int width, int height, Guid uniqueID)
        {
            fTitle = title;
            fFrame = new RECT(x, y, width, height);
            fBackingBuffer = new PixelBuffer(width, height);
            fGraphDelegate = new GraphPortDelegate();
            fGraphDelegate.AddGraphPort(fBackingBuffer.GraphPort);

            fUniqueID = uniqueID;

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

        public Guid UniqueID
        {
            get { return fUniqueID; }
        }

        public virtual void Draw(DrawEvent devent)
        {
            if (null != DrawEvent)
                DrawEvent(this, devent);
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
            Invalidate(Frame);
        }

        public virtual void Invalidate(RECT rect)
        {
            if (null != InvalidateEvent)
                InvalidateEvent(this.UniqueID, rect);
        }

        public virtual void Validate()
        {
            if (null != ValidateEvent)
                ValidateEvent(this.UniqueID);
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
