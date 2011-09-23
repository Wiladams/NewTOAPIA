
    namespace NewTOAPIA.Drawing
    {
        using System;
        using NewTOAPIA.Graphics;

        public class ImageBuffer : IImage
        {
            public const int OrderB = 0;
            public const int OrderG = 1;
            public const int OrderR = 2;
            public const int OrderA = 3;


            protected int[] m_yTable;
            protected int[] m_xTable;
            private byte[] m_ByteBuffer;
            int m_BufferOffset; // the beggining of the image in this buffer
            int m_BufferFirstPixel; // Pointer to first pixel depending on strideInBytes and image position

            int m_Width;  // Width in pixels
            int m_Height; // Height in pixels
            int m_StrideInBytes; // Number of bytes per row. Can be < 0
            int m_DistanceInBytesBetweenPixelsInclusive;
            int m_BitDepth;
            Vector2D m_OriginOffset = new Vector2D(0, 0);

            private IBlender m_Blender;

            const int base_mask = 255;

            public ImageBuffer()
            {
            }

            public ImageBuffer(IBlender blender)
            {
                SetBlender(blender);
            }

            public ImageBuffer(IImage sourceImage, IBlender blender)
            {
                SetDimmensionAndFormat(sourceImage.Width(), sourceImage.Height(), sourceImage.StrideInBytes(), sourceImage.BitDepth, sourceImage.GetDistanceBetweenPixelsInclusive());
                int offset;
                byte[] buffer = sourceImage.GetBuffer(out offset);
                byte[] newBuffer = new byte[buffer.Length];
                agg_basics.memcpy(newBuffer, offset, buffer, offset, buffer.Length - offset);
                SetBuffer(newBuffer, offset);
                SetBlender(blender);
            }

            public ImageBuffer(int width, int height, int bitsPerPixel, IBlender blender)
            {
                Allocate(width, height, width * (bitsPerPixel / 8), bitsPerPixel);
                SetBlender(blender);
            }

#if false
        public ImageBuffer(IImage image, IBlender blender, GammaLookUpTable gammaTable)
        {
            unsafe
            {
                AttachBuffer(image.GetBuffer(), image.Width(), image.Height(), image.StrideInBytes(), image.BitDepth, image.GetDistanceBetweenPixelsInclusive());
            }

            SetBlender(blender);
        }
#endif

            public ImageBuffer(IImage sourceImageToCopy, IBlender blender, int distanceBetweenPixelsInclusive, int bufferOffset, int bitsPerPixel)
            {
                SetDimmensionAndFormat(sourceImageToCopy.Width(), sourceImageToCopy.Height(), sourceImageToCopy.StrideInBytes(), bitsPerPixel, distanceBetweenPixelsInclusive);
                int offset;
                byte[] buffer = sourceImageToCopy.GetBuffer(out offset);
                byte[] newBuffer = new byte[buffer.Length];
                agg_basics.memcpy(newBuffer, offset, buffer, offset, buffer.Length - offset);
                SetBuffer(newBuffer, offset + bufferOffset);
                SetBlender(blender);
            }

            public void AttachBuffer(byte[] buffer, int bufferOffset, int width, int height, int strideInBytes, int bitDepth, int distanceInBytesBetweenPixelsInclusive)
            {
                m_ByteBuffer = null;
                SetDimmensionAndFormat(width, height, strideInBytes, bitDepth, distanceInBytesBetweenPixelsInclusive);
                SetBuffer(buffer, bufferOffset);
            }

            public void Attach(IImage sourceImage, IBlender blender, int distanceBetweenPixelsInclusive, int bufferOffset, int bitsPerPixel)
            {
                SetDimmensionAndFormat(sourceImage.Width(), sourceImage.Height(), sourceImage.StrideInBytes(), bitsPerPixel, distanceBetweenPixelsInclusive);
                int offset;
                byte[] buffer = sourceImage.GetBuffer(out offset);
                SetBuffer(buffer, offset + bufferOffset);
                SetBlender(blender);
            }

            public void Attach(IImage sourceImage, IBlender blender)
            {
                Attach(sourceImage, blender, sourceImage.GetDistanceBetweenPixelsInclusive(), 0, sourceImage.BitDepth);
            }

            public bool Attach(IImage sourceImage, int x1, int y1, int x2, int y2)
            {
                m_ByteBuffer = null;
                DettachBuffer();

                if (x1 > x2 || y1 > y2)
                {
                    throw new Exception("You need to have your x1 and y1 be the lower left corner of your sub image.");
                }
                RectangleI boundsRect = new RectangleI(x1, y1, x2, y2);
                if (boundsRect.Clip(new RectangleI(0, 0, (int)sourceImage.Width() - 1, (int)sourceImage.Height() - 1)))
                {
                    SetDimmensionAndFormat(boundsRect.Width, boundsRect.Height, sourceImage.StrideInBytes(), sourceImage.BitDepth, sourceImage.GetDistanceBetweenPixelsInclusive());
                    int bufferOffset;
                    byte[] buffer = sourceImage.GetPixelPointerXY(boundsRect.x1, boundsRect.y1, out bufferOffset);
                    SetBuffer(buffer, bufferOffset);
                    return true;
                }

                return false;
            }

            public void SetAlpha(byte value)
            {
                if (BitDepth != 32)
                {
                    throw new Exception("You don't have alpha channel to set.  Your image has a bit depth of " + BitDepth.ToString() + ".");
                }
                int numPixels = Width() * Height();
                int offset;
                byte[] buffer = GetBuffer(out offset);
                for (int i = 0; i < numPixels; i++)
                {
                    buffer[offset + i * 4 + 3] = value;
                }
            }

            private void Deallocate()
            {
                m_ByteBuffer = null;
                SetDimmensionAndFormat(0, 0, 0, 32, 4);
            }

            public void Allocate(int inWidth, int inHeight, int inScanWidthInBytes, int bitsPerPixel)
            {
                if (bitsPerPixel != 32 && bitsPerPixel != 24 && bitsPerPixel != 8)
                {
                    throw new Exception("Unsupported bits per pixel.");
                }
                if (inScanWidthInBytes < inWidth * (bitsPerPixel / 8))
                {
                    throw new Exception("Your scan width is not big enough to hold your width and height.");
                }
                SetDimmensionAndFormat(inWidth, inHeight, inScanWidthInBytes, bitsPerPixel, bitsPerPixel / 8);

                m_ByteBuffer = new byte[m_StrideInBytes * m_Height];

                SetUpLookupTables();
            }

            public RendererBase NewRenderer()
            {
                ImageRenderer imageRenderer = new ImageRenderer(this);

                imageRenderer.Rasterizer.SetVectorClipBox(0, 0, Width(), Height());

                return imageRenderer;
            }

            public void CopyFrom(IImage sourceImage)
            {
                int height = Height();
                if (sourceImage.Height() < height)
                {
                    height = sourceImage.Height();
                }

                int StrideABS = StrideInBytesAbs();
                if (sourceImage.StrideInBytesAbs() < StrideABS)
                {
                    StrideABS = sourceImage.StrideInBytesAbs();
                }

                int y;
                for (y = 0; y < height; y++)
                {
                    int destOffset;
                    byte[] destBuffer = GetPixelPointerY(y, out destOffset);
                    int sourceOffset;
                    byte[] sourceBuffer = sourceImage.GetPixelPointerY(y, out sourceOffset);

                    agg_basics.memcpy(destBuffer, destOffset, sourceBuffer, sourceOffset, StrideInBytesAbs());
                }
            }

            public void CopyFrom(IImage sourceImage, int xdst, int ydst, int xsrc, int ysrc, int len)
            {
                if (BitDepth != sourceImage.BitDepth
                    || GetDistanceBetweenPixelsInclusive() != BitDepth / 8
                    || sourceImage.GetDistanceBetweenPixelsInclusive() != sourceImage.BitDepth / 8)
                {
                    throw new Exception("You need to have the same bit depth to copy a length");
                }
                int destOffset;
                byte[] destBuffer = GetPixelPointerXY(xdst, ydst, out destOffset);
                int sourceOffset;
                byte[] sourceBuffer = sourceImage.GetPixelPointerXY(xsrc, ysrc, out sourceOffset);
                agg_basics.memmove(destBuffer, destOffset, sourceBuffer, sourceOffset, len);
            }

            public Vector2D OriginOffset
            {
                get { return m_OriginOffset; }
                set { m_OriginOffset = value; }
            }

            public int Width() { return m_Width; }
            public int Height() { return m_Height; }
            public int StrideInBytes() { return m_StrideInBytes; }
            public int StrideInBytesAbs() { return System.Math.Abs(m_StrideInBytes); }
            public int GetDistanceBetweenPixelsInclusive() { return m_DistanceInBytesBetweenPixelsInclusive; }
            public int BitDepth
            {
                get { return m_BitDepth; }
            }

            public IBlender GetBlender()
            {
                return m_Blender;
            }

            public void SetBlender(IBlender value)
            {
                m_Blender = value;
            }

            private void SetUpLookupTables()
            {
                m_yTable = new int[m_Height];
                for (int i = 0; i < m_Height; i++)
                {
                    m_yTable[i] = i * m_StrideInBytes;
                }

                m_xTable = new int[m_Width];
                for (int i = 0; i < m_Width; i++)
                {
                    m_xTable[i] = i * m_DistanceInBytesBetweenPixelsInclusive;
                }
            }

            public void FlipY()
            {
                m_StrideInBytes *= -1;
                m_BufferFirstPixel = m_BufferOffset;
                if (m_StrideInBytes < 0)
                {
                    int addAmount = -((int)((int)m_Height - 1) * m_StrideInBytes);
                    m_BufferFirstPixel = addAmount + m_BufferOffset;
                }

                SetUpLookupTables();
            }

            public void SetBuffer(byte[] byteBuffer, int bufferOffset)
            {
                if (byteBuffer.Length < m_Height * m_StrideInBytes)
                {
                    throw new Exception("Your buffer does not have enough room it it for your height and strideInBytes.");
                }
                m_ByteBuffer = byteBuffer;
                m_BufferOffset = m_BufferFirstPixel = bufferOffset;
                if (m_StrideInBytes < 0)
                {
                    int addAmount = -((int)((int)m_Height - 1) * m_StrideInBytes);
                    m_BufferFirstPixel = addAmount + m_BufferOffset;
                }
                SetUpLookupTables();
            }

            private void SetDimmensionAndFormat(int width, int height, int strideInBytes, int bitDepth, int distanceInBytesBetweenPixelsInclusive)
            {
                if (m_ByteBuffer != null)
                {
                    throw new Exception("You already have a buffer set. You need to set dimmensoins before the buffer.  You may need to clear the buffer first.");
                }
                m_Width = width;
                m_Height = height;
                m_StrideInBytes = strideInBytes;
                m_BitDepth = bitDepth;
                if (distanceInBytesBetweenPixelsInclusive > 4)
                {
                    throw new System.Exception("It looks like you are passing bits per pixel rather than distance in bytes.");
                }
                if (distanceInBytesBetweenPixelsInclusive < (bitDepth / 8))
                {
                    throw new Exception("You do not have enough room between pixels to support your bit depth.");
                }
                m_DistanceInBytesBetweenPixelsInclusive = distanceInBytesBetweenPixelsInclusive;
                if (strideInBytes < distanceInBytesBetweenPixelsInclusive * width)
                {
                    throw new Exception("You do not have enough strideInBytes to hold the width and pixel distance you have described.");
                }
            }

            public void DettachBuffer()
            {
                m_ByteBuffer = null;
                m_Width = m_Height = m_StrideInBytes = m_DistanceInBytesBetweenPixelsInclusive = 0;
            }

            public byte[] GetBuffer(out int bufferOffset)
            {
                bufferOffset = m_BufferOffset;
                return m_ByteBuffer;
            }

            public byte[] GetPixelPointerY(int y, out int bufferOffset)
            {
                bufferOffset = m_BufferFirstPixel + m_yTable[y];
                //bufferOffset = GetBufferOffsetXY(0, y);
                return m_ByteBuffer;
            }

            public byte[] GetPixelPointerXY(int x, int y, out int bufferOffset)
            {
                bufferOffset = GetBufferOffsetXY(x, y);
                return m_ByteBuffer;
            }

            public RGBA_Bytes pixel(int x, int y)
            {
                return m_Blender.PixelToColorRGBA_Bytes(m_ByteBuffer, GetBufferOffsetXY(x, y));
            }

            public int GetBufferOffsetXY(int x, int y)
            {
                return m_BufferFirstPixel + m_yTable[y] + m_xTable[x];
                //return m_BufferOffset + y * m_StrideInBytes + x * m_DistanceInBytesBetweenPixelsInclusive;
            }

            public void copy_pixel(int x, int y, byte[] c, int ByteOffset)
            {
                throw new System.NotImplementedException();
                //byte* p = GetPixelPointerXY(x, y);
                //((int*)p)[0] = ((int*)c)[0];
                //p[OrderR] = c.r;
                //p[OrderG] = c.g;
                //p[OrderB] = c.b;
                //p[OrderA] = c.a;
            }

            public void BlendPixel(int x, int y, RGBA_Bytes c, byte cover)
            {
                throw new System.NotImplementedException();
                /*
                cob_type::copy_or_blend_pix(
                    (value_type*)m_rbuf->row_ptr(x, y, 1)  + x + x + x, 
                    c.r, c.g, c.b, c.a, 
                    cover);*/
            }

            public void SetPixelFromColor(byte[] destPixel, IColorType c)
            {
                throw new System.NotImplementedException();
                //pDestPixel[OrderR] = (byte)c.R_Byte;
                //pDestPixel[OrderG] = (byte)c.G_Byte;
                //pDestPixel[OrderB] = (byte)c.B_Byte;
            }

            public void copy_hline(int x, int y, int len, RGBA_Bytes sourceColor)
            {
                int bufferOffset;
                byte[] buffer = GetPixelPointerXY(x, y, out bufferOffset);

                m_Blender.CopyPixels(buffer, bufferOffset, sourceColor, len);
            }

            public void copy_vline(int x, int y, int len, RGBA_Bytes sourceColor)
            {
                throw new NotImplementedException();
#if false
            int scanWidth = StrideInBytes();
            byte* pDestBuffer = GetPixelPointerXY(x, y);
            do
            {
                m_Blender.CopyPixel(pDestBuffer, sourceColor);
                pDestBuffer = &pDestBuffer[scanWidth];
            }
            while (--len != 0);
#endif
            }


            public void blend_hline(int x1, int y, int x2, RGBA_Bytes sourceColor, byte cover)
            {
                if (sourceColor.m_A != 0)
                {
                    int len = x2 - x1 + 1;

                    int bufferOffset;
                    byte[] buffer = GetPixelPointerXY(x1, y, out bufferOffset);

                    int alpha = (((int)(sourceColor.m_A) * (cover + 1)) >> 8);
                    if (alpha == base_mask)
                    {
                        m_Blender.CopyPixels(buffer, bufferOffset, sourceColor, len);
                    }
                    else
                    {
                        do
                        {
                            m_Blender.BlendPixel(buffer, bufferOffset, new RGBA_Bytes(sourceColor.m_R, sourceColor.m_G, sourceColor.m_B, alpha));
                            bufferOffset += m_DistanceInBytesBetweenPixelsInclusive;
                        }
                        while (--len != 0);
                    }
                }
            }

            public void blend_vline(int x, int y1, int y2, RGBA_Bytes sourceColor, byte cover)
            {
                throw new NotImplementedException();
#if false
            int ScanWidth = StrideInBytes();
            if (sourceColor.m_A != 0)
            {
                unsafe
                {
                    int len = y2 - y1 + 1;
                    byte* p = GetPixelPointerXY(x, y1);
                    sourceColor.m_A = (byte)(((int)(sourceColor.m_A) * (cover + 1)) >> 8);
                    if (sourceColor.m_A == base_mask)
                    {
                        byte cr = sourceColor.m_R;
                        byte cg = sourceColor.m_G;
                        byte cb = sourceColor.m_B;
                        do
                        {
                            m_Blender.CopyPixel(p, sourceColor);
                            p = &p[ScanWidth];
                        }
                        while (--len != 0);
                    }
                    else
                    {
                        if (cover == 255)
                        {
                            do
                            {
                                m_Blender.BlendPixel(p, sourceColor);
                                p = &p[ScanWidth];
                            }
                            while (--len != 0);
                        }
                        else
                        {
                            do
                            {
                                m_Blender.BlendPixel(p, sourceColor);
                                p = &p[ScanWidth];
                            }
                            while (--len != 0);
                        }
                    }
                }
            }
#endif
            }

            public void blend_solid_hspan(int x, int y, int len, RGBA_Bytes sourceColor, byte[] covers, int coversIndex)
            {
                int colorAlpha = sourceColor.m_A;
                if (colorAlpha != 0)
                {
                    unchecked
                    {
                        int bufferOffset;
                        byte[] buffer = GetPixelPointerXY(x, y, out bufferOffset);

                        do
                        {
                            int alpha = ((colorAlpha) * ((covers[coversIndex]) + 1)) >> 8;
                            if (alpha == base_mask)
                            {
                                m_Blender.CopyPixels(buffer, bufferOffset, sourceColor, 1);
                            }
                            else
                            {
                                m_Blender.BlendPixel(buffer, bufferOffset, new RGBA_Bytes(sourceColor.m_R, sourceColor.m_G, sourceColor.m_B, alpha));
                            }
                            bufferOffset += m_DistanceInBytesBetweenPixelsInclusive;
                            coversIndex++;
                        }
                        while (--len != 0);
                    }
                }
            }

            public void blend_solid_vspan(int x, int y, int len, RGBA_Bytes c, byte[] covers, int coversIndex)
            {
                throw new NotImplementedException();
#if false
            if (sourceColor.m_A != 0)
            {
                int ScanWidth = StrideInBytes();
                unchecked
                {
                    byte* p = GetPixelPointerXY(x, y);
                    do
                    {
                        byte oldAlpha = sourceColor.m_A;
                        sourceColor.m_A = (byte)(((int)(sourceColor.m_A) * ((int)(*covers++) + 1)) >> 8);
                        if (sourceColor.m_A == base_mask)
                        {
                            m_Blender.CopyPixel(p, sourceColor);
                        }
                        else
                        {
                            m_Blender.BlendPixel(p, sourceColor);
                        }
                        p = &p[ScanWidth];
                        sourceColor.m_A = oldAlpha;
                    }
                    while (--len != 0);
                }
            }
#endif
            }

            public void copy_color_hspan(int x, int y, int len, RGBA_Bytes[] colors, int colorsIndex)
            {
                int bufferOffset = GetBufferOffsetXY(x, y);

                do
                {
                    m_Blender.CopyPixels(m_ByteBuffer, bufferOffset, colors[colorsIndex], 1);

                    ++colorsIndex;
                    bufferOffset += m_DistanceInBytesBetweenPixelsInclusive;
                }
                while (--len != 0);
            }

            public void copy_color_vspan(int x, int y, int len, RGBA_Bytes[] colors, int colorsIndex)
            {
                int bufferOffset = GetBufferOffsetXY(x, y);

                do
                {
                    m_Blender.CopyPixels(m_ByteBuffer, bufferOffset, colors[colorsIndex], 1);

                    ++colorsIndex;
                    bufferOffset += m_StrideInBytes;
                }
                while (--len != 0);
            }

            public void blend_color_hspan(int x, int y, int len, RGBA_Bytes[] colors, int colorsIndex, byte[] covers, int coversIndex, bool firstCoverForAll)
            {
                int bufferOffset = GetBufferOffsetXY(x, y);
                m_Blender.BlendPixels(m_ByteBuffer, bufferOffset, colors, colorsIndex, covers, coversIndex, firstCoverForAll, len);
            }

            public void blend_color_vspan(int x, int y, int len, RGBA_Bytes[] colors, int colorsIndex, byte[] covers, int coversIndex, bool firstCoverForAll)
            {
                int bufferOffset = GetBufferOffsetXY(x, y);

                int ScanWidth = StrideInBytesAbs();
                if (!firstCoverForAll)
                {
                    do
                    {
                        DoCopyOrBlend.BasedOnAlphaAndCover(m_Blender, m_ByteBuffer, bufferOffset, colors[colorsIndex], covers[coversIndex++]);
                        bufferOffset += ScanWidth;
                        ++colorsIndex;
                    }
                    while (--len != 0);
                }
                else
                {
                    if (covers[coversIndex] == 255)
                    {
                        do
                        {
                            DoCopyOrBlend.BasedOnAlpha(m_Blender, m_ByteBuffer, bufferOffset, colors[colorsIndex]);
                            bufferOffset += ScanWidth;
                            ++colorsIndex;
                        }
                        while (--len != 0);
                    }
                    else
                    {
                        do
                        {

                            DoCopyOrBlend.BasedOnAlphaAndCover(m_Blender, m_ByteBuffer, bufferOffset, colors[colorsIndex], covers[coversIndex]);
                            bufferOffset += ScanWidth;
                            ++colorsIndex;
                        }
                        while (--len != 0);
                    }
                }
            }

            public void apply_gamma_inv(GammaLookUpTable g)
            {
                throw new System.NotImplementedException();
                //for_each_pixel(apply_gamma_inv_rgba<color_type, order_type, GammaLut>(g));
            }

            private bool IsPixelVisible(int x, int y)
            {
                RGBA_Bytes pixelValue = GetBlender().PixelToColorRGBA_Bytes(m_ByteBuffer, GetBufferOffsetXY(x, y));
                return (pixelValue.A_Byte != 0 || pixelValue.R_Byte != 0 || pixelValue.G_Byte != 0 || pixelValue.B_Byte != 0);
            }

            public void GetVisibleBounds(out RectangleI visibleBounds)
            {
                visibleBounds = new RectangleI(0, 0, Width(), Height());

                // trim the bottom
                bool aPixelsIsVisible = false;
                for (int y = 0; y < m_Height; y++)
                {
                    for (int x = 0; x < m_Width; x++)
                    {
                        if (IsPixelVisible(x, y))
                        {
                            visibleBounds.Bottom = y;
                            y = m_Height;
                            x = m_Width;
                            aPixelsIsVisible = true;
                        }
                    }
                }

                // if we don't run into any pixels set for the top trim than there are no pixels set at all
                if (!aPixelsIsVisible)
                {
                    visibleBounds.SetRect(0, 0, 0, 0);
                    return;
                }

                // trim the bottom
                for (int y = m_Height - 1; y >= 0; y--)
                {
                    for (int x = 0; x < m_Width; x++)
                    {
                        if (IsPixelVisible(x, y))
                        {
                            visibleBounds.Top = y + 1;
                            y = -1;
                            x = m_Width;
                        }
                    }
                }

                // trim the left
                for (int x = 0; x < m_Width; x++)
                {
                    for (int y = 0; y < m_Height; y++)
                    {
                        if (IsPixelVisible(x, y))
                        {
                            visibleBounds.Left = x;
                            y = m_Height;
                            x = m_Width;
                        }
                    }
                }

                // trim the right
                for (int x = m_Width - 1; x >= 0; x--)
                {
                    for (int y = 0; y < m_Height; y++)
                    {
                        if (IsPixelVisible(x, y))
                        {
                            visibleBounds.Right = x + 1;
                            y = m_Height;
                            x = -1;
                        }
                    }
                }
            }

            public void CropToVisible()
            {
                Vector2D OldOriginOffset = OriginOffset;

                //Move the HotSpot to 0, 0 so PPoint will work the way we want
                OriginOffset = new Vector2D(0, 0);

                RectangleI visibleBounds;
                GetVisibleBounds(out visibleBounds);

                if (visibleBounds.Width == Width()
                    && visibleBounds.Height == Height())
                {
                    OriginOffset = OldOriginOffset;
                    return;
                }

                // check if the Not0Rect has any size
                if (visibleBounds.Width > 0)
                {
                    ImageBuffer TempImage = new ImageBuffer();

                    // set TempImage equal to the Not0Rect
                    TempImage.Initialize(this, visibleBounds);

                    // set the frame equal to the TempImage
                    Initialize(TempImage);

                    OriginOffset = new Vector2D(-visibleBounds.Left + OldOriginOffset.x, -visibleBounds.Bottom + OldOriginOffset.y);
                }
                else
                {
                    Deallocate();
                }
            }

            public RectangleI GetBoundingRect()
            {
                RectangleI boundingRect = new RectangleI(0, 0, Width(), Height());
                boundingRect.Offset((int)OriginOffset.x, (int)OriginOffset.y);
                return boundingRect;
            }

            private void Initialize(ImageBuffer sourceImage)
            {
                RectangleI sourceBoundingRect = sourceImage.GetBoundingRect();

                Initialize(sourceImage, sourceBoundingRect);
                OriginOffset = sourceImage.OriginOffset;
            }

            private void Initialize(ImageBuffer sourceImage, RectangleI boundsToCopyFrom)
            {
                if (sourceImage == this)
                {
                    throw new Exception("We do not create a temp buffer for this to work.  You must have a source distinct from the dest.");
                }
                Deallocate();
                Allocate(boundsToCopyFrom.Width, boundsToCopyFrom.Height, boundsToCopyFrom.Width * sourceImage.BitDepth / 8, sourceImage.BitDepth);
                SetBlender(sourceImage.GetBlender());

                if (m_Width != 0 && m_Height != 0)
                {
                    RectangleI DestRect = new RectangleI(0, 0, boundsToCopyFrom.Width, boundsToCopyFrom.Height);
                    RectangleI AbsoluteSourceRect = boundsToCopyFrom;
                    // The first thing we need to do is make sure the frame is cleared. LBB [3/15/2004]
                    RendererBase rendererToDrawWith = NewRenderer();
                    rendererToDrawWith.Clear(new RGBA_Bytes(0, 0, 0, 0));

                    int x = -boundsToCopyFrom.Left - (int)sourceImage.OriginOffset.x;
                    int y = -boundsToCopyFrom.Bottom - (int)sourceImage.OriginOffset.y;

                    rendererToDrawWith.Render(sourceImage, x, y, 0, 1, 1, new RGBA_Bytes(255, 255, 255, 255), true, true);
                }
            }
        }

        public static class DoCopyOrBlend
        {
            const byte base_mask = 255;

            public static void BasedOnAlpha(IBlender Blender, byte[] destBuffer, int bufferOffset, RGBA_Bytes sourceColor)
            {
                //if (sourceColor.m_A != 0)
                {
#if false // we blend regardless of the alpha so that we can get Light Opacity working (used this way we have addative and faster blending in one blender) LBB
                if (sourceColor.m_A == base_mask)
                {
                    Blender.CopyPixel(pDestBuffer, sourceColor);
                }
                else
#endif
                    {
                        Blender.BlendPixel(destBuffer, bufferOffset, sourceColor);
                    }
                }
            }

            public static void BasedOnAlphaAndCover(IBlender Blender, byte[] destBuffer, int bufferOffset, RGBA_Bytes sourceColor, int cover)
            {
                if (cover == 255)
                {
                    BasedOnAlpha(Blender, destBuffer, bufferOffset, sourceColor);
                }
                else
                {
                    //if (sourceColor.m_A != 0)
                    {
                        sourceColor.m_A = (byte)((sourceColor.m_A * (cover + 1)) >> 8);
#if false // we blend regardless of the alpha so that we can get Light Opacity working (used this way we have addative and faster blending in one blender) LBB
                    if (sourceColor.m_A == base_mask)
                    {
                        Blender.CopyPixel(pDestBuffer, sourceColor);
                    }
                    else
#endif
                        {
                            Blender.BlendPixel(destBuffer, bufferOffset, sourceColor);
                        }
                    }
                }
            }
        };
    }
