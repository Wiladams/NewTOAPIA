using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewTOAPIA.Net.Udp
{
    using System.Net;
    using NewTOAPIA;


//Client #1 sends the SYNCH_REQUEST message to the server and records the time at which the message was sent.
//#

//The Server forwards to the Client #2 the message cmdSYNCH_REQUEST
//#

//Client #2 receives the message cmdSYNCH_REQUEST and sends the message cmdSYNCH_REPLY to the server.
//#

//Client #1 receives the message cmdSYNCH_REPLY and records the time at which it has been received.

//[The programs repeat the steps 9,10,11 and 12 several times. The cmdSYNCH_REQUEST messages are spaced by 20 milliseconds]
//# At this point the primary client has gathered the data needed to estimate the latency, so now the client computes the latency, sets its clock to 0 and sends the message cmdSYNCH_DONE. This message includes the estimate of the latency. The primary client uses the following statistics to estimate the latency:

//A(n) = time at which the Nth cmdSYNCH_REPLY was received
//B(n) = time at which the Nth cmdSYNCH_REQUEST was sent
//X(n) = A(n) – B(n) / 2

//   1. Sort the X(n) from smallest latency to largest and choose the median value.
//   2. Compute the standard-deviation s = ( Σx2 - (Σx)2/ n ) / n – 1
//   3. Discard the X(n) values that are not in the interval: I : [ mid-point – s, mid-point + s ]
//   4. Compute the average Σx / n from the interval I, this value is going to be used as latency.

//#

//The Server forwards the message cmdSYNCH_DONE to the secondary client.
//#

//Client #2 receives the message cmdSYNCH_DONE and sets its clock to 0 + latency. 


    public class UdpLatency : Observer<UdpPingReply>
    {
        public virtual void OnNext(UdpPingReply reply)
        {
        }
    }
}
