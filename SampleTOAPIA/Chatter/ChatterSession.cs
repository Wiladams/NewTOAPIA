using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

using NewTOAPIA.Net.Rtp;

namespace Chatter
{
    public class ChatterSession : MultiSession
    {
        public ChatterSession(IPEndPoint endPoint, string uniqueSessionName, string friendlyName, bool rtpTraffic, bool receiveData, IPEndPoint reflector)
            :base(endPoint, uniqueSessionName, friendlyName, rtpTraffic, receiveData, reflector)
        {
        }

        protected override void ParticipantAdded(object sender, RtpEvents.RtpParticipantEventArgs ea)
        {
            Console.WriteLine("Added: CNAME: {0} Name: {1}", 
                ea.RtpParticipant.CName, ea.RtpParticipant.Name);
        }

        protected override void ParticipantRemoved(object sender, RtpEvents.RtpParticipantEventArgs ea)
        {
            Console.WriteLine("Removed: CNAME: {0} Name: {1}", 
                ea.RtpParticipant.CName, ea.RtpParticipant.Name);
        }
    }
}
