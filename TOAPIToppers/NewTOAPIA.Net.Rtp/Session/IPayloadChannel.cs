using NewTOAPIA;

namespace NewTOAPIA.Net.Rtp
{
    public interface IPayloadChannel
    {
        PayloadType ChannelType {get;}
        RtpSender Sender { get; }
        RtpSession Session { get; }

        void JoinSession(RtpSession session, string participantName);
    }
}
