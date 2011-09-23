
namespace NewTOAPIA.GL
{
    using System;
    using System.Runtime.InteropServices;

    using NewTOAPIA.Graphics;

    public class GLPixelData : IDisposable
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        TextureInternalFormat fTextureInternalFormat;
        PixelLayout fPixelFormat;
        PixelComponentType fPixelType;

        IntPtr fDataPtr;
        GCHandle fDataHandle;

        public GLPixelData(int width, int height, TextureInternalFormat intFormat, PixelLayout format, PixelComponentType pType, IntPtr data)
        {
            Width = width;
            Height = height;
            fPixelFormat = format;
            fPixelType = pType;
            fTextureInternalFormat = intFormat;

            fDataPtr = data;
        }

        public GLPixelData(int width, int height, TextureInternalFormat intFormat, PixelLayout format, PixelComponentType pType, byte[] data)
        {
            Width = width;
            Height = height;
            fPixelFormat = format;
            fPixelType = pType;
            fTextureInternalFormat = intFormat;

            unsafe
            {
                fDataHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
                fDataPtr = (IntPtr)fDataHandle.AddrOfPinnedObject();
            }
        }

        public TextureInternalFormat InternalFormat
        {
            get
            {
                return fTextureInternalFormat;
            }
        }

        public PixelLayout PixelFormat
        {
            get { return fPixelFormat; }
        }

        public PixelComponentType PixelType
        {
            get { return fPixelType; }
        }

        #region IPixelArray
        public IntPtr Pixels
        {
            get { return fDataPtr; }
        }
        #endregion

        public virtual void Dispose()
        {
            if (fDataHandle != null)
                fDataHandle.Free();
        }
    }
}

