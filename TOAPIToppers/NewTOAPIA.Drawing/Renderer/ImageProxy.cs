//----------------------------------------------------------------------------
// Anti-Grain Geometry - Version 2.4
// Copyright (C) 2002-2005 Maxim Shemanarev (http://www.antigrain.com)
//
// C# Port port by: Lars Brubaker
//                  larsbrubaker@gmail.com
// Copyright (C) 2007
//
// Permission to copy, use, modify, sell and distribute this software 
// is granted provided this copyright notice appears in all copies. 
// This software is provided "as is" without express or implied
// warranty, and with no claim as to its suitability for any purpose.
//
//----------------------------------------------------------------------------
// Contact: mcseem@antigrain.com
//          mcseemagg@yahoo.com
//          http://www.antigrain.com
//----------------------------------------------------------------------------

namespace NewTOAPIA.Drawing
{
    using System;
    using NewTOAPIA.Graphics;

    public abstract class ImageProxy : IImage
    {
        protected IImage m_LinkedImage;

        public ImageProxy(IImage linkedImage)
        {
            m_LinkedImage = linkedImage;
        }

#if false
        public unsafe void AttachBuffer(byte* pBuffer, int width, int height, int stride, int bitDepth, int distanceBetweenPixelsInclusive)
        {
            m_LinkedImage.AttachBuffer(pBuffer, width, height, stride, bitDepth, distanceBetweenPixelsInclusive);
        }

        public void DettachBuffer()
        {
            m_LinkedImage.DettachBuffer();
        }
#endif

        public virtual void LinkToImage(IImage linkedImage)
        {
            m_LinkedImage = linkedImage;
        }

        public virtual Vector2D OriginOffset
        {
            get { return m_LinkedImage.OriginOffset; }
            set { m_LinkedImage.OriginOffset = value; }
        }

        public virtual int Width()
        {
            return m_LinkedImage.Width();
        }

        public virtual int Height()
        {
            return m_LinkedImage.Height();
        }

        public virtual int StrideInBytes()
        {
            return m_LinkedImage.StrideInBytes();
        }

        public virtual int StrideInBytesAbs()
        {
            return m_LinkedImage.StrideInBytesAbs();
        }

        public RendererBase NewRenderer()
        {
            return m_LinkedImage.NewRenderer();
        }

        public IBlender GetBlender()
        {
            return m_LinkedImage.GetBlender();
        }

        public void SetBlender(IBlender value)
        {
            m_LinkedImage.SetBlender(value);
        }

        public virtual RGBA_Bytes pixel(int x, int y)
        {
            return m_LinkedImage.pixel(y, x);
        }

        public virtual void copy_pixel(int x, int y, byte[] c, int ByteOffset)
        {
            m_LinkedImage.copy_pixel(x, y, c, ByteOffset);
        }

        public virtual void CopyFrom(IImage sourceRaster)
        {
            m_LinkedImage.CopyFrom(sourceRaster);
        }

        public virtual void CopyFrom(IImage sourceRaster, int xdst, int ydst, int xsrc, int ysrc, int len)
        {
            m_LinkedImage.CopyFrom(sourceRaster, xdst, ydst, xsrc, ysrc, len);
        }

        public virtual void SetPixelFromColor(byte[] p, IColorType sourceColor)
        {
            m_LinkedImage.SetPixelFromColor(p, sourceColor);
        }

        public virtual void BlendPixel(int x, int y, RGBA_Bytes sourceColor, byte cover)
        {
            m_LinkedImage.BlendPixel(x, y, sourceColor, cover);
        }

        public virtual void copy_hline(int x, int y, int len, RGBA_Bytes sourceColor)
        {
            m_LinkedImage.copy_hline(x, y, len, sourceColor);
        }

        public virtual void copy_vline(int x, int y, int len, RGBA_Bytes sourceColor)
        {
            m_LinkedImage.copy_vline(x, y, len, sourceColor);
        }

        public virtual void blend_hline(int x1, int y, int x2, RGBA_Bytes sourceColor, byte cover)
        {
            m_LinkedImage.blend_hline(x1, y, x2, sourceColor, cover);
        }

        public virtual void blend_vline(int x, int y1, int y2, RGBA_Bytes sourceColor, byte cover)
        {
            m_LinkedImage.blend_vline(x, y1, y2, sourceColor, cover);
        }

        public virtual void blend_solid_hspan(int x, int y, int len, RGBA_Bytes c, byte[] covers, int coversIndex)
        {
            m_LinkedImage.blend_solid_hspan(x, y, len, c, covers, coversIndex);
        }

        public virtual void copy_color_hspan(int x, int y, int len, RGBA_Bytes[] colors, int colorIndex)
        {
            m_LinkedImage.copy_color_hspan(x, y, len, colors, colorIndex);
        }

        public virtual void copy_color_vspan(int x, int y, int len, RGBA_Bytes[] colors, int colorIndex)
        {
            m_LinkedImage.copy_color_vspan(x, y, len, colors, colorIndex);
        }

        public virtual void blend_solid_vspan(int x, int y, int len, RGBA_Bytes c, byte[] covers, int coversIndex)
        {
            m_LinkedImage.blend_solid_vspan(x, y, len, c, covers, coversIndex);
        }

        public virtual void blend_color_hspan(int x, int y, int len, RGBA_Bytes[] colors, int colorsIndex, byte[] covers, int coversIndex, bool firstCoverForAll)
        {
            m_LinkedImage.blend_color_hspan(x, y, len, colors, colorsIndex, covers, coversIndex, firstCoverForAll);
        }

        public virtual void blend_color_vspan(int x, int y, int len, RGBA_Bytes[] colors, int colorsIndex, byte[] covers, int coversIndex, bool firstCoverForAll)
        {
            m_LinkedImage.blend_color_vspan(x, y, len, colors, colorsIndex, covers, coversIndex, firstCoverForAll);
        }

        public byte[] GetBuffer(out int bufferOffset)
        {
            return m_LinkedImage.GetBuffer(out bufferOffset);
        }

        public byte[] GetPixelPointerY(int y, out int bufferOffset)
        {
            return m_LinkedImage.GetPixelPointerY(y, out bufferOffset);
        }

        public byte[] GetPixelPointerXY(int x, int y, out int bufferOffset)
        {
            return m_LinkedImage.GetPixelPointerXY(x, y, out bufferOffset);
        }

        public virtual int GetDistanceBetweenPixelsInclusive()
        {
            return m_LinkedImage.GetDistanceBetweenPixelsInclusive();
        }

        public virtual int BitDepth
        {
            get
            {
                return m_LinkedImage.BitDepth;
            }
        }
    }
}
