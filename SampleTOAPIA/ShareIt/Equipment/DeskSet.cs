using System;
using System.Collections.Generic;
using System.Text;

using NewTOAPIA;
using NewTOAPIA.GL;
using NewTOAPIA.Net.Rtp;
using NewTOAPIA.Modeling;

namespace ShowIt
{
    public class DeskSet : Node, IRenderable, IReceiveConferenceFrames
    {
        int fPosition;
        ConferenceAttendee fAttendee;
        ReceivingTexture fDesktopViewer;
        ReceivingTexture fWebcamViewer;
        DisplayMonitor fMonitor;

        public DeskSet(int position, ConferenceAttendee attendee)
        {
            fPosition = position;
            fAttendee = attendee;

            // Desk Set Felt Pad

            // Bifold Monitor
            AABB boundary = new AABB(new NewTOAPIA.Vector3D(0.33f, 0.33f, 0.025f), new Point3D(0,1.025f,0));
            fMonitor = new DisplayMonitor(attendee.Session.Model.GI, boundary, new Resolution(1024,768));
            fMonitor.Rotate(-60, new float3(0, 1, 0));
            fMonitor.Translate(new Vector3D(-0.50f, 0.05f, 0.5f));

            // Create some viewers to receive images
            fDesktopViewer = new ReceivingTexture(attendee.Session.Model.GI, 620, 440);
            fWebcamViewer = new ReceivingTexture(attendee.Session.Model.GI, 352, 288);

            // Show the display on multiple monitors
            fMonitor.ImageSource = fWebcamViewer;
            attendee.Session.Model.Room.Whiteboard.ImageSource = fDesktopViewer;

            // Video Pedestal display

            // Whiteboard Pen Set

            AddChild(fMonitor);
        }

        #region Receiving Frames
        public void ReceiveDesktopFrame(object sender, RtpStream.FrameReceivedEventArgs ea)
        {
            // Decode frame
            fDesktopViewer.ReceiveDesktopFrame(sender, ea);

            // Display it on the desktop viewer
        }

        public void ReceiveVideoFrame(object sender, RtpStream.FrameReceivedEventArgs ea)
        {
            fWebcamViewer.ReceiveDesktopFrame(sender, ea);
        }

        public void ReceiveAudioFrame(object sender, RtpStream.FrameReceivedEventArgs ea)
        {
        }
        #endregion

        #region IRenderable
        public virtual void Render(GraphicsInterface gi)
        {
            // Render felt pad
            // pens
            // monitor
            fMonitor.Render(gi);

            // pedestal display
        }
        #endregion
    }
}
