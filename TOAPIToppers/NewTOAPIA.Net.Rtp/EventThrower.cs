using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;


namespace NewTOAPIA.Net.Rtp
{
    using NewTOAPIA.BCL;

    /// <summary>
    /// This class is used to fire events on a separate thread using a queue of events 
    /// to fire with a First In, First Out algorithm.  Rather than use the ThreadPool 
    /// which would not guarantee FIFO since there are 25+ threads to service the events 
    /// in the queue, we have a custom object that uses a single thread to service the 
    /// queue and guarantees FIFO ordering.
    /// 
    /// The reason this class exists is that two initial approaches failed.
    /// 
    /// Approach 1:  Fire events on the thread that discovered them.  For instance, the 
    /// RtcpListener thread would detect a new Rtp Participant and would fire RtpParticipantAdded.  
    /// The client, upon catching the event, would draw a new Participant in the UI,
    /// but to do so it would have to make a web service call to the Venue Service to 
    /// get the Participant's icon.  This would take up to a second and the RtcpListener 
    /// thread would block while this synchronous call was occuring.  This caused a number 
    /// of incoming Rtcp packets to be dropped while the thread was blocked and in high 
    /// stress conditions would even cause Rtp Participant timeouts to occur due to a lack 
    /// of received Rtcp packets for the participant.
    /// 
    /// Approach 2:  Have the main thread forward off events to the ThreadPool for firing.  
    /// The problem here was that events would get queued and then serviced by the 25 threads 
    /// sitting in the thread pool.  Due to the intricacies of when threads get serviced
    /// by the CPU, we couldn't guarantee the order in which the events would get serviced.  
    /// This caused freaky and very rare race conditions where the client might receive an 
    /// RtpStreamAdded event before the RtpParticipantAdded event for the RtpParticipant 
    /// that the RtpStream belonged to was received, especially prevalent under stress 
    /// conditions of large numbers of streams/participants or high CPU utilization.
    /// </summary>
    internal class EventThrower
    {
        #region Private Static Properties
        private static Thread eventThread;
        //private static Queue<WorkItem> syncWorkItems; // of WorkItem structs
        private static LockFreeQueue<WorkItem> syncWorkItems;

        private static AutoResetEvent newWorkItem;
        private static int peakQueueLength;
        #endregion

        #region Public Static Properties
        static public int WorkItemQueueLength
        { 
            get { return EventThrower.syncWorkItems.Count; } 
        }

        static public int PeakEventQueueLength
        { 
            get { return EventThrower.peakQueueLength; } 
        }
        #endregion

        #region Constructors
        static EventThrower()
        {
            peakQueueLength = 0;

            eventThread = new Thread(new ThreadStart(EventThread));
            syncWorkItems = new LockFreeQueue< WorkItem>(); 
            newWorkItem = new AutoResetEvent(false);

            eventThread.IsBackground = true;    // So it will not linger once a main thread is gone
            eventThread.Name = "EventThrower";
            eventThread.Start();
        }
        #endregion
        
        #region Thread Method
        /// <summary>
        /// Thread that services the workItems queue
        /// </summary>
        private static void EventThread()
        {
            while (true)
            {
                try
                {
                    if(newWorkItem.WaitOne())
                    {
                        while(syncWorkItems.Count > 0)
                        {
                            //WorkItem wi = syncWorkItems.Dequeue();
                            WorkItem wi = default(WorkItem);
                            if (syncWorkItems.TryDequeue(out wi))
                                wi.method(wi.parameters);
                        }
                    }
                }
                catch (ThreadAbortException) 
                {
                    // If we get a thread abort, we'll simply
                    // ignore it, and the thread will be aborted
                }
                // WAA - I don't want to catch other exceptions as they get hidden
                // let them bubble up to a higher layer for handling
                //catch (Exception e)
                //{
                //    //eventLog.WriteEntry(e.ToString(), EventLogEntryType.Error, 99);
                //    throw e;
                //}
            }
        }

        #endregion
        
        #region Internal Methods

        /// <summary>
        /// Put a work item on a queue for in order execution by a background thread.
        /// </summary>
        /// <param name="del">The delegate that will be called.</param>
        /// <param name="parameters">The parameters that will be passed to the delegate</param>
        internal static void QueueUserWorkItem(RtpEvents.RaiseEvent del, object[] parameters)
        {
            syncWorkItems.Enqueue(new WorkItem(del, parameters));

            if( peakQueueLength < syncWorkItems.Count )
                peakQueueLength = syncWorkItems.Count;
            
            newWorkItem.Set();
        }
        #endregion

        #region Private Structs
        /// <summary>
        /// A WorkItem consisting of the delegate to be called and an array of objects to pass in as parameters
        /// </summary>
        private struct WorkItem
        {
            public RtpEvents.RaiseEvent method;
            public object[] parameters;

            public WorkItem(RtpEvents.RaiseEvent method, object[] parameters)
            {
                this.method = method;
                this.parameters = parameters;
            }
        }
        #endregion
    }
}
