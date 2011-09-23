using System;
using System.Collections.Generic;

using TOAPI.Types;
using NewTOAPIA.Drawing;
using NewTOAPIA.UI;

namespace HumLog
{
    public class MetaSpace : Window, IControlSpace
    {
        List<UISurface> fSurfaces;
        UISurface fMouseTracker;
        SessionManager fSessionManager;
        SpaceControlChannel fControlChannel;
        GraphPortDelegate fWrappedGraphPort;
        PixelBuffer fBackingBuffer;

        #region Global Variables
        protected static MetaSpace gSharedSpace;
        protected static Dictionary<Guid, OnSurfaceCreatedEventHandler> gSurfaceCreationHandlers;
        #endregion

        static MetaSpace()
        {
            gSharedSpace = null;
            gSurfaceCreationHandlers = new Dictionary<Guid, OnSurfaceCreatedEventHandler>();
        }

        // Only one instace of a metaspace can exist within an application
        // context.
        public static MetaSpace Create(string title, RECT frame)
        {
            if (null == gSharedSpace)
            {
                gSharedSpace = new MetaSpace(title, frame);
            }

            return gSharedSpace;
        }

        protected MetaSpace(string title, RECT aframe)
            :base(title, aframe.Left, aframe.Top, aframe.Width, aframe.Height)
        {
            gSharedSpace = this;

            fSurfaces = new List<UISurface>();
            
            // Register with our own graph port so that we can hook up 
            // for some drawing commands.

            fBackingBuffer = new PixelBuffer(Frame.Width, aframe.Height);

            fWrappedGraphPort = new GraphPortDelegate();
            fWrappedGraphPort.AddGraphPort(fBackingBuffer.GraphPort);
            fWrappedGraphPort.AddGraphPort(base.GraphPort);
            //fWrappedGraphPort.AddGraphPort(base.GraphPort);
            //fWrappedGraphPort.AlphaBlendHandler += new AlphaBlend(AlphaBlend);

            // Add the channel for commands
            fSessionManager = new SessionManager();
            fControlChannel = new SpaceControlChannel(fSessionManager, fSessionManager.UniqueSessionName, this);
        }

        #region Properties
        public static IGraphPort GetImmediateGraphPort()
        {
            if (null != gSharedSpace)
                return gSharedSpace.ImmediateGraphPort;

            return null;
        }

        public virtual IGraphPort ImmediateGraphPort
        {
            get { return base.GraphPort; }
        }

        public override IGraphPort GraphPort
        {
            get
            {
                return fWrappedGraphPort;
            }
        }
        #endregion

        #region Surface Management
        #region Surface Creation
        public static Guid CreateSurface(string title, RECT frame, OnSurfaceCreatedEventHandler handler)
        {
            // Create the uniqueID for the surface so we can pass
            // this 'handle' back to the caller.
            Guid uniqueID = Guid.NewGuid();

            // Save the uniqueID and handler in a dictionary for later usage
            gSurfaceCreationHandlers.Add(uniqueID, handler);

            // Create the surface using the global instance of the MetaSpace.
            gSharedSpace.CreateSurface(title, frame, uniqueID);

            return uniqueID;
        }

        public static void CreateUISurface(string title, RECT frame, Guid uniqueID)
        {
            gSharedSpace.CreateSurface(title, frame, uniqueID);
        }

        public virtual void CreateSurface(string title, RECT frame, Guid uniqueID)
        {
            ((SpaceCommandEncoder)fControlChannel).CreateSurface(title, frame, uniqueID);
        }

        public virtual void OnCreateSurface(string title, RECT frame, Guid uniqueID)
        {
            // First see if the uniqueID already exists 
            UISurface aSurface = GetSurface(uniqueID);

            if (null == aSurface)
            {
                // If the surface does not already exist, then create it
                // Create the surface and remember it for painting
                aSurface = UISurface.CreateSurface(title, frame, uniqueID);
            }

            // register it in the dictionary to associate drawing commands for it.
            OnAddUISurface(aSurface);

            OnSurfaceCreated(uniqueID);
        }

        public virtual void OnSurfaceCreated(Guid uniqueID)
        {
            // After the surface is created
            // Try to call the callback function that was left
            // behind for it.
            if (gSurfaceCreationHandlers.ContainsKey(uniqueID))
            {
                OnSurfaceCreatedEventHandler handler = null;
                handler = gSurfaceCreationHandlers[uniqueID];

                // Don't call the callback if it's null
                if (null != handler)
                    handler(uniqueID);
            }
        }
        #endregion
        public virtual void AddUISurface(UISurface aSurface)
        {
        }

