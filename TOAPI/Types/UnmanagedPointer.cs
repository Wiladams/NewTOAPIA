using System;
using System.Runtime.InteropServices;

namespace TOAPI.Types
{
    public class UnmanagedPointer
    {
        private IntPtr m_ptr;

        public UnmanagedPointer(IntPtr ptr)
        {
            m_ptr = ptr;
        }

        #region Operators
        public static implicit operator IntPtr(UnmanagedPointer aPtr)
        {
            return (obj == null) ? IntPtr.Zero : saPtr.m_ptr;
        }

        public static UnmanagedPointer operator +(UnmanagedPointer aPtr, int size)
        {
            if (IntPtr.Size == 4)
                return new UnmanagedPointer(new IntPtr(aPtr.m_ptr.ToInt32() + size));
            else // Assume 8 bytes
                return new UnmanagedPointer(new IntPtr(aPtr.m_ptr.ToInt64() + size));
        }

        public static bool operator <(UnmanagedPointer lhs, UnmanagedPointer rhs)
        {
            if (IntPtr.Size == 4)
            {
                return (lhs.m_ptr.ToInt32() < rhs.m_ptr.ToInt32());
            }
            else // Assume 8 bytes
            {
                return (lhs.m_ptr.ToInt64() < rhs.m_ptr.ToInt64());
            }
        }

        public static bool operator >(UnmanagedPointer lhs, UnmanagedPointer rhs)
        {
            if (IntPtr.Size == 4)
            {
                return (lhs.m_ptr.ToInt32() > rhs.m_ptr.ToInt32());
            }
            else // Assume 8 bytes
            {
                return (lhs.m_ptr.ToInt64() > rhs.m_ptr.ToInt64());
            }
        }
        #endregion

    }
}
