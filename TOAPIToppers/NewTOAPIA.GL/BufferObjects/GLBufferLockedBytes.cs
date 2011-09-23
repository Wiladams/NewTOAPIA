using System;
using System.Threading;

namespace NewTOAPIA.GL
{
    public class GLBufferLockedBytes : IDisposable
    {
        GLBufferObject fBuffer;
        BufferTarget fTarget;
        BufferAccess fAccess;
        IntPtr fData;
        int fOffset;
        int fSize;
        Thread fLockingThread;

        #region Constructor
        public GLBufferLockedBytes(GLBufferObject buffer, BufferTarget target, BufferAccess access, IntPtr pointer, int offset, int size)
        {
            fTarget = target;
            fBuffer = buffer;
            fOffset = offset;
            fSize = size;
            fAccess = access;
            fLockingThread = Thread.CurrentThread;
            fData = pointer;
        }
        #endregion

        #region Properties
        public BufferAccess Access
        {
            get { return fAccess; }
        }

        public IntPtr Data
        {
            get { return fData; }
        }

        public int Offset
        {
            get { return fOffset; }
        }

        public int Size
        {
            get { return fSize; }
        }

        public BufferTarget Target
        {
            get { return fTarget; }
        }

        #endregion

        #region Destructors
        public virtual void Dispose()
        {
            // Release the lock from the buffer
        }
        #endregion
    }
}
