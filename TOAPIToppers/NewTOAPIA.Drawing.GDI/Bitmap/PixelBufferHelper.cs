using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

using TOAPI.GDI32;
using TOAPI.Types;

namespace NewTOAPIA.Drawing.GDI
{
    using NewTOAPIA.Graphics;

    public class PixelBufferHelper
    {
        #region Static Construction Helpers
        public static GDIDIBSection CreateFromFile(string filename)
        {
            Image image = Image.FromFile(filename);

            return CreateFromImage(image);
        }

        public static GDIDIBSection CreateFromResource(string resourceName)
        {
            System.IO.Stream stream = AppDomain.CurrentDomain.GetType().Assembly.GetManifestResourceStream(resourceName);
            if (null == stream)
                return null;

            Image image = Image.FromStream(stream);

            return CreateFromImage(image);
        }

        public static GDIDIBSection CreateFromImage(Image im)
        {
            Bitmap bm = new Bitmap(im);
            return CreatePixelBufferFromBitmap(bm);
        }
        #endregion

        public static GDIDIBSection CreatePixelBufferFromBitmap(Bitmap bitmap)
        {
            //throw new NotImplementedException("CreatePixelBufferFromBitmap, needs to be reconsidered");

            int width = bitmap.Width;
            int height = bitmap.Height;

            // 1. Create basic pixel buffer object
            GDIDIBSection aPixBuffer = new GDIDIBSection(width, height);

            // 2. copy the bits into the new bitmap
            // Use the accessor on the dib section
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Set the pixel
                    //aPixBuffer.SetColor(x, y, bitmap.GetPixel(x, y));
                }
            }

            return aPixBuffer;
        }

        public static PixelArray<BGRAb> CreatePixelArrayFromBitmap(Bitmap bitmap)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;

            int copyWidth = width;
            int copyHeight = height;

            // 1. Create basic pixel buffer object
            PixelArray<BGRAb> pixArray = new PixelArray<BGRAb>(width, height);

            // 2. copy the bits into the new pixelArray

            //unsafe
            {

                BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), 
                    ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                PixelAccessorBGRAb srcAccess = new PixelAccessorBGRAb(bitmap.Width, bitmap.Height, PixmapOrientation.TopToBottom, bitmapData.Scan0, bitmapData.Stride);

                for (int y = 0; y < bitmap.Height; y++)
                {
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        pixArray.SetPixel(x, y, srcAccess.GetPixel(x, y));
                    }
                }

                bitmap.UnlockBits(bitmapData);
            }

            return pixArray;
        }

        /// <summary>
        /// This routine will convert an implementer of IPixelArray into 
        /// a System.Drawing.Bitmap object.  It is the opposite of 
        /// CreatePixelArrayFromBitmap().
        /// 
        /// 
        /// </summary>
        /// <param name="pixArray">A implementation of the IPixelArray interface.  This must be one of the 32-bit component types.</param>
        /// <returns></returns>
        public static Bitmap CreateBitmapFromPixelArray(IPixelArray accessor)
        {
            if (null == accessor)
                return null;

            int width = accessor.Width;
            int height = accessor.Height;

            if ((width <= 0) || (height <= 0))
            {
                return (null);
            }

            // Create a Bitmap object of the appropriate dimensions
            Bitmap bitmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            if (null == bitmap)
            {
                return (null);
            }

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    ColorRGBA srcColor = accessor.GetColor(x, y);
                    srcColor.A = 1.0f;
                    Color newColor = Color.FromArgb((int)srcColor.RGB);
                    bitmap.SetPixel(x, y, newColor);
                }
            }

            return (bitmap);
        }

        //public static Bitmap CreateBitmapFromPixelAccessor(PixelAccessorBGRAb srcAccess)
        //{
        //    if (null == pixArray)
        //        return null;

        //    int width = pixArray.Width;
        //    int height = pixArray.Height;

        //    if ((width <= 0) || (height <= 0) || (IntPtr.Zero == pixArray.Pixels))
        //    {
        //        return (null);
        //    }

        //    // Create a Bitmap object of the appropriate dimensions
        //    Bitmap bitmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

        //    if (null == bitmap)
        //    {
        //        return (null);
        //    }

        //    for (int y = 0; y < bitmap.Height; y++)
        //    {
        //        for (int x = 0; x < bitmap.Width; x++)
        //        {
        //            ColorRGBA srcColor = pixArray.GetColor(x, y);
        //            srcColor.Alpha = 1.0f;
        //            Color newColor = srcColor;
        //            bitmap.SetPixel(x, y, newColor);
        //        }
        //    }

        //    return (bitmap);
        //}

    }
}
