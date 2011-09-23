using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.GL
{
    /// <summary>
    /// The BufferObjectVertices uses a target of BufferTarget.ArrayBuffer.  It is best to 
    /// store any vertex attributes, such as vertex coordinates, texture coordinates, 
    /// normals and color component arrays. 
    /// 
    /// </summary>
    public class BufferObjectVertices : GLBufferObject
    {
        public BufferObjectVertices(GraphicsInterface gi)
            :base(gi, BufferTarget.ArrayBuffer)
        {
        }
    }
}
