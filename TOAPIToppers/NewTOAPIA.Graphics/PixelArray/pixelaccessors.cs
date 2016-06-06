

namespace NewTOAPIA.Graphics
{
    using System;
    using System.Runtime.InteropServices;

    #region BGRb
    unsafe public class PixelAccessorBGRb : PixelAccessor
    {
        public PixelAccessorBGRb(int width, int height, PixmapOrientation orientation, IntPtr pixelPointer, int bytesPerRow)
            : base(width, height, PixelType.BGRb, bytesPerRow, orientation, pixelPointer)
        {
        }

        public BGRb RetrievePixel(int x, int y)
        {
            IntPtr pixelPtr = GetPixelPointer(x, y);
            BGRb result = (BGRb)Marshal.PtrToStructure(pixelPtr, typeof(BGRb));

            return result;
        }

        public void AssignPixel(int x, int y, BGRb aPixel)
        {
            BGRb* pixelPtr = (BGRb*)GetPixelPointer(x, y).ToPointer();
            pixelPtr->SetBytes(aPixel.GetBytes());
        }
    }
    #endregion

    #region BGRAb
    public class PixelAccessorBGRAb : PixelAccessor
    {
        public PixelAccessorBGRAb(int width, int height, PixmapOrientation orientation, IntPtr pixelPointer, int bytesPerRow)
            : base(width, height, PixelType.BGRAb, bytesPerRow, orientation, pixelPointer)
        {
        }
    }
    #endregion

    #region Lumb
    public class PixelAccessorLumb : PixelAccessor
    {
        public PixelAccessorLumb(int width, int height, PixmapOrientation orientation, IntPtr pixelPointer)
            : base(width, height, PixelType.Lumb, width, orientation, pixelPointer)
        {
        }

    }
    #endregion

}
