
namespace NewTOAPIA.BCL
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public class BlockingBoundedQueue<T>
    {
        private Queue<T> fQueue = new Queue<T>();
        private int fCapacity;

        // Indicating queue is full
        private object m_fullEvent = new object();
        private int m_fullWaiters = 0;

        // Indicating consumers are waiting
        private object m_emptyEvent = new object();
        private int m_emptyWaiters = 0;

        public BlockingBoundedQueue(int capacity)
        {
            fCapacity = capacity;
        }

        public int Count
        {
            get
            {
                lock (fQueue)
                    return fQueue.Count;
            }
        }

        public void Clear()
        {
            lock (fQueue)
                fQueue.Clear();
        }

        public bool Contains(T item)
        {
            lock (fQueue)
                return fQueue.Contains(item);
        }

        public void Enqueue(T item)
        {
            lock (fQueue)
            {
                // If full, wait until an item is consumed.
                while (fQueue.Count == fCapacity)
                {
                    m_fullWaiters++;
                    try
                    {
                        lock (m_fullEvent)
                        {
                            Monitor.Exit(fQueue);
                            Monitor.Wait(m_fullEvent); // @BUGBUG: deadlock prone.
                            Monitor.Enter(fQueue);
                        }
                    }
                    finally
                    {
                        m_fullWaiters--;
                    }
                }

                fQueue.Enqueue(item);
            }

            // Wake consumers who are waiting for a new item.
            if (m_emptyWaiters > 0)
                lock (m_emptyEvent)
                    Monitor.Pulse(m_emptyEvent);
        }

        public T Dequeue()
        {
            T item;

            lock (fQueue)
            {
                while (fQueue.Count == 0)
                {
                    // Queue is empty, wait for a new item to arrive.
                    m_emptyWaiters++;
                    try
                    {
                        lock (m_emptyEvent)
                        {
                            Monitor.Exit(fQueue);
                            Monitor.Wait(m_emptyEvent); // @BUGBUG: deadlock prone.
                            Monitor.Enter(fQueue);
                        }
                    }
                    finally
                    {
                        m_emptyWaiters--;
                    }
                }

                item = fQueue.Dequeue();
            }

            // Wake producers who are waiting to produce.
            if (m_fullWaiters > 0)
                lock (m_fullEvent)
                    Monitor.Pulse(m_fullEvent);

            return item;
        }

        public T Peek()
        {
            lock (fQueue)
                return fQueue.Peek();
        }
    }
}

