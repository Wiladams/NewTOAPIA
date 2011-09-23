

namespace NewTOAPIA.Graphics
{
    //public enum GLPixelFormat
    //{
    //    ColorIndex = 0x1900,
    //    StencilIndex = 0x1901,
    //    DepthComponent = 0x1902,
    //    Red = 0x1903,
    //    Green = 0x1904,
    //    Blue = 0x1905,
    //    Alpha = 0x1906,
    //    Rgb = 0x1907,
    //    Rgba = 0x1908,
    //    Luminance = 0x1909,
    //    LuminanceAlpha = 0x190A,
    //    Bgr = 32992,
    //    Bgra = 32993,
    //}

    /// <summary>
    /// Gives the layout of an individual pixel in memory.
    /// This, combined with the PixelComponentType, give you
    /// the complete memory representation of a pixel.
    /// 
    /// The selection of the enum numbers here favor OpenGL 
    /// definitions for the same.
    /// </summary>

    public enum PixelLayout
    {
        ColorIndex = 0x1900,
        StencilIndex = 0x1901,
        DepthComponent = 0x1902,
        
        /// <summary>
        /// The pixels represent only the red information of an image.
        /// </summary>
        Red = 0x1903,

        /// <summary>
        /// The pixels represent only the green informaiton of an image.
        /// </summary>
        Green = 0x1904,

        /// <summary>
        /// The pixels represent only the blue informaiton of an image.
        /// </summary>
        Blue = 0x1905,

        /// <summary>
        /// The pixels represent alpha informaiton of an image.  In many cases,
        /// the alpha information is the opacity value of an image.  It might be 
        /// held as a separate image for easier processing.
        /// </summary>
        Alpha = 0x1906,

        /// <summary>
        /// The pixels represent a full set of Red, Green, and Blue components per
        /// pixel.  
        /// In memory, the 'R' - red comes first, followed by the 'g' - green, followed by the 'b' - blue
        /// </summary>
        Rgb = 0x1907,

        /// <summary>
        /// The pixels represent Red, Green, and Blue, as well as an alpha component 
        /// for each pixel.
        /// In memory, the 'R' - red comes first, followed by the 'g' - green, followed by the 'b' - blue
        /// </summary>
        Rgba = 0x1908,

        /// <summary>
        /// The individual pixels represent only luminance information.  This is essentially a grayscale image.
        /// </summary>
        Luminance = 0x1909,

        /// <summary>
        /// Luminance information is followed by an alpha component per pixel.
        /// </summary>
        LuminanceAlpha = 0x190A,

        /// <summary>
        /// The pixels represent 'B' - Blue, 'G' - Green, 'R' - Red, in that order in memory.
        /// </summary>
        Bgr = 0x80e0,

        /// <summary>
        /// The pixels represent 'B' - Blue, 'G' - Green, 'R' - Red, and 'A' - Alpha, in that order in memory.
        /// </summary>
        Bgra = 0x80e1,
    }

    ////public enum PixelType
    ////{
    ////    Bitmap = gl.GL_BITMAP,
    ////    Byte = gl.GL_BYTE,
    ////    UnsignedByte = gl.GL_UNSIGNED_BYTE,
    ////    Short = gl.GL_SHORT,
    ////    UnsignedShort = gl.GL_UNSIGNED_SHORT,
    ////    Int = gl.GL_INT,
    ////    UnsignedInt = gl.GL_UNSIGNED_INT,
    ////    Float = gl.GL_FLOAT,
    ////    UnsignedByte_332 = gl.GL_UNSIGNED_BYTE_3_3_2,
    ////    UnsignedByte_233_Rev = gl.GL_UNSIGNED_BYTE_2_3_3_REV,
    ////    UnsignedShort_565 = gl.GL_UNSIGNED_SHORT_5_6_5,
    ////    UnsignedShort_565_Rev = gl.GL_UNSIGNED_SHORT_5_6_5_REV,
    ////    UnsignedShort_4444 = gl.GL_UNSIGNED_SHORT_4_4_4_4,
    ////    UnsignedShort_4444_Rev = gl.GL_UNSIGNED_SHORT_4_4_4_4_REV,
    ////    UnsignedShort_5551 = gl.GL_UNSIGNED_SHORT_5_5_5_1,
    ////    UnsignedShort_1555_Rev = gl.GL_UNSIGNED_SHORT_1_5_5_5_REV,
    ////    UnsignedInt_8888 = gl.GL_UNSIGNED_INT_8_8_8_8,
    ////    UnsignedInt_8888_Rev = gl.GL_UNSIGNED_INT_8_8_8_8_REV,
    ////    UnsignedInt_1010102 = gl.GL_UNSIGNED_INT_10_10_10_2,
    ////    UnsignedInt_2101010_Rev = gl.GL_UNSIGNED_INT_2_10_10_10_REV
    ////}

    /// <summary>
    /// The PixelComponentType determines what basic underlying data type is used for each component
    /// of the pixel.  A very typical representation is to use a 'Byte' per component, but any type 
    /// </summary>
    public enum PixelComponentType
    {
        /// <summary>
        /// Each component of the pixel is represented by a byte, which is 8 bits.
        /// </summary>
        Byte = 0x1400,          // GL_BYTE
        UnsignedByte = 0x1401,  // gl.GL_UNSIGNED_BYTE,

        /// <summary>
        /// Each component of the pixel is represented by a short, which is 16 bits.
        /// </summary>
        Short = 0x1402, // GL_SHORT

        /// <summary>
        /// Each component of the pixel is represented by a int, which is 32 bits.
        /// </summary>
        Int = 0x1404,   // GL_INT

        /// <summary>
        /// Each component of the pixel is represented by a float, which is a 
        /// single precision floating point number of 32 bits.
        /// </summary>
        Float = 0x1406, // GL_FLOAT

        Double = 0x1407,
    }

    /// <summary>
    /// The PixmapOrientation determines how the pixel rows are layed out in the 
    /// PixelArray memory.
    /// </summary>
    public enum PixmapOrientation
    {
        /// <summary>
        /// The least row of the image is at the low byte of memory.  Rows progress
        /// higher as memory increases.
        /// </summary>
        TopToBottom,

        /// <summary>
        /// The highest row of the image is stored at the low byte of memory.  Rows of 
        /// the image decrease as memory increases.
        /// </summary>
        BottomToTop
    }
}
