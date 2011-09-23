using System;

using NewTOAPIA;
using NewTOAPIA.Drawing;
using NewTOAPIA.Net.Rtp;

namespace HamSketch
{
    class GraphicsChannel : GraphPortDelegate
    {
        IGraphPort fRenderer;
        RtpSession fSession;
        RtpSender fRtpSender;
        string fParticipantName;

        GraphPortChunkEncoder fChunkEncoder;
        GraphPortChunkDecoder fChunkDecoder;

        public GraphicsChannel(RtpSession aSession, string participantName, IGraphPort localRenderer)
        {
            fSession = aSession;
            fParticipantName = participantName;

            fRenderer = localRenderer;

            fChunkDecoder = new GraphPortChunkDecoder();
            fChunkDecoder.AddGraphPort(fRenderer);

            HookRtpEvents();
            JoinSession();
        }

        void HookRtpEvents()
        {
            RtpEvents.RtpStreamAdded += new RtpEvents.RtpStreamAddedEventHandler(RtpStreamAdded);
            RtpEvents.RtpStreamRemoved += new RtpEvents.RtpStreamRemovedEventHandler(RtpStreamRemoved);
        }

        void JoinSession()
        {
            // Add the channel for graphics commands
            fRtpSender = fSession.CreateRtpSender(fParticipantName, PayloadType.xApplication2, null);
            //fRtpSender = fSession.CreateRtpSenderFec(fParticipantName, PayloadType.xApplication2, null, 0, 200);
            fRtpSender.DelayBetweenPackets = 0;

            // Create the sending graph port
            fChunkEncoder = new GraphPortChunkEncoder();
            fChunkEncoder.ChunkPackedEvent += new GraphPortChunkEncoder.ChunkPacked(GDIChunkPacked);
            AddGraphPort(fChunkEncoder);
        }

        protected virtual void RtpStreamAdded(object sender, RtpEvents.RtpStreamEventArgs ea)
        {
            if (PayloadType.xApplication2 == ea.RtpStream.PayloadType)
                ea.RtpStream.FrameReceived += new RtpStream.FrameReceivedEventHandler(GDICommandReceived);
        }

        protected virtual void RtpStreamRemoved(object sender, RtpEvents.RtpStreamEventArgs ea)
        {
            if (PayloadType.xApplication2 == ea.RtpStream.PayloadType)
                ea.RtpStream.FrameReceived -= new RtpStream.FrameReceivedEventHandler(GDICommandReceived);
        }

        void GDIChunkPacked(NewTOAPIA.BufferChunk command)
        {
            fRtpSender.Send(command);
        }

        private void GDICommandReceived(object sender, RtpStream.FrameReceivedEventArgs ea)
        {
            // send the frame to the receiver
            fChunkDecoder.ReceiveData(ea.Frame);
        }
    }
}
