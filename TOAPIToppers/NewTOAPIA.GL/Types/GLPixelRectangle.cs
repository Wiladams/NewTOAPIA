
namespace NewTOAPIA.GL
{
    using System;
    using System.Runtime.InteropServices;

    using NewTOAPIA.Drawing;
    using NewTOAPIA.Graphics;

    public abstract class GLPixelRectangle : GIObject
    {
        public GLBufferObject BufferObject { get; set; }

        public GLPixelRectangle(GraphicsInterface gi)
            : base(gi)
        {
        }
    }

    public class GLPixelRectangleDraw : GLPixelRectangle, IDisposable
    {
        PixelRectangleInfo fPixelRectInfo;

        public GLPixelRectangleDraw(GraphicsInterface gi, PixelRectangleInfo pixelInfo)
            : base(gi)
        {
            fPixelRectInfo = pixelInfo;

            int buffSize = pixelInfo.GetImageSize();
            BufferObject = new PixelBufferObjectUnpacked(gi, BufferUsage.StaticDraw, IntPtr.Zero, buffSize);
        }

        public virtual void Dispose()
        {
            BufferObject.Dispose();
        }
    }
}
