namespace NewTOAPIA.Net.Udt
{
    using System;

    public class CRcvLossList : CLossList
    {           
        int[] m_piPrior;                      // prior node in the list;

        int m_iTail;                         // last node in the list;
        long m_TimeStamp;		// last list update time or NAK feedback time


        public CRcvLossList(int size)
            :base(size)
        {
            m_iTail = (-1);
            m_piPrior = new int[m_iSize];
            m_TimeStamp = CClock.getTime();
        }


        // Functionality:
        //    Insert a series of loss seq. no. between "seqno1" and "seqno2" into the receiver's loss list.
        // Parameters:
        //    0) [in] seqno1: sequence number starts.
        //    1) [in] seqno2: seqeunce number ends.
        // Returned value:
        //    None.

        public void insert(Int32 seqno1, Int32 seqno2)
        {
            m_TimeStamp = CClock.getTime();

            // Data to be inserted must be larger than all those in the list
            // guaranteed by the UDT receiver

            if (0 == m_iLength)
            {
                // insert data into an empty list
                m_iHead = 0;
                m_iTail = 0;
                m_piData1[m_iHead] = seqno1;
                if (seqno2 != seqno1)
                    m_piData2[m_iHead] = seqno2;

                m_piNext[m_iHead] = -1;
                m_piPrior[m_iHead] = -1;
                m_iLength += CSeqNo.seqlen(seqno1, seqno2);

                return;
            }

            // otherwise searching for the position where the node should be
            int offset = CSeqNo.seqoff(m_piData1[m_iHead], seqno1);
            int loc = (m_iHead + offset) % m_iSize;

            if ((-1 != m_piData2[m_iTail]) && (CSeqNo.incseq(m_piData2[m_iTail]) == seqno1))
            {
                // coalesce with prior node, e.g., [2, 5], [6, 7] becomes [2, 7]
                loc = m_iTail;
                m_piData2[loc] = seqno2;
            }
            else
            {
                // create new node
                m_piData1[loc] = seqno1;

                if (seqno2 != seqno1)
                    m_piData2[loc] = seqno2;

                m_piNext[m_iTail] = loc;
                m_piPrior[loc] = m_iTail;
                m_piNext[loc] = -1;
                m_iTail = loc;
            }

            m_iLength += CSeqNo.seqlen(seqno1, seqno2);
        }

        // Functionality:
        //    Remove a loss seq. no. from the receiver's loss list.
        // Parameters:
        //    0) [in] seqno: sequence number.
        // Returned value:
        //    if the packet is removed (true) or no such lost packet is found (false).

        public override bool remove(int seqno)
        {
            m_TimeStamp = CClock.getTime();

            if (0 == m_iLength)
                return false;

            // locate the position of "seqno" in the list
            int offset = CSeqNo.seqoff(m_piData1[m_iHead], seqno);
            if (offset < 0)
                return false;

            int loc = (m_iHead + offset) % m_iSize;

            if (seqno == m_piData1[loc])
            {
                // This is a seq. no. that starts the loss sequence

                if (-1 == m_piData2[loc])
                {
                    // there is only 1 loss in the sequence, delete it from the node
                    if (m_iHead == loc)
                    {
                        m_iHead = m_piNext[m_iHead];
                        if (-1 != m_iHead)
                            m_piPrior[m_iHead] = -1;
                    }
                    else
                    {
                        m_piNext[m_piPrior[loc]] = m_piNext[loc];
                        if (-1 != m_piNext[loc])
                            m_piPrior[m_piNext[loc]] = m_piPrior[loc];
                        else
                            m_iTail = m_piPrior[loc];
                    }

                    m_piData1[loc] = -1;
                }
                else
                {
                    // there are more than 1 loss in the sequence
                    // move the node to the next and update the starter as the next loss inSeqNo(seqno)

                    // find next node
                    int i = (loc + 1) % m_iSize;

                    // remove the "seqno" and change the starter as next seq. no.
                    m_piData1[i] = CSeqNo.incseq(m_piData1[loc]);

                    // process the sequence end
                    if (CSeqNo.seqcmp(m_piData2[loc], CSeqNo.incseq(m_piData1[loc])) > 0)
                        m_piData2[i] = m_piData2[loc];

                    // remove the current node
                    m_piData1[loc] = -1;
                    m_piData2[loc] = -1;

                    // update list pointer
                    m_piNext[i] = m_piNext[loc];
                    m_piPrior[i] = m_piPrior[loc];

                    if (m_iHead == loc)
                        m_iHead = i;
                    else
                        m_piNext[m_piPrior[i]] = i;

                    if (m_iTail == loc)
                        m_iTail = i;
                    else
                        m_piPrior[m_piNext[i]] = i;
                }

                m_iLength--;

                return true;
            }

            // There is no loss sequence in the current position
            // the "seqno" may be contained in a previous node

            // searching previous node
            int i = (loc - 1 + m_iSize) % m_iSize;
            while (-1 == m_piData1[i])
                i = (i - 1 + m_iSize) % m_iSize;

            // not contained in this node, return
            if ((-1 == m_piData2[i]) || (CSeqNo.seqcmp(seqno, m_piData2[i]) > 0))
                return false;

            if (seqno == m_piData2[i])
            {
                // it is the sequence end

                if (seqno == CSeqNo.incseq(m_piData1[i]))
                    m_piData2[i] = -1;
                else
                    m_piData2[i] = CSeqNo.decseq(seqno);
            }
            else
            {
                // split the sequence

                // construct the second sequence from CSeqNo.incseq(seqno) to the original sequence end
                // located at "loc + 1"
                loc = (loc + 1) % m_iSize;

                m_piData1[loc] = CSeqNo.incseq(seqno);
                if (CSeqNo.seqcmp(m_piData2[i], m_piData1[loc]) > 0)
                    m_piData2[loc] = m_piData2[i];

                // the first (original) sequence is between the original sequence start to CSeqNo.decseq(seqno)
                if (seqno == CSeqNo.incseq(m_piData1[i]))
                    m_piData2[i] = -1;
                else
                    m_piData2[i] = CSeqNo.decseq(seqno);

                // update the list pointer
                m_piNext[loc] = m_piNext[i];
                m_piNext[i] = loc;
                m_piPrior[loc] = i;

                if (m_iTail == i)
                    m_iTail = loc;
                else
                    m_piPrior[m_piNext[loc]] = loc;
            }

            m_iLength--;

            return true;
        }

