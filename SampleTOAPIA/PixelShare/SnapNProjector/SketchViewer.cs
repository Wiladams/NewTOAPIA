using System;
using System.IO;

using NewTOAPIA.Net.Rtp;    // Classes used - RtpSession, RtpSender, RtpParticipant, RtpStream
using NewTOAPIA.Drawing;
using NewTOAPIA;
using NewTOAPIA.Imaging;

namespace SnapNProjector
{
    public class SketchViewer
    {
        public delegate void ReceivedNewImage(int x, int y, PixelBuffer24 anImage);

        public event ReceivedNewImage DataChangedEvent;

        // Conferencing stuff
        MultiSession fSession;
        PayloadChannel fSketchChannel;

        PixelBuffer24 fBackingBuffer;

        #region Constructor
        public SketchViewer(MultiSession session, int width, int height)
        {
            fSession = session;

            // Create the backing buffer to retain the image
            fBackingBuffer = new PixelBuffer24(width, height);

            // Add the channel for graphics commands
            fSketchChannel = fSession.CreateChannel(PayloadType.Whiteboard);

            fSketchChannel.FrameReceivedEvent += FrameReceived;
        }
        #endregion

        void DecodeChunk(BufferChunk aRecord)
        {
            // Get the first int, which is the command
            int recordType = aRecord.NextInt32();

            
            // Get the X, Y
            int x = aRecord.NextInt32();
            int y = aRecord.NextInt32();
            int width = aRecord.NextInt32();
            int height = aRecord.NextInt32();

            // get the length of the image bytes buffer
            int dataSize = aRecord.NextInt32();

            byte[] imageBytes = (byte[])aRecord;

            // Create a memory stream from the imageBytes
            MemoryStream ms = new MemoryStream(imageBytes, false);

            // Create a pixelArray from the stream
            if ((width != fBackingBuffer.Width) || (height != fBackingBuffer.Height))
                fBackingBuffer = new PixelBuffer24(width, height);

            TargaRunLengthCodec rlc = new TargaRunLengthCodec();
            rlc.Decode(ms, fBackingBuffer);


            if (null != DataChangedEvent)
                DataChangedEvent(x, y, fBackingBuffer);
        }

        private void FrameReceived(object sender, RtpStream.FrameReceivedEventArgs ea)
        {
            // send the frame to the receiver
            DecodeChunk(ea.Frame);

        }

        public PixelBuffer24 PixelArray
        {
            get { return fBackingBuffer; }
        }    
    }
}
