using System;
using System.Net;
using System.Configuration;
using System.Globalization;

using NewTOAPIA;    // BufferChunk
using NewTOAPIA.Net.Rtp;    // Classes used - RtpSession, RtpSender, RtpParticipant, RtpStream

namespace HamSketch
{
    public class MultiSession
    {
        RtpSession fRtpSession;
        string fUniqueSessionName;

        private static IPEndPoint gSessionEndPoint = RtpSession.DefaultEndPoint;

        static MultiSession()
        {
            string setting;
            // See if there was a multicast IP address set in the app.config
            if ((setting = ConfigurationManager.AppSettings["EndPoint"]) != null)
            {
                string[] args = setting.Split(new char[] { ':' }, 2);
                gSessionEndPoint = new IPEndPoint(IPAddress.Parse(args[0]), int.Parse(args[1], CultureInfo.InvariantCulture));
            }
        }

        public MultiSession(string uniqueSessionName)
        {
            fUniqueSessionName = uniqueSessionName;

            // Step 1
            HookRtpEvents();

            // Step 2
            RtpParticipant participant = new RtpParticipant(fUniqueSessionName, fUniqueSessionName);
            fRtpSession = new RtpSession(gSessionEndPoint, participant, true, true);

            // Wait for channels to be added
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

        public virtual void AddChannel(PayloadChannel aChannel)
        {
            aChannel.JoinSession(fRtpSession, fUniqueSessionName);
        }

        public PayloadChannel CreateChannel(PayloadType aType)
        {
            PayloadChannel aChannel = new PayloadChannel(aType);
            AddChannel(aChannel);

            return aChannel;
        }

        // Hook Rtp events
        protected virtual void HookRtpEvents()
        {
            RtpEvents.RtpParticipantAdded += new RtpEvents.RtpParticipantAddedEventHandler(RtpParticipantAdded);
            RtpEvents.RtpParticipantRemoved += new RtpEvents.RtpParticipantRemovedEventHandler(RtpParticipantRemoved);
            RtpEvents.RtpStreamAdded += new RtpEvents.RtpStreamAddedEventHandler(RtpStreamAdded);
            RtpEvents.RtpStreamRemoved += new RtpEvents.RtpStreamRemovedEventHandler(RtpStreamRemoved);
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
            //ea.RtpStream.FrameReceived += new RtpStream.FrameReceivedEventHandler(FrameReceived);
        }

        protected virtual void RtpStreamRemoved(object sender, RtpEvents.RtpStreamEventArgs ea)
        {
            //ea.RtpStream.FrameReceived -= new RtpStream.FrameReceivedEventHandler(FrameReceived);
        }
    }
}
