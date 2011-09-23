using System;
using System.Net;
using System.Configuration;
using System.Globalization;

using NewTOAPIA;    // BufferChunk

namespace NewTOAPIA.Net.Rtp
{
    public class MultiSession : IDisposable
    {
        RtpSession fRtpSession;
        string fUniqueSessionName;
        string fFriendlyName;
        RtpParticipant fLocalParticipant;

        protected MultiSession()
        {
        }

        public MultiSession(string uniqueSessionName, IPEndPoint endPoint)
            : this(endPoint, uniqueSessionName, uniqueSessionName, true, true, null)
        {
        }

        public MultiSession(IPEndPoint endPoint, string uniqueSessionName, string friendlyName, bool rtpTraffic, bool receiveData, IPEndPoint reflector)
        {
            fUniqueSessionName = uniqueSessionName;
            fFriendlyName = friendlyName;

            // Step 1
            fLocalParticipant = new RtpParticipant(fUniqueSessionName, friendlyName);

            if (null == reflector)
                fRtpSession = new RtpSession(endPoint, fLocalParticipant, true, true);
            else
                fRtpSession = new RtpSession(endPoint, fLocalParticipant, true, true, reflector);

            // Hook up all the event handler delegates
            // before we actually start the network rolling
            HookRtpEvents();

            // Start the network rolling
            fRtpSession.Initialize();
        }

        #region Properties
        public RtpParticipant LocalParticipant
        {
            get { return fLocalParticipant; }
        }

        public string UniqueSessionName
        {
            get
            {
                if (fUniqueSessionName == null)
                    fUniqueSessionName = Guid.NewGuid().ToString();

                return fUniqueSessionName;
            }
        }

        public RtpSession Session
        {
            get { return fRtpSession; }
            protected set { fRtpSession = value; }
        }
        #endregion

        public virtual void AddChannel(PayloadChannel aChannel)
        {
            aChannel.JoinSession(fRtpSession, fFriendlyName);
        }

        public virtual void RemoveChannel(PayloadChannel aChannel)
        {
            //aChannel.LeaveSession();
        }

        public PayloadChannel CreateChannel(PayloadType aType)
        {
            return CreateChannel(aType, true);
        }

        public PayloadChannel CreateChannel(PayloadType aType, bool passive)
        {
            PayloadChannel aChannel = new PayloadChannel(aType,passive);
            AddChannel(aChannel);

            return aChannel;
        }

        // Hook Rtp events
        protected virtual void HookRtpEvents()
        {
            //fRtpSession.Events.HiddenSocketException += new RtpEvents.HiddenSocketExceptionEventHandler(Events_HiddenSocketException);

            // Dealing with participants
            fRtpSession.Events.RtpParticipantAdded += ParticipantAdded;
            fRtpSession.Events.RtpParticipantRemoved += ParticipantRemoved;
            fRtpSession.Events.RtpParticipantTimeout += new RtpEvents.RtpParticipantTimeoutEventHandler(ParticipantTimeout);
            //Events.RtpParticipantDataChanged += new RtpEvents.RtpParticipantDataChangedEventHandler(Events_RtpParticipantDataChanged);
            
            // RTP Packets
            fRtpSession.Events.InvalidPacket += new RtpEvents.InvalidPacketEventHandler(InvalidPacket);

            // RTCP Packets
            fRtpSession.Events.AppPacketReceived += new RtpEvents.AppPacketReceivedEventHandler(AppPacketReceived);
            fRtpSession.Events.NetworkTimeout += new RtpEvents.NetworkTimeoutEventHandler(NetworkTimeout);
            fRtpSession.Events.ReceiverReport += new RtpEvents.ReceiverReportEventHandler(ReceiverReport);
            
            // Dealing with streams
            fRtpSession.Events.RtpStreamAdded += RtpStreamAdded;
            fRtpSession.Events.RtpStreamRemoved += RtpStreamRemoved;
            fRtpSession.Events.RtpStreamTimeout += new RtpEvents.RtpStreamTimeoutEventHandler(StreamTimeout);
        }

        void Events_RtpParticipantDataChanged(object sender, RtpEvents.RtpParticipantEventArgs ea)
        {
            throw new NotImplementedException();
        }

