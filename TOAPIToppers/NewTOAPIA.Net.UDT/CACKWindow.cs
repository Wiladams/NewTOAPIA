namespace NewTOAPIA.Net.Udt
{
    using System;

    public class CACKWindow
    {
        int[] m_piACKSeqNo;       // Seq. No. for the ACK packet
        int[] m_piACK;            // Data Seq. No. carried by the ACK packet
        long[] m_pTimeStamp;      // The timestamp when the ACK was sent

        int m_iSize;                 // Size of the ACK history window
        int m_iHead;                 // Pointer to the lastest ACK record
        int m_iTail;                 // Pointer to the oldest ACK record

        public CACKWindow()
            : this(1024)
        {
        }

        public CACKWindow(int size)
        {
            m_iSize = size;
            m_iHead = 0;
            m_iTail = 0;

            m_piACKSeqNo = new int[m_iSize];
            m_piACK = new int[m_iSize];
            m_pTimeStamp = new long[m_iSize];

            m_piACKSeqNo[0] = -1;
        }

        // Functionality:
        //    Write an ACK record into the window.
        // Parameters:
        //    0) [in] seq: ACK seq. no.
        //    1) [in] ack: DATA ACK no.
        // Returned value:
        //    None.

        public void store(Int32 seq, Int32 ack)
        {
            m_piACKSeqNo[m_iHead] = seq;
            m_piACK[m_iHead] = ack;
            m_pTimeStamp[m_iHead] = CClock.getTime();

            m_iHead = (m_iHead + 1) % m_iSize;

            // overwrite the oldest ACK since it is not likely to be acknowledged
            if (m_iHead == m_iTail)
                m_iTail = (m_iTail + 1) % m_iSize;
        }

        // Functionality:
        //    Search the ACK-2 "seq" in the window, find out the DATA "ack" and caluclate RTT .
        // Parameters:
        //    0) [in] seq: ACK-2 seq. no.
        //    1) [out] ack: the DATA ACK no. that matches the ACK-2 no.
        // Returned value:
        //    RTT.

        int acknowledge(Int32 seq, Int32 ack)
        {
            if (m_iHead >= m_iTail)
            {
                // Head has not exceeded the physical boundary of the window

                for (int i = m_iTail, n = m_iHead; i < n; ++i)
                    // looking for indentical ACK Seq. No.
                    if (seq == m_piACKSeqNo[i])
                    {
                        // return the Data ACK it carried
                        ack = m_piACK[i];

                        // calculate RTT
                        int rtt = (int)(CClock.getTime() - m_pTimeStamp[i]);
                        if (i + 1 == m_iHead)
                        {
                            m_iTail = m_iHead = 0;
                            m_piACKSeqNo[0] = -1;
                        }
                        else
                            m_iTail = (i + 1) % m_iSize;

                        return rtt;
                    }

                // Bad input, the ACK node has been overwritten
                return -1;
            }

            // Head has exceeded the physical window boundary, so it is behind tail
            for (int j = m_iTail, n = m_iHead + m_iSize; j < n; ++j)
                // looking for indentical ACK seq. no.
                if (seq == m_piACKSeqNo[j % m_iSize])
                {
                    // return Data ACK
                    j %= m_iSize;
                    ack = m_piACK[j];

                    // calculate RTT
                    int rtt = (int)(CClock.getTime() - m_pTimeStamp[j]);
                    if (j == m_iHead)
                    {
                        m_iTail = m_iHead = 0;
                        m_piACKSeqNo[0] = -1;
                    }
                    else
                        m_iTail = (j + 1) % m_iSize;

                    return rtt;
                }

            // bad input, the ACK node has been overwritten
            return -1;
        }
    }
}
