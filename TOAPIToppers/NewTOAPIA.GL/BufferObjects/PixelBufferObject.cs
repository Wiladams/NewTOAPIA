
namespace NewTOAPIA.GL
{
    using System;

    public class PixelBufferObjectPacked : GLBufferObject
    {
        public PixelBufferObjectPacked(GraphicsInterface gi)
            : base(gi, BufferTarget.PixelPackBuffer)
        {
        }
    
        public PixelBufferObjectPacked(GraphicsInterface gi, BufferUsage usage, IntPtr dataPtr, int size)
            : base(gi, BufferTarget.PixelPackBuffer, usage, dataPtr, size)
        {
        }
    }

    public class PixelBufferObjectUnpacked : GLBufferObject
    {
        public PixelBufferObjectUnpacked(GraphicsInterface gi)
            : base(gi, BufferTarget.PixelUnpackBuffer)
        {
        }
    
        public PixelBufferObjectUnpacked(GraphicsInterface gi, BufferUsage usage, IntPtr dataPtr, int size)
            : base(gi, BufferTarget.PixelUnpackBuffer, usage, dataPtr, size)
        {
        }

    }

}
