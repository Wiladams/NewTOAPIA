using System;
using TOAPI.Kernel32;

namespace NewTOAPIA.Kernel
{
    /// <summary>
    /// The PrecisionTimer wraps the Kernel32 calls to QueryPerformanceFrequency
    /// and QueryPerformanceCounter.  By doing so, this class provides easy access
    /// to this high precision timing mechanism if it is available in the system.
    /// 
    /// A class such as System.StopWatch can be built using this Precision timer
    /// class as well as other timing mechanisms when precision timing is not available.
    /// When you know your application is running on a system that supports precision
    /// timing, you can simply use this class.
    /// </summary>
    public class PrecisionTimer
    {
        private bool fIsHighResolution;
        private readonly long fCountsPerSecond;
        private long fStartCount;

        public PrecisionTimer()
        {
            // 0 == failure
            int success = Kernel32.QueryPerformanceFrequency(out fCountsPerSecond);
            if (success != 0)
                fIsHighResolution = true;

            Reset();
        }

        #region Properties
        public long Frequency
        {
            get { return fCountsPerSecond; }
        }

        public long Ticks
        {
            get
            {
                long ticks = 0;
                int success = Kernel32.QueryPerformanceCounter(out ticks);

                return ticks;
            }
        }

        #endregion

        /// <summary>
        /// Reset the startCount, which is the current tick count.
        /// This will reset the elapsed time because elapsed time is the 
        /// difference between the current tick count, and the one that
        /// was set here in the Reset() call.
        /// </summary>
        public void Reset()
        {
            if (fIsHighResolution)
            {
                int success = Kernel32.QueryPerformanceCounter(out fStartCount);
                if (success != 0)
                {
                    fIsHighResolution = true;
                }
                else
                {
                    fIsHighResolution = false;
                }
            }
        }

        /// <summary>
        /// Return the number of seconds that elapsed since Reset() was called.
        /// </summary>
        /// <returns>The number of elapsed seconds.</returns>
        public double GetElapsedSeconds()
        {
            if (fIsHighResolution)
            {
                long currentCount;
                int success = Kernel32.QueryPerformanceCounter(out currentCount);

                // if the return value is 0, the call failed
                if (0 == success)
                    return 0;

                if (fCountsPerSecond > 0)
                {
                    return ((currentCount - fStartCount) / ((double)fCountsPerSecond));
                }
                else
                {
                    return (0);
                }
            }

            return (0);
        }
    }
}
