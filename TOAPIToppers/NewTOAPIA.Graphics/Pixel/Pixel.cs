
namespace NewTOAPIA.Graphics
{
    using System;
    using System.Runtime.InteropServices;

    public class Pixel : IPixel
    {
        byte[] Data { get; set; }

        #region IPixelInformation
        public int BytesPerPixel { get { return PixelInformation.GetBytesPerPixel(Layout, ComponentType); } }
        public PixelLayout Layout { get; private set; }
        public PixelComponentType ComponentType { get; private set; }
        public int Dimension { get { return PixelInformation.GetComponentsPerLayout(Layout); } }
        #endregion


        #region Constructors
        public Pixel(PixelInformation pixInfo, byte[] data)
            : this(pixInfo, data, 0)
        {
        }

        public Pixel(PixelInformation pixInfo, byte[] data, int startIndex)
            : this(pixInfo.Layout, pixInfo.ComponentType, data, startIndex)
        {
        }

        public Pixel(PixelLayout layout, PixelComponentType pType, ColorRGBA color)
        {
            Layout = layout;
            ComponentType = pType;

            SetColor(color);
        }

        public Pixel(PixelLayout layout, PixelComponentType pType, byte[] data, int startIndex)
        {
            Layout = layout;
            ComponentType = pType;

            int pixelLength = PixelInformation.GetBytesPerPixel(layout, pType); 

            // Allocate some space to hold the pixel data
            Data = new byte[pixelLength];

            // Copy the pixel data into the new space
            Array.Copy(data, startIndex, Data, 0, pixelLength);
        }

        public Pixel(IPixel pel)
        {
            Layout = pel.Layout;
            ComponentType = pel.ComponentType;
            SetColor(pel.GetColor());
        }
        #endregion

        #region IPixel
        public RGBAb ToRGBAb()
        {
            return new RGBAb(GetColor());
        }

        public int Red { get { return (int)255; } set {  } }
        public int Green { get { return (int)255; } set { } }
        public int Blue { get { return (int)255; } set { } }
        public int Alpha { get { return (int)255; } set { } }

        public ColorRGBA GetColor()
        {
            return Pixel.GetColor(Layout, ComponentType, GetBytes());
        }

        public void SetColor(ColorRGBA color)
        {
            SetBytes( Pixel.GetBytesForColor(Layout, ComponentType, color));
        }

        public byte[] GetBytes()
        {
            return Data;
        }

        public void SetBytes(byte[] bytes)
        {
            Data = bytes;
        }

        public void SetBytes(byte[] data, int startIndex)
        {
            int pixelLength = PixelInformation.GetBytesPerPixel(Layout, ComponentType);

            // Allocate some space to hold the pixel data
            Data = new byte[pixelLength];

            // Copy the pixel data into the new space
            Array.Copy(data, startIndex, Data, 0, pixelLength);
        }

        public void SetBytes(IntPtr data, int startIndex)
        {
            int pixelLength = PixelInformation.GetBytesPerPixel(Layout, ComponentType);

            // Allocate some space to hold the pixel data
            Data = new byte[pixelLength];

            // Copy the pixel data into the new space
            Marshal.Copy(data, Data, 0, pixelLength);
        }
        #endregion

        public override string ToString()
        {
            return GetColor().ToString();
        }


        public static bool operator==(Pixel lhs, IPixel rhs)
        {
            return lhs.Layout == rhs.Layout && lhs.ComponentType == rhs.ComponentType;
        }

        public static bool operator !=(Pixel lhs, IPixel rhs)
        {
            return !(lhs == rhs);
        }

        public bool Equals(Pixel rhs)
        {
            return this == rhs;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Data.GetHashCode();
        }

        #region Static Helpers
        public static byte[] GetBytesForColor(IPixelInformation pInfo, ColorRGBA color)
        {
            return GetBytesForColor(pInfo.Layout, pInfo.ComponentType, color);
        }

        public static byte[] GetBytesForColor(PixelLayout layout, PixelComponentType pType, ColorRGBA color)
        {
            int nComponents = PixelInformation.GetComponentsPerLayout(layout);
            int componentSize = PixelInformation.GetBytesPerComponent(pType);
            double maxValue = PixelInformation.GetMaxComponentValue(pType);

            // First use the layout to put the right floats into the source array
            float[] srcFloats = new float[nComponents];

            switch (layout)
            {
                case PixelLayout.Bgr:
                    srcFloats[0] = color.B;
                    srcFloats[1] = color.G;
                    srcFloats[2] = color.R;

                    break;

                case PixelLayout.Bgra:
                    srcFloats[0] = color.B;
                    srcFloats[1] = color.G;
                    srcFloats[2] = color.R;
                    srcFloats[3] = color.A;
                    break;

                case PixelLayout.Rgb:
                    srcFloats[0] = color.R;
                    srcFloats[1] = color.G;
                    srcFloats[2] = color.B;
                    break;

                case PixelLayout.Rgba:
                    srcFloats[0] = color.R;
                    srcFloats[1] = color.G;
                    srcFloats[2] = color.B;
                    srcFloats[3] = color.A;
                    break;

                case PixelLayout.Luminance:
                    srcFloats[0] = color.R;
                    break;
            }


            // Allocate the result array
            byte[] result = new byte[PixelInformation.GetBytesPerPixel(layout, pType)];

            // Perform the type conversion, and place the bytes into 
            // the result array
            for (int i = 0; i < nComponents; i++)
            {
                switch (pType)
                {
                    case PixelComponentType.Byte:
                    case PixelComponentType.UnsignedByte:
                        {
                            byte aValue = (byte)(srcFloats[i] * byte.MaxValue);
                            Array.Copy(BitConverter.GetBytes(aValue), 0, result, i * componentSize, componentSize);
                        }
                        break;

                    case PixelComponentType.Short:
                        {
                            short aValue = (short)(srcFloats[i] * short.MaxValue);
                            Array.Copy(BitConverter.GetBytes(aValue), 0, result, i * componentSize, componentSize);
                        }
                        break;

                    case PixelComponentType.Int:
                        {
                            int aValue = (int)(srcFloats[i] * int.MaxValue);
                            Array.Copy(BitConverter.GetBytes(aValue), 0, result, i * componentSize, componentSize);
                        }
                        break;

                    case PixelComponentType.Float:
                        {
                            float aValue = (float)(srcFloats[i] * float.MaxValue);
                            Array.Copy(BitConverter.GetBytes(aValue), 0, result, i * componentSize, componentSize);
                        }
                        break;

                    case PixelComponentType.Double:
                        {
                            double aValue = (double)(srcFloats[i] * double.MaxValue);
                            Array.Copy(BitConverter.GetBytes(aValue), 0, result, i * componentSize, componentSize);
                        }
                        break;
                }
            }

            return result;
        }

