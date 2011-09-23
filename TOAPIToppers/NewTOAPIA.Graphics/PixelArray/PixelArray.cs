using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewTOAPIA.Graphics
{
    using System.Runtime.InteropServices;

    public class PixelArray : PixelRectangleInfo, IPixelArray, IEnumerable<IPixel>
    {
        public PixelAccessor Accessor { get; protected set; }
        public byte[] fPixelBytes;
        public IntPtr Pixels { get; set; }

        #region Constructor

        public PixelArray(int width, int height, IPixelInformation pInfo, PixmapOrientation orient, int alignment)
            :base(width, height, pInfo.Layout, pInfo.ComponentType, orient, alignment)
        {
            fPixelBytes = new byte[BytesPerRow * height];
            LockPixelBytes();
        }

        public PixelArray(int width, int height, IPixelInformation pInfo, PixmapOrientation orient, int alignment, byte[] pixelData)
            : base(width, height, pInfo.Layout, pInfo.ComponentType, orient, alignment)
        {
            fPixelBytes = pixelData;
        }
        #endregion

        public int Dimension { get { return PixelInformation.GetComponentsPerLayout(Layout); } }

        public byte[] PixelBytes
        {
            get
            {
                return fPixelBytes;
            }
        }

        public IntPtr LockPixelBytes()
        {
            GCHandle dataHandle = GCHandle.Alloc(fPixelBytes, GCHandleType.Pinned);
            Pixels = (IntPtr)dataHandle.AddrOfPinnedObject();
            return Pixels;
        }

        #region IColorAccessor
        public virtual ColorRGBA GetColor(int x, int y)
        {
            // Calculate byte offset
            int pixelOffset = GetPixelByteOffset(x, y);

            // Create color from bytes
            ColorRGBA color = Pixel.GetColor(GetPixelInformation(), PixelBytes, pixelOffset);

            return color;
        }

        public virtual void SetColor(int x, int y, ColorRGBA color)
        {
            // Find the byte offset for the pixel
            int pixelOffset = GetPixelByteOffset(x, y);

            byte[] colorBytes = Pixel.GetBytesForColor(GetPixelInformation().Layout, GetPixelInformation().ComponentType,
                color);

            // copy the color bytes into the array
            Array.Copy(colorBytes, 0, PixelBytes, pixelOffset, colorBytes.Length);
        }
        #endregion

        #region IPixelAccessor
        public virtual IPixel GetPixel(int x, int y)
        {
            Pixel newPixel = new Pixel(GetPixelInformation(), GetPixelBytes(x, y));

            return newPixel;
        }

        public virtual byte[] GetPixelBytes(int x, int y)
        {
            // Find the byte offset for the pixel
            int pixelOffset = GetPixelByteOffset(x, y);
            int bpp = GetPixelInformation().BytesPerPixel;

            byte[] pixelBytes = new byte[bpp];
            Array.Copy(PixelBytes, pixelOffset, pixelBytes, 0, bpp);

            return pixelBytes;
        }

        public virtual void SetPixel(int x, int y, IPixel pel)
        {
            // If the pixel types don't match, then 
            PixelInformation pelInfo = new PixelInformation(pel.Layout, pel.ComponentType);
            if (GetPixelInformation().Layout == pelInfo.layout && GetPixelInformation().componentType == pel.ComponentType)
            {
                SetPixelBytes(x, y, pel.GetBytes());
                return;
            }

            // If the pixel types don't match, then we have to go through
            // color instead of just copying the pixel directly.
            SetColor(x, y, Pixel.GetColor(pelInfo, pel.GetBytes()));
        }

        public virtual void SetPixelBytes(int x, int y, byte[] pixelData)
        {
            // Find the byte offset for the pixel
            int pixelOffset = GetPixelByteOffset(x, y);

            // copy the color bytes into the array
            Array.Copy(pixelData, 0, PixelBytes, pixelOffset, pixelData.Length);
        }
        #endregion

        // By providing an IEnumerator implementation,
        // we can now use the foreach() construct.
        // This is useful when applying whole image operators
        public IEnumerator<IPixel> GetEnumerator()
        {
            for (int row = 0; row < Height; row++)
            {
                for (int column = 0; column < Width; column++)
                {
                    yield return GetPixel(column, row);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
