namespace NewTOAPIA.Net.Udt
{
    using System;

    public class CHandShake
    {
        Int32 m_iVersion;          // UDT version
        Int32 m_iType;             // UDT socket type
        Int32 m_iISN;              // random initial sequence number
        Int32 m_iMSS;              // maximum segment size
        Int32 m_iFlightFlagSize;   // flow control window size
        Int32 m_iReqType;          // connection request type: 1: regular connection request, 0: rendezvous connection request, -1/-2: response
        Int32 m_iID;		// socket ID
        Int32 m_iCookie;		// cookie
        UInt32[] m_piPeerIP = new UInt32[4];	// The IP address that the peer's UDP port is bound to
    }
}
