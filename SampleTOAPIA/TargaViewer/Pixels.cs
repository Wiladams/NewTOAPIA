using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

using NewTOAPIA;

namespace TargaViewer
{
    [Serializable]
    [StructLayout(LayoutKind.Explicit)]
    public struct PixelBGRb : IPixelBGR<byte>
    {
        public static PixelBGRb Empty = new PixelBGRb();

        [FieldOffset(0)]
        public byte B;

        [FieldOffset(1)]
        public byte G;

        [FieldOffset(2)]
        public byte R;


        #region Constructors
        public PixelBGRb(byte red, byte green, byte blue)
        {
            R = red;
            G = green;
            B = blue;
        }

        #endregion

        #region IPixel
        public ColorRGBA GetColor(out ColorRGBA color)
        {
            color.r = (float)Red / 0xff;
            color.g = (float)Green / 0xff;
            color.b = (float)Blue / 0xff;
            color.a = 1.0f;

            return color;
        }

        public ColorRGBA GetColor()
        {
            return new ColorRGBA((float)Red / 0xff, (float)Green / 0xff, (float)Blue / 0xff);
        }

        public void SetColor(ColorRGBA aColor)
        {
            Red = (byte)((aColor.Red * 0xff) + 0.5f);
            Green = (byte)((aColor.Green * 0xff) + 0.5f);
            Blue = (byte)((aColor.Blue * 0xff) + 0.5f);
        }
        #endregion

        #region IPixelInformation
        public int BitsPerPixel { get { return PixelInformation.GetBitsPerPixel(Layout, ComponentType); } }
        public PixelLayout Layout { get { return PixelLayout.Bgr; } }
        public PixelComponentType ComponentType { get { return PixelComponentType.Byte; } }
        #endregion

        #region Component Access
        public byte Red
        {
            get
            {
                return R;
            }
            set
            {
                R = value;
            }
        }

        public byte Green
        {
            get
            {
                return G;
            }
            set
            {
                G = value;
            }
        }

        public byte Blue
        {
            get
            {
                return B;
            }
            set
            {
                B = value;
            }
        }
        #endregion
    }
}
