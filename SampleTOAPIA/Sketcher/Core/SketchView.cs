using System;

using NewTOAPIA;
using NewTOAPIA.Net.Rtp;    // Classes used - RtpSession, RtpSender, RtpParticipant, RtpStream
using NewTOAPIA.Drawing;
using NewTOAPIA.Drawing.GDI;

namespace Sketcher.Core
{
    public class SketchView
    {
        public delegate void DataChangedEventHandler();

        public event DataChangedEventHandler DataChangedEvent;

        // Conferencing stuff
        MultiSession fSession;
        PayloadChannel fSketchChannel;

        GraphPortChunkDecoder fChunkDecoder;

        GDIDIBSection backingBuffer;
        GDIRenderer fBackingGraphPort;

        public SketchView(MultiSession session, int width, int height)
        {
            fSession = session;

            // Create the backing buffer to retain the image
            backingBuffer = GDIDIBSection.Create(width, height);
            GDIContext bufferContext = GDIContext.CreateForBitmap(backingBuffer);

            bufferContext.ClearToWhite();
            fBackingGraphPort = new GDIRenderer(bufferContext);
            //fBackingGraphPort.ClearToWhite();

            // Add the channel for graphics commands
            fSketchChannel = fSession.CreateChannel(PayloadType.Whiteboard);

            fChunkDecoder = new GraphPortChunkDecoder();
            fChunkDecoder.AddGraphPort(fBackingGraphPort);

            fSketchChannel.FrameReceivedEvent += new RtpStream.FrameReceivedEventHandler(GDICommandReceived);
        }

        private void GDICommandReceived(object sender, RtpStream.FrameReceivedEventArgs ea)
        {
            // send the frame to the receiver
            fChunkDecoder.ReceiveData(ea.Frame);

            if (null != DataChangedEvent)
                DataChangedEvent();

        }

        public GDIDIBSection Pixels
        {
            get { return backingBuffer; }
        }
    
    }
}
