namespace NewTOAPIA.Net.Udt
{
    using System.Net.Sockets;

    public class CMultiplexer
    {
        public CSndQueue m_pSndQueue;	// The sending queue
        public CRcvQueue m_pRcvQueue;	// The receiving queue
        public CChannel m_pChannel;	    // The UDP channel for sending and receiving
        public CClock m_pTimer;		    // The timer

        public int m_iPort;			    // The UDP port number of this multiplexer
        public AddressFamily m_iIPversion;		// IP version
        public int m_iMSS;			    // Maximum Segment Size
        public int m_iRefCount;		    // number of UDT instances that are associated with this multiplexer
        public bool m_bReusable;		// if this one can be shared with others
    }
}