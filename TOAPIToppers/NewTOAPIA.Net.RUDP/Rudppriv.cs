using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewTOAPIA.Net.RUDP
{

    public class Rudppriv
    {
        Ipht ht;

        /* MIB counters */
        Rudpstats ustats;

        /* non-MIB stats */
        ulong csumerr;		/* checksum errors */
        ulong lenerr;			/* short packet */
        ulong rxmits;			/* # of retransmissions */
        ulong orders;			/* # of out of order pkts */

        /* keeping track of the ack kproc */
        int ackprocstarted;
        QLock apl;
    }
}
