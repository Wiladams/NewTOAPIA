using System;
using System.Collections.Generic;

using TOAPI.Types;
using NewTOAPIA.Drawing;
using NewTOAPIA.UI;

namespace HamSketch
{
    public class MetaSpace : Window, ISpaceControl
    {
        List<UISurface> fSurfaces;
        UISurface fMouseTracker;
        SessionManager fSessionManager;
        SpaceControlChannel fControlChannel;

        static MetaSpace gSharedSpace;

        static MetaSpace()
        {
            gSharedSpace = null;
        }

        protected MetaSpace(string title, RECT aframe)
            :base(title, aframe.Left, aframe.Top, aframe.Width, aframe.Height)
        {
            fSurfaces = new List<UISurface>();
            
            // Add the channel for commands
            fSessionManager = new SessionManager();
            fControlChannel = new SpaceControlChannel(fSessionManager, fSessionManager.UniqueSessionName, this);
        }

        public virtual UISurface CreateUISurface(string title, RECT frame)
        {
            // Create the surface and remember it for painting
            UISurface newSurface = UISurface.CreateSurface(title, frame);

            // register it in the dictionary to associate drawing commands for it.
            AddUISurface(newSurface);

            return newSurface;
        }

        public override void OnPaint(DrawEventArgs pea)
        {

            foreach (UISurface surf in fSurfaces)
            {
                // Create a draw event for each surface.

                // First intersect the clipping rectangle with the frame
                // of the surface.  If the intersection is empty, then just
                // continue on to the next surface.
                RECT surfaceClip = surf.Frame;
                surfaceClip.Intersect(pea.ClipRect);

                // If there is an intersecting section, then
                // adjust it so the surface receives a clipping rectangle in
                // it's local space coordinates.
                surfaceClip.Offset(surfaceClip.Left - surf.Frame.Left, surfaceClip.Top - surf.Frame.Top);

                // We now have a localized clipping rect, so pass along a new
                // DrawEvent with the new clipping rect.
                DrawEvent devent = new DrawEvent(pea.GraphDevice, surfaceClip);                
                surf.Draw(devent);
            }
        }

        public virtual void OnValidateSurface(Guid aGuid)
        {
        }

        public virtual void OnInvalidateSurface(Guid aGuid, RECT aRect)
        {
            // Redraw the surface that needs drawing
            if (UISurface.gSurfaces.ContainsKey(aGuid))
            {
                UISurface aSurface = UISurface.gSurfaces[aGuid];
                DrawEvent devent = new DrawEvent(GraphPort, aRect);

                aSurface.Draw(devent);

                // Since these calls are typically driven
                // by actions on the surface, it might be 
                // necessary to send them to the metaspace.
            }
        }

        #region Global Methods
        public virtual void BitBlt(int x, int y, PixelBuffer pixBuff)
        {
            GraphPort.BitBlt(x, y, pixBuff);
        }

        public virtual void AlphaBlend(int x, int y, int width, int height,
                PixelBuffer bitmap, int srcX, int srcY, int srcWidth, int srcHeight,
                byte alpha)
        {
            GraphPort.AlphaBlend(x, y, width, height, bitmap, srcX, srcY, srcWidth, srcHeight, alpha);
        }

        public virtual void ScaleBitmap(PixelBuffer aBitmap, RECT aFrame)
        {
            GraphPort.ScaleBitmap(aBitmap, aFrame);
        }

        public void MouseEvent(Object sender, MouseEventArgs e)
        {
            switch (e.EventType)
            {
                case MouseEventType.MouseDown:
                    OnMouseDown(e);
                    break;

                case MouseEventType.MouseMove:
                    OnMouseMove(e);
                    break;

                case MouseEventType.MouseUp:
                    OnMouseUp(e);
                    break;

                case MouseEventType.MouseEnter:
                    OnMouseEnter(e);
                    break;

                case MouseEventType.MouseHover:
                    OnMouseHover(e);
                    break;

                case MouseEventType.MouseLeave:
                    OnMouseLeave(e);
                    break;

                case MouseEventType.MouseWheel:
                    OnMouseWheel(e);
                    break;
            }
        }
        #endregion

        public virtual void AddUISurface(UISurface aSurface)
        {
            fSurfaces.Add(aSurface);

            // Register to know when drawing occurs
            aSurface.DrawEvent += new DrawEventHandler(Surface_DrawEvent);

            // Register to receive some of the surfaces events
            aSurface.InvalidateEvent += new InvalidateDelegate(OnInvalidateSurface);
            aSurface.ValidateEvent += new ValidateDelegate(OnValidateSurface);

            aSurface.MoveByEvent += new MoveByEventHandler(Surface_MoveByEvent);
            aSurface.MoveToEvent += new MoveToEventHandler(Surface_MoveToEvent);
        }

        void Surface_DrawEvent(object sender, DrawEvent devent)
        {
            ((IDrawable)sender).OnDraw(devent);
        }

        void Surface_MoveToEvent(object sender, int x, int y)
        {
            ((IMoveable)sender).OnMovedTo(x, y);
            Invalidate();
        }

        void Surface_MoveByEvent(object sender, int dx, int dy)
        {
            ((IMoveable)sender).OnMovedBy(dx, dy);
            Invalidate();
        }


