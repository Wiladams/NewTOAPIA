using System;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;

using NewTOAPIA;
using NewTOAPIA.Drawing;

namespace NewTOAPIA.Drawing
{
    // Targa Image file handling
    // Some reference documentation can be found here:
    // http://www.fileformat.info/format/tga/egff.htm#TGA-DMYID.2

    #region Header Information
    /// <summary>
    /// 
    /// </summary>
    public enum TargaColorMapType : byte
    {
        NoPalette = 0,
        Palette = 1
    }

    enum ImageOrigin : byte
    {
        OriginMask = 0x30,
        BottomLeft = 0x00,
        BottomRight = 0x10,
        TopLeft = 0x20,
        TopRight = 0x30
    }

    enum HorizontalOrientation : byte
    {
        LeftToRight,
        RightToLeft
    }

    public enum TargaImageType : byte
    {
        NoImageData = 0,
        ColorMapped = 1,
        TrueColor = 2,
        Monochrome = 3,
        ColorMappedCompressed = 9,
        TrueColorCompressed = 10,
        MonochromeCompressed = 11,
    }

    public struct TargaHeader
    {
        public byte IDLength;              /* 00h  Size of Image ID field */
        public TargaColorMapType ColorMapType;          /* 01h  Color map type */
        public TargaImageType ImageType;   /* 02h  Image type code */
        public short CMapStart;            /* 03h  Color map origin */
        public short CMapLength;           /* 05h  Color map length */
        public byte CMapDepth;             /* 07h  Depth of color map entries */
        public short XOffset;              /* 08h  X origin of image */
        public short YOffset;              /* 0Ah  Y origin of image */
        public short Width;                // 0Ch  Width of image - Maximum 512
        public short Height;               // 0Eh  Height of image - Maximum 482
        public byte PixelDepth;            /* 10h  Image pixel size */
        public byte ImageDescriptor;       /* 11h  Image descriptor byte */
    };

    public class TargaFooter
    {
        public int ExtensionAreaOffset;        // Bytes 0-3: The Extension Area Offset
        public int DeveloperDirectoryOffset;   // Bytes 4-7: The Developer Directory Offset
        public string Signature;               // Bytes 8-23: The Signature
        public byte Period;                    // Byte 24: ASCII Character “.”
        public byte BinaryZero;                // Byte 25: Binary zero string terminator (0x00)
    };
    #endregion

