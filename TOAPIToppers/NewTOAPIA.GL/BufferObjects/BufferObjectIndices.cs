using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.GL
{
    /// <summary>
    /// The BufferObjectIndices uses a target of BufferTarget.ElementArrayBuffer.  
    /// It is best to indices for drawing elements. 
    /// </summary>

    public class BufferObjectIndices : GLBufferObject
    {
        public BufferObjectIndices(GraphicsInterface gi)
            : base(gi, BufferTarget.ElementArrayBuffer)
        {
        }

        //public BufferObjectIndices(GraphicsInterface gi, BufferUsage usage, int[] indices)
        //    : base(gi, BufferTarget.ElementArrayBuffer, usage, indices)
        //{
        //}

    }
}
