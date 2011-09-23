using System;
using System.Drawing;
using System.Net;

using NewTOAPIA;
using NewTOAPIA.UI;         // Window
using NewTOAPIA.Net.Rtp;    // PayloadChannel, MultiSession
using NewTOAPIA.Drawing;    // PixelBuffer24

using DistributedDesktop;           // IUIPort, HostForm, UIPortDelegate, GraphPortChunkDecoder

namespace DeskView
{
    public partial class Form1 : Window
    {
        #region Private Fields
        MultiSession fSession;                  // The session we connect to
        PayloadChannel fSketchChannel;          // The channel we listen to
        GraphPortChunkDecoder fChunkDecoder;    // Decode the incoming packets

        PayloadChannel fUserIOChannel;
        UserIOChannelDecoder fUserIODecoder;
        UserIOChannelEncoder fUserIOEncoder;

        GDIDIBSection fBackingBuffer;           // The pixel buffer that receives the images
        Point fCursorLocation;

        bool fAutoScale;
        #endregion


        #region Constructor
        public Form1()
            : base("Desktop Viewer", 20, 20, 640, 480)
        {
            //fAutoScale = true;

            // Create the backing buffer to retain the image
            fBackingBuffer = new GDIDIBSection(1600, 1200);

            // 1.  Show a dialog box to allow the user to type in the 
            // group IP address and port number.
            HostForm groupForm = new HostForm();
            groupForm.ShowDialog();

            // 2. Get the address and port from the form, and use
            // them to setup the MultiSession object
            string groupIP = groupForm.groupAddressField.Text;
            int groupPort = int.Parse(groupForm.groupPortField.Text);
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(groupIP), groupPort);

            Title = "Desktop Viewer - " + ipep.ToString();

            fSession = new MultiSession(Guid.NewGuid().ToString(), ipep);
            fSketchChannel = fSession.CreateChannel(PayloadType.dynamicPresentation);

            // 3. Setup the chunk decoder so we can receive new images
            // when they come in
            fChunkDecoder = new GraphPortChunkDecoder(fBackingBuffer, fSketchChannel);
            fChunkDecoder.PixBltPixelBuffer24Handler += this.PixBltPixelBuffer24;
            fChunkDecoder.PixBltLumbHandler += this.PixBltLum24;

            fUserIOChannel = fSession.CreateChannel(PayloadType.xApplication2);
            fUserIOEncoder = new UserIOChannelEncoder(fUserIOChannel);
            fUserIODecoder = new UserIOChannelDecoder(fUserIOChannel);
            fUserIODecoder.MoveCursorEvent += MoveCursor;
        }
        #endregion

        /// <summary>
        /// This is a response to the host telling us where the cursor is currently
        /// located.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public virtual void MoveCursor(int x, int y)
        {
            //Console.WriteLine("Cursor X: {0}  Y: {1}", x, y);
            // Draw a cursor to indicate where it is located
            fCursorLocation = new Point(x, y);
            
            // Draw the cursor
            ClientAreaGraphPort.FillRectangle(GDISolidBrush.BlackBrush, new Rectangle(fCursorLocation.X, fCursorLocation.Y, 8, 8));
            ClientAreaGraphPort.Flush();
        }

        /// <summary>
        /// This method is called through an Event dispatch.  It is registered with 
        /// the DataChangedEvent of the viewer class.  It should be raised whenever
        /// a new set of drawing commands hits to viewer from the network.
        /// </summary>
        /// 
        public virtual void PixBltPixelBuffer24(GDIDIBSection pixMap, int x, int y)
        {
            fBackingBuffer = pixMap;

            Rectangle srcRect = new Rectangle(0, 0, pixMap.Width, pixMap.Height);

            // If there is a cursor currently being shown,
            // then we should draw the cursor on top of the
            // current image before displaying the image
            //aImage.DrawCursor(x, y)

            if (fAutoScale)
                DeviceContextClientArea.AlphaBlend(pixMap.DeviceContext, srcRect, ClientRectangle, 255);
            else
            {
                DeviceContextClientArea.BitBlt(pixMap.DeviceContext, new Point(0, 0), ClientRectangle, TernaryRasterOps.SRCCOPY);

                // Draw the cursor
                ClientAreaGraphPort.FillRectangle(GDISolidBrush.BlackBrush, new Rectangle(fCursorLocation.X, fCursorLocation.Y, 8, 8));
                ClientAreaGraphPort.Flush();
            }
        }

