/*****************************************************************************
Copyright (c) 2001 - 2009, The Board of Trustees of the University of Illinois.
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are
met:

* Redistributions of source code must retain the above
  copyright notice, this list of conditions and the
  following disclaimer.

* Redistributions in binary form must reproduce the
  above copyright notice, this list of conditions
  and the following disclaimer in the documentation
  and/or other materials provided with the distribution.

* Neither the name of the University of Illinois
  nor the names of its contributors may be used to
  endorse or promote products derived from this
  software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS
IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO,
THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*****************************************************************************/

/*****************************************************************************
written by
   Yunhong Gu, last updated 05/05/2009
*****************************************************************************/

namespace NewTOAPIA.Net.Udt
{
    using System;

    using UDTSOCKET = System.Int32;

    public class CCC
    {
        protected Int32 m_iSYNInterval;	// UDT constant parameter, SYN

        internal double m_dPktSndPeriod;              // Packet sending period, in microseconds
        internal double m_dCWndSize;                  // Congestion window size, in packets

        protected int m_iBandwidth;			// estimated bandwidth, packets per second
        protected double m_dMaxCWndSize;               // maximum cwnd size, in packets

        protected int m_iMSS;				// Maximum Packet Size, including all packet headers
        protected Int32 m_iSndCurrSeqNo;		// current maximum seq no sent out
        protected int m_iRcvRate;			// packet arrive rate at receiver side, packets per second
        protected int m_iRTT;				// current estimated RTT, microsecond

        protected object m_pcParam;			// user defined parameter
        protected int m_iPSize;			// size of m_pcParam

        private UDTSOCKET m_UDT;                     // The UDT entity that this congestion control algorithm is bound to

        private int m_iACKPeriod;                    // Periodical timer to send an ACK, in milliseconds
        private int m_iACKInterval;                  // How many packets to send one ACK, in packets

        private bool m_bUserDefinedRTO;              // if the RTO value is defined by users
        private int m_iRTO;                          // RTO value, microseconds

        private CPerfMon m_PerfInfo;                 // protocol statistics information


        public CCC()
        {
            //m_iBandwidth(),
            //m_dMaxCWndSize(),
            //m_iMSS(),
            //m_iSndCurrSeqNo(),
            //m_iRcvRate(),
            //m_iRTT(),
            //m_UDT(),
            //m_PerfInfo()

            m_iSYNInterval = CUDT.m_iSYNInterval;
            m_dPktSndPeriod = (1.0f);
            m_dCWndSize = (16.0f);
            m_pcParam = IntPtr.Zero;
            m_iPSize = (0);
            m_iACKPeriod = (0);
            m_iACKInterval = (0);
            m_bUserDefinedRTO = (false);
            m_iRTO = (-1);

        }

        ~CCC()
        {
            //m_pcParam.Dispose();
        }

        //private:
        //   CCC(const CCC&);
        //   CCC& operator=(const CCC&) {return *this;}

        // Functionality:
        //    Callback function to be called (only) at the start of a UDT connection.
        //    note that this is different from CCC(), which is always called.
        // Parameters:
        //    None.
        // Returned value:
        //    None.

        public virtual void init()
        {
        }

        // Functionality:
        //    Callback function to be called when a UDT connection is closed.
        // Parameters:
        //    None.
        // Returned value:
        //    None.

        public virtual void close()
        {
        }

        // Functionality:
        //    Callback function to be called when an ACK packet is received.
        // Parameters:
        //    0) [in] ackno: the data sequence number acknowledged by this ACK.
        // Returned value:
        //    None.

        public virtual void onACK(Int32 ack)
        {
        }

        // Functionality:
        //    Callback function to be called when a loss report is received.
        // Parameters:
        //    0) [in] losslist: list of sequence number of packets, in the format describled in packet.cpp.
        //    1) [in] size: length of the loss list.
        // Returned value:
        //    None.

