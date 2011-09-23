namespace NewTOAPIA.Net.Udt
{
    using System;

    public class CPerfMon
    {
        // global measurements
        Int64 msTimeStamp;                 // time since the UDT entity is started, in milliseconds
        Int64 pktSentTotal;                // total number of sent data packets, including retransmissions
        Int64 pktRecvTotal;                // total number of received packets
        int pktSndLossTotal;                 // total number of lost packets (sender side)
        int pktRcvLossTotal;                 // total number of lost packets (receiver side)
        int pktRetransTotal;                 // total number of retransmitted packets
        int pktSentACKTotal;                 // total number of sent ACK packets
        int pktRecvACKTotal;                 // total number of received ACK packets
        int pktSentNAKTotal;                 // total number of sent NAK packets
        int pktRecvNAKTotal;                 // total number of received NAK packets
        Int64 usSndDurationTotal;		// total time duration when UDT is sending data (idle time exclusive)

        // local measurements
        internal Int64 pktSent;                     // number of sent data packets, including retransmissions
        internal Int64 pktRecv;                     // number of received packets
        internal int pktSndLoss;                      // number of lost packets (sender side)
        internal int pktRcvLoss;                      // number of lost packets (receiver side)
        internal int pktRetrans;                      // number of retransmitted packets
        internal int pktSentACK;                      // number of sent ACK packets
        internal int pktRecvACK;                      // number of received ACK packets
        internal int pktSentNAK;                      // number of sent NAK packets
        internal int pktRecvNAK;                      // number of received NAK packets
        internal double mbpsSendRate;                 // sending rate in Mb/s
        internal double mbpsRecvRate;                 // receiving rate in Mb/s
        internal Int64 usSndDuration;		// busy sending time (i.e., idle time exclusive)

        // instant measurements
        internal double usPktSndPeriod;               // packet sending period, in microseconds
        internal int pktFlowWindow;                   // flow window size, in number of packets
        internal int pktCongestionWindow;             // congestion window size, in number of packets
        internal int pktFlightSize;                   // number of packets on flight
        internal double msRTT;                        // RTT, in milliseconds
        internal double mbpsBandwidth;                // estimated bandwidth, in Mb/s
        internal int byteAvailSndBuf;                 // available UDT sender buffer size
        internal int byteAvailRcvBuf;                 // available UDT receiver buffer size
    }
}