        /// <summary>
        /// This method is called through an Event dispatch.  It is registered with 
        /// the DataChangedEvent of the viewer class.  It should be raised whenever
        /// a new set of drawing commands hits to viewer from the network.
        /// </summary>
        /// 
        public virtual void PixBltLum24(PixelArray<Lumb> pixMap, int x, int y)
        {
            //fBackingBuffer = pixMap;
            // Copy luminance back into pixel buffer
            if (fBackingBuffer.Width != pixMap.Width || fBackingBuffer.Height != pixMap.Height)
                fBackingBuffer = new GDIDIBSection(pixMap.Width, pixMap.Height);

            PixmapTransfer.Copy(fBackingBuffer, pixMap);

            Rectangle srcRect = new Rectangle(0, 0, pixMap.Width, pixMap.Height);

            if (fAutoScale)
                DeviceContextClientArea.AlphaBlend(fBackingBuffer.DeviceContext, srcRect, ClientRectangle, 255);
            else
            {
                DeviceContextClientArea.BitBlt(fBackingBuffer.DeviceContext, new Point(0, 0), new Rectangle(0, 0, pixMap.Width, pixMap.Height), TernaryRasterOps.SRCCOPY);
                DeviceContextClientArea.ClearToWhite();

                // Draw the cursor
                //ClientAreaGraphPort.FillRectangle(GDISolidBrush.BlackBrush, new Rectangle(fCursorLocation.X, fCursorLocation.Y, 8, 8));
                //ClientAreaGraphPort.Flush();
            }
        }
        /// <summary>
        /// We override OnPaint so that when redrawing is not necessarily occuring, we will still refresh
        /// areas of the window that need refreshing, from the bitmap.
        /// </summary>
        /// <param name="devent"></param>
        public override void OnPaint(DrawEvent devent)
        {
            if (fBackingBuffer != null)
            {
                Rectangle srcRect = new Rectangle(0, 0, fBackingBuffer.Width, fBackingBuffer.Height);

                if (fAutoScale)
                    DeviceContextClientArea.AlphaBlend(fBackingBuffer.DeviceContext, srcRect, ClientRectangle, 255);
                else
                    DeviceContextClientArea.BitBlt(fBackingBuffer.DeviceContext, new Point(0, 0), ClientRectangle, TernaryRasterOps.SRCCOPY);
            }
        }

        /// <summary>
        /// When the Window is closing, we shut down the session object to clean up 
        /// participation in the session correctly.
        /// </summary>
        /// <returns>Always return 'true' indicating no objection to closing down.</returns>
        public override bool OnCloseRequested()
        {
            fSession.Dispose();

            return true;
        }

        public override void OnMouseActivity(object sender, MouseActivityArgs ma)
        {
            fUserIOEncoder.MouseActivity(sender, ma);

            base.OnMouseActivity(sender, ma);
        }

        public override IntPtr OnKeyboardActivity(object sender, KeyboardActivityArgs kbda)
        {
            switch (kbda.AcitivityType)
            {
                // Only send down/up, not the keychar or we'll get doubles
                case KeyActivityType.KeyDown:
                case KeyActivityType.KeyUp:
                case KeyActivityType.SysKeyDown:
                case KeyActivityType.SysKeyUp:
                    fUserIOEncoder.KeyboardActivity(sender, kbda);
                    break;

                default:
                    //base.OnKeyboardActivity(sender, kbda);
                    break;
            }

            return IntPtr.Zero;
        }

        public override IntPtr OnKeyUp(KeyboardActivityArgs ke)
        {
            switch (ke.VirtualKeyCode)
            {
                case VirtualKeyCodes.S:
                    fAutoScale = !fAutoScale;
                    Invalidate();
                    break;
            }

            return base.OnKeyUp(ke);
        }
    }
}
