
namespace NewTOAPIA.Graphics
{
    using System;
    using NewTOAPIA.Graphics;

    /// <summary>
    /// This is the most fundamental representation of Pixel based images.
    /// As the most basic type, it is meant to operate as an interchange
    /// representation between different pixel map types.
    /// </summary>
    public interface IPixelArray : IColorAccessor, IPixelInformation
    {
        /// <summary>
        /// Represents the number of pixels that are in each row.
        /// </summary>
        //int Width { get; }

        /// <summary>
        /// Indicates how many rows there are in the image.
        /// </summary>
        //int Height { get; }


        int BytesPerRow { get; }

        //int StridePixels { get; }


        /// <summary>
        /// An indication of whether the image is stored top to bottom, or
        /// bottom to top.
        /// </summary>
        PixmapOrientation Orientation { get; }


        /// <summary>
        ///  An indication of the alignment for the image.  This is a number
        ///  of bytes, where '1' would be byte aligned, '2' would be 2 byte (WORD) aligned.
        ///  '4' would be 4 byte (DWORD) aligned.  The default is '1', which means there is no
        ///  padding on the end of each row.  If the alignment is anything but 1, then
        ///  the image will be padded to ensure the specified alignment occurs for the 
        ///  beginning of each new row.
        /// </summary>
        int Alignment { get;  }

        /// <summary>
        /// The all purpose pointer to the actual pixel information.  The pointer is
        /// IntPtr rather than any other type as it does not force or imply unsafe code.
        /// Consumers of the property can typecast to a pointer in unsafe code to access
        /// the pixel information directly.
        /// </summary>
        IntPtr Pixels { get;  }
    }
}
