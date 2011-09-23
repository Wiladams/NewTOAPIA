using System;
using System.Collections.Generic;
using System.Text;

using NewTOAPIA;
using NewTOAPIA.Drawing;
using NewTOAPIA.Media;
using NewTOAPIA.Media.Capture;

namespace ShowIt
{
    /// <summary>
    /// This class represents a camera that may be pointed at each individual attendee
    /// on their local machine.  It captures the image, and sends it out into the conference.
    /// </summary>
    public class AttendeeCamera
    {
        VideoCaptureDevice fCamera;
        ConferenceAttendee fAttendee;

        public AttendeeCamera(ConferenceAttendee attendee, int deviceIndex)
        {
            fAttendee = attendee;

            fCamera = VideoCaptureDevice.CreateCaptureDeviceFromIndex(deviceIndex, 320, 240);
            fCamera.NewFrame += new CameraEventHandler(fCamera_NewFrame);
        }

        void fCamera_NewFrame(object sender, CameraEventArgs e)
        {
            // Wrap the frame up in a PixelAccessor
            PixelAccessorBGRb accessor = new PixelAccessorBGRb(e.Width, e.Height, PixmapOrientation.BottomToTop, e.fData, e.Width*3);

            fAttendee.SendVideoFrame(accessor);
        }

        public void Start()
        {
            fCamera.Start();
        }

        public void Stop()
        {
            fCamera.Stop();
        }


    }
}
