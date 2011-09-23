using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Threading;

using NewTOAPIA;
using NewTOAPIA.Net.Rtp;

namespace ShowIt
{
    /// <summary>
    /// The ConferenceSession is a centralized object that manages the conference.
    /// This object makes a mapping between the attendees of a multicast session, 
    /// and the Attendee objects that represent them in the virtual world.
    /// 
    /// The general model of message distribution is to send all the frames received
    /// from a particular session participant to their Attendee object.  From there,
    /// the frames can be rendered in the appropriate way for that attendee.
    /// </summary>
    public class ConferenceSession : MultiSession, IReceiveConferenceFrames
    {
        //Dictionary<int, ConferenceAttendee> fAttendees;
        RtpParticipant fLocalParticipant;

        PayloadChannel fVideoChannel;
        PayloadChannel fAudioChannel;
        PayloadChannel fDesktopChannel;

        ShowItModel fModel;

        /// <summary>
        /// Most fundamental constructor.  We need at least an IP address and identifiers to get a session going.
        /// The uniqueName is generated from: Guid.NewGuid().ToString()
        /// The friendlyName is generated from: Environment.UserName
        /// </summary>
        /// <param name="endPoint"></param>
        public ConferenceSession(IPEndPoint endPoint)
            : this(endPoint, Guid.NewGuid().ToString(), Environment.UserName, true, true, null)
        {
        }

        ConferenceSession(IPEndPoint endPoint, string uniqueName, string friendlyName, bool rtpTraffic, bool receiveData, IPEndPoint reflector)
            : base()
        {
            fModel = new ShowItModel(this);


            // Step 1
            fLocalParticipant = new RtpParticipant(uniqueName, friendlyName);

            if (null == reflector)
                Session = new RtpSession(endPoint, fLocalParticipant, true, true);
            else
                Session = new RtpSession(endPoint, fLocalParticipant, true, true, reflector);

            HookRtpEvents();


            // After everything is setup, initialize the session, which will
            // start the network messages flowing.
            Session.Initialize();

        
            // Create the three channels that are used in the conference
            fDesktopChannel = CreateChannel(PayloadType.xApplication2);
            fDesktopChannel.FrameReceivedEvent += new RtpStream.FrameReceivedEventHandler(ReceiveDesktopFrame);
            
            fAudioChannel = CreateChannel(PayloadType.xApplication3);
            fAudioChannel.FrameReceivedEvent += new RtpStream.FrameReceivedEventHandler(ReceiveAudioFrame);
            
            fVideoChannel = CreateChannel(PayloadType.xApplication4);
            fVideoChannel.FrameReceivedEvent += new RtpStream.FrameReceivedEventHandler(ReceiveVideoFrame);

            // Wait until the channels are ready
            // to send before proceeding
            do
            {
                Thread.Sleep(10);
            } while (!fDesktopChannel.IsReadyToSend && !fAudioChannel.IsReadyToSend && !fVideoChannel.IsReadyToSend);

        }

        #region Properties
        public ShowItModel Model
        {
            get { return fModel; }
        }

        public PayloadChannel DesktopChannel
        {
            get { return fDesktopChannel; }
        }

        public PayloadChannel AudioChannel
        {
            get { return fAudioChannel; }
        }

        public PayloadChannel VideoChannel
        {
            get { return fVideoChannel; }
        }
        #endregion

        #region Receiving Frame Events
        public virtual void ReceiveDesktopFrame(object sender, RtpStream.FrameReceivedEventArgs ea)
        {
            fModel.ReceiveDesktopFrame(sender, ea);
        }

        public virtual void ReceiveVideoFrame(object sender, RtpStream.FrameReceivedEventArgs ea)
        {
            fModel.ReceiveVideoFrame(sender, ea);
        }

        public virtual void ReceiveAudioFrame(object sender, RtpStream.FrameReceivedEventArgs ea)
        {
            fModel.ReceiveAudioFrame(sender, ea);
        }

        #endregion

        #region Sending Conference Frames
        public void SendDesktopFrame(BufferChunk chunk)
        {
            if (null != DesktopChannel)
            {
                DesktopChannel.Send(chunk);
            }
        }

        public void SendVideoFrame(BufferChunk chunk)
        {
            if (null != VideoChannel)
            {
                VideoChannel.Send(chunk);
            }
        }

        public void SendAudioFrame(BufferChunk chunk)
        {
            if (null != AudioChannel)
            {
                AudioChannel.Send(chunk);
            }
        }
        #endregion

        #region Responding to Session Events
        protected override void ParticipantAdded(object sender, RtpEvents.RtpParticipantEventArgs ea)
        {
            fModel.AddPendingParticipant(ea.RtpParticipant);
        }
        #endregion

        public void Run()
        {

            NewTOAPIA.GL.GLApplication app = new NewTOAPIA.GL.GLApplication("Share IT");
            app.Run(fModel);
        }
    }
}
