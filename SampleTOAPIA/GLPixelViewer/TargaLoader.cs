using System;
using System.IO;

using NewTOAPIA.Drawing;
using NewTOAPIA.GL;

namespace PixelViewer
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
    #endregion

    public class TargaHandler
    {
        public static GLPixelData CreatePixelDataFromFile(string filename)
        {
            int tgaSize;

            // Open the file.
            if ((null == filename) || (string.Empty == filename))
                return null;

            FileStream filestream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(filestream);

            // Read the header.
            TargaHeader fHeader;
            fHeader.IDLength = reader.ReadByte();
            fHeader.ColorMapType = (TargaColorMapType)reader.ReadByte();
            fHeader.ImageType = (TargaImageType)reader.ReadByte();
            fHeader.CMapStart = reader.ReadInt16();
            fHeader.CMapLength = reader.ReadInt16();
            fHeader.CMapDepth = reader.ReadByte();
            fHeader.XOffset = reader.ReadInt16();
            fHeader.YOffset = reader.ReadInt16();
            fHeader.Width = reader.ReadInt16();
            fHeader.Height = reader.ReadInt16();
            fHeader.PixelDepth = reader.ReadByte();
            fHeader.ImageDescriptor = reader.ReadByte();


            int bytesPerPixel = fHeader.PixelDepth / 8;
            
            // This routine can only deal with the uncompressed image types.
            // So, fail if this is not the case.
            if ((TargaImageType.TrueColor != fHeader.ImageType) && (TargaImageType.Monochrome != fHeader.ImageType))
            {
                filestream.Close();
                return null;
            }

            //// calculate image size based on bytes per pixel, width and height.
            GLPixelFormat pFormat = GLPixelFormat.Bgr;
            TextureInternalFormat pIntFormat = TextureInternalFormat.Rgb8;

            switch (bytesPerPixel)
            {
                case 3:
                    pFormat = GLPixelFormat.Bgr;
                    pIntFormat = TextureInternalFormat.Rgb8;
                    break;

                case 4:
                    pFormat = GLPixelFormat.Bgra;
                    pIntFormat = TextureInternalFormat.Rgba8;
                    break;

                case 1:
                    pFormat = GLPixelFormat.Luminance;
                    pIntFormat = TextureInternalFormat.Luminance8;
                    break;
            }

            tgaSize = fHeader.Width * fHeader.Height * bytesPerPixel;

            // Load the actual image data

            byte[] imageData = reader.ReadBytes((int)tgaSize);

            // For 32 bit Targa files, the alpha byte is set to zero.
            // So, we'll subtract from 255 and set that as the alpha value.
            //byte tempColor;
            if (bytesPerPixel == 4)
            {
                for (long index = 0; index < tgaSize; index += bytesPerPixel)
                {
                    //    tempColor = imageData[index];
                    //    imageData[index] = imageData[index + 2];
                    //    imageData[index + 2] = tempColor;
                    imageData[index + 3] = (byte)(255 - imageData[index + 3]);
                }
            }

            filestream.Close();

            GLPixelData pixeldata = new GLPixelData(fHeader.Width, fHeader.Height, pIntFormat, pFormat, PixelType.UnsignedByte, imageData);

            return pixeldata;
        }

        //public void WritePixelDataToFile(GLPixelData pixMap, string filename)
        //{
        //    FileStream filestream = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);
        //    BinaryWriter writer = new BinaryWriter(filestream);

        //    int imageSize = pixMap.Data.Length;

        //    TargaHeader tgaHeader = new TargaHeader();
        //    tgaHeader.IDLength = 0;              /* 00h  Size of Image ID field */
        //    tgaHeader.ColorMapType = TargaColorMapType.NoPalette;          /* 01h  Color map type */
        //    tgaHeader.ImageType = TargaImageType.TrueColor;   /* 02h  Image type code */
        //    tgaHeader.CMapStart = 0;            /* 03h  Color map origin */
        //    tgaHeader.CMapLength = 0;           /* 05h  Color map length */
        //    tgaHeader.CMapDepth = 0;             /* 07h  Depth of color map entries */
        //    tgaHeader.XOffset = 0;              /* 08h  X origin of image */
        //    tgaHeader.YOffset = 0;              /* 0Ah  Y origin of image */
        //    tgaHeader.Width = (short)pixMap.Width;                // 0Ch  Width of image - Maximum 512
        //    tgaHeader.Height = (short)pixMap.Height;               // 0Eh  Height of image - Maximum 482
        //    tgaHeader.PixelDepth = 32;            /* 10h  Image pixel size */
        //    tgaHeader.ImageDescriptor = 0;       /* 11h  Image descriptor byte */

        //    switch (pixMap.PixelFormat)
        //    {
        //        case GLPixelFormat.Bgr:
        //        case GLPixelFormat.Rgb:
        //            tgaHeader.PixelDepth = 24;
        //            break;

        //        case GLPixelFormat.Bgra:
        //        case GLPixelFormat.Rgba:
        //            tgaHeader.PixelDepth = 32;    
        //        break;

        //        case GLPixelFormat.Luminance:
        //            tgaHeader.PixelDepth = 8;
        //        break;
        //    }

        //    // Write the header
        //    //fHeader.IDLength = reader.ReadByte();
        //    writer.Write((byte)tgaHeader.IDLength);

        //    //fHeader.ColorMapType = (TargaColorMapType)reader.ReadByte();
        //    writer.Write((byte)tgaHeader.ColorMapType);

        //    //fHeader.ImageType = (TargaImageType)reader.ReadByte();
        //    writer.Write((byte)tgaHeader.ImageType);

        //    //fHeader.CMapStart = reader.ReadInt16();
        //    writer.Write((Int16)tgaHeader.CMapStart);

        //    //fHeader.CMapLength = reader.ReadInt16();
        //    writer.Write((Int16)tgaHeader.CMapLength);

        //    //fHeader.CMapDepth = reader.ReadByte();
        //    writer.Write((byte)tgaHeader.CMapDepth);
            
        //    //fHeader.XOffset = reader.ReadInt16();
        //    writer.Write((Int16)tgaHeader.XOffset);

        //    //fHeader.YOffset = reader.ReadInt16();
        //    writer.Write((Int16)tgaHeader.YOffset);

        //    //fHeader.Width = reader.ReadInt16();
        //    writer.Write((Int16)tgaHeader.Width);

        //    //fHeader.Height = reader.ReadInt16();
        //    writer.Write((Int16)tgaHeader.Height);

        //    //fHeader.PixelDepth = reader.ReadByte();
        //    writer.Write((byte)tgaHeader.PixelDepth);

        //    //fHeader.ImageDescriptor = reader.ReadByte();
        //    writer.Write((byte)tgaHeader.ImageDescriptor);


        //    // Write out the actual pixel data
        //    //writer.Write(pixMap.Pixels, 0, imageSize);

        //    // Close the streams
        //    writer.Close();
        //    filestream.Close();
        //}
    }
}




