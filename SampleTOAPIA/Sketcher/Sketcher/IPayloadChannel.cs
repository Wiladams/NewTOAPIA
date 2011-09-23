using NewTOAPIA;
using NewTOAPIA.Net.Rtp;

namespace HamSketch
{
    public interface IPayloadChannel
    {
        PayloadType ChannelType {get;}
        RtpSender Sender { get; }
        RtpSession Session { get; }

        void JoinSession(RtpSession session, string participantName);
    }
}