        public virtual void OnAddUISurface(UISurface aSurface)
        {
            fSurfaces.Add(aSurface);

            // Register to know when drawing occurs
            //aSurface.DrawEvent += new DrawEventHandler(Surface_DrawEvent);
            //aSurface.GraphPort.AlphaBlendHandler += new AlphaBlend(AlphaBlend);

            // Register to receive some of the surfaces events
            //aSurface.InvalidateEvent += new InvalidateSurfaceRectEventHandler(InvalidateSurface);
            //aSurface.ValidateEvent += new ValidateSurfaceEventHandler(ValidateSurface);

            aSurface.MoveByEvent += new MoveByEventHandler(Surface_MoveByEvent);
            aSurface.MoveToEvent += new MoveToEventHandler(Surface_MoveToEvent);

            Invalidate();
        }

        public static UISurface GetSurface(Guid uniqueID)
        {
            if (UISurface.gSurfaces.ContainsKey(uniqueID))
                return UISurface.gSurfaces[uniqueID];

            return null;
        }


        #region Surface Validation
        public static void RefreshSurface(Guid surfaceID, RECT aRect)
        {
            // Use the immediate mode, as Refresh is meant to be a local-only
            // version of "InvalidateSurface".
            gSharedSpace.OnInvalidateSurfaceRect(surfaceID, aRect);
        }

        public virtual void ValidateSurface(Guid aGuid)
        {
        }

        public virtual void OnValidateSurface(Guid surfaceID)
        {
        }

        public static void InvalidateSurface(Guid surfaceID, RECT aRect)
        {
            gSharedSpace.InvalidateSurfaceRect(surfaceID, aRect);
        }

        public virtual void InvalidateSurfaceRect(Guid surfaceID, RECT aRect)
        {
            //fControlChannel.InvalidateSurfaceRect(surfaceID, aRect);

            UISurface aSurface = GetSurface(surfaceID);
            if (null == aSurface)
                return;

            // Make sure the surface draws into our graph port
            DrawEvent devent = new DrawEvent(GraphPort, aRect);

            aSurface.Draw(devent);

            RECT iRect = aRect;
            iRect.Offset(aSurface.Frame.Left, aSurface.Frame.Top);

            // Get a clone bitmap from the MetaSpace backing store
            // that is the size of the updated rectangle from
            // the surface.

            // Send this clone out as a copy to the network
            CopyPixels(iRect.Left, iRect.Top, iRect.Width, iRect.Height, fBackingBuffer);
        }

        public virtual void OnInvalidateSurfaceRect(Guid aGuid, RECT aRect)
        {
            // Redraw the surface that needs drawing
            UISurface aSurface = GetSurface(aGuid);
            if (null == aSurface)
                return;

            DrawEvent devent = new DrawEvent(GraphPort, aRect);

            aSurface.Draw(devent);

            // Invalidate the appropriate portion of the local window
            RECT iRect = aRect;
            iRect.Offset(aSurface.Frame.Left, aSurface.Frame.Top);

            GraphPort.AlphaBlend(iRect.Left, iRect.Top, iRect.Width, iRect.Height, fBackingBuffer,
                iRect.Left, iRect.Top, iRect.Width, iRect.Height, 255);
        }
        #endregion
        #endregion



        #region Global Methods
        //public virtual void BitBlt(int x, int y, PixelBuffer pixBuff)
        //{
        //    fControlChannel.BitBlt(x, y, pixBuff);
        //}

        public virtual void OnBitBlt(int x, int y, PixelBuffer pixBuff)
        {
            base.GraphPort.BitBlt(x, y, pixBuff);
        }

        public virtual void CopyPixels(int x, int y, int width, int height, PixelBuffer pixBuff)
        {
            ((SpaceCommandEncoder)fControlChannel).CopyPixels(x, y, width, height, pixBuff);
        }

        public virtual void OnCopyPixels(int x, int y, int width, int height, PixelBuffer pixBuff)
        {
            GraphPort.BitBlt(x, y, pixBuff);
            GraphPort.Flush();
            Invalidate(new RECT(x, y, width, height));
        }

