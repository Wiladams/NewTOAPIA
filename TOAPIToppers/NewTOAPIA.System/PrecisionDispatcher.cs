using System;
using System.Threading;

namespace NewTOAPIA.Kernel
{
    public class TimedDispatcher
    {
        public delegate void TimedDispatch(TimedDispatcher dispatcher, double time, Object[] dispatchParams);

        PrecisionTimer fStopWatch;
        Thread fDispatchThread;
        bool fIsRunning;
        double fInterval;
        double fLatency;
        double fDispatchLatency;
        int fLatencyReports;
        int fFrameCounter;

        TimedDispatch fDispatch;
        Object[] fParams;

        public TimedDispatcher(double intervalSeconds, TimedDispatch dispatch, Object[] dispatchParams)
        {
            fDispatch = dispatch;
            fParams = dispatchParams;

            fInterval = intervalSeconds;
            fLatency = 0.103;   // Initial latency, 3 milliseconds
            fLatencyReports = 1;

            fStopWatch = new PrecisionTimer();
        }

        #region Properties
        public double DispatchLatency
        {
            get { return fDispatchLatency; }
        }

        public int FrameCount
        {
            get { return fFrameCounter; }
        }

        public double FrameRate
        {
            get { return fFrameCounter / fStopWatch.GetElapsedSeconds(); }
        }

        public bool IsRunning
        {
            get { return fIsRunning; }
        }

        public double Latency
        {
            get { return fLatency; }
        }
        #endregion

        #region Public Methods
        public void ApplyLatency(double dispatchedTime)
        {
            double diffTime = fStopWatch.GetElapsedSeconds() - dispatchedTime;
            double newLatency = ((fLatency * fLatencyReports) + diffTime) / (fLatencyReports + 1);

            if (newLatency < 0)
                newLatency = 0;

            fLatencyReports++;
            fLatency = newLatency;
        }

        public void Start()
        {
            fIsRunning = true;
            fFrameCounter = 0;
            
            fDispatchThread = new Thread(new ThreadStart(DispatchLoop));
            fDispatchThread.IsBackground = true;
            fDispatchThread.Start();
        }

        public void Stop()
        {
            fIsRunning = false;
            while (fDispatchThread.IsAlive)
                Thread.Sleep(100);
        }
        #endregion

        #region Timing Thread
        void DispatchLoop()
        {
            double nextTime = fStopWatch.GetElapsedSeconds();;
            double dispatchStart;

            while (fIsRunning)
            {
                // Figure out when the next frame should be displayed
                nextTime += fInterval;


                double currentTime = fStopWatch.GetElapsedSeconds();

                // We know when the next frame is to be displayed.
                // Calculate the amount to sleep by looking at the current time, and 
                // the known latency.
                double diffTime = nextTime - currentTime - fLatency;
                if (diffTime < 0)
                    diffTime = 0;

                // Turn it into milliseconds
                int millis = (int)((diffTime * 1000)+0.5);

                // if the milliseconds to sleep is greater than 0,
                // then thread sleep for that amount of time.
                if (millis > 0)
                    Thread.Sleep(millis);

                
                // After we wake up, we perform the dispatch
                dispatchStart = fStopWatch.GetElapsedSeconds();
                if (null != fDispatch)
                {
                    fDispatch(this, dispatchStart, fParams);
                }
                fDispatchLatency = fStopWatch.GetElapsedSeconds() - dispatchStart;

                // Increment the frame count just so we know
                // what frame we're on.
                fFrameCounter++;
            }
        }
        #endregion
    }
}
