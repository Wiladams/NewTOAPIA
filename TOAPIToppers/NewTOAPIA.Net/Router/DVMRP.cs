using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewTOAPIA.Net.Router
{
    public class DVMRP : RoutingProtocol
    {
        // BASED on RIP - 
        // Features of TRPB - Truncated Reverse Path Broadcasting
        // Encapsulated in IP datagrams (protocol - IGMP (2))
        // Interior Gateway Protocol (within an autonomous system)

        // header
        // 4 bits - version
        // 8 bits - Type
        // 16 bits - Sub-Type
        // 24-32 bits - Checksum
        // DVMRP Data Stream

    }
}
