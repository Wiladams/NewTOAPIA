

namespace NewTOAPIA.Net.Udt
{
    using System;
    using System.Linq;

    class CPktTimeWindow
    {
        private int m_iAWSize;               // size of the packet arrival history window
        private int[] m_piPktWindow;          // packet information window
        private int m_iPktWindowPtr;         // position pointer of the packet info. window.

        private int m_iPWSize;               // size of probe history window size
        private int[] m_piProbeWindow;        // record inter-packet time for probing packet pairs
        private int m_iProbeWindowPtr;       // position pointer to the probing window

        private int m_iLastSentTime;         // last packet sending time
        private int m_iMinPktSndInt;         // Minimum packet sending interval

        private Int64 m_LastArrTime;      // last packet arrival time
        private Int64 m_CurrArrTime;      // current packet arrival time
        private Int64 m_ProbeTime;        // arrival time of the first probing packet

        CPktTimeWindow()
            : this(16, 16)
        {
        }

        public CPktTimeWindow(int asize, int psize)
        {
            //m_piPktWindow(null),
            //m_iPktWindowPtr(0),
            //m_piProbeWindow(null),
            //m_iProbeWindowPtr(0),
            //m_iLastSentTime(0),
            //m_LastArrTime(),
            //m_CurrArrTime(),
            //m_ProbeTime()

            m_iAWSize = (asize);
            m_iPWSize = (psize);
            m_iMinPktSndInt = (1000000);

            m_piPktWindow = new int[m_iAWSize];
            m_piProbeWindow = new int[m_iPWSize];

            m_LastArrTime = CClock.getTime();

            for (int i = 0; i < m_iAWSize; ++i)
                m_piPktWindow[i] = 1000000;

            for (int k = 0; k < m_iPWSize; ++k)
                m_piProbeWindow[k] = 1000;
        }


        // Functionality:
        //    read the minimum packet sending interval.
        // Parameters:
        //    None.
        // Returned value:
        //    minimum packet sending interval (microseconds).

        public int getMinPktSndInt()
        {
            return m_iMinPktSndInt;
        }

        // Functionality:
        //    Calculate the packes arrival speed.
        // Parameters:
        //    None.
        // Returned value:
        //    Packet arrival speed (packets per second).

        public int getPktRcvSpeed()
        {
            // sorting
            int[] pi = m_piPktWindow;

            for (int i = 0, n = (m_iAWSize >> 1) + 1; i < n; ++i)
            {
                int* pj = pi;
                for (int j = i, m = m_iAWSize; j < m; ++j)
                {
                    if (*pi > *pj)
                    {
                        int temp = *pi;
                        *pi = *pj;
                        *pj = temp;
                    }
                    ++pj;
                }
                ++pi;
            }

            // read the median value
            int median = (m_piPktWindow[(m_iAWSize >> 1) - 1] + m_piPktWindow[m_iAWSize >> 1]) >> 1;
            int count = 0;
            int sum = 0;
            int upper = median << 3;
            int lower = median >> 3;

            // median filtering
            int[] pk = m_piPktWindow;
            for (int k = 0, l = m_iAWSize; k < l; ++k)
            {
                if ((*pk < upper) && (*pk > lower))
                {
                    ++count;
                    sum += *pk;
                }
                ++pk;
            }

            // claculate speed, or return 0 if not enough valid values
            if (count > (m_iAWSize >> 1))
                return (int)Math.Ceiling(1000000.0 / (sum / count));
            else
                return 0;
        }

        // Functionality:
        //    Estimate the bandwidth.
        // Parameters:
        //    None.
        // Returned value:
        //    Estimated bandwidth (packets per second).

        public int getBandwidth()
        {
            // sorting
            int[] pi = m_piProbeWindow;
            for (int i = 0, n = (m_iPWSize >> 1) + 1; i < n; ++i)
            {
                int* pj = pi;
                for (int j = i, m = m_iPWSize; j < m; ++j)
                {
                    if (*pi > *pj)
                    {
                        int temp = *pi;
                        *pi = *pj;
                        *pj = temp;
                    }
                    ++pj;
                }
                ++pi;
            }

            // read the median value
            int median = (m_piProbeWindow[(m_iPWSize >> 1) - 1] + m_piProbeWindow[m_iPWSize >> 1]) >> 1;
            int count = 1;
            int sum = median;
            int upper = median << 3;
            int lower = median >> 3;

            // median filtering
            int[] pk = m_piProbeWindow;

            for (int k = 0, l = m_iPWSize; k < l; ++k)
            {
                if ((*pk < upper) && (*pk > lower))
                {
                    ++count;
                    sum += *pk;
                }
                ++pk;
            }

            return (int)Math.Ceiling(1000000.0 / ((double)(sum) / (double)(count)));
        }

        // Functionality:
        //    Record time information of a packet sending.
        // Parameters:
        //    0) currtime: timestamp of the packet sending.
        // Returned value:
        //    None.

        public void onPktSent(int currtime)
        {
            int interval = currtime - m_iLastSentTime;

            if ((interval < m_iMinPktSndInt) && (interval > 0))
                m_iMinPktSndInt = interval;

            m_iLastSentTime = currtime;
        }

        // Functionality:
        //    Record time information of an arrived packet.
        // Parameters:
        //    None.
        // Returned value:
        //    None.

        public void onPktArrival()
        {
            m_CurrArrTime = CClock.getTime();

            // record the packet interval between the current and the last one
            m_piPktWindow[m_iPktWindowPtr] = (int)(m_CurrArrTime - m_LastArrTime);

            // the window is logically circular
            ++m_iPktWindowPtr;
            if (m_iPktWindowPtr == m_iAWSize)
                m_iPktWindowPtr = 0;

            // remember last packet arrival time
            m_LastArrTime = m_CurrArrTime;
        }

        // Functionality:
        //    Record the arrival time of the first probing packet.
        // Parameters:
        //    None.
        // Returned value:
        //    None.

        public void probe1Arrival()
        {
            m_ProbeTime = CClock.getTime();
        }

        // Functionality:
        //    Record the arrival time of the second probing packet and the interval between packet pairs.
        // Parameters:
        //    None.
        // Returned value:
        //    None.

        public void probe2Arrival()
        {
            m_CurrArrTime = CClock.getTime();

            // record the probing packets interval
            m_piProbeWindow[m_iProbeWindowPtr] = (int)(m_CurrArrTime - m_ProbeTime);
            // the window is logically circular
            ++m_iProbeWindowPtr;
            if (m_iProbeWindowPtr == m_iPWSize)
                m_iProbeWindowPtr = 0;
        }
    }
}