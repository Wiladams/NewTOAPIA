namespace NewTOAPIA.Net.Udt
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public class CSndLossList : CLossList
    {
        int m_iLastInsertPos;                // position of last insert node

        Mutex m_ListLock;          // used to synchronize list operation


        public CSndLossList(int size)
            :base(size)
        {
            m_iLastInsertPos = (-1);

            // sender list needs mutex protection
            m_ListLock = new Mutex();
        }

        //~CSndLossList()
        //{
        //    m_ListLock.Close();
        //}

        // Functionality:
        //    Insert a seq. no. into the sender loss list.
        // Parameters:
        //    0) [in] seqno1: sequence number starts.
        //    1) [in] seqno2: sequence number ends.
        // Returned value:
        //    number of packets that are not in the list previously.

        public override int insert(Int32 seqno1, Int32 seqno2)
        {
            CGuard listguard = new CGuard(m_ListLock);

            if (0 == m_iLength)
            {
                // insert data into an empty list

                m_iHead = 0;
                m_piData1[m_iHead] = seqno1;
                if (seqno2 != seqno1)
                    m_piData2[m_iHead] = seqno2;

                m_piNext[m_iHead] = -1;
                m_iLastInsertPos = m_iHead;

                m_iLength += CSeqNo.seqlen(seqno1, seqno2);

                return m_iLength;
            }

            // otherwise find the position where the data can be inserted
            int origlen = m_iLength;
            int offset = CSeqNo.seqoff(m_piData1[m_iHead], seqno1);
            int loc = (m_iHead + offset + m_iSize) % m_iSize;

            if (offset < 0)
            {
                // Insert data prior to the head pointer

                m_piData1[loc] = seqno1;
                if (seqno2 != seqno1)
                    m_piData2[loc] = seqno2;

                // new node becomes head
                m_piNext[loc] = m_iHead;
                m_iHead = loc;
                m_iLastInsertPos = loc;

                m_iLength += CSeqNo.seqlen(seqno1, seqno2);
            }
            else if (offset > 0)
            {
                if (seqno1 == m_piData1[loc])
                {
                    m_iLastInsertPos = loc;

                    // first seqno is equivlent, compare the second
                    if (-1 == m_piData2[loc])
                    {
                        if (seqno2 != seqno1)
                        {
                            m_iLength += CSeqNo.seqlen(seqno1, seqno2) - 1;
                            m_piData2[loc] = seqno2;
                        }
                    }
                    else if (CSeqNo.seqcmp(seqno2, m_piData2[loc]) > 0)
                    {
                        // new seq pair is longer than old pair, e.g., insert [3, 7] to [3, 5], becomes [3, 7]
                        m_iLength += CSeqNo.seqlen(m_piData2[loc], seqno2) - 1;
                        m_piData2[loc] = seqno2;
                    }
                    else
                        // Do nothing if it is already there
                        return 0;
                }
                else
                {
                    // searching the prior node
                    int i;
                    if ((-1 != m_iLastInsertPos) && (CSeqNo.seqcmp(m_piData1[m_iLastInsertPos], seqno1) < 0))
                        i = m_iLastInsertPos;
                    else
                        i = m_iHead;

                    while ((-1 != m_piNext[i]) && (CSeqNo.seqcmp(m_piData1[m_piNext[i]], seqno1) < 0))
                        i = m_piNext[i];

                    if ((-1 == m_piData2[i]) || (CSeqNo.seqcmp(m_piData2[i], seqno1) < 0))
                    {
                        m_iLastInsertPos = loc;

                        // no overlap, create new node
                        m_piData1[loc] = seqno1;
                        if (seqno2 != seqno1)
                            m_piData2[loc] = seqno2;

                        m_piNext[loc] = m_piNext[i];
                        m_piNext[i] = loc;

                        m_iLength += CSeqNo.seqlen(seqno1, seqno2);
                    }
                    else
                    {
                        m_iLastInsertPos = i;

                        // overlap, coalesce with prior node, insert(3, 7) to [2, 5], ... becomes [2, 7]
                        if (CSeqNo.seqcmp(m_piData2[i], seqno2) < 0)
                        {
                            m_iLength += CSeqNo.seqlen(m_piData2[i], seqno2) - 1;
                            m_piData2[i] = seqno2;

                            loc = i;
                        }
                        else
                            return 0;
                    }
                }
            }
            else
            {
                m_iLastInsertPos = m_iHead;

                // insert to head node
                if (seqno2 != seqno1)
                {
                    if (-1 == m_piData2[loc])
                    {
                        m_iLength += CSeqNo.seqlen(seqno1, seqno2) - 1;
                        m_piData2[loc] = seqno2;
                    }
                    else if (CSeqNo.seqcmp(seqno2, m_piData2[loc]) > 0)
                    {
                        m_iLength += CSeqNo.seqlen(m_piData2[loc], seqno2) - 1;
                        m_piData2[loc] = seqno2;
                    }
                    else
                        return 0;
                }
                else
                    return 0;
            }

            // coalesce with next node. E.g., [3, 7], ..., [6, 9] becomes [3, 9] 
            while ((-1 != m_piNext[loc]) && (-1 != m_piData2[loc]))
            {
                int i = m_piNext[loc];

                if (CSeqNo.seqcmp(m_piData1[i], CSeqNo.incseq(m_piData2[loc])) <= 0)
                {
                    // coalesce if there is overlap
                    if (-1 != m_piData2[i])
                    {
                        if (CSeqNo.seqcmp(m_piData2[i], m_piData2[loc]) > 0)
                        {
                            if (CSeqNo.seqcmp(m_piData2[loc], m_piData1[i]) >= 0)
                                m_iLength -= CSeqNo.seqlen(m_piData1[i], m_piData2[loc]);

                            m_piData2[loc] = m_piData2[i];
                        }
                        else
                            m_iLength -= CSeqNo.seqlen(m_piData1[i], m_piData2[i]);
                    }
                    else
                    {
                        if (m_piData1[i] == CSeqNo.incseq(m_piData2[loc]))
                            m_piData2[loc] = m_piData1[i];
                        else
                            m_iLength--;
                    }

                    m_piData1[i] = -1;
                    m_piData2[i] = -1;
                    m_piNext[loc] = m_piNext[i];
                }
                else
                    break;
            }

            return m_iLength - origlen;
        }

        // Functionality:
        //    Remove ALL the seq. no. that are not greater than the parameter.
        // Parameters:
        //    0) [in] seqno: sequence number.
        // Returned value:
        //    None.

        public override bool remove(int seqno)
        {
            CGuard listguard = new CGuard(m_ListLock);

            if (0 == m_iLength)
                return false;

            // Remove all from the head pointer to a node with a larger seq. no. or the list is empty
            int offset = CSeqNo.seqoff(m_piData1[m_iHead], seqno);
            int loc = (m_iHead + offset + m_iSize) % m_iSize;

            if (0 == offset)
            {
                // It is the head. Remove the head and point to the next node
                loc = (loc + 1) % m_iSize;

                if (-1 == m_piData2[m_iHead])
                    loc = m_piNext[m_iHead];
                else
                {
                    m_piData1[loc] = CSeqNo.incseq(seqno);
                    if (CSeqNo.seqcmp(m_piData2[m_iHead], CSeqNo.incseq(seqno)) > 0)
                        m_piData2[loc] = m_piData2[m_iHead];

                    m_piData2[m_iHead] = -1;

                    m_piNext[loc] = m_piNext[m_iHead];
                }

                m_piData1[m_iHead] = -1;

                if (m_iLastInsertPos == m_iHead)
                    m_iLastInsertPos = -1;

                m_iHead = loc;

                m_iLength--;
            }
            else if (offset > 0)
            {
                int h = m_iHead;

                if (seqno == m_piData1[loc])
                {
                    // target node is not empty, remove part/all of the seqno in the node.
                    int temp = loc;
                    loc = (loc + 1) % m_iSize;

                    if (-1 == m_piData2[temp])
                        m_iHead = m_piNext[temp];
                    else
                    {
                        // remove part, e.g., [3, 7] becomes [], [4, 7] after remove(3)
                        m_piData1[loc] = CSeqNo.incseq(seqno);
                        if (CSeqNo.seqcmp(m_piData2[temp], m_piData1[loc]) > 0)
                            m_piData2[loc] = m_piData2[temp];
                        m_iHead = loc;
                        m_piNext[loc] = m_piNext[temp];
                        m_piNext[temp] = loc;
                        m_piData2[temp] = -1;
                    }
                }
                else
                {
                    // target node is empty, check prior node
                    int i = m_iHead;
                    while ((-1 != m_piNext[i]) && (CSeqNo.seqcmp(m_piData1[m_piNext[i]], seqno) < 0))
                        i = m_piNext[i];

                    loc = (loc + 1) % m_iSize;

                    if (-1 == m_piData2[i])
                        m_iHead = m_piNext[i];
                    else if (CSeqNo.seqcmp(m_piData2[i], seqno) > 0)
                    {
                        // remove part/all seqno in the prior node
                        m_piData1[loc] = CSeqNo.incseq(seqno);
                        if (CSeqNo.seqcmp(m_piData2[i], m_piData1[loc]) > 0)
                            m_piData2[loc] = m_piData2[i];

                        m_piData2[i] = seqno;

                        m_piNext[loc] = m_piNext[i];
                        m_piNext[i] = loc;

                        m_iHead = loc;
                    }
                    else
                        m_iHead = m_piNext[i];
                }

                // Remove all nodes prior to the new head
                while (h != m_iHead)
                {
                    if (m_piData2[h] != -1)
                    {
                        m_iLength -= CSeqNo.seqlen(m_piData1[h], m_piData2[h]);
                        m_piData2[h] = -1;
                    }
                    else
                        m_iLength--;

                    m_piData1[h] = -1;

                    if (m_iLastInsertPos == h)
                        m_iLastInsertPos = -1;

                    h = m_piNext[h];
                }
            }

            return true;
        }

        // Functionality:
        //    Read the loss length.
        // Parameters:
        //    None.
        // Returned value:
        //    The length of the list.

        public override int getLossLength()
        {
            CGuard listguard = new CGuard(m_ListLock);

            return m_iLength;
        }

        // Functionality:
        //    Read the first (smallest) loss seq. no. in the list and remove it.
        // Parameters:
        //    None.
        // Returned value:
        //    The seq. no. or -1 if the list is empty.

        public Int32 getLostSeq()
        {
            if (0 == m_iLength)
                return -1;

            CGuard listguard = new CGuard(m_ListLock);

            if (0 == m_iLength)
                return -1;

            if (m_iLastInsertPos == m_iHead)
                m_iLastInsertPos = -1;

            // return the first loss seq. no.
            Int32 seqno = m_piData1[m_iHead];

            // head moves to the next node
            if (-1 == m_piData2[m_iHead])
            {
                //[3, -1] becomes [], and head moves to next node in the list
                m_piData1[m_iHead] = -1;
                m_iHead = m_piNext[m_iHead];
            }
            else
            {
                // shift to next node, e.g., [3, 7] becomes [], [4, 7]
                int loc = (m_iHead + 1) % m_iSize;

                m_piData1[loc] = CSeqNo.incseq(seqno);
                if (CSeqNo.seqcmp(m_piData2[m_iHead], m_piData1[loc]) > 0)
                    m_piData2[loc] = m_piData2[m_iHead];

                m_piData1[m_iHead] = -1;
                m_piData2[m_iHead] = -1;

                m_piNext[loc] = m_piNext[m_iHead];
                m_iHead = loc;
            }

            m_iLength--;

            return seqno;
        }
    }
}
