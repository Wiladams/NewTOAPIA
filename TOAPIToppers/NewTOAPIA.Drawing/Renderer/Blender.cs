namespace NewTOAPIA.Drawing
{
    using System;

    public interface IBlender
    {
        int NumPixelBits { get; }

        RGBA_Bytes PixelToColorRGBA_Bytes(byte[] buffer, int bufferOffset);
        void CopyPixels(byte[] buffer, int bufferOffset, RGBA_Bytes sourceColor, int count);

        void BlendPixel(byte[] buffer, int bufferOffset, RGBA_Bytes sourceColor);
        void BlendPixels(byte[] buffer, int bufferOffset,
            RGBA_Bytes[] sourceColors, int sourceColorsOffset,
            byte[] sourceCovers, int sourceCoversOffset, bool firstCoverForAll, int count);
    }

    public class BlenderBaseBGRA
    {
        public int NumPixelBits { get { return 32; } }

        public const byte base_mask = 255;
    };

    public sealed class BlenderBGRA : BlenderBaseBGRA, IBlender
    {
        public RGBA_Bytes PixelToColorRGBA_Bytes(byte[] buffer, int bufferOffset)
        {
            return new RGBA_Bytes(buffer[bufferOffset + ImageBuffer.OrderR], buffer[bufferOffset + ImageBuffer.OrderG], buffer[bufferOffset + ImageBuffer.OrderB], buffer[bufferOffset + ImageBuffer.OrderA]);
        }

        public void CopyPixels(byte[] pBuffer, int bufferOffset, RGBA_Bytes sourceColor, int count)
        {
            do
            {
                pBuffer[bufferOffset + ImageBuffer.OrderR] = sourceColor.m_R;
                pBuffer[bufferOffset + ImageBuffer.OrderG] = sourceColor.m_G;
                pBuffer[bufferOffset + ImageBuffer.OrderB] = sourceColor.m_B;
                pBuffer[bufferOffset + ImageBuffer.OrderA] = sourceColor.m_A;
                bufferOffset += 4;
            }
            while (--count != 0);
        }

        public void BlendPixel(byte[] buffer, int bufferOffset, RGBA_Bytes sourceColor)
        {
            unchecked
            {
                int r = buffer[bufferOffset + ImageBuffer.OrderR];
                int g = buffer[bufferOffset + ImageBuffer.OrderG];
                int b = buffer[bufferOffset + ImageBuffer.OrderB];
                int a = buffer[bufferOffset + ImageBuffer.OrderA];
                buffer[bufferOffset + ImageBuffer.OrderR] = (byte)(((sourceColor.m_R - r) * sourceColor.m_A + (r << (int)RGBA_Bytes.base_shift)) >> (int)RGBA_Bytes.base_shift);
                buffer[bufferOffset + ImageBuffer.OrderG] = (byte)(((sourceColor.m_G - g) * sourceColor.m_A + (g << (int)RGBA_Bytes.base_shift)) >> (int)RGBA_Bytes.base_shift);
                buffer[bufferOffset + ImageBuffer.OrderB] = (byte)(((sourceColor.m_B - b) * sourceColor.m_A + (b << (int)RGBA_Bytes.base_shift)) >> (int)RGBA_Bytes.base_shift);
                buffer[bufferOffset + ImageBuffer.OrderA] = (byte)((sourceColor.m_A + a) - ((sourceColor.m_A * a + base_mask) >> (int)RGBA_Bytes.base_shift));
            }
        }

        public void BlendPixels(byte[] destBuffer, int bufferOffset,
            RGBA_Bytes[] sourceColors, int sourceColorsOffset,
            byte[] covers, int coversIndex, bool firstCoverForAll, int count)
        {
            if (firstCoverForAll)
            {
                int cover = covers[coversIndex];
                if (cover == 255)
                {
                    do
                    {
                        BlendPixel(destBuffer, bufferOffset, sourceColors[sourceColorsOffset++]);
                        bufferOffset += 4;
                    }
                    while (--count != 0);
                }
                else
                {
                    do
                    {
                        sourceColors[sourceColorsOffset].m_A = (byte)((sourceColors[sourceColorsOffset].m_A * cover + 255) >> 8);
                        BlendPixel(destBuffer, bufferOffset, sourceColors[sourceColorsOffset]);
                        bufferOffset += 4;
                        ++sourceColorsOffset;
                    }
                    while (--count != 0);
                }
            }
            else
            {
                do
                {
                    int cover = covers[coversIndex++];
                    if (cover == 255)
                    {
                        BlendPixel(destBuffer, bufferOffset, sourceColors[sourceColorsOffset]);
                    }
                    else
                    {
                        RGBA_Bytes color = sourceColors[sourceColorsOffset];
                        color.m_A = (byte)((color.m_A * (cover) + 255) >> 8);
                        BlendPixel(destBuffer, bufferOffset, color);
                    }
                    bufferOffset += 4;
                    ++sourceColorsOffset;
                }
                while (--count != 0);
            }
        }
    };

    public sealed class BlenderGammaBGRA : BlenderBaseBGRA, IBlender
    {
        private GammaLookUpTable m_gamma;

        public BlenderGammaBGRA()
        {
            m_gamma = new GammaLookUpTable();
        }

        public BlenderGammaBGRA(GammaLookUpTable g)
        {
            m_gamma = g;
        }

        public void gamma(GammaLookUpTable g)
        {
            m_gamma = g;
        }

        public RGBA_Bytes PixelToColorRGBA_Bytes(byte[] buffer, int bufferOffset)
        {
            return new RGBA_Bytes(buffer[bufferOffset + ImageBuffer.OrderR], buffer[bufferOffset + ImageBuffer.OrderG], buffer[bufferOffset + ImageBuffer.OrderB], buffer[bufferOffset + ImageBuffer.OrderA]);
        }

        public void CopyPixels(byte[] pBuffer, int bufferOffset, RGBA_Bytes sourceColor, int count)
        {
            do
            {
                pBuffer[bufferOffset + ImageBuffer.OrderR] = m_gamma.inv(sourceColor.m_R);
                pBuffer[bufferOffset + ImageBuffer.OrderG] = m_gamma.inv(sourceColor.m_G);
                pBuffer[bufferOffset + ImageBuffer.OrderB] = m_gamma.inv(sourceColor.m_B);
                pBuffer[bufferOffset + ImageBuffer.OrderA] = m_gamma.inv(sourceColor.m_A);
                bufferOffset += 4;
            }
            while (--count != 0);
        }

        public void BlendPixel(byte[] buffer, int bufferOffset, RGBA_Bytes sourceColor)
        {
            unchecked
            {
                int r = buffer[bufferOffset + ImageBuffer.OrderR];
                int g = buffer[bufferOffset + ImageBuffer.OrderG];
                int b = buffer[bufferOffset + ImageBuffer.OrderB];
                int a = buffer[bufferOffset + ImageBuffer.OrderA];
                buffer[bufferOffset + ImageBuffer.OrderR] = m_gamma.inv((byte)(((sourceColor.m_R - r) * sourceColor.m_A + (r << (int)RGBA_Bytes.base_shift)) >> (int)RGBA_Bytes.base_shift));
                buffer[bufferOffset + ImageBuffer.OrderG] = m_gamma.inv((byte)(((sourceColor.m_G - g) * sourceColor.m_A + (g << (int)RGBA_Bytes.base_shift)) >> (int)RGBA_Bytes.base_shift));
                buffer[bufferOffset + ImageBuffer.OrderB] = m_gamma.inv((byte)(((sourceColor.m_B - b) * sourceColor.m_A + (b << (int)RGBA_Bytes.base_shift)) >> (int)RGBA_Bytes.base_shift));
                buffer[ImageBuffer.OrderA] = (byte)((sourceColor.m_A + a) - ((sourceColor.m_A * a + base_mask) >> (int)RGBA_Bytes.base_shift));
            }
        }

        public void BlendPixels(byte[] buffer, int bufferOffset,
            RGBA_Bytes[] sourceColors, int sourceColorsOffset,
            byte[] sourceCovers, int sourceCoversOffset, bool firstCoverForAll, int count)
        {
            throw new NotImplementedException();
        }
    };

    public sealed class BlenderPreMultBGRA : BlenderBaseBGRA, IBlender
    {
        static int[] m_Saturate9BitToByte = new int[1 << 9];

        public BlenderPreMultBGRA()
        {
            if (m_Saturate9BitToByte[2] == 0)
            {
                for (int i = 0; i < m_Saturate9BitToByte.Length; i++)
                {
                    m_Saturate9BitToByte[i] = Math.Min(i, 255);
                }
            }
        }

        public RGBA_Bytes PixelToColorRGBA_Bytes(byte[] buffer, int bufferOffset)
        {
            return new RGBA_Bytes(buffer[bufferOffset + ImageBuffer.OrderR], buffer[bufferOffset + ImageBuffer.OrderG], buffer[bufferOffset + ImageBuffer.OrderB], buffer[bufferOffset + ImageBuffer.OrderA]);
        }

        public void CopyPixels(byte[] pBuffer, int bufferOffset, RGBA_Bytes sourceColor, int count)
        {
            for (int i = 0; i < count; i++)
            {
                pBuffer[bufferOffset + ImageBuffer.OrderR] = sourceColor.m_R;
                pBuffer[bufferOffset + ImageBuffer.OrderG] = sourceColor.m_G;
                pBuffer[bufferOffset + ImageBuffer.OrderB] = sourceColor.m_B;
                pBuffer[bufferOffset + ImageBuffer.OrderA] = sourceColor.m_A;
                bufferOffset += 4;
            }
        }

        public void BlendPixel(byte[] pDestBuffer, int bufferOffset, RGBA_Bytes sourceColor)
        {
            int OneOverAlpha = base_mask - sourceColor.m_A;
            unchecked
            {
                int r = m_Saturate9BitToByte[((pDestBuffer[bufferOffset + ImageBuffer.OrderR] * OneOverAlpha + 255) >> 8) + sourceColor.m_R];
                int g = m_Saturate9BitToByte[((pDestBuffer[bufferOffset + ImageBuffer.OrderG] * OneOverAlpha + 255) >> 8) + sourceColor.m_G];
                int b = m_Saturate9BitToByte[((pDestBuffer[bufferOffset + ImageBuffer.OrderB] * OneOverAlpha + 255) >> 8) + sourceColor.m_B];
                int a = pDestBuffer[bufferOffset + ImageBuffer.OrderA];
                pDestBuffer[bufferOffset + ImageBuffer.OrderR] = (byte)r;
                pDestBuffer[bufferOffset + ImageBuffer.OrderG] = (byte)g;
                pDestBuffer[bufferOffset + ImageBuffer.OrderB] = (byte)b;
                pDestBuffer[bufferOffset + ImageBuffer.OrderA] = (byte)(base_mask - m_Saturate9BitToByte[(OneOverAlpha * (base_mask - a) + 255) >> 8]);
            }
        }

        public void BlendPixels(byte[] pDestBuffer, int bufferOffset,
            RGBA_Bytes[] sourceColors, int sourceColorsOffset,
            byte[] sourceCovers, int sourceCoversOffset, bool firstCoverForAll, int count)
        {
            if (firstCoverForAll)
            {
                if (sourceCovers[sourceCoversOffset] == 255)
                {
                    for (int i = 0; i < count; i++)
                    {
                        BlendPixel(pDestBuffer, bufferOffset, sourceColors[sourceColorsOffset]);
                        sourceColorsOffset++;
                        bufferOffset += 4;
                    }
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    sourceColors[sourceColorsOffset].A_Byte = m_Saturate9BitToByte[((sourceColors[sourceColorsOffset].A_Byte * sourceCovers[sourceCoversOffset] + 255) >> 8)];
                    BlendPixel(pDestBuffer, bufferOffset, sourceColors[sourceColorsOffset]);
                    sourceColorsOffset++;
                    bufferOffset += 4;
                }
            }
        }
    }
}