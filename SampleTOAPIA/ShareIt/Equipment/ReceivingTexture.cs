using System;
using System.Net;

using NewTOAPIA;
using NewTOAPIA.Drawing;
using NewTOAPIA.Drawing.GDI;
using NewTOAPIA.GL;
using NewTOAPIA.Net.Rtp;

using PixelShare.Core;

namespace ShowIt
{
    public class ReceivingTexture : IHaveTexture, IReceiveDesktopFrame
    {
        GraphPortChunkDecoder fChunkDecoder;

        GDIDIBSection backingBuffer;
        DynamicTexture fDynamicTexture;

        public ReceivingTexture(GraphicsInterface gi, int width, int height)
        {
            // Create the backing buffer to retain the image
            backingBuffer = new GDIDIBSection(width, height);
            backingBuffer.DeviceContext.ClearToBlack();

            fDynamicTexture = new DynamicTexture(gi, width, height, 3);

            // 3. Setup the chunk decoder so we can receive new images
            // when they come in
            //fChunkDecoder = new GraphPortChunkDecoder(backingBuffer);
            fChunkDecoder = new GraphPortChunkDecoder(backingBuffer, null);
            fChunkDecoder.PixBltPixelBuffer24Handler += this.PixBltPixelBuffer24;
            fChunkDecoder.PixBltLumbHandler += this.PixBltLum24;
        }

        public void ReceiveDesktopFrame(object sender, RtpStream.FrameReceivedEventArgs ea)
        {
            // WAA
            //fChunkDecoder.ReceiveChunk(ea.Frame);
        }

        public virtual void PixBltPixelBuffer24(GDIDIBSection pixMap, int x, int y)
        {
            PixelAccessorBGRb accessor = new PixelAccessorBGRb(pixMap.Width, pixMap.Height, pixMap.Orientation, pixMap.Pixels, pixMap.Width * 3);
            fDynamicTexture.UpdateImage(accessor);
        }

        public virtual void PixBltLum24(PixelArray<Lumb> pixMap, int x, int y)
        {
            //fBackingGraphPort.PixBlt(pixMap, x, y);
        }

        public GLTexture Texture
        {
            get { return fDynamicTexture; }
        }

    }
}
