using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace NewTOAPIA.GL
{
    using System.Runtime.InteropServices;

    using NewTOAPIA.Drawing;
    using NewTOAPIA.Graphics;

    public class TextureHelper
    {
        #region Public Methods
        public static bool FillInAllocatedRGBADataWithBitmapData(Bitmap bitmap, byte[] rgbaData)
        {
            if (null == bitmap)
            {
                return (false);
            }

            if (null == rgbaData)
            {
                return (false);
            }

            if ((bitmap.Width <= 0) || (bitmap.Height <= 0))
            {
                return (false);
            }

            int totalBytes = ((bitmap.Width * 4) * bitmap.Height);

            if (rgbaData.Length != totalBytes)
            {
                return (false);
            }


            // The source has the pixel (0,0) at the upper-left of the image.
            // The destination OpenGL texture has texel (0,0) at the lower-left
            // of the texture.

            // Copy the BGRA source to the destination RGBA buffer.

            //int sourceLineStart = 0;

            int destinationDirection = -1;
            int destinationStride = (4 * bitmap.Width);
            int destinationHeight = (bitmap.Height);
            //int destinationLineStart = 0;
            int destinationLineStart = (destinationStride * (destinationHeight - 1));

            int copyWidth = bitmap.Width;
            int copyHeight = bitmap.Height;

            int x = 0;
            int y = 0;
            int k = 0;

            bool exceptionFlag = false;

            try
            {

#if ALLOW_OPTIMIZATIONS_THAT_REQUIRE_UNSAFE_CODE_COMPILE_SETTING
        unsafe
        {
          System.Drawing.Imaging.BitmapData bitmapData =
            bitmap.LockBits
              (
              new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
              System.Drawing.Imaging.ImageLockMode.ReadOnly,
              System.Drawing.Imaging.PixelFormat.Format32bppArgb
              );

          byte* sourceBGRA = (byte*)bitmapData.Scan0.ToPointer();
          int sourceStride = bitmapData.Stride;

          for (y = 0; y < copyHeight; y++)
          {
            k = 0;
            for (x = 0; x < copyWidth; x++)
            {
              rgbaData[destinationLineStart + k + 0] = sourceBGRA[sourceLineStart + k + 2];
              rgbaData[destinationLineStart + k + 1] = sourceBGRA[sourceLineStart + k + 1];
              rgbaData[destinationLineStart + k + 2] = sourceBGRA[sourceLineStart + k + 0];
              rgbaData[destinationLineStart + k + 3] = sourceBGRA[sourceLineStart + k + 3];
              k += 4;
            }
            sourceLineStart += sourceStride;
            destinationLineStart -= destinationStride;
          }

          bitmap.UnlockBits(bitmapData);
        }

#else

                // The following is an alternative to using "unsafe" code to copy bytes
                // from the bitmap to a buffer.

                {
                    Color color = Color.Black;

                    for (y = 0; y < copyHeight; y++)
                    {
                        k = destinationLineStart;
                        for (x = 0; x < copyWidth; x++)
                        {
                            color = bitmap.GetPixel(x, y);
                            rgbaData[k + 0] = color.R;
                            rgbaData[k + 1] = color.G;
                            rgbaData[k + 2] = color.B;
                            rgbaData[k + 3] = color.A;
                            k += 4;
                        }
                        destinationLineStart += destinationDirection * destinationStride;
                    }

                }

#endif
            }
            catch
            {
                exceptionFlag = true;
            }

            if (true == exceptionFlag)
            {
                return (false);
            }

            return (true);
        }


        public static byte[] ConvertBitmapToRGBAData(Bitmap bitmap)
        {
            if (null == bitmap)
            {
                return (null);
            }

            if ((bitmap.Width <= 0) || (bitmap.Height <= 0))
            {
                return (null);
            }

            int totalBytes = 0;
            totalBytes = ((bitmap.Width * 4) * bitmap.Height);

            byte[] rgbaData = new byte[totalBytes];

            bool result = TextureHelper.FillInAllocatedRGBADataWithBitmapData(bitmap, rgbaData);

            if (false == result)
            {
                // If we fail to convert the bitmap to data
                return (null);
            }

            return (rgbaData);
        }


        public static bool CopyRGBABufferToCompatibleBitmap(byte[] rgbaData, Bitmap bitmap)
        {
            if (null == rgbaData)
            {
                return (false);
            }

            if (null == bitmap)
            {
                return (false);
            }

            if ((bitmap.Width <= 0) || (bitmap.Height <= 0))
            {
                return (false);
            }

            int totalBytes = ((4 * bitmap.Width) * bitmap.Height);
            if (rgbaData.Length != totalBytes)
            {
                return (false);
            }


            // The source OpenGL RGBA buffer has pixel (0,0) at the lower-left.
            // The destination Bitmap BGRA has the pixel (0,0) at the upper-left.

            // Copy the RGBA source to the destination BGRA buffer.

            int sourceStride = (bitmap.Width * 4);
            int sourceHeight = (bitmap.Height);
            int sourceLineStart = (sourceStride * (sourceHeight - 1));

            //int destinationLineStart = 0;

            int copyWidth = bitmap.Width;
            int copyHeight = bitmap.Height;

            int x = 0;
            int y = 0;
            int k = 0;



            bool exceptionFlag = false;

            try
            {

#if ALLOW_OPTIMIZATIONS_THAT_REQUIRE_UNSAFE_CODE_COMPILE_SETTING
        unsafe
        {
          System.Drawing.Imaging.BitmapData bitmapData =
            bitmap.LockBits
              (
              new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
              System.Drawing.Imaging.ImageLockMode.ReadWrite,
              System.Drawing.Imaging.PixelFormat.Format32bppArgb
              );

          byte* destinationBGRA = (byte*)bitmapData.Scan0.ToPointer();
          int destinationStride = bitmapData.Stride;

          for (y = 0; y < copyHeight; y++)
          {
            k = 0;
            for (x = 0; x < copyWidth; x++)
            {
              destinationBGRA[destinationLineStart + k + 0] = rgbaData[sourceLineStart + k + 2];
              destinationBGRA[destinationLineStart + k + 1] = rgbaData[sourceLineStart + k + 1];
              destinationBGRA[destinationLineStart + k + 2] = rgbaData[sourceLineStart + k + 0];
              destinationBGRA[destinationLineStart + k + 3] = rgbaData[sourceLineStart + k + 3];
              k += 4;
            }
            sourceLineStart -= sourceStride;
            destinationLineStart += destinationStride;
          }

          bitmap.UnlockBits(bitmapData);
        }

#else

                // The following is an alternative to using "unsafe" code to copy bytes
                // from the bitmap to a buffer.

                {

                    for (y = 0; y < copyHeight; y++)
                    {
                        k = sourceLineStart;
                        for (x = 0; x < copyWidth; x++)
                        {
                            bitmap.SetPixel(x, y, Color.FromArgb(rgbaData[k + 3], rgbaData[k + 0], rgbaData[k + 1], rgbaData[k + 2]));
                            k += 4;
                        }
                        sourceLineStart -= sourceStride;
                    }

                }

#endif

            }
            catch
            {
                exceptionFlag = true;
            }


            if (true == exceptionFlag)
            {
                return (false);
            }


            return (true);
        }


        public static Bitmap ConvertRGBABufferToBitmap(byte[] rgbaData, int width, int height)
        {
            if ((width <= 0) || (height <= 0) || (null == rgbaData))
            {
                return (null);
            }

            // Create a Bitmap object of the appropriate dimensions
            Bitmap bitmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            if (null == bitmap)
            {
                return (null);
            }

            bool result = TextureHelper.CopyRGBABufferToCompatibleBitmap(rgbaData, bitmap);
            if (false == result)
            {
                // Copy failed in some manner.  But the Bitmap can still be returned.
            }

            return (bitmap);
        }


        public static bool WriteBitmapToImageFile(Bitmap bitmap, String filePathAndName, System.Drawing.Imaging.ImageFormat imageFormat)
        {
            if (null == bitmap)
            {
                return (false);
            }

            if (null == filePathAndName)
            {
                return (false);
            }

            if (filePathAndName.Length <= 0)
            {
                return (false);
            }

            bool exceptionFlag = false;

            try
            {

                if (System.Drawing.Imaging.ImageFormat.Jpeg == imageFormat)
                {
                    // JPEG 
                    int quality100 = 100; // NOTE: .NET built-in JPEG encoder does severe color down-sampling (BAD!), so use highest possible quality.

                    //Get the list of available encoders
                    System.Drawing.Imaging.ImageCodecInfo[] encoderInfoArray = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders();

                    //find the encoder with the image/jpeg mime-type
                    System.Drawing.Imaging.ImageCodecInfo jpegEncoderInfo = null;

                    foreach (System.Drawing.Imaging.ImageCodecInfo imageCodecInfo in encoderInfoArray)
                    {
                        if (0 == String.Compare(imageCodecInfo.MimeType, "image/jpeg", true))
                        {
                            jpegEncoderInfo = imageCodecInfo;
                        }
                    }

                    if (null != jpegEncoderInfo)
                    {
                        //Create a collection of encoder parameters (we only need one in the collection)
                        System.Drawing.Imaging.EncoderParameters encoderParameters = new System.Drawing.Imaging.EncoderParameters(1);

                        // Create an encoder parameter for the "Quality" parameter of JPEG
                        System.Drawing.Imaging.EncoderParameter encoderParameter = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)quality100);

                        // Put the parameter in to the parameter array.
                        encoderParameters.Param[0] = encoderParameter;

                        bitmap.Save(filePathAndName, jpegEncoderInfo, encoderParameters);
                    }
                }


                if (System.Drawing.Imaging.ImageFormat.Bmp == imageFormat)
                {
                    // BMP 
                    bitmap.Save(filePathAndName, imageFormat);
                }


                if (System.Drawing.Imaging.ImageFormat.Gif == imageFormat)
                {
                    // GIF 

                    // Note: For high-quality GIF, use optimized color palette builder code available online...

                    bitmap.Save(filePathAndName, imageFormat);
                }


                if (System.Drawing.Imaging.ImageFormat.Png == imageFormat)
                {
                    // PNG 
                    bitmap.Save(filePathAndName, imageFormat);
                }


            }
            catch
            {
                exceptionFlag = true;
            }


            if (true == exceptionFlag)
            {
                return (false);
            }

            return (true);
        }


        public static bool WriteRGBABufferToImageFile(byte[] rgbaData, int width, int height, String filePathAndName, System.Drawing.Imaging.ImageFormat imageFormat)
        {
            if (null == rgbaData)
            {
                return (false);
            }

            if (null == filePathAndName)
            {
                return (false);
            }

            if (filePathAndName.Length <= 0)
            {
                return (false);
            }

            // Convert RGBA data to Bitmap
            Bitmap bitmap = TextureHelper.ConvertRGBABufferToBitmap(rgbaData, width, height);

            if (null == bitmap)
            {
                return (false);
            }

            bool result = TextureHelper.WriteBitmapToImageFile(bitmap, filePathAndName, imageFormat);

            bitmap.Dispose();
            bitmap = null;

            if (false == result)
            {
                return (false);
            }

            return (true);
        }


        public static Bitmap ReadImageFileToBitmap(String filePathAndName)
        {
            Bitmap bitmap = null;

            bool exceptionFlag = false;

            try
            {
                bitmap = new Bitmap(filePathAndName);
            }
            catch
            {
                exceptionFlag = true;
            }

            if (true == exceptionFlag)
            {
                return (null);
            }

            return (bitmap);
        }

        //public void TransferTextureDataBackToHostMemory(GraphicsInterface gr)
        //{
        //    if (null == RGBAData)
        //    {
        //        return;
        //    }

        //    try
        //    {
        //        gl.glGetTexImage
        //          (
        //          gl.GL_TEXTURE_2D,     // target
        //          0,                    // level (0 == top-most mip-map level)
        //          gl.GL_RGBA,           // internalFormat
        //          gl.GL_UNSIGNED_BYTE,  // type
        //          RGBAData        // data
        //          );
        //    }
        //    catch
        //    {
        //    }
        //}

        public static GLTexture2D CreateCheckerboardTexture(GraphicsInterface gi, int width, int height, int blockSize)
        {
            int totalBytes = ((width * 4) * height);
            byte[] databytes = new byte[totalBytes];
            //bool useMipMaps = true;

            int x = 0;
            int y = 0;
            int k = 0;
            byte val = 0;

            for (y = 0; y < height; y++)
            {
                for (x = 0; x < width; x++)
                {
                    k = ((width * 4) * y) + (x * 4);

                    val = 0;
                    if (0 == (((x / blockSize) + (y / blockSize)) % 2))
                        val = 255;

                    databytes[k + 0] = val; // red
                    databytes[k + 1] = val; // green
                    databytes[k + 2] = val; // blue
                    databytes[k + 3] = 255; // opacity (255 == opaque)
                }
            }

            GLPixelData pixMap = new GLPixelData(width, height, TextureInternalFormat.Rgba8, PixelLayout.Rgba, PixelComponentType.UnsignedByte, databytes);
            GLTexture2D tex = new GLTexture2D(gi, pixMap, false);
            
            //GCHandle data_ptr = GCHandle.Alloc(databytes, GCHandleType.Pinned);
            //IntPtr fDataPtr = (IntPtr)data_ptr.AddrOfPinnedObject();
            //GLTexture2D tex = new GLTexture2D(gi, width, height, TextureInternalFormat.Rgba8, TexturePixelFormat.Rgba, PixelComponentType.UnsignedByte, fDataPtr, false);
            //data_ptr.Free();

            return tex;
        }


        //public bool UpdateTextureWithBitmapData(GraphicsInterface gr, Bitmap bitmap)
        //{
        //    if (null == bitmap)
        //    {
        //        return (false);
        //    }

        //    bool result;

        //    if ((fPixelData.Width != bitmap.Width) || (fPixelData.Height != bitmap.Height))
        //    {
        //        // Uh, oh!  User submitted a bitmap which has dimensions different from the
        //        // original dimensions!  Create a new texture!
        //        result = CreateTextureFromBitmap(gr, bitmap, fUseMipMaps);
        //        return (result);
        //    }

        //    // Okay, the supplied bitmap is compatible with the current texture dimensions.
        //    // Copy bitmap data to our already-allocated byte buffer.
        //    result = GLImage.FillInAllocatedRGBADataWithBitmapData(bitmap, fPixelData.Data);
        //    if (false == result)
        //    {
        //        return (false);
        //    }

        //    SubmitModifiedInternalRGBADataToTexture(gr);

        //    return (true);
        //}

        //public bool TransferOpenGLTextureDataBackToHostMemoryAndCopyToCompatibleBitmap(GraphicsInterface gr, Bitmap bitmap)
        //{
        //    if (null == fPixelData.Data)
        //    {
        //        return (false);
        //    }

        //    TransferTextureDataBackToHostMemory(gr);

        //    if (null == bitmap)
        //    {
        //        return (false);
        //    }

        //    if ((fPixelData.Width != bitmap.Width) || (fPixelData.Height != bitmap.Height))
        //    {
        //        // Uh, oh!  User submitted a bitmap which has dimensions different from the
        //        // texture dimensions!  Give up!
        //        return (false);
        //    }

        //    bool result = GLImage.CopyRGBABufferToCompatibleBitmap(fPixelData.Data, bitmap);

        //    return (result);
        //}


        //public void SubmitModifiedInternalRGBADataToTexture(GraphicsInterface gr)
        //{
        //    if (null == fPixelData.Data)
        //    {
        //        return;
        //    }

        //    // Make sure total bytes matches RGBA * width * height.
        //    int totalBytes = ((4 * fPixelData.Width) * fPixelData.Height);
        //    if (totalBytes != fPixelData.Data.Length)
        //    {
        //        return;
        //    }


        //    gl.glBindTexture(gl.GL_TEXTURE_2D, fTextureID);



        //    if (fUseMipMaps)
        //    {
        //        // Will this work?
        //        //glu.gluBuild2DMipmaps
        //        //(
        //        //    gl.GL_TEXTURE_2D,      // target                    
        //        //    gl.GL_RGBA,            // internalFormat
        //        //    fWidth,           // width
        //        //    fHeight,          // height
        //        //    gl.GL_RGBA,            // format
        //        //    gl.GL_UNSIGNED_BYTE,   // type
        //        //    fRGBAData         // data
        //        //);
        //    }
        //    else
        //    {
        //        gl.glTexSubImage2D
        //        (
        //            gl.GL_TEXTURE_2D,      // target
        //            0,                     // level
        //            0,                     // xoffset
        //            0,                     // yoffset
        //            fPixelData.Width,           // width
        //            fPixelData.Height,          // height
        //            gl.GL_RGBA,            // format
        //            gl.GL_UNSIGNED_BYTE,   // type
        //            fPixelData.Data         // pixels
        //        );
        //    }

        //}
        #endregion

        #region Static Methods
        public static GLTexture2D CreateTextureFromPixelData(GraphicsInterface gi, GLPixelData pixeldata, bool createMipMaps)
        {
            GLTexture2D tex = new GLTexture2D(gi, pixeldata, createMipMaps);

            return tex;
        }

        public static GLTexture2D CreateTextureFromBitmap(GraphicsInterface gi, Bitmap bitmap, bool useMipMaps)
        {
            if (null == bitmap)
            {
                return (null);
            }

            byte[] pixeldata = TextureHelper.ConvertBitmapToRGBAData(bitmap);
            GLPixelData pixMap = new GLPixelData(bitmap.Width, bitmap.Height, TextureInternalFormat.Rgba8, PixelLayout.Rgba, PixelComponentType.UnsignedByte, pixeldata);

            GLTexture2D tex = CreateTextureFromPixelData(gi, pixMap, useMipMaps);

            return tex;
        }

        public static GLTexture2D CreateTextureFromFile(GraphicsInterface gr, String filePathAndName, bool useMipMaps)
        {
            Bitmap bitmap = TextureHelper.ReadImageFileToBitmap(filePathAndName);

            if (null == bitmap)
            {
                return null;
            }

            GLTexture2D tex = CreateTextureFromBitmap(gr, bitmap, useMipMaps);

            bitmap.Dispose();

            return tex;
        }

        #endregion

        #region DrawTextureImageUnrotatedAndOrthographically
        public static void DrawTextureImageUnrotatedAndOrthographically(GraphicsInterface gi, int clientWidth, int clientHeight, GLTexture2D texture,
          int drawX, int drawYTextMode, // i.e., 0 == draw TOP of image at TOP of viewport, Y-axis points DOWN
          int drawWidth, int drawHeight)
        {
            // Change rendering conditions
            gi.Features.DepthTest.Disable();
            gi.Features.CullFace.Disable();

            // Preserve current matrices, and switch to an orthographic view, and 
            //   do scaling and translation as necessary.
            gi.MatrixMode(MatrixMode.Projection);
            gi.PushMatrix();
            gi.MatrixMode(MatrixMode.Modelview);
            gi.PushMatrix();


            gi.MatrixMode(MatrixMode.Projection);
            gi.LoadIdentity();
            gi.Ortho(0, (clientWidth - 1), 0, (clientHeight - 1), -1.0, 1.0);

            gi.MatrixMode(MatrixMode.Modelview);
            gi.LoadIdentity();



            if (null != texture)
            {
                // Enable texture
                texture.Bind();
            }

            // Enable blending
            gi.Features.Blend.Enable();
            gi.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);


            // Draw a quad

            gi.Drawing.Quads.Begin();

            // TOP-LEFT
            gi.TexCoord(0.0f, 1.0f);
            gi.Color(ColorRGBA.White);
            gi.Vertex((drawX), ((clientHeight - 1) - drawYTextMode), 0.0f);

            // BOTTOM-LEFT
            gi.TexCoord(0.0f, 0.0f);
            gi.Color(1.0f, 1.0f, 1.0f, 1.0f);
            gi.Vertex(drawX, ((clientHeight - 1) - (drawYTextMode + drawHeight)), 0.0f);

            // BOTTOM-RIGHT
            gi.TexCoord(1.0f, 0.0f);
            gi.Color(1.0f, 1.0f, 1.0f, 1.0f);
            gi.Vertex((drawX + (drawWidth)), ((clientHeight - 1) - (drawYTextMode + drawHeight)), 0.0f);

            // TOP-RIGHT
            gi.TexCoord(1.0f, 1.0f);
            gi.Color(1.0f, 1.0f, 1.0f, 1.0f);
            gi.Vertex((drawX + (drawWidth)), ((clientHeight - 1) - drawYTextMode), 0.0f);

            gi.Drawing.Quads.End();


            // Disable blending
            gi.Features.Blend.Disable();

            if (null != texture)
            {
                // Disable texture
                gi.Features.Texturing2D.Disable();
                gi.BindTexture(TextureBindTarget.Texture2d, 0);
            }


            // Restore original matrices.
            gi.MatrixMode(MatrixMode.Modelview);
            gi.PopMatrix();
            gi.MatrixMode(MatrixMode.Projection);
            gi.PopMatrix();


            // Restore rendering conditions
            gi.FrontFace(FrontFaceDirection.Ccw); // MUST DO AFTER USING wglUseFontOutlines LISTS!!!
            gi.Features.DepthTest.Enable();
            gi.Features.CullFace.Enable();
        }
        #endregion
    }
}
