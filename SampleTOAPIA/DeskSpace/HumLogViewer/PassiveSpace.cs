using System;
using System.Collections.Generic;

using TOAPI.Types;
using NewTOAPIA.Drawing;
using NewTOAPIA.UI;

using HumLog;

namespace HumLogViewer
{
    public class PassiveSpace : Window, IReceiveSpaceControl
    {
        SessionManager fSessionManager;
        SpaceControlChannel fControlChannel;
        PixelBuffer fBackingBuffer;
        GraphPortDelegate fWrappedGraphPort;

        static PassiveSpace()
        {
        }

        public PassiveSpace(string title, RECT aframe)
            : base(title, aframe.Left,aframe.Top,aframe.Width,aframe.Height)
        {
            fBackingBuffer = new PixelBuffer(Frame.Width, aframe.Height);

            fWrappedGraphPort = new GraphPortDelegate();
            fWrappedGraphPort.AddGraphPort(fBackingBuffer.GraphPort);
            fWrappedGraphPort.AddGraphPort(base.GraphPort);

            // Add the channel for commands
            fSessionManager = new SessionManager();
            fControlChannel = new SpaceControlChannel(fSessionManager, fSessionManager.UniqueSessionName, this);
        }

        public override IGraphPort GraphPort
        {
            get
            {
                return fWrappedGraphPort;
            }
        }

        public override void OnPaint(DrawEvent pea)
        {
            // After all the children have done their drawing into our backing buffer
            // Display the backing buffer on the screen.
            int x = pea.ClipRect.Left;
            int y = pea.ClipRect.Top;
            int width = pea.ClipRect.Width;
            int height = pea.ClipRect.Height;

            // Render our image to the actual screen
            base.GraphPort.AlphaBlend(x, y, width, height, fBackingBuffer, x, y, width, height, 255);
        }

        #region Surface Management
        #region Surface Creation

        public virtual void OnCreateSurface(string title, RECT frame, Guid uniqueID)
        {
            // Do nothing
        }

        public virtual void OnSurfaceCreated(Guid uniqueID)
        {
            // Do nothing
        }
        #endregion


        #region Surface Validation
        public virtual void OnValidateSurface(Guid surfaceID)
        {
        }

        public virtual void OnInvalidateSurfaceRect(Guid aGuid, RECT aRect)
        {
            //// Redraw the surface that needs drawing
            //UISurface aSurface = GetSurface(aGuid);
            //if (null == aSurface)
            //    return;

            //DrawEvent devent = new DrawEvent(GraphPort, aRect);

            //aSurface.Draw(devent);

            //// Invalidate the appropriate portion of the local window
            //RECT iRect = aRect;
            //iRect.Offset(aSurface.Frame.Left, aSurface.Frame.Top);

            //GraphPort.AlphaBlend(iRect.Left, iRect.Top, iRect.Width, iRect.Height, fBackingBuffer,
            //    iRect.Left, iRect.Top, iRect.Width, iRect.Height, 255);
        }

        #endregion
        #endregion



        #region Bitmap Manipulation


        public virtual void OnCopyPixels(int x, int y, int width, int height, PixelBuffer pixBuff)
        {
            GraphPort.BitBlt(x, y, pixBuff);
            GraphPort.Flush();
            Invalidate(new RECT(x, y, width, height));
        }

        public virtual void OnAlphaBlend(int x, int y, int width, int height, PixelBuffer bitmap, int srcX, int srcY, int srcWidth, int srcHeight, byte alpha)
        {
            base.GraphPort.AlphaBlend(x, y, width, height, bitmap, srcX, srcY, srcWidth, srcHeight, alpha);
        }
#endregion

        #region Mouse Events
        //public override void MouseActivity(object sender, MouseEventArgs e)
        //{
        //    // Use the controller if you want mouse activity
        //    // to go to every participant.
        //    //fControlChannel.MouseActivity(sender, e);

        //    // Do it this way if you want to short circuit
        //    // sending mouse commands out.
        //    OnMouseActivity(sender, e);
        //}


        #endregion

    }
}
