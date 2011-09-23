namespace NewTOAPIA
{
    using System;
    using System.Threading;
    using System.Diagnostics;

    public class CClock
    {
        public static Int64 gMhzFactor = 1000000;
        public static readonly double CPUFrequency;	// CPU frequency : clock cycles per microsecond
        private static AutoResetEvent m_EventCond = new AutoResetEvent(false);
        
        private Int64 m_ullSchedTime;             // next scheduled time

        private AutoResetEvent m_TickCond;

        #region Constructor
        static CClock()
        {
            CPUFrequency = CClock.readCPUFrequency();
        }

        public CClock()
        {
            m_TickCond = new AutoResetEvent(false);      
        }

        ~CClock()
        {
            //m_TickLock.Close();
            m_TickCond.Close();
        }
        #endregion

        // Functionality:
        //    Sleep for "interval" CCs.
        // Parameters:
        //    0) [in] interval: CCs to sleep.
        // Returned value:
        //    None.

        /// <summary>
        /// Sleep for a number of microseconds
        /// </summary>
        /// <param name="interval">Number of microseconds to sleep</param>
        public void Sleep(Int64 interval)
        {
            Int64 t;
            rdtsc(out t);

            // sleep next "interval" time
            SleepTo(t + interval);
        }

        // Functionality:
        //    Seelp until CC "nexttime".
        // Parameters:
        //    0) [in] nexttime: next time the caller is waken up.
        // Returned value:
        //    None.

        public void SleepTo(Int64 nexttime)
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

            Tick();
        }

        /// <summary>
        /// trigger the clock for a tick, for better granuality in no_busy_waiting timer.
        /// </summary>
        public void Tick()
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
        // Functionality:
        //    Read the CPU clock cycle into x.
        // Parameters:
        //    0) [out] x: to record cpu clock cycles.
        // Returned value:
        //    None.

        public static void rdtsc(out Int64 x)
        {
            x = System.Diagnostics.Stopwatch.GetTimestamp();
        }


        // Functionality:
        //    check the current time, 64bit, in microseconds.
        // Parameters:
        //    None.
        // Returned value:
        //    current time in microseconds.

        public static long GetTimeInMicroseconds()
        {
            long cc;

            cc = System.Diagnostics.Stopwatch.GetTimestamp();
            double seconds = (double)cc / CPUFrequency;
            return (long)(seconds * gMhzFactor);
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