        // Functionality:
        //    Remove all packets between seqno1 and seqno2.
        // Parameters:
        //    0) [in] seqno1: start sequence number.
        //    1) [in] seqno2: end sequence number.
        // Returned value:
        //    if the packet is removed (true) or no such lost packet is found (false).

        public bool remove(Int32 seqno1, Int32 seqno2)
        {
            if (seqno1 <= seqno2)
            {
                for (Int32 i = seqno1; i <= seqno2; ++i)
                    remove(i);
            }
            else
            {
                for (Int32 j = seqno1; j < CSeqNo.m_iMaxSeqNo; ++j)
                    remove(j);
                for (Int32 k = 0; k <= seqno2; ++k)
                    remove(k);
            }

            return true;
        }

        // Functionality:
        //    Find if there is any lost packets whose sequence number falling seqno1 and seqno2.
        // Parameters:
        //    0) [in] seqno1: start sequence number.
        //    1) [in] seqno2: end sequence number.
        // Returned value:
        //    True if found; otherwise false.

        public bool find(Int32 seqno1, Int32 seqno2)
        {
            if (0 == m_iLength)
                return false;

            int p = m_iHead;

            while (-1 != p)
            {
                if ((CSeqNo.seqcmp(m_piData1[p], seqno1) == 0) ||
                    ((CSeqNo.seqcmp(m_piData1[p], seqno1) > 0) && (CSeqNo.seqcmp(m_piData1[p], seqno2) <= 0)) ||
                    ((CSeqNo.seqcmp(m_piData1[p], seqno1) < 0) && (m_piData2[p] != -1) && CSeqNo.seqcmp(m_piData2[p], seqno1) >= 0))
                    return true;

                p = m_piNext[p];
            }

            return false;
        }


        // Functionality:
        //    Read the first (smallest) seq. no. in the list.
        // Parameters:
        //    None.
        // Returned value:
        //    the sequence number or -1 if the list is empty.

        int getFirstLostSeq()
        {
            if (0 == m_iLength)
                return -1;

            return m_piData1[m_iHead];
        }

        // Functionality:
        //    Get a encoded loss array for NAK report.
        // Parameters:
        //    0) [out] array: the result list of seq. no. to be included in NAK.
        //    1) [out] physical length of the result array.
        //    2) [in] limit: maximum length of the array.
        //    3) [in] threshold: Time threshold from last NAK report.
        // Returned value:
        //    None.

        public void getLossArray(Int32[] array, int len, int limit, int threshold)
        {
            len = 0;

            // do not feedback NAK unless no retransmission is received within a certain interval
            if ((int)(CClock.getTime() - m_TimeStamp) < threshold)
                return;

            int i = m_iHead;

            while ((len < limit - 1) && (-1 != i))
            {
                array[len] = m_piData1[i];
                if (-1 != m_piData2[i])
                {
                    // there are more than 1 loss in the sequence
                    array[len] |= unchecked(0x80000000);
                    ++len;
                    array[len] = m_piData2[i];
                }

                ++len;

                i = m_piNext[i];
            }

            m_TimeStamp = CClock.getTime();
        }
    }
}
