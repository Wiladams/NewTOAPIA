using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewTOAPIA.Net.Rtp
{
    /// <summary>
    /// SessionExeption is thrown if anything goes wrong in the session object
    /// </summary>
    public class SessionException : ApplicationException
    {
        public SessionException(string msg) : base(msg) { }
    }

    /// <summary>
    /// RtpTrafficDisabledException is thrown if Rtp methods in the Session are accessed
    /// while in rtpTraffic == false mode
    /// </summary>
    public class RtpTrafficDisabledException : ApplicationException
    {
        public RtpTrafficDisabledException(string msg) : base(msg) { }
    }
    
    /// <summary>
    /// RtcpSendingDisabledException is thrown if Rtcp sender methods in the Session are accessed
    /// while participant == null
    /// </summary>
    public class RtcpSendingDisabledException : ApplicationException
    {
        public RtcpSendingDisabledException(string msg) : base(msg) { }
    }

}
