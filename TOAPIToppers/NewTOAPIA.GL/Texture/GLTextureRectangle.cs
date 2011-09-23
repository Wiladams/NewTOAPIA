namespace NewTOAPIA.GL
{
    using System;
    using NewTOAPIA.Graphics;

    public class GLTextureRectangle : GLTexture2D
    {
        public GLTextureRectangle(GraphicsInterface gi, int width, int height, TextureInternalFormat internalFormat, TexturePixelFormat pixelFormat, PixelComponentType pixelType)
            : base(gi, TextureBindTarget.Rectangle)
        {
            gi.Enable(GLOption.TextureRectangle);
            SetupTexture2D(gi, width, height, internalFormat, pixelFormat, pixelType, IntPtr.Zero, false);
        }
    }
}
