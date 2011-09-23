using System;
using System.Net;

using NewTOAPIA.Net.Rtp;    // Classes used - RtpSession, RtpSender, RtpParticipant, RtpStream
using NewTOAPIA.Drawing;
using NewTOAPIA;
using NewTOAPIA.GL;
using SnapNShare;

namespace Arena
{
    public class SketchViewer
    {
        // Conferencing stuff
        MultiSession fSession;
        PayloadChannel fSketchChannel;

        GraphPortChunkDecoder fChunkDecoder;

        GDIDIBSection backingBuffer;
        DynamicTexture fDynamicTexture;

        public SketchViewer(GraphicsInterface gi, int width, int height)
        {
            // Create the backing buffer to retain the image
            backingBuffer = new GDIDIBSection(width, height);
            //backingBuffer.DeviceContext.ClearToWhite();

            fDynamicTexture = new DynamicTexture(gi, width, height, 3);

            // Create the session
            fSession = new MultiSession(Guid.NewGuid().ToString(), new IPEndPoint(IPAddress.Parse("234.5.7.15"), 5004));
            fSketchChannel = fSession.CreateChannel(PayloadType.Whiteboard);

            // 3. Setup the chunk decoder so we can receive new images
            // when they come in
            fChunkDecoder = new GraphPortChunkDecoder(backingBuffer, fSketchChannel);
            fChunkDecoder.PixBltPixelBuffer24Handler += this.PixBltPixelBuffer24;
            fChunkDecoder.PixBltLumbHandler += this.PixBltLum24;
        }

        public virtual void PixBltPixelBuffer24(GDIDIBSection pixMap, int x, int y)
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
        //    fChunkDecoder.ReceiveData(ea.Frame);
        //}

        public GLTexture TexTure
        {
            get { return fDynamicTexture; }
        }

    }
}
