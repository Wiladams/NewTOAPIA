using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NewTOAPIA.Graphics
{

    public class PixelArray<T> : PixelArray
        where T : IPixelInformation, new()
    {
        #region Constructors
        //public PixelArray(IPixelArray srcArray)
        //    : this(srcArray.Width, srcArray.Height, PixmapOrientation.TopToBottom, new T())
        //{
        //    // just copy color row by column
        //    for (int row = 0; row < Height; row++)
        //        for (int column = 0; column < Width; column++)
        //        {
        //            SetColor(column, row, srcArray.GetColor(column, row));
        //        }
        //}

        //public PixelArray(PixelAccessor<T> srcArray)
        //    : this(srcArray.Width, srcArray.Height, PixmapOrientation.TopToBottom, new T())
        //{
        //    // just copy row by column
        //    for (int row = 0; row < Height; row++)
        //        for (int column = 0; column < Width; column++)
        //        {
        //            AssignPixel(column, row, srcArray.RetrievePixel(column, row));
        //        }
        //}

        //public PixelArray(PixelArray<T> srcArray)
        //    : this(srcArray.Width, srcArray.Height, srcArray.Orientation, new T())
        //{
        //    // just copy row by column
        //    for (int row = 0; row < Height; row++)
        //        for (int column = 0; column < Width; column++)
        //        {
        //            AssignPixel(column, row, srcArray.RetrievePixel(column, row));
        //        }
        //}

        public PixelArray(int width, int height)
            : this(width, height, PixmapOrientation.TopToBottom)
        {
        }

        public PixelArray(int width, int height, byte[] pixelData)
            :base(width, height, new T(), PixmapOrientation.TopToBottom, 1, pixelData)
        {
        }

        public PixelArray(int width, int height, PixmapOrientation orient)
            :base(width, height, new T(), orient, 1)

        {
            //// Pin the array in memory, and get a data pointer to it
            //IntPtr dataPtr;

            ////unsafe
            ////{
            //    GCHandle dataHandle = GCHandle.Alloc(fTypedPixelArray, GCHandleType.Pinned);
            //    dataPtr = (IntPtr)dataHandle.AddrOfPinnedObject();
            ////}

            //Pixels = dataPtr;
        }

        #endregion

    }
}

