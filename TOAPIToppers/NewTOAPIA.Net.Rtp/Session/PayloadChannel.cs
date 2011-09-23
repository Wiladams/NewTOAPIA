using System;

using NewTOAPIA;

namespace NewTOAPIA.Net.Rtp
{
    public class PayloadChannel : IPayloadChannel
    {
        public event RtpStream.FrameReceivedEventHandler FrameReceivedEvent;

        bool fIsReadyToSend;
        bool fUseFrameReceived;

        PayloadType fPayloadType;
        RtpSession fSession;
        RtpSender fRtpSender;
        RtpStream fStream;

        #region Constructor
        public PayloadChannel(PayloadType pType)
            :this(pType, true)
        {
        }

        public PayloadChannel(PayloadType pType, bool passive)
        {
            fUseFrameReceived = passive;
            fPayloadType = pType;
        }

        #endregion

        #region Properties
        public PayloadType ChannelType
        {
            get { return fPayloadType; }
        }

        public bool IsReadyToSend
        {
            get { return fIsReadyToSend; }
        }

        public RtpStream Stream
        {
            get { return fStream; }
        }

        public RtpSender Sender
        {
            get { return fRtpSender; }
            protected set { fRtpSender = value; }
        }

        public RtpSession Session
        {
            get { return fSession; }
        }
        #endregion

        public void Send(BufferChunk aChunk)
        {
            Sender.Send(aChunk);
        }

        public void Send(IntPtr[] ptrs, int[] lengths, bool prependLengths)
        {
            Sender.Send(ptrs, lengths, prependLengths);
        }

        public virtual void JoinSession(RtpSession session, string participantName)
        {
            fSession = session;

            // Must hook up events before creating sender so we know when the
            // ChannelType stream is added.
            HookRtpEvents();

            CreateRtpSender(participantName);

        }

        public virtual void LeaveSession()
        {
        }

        public virtual void CreateRtpSender(string participantName)
        {
            // Create the sender for the specified channel type
            fRtpSender = fSession.CreateRtpSender(participantName, ChannelType, null);
            //fRtpSender = fSession.CreateRtpSenderFec(participantName, ChannelType, null, 3, 1);
            //fRtpSender.DelayBetweenPackets = 0;
        }

        void HookRtpEvents()
        {
            fSession.Events.RtpStreamAdded += RtpStreamAdded;
            fSession.Events.RtpStreamRemoved += RtpStreamRemoved;
        }

        void UnhookRtpEvents()
        {
            fSession.Events.RtpStreamAdded -= RtpStreamAdded;
            fSession.Events.RtpStreamRemoved -= RtpStreamRemoved;
        }

        protected virtual void RtpStreamAdded(object sender, RtpEvents.RtpStreamEventArgs ea)
        {
            if (ChannelType != ea.RtpStream.PayloadType)
                return;

            if (fUseFrameReceived)
            {
                ea.RtpStream.FrameReceived += FrameReceived;
            }

            fIsReadyToSend = true;
            fStream = ea.RtpStream;
        }

        protected virtual void RtpStreamRemoved(object sender, RtpEvents.RtpStreamEventArgs ea)
        {
            if (ChannelType != ea.RtpStream.PayloadType)
                return;

            if (fUseFrameReceived)
            {
                ea.RtpStream.FrameReceived -= FrameReceived;
            }
            fIsReadyToSend = false;
            fStream = null;
        }

        private void FrameReceived(object sender, RtpStream.FrameReceivedEventArgs ea)
        {
            if (null != FrameReceivedEvent)
                FrameReceivedEvent(sender, ea);
        }

    }
}