        void Events_HiddenSocketException(object sender, RtpEvents.HiddenSocketExceptionEventArgs args)
        {
            throw new NotImplementedException();
        }



        protected virtual void UnhookRtpEvents()
        {
            //fRtpSession.Events.HiddenSocketException += new RtpEvents.HiddenSocketExceptionEventHandler(Events_HiddenSocketException);

            // Participant removal
            fRtpSession.Events.RtpParticipantAdded -= new RtpEvents.RtpParticipantAddedEventHandler(ParticipantAdded);
            fRtpSession.Events.RtpParticipantRemoved -= new RtpEvents.RtpParticipantRemovedEventHandler(ParticipantRemoved);
            fRtpSession.Events.RtpParticipantTimeout -= new RtpEvents.RtpParticipantTimeoutEventHandler(ParticipantTimeout);
            //fRtpSession.Events.RtpParticipantDataChanged -= new RtpEvents.RtpParticipantDataChangedEventHandler(Events_RtpParticipantDataChanged);

            // RTP Packets
            fRtpSession.Events.InvalidPacket -= new RtpEvents.InvalidPacketEventHandler(InvalidPacket);

            // RTCP packets
            fRtpSession.Events.AppPacketReceived -= new RtpEvents.AppPacketReceivedEventHandler(AppPacketReceived);
            fRtpSession.Events.NetworkTimeout -= new RtpEvents.NetworkTimeoutEventHandler(NetworkTimeout);
            fRtpSession.Events.ReceiverReport -= new RtpEvents.ReceiverReportEventHandler(ReceiverReport);

            // Stream removal
            fRtpSession.Events.RtpStreamAdded -= RtpStreamAdded;
            fRtpSession.Events.RtpStreamRemoved -= new RtpEvents.RtpStreamRemovedEventHandler(RtpStreamRemoved);
            fRtpSession.Events.RtpStreamTimeout -= new RtpEvents.RtpStreamTimeoutEventHandler(StreamTimeout);
        }

        #region Event Handlers
        protected virtual void AppPacketReceived(object sender, RtpEvents.AppPacketReceivedEventArgs ea)
        {
            //throw new NotImplementedException();
        }

        protected virtual void InvalidPacket(object sender, RtpEvents.InvalidPacketEventArgs ea)
        {
            throw new NotImplementedException();
        }

        protected virtual void NetworkTimeout(object sender, RtpEvents.NetworkTimeoutEventArgs ea)
        {
            throw new NotImplementedException();
        }

        protected virtual void ReceiverReport(object sender, RtpEvents.ReceiverReportEventArgs ea)
        {
            //throw new NotImplementedException();
        }

        protected virtual void ParticipantAdded(object sender, RtpEvents.RtpParticipantEventArgs ea)
        {
            //Console.WriteLine("Participant Joined: {0}", ea.RtpParticipant.Name);
        }

        protected virtual void ParticipantRemoved(object sender, RtpEvents.RtpParticipantEventArgs ea)
        {
            //Console.WriteLine("Participant Left: {0}", ea.RtpParticipant.Name);
        }

        protected virtual void ParticipantTimeout(object sender, RtpEvents.RtpParticipantEventArgs ea)
        {

            throw new NotImplementedException();
        }

        protected virtual void RtpStreamAdded(object sender, RtpEvents.RtpStreamEventArgs ea)
        {
            //ea.RtpStream.FrameReceived += new RtpStream.FrameReceivedEventHandler(FrameReceived);
        }

        protected virtual void RtpStreamRemoved(object sender, RtpEvents.RtpStreamEventArgs ea)
        {
            //ea.RtpStream.FrameReceived -= new RtpStream.FrameReceivedEventHandler(FrameReceived);
        }

        protected virtual void StreamTimeout(object sender, RtpEvents.RtpStreamEventArgs ea)
        {
            throw new NotImplementedException();
        }

        #endregion

        ~MultiSession()
        {
            Dispose();
        }

        public void Dispose()
        {
            UnhookRtpEvents();
            fRtpSession.Dispose();
        }
    }
}
