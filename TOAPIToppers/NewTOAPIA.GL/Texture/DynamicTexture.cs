using System;

using NewTOAPIA;

namespace NewTOAPIA.GL
{
    using NewTOAPIA.Drawing;
    using NewTOAPIA.Graphics;

    public class DynamicTexture : GLTexture2D
    {
        #region Fields
        IntPtr fPixelPtr;
        int fPixelLength;
        int lastBound;
        int latestFrame;
        int newWidth;
        int newHeight;
        IntPtr newPixels;
        bool fNeedsResize;
        //PixelBufferObjectUnpacked fBufferObject;
        #endregion

        #region Constructor
        public DynamicTexture(GraphicsInterface gi, int width, int height, int bytesPerPixel)
            : base(gi, width, height)
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
            //if (null != fBufferObject)
            //{
            //    fBufferObject.Dispose();
            //}

            // Delete the current texture ID
            DeleteTextureId();

            // Get a new texture ID
            TextureID = GetNewTextureID();

            //fBufferObject = new PixelBufferObjectUnpacked(GI, BufferUsage.StreamDraw, fPixelLength);

            // Set our typical parameters
            GI.BindTexture(TextureBindTarget.Texture2d, TextureID);

            SetupTexture2D(GI, width, height, fInternalFormat, fPixelFormat, fPixelType, pixels, false);

            //SetupFiltering();
            //SetupWrapping();
            
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

        #endregion


        public override void Bind()
        {
            // If we haven't received any frames as yet, then
            // just return immediately.
            //if (latestFrame == 0)
            //    return ;

            if (fNeedsResize)
            {
                Resize(newWidth, newHeight, newPixels, 3);
            }

            //fBufferObject.Bind();

            // If we have a frame that's different than the frame we
            // bound to last time, then copy the new frame over to 
            // the texture object.
            //if (lastBound != latestFrame)
            {
                lastBound = latestFrame;
                GI.PixelStore(PixelStore.UnpackAlignment, 1);

                // Copy image data into the buffer object
                //fBufferObject.Write(fPixelLength, fPixelPtr, BufferUsage.StreamDraw);

                // Now load the data into the texture
                // Since we're using a Buffer Object, this should be nothing more
                // than a DMA transfer on the GPU
                GI.BindTexture(BindTarget, TextureID);
                GI.TexImage2D(0, fInternalFormat, Width, Height, 0, fPixelFormat, fPixelType, fPixelPtr);
            }

        }

        public override void Unbind()
        {
                GI.BindTexture(BindTarget, 0);
                //fBufferObject.Unbind();
        }

        public void UpdateImage(PixelAccessorBGRb accessor)
        {
            if (accessor.Width != Width || accessor.Height != Height)
            {
                newWidth = accessor.Width;
                newHeight = accessor.Height;
                newPixels = accessor.Pixels;
                fNeedsResize = true;
            }

            fPixelPtr = accessor.Pixels;
            latestFrame++;
        }
    }
}
