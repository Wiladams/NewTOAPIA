using System;
using System.Runtime.InteropServices;
using System.IO;

using TOAPI.OpenGL;

namespace NewTOAPIA.GL
{
    public class GLBufferObject : GIObject, IBindable, IDisposable
    {
        const int UnbindID = 0;

        int fBufferID;
        BufferTarget fBufferTarget;


        #region Constructor
        public GLBufferObject(GraphicsInterface gi, BufferTarget target)
            : this(gi, target, BufferUsage.StreamDraw, IntPtr.Zero, 0)
        {
        }

        public GLBufferObject(GraphicsInterface gi, BufferTarget target, BufferUsage usage, IntPtr dataPtr, int size)
            :base(gi)
        {
            fBufferTarget = target;

            // Generate the buffer ID
            gi.GenBufferID(out fBufferID);

            // Bind the buffer so subsequent calls apply to that buffer
            Bind();

            if (size > 0)
                AllocateStorage(dataPtr, size, usage);

            Unbind();
        }
        #endregion

        public void AllocateStorage(IntPtr dataPtr, int size, BufferUsage usage)
        {
            if (size < 1)
                return;

            //if (dataPtr == IntPtr.Zero)
            //    return;

            // Allocate memory for the buffer
            GI.BufferData(fBufferTarget, size, dataPtr, usage);
        }

        #region Properties

        public int BufferID
        {
            get { return fBufferID; }
        }

        public BufferTarget Target
        {
            get { return fBufferTarget; }
        }

        public int Size
        {
            get
            {
                int[] pvalues = new int[1];
                GI.GetBufferParameteriv(fBufferTarget, BufferParameter.BufferSize, pvalues);
                int retValue = pvalues[0];
                return retValue;
            }

            set
            {
                GI.BufferData(Target, value, IntPtr.Zero, Usage);
            }
        }

        public BufferUsage Usage
        {
            get
            {
                int[] pvalues = new int[1];
                GI.GetBufferParameteriv(fBufferTarget, BufferParameter.BufferUsage, pvalues);
                int retValue = pvalues[0];
                return (BufferUsage)retValue;

            }
        }
        public bool IsMapped
        {
            get
            {
                int[] pvalues = new int[1];
                GI.GetBufferParameteriv(fBufferTarget, BufferParameter.BufferMapped, pvalues);
                int retValue = pvalues[0];

                return retValue ==0? false : true;
            }
        }

        #endregion

        #region Public Methods
        public virtual void Bind()
        {
            GI.BindBuffer(fBufferTarget, fBufferID);
        }

        public virtual void Unbind()
        {
            GI.BindBuffer(fBufferTarget, UnbindID);
        }

        //public Stream GetStream(FileAccess access)
        //{
        //    // Map the thing to client memory
        //    IntPtr mappedPtr = MapBuffer(BufferAccess.ReadWrite);

        //    if (mappedPtr == null)
        //        return null;

        //    UnmanagedMemoryStream memStream = null;

        //    // Now that we have it mapped, create the memorystream
        //    unsafe
        //    {
        //        byte* memBytePtr = (byte*)mappedPtr.ToPointer();

        //        memStream = new UnmanagedMemoryStream(memBytePtr, this.Size);
        //    }

        //    return memStream;
        //}


        public void Write(byte[] data, int offset, int size)
        {
            GCHandle dataPtr = GCHandle.Alloc(data, GCHandleType.Pinned);
            try
            {
                Write(dataPtr.AddrOfPinnedObject(), offset, size);
            }
            finally
            {
                dataPtr.Free();
            }
        }

        public void Write(IntPtr data, int size)
        {
            Write(data, 0, size);
        }

        public void Write(IntPtr data, int offset, int size)
        {
            GI.BufferSubData(fBufferTarget, (int)0, size, data);
        }

        public IntPtr GetMappedPointer()
        {
            IntPtr pointer = new IntPtr();

            GI.GetBufferMappedPointer(Target, ref pointer);

            return pointer;
        }

        public IntPtr MapBuffer(BufferAccess access)
        {
            // If it's already mapped, return the pointer that already
            // exists for the mapping.
            if (IsMapped)
                return GetMappedPointer();

            // If it's not already mapped, then map it and return the pointer.
            IntPtr mappedPointer = GI.MapBuffer(fBufferTarget, access);

            return mappedPointer;
        }

        public void UnmapBuffer()
        {
            GI.UnmapBuffer(fBufferTarget);
        }
        #endregion

        #region Disposal
        public virtual void Dispose()
        {
            GI.DeleteBuffers(1, new int[] { fBufferID });
        }
        #endregion
    }
}
