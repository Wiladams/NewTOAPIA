using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewTOAPIA.Net.RUDP
{
    public enum UDP
    {
        UDP_PHDRSIZE = 12,	/* pseudo header */
        //	UDP_HDRSIZE	= 20,	    /* pseudo header + udp header */
        UDP_RHDRSIZE = 36,	/* pseudo header + udp header + rudp header */
        UDP_IPHDR = 8,	    /* ip header */
        IP_UDPPROTO = 254,
        UDP_USEAD7 = 52,	    /* size of new ipv6 headers struct */

        Rudprxms = 200,
        Rudptickms = 50,
        Rudpmaxxmit = 10,
        Maxunacked = 100,
    }
}
