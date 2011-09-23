
namespace NewTOAPIA.Net.Session
{
    using System;
    using System.Net;
    
    // http://www.ietf.org/rfc/rfc4566.txt

    public class IPParticipant
    {
        public Uri CanonicalName { get; private set; }

        public IPParticipant(Uri cname)
        {
            CanonicalName = cname;
        }

    }
}
