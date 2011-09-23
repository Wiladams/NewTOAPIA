using System;
using System.Runtime.InteropServices;

namespace NewTOAPIA.Graphics
{
    public class PixelAccessor<T> : PixelAccessor
        where T : IPixel, new()
    {
        #region Constructors
        public PixelAccessor(int width, int height, int bytesPerRow, PixmapOrientation orientation, IntPtr pixelPointer)
            : base(width, height, new T(), bytesPerRow, orientation, pixelPointer)
        {
        }
        #endregion

        public T RetrievePixel(int x, int y)
        {
            IntPtr pixelPtr = GetPixelPointer(x, y);
            T result = (T)Marshal.PtrToStructure(pixelPtr, typeof(BGRb));

            return result;
        }

        public void AssignPixel(int x, int y, T aPixel)
        {
            SetPixel(x, y, aPixel);

            //T* pixelPtr = (T*)GetPixelPointer(x, y).ToPointer();
            //pixelPtr->SetBytes(aPixel.GetBytes());
        }
    }
}