using System;

using NewTOAPIA;
using NewTOAPIA.GL;

using TOAPI.OpenGL;

namespace SnapNGLView
{
    public class DynamicTexture : GLTexture
    {
        #region Fields
        PixelBufferObjectUnpacked fBufferObject;

        IntPtr fPixelPtr;
        int fPixelLength;
        int fWidth;
        int fHeight;

        int lastBound;
        int latestFrame;

        int newWidth;
        int newHeight;
        IntPtr newPixels;
        bool fNeedsResize;
        #endregion

        #region Constructor
        public DynamicTexture(GraphicsInterface gi, int width, int height, int bytesPerPixel)
            : base(gi)
        {
            lastBound = 0;
            latestFrame = 0;
            newPixels = IntPtr.Zero;

            Resize(width, height, newPixels, bytesPerPixel);
        }

        void Resize(int width, int height, IntPtr pixels, int bytesPerPixel)
        {
            fWidth = width;
            fHeight = height;
            fPixelLength = width * height * bytesPerPixel;
            fPixelPtr = pixels;

            // Free up the current buffer object
            if (null != fBufferObject)
            {
                fBufferObject.Dispose();
            }

            fBufferObject = new PixelBufferObjectUnpacked(GI, BufferUsage.StreamDraw, fPixelLength);

            // Delete the current texture ID
            uint[] texid = { TextureID };
            GI.DeleteTextures(1, texid);

            // Get a new texture ID
            TextureID = GLTexture.GetNewTextureID();


            // Set our typical parameters
            GI.BindTexture(TextureBindTarget.Texture2d, TextureID);
            GI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureWrapS, TextureWrapMode.Clamp);
            GI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureWrapT, TextureWrapMode.Clamp);
            
            GI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureMinFilter, TextureMinFilter.Linear);
            GI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureMagFilter, TextureMagFilter.Linear);
            GI.TexEnv(TextureEnvModeParam.Decal);

            fNeedsResize = false;
        }

        #endregion

        #region Properties

        public override int Width
        {
            get
            {
                return fWidth;
            }
        }

        public override int Height
        {
            get
            {
                return fHeight;
            }
        }

        public override GLPixelData PixelData
        {
            get
            {
                return null;
            }
        }
        #endregion


        public override void Bind()
        {
            // If we haven't received any frames as yet, then
            // just return immediately.
            if (latestFrame == 0)
                return ;

            if (fNeedsResize)
            {
                Resize(newWidth, newHeight, newPixels, 3);
            }

            fBufferObject.Bind();

            // If we have a frame that's different than the frame we
            // bound to last time, then copy the new frame over to 
            // the texture object.
            //if (lastBound != latestFrame)
            {
                lastBound = latestFrame;

                // Copy image data into the buffer object
                fBufferObject.Write(fPixelLength, fPixelPtr, BufferUsage.StreamDraw);

                // Now load the data into the texture
                // Since there is a Bound Buffer Object, this should be nothing more
                // than a DMA transfer on the GPU
                GI.PixelStore(PixelStore.UnpackAlignment, 1);
                GI.BindTexture(TextureBindTarget.Texture2d, TextureID);
                GI.TexImage2D(0, TextureInternalFormat.Rgb8, Width, Height, 0, TexturePixelFormat.Bgr, PixelType.UnsignedByte, IntPtr.Zero);
            }

        }

        public override void Unbind()
        {
            // Unbind the texture itself
            GI.BindTexture(TextureBindTarget.Texture2d, 0);

            // Unbind the buffer object
            fBufferObject.Unbind();
        }

        public void UpdateImage(PixelAccessorBGRb accessor)
        {
            latestFrame++;

            if (accessor.Width != Width || accessor.Height != Height)
            {
                newWidth = accessor.Width;
                newHeight = accessor.Height;
                newPixels = accessor.Pixels;
                fNeedsResize = true;

                return;
            }

            fPixelPtr = accessor.Pixels;
        }
    }
}
