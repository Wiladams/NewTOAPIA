using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.GL
{
    public class GPUMemory
    {
        GLBufferObject fGLBuffer;
        GraphicsInterface fGI;
        
        public GPUMemory(GraphicsInterface gi, BufferTarget target, BufferUsage usage, int size)
        {
            fGI = gi;
            fGLBuffer = new GLBufferObject(gi, target, usage, IntPtr.Zero, size);
        }

        public GraphicsInterface GI
        {
            get { return fGI; }
        }

        public void Write(byte[] data, int offset, int count)
        {
            fGLBuffer.Write(data, offset, count);
        }

        public int Read(byte[] data, int offset, int count)
        {
            return 0;
        }
    }
}