        public virtual void AlphaBlend(int x, int y, int width, int height,
                PixelBuffer bitmap, int srcX, int srcY, int srcWidth, int srcHeight,
                byte alpha)
        {
            ((SpaceCommandEncoder)fControlChannel).AlphaBlend(x, y, width, height, bitmap, srcX, srcY, srcWidth, srcHeight, alpha);
        }

        public virtual void OnAlphaBlend(int x, int y, int width, int height, PixelBuffer bitmap, int srcX, int srcY, int srcWidth, int srcHeight, byte alpha)
        {
            base.GraphPort.AlphaBlend(x, y, width, height, bitmap, srcX, srcY, srcWidth, srcHeight, alpha);
        }

        public virtual void ScaleBitmap(PixelBuffer aBitmap, RECT aFrame)
        {
            ((SpaceCommandEncoder)fControlChannel).ScaleBitmap(aBitmap, aFrame);
        }

        public virtual void MouseEvent(Object sender, MouseActivityArgs e)
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


        public override void OnPaint(DrawEvent pea)
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
                DrawEvent devent = new DrawEvent(fBackingBuffer.GraphPort, surfaceClip);
                surf.Draw(devent);
            }

            // After all the children have done their drawing into our backing buffer
            // Display the backing buffer on the screen.
            int x = pea.ClipRect.Left;
            int y = pea.ClipRect.Top;
            int width = pea.ClipRect.Width;
            int height = pea.ClipRect.Height;

            // Render our image to the actual screen
            base.GraphPort.AlphaBlend(x, y, width, height, fBackingBuffer, x, y, width, height, 255);
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
        public override void MouseActivity(object sender, MouseActivityArgs e)
        {
            // Use the controller if you want mouse activity
            // to go to every participant.
            //fControlChannel.MouseActivity(sender, e);
            
            // Do it this way if you want to short circuit
            // sending mouse commands out.
            OnMouseActivity(sender, e);
        }

        public override void OnMouseActivity(object sender, MouseActivityArgs e)
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
        public override void OnMouseDown(MouseActivityArgs e)
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
            MouseActivityArgs args = new MouseActivityArgs(0,MouseEventType.MouseDown,e.Button,e.Clicks,  
                (e.X-tracker.Frame.Left),	(e.Y-tracker.Frame.Top), e.Delta, e.Source);
            
            tracker.OnMouseDown(args);

            base.OnMouseDown(e);
        }

        public override void OnMouseMove(MouseActivityArgs e)
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
                MouseActivityArgs args = new MouseActivityArgs(0, MouseEventType.MouseMove, e.Button, e.Clicks,
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
                MouseActivityArgs args = new MouseActivityArgs(0, MouseEventType.MouseLeave, e.Button, e.Clicks,
                    e.X - fMouseTracker.Frame.Left, e.Y-fMouseTracker.Frame.Top, e.Delta, e.Source);

                fMouseTracker.OnMouseLeave(args);
            }

            fMouseTracker = tracker;

            if (fMouseTracker != null)
            {
                // Create a new mouse event with the point translated into the 
                // coordinate space of the client
                MouseActivityArgs args = new MouseActivityArgs(0, MouseEventType.MouseEnter, e.Button, e.Clicks,
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


        public override void OnMouseUp(MouseActivityArgs e)
        {
            //Console.WriteLine("MetaSpace.OnMouseUp - {0}", e.ToString());

            if (fMouseTracker != null)
            {
                MouseActivityArgs args = new MouseActivityArgs(0, MouseEventType.MouseUp, e.Button, e.Clicks,
                    (e.X-fMouseTracker.Frame.Left), (e.Y-fMouseTracker.Frame.Top), e.Delta, e.Source);

                fMouseTracker.OnMouseUp(args);
                fMouseTracker = null;
            } else
            {
                //Console.WriteLine("MetaSpace.OnMouseUp - no tracker found");
            }
        }

        public override void OnMouseWheel(MouseActivityArgs e)
        {
            base.OnMouseWheel(e);
        }

        public override void OnMouseEnter(MouseActivityArgs e)
        {
            base.OnMouseEnter(e);
        }

        public override void OnMouseHover(MouseActivityArgs e)
        {
            base.OnMouseHover(e);
        }

        public override void OnMouseLeave(MouseActivityArgs e)
        {
            base.OnMouseLeave(e);
        }
        #endregion
        #endregion

    }
}
