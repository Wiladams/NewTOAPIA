using System;
using System.Collections.Generic;
using System.Text;

using NewTOAPIA.Net.Rtp;

namespace ShowIt
{
    public interface IReceiveDesktopFrame
    {
        void ReceiveDesktopFrame(object sender, RtpStream.FrameReceivedEventArgs ea);
    }

    public interface IReceiveVideoFrame
    {
        void ReceiveVideoFrame(object sender, RtpStream.FrameReceivedEventArgs ea);
    }

    public interface IReceiveAudioFrame
    {
        void ReceiveAudioFrame(object sender, RtpStream.FrameReceivedEventArgs ea);
    }

    /// <summary>
    /// This interface represents an ability to receive specific types 
    /// of frames that are brought into the conference.
    /// </summary>
    public interface IReceiveConferenceFrames : IReceiveDesktopFrame, IReceiveAudioFrame, IReceiveVideoFrame
    {
    }
}