        public static ColorRGBA GetColor(IPixelInformation pixelInfo, byte[] data)
        {
            return GetColor(pixelInfo, data, 0);
        }

        public static ColorRGBA GetColor(IPixelInformation pixelInfo, byte[] data, int startIndex)
        {
            return GetColor(pixelInfo.Layout, pixelInfo.ComponentType, data, startIndex);
        }

        public static ColorRGBA GetColor(PixelLayout layout, PixelComponentType pType, byte[] data)
        {
            return GetColor(layout, pType, data, 0);
        }


        public static ColorRGBA GetColor(PixelLayout layout, PixelComponentType pType, byte[] data, int startIndex)
        {
            int bytesPerPixel = PixelInformation.GetBytesPerPixel(layout, pType);

            // how many components are there?
            int nComponents = PixelInformation.GetComponentsPerLayout(layout);
            int componentSize = PixelInformation.GetBytesPerComponent(pType);
            double maxValue = PixelInformation.GetMaxComponentValue(pType);

            float[] floats = new float[nComponents];

            for (int component = 0; component < nComponents; component++)
            {
                float afloat = 0f;

                switch (pType)
                {
                    case PixelComponentType.Byte:
                    case PixelComponentType.UnsignedByte:
                        afloat = (float)(data[startIndex+component * componentSize] / maxValue);
                        break;

                    case PixelComponentType.Short:
                        afloat = (float)(BitConverter.ToInt16(data, startIndex + component * componentSize) / maxValue);
                        break;

                    case PixelComponentType.Int:
                        afloat = (float)(BitConverter.ToInt32(data, startIndex + component * componentSize) / maxValue);
                        break;

                    case PixelComponentType.Float:
                        afloat = (float)(BitConverter.ToSingle(data, startIndex + component * componentSize) / maxValue);
                        break;

                    case PixelComponentType.Double:
                        afloat = (float)(BitConverter.ToDouble(data, startIndex + component * componentSize) / maxValue);
                        break;
                }

                floats[component] = afloat;
            }

            ColorRGBA newColor = new ColorRGBA(floats);

            return newColor;
        }

        public static ColorRGBA GetColor(IPixelInformation pixelInfo, IntPtr data, int startIndex)
        {
            int bytesPerPixel = pixelInfo.BytesPerPixel;


            // how many components are there?
            int nComponents = PixelInformation.GetComponentsPerLayout(pixelInfo.Layout);
            int componentSize = PixelInformation.GetBytesPerComponent(pixelInfo.ComponentType);
            double maxValue = PixelInformation.GetMaxComponentValue(pixelInfo.ComponentType);

            float[] floats = new float[nComponents];


            switch (pixelInfo.ComponentType)
            {
                case PixelComponentType.Byte:
                case PixelComponentType.UnsignedByte:
                    byte[] bufferb = new byte[nComponents];
                    Marshal.Copy(data, bufferb, 0, nComponents);
                    for (int i = 0; i < nComponents; i++)
                        floats[i] = (float)(bufferb[i] / maxValue);
                    break;

                case PixelComponentType.Short:
                    short[] buffers = new short[nComponents];
                    Marshal.Copy(data, buffers, 0, nComponents);
                    for (int i = 0; i < nComponents; i++)
                        floats[i] = (float)(buffers[i] / maxValue);
                    break;


                case PixelComponentType.Int:
                    int[] bufferi = new int[nComponents];
                    Marshal.Copy(data, bufferi, 0, nComponents);
                    for (int i = 0; i < nComponents; i++)
                        floats[i] = (float)(bufferi[i] / maxValue);
                    break;

                case PixelComponentType.Float:
                    int[] bufferf = new int[nComponents];
                    Marshal.Copy(data, bufferf, 0, nComponents);
                    for (int i = 0; i < nComponents; i++)
                        floats[i] = (float)(bufferf[i] / maxValue);
                    break;

                case PixelComponentType.Double:
                    int[] bufferd = new int[nComponents];
                    Marshal.Copy(data, bufferd, 0, nComponents);
                    for (int i = 0; i < nComponents; i++)
                        floats[i] = (float)(bufferd[i] / maxValue);
                    break;
            }

            ColorRGBA newColor = new ColorRGBA(floats);

            return newColor;
        }

        #endregion
    }
}
