using System;
using System.Net;

using NewTOAPIA.Net.Rtp;    // Classes used - RtpSession, RtpSender, RtpParticipant, RtpStream
using NewTOAPIA.Drawing;
using NewTOAPIA;
using NewTOAPIA.GL;

using SnapNShare;

namespace SnapNGLView
{
    public class GLSketchViewer
    {
        // Conferencing stuff
        MultiSession fSession;
        PayloadChannel fSketchChannel;

        GraphPortChunkDecoder fChunkDecoder;

        PixelBuffer24 backingBuffer;
        SnapNGLView.DynamicTexture fDynamicTexture;

        public GLSketchViewer(GraphicsInterface gi, int width, int height)
        {
            fDynamicTexture = new SnapNGLView.DynamicTexture(gi, width, height, 3);

            // Create the backing buffer to retain the image
            backingBuffer = new PixelBuffer24(width, height);
            //backingBuffer.DeviceContext.ClearToWhite();

            // 1.  Show a dialog box to allow the user to type in the 
            // group IP address and port number.
            HostForm groupForm = new HostForm();
            groupForm.ShowDialog();

            // 2. Get the address and port from the form, and use
            // them to setup the MultiSession object
            string groupIP = groupForm.groupAddressField.Text;
            int groupPort = int.Parse(groupForm.groupPortField.Text);
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(groupIP), groupPort);

            // Create the session
            fSession = new MultiSession(Guid.NewGuid().ToString(), ipep);

            // Add the channel for graphics commands
            fSketchChannel = fSession.CreateChannel(PayloadType.Whiteboard);

            // 3. Setup the chunk decoder so we can receive new images
            // when they come in
            fChunkDecoder = new GraphPortChunkDecoder(backingBuffer, fSketchChannel);
            fChunkDecoder.PixBltPixelBuffer24Handler += this.PixBltPixelBuffer24;
            fChunkDecoder.PixBltLumbHandler += this.PixBltLum24;

            //fSketchChannel.FrameReceivedEvent += FrameReceived;

        }

        public virtual void PixBltPixelBuffer24(PixelBuffer24 pixMap, int x, int y)
        {
            fDynamicTexture.UpdateImage(new PixelAccessorBGRb(pixMap));
        }

        public virtual void PixBltLum24(PixelArray<Lumb> pixMap, int x, int y)
        {
            //fBackingGraphPort.PixBlt(pixMap, x, y);
        }

        //private void FrameReceived(object sender, RtpStream.FrameReceivedEventArgs ea)
        //{
        //    // Decode the frame
        //    fChunkDecoder.ReceiveChunk(ea.Frame);
        //}

        public GLTexture TexTure
        {
            get { return fDynamicTexture; }
        }

    }
}
