namespace NewTOAPIA.Net.Udt
{
    using System;

    public class CUDTCC : CCC
    {
        int m_iRCInterval;			// UDT Rate control interval
        Int64 m_LastRCTime;		// last rate increase time
        bool m_bSlowStart;			// if in slow start phase
        Int32 m_iLastAck;			// last ACKed seq no
        bool m_bLoss;			// if loss happened since last rate increase
        Int32 m_iLastDecSeq;		// max pkt seq no sent out when last decrease happened
        double m_dLastDecPeriod;		// value of pktsndperiod when last decrease happened
        int m_iNAKCount;                     // NAK counter
        int m_iDecRandom;                    // random threshold on decrease by number of loss events
        int m_iAvgNAKNum;                    // average number of NAKs per congestion
        int m_iDecCount;			// number of decreases in a congestion epoch

        public CUDTCC()
        {
            //       m_iRCInterval(),
            //m_LastRCTime(),
            //m_bSlowStart(),
            //m_iLastAck(),
            //m_bLoss(),
            //m_iLastDecSeq(),
            //m_dLastDecPeriod(),
            //m_iNAKCount(),
            //m_iDecRandom(),
            //m_iAvgNAKNum(),
            //m_iDecCount()
        }


        public virtual void init()
        {
            m_iRCInterval = m_iSYNInterval;
            m_LastRCTime = CClock.getTime();
            setACKTimer(m_iRCInterval);

            m_bSlowStart = true;
            m_iLastAck = m_iSndCurrSeqNo;
            m_bLoss = false;
            m_iLastDecSeq = CSeqNo.decseq(m_iLastAck);
            m_dLastDecPeriod = 1;
            m_iAvgNAKNum = 0;
            m_iNAKCount = 0;
            m_iDecRandom = 1;

            m_dCWndSize = 16;
            m_dPktSndPeriod = 1;
        }

        public virtual void onACK(Int32 ack)
        {
            Int64 currtime = CClock.getTime();
            if (currtime - m_LastRCTime < (UInt32)m_iRCInterval)
                return;

            m_LastRCTime = currtime;

            if (m_bSlowStart)
            {
                m_dCWndSize += CSeqNo.seqlen(m_iLastAck, ack);
                m_iLastAck = ack;

                if (m_dCWndSize > m_dMaxCWndSize)
                {
                    m_bSlowStart = false;
                    if (m_iRcvRate > 0)
                        m_dPktSndPeriod = 1000000.0 / m_iRcvRate;
                    else
                        m_dPktSndPeriod = m_dCWndSize / (m_iRTT + m_iRCInterval);
                }
            }
            else
                m_dCWndSize = m_iRcvRate / 1000000.0 * (m_iRTT + m_iRCInterval) + 16;

            // During Slow Start, no rate increase
            if (m_bSlowStart)
                return;

            if (m_bLoss)
            {
                m_bLoss = false;
                return;
            }

            Int64 B = (Int64)(m_iBandwidth - 1000000.0 / m_dPktSndPeriod);
            if ((m_dPktSndPeriod > m_dLastDecPeriod) && ((m_iBandwidth / 9) < B))
                B = m_iBandwidth / 9;

            double inc;

            if (B <= 0)
                inc = 1.0 / m_iMSS;
            else
            {
                // inc = max(10 ^ ceil(log10( B * MSS * 8 ) * Beta / MSS, 1/MSS)
                // Beta = 1.5 * 10^(-6)

                inc = Math.Pow(10.0, Math.Ceiling(Math.Log10(B * m_iMSS * 8.0))) * 0.0000015 / m_iMSS;

                if (inc < 1.0 / m_iMSS)
                    inc = 1.0 / m_iMSS;
            }

            m_dPktSndPeriod = (m_dPktSndPeriod * m_iRCInterval) / (m_dPktSndPeriod * inc + m_iRCInterval);

            //set maximum transfer rate
            if ((m_pcParam != IntPtr.Zero) && (m_iPSize == 8))
            {
                Int64 maxSR = m_pcParam.ToInt64();
                if (maxSR <= 0)
                    return;

                double minSP = 1000000.0 / ((double)(maxSR) / m_iMSS);
                if (m_dPktSndPeriod < minSP)
                    m_dPktSndPeriod = minSP;
            }
        }

        public virtual void onLoss(Int32[] losslist, int anInt)
{
   //Slow Start stopped, if it hasn't yet
   if (m_bSlowStart)
   {
      m_bSlowStart = false;
      if (m_iRcvRate > 0)
         m_dPktSndPeriod = 1000000.0 / m_iRcvRate;
      else
         m_dPktSndPeriod = m_dCWndSize / (m_iRTT + m_iRCInterval);
   }

   m_bLoss = true;

   if (CSeqNo.seqcmp(losslist[0] & 0x7FFFFFFF, m_iLastDecSeq) > 0)
   {
      m_dLastDecPeriod = m_dPktSndPeriod;
      m_dPktSndPeriod = Math.Ceiling(m_dPktSndPeriod * 1.125);

      m_iAvgNAKNum = (int)Math.Ceiling(m_iAvgNAKNum * 0.875 + m_iNAKCount * 0.125);
      m_iNAKCount = 1;
      m_iDecCount = 1;

      m_iLastDecSeq = m_iSndCurrSeqNo;

      // remove global synchronization using randomization
      srand(m_iLastDecSeq);
      m_iDecRandom = (int)Math.Ceiling(m_iAvgNAKNum * ((double)(rand()) / RAND_MAX));
      if (m_iDecRandom < 1)
         m_iDecRandom = 1;
   }
   else if ((m_iDecCount ++ < 5) && (0 == (++ m_iNAKCount % m_iDecRandom)))
   {
      // 0.875^5 = 0.51, rate should not be decreased by more than half within a congestion period
      m_dPktSndPeriod = Math.Ceiling(m_dPktSndPeriod * 1.125);
      m_iLastDecSeq = m_iSndCurrSeqNo;
   }
}

        public virtual void onTimeout()
        {
            if (m_bSlowStart)
            {
                m_bSlowStart = false;
                if (m_iRcvRate > 0)
                    m_dPktSndPeriod = 1000000.0 / m_iRcvRate;
                else
                    m_dPktSndPeriod = m_dCWndSize / (m_iRTT + m_iRCInterval);
            }
            else
            {
                /*
                m_dLastDecPeriod = m_dPktSndPeriod;
                m_dPktSndPeriod = ceil(m_dPktSndPeriod * 2);
                m_iLastDecSeq = m_iLastAck;
                */
            }
        }
    }
}