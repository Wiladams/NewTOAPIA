using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using NewTOAPIA;
using NewTOAPIA.Drawing;
using NewTOAPIA.Imaging;
using NewTOAPIA.Media;
using NewTOAPIA.Media.Capture;
using NewTOAPIA.Net.Rtp;

using PixelShare.Core;

namespace ShowIt
{
    public class ConferenceAttendee : IReceiveConferenceFrames
    {
        ConferenceSession fSession;
        RtpParticipant fParticipant;
        DeskSet fDeskSet;
        AttendeeCamera fCaptureCamera;
        DesktopCapture fDesktopCapture;
        ShowItModel fModel;

        #region Constructor
        public ConferenceAttendee(ConferenceSession session, RtpParticipant participant)
        {
            fSession = session;
            fParticipant = participant;

            // Create the equipment the attendee controls
            // Personal desktop capture
            fDesktopCapture = new DesktopCapture(this);

            // Personal video Capture
            int camIndex = 0;
            int numSources = VideoCaptureDevice.GetNumberOfInputDevices();
            if (numSources > 4)
                camIndex = 4;

            fCaptureCamera = new AttendeeCamera(this, camIndex);
            fCaptureCamera.Start();

            // Personal audio capture

            // Desk Set
            fDeskSet = session.Model.CreateDeskSet(this);
        }
        #endregion

        #region Properties
        public ConferenceSession Session
        {
            get { return fSession; }
        }
        #endregion

        #region Receiving Frames
        public void ReceiveDesktopFrame(object sender, RtpStream.FrameReceivedEventArgs ea)
        {
            fDeskSet.ReceiveDesktopFrame(sender, ea);
        }

        public void ReceiveVideoFrame(object sender, RtpStream.FrameReceivedEventArgs ea)
        {
            fDeskSet.ReceiveVideoFrame(sender, ea);
        }

        public void ReceiveAudioFrame(object sender, RtpStream.FrameReceivedEventArgs ea)
        {
            fDeskSet.ReceiveVideoFrame(sender, ea);
        }
        #endregion

        #region Sending Frames
        public void SendDesktopFrame(BufferChunk chunk)
        {
            Session.SendDesktopFrame(chunk);
        }

        public void SendVideoFrame(PixelAccessor<BGRb> aFrame)
        {
            BufferChunk chunk = EncodeBGRb(aFrame, 0, 0);
            Session.SendVideoFrame(chunk);
        }

        public void SendAudioFrame()
        {
            //Session.AudioChannel.Send(audioChunk);
        }
        #endregion

        public  BufferChunk EncodeBGRb(PixelAccessor<BGRb> accessor, int x, int y)
        {
            MemoryStream ms = new MemoryStream();

            // 2. Run length encode the image to a memory stream
            NewTOAPIA.Imaging.TargaRunLengthCodec rlc = new NewTOAPIA.Imaging.TargaRunLengthCodec();
            rlc.Encode(accessor, ms);

            // 3. Get the bytes from the stream
            byte[] imageBytes = ms.GetBuffer();
            int dataLength = (int)imageBytes.Length;

            // 4. Allocate a buffer chunk to accomodate the bytes, plus some more
            BufferChunk chunk = new BufferChunk(dataLength + 128);

            // 5. Put the command, destination, and data size into the buffer first
            chunk += (int)UICommands.PixBltRLE;
            ChunkUtils.Pack(chunk, x, y);
            ChunkUtils.Pack(chunk, accessor.Width, accessor.Height);
            chunk += dataLength;

            // 6. Put the image bytes into the chunk
            chunk += imageBytes;


            // 7. Finally, return the packet
            return chunk;
        }

    }
}
