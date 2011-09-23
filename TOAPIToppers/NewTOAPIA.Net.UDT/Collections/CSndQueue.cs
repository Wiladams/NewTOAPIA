namespace NewTOAPIA.Net.Udt
{
    using System;
    using System.Net;
    using System.Threading;

    using DWORD = System.UInt32;

    public class CSndQueue
    {
        #region Fields
        private Thread m_WorkerThread;

        private CSndUList m_pSndUList;		// List of UDT instances for data sending
        private CChannel m_pChannel;        // The UDP channel for data sending
        private CClock m_pTimer;			// Timing facility

        private Mutex m_WindowLock;
        private AutoResetEvent m_WindowCond;

        private bool m_bClosing;		// closing the worker
        private AutoResetEvent m_ExitCond;
        #endregion

        //public CSndQueue()
        //{
            
        //    //m_WorkerThread(),
        //    //m_pSndUList(null),
        //    //m_pChannel(null),
        //    //m_pTimer(null),
        //    //m_WindowLock(),
        //    //m_WindowCond(),
        //    //m_bClosing(false),
        //    //m_ExitCond()

        //    m_WindowLock = new Mutex();
        //    m_WindowCond = new AutoResetEvent(false);
        //    m_ExitCond = new AutoResetEvent(false);
        //}

        public CSndQueue(CChannel c, CClock t)
        {
            m_WindowLock = new Mutex();
            m_WindowCond = new AutoResetEvent(false);
            m_ExitCond = new AutoResetEvent(false);

            init(c, t);
        }

        ~CSndQueue()
        {
            m_bClosing = true;

            m_WindowCond.Set();

            if (null != m_WorkerThread)
                m_ExitCond.WaitOne(-1);

            //CloseHandle(m_WorkerThread);
            m_WindowLock.Close();
            m_WindowCond.Close();
            m_ExitCond.Close();

            //delete m_pSndUList;
        }

        // Functionality:
        //    Initialize the sending queue.
        // Parameters:
        //    1) [in] c: UDP channel to be associated to the queue
        //    2) [in] t: Timer
        // Returned value:
        //    None.

        void init(CChannel c, CClock t)
        {
            m_pChannel = c;
            m_pTimer = t;
            m_pSndUList = new CSndUList();
            m_pSndUList.m_pWindowLock = m_WindowLock;
            m_pSndUList.m_pWindowCond = m_WindowCond;
            m_pSndUList.m_pTimer = m_pTimer;

            DWORD threadID;
            m_WorkerThread = CreateThread(null, 0, CSndQueue.worker, this, 0, out threadID);
            if (null == m_WorkerThread)
                throw new CUDTException(3, 1, -1);
        }

        // Functionality:
        //    Send out a packet to a given address.
        // Parameters:
        //    1) [in] addr: destination address
        //    2) [in] packet: packet to be sent out
        // Returned value:
        //    Size of data sent out.

        public int sendto(IPEndPoint addr, CPacket packet)
        {
            // send out the packet immediately (high priority), this is a control packet
            m_pChannel.sendto(addr, packet);

            return packet.getLength();
        }

        public static DWORD worker(Object param)
        {
            //CSndQueue self = new CSndQueue(param);
            CSndQueue self = (CSndQueue)param;

            while (!self.m_bClosing)
            {
                Int64 ts = self.m_pSndUList.getNextProcTime();

                if (ts > 0)
                {
                    // wait until next processing time of the first socket on the list
                    Int64 currtime;
                    CClock.rdtsc(out currtime);
                    if (currtime < ts)
                        self.m_pTimer.SleepTo(ts);

                    // it is time to process it, pop it out/remove from the list
                    IPAddress addr;
                    CPacket pkt;
                    if (self.m_pSndUList.pop(addr, pkt) < 0)
                        continue;

                    self.m_pChannel.sendto(addr, pkt);
                }
                else
                {
                    // wait here if there is no sockets with data to be sent
                    self.m_WindowCond.WaitOne(-1);
                }
            }

            self.m_ExitCond.Set();
            return 0;
        }
    }
}
