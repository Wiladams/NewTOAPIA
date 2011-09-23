using System;
using System.IO;

using NewTOAPIA.Net.Rtp;    // Classes used - RtpSession, RtpSender, RtpParticipant, RtpStream
using NewTOAPIA.Drawing;
using NewTOAPIA;
using NewTOAPIA.Imaging;

namespace SnapNShare
{
    public class SketchViewer : UIPortDelegate
    {
        public delegate void ReceivedNewImage(PixelBuffer24 anImage);
        public event ReceivedNewImage DataChangedEvent;

        // Conferencing stuff
        PayloadChannel fSketchChannel;

        PixelBuffer24 fBackingBuffer;

        GraphPortChunkDecoder fChunkDecoder;

        #region Constructor
        public SketchViewer(PayloadChannel channel, int width, int height)
        {
            fSketchChannel = channel;

            // Create the backing buffer to retain the image
            fBackingBuffer = new PixelBuffer24(width, height);

            // Create the decoder and assign ourself to received 
            // decoded methods
            fChunkDecoder = new GraphPortChunkDecoder(fBackingBuffer, fSketchChannel);
            fChunkDecoder.AddPort(this);
        }
        #endregion


        public override void PixBltPixelBuffer24(PixelBuffer24 pixMap, int x, int y)
        {
            fBackingBuffer = pixMap;

            if (null != DataChangedEvent)
                DataChangedEvent(fBackingBuffer);
        }

    }
}
