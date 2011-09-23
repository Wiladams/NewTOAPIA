using System;
using System.Configuration;
using System.Globalization;
using System.Net;

using NewTOAPIA;    // BufferChunk
using NewTOAPIA.Net.Rtp;    // Classes used - RtpSession, RtpSender, RtpParticipant, RtpStream
using NewTOAPIA.UI;


namespace HumLog
{
    public class SessionManager
    {
        RtpParticipant fSessionParticipant;
        RtpSession fSession;
        string fUniqueSessionName;

        private static IPEndPoint gSessionEndPoint = RtpSession.DefaultEndPoint;

        static SessionManager()
        {
            string setting;

            // See if there was a multicast IP address set in the app.config
            if ((setting = ConfigurationManager.AppSettings["EndPoint"]) != null)
            {
                string[] args = setting.Split(new char[] { ':' }, 2);
                gSessionEndPoint = new IPEndPoint(IPAddress.Parse(args[0]), int.Parse(args[1], CultureInfo.InvariantCulture));
            }
        }

        public SessionManager()
        {
            fUniqueSessionName = null;

            HookRtpEvents(); // CF:1
            JoinRtpSession(UniqueSessionName,"UserName"); // CF:2
        }

        #region Properties
        public RtpSession Session
        {
            get { return fSession; }
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

        #endregion

        #region  CF1 Hook Rtp events
        protected virtual void HookRtpEvents()
        {
            RtpEvents.RtpParticipantAdded += new RtpEvents.RtpParticipantAddedEventHandler(ParticipantJoined);
            RtpEvents.RtpParticipantRemoved += new RtpEvents.RtpParticipantRemovedEventHandler(ParticipantLeft);
            //RtpEvents.RtpStreamAdded += new RtpEvents.RtpStreamAddedEventHandler(RtpStreamAdded);
            //RtpEvents.RtpStreamRemoved += new RtpEvents.RtpStreamRemovedEventHandler(RtpStreamRemoved);
        }
        #endregion

        // CF2 Create participant, join session
        // CF3 Retrieve RtpSender
        protected virtual RtpSender AddChannel(string name, PayloadType channelType)
        {
            RtpSender sender = fSession.CreateRtpSender(name, channelType, null);
            
            return sender;
        }

        protected virtual void JoinRtpSession(string ID, string name)
        {
            fSessionParticipant = new RtpParticipant(ID, name);
            fSession = new RtpSession(gSessionEndPoint, fSessionParticipant, true, true);
        }

        
        #region CF5 Receive data from network
        protected virtual void ParticipantJoined(object sender, RtpEvents.RtpParticipantEventArgs ea)
        {
            //Console.WriteLine("Participant Joined: {0}", ea.RtpParticipant.Name);
        }

        protected virtual void ParticipantLeft(object sender, RtpEvents.RtpParticipantEventArgs ea)
        {
            //ShowMessage(string.Format(CultureInfo.CurrentCulture, Strings.HasLeftTheChatSession,
            //    ea.RtpParticipant.Name));
        }

        protected virtual void RtpStreamAdded(object sender, RtpEvents.RtpStreamEventArgs ea)
        {
            ea.RtpStream.FrameReceived += new RtpStream.FrameReceivedEventHandler(FrameReceived);
        }

        protected virtual void RtpStreamRemoved(object sender, RtpEvents.RtpStreamEventArgs ea)
        {
            ea.RtpStream.FrameReceived -= new RtpStream.FrameReceivedEventHandler(FrameReceived);
        }

        protected virtual void FrameReceived(object sender, RtpStream.FrameReceivedEventArgs ea)
        {
            //ShowMessage(string.Format(CultureInfo.CurrentCulture, "{0}: {1}", ea.RtpStream.Properties.Name,
            //    (string)ea.Frame));
        }
        #endregion

        #region CF6 Unhook events, dispose RtpSession
        protected virtual void Cleanup()
        {
            UnhookRtpEvents();
            LeaveRtpSession();
        }

        protected virtual void UnhookRtpEvents()
        {
            RtpEvents.RtpParticipantAdded -= new RtpEvents.RtpParticipantAddedEventHandler(ParticipantJoined);
            RtpEvents.RtpParticipantRemoved -= new RtpEvents.RtpParticipantRemovedEventHandler(ParticipantLeft);
            RtpEvents.RtpStreamAdded -= new RtpEvents.RtpStreamAddedEventHandler(RtpStreamAdded);
            RtpEvents.RtpStreamRemoved -= new RtpEvents.RtpStreamRemovedEventHandler(RtpStreamRemoved);
        }

        protected virtual void LeaveRtpSession()
        {
            if (fSession != null)
            {
                // Clean up all outstanding objects owned by the RtpSession
                fSession.Dispose();
                fSession = null;
            }
        }
        #endregion
    }
}
