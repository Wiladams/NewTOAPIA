namespace NewTOAPIA.Net.Udt
{
    using System;
    using System.Net.Sockets;

    public class CInfoBlock
    {
        internal byte[] m_piIP = new byte[4*4];
        internal AddressFamily m_iIPversion;
        internal Int64  m_ullTimeStamp;
        internal int m_iRTT;
        internal int m_iBandwidth;
        internal int m_iLossRate;
        internal int m_iReorderDistance;
        internal double m_dInterval;
        internal double m_dCWnd;
    }
}