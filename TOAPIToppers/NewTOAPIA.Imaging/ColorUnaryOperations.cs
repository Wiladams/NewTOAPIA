using System;

namespace NewTOAPIA.Graphics.Imaging
{
    using NewTOAPIA.Drawing;
    using NewTOAPIA.Graphics;

    public class AverageWithConstantColor : ITransformColor
        {
            ColorRGBA fColor;

            public AverageWithConstantColor(ColorRGBA aColor)
            {
                fColor = aColor;
            }

            public ColorRGBA Transform(ColorRGBA dst)
            {
                float red = (dst.R + fColor.R) / 2.0f;
                float green = (dst.G + fColor.G) / 2.0f;
                float blue = (dst.B + fColor.B) / 2.0f;

                ColorRGBA newColor = new ColorRGBA(red, green, blue, dst.A);

                return newColor;
            }
        }

        // Blend Functions
        // Source Factor
        /// <summary>
        /// Zero
        /// One
        /// SrcColor
        /// OneMinueSrcColor
        /// DstColor
        /// OneMinusDstColor
        /// SrcAlpha,
        /// OneMinusSrcAlpha
        /// DstAlpha
        /// 
        /// </summary>
    public class ConstantAlpha : ITransformColor
        {
            float fAlpha;

            public ConstantAlpha(float alpha)
            {
                fAlpha = alpha;
            }

            public ColorRGBA Transform(ColorRGBA dst)
            {
                dst.A = fAlpha;
                return dst;
            }
        }

    public class ConstantColor : ITransformColor
        {
            ColorRGBA fColor;

            public ConstantColor(ColorRGBA aColor)
            {
                fColor = aColor;
            }

            public ColorRGBA Transform(ColorRGBA dst)
            {
                return fColor;
            }
        }

    public class Identity : ITransformColor
        {
            public ColorRGBA Transform(ColorRGBA dst)
            {
                return dst;
            }
        }

        /// <summary>
        /// Operator 'NOT' is a logical operator for pixels.
        /// it will basically flip bits around 128 and create
        /// what looks like a negative image.
        /// The alpha channel is left unchanged.
        /// </summary>
    public class NOT : ITransformColor
        {
            public ColorRGBA Transform(ColorRGBA dst)
            {
                // First convert to a Pixel value so we can twiddle bits
                BGRAb pixel = new BGRAb();
                pixel.SetColor(dst);

                pixel.Red = (byte)(255 - pixel.Red);
                pixel.Green = (byte)(255 - pixel.Green);
                pixel.Blue = (byte)(255 - pixel.Blue);

                return pixel.GetColor();
            }
        }

    public class NTSCLuminance : ITransformColor
        {
            public ColorRGBA Transform(ColorRGBA dst)
            {
                float red = dst.R * 0.299f;
                float green = dst.G * 0.587f;
                float blue = dst.B * 0.114f;
                float lum = red + green + blue;

                return new ColorRGBA(lum, lum, lum, dst.A);
            }
        }

        /// <summary>
        /// Subtract a constant color.  Stretch the results by adding 1.0 to the
        /// result of the addition and then dividing by 2.0.  It takes more computation,
        /// but it ensures that the values will stay within the range of 0.0 -> 1.0
        /// </summary>
    public class SubtractColor : ITransformColor
        {
            ColorRGBA fColor;

            public SubtractColor(ColorRGBA aColor)
            {
                fColor = aColor;
            }

            public ColorRGBA Transform(ColorRGBA dst)
            {
                return PerformOperation(dst, fColor);
            }

            public static ColorRGBA PerformOperation(ColorRGBA dst, ColorRGBA aColor)
            {
                
                float red = Math.Max(0,dst.R - aColor.R);
                float green = Math.Max(0, dst.G - aColor.G);
                float blue = Math.Max(0, dst.B - aColor.B);
                
                ColorRGBA newColor = new ColorRGBA(red, green, blue, dst.A);

                return newColor;
            }
        }

}
