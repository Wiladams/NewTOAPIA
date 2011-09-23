using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA  
{
    using System.Runtime.InteropServices;

    public class UnmanagedMemory : IDisposable
    {
        int fSize;
        UnmanagedPointer fMemoryPtr { get; set; }
        private bool disposed = false;

        public UnmanagedMemory(int size)
        {
            this.fSize = size;
            fMemoryPtr = new UnmanagedPointer(Marshal.AllocCoTaskMem((int)size));

        }

        public UnmanagedPointer MemoryPointer
        {
            get
            {
                if (this.disposed)
                {
                    throw new ObjectDisposedException("UnmanagedPointer");
                }

                return fMemoryPtr;
            }
        }

        public virtual byte[] ToByteArray()
        {
            byte[] arr = new byte[fSize];
            Marshal.Copy(MemoryPointer, arr, 0, fSize);

            return arr;
        }

        public void Dispose()
        {
            Dispose(true);
            // Take yourself off the Finalization queue 
            // to prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);

        }

        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the 
        // runtime from inside the finalizer and you should not reference 
        // other objects. Only unmanaged resources can be disposed.
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                // If disposing equals true, dispose all managed 
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                }

                // Release unmanaged resources. If disposing is false, 
                // only the following code is executed.
                Marshal.FreeCoTaskMem(fMemoryPtr);
                fMemoryPtr = null;
                // Note that this is not thread safe.
                // Another thread could start disposing the object
                // after the managed resources are disposed,
                // but before the disposed flag is set to true.
                // If thread safety is necessary, it must be
                // implemented by the client.

            }
            disposed = true;
        }

        // Use C# destructor syntax for finalization code.
        // This destructor will run only if the Dispose method 
        // does not get called.
        // It gives your base class the opportunity to finalize.
        // Do not provide destructors in types derived from this class.
        ~UnmanagedMemory()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }
    }
}
