
namespace NewTOAPIA.Graphics
{
    using System;
    using System.Runtime.InteropServices;

    public struct PixelInformation : IPixelInformation, IEquatable<PixelInformation>
    {
        #region Fields
        public PixelLayout layout;
        public PixelComponentType componentType;
        #endregion

        #region Constructor
        public PixelInformation(IPixelInformation pInfo)
        {
            this.layout = pInfo.Layout;
            this.componentType = pInfo.ComponentType;
        }

        public PixelInformation(PixelLayout layout, PixelComponentType componentType)
        {
            this.layout = layout;
            this.componentType = componentType;
        }
        #endregion

        #region Properties
        public PixelComponentType ComponentType { get { return ComponentType; } }
        public PixelLayout Layout { get { return layout; } }
        public int Dimension { get { return GetComponentsPerLayout(layout); } }

        public int BitsPerPixel
        {
            get { return GetBitsPerPixel(Layout, ComponentType); }
        }

        public int BytesPerPixel
        {
            get { return BitsPerPixel / 8; }
        }
        #endregion

        #region IEQuatable
        public bool Equals(PixelInformation rhs)
        {
            return layout == rhs.layout && componentType == rhs.componentType;
        }
        #endregion

        #region Static Helpers
        public static int GetBitsPerPixel(PixelInformation pInfo)
        {
            return GetBitsPerType(pInfo.ComponentType) * GetComponentsPerLayout(pInfo.Layout);
        }

        public static int GetBitsPerPixel(PixelLayout layout, PixelComponentType componentType)
        {
            return GetBitsPerType(componentType) * GetComponentsPerLayout(layout);
        }

        public static int GetBytesPerPixel(PixelLayout layout, PixelComponentType componentType)
        {
            return GetBytesPerPixel(layout, componentType) / 8;
        }

        public static int GetComponentsPerLayout(PixelLayout format)
        {
            switch (format)
            {
                case PixelLayout.ColorIndex:
                case PixelLayout.StencilIndex:
                case PixelLayout.DepthComponent:
                    return 1;

                case PixelLayout.Alpha:
                case PixelLayout.Red:
                case PixelLayout.Green:
                case PixelLayout.Blue:
                case PixelLayout.Luminance:
                    return 1;

                case PixelLayout.LuminanceAlpha:
                    return 2;

                case PixelLayout.Rgb:
                case PixelLayout.Bgr:
                    return 3;

                case PixelLayout.Rgba:
                case PixelLayout.Bgra:
                    return 4;
            }

            return 0;
        }

        public static int GetBytesPerComponent(PixelComponentType aType)
        {
            return GetBitsPerType(aType) / 8;
        }

        public static int GetBitsPerType(PixelComponentType pType)
        {
            switch (pType)
            {
                case PixelComponentType.Byte:
                case PixelComponentType.UnsignedByte:
                    //case PixelComponentType.UnsignedByte_332:
                    //case PixelComponentType.UnsignedByte_233_Rev:
                    return sizeof(byte) * 8;

                case PixelComponentType.Short:
                    //case PixelComponentType.UnsignedShort:
                    //case PixelComponentType.UnsignedShort_565:
                    //case PixelComponentType.UnsignedShort_565_Rev:
                    //case PixelComponentType.UnsignedShort_4444:
                    //case PixelComponentType.UnsignedShort_4444_Rev:
                    //case PixelComponentType.UnsignedShort_5551:
                    //case PixelComponentType.UnsignedShort_1555_Rev:
                    return sizeof(short) * 8;

                case PixelComponentType.Int:
                    //case PixelComponentType.UnsignedInt:
                    return sizeof(int) * 8;

                case PixelComponentType.Float:
                    //case PixelComponentType.UnsignedInt_8888:
                    //case PixelComponentType.UnsignedInt_8888_Rev:
                    //case PixelComponentType.UnsignedInt_1010102:
                    //case PixelComponentType.UnsignedInt_2101010_Rev:
                    return sizeof(float) * 8;

                case PixelComponentType.Double:
                    return sizeof(double) * 8;

                //case PixelComponentType.Bitmap:
                default:
                    return 0;
            }
        }

        public static double GetMaxComponentValue(PixelComponentType aType)
        {
            switch (aType)
            {
                case PixelComponentType.Byte:
                    return byte.MaxValue;
                case PixelComponentType.Short:
                    return short.MaxValue;
                case PixelComponentType.Int:
                    return int.MaxValue;
                case PixelComponentType.Float:
                    return Single.MaxValue;
                case PixelComponentType.Double:
                    return double.MaxValue;
            }

            return 0;
        }

        #endregion
    }
}
