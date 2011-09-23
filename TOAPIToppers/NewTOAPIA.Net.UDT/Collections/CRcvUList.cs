namespace NewTOAPIA.Net.Udt
{
    using System;

    public class CRNode
    {
        CUDT m_pUDT;                // Pointer to the instance of CUDT socket
        internal UInt32 m_llTimeStamp;      // Time Stamp

        CRNode m_pPrev;             // previous link
        CRNode m_pNext;             // next link

        bool m_bOnList;              // if the node is already on the list
    }

    public class CRcvUList
    {
        public CRNode m_pUList = null;		// the head node
        private CRNode m_pLast = null;		// the last node


        // Functionality:
        //    Insert a new UDT instance to the list.
        // Parameters:
        //    1) [in] u: pointer to the UDT instance
        // Returned value:
        //    None.

        public void insert(CUDT u)
        {
            CRNode n = u.m_pRNode;
            CClock.rdtsc(n.m_llTimeStamp);

            n.m_bOnList = true;

            if (null == m_pUList)
            {
                // empty list, insert as the single node
                n.m_pPrev = n.m_pNext = null;
                m_pLast = m_pUList = n;

                return;
            }

            // always insert at the end for RcvUList
            n.m_pPrev = m_pLast;
            n.m_pNext = null;
            m_pLast.m_pNext = n;
            m_pLast = n;
        }

        // Functionality:
        //    Remove the UDT instance from the list.
        // Parameters:
        //    1) [in] u: pointer to the UDT instance
        // Returned value:
        //    None.

        public void remove(CUDT u)
        {
            CRNode n = u.m_pRNode;

            if (!n.m_bOnList)
                return;

            if (null == n.m_pPrev)
            {
                // n is the first node
                m_pUList = n.m_pNext;
                if (null == m_pUList)
                    m_pLast = null;
                else
                    m_pUList.m_pPrev = null;
            }
            else
            {
                n.m_pPrev.m_pNext = n.m_pNext;
                if (null == n.m_pNext)
                {
                    // n is the last node
                    m_pLast = n.m_pPrev;
                }
                else
                    n.m_pNext.m_pPrev = n.m_pPrev;
            }

            n.m_pNext = n.m_pPrev = null;

            n.m_bOnList = false;
        }

        // Functionality:
        //    Move the UDT instance to the end of the list, if it already exists; otherwise, do nothing.
        // Parameters:
        //    1) [in] u: pointer to the UDT instance
        // Returned value:
        //    None.

        public void update(CUDT u)
        {
            CRNode n = u.m_pRNode;

            if (!n.m_bOnList)
                return;

            CClock.rdtsc(n.m_llTimeStamp);

            // if n is the last node, do not need to change
            if (null == n.m_pNext)
                return;

            if (null == n.m_pPrev)
            {
                m_pUList = n.m_pNext;
                m_pUList.m_pPrev = null;
            }
            else
            {
                n.m_pPrev.m_pNext = n.m_pNext;
                n.m_pNext.m_pPrev = n.m_pPrev;
            }

            n.m_pPrev = m_pLast;
            n.m_pNext = null;
            m_pLast.m_pNext = n;
            m_pLast = n;
        }
    }
}