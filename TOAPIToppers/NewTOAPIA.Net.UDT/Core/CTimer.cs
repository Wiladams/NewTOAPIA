namespace NewTOAPIA.Net.Udt
{
    using System;
    using System.Threading;
    using System.Diagnostics;

    using TOAPI.Kernel32;

    public class CTimer
    {
        public static Int64 gMhzFactor = 1000000;

        private static double s_ullCPUFrequency;	// CPU frequency : clock cycles per microsecond
        private Int64 m_ullSchedTime;             // next scheduled time

        private AutoResetEvent m_TickCond;
        private Mutex m_TickLock;

        private static AutoResetEvent m_EventCond;
        private static Mutex m_EventLock;

        static CTimer()
        {
            s_ullCPUFrequency = CTimer.readCPUFrequency();
            m_EventLock = new Mutex();
            m_EventCond = new AutoResetEvent(false);
        }

        public CTimer()
        {
            m_TickLock = new Mutex();                   // CreateMutex(null, false, null);
            m_TickCond = new AutoResetEvent(false);     // m_TickCond = = new AutoResetEvent(false);      
        }

        ~CTimer()
        {
            m_TickLock.Close();
            m_TickCond.Close();
        }


        // Functionality:
        //    Sleep for "interval" CCs.
        // Parameters:
        //    0) [in] interval: CCs to sleep.
        // Returned value:
        //    None.

        public void sleep(Int64 interval)
        {
            Int64 t;
            rdtsc(out t);

            // sleep next "interval" time
            sleepto(t + interval);
        }

        // Functionality:
        //    Seelp until CC "nexttime".
        // Parameters:
        //    0) [in] nexttime: next time the caller is waken up.
        // Returned value:
        //    None.

        public void sleepto(Int64 nexttime)
        {
            // Use class member such that the method can be interrupted by others
            m_ullSchedTime = nexttime;

            Int64 t;
            rdtsc(out t);

            while (t < m_ullSchedTime)
            {
                m_TickCond.WaitOne(1);

                rdtsc(out t);
            }
        }

        /// <summary>
        /// Stop the sleep() or sleepto() methods.
        /// </summary>
        public void interrupt()
        {
            // schedule the sleepto time to the current CCs, so that it will stop
            rdtsc(out m_ullSchedTime);

            tick();
        }

        /// <summary>
        /// trigger the clock for a tick, for better granuality in no_busy_waiting timer.
        /// </summary>
        public void tick()
        {
            m_TickCond.Set();
        }




        // Functionality:
        //    trigger an event such as new connection, close, new data, etc. for "select" call.
        // Parameters:
        //    None.
        // Returned value:
        //    None.

        public void triggerEvent()
        {
            m_EventCond.Set();
        }

        // Functionality:
        //    wait for an event to br triggered by "triggerEvent".
        // Parameters:
        //    None.
        // Returned value:
        //    None.

        public void waitForEvent()
        {
            m_EventCond.WaitOne(1);
        }

        #region Static Helpers
        public static long ticksToMicroseconds(long ticks)
        {
            return ticks / gMhzFactor;
        }

        // Functionality:
        //    Read the CPU clock cycle into x.
        // Parameters:
        //    0) [out] x: to record cpu clock cycles.
        // Returned value:
        //    None.

        public static void rdtsc(out Int64 x)
        {
            Int64 ccounter;

            ccounter = System.Diagnostics.Stopwatch.GetTimestamp();
            x = ccounter;
        }


        // Functionality:
        //    check the current time, 64bit, in microseconds.
        // Parameters:
        //    None.
        // Returned value:
        //    current time in microseconds.

        public static Int64 getTime()
        {
            long ccf;
            //HANDLE hCurThread = ::GetCurrentThread(); 
            //DWORD_PTR dwOldMask = ::SetThreadAffinityMask(hCurThread, 1);
            if (Kernel32.QueryPerformanceFrequency(out ccf) > 0)
            {
                long cc;
                if (Kernel32.QueryPerformanceCounter(out cc) > 0)
                {
                    //SetThreadAffinityMask(hCurThread, dwOldMask); 
                    return (cc * gMhzFactor) / ccf;
                }
            }

            //SetThreadAffinityMask(hCurThread, dwOldMask); 
            //return GetTickCount() * 1000;
            return 1000;
        }

        // Functionality:
        //    return the CPU frequency.
        // Parameters:
        //    None.
        // Returned value:
        //    CPU frequency.

        public static double getCPUFrequency()
        {
            return s_ullCPUFrequency;
        }

        /// <summary>
        /// Returns the frequency of the CPU in Mhz
        /// </summary>
        /// <returns></returns>
        public static double readCPUFrequency()
        {
            if (System.Diagnostics.Stopwatch.IsHighResolution)
                return (double)System.Diagnostics.Stopwatch.Frequency / gMhzFactor;
            else
                return 1;
        }
        #endregion
    }
}