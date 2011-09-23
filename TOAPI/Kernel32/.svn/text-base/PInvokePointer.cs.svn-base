using System;
using System.Runtime.InteropServices;

namespace TOAPI.Kernel32
{
    public class PInvokePointer : IDisposable
    {

        private IntPtr m_ptr;

        private int m_size;

        public PInvokePointer(IntPtr ptr, int size)
        {
            m_ptr = ptr;
            m_size = size;
        }

        public PInvokePointer(int size)
        {
            m_ptr = Marshal.AllocCoTaskMem(size);
            m_size = size;
        }

        public virtual IntPtr IntPtr
        {
            get
            {
                return m_ptr;
            }
        }

        public virtual void Free()
        {
            Marshal.FreeCoTaskMem(m_ptr);
            m_ptr = System.IntPtr.Zero;
            m_size = 0;
        }

        public virtual byte[] ToByteArray()
        {
            byte[] arr = new byte[m_size];
            Marshal.Copy(m_ptr, arr, 0, m_size);
            return arr;
        }

        public virtual void Dispose()
        {
            Free();
        }
    }
}
