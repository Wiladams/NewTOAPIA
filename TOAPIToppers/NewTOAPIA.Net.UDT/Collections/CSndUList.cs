namespace NewTOAPIA.Net.Udt
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading;

    using NewTOAPIA;

    internal class CSNode
    {
        internal CUDT m_pUDT;		    // Pointer to the instance of CUDT socket
        internal UInt32 m_llTimeStamp;  // Time Stamp
        internal int m_iHeapLoc;		// location on the heap, -1 means not on the heap
    }

    internal class CSndUList
    {
        List<CSNode> m_pHeap = new List<CSNode>();
        private int m_iArrayLength;			// physical length of the array
        private int m_iLastEntry;			// position of last entry on the heap array

        private Mutex m_ListLock;
        internal Mutex m_pWindowLock;
        internal AutoResetEvent m_pWindowCond;

        internal CClock m_pTimer;

        public CSndUList()
        {
            m_iArrayLength = (4096);
            m_iLastEntry = (-1);

            //m_pHeap = new CSNode[m_iArrayLength];

            m_ListLock = new Mutex();
        }

        ~CSndUList()
        {
            //m_pHeap.Dispose();
            m_ListLock.Close();
        }


        // Functionality:
        //    Insert a new UDT instance into the list.
        // Parameters:
        //    1) [in] ts: time stamp: next processing time
        //    2) [in] u: pointer to the UDT instance
        // Returned value:
        //    None.

        public void Insert(Int64 ts, CUDT u)
        {
            CGuard listguard = new CGuard(m_ListLock);

            //// increase the heap array size if necessary
            //if (m_iLastEntry == m_iArrayLength - 1)
            //{
            //    CSNode[] temp = null;

            //    try
            //    {
            //        temp = new CSNode[m_iArrayLength * 2];
            //    }
            //    catch (Exception e)
            //    {
            //        return;
            //    }

            //    m_pHeap.CopyTo(temp);

            //    m_iArrayLength *= 2;
            //    m_pHeap = temp;
            //}

            insert_(ts, u);
        }

        private void insert_(Int64 ts, CUDT u)
        {
            CSNode n = u.m_pSNode;

            // do not insert repeated node
            if (n.m_iHeapLoc >= 0)
                return;

            m_iLastEntry++;
            m_pHeap.Add(n);
            //m_pHeap[m_iLastEntry] = n;
            n.m_llTimeStamp = ts;

            // Sort the new node based on timestamp
            // we can probably replace this with a sorted list
            // then we won't need to perform the sort ourselves 
            int q = m_iLastEntry;
            int p = q;
            while (p != 0)
            {
                p = (q - 1) >> 1;
                if (m_pHeap[p].m_llTimeStamp > m_pHeap[q].m_llTimeStamp)
                {
                    CSNode t = m_pHeap[p];
                    m_pHeap[p] = m_pHeap[q];
                    m_pHeap[q] = t;
                    t.m_iHeapLoc = q;
                    q = p;
                }
                else
                    break;
            }

            n.m_iHeapLoc = q;

            // first entry, activate the sending queue
            if (0 == m_iLastEntry)
            {
                m_pWindowCond.Set();
            }
        }

        // Functionality:
        //    Update the timestamp of the UDT instance on the list.
        // Parameters:
        //    1) [in] u: pointer to the UDT instance
        //    2) [in] resechedule: if the timestampe shoudl be rescheduled
        // Returned value:
        //    None.

        public void update(CUDT u, bool reschedule)
        {
            CGuard listguard = new CGuard(m_ListLock);

            CSNode n = u.m_pSNode;

            if (n.m_iHeapLoc >= 0)
            {
                if (!reschedule)
                    return;

                if (n.m_iHeapLoc == 0)
                {
                    n.m_llTimeStamp = 1;
                    m_pTimer.interrupt();
                    return;
                }

                remove_(u);
            }

            insert_(1, u);
        }

        // Functionality:
        //    Retrieve the next packet and peer address from the first entry, and reschedule it in the queue.
        // Parameters:
        //    0) [out] addr: destination address of the next packet
        //    1) [out] pkt: the next packet to be sent
        // Returned value:
        //    1 if successfully retrieved, -1 if no packet found.

        public int pop(IPAddress addr, CPacket pkt)
        {
            CGuard listguard = new CGuard(m_ListLock);

            if (-1 == m_iLastEntry)
                return -1;

            CUDT u = m_pHeap[0].m_pUDT;
            remove_(u);

            if (!u.m_bConnected || u.m_bBroken)
                return -1;

            // pack a packet from the socket
            Int64 ts;
            if (u.packData(pkt, ts) <= 0)
                return -1;

            addr = u.m_pPeerAddr;

            // insert a new entry, ts is the next processing time
            if (ts > 0)
                insert_(ts, u);

            return 1;
        }

        // Functionality:
        //    Remove UDT instance from the list.
        // Parameters:
        //    1) [in] u: pointer to the UDT instance
        // Returned value:
        //    None.

        void remove(CUDT u)
        {
            CGuard listguard = new CGuard(m_ListLock);

            CSNode n = u.m_pSNode;

            if (n.m_iHeapLoc >= 0)
            {
                // remove the node from heap
                m_pHeap[n.m_iHeapLoc] = m_pHeap[m_iLastEntry];
                m_iLastEntry--;
                m_pHeap[n.m_iHeapLoc].m_iHeapLoc = n.m_iHeapLoc;

                int q = n.m_iHeapLoc;
                int p = q * 2 + 1;
                while (p <= m_iLastEntry)
                {
                    if ((p + 1 <= m_iLastEntry) && (m_pHeap[p].m_llTimeStamp > m_pHeap[p + 1].m_llTimeStamp))
                        p++;

                    if (m_pHeap[q].m_llTimeStamp > m_pHeap[p].m_llTimeStamp)
                    {
                        CSNode t = m_pHeap[p];
                        m_pHeap[p] = m_pHeap[q];
                        m_pHeap[p].m_iHeapLoc = p;
                        m_pHeap[q] = t;
                        m_pHeap[q].m_iHeapLoc = q;

                        q = p;
                        p = q * 2 + 1;
                    }
                    else
                        break;
                }

                n.m_iHeapLoc = -1;
            }
        }

        // Functionality:
        //    Retrieve the next scheduled processing time.
        // Parameters:
        //    None.
        // Returned value:
        //    Scheduled processing time of the first UDT socket in the list.
        internal Int64 getNextProcTime()
        {
            CGuard listguard = new CGuard(m_ListLock);

            if (-1 == m_iLastEntry)
                return 0;

            return m_pHeap[0].m_llTimeStamp;
        }
    }
}