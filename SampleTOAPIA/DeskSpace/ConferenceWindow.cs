using System;
using System.Configuration;
using System.Globalization;
using System.Net;
using System.Text;

using NewTOAPIA;    // BufferChunk
using NewTOAPIA.Net.Rtp;    // Classes used - RtpSession, RtpSender, RtpParticipant, RtpStream
using NewTOAPIA.UI;

namespace HamSketch
{
    public class ConferenceWindow : Window
    {
        RtpSession rtpSession;
        string fUniqueSessionName;

        private static IPEndPoint gSessionEndPoint = RtpSession.DefaultEndPoint;

        static ConferenceWindow()
        {
            string setting;
            // See if there was a multicast IP address set in the app.config
            if ((setting = ConfigurationManager.AppSettings["EndPoint"]) != null)
            {
                string[] args = setting.Split(new char[] { ':' }, 2);
                gSessionEndPoint = new IPEndPoint(IPAddress.Parse(args[0]), int.Parse(args[1], CultureInfo.InvariantCulture));
            }
        }

        public ConferenceWindow(string title, int x, int y, int width, int height)
            : base(title, x, y, width, height)
        {
            //this.Text = title;
            //this.Location = new System.Drawing.Point(x, y);
            //this.Size = new System.Drawing.Size(width, height);

            fUniqueSessionName = null;
            JoinSession();
        }

        // CF1 Hook Rtp events
        protected virtual void HookRtpEvents()
        {
            RtpEvents.RtpParticipantAdded += new RtpEvents.RtpParticipantAddedEventHandler(RtpParticipantAdded);
            RtpEvents.RtpParticipantRemoved += new RtpEvents.RtpParticipantRemovedEventHandler(RtpParticipantRemoved);
            RtpEvents.RtpStreamAdded += new RtpEvents.RtpStreamAddedEventHandler(RtpStreamAdded);
            RtpEvents.RtpStreamRemoved += new RtpEvents.RtpStreamRemovedEventHandler(RtpStreamRemoved);
        }



        // CF2 Create participant, join session
        // CF3 Retrieve RtpSender
        protected virtual RtpSender AddChannel(string name, PayloadType channelType)
        {
            RtpSender sender = rtpSession.CreateRtpSender(name, channelType, null);
            return sender;
        }

        protected virtual void JoinRtpSession(string name)
        {
            RtpParticipant participant = new RtpParticipant(name, name);
            rtpSession = new RtpSession(gSessionEndPoint, participant, true, true);
        }

        public RtpSession Session
        {
            get { return rtpSession; }
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

        protected virtual void AddChannels()
        {
        }

        protected virtual void JoinSession()
        {
            HookRtpEvents(); // CF:1

            JoinRtpSession(UniqueSessionName); // CF:2

            AddChannels();
        }

        // CF5 Receive data from network
        protected virtual void RtpParticipantAdded(object sender, RtpEvents.RtpParticipantEventArgs ea)
        {
            Console.WriteLine("Participant Joined: {0}", ea.RtpParticipant.Name);
            //ShowMessage(string.Format(CultureInfo.CurrentCulture, Strings.HasJoinedTheChatSession,
            //    ea.RtpParticipant.Name));
        }

        protected virtual void RtpParticipantRemoved(object sender, RtpEvents.RtpParticipantEventArgs ea)
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

        // CF6 Unhook events, dispose RtpSession
        protected virtual void Cleanup()
        {
            UnhookRtpEvents();
            LeaveRtpSession();
        }

        protected virtual void UnhookRtpEvents()
        {
            RtpEvents.RtpParticipantAdded -= new RtpEvents.RtpParticipantAddedEventHandler(RtpParticipantAdded);
            RtpEvents.RtpParticipantRemoved -= new RtpEvents.RtpParticipantRemovedEventHandler(RtpParticipantRemoved);
            RtpEvents.RtpStreamAdded -= new RtpEvents.RtpStreamAddedEventHandler(RtpStreamAdded);
            RtpEvents.RtpStreamRemoved -= new RtpEvents.RtpStreamRemovedEventHandler(RtpStreamRemoved);
        }

        protected virtual void LeaveRtpSession()
        {
            if (rtpSession != null)
            {
                // Clean up all outstanding objects owned by the RtpSession
                rtpSession.Dispose();
                rtpSession = null;
            }
        }
    }
}
