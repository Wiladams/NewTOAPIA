using System;

using NewTOAPIA;
using NewTOAPIA.Drawing;
using NewTOAPIA.Net.Rtp;

namespace HamSketch
{
    

    class SpaceControlChannel : SpaceControlDelegate
    {
        #region ControlCommand Constants
        public const int
            SC_None = 0,
            SC_MouseEvent = 1,
            SC_KeyboardEvent = 2,
            SC_InvalidateRect = 3,
            SC_Invalidate = 4,
            SC_BitBlt = 5,
            SC_AlphaBlend = 6,
            SC_ScaleBitmap = 7;
        #endregion

        SessionManager fSessionManager;
        MetaSpace fSharedSpace;

        RtpSession fSession;
        RtpSender fRtpSender;
        string fParticipantName;

        SpaceCommandEncoder fChunkEncoder;
        SpaceCommandDecoder fChunkDecoder;

        public SpaceControlChannel(SessionManager sessManager, string participantName, MetaSpace aSpace)
        {
            fSessionManager = sessManager;
            fParticipantName = participantName;
            fSharedSpace = aSpace;
            fSession = fSessionManager.Session;

            fChunkDecoder = new SpaceCommandDecoder();
            fChunkDecoder.AddSpaceController(aSpace);

            AddChannels();
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

            // Create the sending graph port
            fChunkEncoder = new SpaceCommandEncoder();
            fChunkEncoder.ChunkPackedEvent += new SpaceCommandEncoder.PackCommandEventHandler(SendCommand);
            AddSpaceController(fChunkEncoder);
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

        void SendCommand(NewTOAPIA.BufferChunk command)
        {
            fRtpSender.Send(command);
        }

        private void CommandReceived(object sender, RtpStream.FrameReceivedEventArgs ea)
        {
            // send the frame to the receiver
            fChunkDecoder.ReceiveData(ea);
        }
    }
}
