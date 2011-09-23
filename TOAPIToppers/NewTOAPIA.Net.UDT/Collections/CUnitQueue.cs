/*****************************************************************************
Copyright (c) 2001 - 2008, The Board of Trustees of the University of Illinois.
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
   Yunhong Gu, last updated 06/02/2008
*****************************************************************************/

namespace NewTOAPIA.Net.Udt
{
    using System;
    using System.Net.Sockets;

    public class CUnit
    {
        internal CPacket m_Packet;	    // packet
        internal int m_iFlag;			// 0: free, 1: occupied, 2: msg read but not freed (out-of-order), 3: msg dropped
    }

    public class CUnitQueue
    {
        private class CQEntry
        {
            public CUnit m_pUnit;		// unit queue
            public IntPtr m_pBuffer;		// data buffer
            public int m_iSize;		// size of each queue

            public CQEntry m_pNext;
        }

        CQEntry m_pQEntry = null;			// pointer to the first unit queue
        CQEntry m_pCurrQueue = null;		// pointer to the current available queue
        CQEntry m_pLastQueue = null;		// pointer to the last unit queue

        CUnit m_pAvailUnit;         // recent available unit

        int m_iSize = 0;			// total size of the unit queue, in number of packets
        internal int m_iCount = 0;		// total number of valid packets in the queue

        int m_iMSS;			// unit buffer size
        public AddressFamily m_iIPversion;		// IP version

        #region Constructor - Destructor
        //public CUnitQueue()
        //{
        //}

        //~CUnitQueue()
        //{
        //    CQEntry p = m_pQEntry;

        //    while (p != null)
        //    {
        //        CQEntry q = p;
        //        if (p == m_pLastQueue)
        //            p = null;
        //        else
        //            p = p.m_pNext;

        //        q.Dispose();
        //    }
        //}
        #endregion

        // Functionality:
        //    Initialize the unit queue.
        // Parameters:
        //    1) [in] size: queue size
        //    2) [in] mss: maximum segment size
        //    3) [in] version: IP version
        // Returned value:
        //    0: success, -1: failure.

        public int init(int size, int mss, AddressFamily version)
        {
            CQEntry tempq = null;
            CUnit tempu = null;
            IntPtr tempb = IntPtr.Zero;

            try
            {
                tempq = new CQEntry();
                tempu = new CUnit[size];
                tempb = new char[size * mss];
            }
            catch (Exception e)
            {
                //tempq.Dispose();
                // tempu.Dispose();
                // tempb.Dispose();

                return -1;
            }

            for (int i = 0; i < size; ++i)
            {
                tempu[i].m_iFlag = 0;
                tempu[i].m_Packet.m_pcData = tempb + i * mss;
            }
            tempq.m_pUnit = tempu;
            tempq.m_pBuffer = tempb;
            tempq.m_iSize = size;

            m_pQEntry = m_pCurrQueue = m_pLastQueue = tempq;
            m_pQEntry.m_pNext = m_pQEntry;

            m_pAvailUnit = m_pCurrQueue.m_pUnit;

            m_iSize = size;
            m_iMSS = mss;
            m_iIPversion = version;

            return 0;
        }

        // Functionality:
        //    Increase (double) the unit queue size.
        // Parameters:
        //    None.
        // Returned value:
        //    0: success, -1: failure.

        public int increase()
        {
            // adjust/correct m_iCount
            int real_count = 0;
            CQEntry p = m_pQEntry;
            while (p != null)
            {
                CUnit u = p.m_pUnit;
                for (CUnit end = u + p.m_iSize; u != end; ++u)
                    if (u.m_iFlag != 0)
                        ++real_count;

                if (p == m_pLastQueue)
                    p = null;
                else
                    p = p.m_pNext;
            }
            m_iCount = real_count;
            if ((double)(m_iCount) / m_iSize < 0.9)
                return -1;

            CQEntry tempq = null;
            CUnit tempu = null;
            IntPtr tempb = IntPtr.Zero;

            // all queues have the same size
            int size = m_pQEntry.m_iSize;

            try
            {
                tempq = new CQEntry();
                tempu = new CUnit[size];
                tempb = new char[size * m_iMSS];
            }
            catch (Exception e)
            {
                tempq.Dispose();
                tempu.Dispose();
                tempb.Dispose();
                //delete tempq;
                //delete [] tempu;
                //delete [] tempb;

                return -1;
            }

            for (int i = 0; i < size; ++i)
            {
                tempu[i].m_iFlag = 0;
                tempu[i].m_Packet.m_pcData = tempb + i * m_iMSS;
            }
            tempq.m_pUnit = tempu;
            tempq.m_pBuffer = tempb;
            tempq.m_iSize = size;

            m_pLastQueue.m_pNext = tempq;
            m_pLastQueue = tempq;
            m_pLastQueue.m_pNext = m_pQEntry;

            m_iSize += size;

            return 0;
        }

        // Functionality:
        //    Decrease (halve) the unit queue size.
        // Parameters:
        //    None.
        // Returned value:
        //    0: success, -1: failure.

        public int shrink()
        {
            // currently queue cannot be shrunk.
            return -1;
        }

        // Functionality:
        //    find an available unit for incoming packet.
        // Parameters:
        //    None.
        // Returned value:
        //    Pointer to the available unit, null if not found.

        public CUnit getNextAvailUnit()
        {
            if (m_iCount * 10 > m_iSize * 9)
                increase();

            if (m_iCount >= m_iSize)
                return null;

            CQEntry entrance = m_pCurrQueue;

            do
            {
                for (CUnit sentinel = m_pCurrQueue.m_pUnit + m_pCurrQueue.m_iSize - 1; m_pAvailUnit != sentinel; ++m_pAvailUnit)
                    if (m_pAvailUnit.m_iFlag == 0)
                        return m_pAvailUnit;

                if (m_pCurrQueue.m_pUnit.m_iFlag == 0)
                {
                    m_pAvailUnit = m_pCurrQueue.m_pUnit;
                    return m_pAvailUnit;
                }

                m_pCurrQueue = m_pCurrQueue.m_pNext;
                m_pAvailUnit = m_pCurrQueue.m_pUnit;
            } while (m_pCurrQueue != entrance);

            increase();

            return null;
        }

    }
}


