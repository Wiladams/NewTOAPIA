namespace NewTOAPIA.BCL
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;

    public class LockFreeQueue<T> : IEnumerable<T>
    {
        class Node
        {
            internal T fValue;
            internal volatile Node fNext;
        }

        private volatile Node fHead;
        private volatile Node fTail;

        public LockFreeQueue()
        {
            fHead = fTail = new Node();
        }

        public int Count
        {
            get
            {
                int count = 0;
                for (Node curr = fHead.fNext;
                    curr != null; curr = curr.fNext) count++;
                return count;
            }
        }

        public bool IsEmpty
        {
            get { return fHead.fNext == null; }
        }

        private Node GetTailAndCatchUp()
        {
            Node tail = fTail;
            Node next = tail.fNext;

            // Update the tail until it really points to the end.
            while (next != null)
            {
                Interlocked.CompareExchange(ref fTail, next, tail);
                tail = fTail;
                next = tail.fNext;
            }

            return tail;
        }

        public void Enqueue(T obj)
        {
            // Create a new node.
            Node newNode = new Node();
            newNode.fValue = obj;

            // Add to the tail end.
            Node tail;
            do
            {
                tail = GetTailAndCatchUp();
                newNode.fNext = tail.fNext;
            }
            while (Interlocked.CompareExchange(
                ref tail.fNext, newNode, null) != null);

            // Try to swing the tail. If it fails, we'll do it later.
            Interlocked.CompareExchange(ref fTail, newNode, tail);
        }

        public bool TryDequeue(out T val)
        {
            while (true)
            {
                Node head = fHead;
                Node next = head.fNext;

                if (next == null)
                {
                    val = default(T);
                    return false;
                }
                else
                {
                    if (Interlocked.CompareExchange(
                        ref fHead, next, head) == head)
                    {
                        // Note: this read would be unsafe with a C++
                        // implementation. Another thread may have dequeued
                        // and freed 'next' by the time we get here, at
                        // which point we would try to dereference a bad
                        // pointer. Because we're in a GC-based system,
                        // we're OK doing this -- GC keeps it alive.
                        val = next.fValue;
                        return true;
                    }
                }
            }
        }

        public bool TryPeek(out T val)
        {
            Node curr = fHead.fNext;

            if (curr == null)
            {
                val = default(T);
                return false;
            }
            else
            {
                val = curr.fValue;
                return true;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node curr = fHead.fNext;
            Node tail = GetTailAndCatchUp();

            while (curr != null)
            {
                yield return curr.fValue;

                if (curr == tail)
                    break;

                curr = curr.fNext;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)this).GetEnumerator();
        }
    }
}