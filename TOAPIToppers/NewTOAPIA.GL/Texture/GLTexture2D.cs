using System;


namespace NewTOAPIA.GL
{
    using TOAPI.OpenGL;

    using NewTOAPIA;
    using NewTOAPIA.Drawing;
    using NewTOAPIA.Graphics;

    public class GLTexture2D : GLTexture
    {
        #region Private Fields

        protected int fWidth;
        protected int fHeight;
        #endregion

        #region Constructors
        protected GLTexture2D(GraphicsInterface gi, TextureBindTarget bindTarget)
            : base(gi, bindTarget)
        {
        }

        public GLTexture2D(GraphicsInterface gi, int width, int height)
            : this(gi, width, height, TextureInternalFormat.Rgba8, TexturePixelFormat.Rgba, PixelComponentType.UnsignedByte, IntPtr.Zero, false)
        {
        }

        public GLTexture2D(GraphicsInterface gi, PixelAccessorBGRb pixMap, bool createMipMaps)
            : this(gi, pixMap.Width, pixMap.Height, TextureInternalFormat.Rgb8, TexturePixelFormat.Bgr, PixelComponentType.Byte, pixMap.Pixels, createMipMaps)
        {
        }

        public GLTexture2D(GraphicsInterface gi, GLPixelData pixeldata, bool createMipMaps)
            : this(gi, pixeldata.Width, pixeldata.Height, pixeldata.InternalFormat, (TexturePixelFormat)pixeldata.PixelFormat, pixeldata.PixelType, pixeldata.Pixels, createMipMaps)
        {
        }

        public GLTexture2D(GraphicsInterface gi, int width, int height, TextureInternalFormat internalFormat, TexturePixelFormat pixelFormat, PixelComponentType pixelType, IntPtr pixelData, bool createMipMaps)
            : base(gi, TextureBindTarget.Texture2d)
        {
            SetupTexture2D(gi, width, height, internalFormat, pixelFormat, pixelType, pixelData, createMipMaps);
        }

        protected virtual void SetupTexture2D(GraphicsInterface gi, int width, int height, TextureInternalFormat internalFormat, TexturePixelFormat pixelFormat, PixelComponentType pixelType, IntPtr pixelData, bool createMipMaps)
        {
            fWidth = width;
            fHeight = height;

            fInternalFormat = internalFormat;
            fPixelFormat = pixelFormat;
            fPixelType = pixelType;

            // Setup the alignment
            fGI.PixelStore(PixelStore.UnpackAlignment, 1);

            // Allocate storage for the texture
            // We do this once at the beginning to allocate space for the texture object
            fGI.TexImage2D(0, internalFormat, fWidth, fHeight, 0, pixelFormat, pixelType, pixelData);

            // Setup default filters and wrapping
            SetupFiltering();
            SetupWrapping();

            if (createMipMaps)
            {
                // Make call to generate MipMap
                //fGI.GenerateMipmap();
                
                // Alter the min filter to use the mipmap
                fGI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureMinFilter, TextureMinFilter.LinearMipmapLinear);
            }

        }

        protected void SetupFiltering()
        {
            // Filtering
            fGI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureMagFilter, TextureMagFilter.Nearest);
            fGI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureMinFilter, TextureMinFilter.Nearest);
        }

        protected void SetupWrapping()
        {
            // Wrapping
            fGI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureWrapS, TextureWrapMode.Clamp);
            fGI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureWrapT, TextureWrapMode.Clamp);
        }


        #endregion

        #region Public Methods

        public virtual byte[] RetrievePixelData()
        {
            byte[] data = new byte[1];
            GI.GetTexImage((GetTextureImageTarget)BindTarget, 0, (GetTextureImageFormat)fPixelFormat, (GetTextureImageType)fPixelType, data);
            return data;
        }

        public virtual void AssignTexelData(GLPixelData pixData)
        {
        }
        #endregion

        #region Properties
        public virtual int Width
        {
            get { return fWidth; }
        }

        public virtual int Height
        {
            get { return fHeight; }
        }
        #endregion

    }
}
