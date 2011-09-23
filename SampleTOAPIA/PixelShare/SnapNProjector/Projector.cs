using System;
using System.Drawing;
using System.Net;

using NewTOAPIA.UI;
using NewTOAPIA.Net.Rtp;    // Classes used - PayloadChannel, RtpSender, MultiSession, RtpStream
using NewTOAPIA.Drawing;

using SnapNShare;

namespace SnapNProjector
{
    public partial class Projector : Window
    {
        // Conferencing stuff
        MultiSession fSession;

        /// <summary>
        /// This is the pixelbuffer that is receiving the updates to the image.
        /// </summary>
        SketchViewer fViewer;

        // Create the drawing context for the screen so we can draw
        GDIContext fDrawingContext;
        Rectangle fDestinationRect;

        public Projector()
            : base("Snap N Projector", 20, 20, 640, 480)
        {
            HostForm groupForm = new HostForm();
            groupForm.ShowDialog();

            // Get the address and port from the form
            string groupIP = groupForm.groupAddressField.Text;
            int groupPort = int.Parse(groupForm.groupPortField.Text);
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(groupIP), groupPort);

            
            fDrawingContext = GDIContext.CreateForDesktopBackground();
            fDestinationRect = new Rectangle(0, 0, fDrawingContext.SizeInPixels.Width, fDrawingContext.SizeInPixels.Height);

            // Create the session
            fSession = new MultiSession(Guid.NewGuid().ToString(), ipep);

            fViewer = new SketchViewer(fSession, 1600, 1200);
            fViewer.DataChangedEvent += ReceiveImage;
        }

        /// <summary>
        /// This method is called through an Event dispatch.  It is registered with 
        /// the DataChangedEvent of the viewer class.  It should be raised whenever
        /// a new set of drawing commands hits to viewer from the network.
        /// </summary>
        void ReceiveImage(int x, int y, PixelBuffer24 aImage)
        {
            Rectangle srcRect = new Rectangle(0, 0, aImage.Width, aImage.Height);
            Rectangle dstRect = new Rectangle(x, y, aImage.Width, aImage.Height);

            //fDrawingContext.BitBlt(aImage.DeviceContext, new Point(0, 0), new Rectangle(x, y, aImage.Width, aImage.Height), TernaryRasterOps.SRCCOPY);
            fDrawingContext.AlphaBlend(aImage.DeviceContext, srcRect, fDestinationRect, 255);
        }

    }
}