    public class TargaLoader
    {
        public static IPixelArray CreatePixelDataFromFile(string filename)
        {
            int tgaSize;
            bool isExtendedFile;

            // Open the file.
            if ((null == filename) || (string.Empty == filename))
                return null;

            FileStream filestream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(filestream);

            // Targa images come in many different formats, and there are a couple of different versions
            // of the specification.
            // First thing to do is determine if the file is adhereing to version 2.0 of the spcification.
            // We do that by reading a 'footer', which is the last 26 bytes of the file.
            long fileLength = filestream.Length;
            long seekPosition = filestream.Seek(fileLength - 26, SeekOrigin.Begin);
            byte[] targaFooterBytes = reader.ReadBytes(26);
            string targaXFileID = "TRUEVISION-XFILE";
            string targaFooterSignature = System.Text.ASCIIEncoding.ASCII.GetString(targaFooterBytes, 8, 16);
            TargaFooter footer = null;
            isExtendedFile = (0 == string.Compare(targaFooterSignature, targaXFileID));
            if (isExtendedFile)
            {
                // Since we now know it's an extended file, 
                // we'll create the footer object and fill
                // in the details.
                footer = new TargaFooter();

                // Of the 26 bytes we read from the end of the file
                // the bytes are layed out as follows.
                //Bytes 0-3: The Extension Area Offset
                //Bytes 4-7: The Developer Directory Offset
                //Bytes 8-23: The Signature
                //Byte 24: ASCII Character “.”
                //Byte 25: Binary zero string terminator (0x00)

                // We take those raw bytes, and turn them into meaningful fields
                // in the footer object.
                footer.ExtensionAreaOffset = BitConverter.ToInt32(targaFooterBytes, 0);
                footer.DeveloperDirectoryOffset = BitConverter.ToInt32(targaFooterBytes, 4);
                footer.Signature = targaFooterSignature;
                footer.Period = (byte)'.';
                footer.BinaryZero = 0;

            }

            TargaHeader fHeader = new TargaHeader();

            // If you want to use unsafe code, you could do the following
            // there are two primary drawbacks.
            // 1. The targa image data is in Little-Endian format.  On platforms
            // that are big-endian, the data will not necessarily show up correctly in the header.
            // 2. You must compile with unsafe code.  That may or may not be a problem depending
            // on the application, but it really isn't a necessity.
            // 
            // The speed gain may not be realized, so there's really no reason for the added complexity.
            // First, just reader enough of bytes from the file to capture the 
            // header into a chunk of memory.
            //byte[] headerBytes = reader.ReadBytes(Marshal.SizeOf(fHeader));
            //IntPtr headerPtr;

            //unsafe
            //{
            //    GCHandle dataHandle = GCHandle.Alloc(headerBytes, GCHandleType.Pinned);
            //    try
            //    {
            //        headerPtr = (IntPtr)dataHandle.AddrOfPinnedObject();

            //        // Now use the marshaller to copy the header bytes into a structure
            //        fHeader = (TargaHeader)Marshal.PtrToStructure(headerPtr, typeof(TargaHeader));

            //    }
            //    finally
            //    {
            //        dataHandle.Free();
            //    }
            //}

            filestream.Seek(0, SeekOrigin.Begin);
            fHeader.IDLength = reader.ReadByte();
            fHeader.ColorMapType = (TargaColorMapType)reader.ReadByte();
            fHeader.ImageType = (TargaImageType)reader.ReadByte();
            fHeader.CMapStart = reader.ReadInt16();
            fHeader.CMapLength = reader.ReadInt16();
            fHeader.CMapDepth = reader.ReadByte();


            // Image description
            fHeader.XOffset = reader.ReadInt16();
            fHeader.YOffset = reader.ReadInt16();
            fHeader.Width = reader.ReadInt16();         // Width of image in pixels
            fHeader.Height = reader.ReadInt16();        // Height of image in pixels
            fHeader.PixelDepth = reader.ReadByte();     // How many bits per pixel
            fHeader.ImageDescriptor = reader.ReadByte();


            //              Image Descriptor Byte.                        |
            //  Bits 3-0 - number of attribute bits associated with each  |
            //               pixel.  For the Targa 16, this would be 0 or |
            //               1.  For the Targa 24, it should be 0.  For   |
            //               Targa 32, it should be 8.                    |
            //  Bit 4    - controls left/right transfer of pixels to 
            ///             the screen.
            ///             0 = left to right
            ///             1 = right to left
            //  Bit 5    - controls top/bottom transfer of pixels to 
            ///             the screen.
            ///             0 = bottom to top
            ///             1 = top to bottom
            ///             
            ///             In Combination bits 5/4, they would have these values
            ///             00 = bottom left
            ///             01 = bottom right
            ///             10 = top left
            ///             11 = top right
            ///             
            //  Bits 7-6 - Data storage interleaving flag.                |
            //             00 = non-interleaved.                          |
            //             01 = two-way (even/odd) interleaving.          |
            //             10 = four way interleaving.                    |
            //             11 = reserved.         

            byte desc = fHeader.ImageDescriptor;
            byte attrBits = (byte)(desc & 0x0F);
            ImageOrigin origin = (ImageOrigin)(desc & (byte)ImageOrigin.OriginMask);
            byte interleave = (byte)((desc & 0xC0) >> 6);


            // This routine can only deal with the uncompressed image types.
            // So, fail if this is not the case.
            if ((TargaImageType.TrueColor != fHeader.ImageType) && (TargaImageType.Monochrome != fHeader.ImageType))
            {
                filestream.Close();
                return null;
            }


            PixmapOrientation pixmapOrientation = PixmapOrientation.BottomToTop;
            if ((ImageOrigin.BottomLeft == origin) || (ImageOrigin.BottomRight == origin))
                pixmapOrientation = PixmapOrientation.BottomToTop;
            else
                pixmapOrientation = PixmapOrientation.TopToBottom;

            int bytesPerPixel = fHeader.PixelDepth / 8;


            // Skip past the Image Identification field
            byte[] ImageIdentification;
            if (fHeader.IDLength > 0)
                ImageIdentification = reader.ReadBytes(fHeader.IDLength);

            // calculate image size based on bytes per pixel, width and height.
            tgaSize = fHeader.Width * fHeader.Height * bytesPerPixel;
            byte[] imageData = reader.ReadBytes((int)tgaSize);
            filestream.Close();

            // Pin the array in mememory, and get a data pointer to it
            IntPtr dataPtr;

            unsafe
            {
                GCHandle dataHandle = GCHandle.Alloc(imageData, GCHandleType.Pinned);
                dataPtr = (IntPtr)dataHandle.AddrOfPinnedObject();

                // Create the appropriate PixelArray
                // then create an accessor to match
                // Copy the data from the buffer pointer to the new array
                switch (bytesPerPixel)
                {
                    case 3:
                        {
                            PixelAccessorBGRb srcAccess = new PixelAccessorBGRb(fHeader.Width, fHeader.Height, pixmapOrientation, dataPtr);
                            PixelArray<BGRb> pixmap = new PixelArray<BGRb>(srcAccess);

                            return pixmap;
                        }
                        break;

                    case 4:
                        {
                            PixelAccessorBGRAb srcAccess = new PixelAccessorBGRAb(fHeader.Width, fHeader.Height, pixmapOrientation, dataPtr);
                            PixelArray<BGRAb> pixmap = new PixelArray<BGRAb>(srcAccess);

                            return pixmap;
                        }
                        break;

                    case 1:
                        {
                            PixelAccessorLumb srcAccess = new PixelAccessorLumb(fHeader.Width, fHeader.Height, pixmapOrientation, dataPtr);
                            PixelArray<Lumb> pixmap = new PixelArray<Lumb>(srcAccess);

                            return pixmap;
                        }
                        break;
                }
            }

            return null;
        }
    }
}




