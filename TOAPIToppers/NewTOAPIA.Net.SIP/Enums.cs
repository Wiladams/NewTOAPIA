using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewTOAPIA.Net.SIP
{
    /// <summary>
    /// SIP request methods
    /// </summary>
    public enum SIPMethods
    {
        INVITE,
        ACK,
        BYE,
        CANCEL,
        OPTIONS,
        REGISTER,
        PRACK,
        SUBSCRIBE,
        NOTIFY,
        PUBLISH,
        INFO,
        REFER,
        MESSAGE,
        UPDATE
    }
}
