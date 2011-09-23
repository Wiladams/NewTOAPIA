

#pragma warning disable 0420

namespace NewTOAPIA.BCL
{
    using System;
    using System.Threading;

    public class LockFreeStack<T>
    {
        class Node
        {
            internal T fValue;
            internal volatile Node fNext;
        }

        volatile Node fHead;

        public void Push(T value)
        {
            Node n = new Node();
            n.fValue = value;

            Node h;
            do
            {
                h = fHead;
                n.fNext = h;
            }
            while (Interlocked.CompareExchange(ref fHead, n, h) != h);
        }

        public T Pop()
        {
            Node n;
            do
            {
                n = fHead;
                if (n == null) 
                    throw new Exception("stack empty");
            }
            while (Interlocked.CompareExchange(ref fHead, n.fNext, n) != n);

            return n.fValue;
        }
    }
}
