using System;

using NewTOAPIA;
using NewTOAPIA.Drawing;
using NewTOAPIA.Net.Rtp;

namespace HumLog
{
    

    public class SpaceControlChannel //: SpaceControlDelegate
    {
        #region ControlCommand Constants
        public const int
            SC_None = 0,
            SC_MouseEvent = 1,
            SC_KeyboardEvent = 2,
            SC_InvalidateSurfaceRect = 3,
            SC_BitBlt = 5,
            SC_AlphaBlend = 6,
            SC_ScaleBitmap = 7,
            SC_CreateSurface = 8,
            SC_CopyPixels = 9;
        #endregion

        SessionManager fSessionManager;
        IReceiveSpaceControl fSharedSpace;

        RtpSession fSession;
        RtpSender fRtpSender;
        string fParticipantName;

        SpaceCommandEncoder fChunkEncoder;
        SpaceCommandDecoder fChunkDecoder;

        public SpaceControlChannel(SessionManager sessManager, string participantName, IReceiveSpaceControl aSpace)
        {
            fSessionManager = sessManager;
            fParticipantName = participantName;
            fSharedSpace = aSpace;
            fSession = fSessionManager.Session;


            AddChannels();
        }

        #region Public
        SpaceCommandEncoder SpaceControlSender
        {
            get { return fChunkEncoder; }
        }

        SpaceCommandDecoder SpaceControlReceiver
        {
            get {return fChunkDecoder;}
        }
        #endregion

        public static explicit operator SpaceCommandEncoder(SpaceControlChannel source)
        {
            return source.SpaceControlSender;
        }

        public static explicit operator SpaceCommandDecoder(SpaceControlChannel source)
        {
            return source.SpaceControlReceiver;
        }

        #region Session Management
        void HookRtpEvents()
        {
            RtpEvents.RtpStreamAdded += new RtpEvents.RtpStreamAddedEventHandler(RtpStreamAdded);
            RtpEvents.RtpStreamRemoved += new RtpEvents.RtpStreamRemovedEventHandler(RtpStreamRemoved);
        }

        void AddChannels()
        {
            HookRtpEvents();

            // Add the channel for commands
            fRtpSender = fSession.CreateRtpSender(fParticipantName, PayloadType.xApplication3, null);
            fRtpSender.DelayBetweenPackets = 0;

            // Create the receiver
            fChunkDecoder = new SpaceCommandDecoder(fSharedSpace);

            // Create the sender
            fChunkEncoder = new SpaceCommandEncoder(fRtpSender);
        }

        protected virtual void RtpStreamAdded(object sender, RtpEvents.RtpStreamEventArgs ea)
        {
            if (PayloadType.xApplication3 == ea.RtpStream.PayloadType)
                ea.RtpStream.FrameReceived += new RtpStream.FrameReceivedEventHandler(CommandReceived);
        }

        protected virtual void RtpStreamRemoved(object sender, RtpEvents.RtpStreamEventArgs ea)
        {
            if (PayloadType.xApplication3 == ea.RtpStream.PayloadType)
                ea.RtpStream.FrameReceived -= new RtpStream.FrameReceivedEventHandler(CommandReceived);
        }
        #endregion

        void SendCommand(BufferChunk command)
        {
            // WAA - short circuit for debugging
            //fChunkDecoder.ReceiveData(command);
            fRtpSender.Send(command);
        }

        private void CommandReceived(object sender, RtpStream.FrameReceivedEventArgs ea)
        {
            // send the frame to the receiver
            fChunkDecoder.ReceiveFrame(ea);
        }
    }
}
