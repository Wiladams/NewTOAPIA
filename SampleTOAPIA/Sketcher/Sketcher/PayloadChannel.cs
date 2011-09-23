
using NewTOAPIA;
using NewTOAPIA.Net.Rtp;

namespace HamSketch
{
    public class PayloadChannel : IPayloadChannel
    {
        public event RtpStream.FrameReceivedEventHandler FrameReceivedEvent;

        PayloadType fPayloadType;
        RtpSession fSession;
        RtpSender fRtpSender;
        string fParticipantName;

        public PayloadChannel(PayloadType pType)
        {
            fPayloadType = pType;
        }

        public PayloadType ChannelType
        {
            get { return fPayloadType; }
        }

        public void Send(BufferChunk aChunk)
        {
            Sender.Send(aChunk);
        }

        public RtpSender Sender
        {
            get { return fRtpSender; }
        }

        public RtpSession Session
        {
            get { return fSession; }
        }

        public virtual void JoinSession(RtpSession session, string participantName)
        {
            fSession = session;
            fParticipantName = participantName;

            // Must hook up events before creating sender so we know when the
            // ChannelType stream is added.
            HookRtpEvents();


            // Add the channel for graphics commands
            fRtpSender = fSession.CreateRtpSender(fParticipantName, ChannelType, null);
            //fRtpSender = fSession.CreateRtpSenderFec(fParticipantName, PayloadType.xApplication2, null, 0, 200);
            //fRtpSender.DelayBetweenPackets = 0;

        }

        void HookRtpEvents()
        {
            RtpEvents.RtpStreamAdded += new RtpEvents.RtpStreamAddedEventHandler(RtpStreamAdded);
            RtpEvents.RtpStreamRemoved += new RtpEvents.RtpStreamRemovedEventHandler(RtpStreamRemoved);
        }

        protected virtual void RtpStreamAdded(object sender, RtpEvents.RtpStreamEventArgs ea)
        {
            if (ChannelType == ea.RtpStream.PayloadType)
                ea.RtpStream.FrameReceived += new RtpStream.FrameReceivedEventHandler(FrameReceived);
        }

        protected virtual void RtpStreamRemoved(object sender, RtpEvents.RtpStreamEventArgs ea)
        {
            if (ChannelType == ea.RtpStream.PayloadType)
                ea.RtpStream.FrameReceived -= new RtpStream.FrameReceivedEventHandler(FrameReceived);
        }

        private void FrameReceived(object sender, RtpStream.FrameReceivedEventArgs ea)
        {
            if (null != FrameReceivedEvent)
                FrameReceivedEvent(sender, ea);
        }

    }
}
