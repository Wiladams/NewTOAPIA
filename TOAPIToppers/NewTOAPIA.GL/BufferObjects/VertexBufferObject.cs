using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.GL
{
    /// <summary>
    /// The VertexBufferObject uses a target of BufferTarget.ArrayBuffer.  
    /// It is best to store vertex attributes, such as vertex coordinates, 
    /// texture coordinates, normals and color components.
    /// </summary>
    public class VertexBufferObject : GLBufferObject
    {
        public VertexBufferObject(GraphicsInterface gi)
            :base(gi, BufferTarget.ArrayBuffer)
        {
        }
    }
}