        public virtual void onLoss(Int32[] lossList, int length)
        {
        }

        // Functionality:
        //    Callback function to be called when a timeout event occurs.
        // Parameters:
        //    None.
        // Returned value:
        //    None.

        public virtual void onTimeout()
        {
        }

        // Functionality:
        //    Callback function to be called when a data is sent.
        // Parameters:
        //    0) [in] seqno: the data sequence number.
        //    1) [in] size: the payload size.
        // Returned value:
        //    None.

        public virtual void onPktSent(CPacket packet)
        {
        }

        // Functionality:
        //    Callback function to be called when a data is received.
        // Parameters:
        //    0) [in] packet: the packet, including sequence number, and payload size.
        // Returned value:
        //    None.

        public virtual void onPktReceived(CPacket packet)
        {
        }

        // Functionality:
        //    Callback function to Process a user defined packet.
        // Parameters:
        //    0) [in] pkt: the user defined packet.
        // Returned value:
        //    None.

        public virtual void processCustomMsg(CPacket packet)
        {
        }


        // Functionality:
        //    Set periodical acknowldging and the ACK period.
        // Parameters:
        //    0) [in] msINT: the period to send an ACK.
        // Returned value:
        //    None.

        protected void setACKTimer(int msINT)
        {
            m_iACKPeriod = msINT;

            if (m_iACKPeriod > m_iSYNInterval)
                m_iACKPeriod = m_iSYNInterval;
        }

        // Functionality:
        //    Set packet-based acknowldging and the number of packets to send an ACK.
        // Parameters:
        //    0) [in] pktINT: the number of packets to send an ACK.
        // Returned value:
        //    None.

        protected void setACKInterval(int pktINT)
        {
            m_iACKInterval = pktINT;
        }

        // Functionality:
        //    Set RTO value.
        // Parameters:
        //    0) [in] msRTO: RTO in macroseconds.
        // Returned value:
        //    None.

        protected void setRTO(int usRTO)
        {
            m_bUserDefinedRTO = true;
            m_iRTO = usRTO;
        }

        // Functionality:
        //    Send a user defined control packet.
        // Parameters:
        //    0) [in] pkt: user defined packet.
        // Returned value:
        //    None.

        protected void sendCustomMsg(CPacket pkt)
        {
            CUDT u = CUDT.getUDTHandle(m_UDT);

            if (null != u)
            {
                pkt.m_iID = u.m_PeerID;
                u.m_pSndQueue.sendto(u.m_pPeerAddr, pkt);
            }
        }

        // Functionality:
        //    retrieve performance information.
        // Parameters:
        //    None.
        // Returned value:
        //    Pointer to a performance info structure.

        private CPerfMon getPerfInfo()
        {
            CUDT u = CUDT.getUDTHandle(m_UDT);
            if (null != u)
                u.sample(m_PerfInfo, false);

            return m_PerfInfo;
        }


        private void setMSS(int mss)
        {
            m_iMSS = mss;
        }

        private void setBandwidth(int bw)
        {
            m_iBandwidth = bw;
        }

        private void setSndCurrSeqNo(Int32 seqno)
        {
            m_iSndCurrSeqNo = seqno;
        }

        private void setRcvRate(int rcvrate)
        {
            m_iRcvRate = rcvrate;
        }

        private void setMaxCWndSize(int cwnd)
        {
            m_dMaxCWndSize = cwnd;
        }

        private void setRTT(int rtt)
        {
            m_iRTT = rtt;
        }

        // Functionality:
        //    Set user defined parameters.
        // Parameters:
        //    0) [in] param: the paramters in one buffer.
        //    1) [in] size: the size of the buffer.
        // Returned value:
        //    None.

        private void setUserParam(object param, int size)
        {
            m_pcParam.Dispose();
            m_pcParam = new byte[size];
            memcpy(m_pcParam, param, size);
            m_iPSize = size;
        }
    }
}