        // return all the UISurfaces in the hierarchy that are hit by the point
        public virtual void UISurfacesAt(int x, int y, ref Stack<UISurface> coll)
        {
            foreach (UISurface surf in fSurfaces)
            {
                if (surf.Frame.Contains(x, y))
                {
                    coll.Push(surf);
                }
            }
        }

        public UISurface GetUISurfaceAt(int x, int y)
        {
            // First get a list of UI Surfaces that fall under the specified
            // point.  This could be done by going through the list of surfaces
            // in reverse order as the last one in the list is the last one drawn,
            // and thus the one most directly under the mouse.
            Stack<UISurface> surfaceStack = new Stack<UISurface>();
            UISurfacesAt(x, y, ref surfaceStack);

            // Return the topmost element on the stack if
            // it exists.
            if (surfaceStack.Count > 0)
                return surfaceStack.Pop();

            return null;
        }

        #region Mouse Events
        public override void MouseActivity(object sender, MouseEventArgs e)
        {
            fControlChannel.MouseActivity(sender, e);
        }

        public override void OnMouseActivity(object sender, MouseEventArgs e)
        {
            switch (e.EventType)
            {
                case MouseEventType.MouseDown:
                    OnMouseDown(e);
                    break;

                case MouseEventType.MouseMove:
                    OnMouseMove(e);
                    break;

                case MouseEventType.MouseUp:
                    OnMouseUp(e);
                    break;

                case MouseEventType.MouseEnter:
                    OnMouseEnter(e);
                    break;

                case MouseEventType.MouseHover:
                    OnMouseHover(e);
                    break;

                case MouseEventType.MouseLeave:
                    OnMouseLeave(e);
                    break;

                case MouseEventType.MouseWheel:
                    OnMouseWheel(e);
                    break;
            }
        }

        #region Reacting to the mouse
        public override void OnMouseDown(MouseEventArgs e)
        {
            //Console.WriteLine("MetaSpace.OnMouseDown - {0}", e.ToString());
            UISurface tracker = GetUISurfaceAt(e.X, e.Y);

            // If no tracker, then don't do anything
            // Really, we should let the window handle it, but 
            // for now, just return null
            if (tracker == null)
                return;

            fMouseTracker = tracker;

            // Create a new mouse event with the point translated into the 
            // coordinate space of the client
            MouseEventArgs args = new MouseEventArgs(0,MouseEventType.MouseDown,e.Button,e.Clicks,  
                (e.X-tracker.Frame.Left),	(e.Y-tracker.Frame.Top), e.Delta, e.Source);
            
            tracker.OnMouseDown(args);

            base.OnMouseDown(e);
        }

        public override void OnMouseMove(MouseEventArgs e)
        {
            //Console.WriteLine("MetaSpace.OnMouseMove - {0}", e.ToString());
            UISurface tracker = GetUISurfaceAt(e.X, e.Y);

            // If the current tracker is the same as who we've
            // already been tracking
            // then we just send it a mousemove event.
            if ((tracker != null) && (tracker == fMouseTracker))
            {
                // Create a new mouse event with the point translated into the 
                // coordinate space of the client
                MouseEventArgs args = new MouseEventArgs(0, MouseEventType.MouseMove, e.Button, e.Clicks,
                    (e.X-tracker.Frame.Left), (e.Y-tracker.Frame.Top), e.Delta, e.Source);

                fMouseTracker.OnMouseMove(args);
                
                return;
            }

            // If the tracker is different
            // Then the current tracker needs to receive the MouseExit event
            if (fMouseTracker != null)
            {
                // Create a new mouse event with the point translated into the 
                // coordinate space of the client
                MouseEventArgs args = new MouseEventArgs(0, MouseEventType.MouseLeave, e.Button, e.Clicks,
                    e.X - fMouseTracker.Frame.Left, e.Y-fMouseTracker.Frame.Top, e.Delta, e.Source);

                fMouseTracker.OnMouseLeave(args);
            }

            fMouseTracker = tracker;

            if (fMouseTracker != null)
            {
                // Create a new mouse event with the point translated into the 
                // coordinate space of the client
                MouseEventArgs args = new MouseEventArgs(0, MouseEventType.MouseEnter, e.Button, e.Clicks,
                    e.X-fMouseTracker.Frame.Left, e.Y-fMouseTracker.Frame.Top, e.Delta, e.Source);

                fMouseTracker.OnMouseEnter(args);
            }
            else
            {
                // Otherwise, no trackers have been hit, so we're just free floating
                // in the window.  Set the cursor to the generic arrow
                //Win32Cursor.Current = Win32Cursor.Arrow;
            }

            base.OnMouseMove(e);
        }


        public override void OnMouseUp(MouseEventArgs e)
        {
            //Console.WriteLine("MetaSpace.OnMouseUp - {0}", e.ToString());

            if (fMouseTracker != null)
            {
                MouseEventArgs args = new MouseEventArgs(0, MouseEventType.MouseUp, e.Button, e.Clicks,
                    (e.X-fMouseTracker.Frame.Left), (e.Y-fMouseTracker.Frame.Top), e.Delta, e.Source);

                fMouseTracker.OnMouseUp(args);
                fMouseTracker = null;
            } else
            {
                //Console.WriteLine("MetaSpace.OnMouseUp - no tracker found");
            }
        }

        public override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
        }

        public override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
        }

        public override void OnMouseHover(MouseEventArgs e)
        {
            base.OnMouseHover(e);
        }

        public override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
        }
        #endregion
        #endregion

    }
}
