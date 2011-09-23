using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NewTOAPIA.Imaging
{
    public class RLC
    {
        public void WriteCompressedRun(PixelAccessor<BGRb> pixMap, MemoryStream stream, int x, int y, int length)
        {
            byte headerbyte = 0x80;  // Start with the 7th bit set to indicate a compressed run
            headerbyte |= (byte)(length - 1);    // Add in the runLength - 1;
            
            // write the header byte
            stream.WriteByte(headerbyte);

            // write the pixel value just once
            BGRb pixel = pixMap.GetPixel(x, y);
            stream.WriteByte(pixel.Red);
            stream.WriteByte(pixel.Green);
            stream.WriteByte(pixel.Blue);
        }

        public void WriteRawRun(PixelAccessor<BGRb> pixMap, MemoryStream stream, int x, int y, int length)
        {
            byte headerbyte = (byte)(length-1);

            // write the header byte
            stream.WriteByte(headerbyte);

            for (int column = 0; column < length; column++)
            {
                BGRb pixel = pixMap.GetPixel(x+column, y);
                stream.WriteByte(pixel.Red);
                stream.WriteByte(pixel.Green);
                stream.WriteByte(pixel.Blue);
            }
        }

        public void Encode(PixelAccessor<BGRb> pixMap, MemoryStream stream)
        {
            BGRb nextPixel = new BGRb();
            BGRb lastPixel = new BGRb();

            for (int y = 0; y < pixMap.Height; y++)
            {
                int sentinelIndex = 0;
                
                int runLength = 0;
                byte headerbyte = 0; // 7th bit indicates a RLE packet
                //bool endOfRun = false;
                bool isCompressed = false;
                bool canCompress = false;

                for (int x = 0; x < pixMap.Width; x++)
                {
                   nextPixel = pixMap.RetrievePixel(x, y);

                    if (runLength > 0)
                    {
                        lastPixel = pixMap.RetrievePixel(x - 1, y);

                        // Check the next pixel against the last pixel to determine if it's the same
                        // or not.
                        if ((lastPixel.Red == nextPixel.Red) && (lastPixel.Green == nextPixel.Green) && (lastPixel.Blue == nextPixel.Blue))
                            canCompress = true;
                        else
                            canCompress = false;

                    }


                    if (canCompress)
                    {
                        // we're already on a compression run, and we can 
                        // continue, so we increment the runlength
                        if (isCompressed)
                        {
                            runLength++;
                            if (x == pixMap.Width - 1)
                            {
                                WriteCompressedRun(pixMap, stream, sentinelIndex, y, runLength);

                                isCompressed = false;
                                sentinelIndex = x;
                                runLength = 1;
                            }
                        }
                        else
                        {
                            if (runLength > 0)
                            {
                                WriteRawRun(pixMap, stream, sentinelIndex, y, runLength);
                                runLength = 1;
                                sentinelIndex = x;

                            }
                            else
                            {
                                sentinelIndex = x - 1;
                                isCompressed = true;
                                runLength++;
                            }
                        }
                    }
                    else
                    {
                        if (isCompressed)
                        {
                            // We're at the end of a compressed run
                            WriteCompressedRun(pixMap, stream, sentinelIndex, y, runLength);

                            isCompressed = false;
                            sentinelIndex = x;
                            runLength = 1;
                        }
                        else
                        {
                            if (runLength < 128)
                                runLength++;

                            if (runLength == 128 || (x == pixMap.Width - 1))
                            {
                                WriteRawRun(pixMap, stream, sentinelIndex, y, runLength);
                                runLength = 1;
                                sentinelIndex = x;
                            }
                        }
                    }                    
                }
            }
        }

        public void Decode(Stream stream, PixelAccessor<BGRb> pixMap)
        {
            // For each row, we want to walk the pixels incrementing 
            // a count of whichever pixels are the same in a run, up 
            // to 128.
            int runLength = 0;
            int offset = 0;
            BGRb nextPixel = new BGRb();
            bool isCompressed = false;
            bool skip;
            byte headerbyte = 0;

            for (int y = 0; y < pixMap.Height; y++)
            {
                for (int x = 0; x < pixMap.Width; x++ )
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
                        // Get the actual data from the stream
                        byte red = (byte)stream.ReadByte();
                        byte green = (byte)stream.ReadByte();
                        byte blue = (byte)stream.ReadByte();

                        // Create a pixel to represent the data
                        nextPixel.Red = red;
                        nextPixel.Green = green;
                        nextPixel.Blue = blue;
                    }

                    pixMap.AssignPixel(x, y, nextPixel);
                }
            }
        }
    }
}
