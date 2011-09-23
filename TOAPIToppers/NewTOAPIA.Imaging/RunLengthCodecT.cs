using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NewTOAPIA.Imaging
{
    using NewTOAPIA.Graphics;
    using NewTOAPIA.Drawing;

    public class TargaRunLengthCodec<T>
        where T : IPixel, IEquatable<T>, new()
    {
        enum PacketType
        {
            Raw = 0,
            Rle = 0x80,
        }

        /// <summary>
        /// Determine the packet type that appears in the image.  The type is either
        /// Raw, or Rle.  Raw means that the next color is different than the current one.
        /// Rle means the next color is the same as the current one.
        /// </summary>
        /// <param name="pixMap"></param>
        /// <param name="row"></param>
        /// <param name="pos"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        static PacketType RLEPacketType(PixelAccessor<T> pixMap, int row, int pos, int width)
        {
            if (pos == width - 1)
                return PacketType.Raw; // single pixel, so return 'Raw' immediately

            T pixel = pixMap.RetrievePixel(pos, row);
            T nextPixel = pixMap.RetrievePixel(pos + 1, row);
            bool same = pixel.Equals(nextPixel);

            if (same) // duplicate pixel, so Run Length Encode
            {
                return PacketType.Rle;
            }

            return PacketType.Raw;
        }


        /// <summary>
        /// Find the length of the current packet.  It is either a Rle run, where
        /// we're repeating some color, or it's a Raw run, where every pixel is different.
        /// </summary>
        /// <param name="pixMap"></param>
        /// <param name="row"></param>
        /// <param name="pos"></param>
        /// <param name="width"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        static int RLEPacketLength(PixelAccessor<T> pixMap, int row, int pos, int width, PacketType type)
        {
            int len = 2;

            // We're at the last pixel in the row,
            // so return '1' immediately.
            if (pos == width - 1)
                return 1;

            // Same for the second to last pixel in the row
            // if it's raw, or Rle, this will be correct
            if (pos == width - 2)
                return 2;

            if (PacketType.Rle == type)
            {
                while (pos + len < width)
                {
                    T pixel = pixMap.RetrievePixel(pos, row);
                    T nextPixel = pixMap.RetrievePixel(pos + len, row);
                    bool same = pixel.Equals(nextPixel);

                    if (same)
                        len++;
                    else
                        return len;

                    if (len == 128)
                        return 128;
                }
            }
            else // type == Raw
            {
                while (pos + len < width)
                {
                    if (RLEPacketType(pixMap, row, pos + len, width) == PacketType.Raw)
                        len++;
                    else
                        return len;

                    if (len == 128)
                        return 128;
                }
            }

            return len; // hit end of row (width)
        }

        /// <summary>
        /// Write one row of the image into the stream using the Targa Rle compression technique.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="src"></param>
        /// <param name="row"></param>
        static void WriteRLERow(Stream stream, PixelAccessor<T> src, int row)
        {
            int pos = 0;

            while (pos < src.Width)
            {
                PacketType type = RLEPacketType(src, row, pos, src.Width);
                int len = RLEPacketLength(src, row, pos, src.Width, type);
                byte packet_header;

                packet_header = (byte)(len - 1);
                if (PacketType.Rle == type)
                    packet_header |= 0x80;

                stream.WriteByte(packet_header);

                if (PacketType.Rle == type)
                {
                    // Write a single pixel
                    T pixel = src.RetrievePixel(pos, row);
                    byte[] bytes = pixel.GetBytes();
                    stream.Write(bytes, 0, bytes.Length);
                }
                else // type is Raw
                {
                    // Write a series of pixels starting at the
                    // 'pos' and running for 'len' bytes.
                    for (int column = 0; column < len; column++)
                    {
                        T pixel = src.RetrievePixel(pos + column, row);
                        byte[] bytes = pixel.GetBytes();
                        stream.Write(bytes, 0, bytes.Length);
                    }
                }

                pos += len;
            }
        }

        public void Encode(PixelAccessor<T> pixMap, Stream stream)
        {
            for (int y = 0; y < pixMap.Height; y++)
            {
                WriteRLERow(stream, pixMap, y);
            }
        }

        //public void Encode(PixelAccessor<T> accessor, Stream stream)
        //{
        //    //PixelAccessor<T> accessor = new PixelAccessor<T>(pixMap);
        //    Encode(accessor, stream);
        //}

        public void Decode(Stream stream, PixelAccessor<T> pixMap)
        {
            // For each row, we want to walk the pixels incrementing 
            // a count of whichever pixels are the same in a run, up 
            // to 128.
            int runLength = 0;
            T nextPixel = new T();
            bool isCompressed = false;
            bool skip;
            byte headerbyte = 0;

            int bytesPerPixel = PixelInformation.GetBytesPerPixel(nextPixel.Layout, nextPixel.ComponentType);
            byte[] buffer = new byte[bytesPerPixel];

            for (int y = 0; y < pixMap.Height; y++)
            {
                for (int x = 0; x < pixMap.Width; x++)
                {
                    if (runLength > 0)
                    {
                        runLength--;
                        skip = isCompressed;
                    }
                    else
                    {
                        headerbyte = (byte)stream.ReadByte();
                        // If the high order bit of the first byte is a '1', then
                        // the next byte represents a compressed run, otherwise it is a raw run
                        isCompressed = ((headerbyte & 0x80) == 0x80);
                        runLength = headerbyte & 0x7f;
                        skip = false;
                    }

                    if (!skip)
                    {
                        int bytesRead = stream.Read(buffer, 0, bytesPerPixel);
                        nextPixel.SetBytes(buffer);
                    }

                    pixMap.AssignPixel(x, y, nextPixel);
                }
            }
        }

        //public void Decode(Stream stream, PixelArray<T> pixMap)
        //{
        //    PixelAccessor<T> accessor = new PixelAccessor<T>(pixMap);
        //    Decode(stream, accessor);
        //}
    }
}

