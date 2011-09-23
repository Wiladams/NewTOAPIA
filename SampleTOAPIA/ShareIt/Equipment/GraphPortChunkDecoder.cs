using System;
using System.IO;
using System.Drawing;

using NewTOAPIA;
using NewTOAPIA.Drawing;
using NewTOAPIA.Imaging;
using NewTOAPIA.Net.Rtp;

using TOAPI.GDI32;
using TOAPI.Types;

namespace ShowIt
{
    public class GraphPortChunkDecoder : UIPortDelegate
    {
        PixelBuffer24 fPixMap;
        PixelArray<Lumb> fGrayImage;

        #region Constructor
        public GraphPortChunkDecoder(PixelBuffer24 pixMap)
        {
            fPixMap = pixMap;
            fGrayImage = new PixelArray<Lumb>(pixMap.Width, pixMap.Height);
        }
        #endregion

        public PixelBuffer24 PixelBuffer
        {
            get { return fPixMap; }
        }

        public virtual void ReceiveChunk(BufferChunk aRecord)
        {
            // First read out the record type
            int recordType = aRecord.NextInt32();

            // Then deserialize the rest from there
            switch (recordType)
            {
                case (int)UICommands.PixBltRLE:
                    {
                        // Get the X, Y
                        int x = aRecord.NextInt32();
                        int y = aRecord.NextInt32();
                        int width = aRecord.NextInt32();
                        int height = aRecord.NextInt32();

                        // get the length of the image bytes buffer
                        int dataSize = aRecord.NextInt32();

                        byte[] imageBytes = (byte[])aRecord;

                        // Create a memory stream from the imageBytes
                        MemoryStream ms = new MemoryStream(imageBytes, false);

                        // Create a pixelArray from the stream
                        if ((width != fPixMap.Width) || (height != fPixMap.Height))
                            fPixMap = new PixelBuffer24(width, height);

                        TargaRunLengthCodec rlc = new TargaRunLengthCodec();
                        rlc.Decode(ms, fPixMap);

                        // And finally, call the local PixBlt function
                        PixBltPixelBuffer24(fPixMap, x, y);
                    }
                    break;

                case (int)UICommands.PixBltLuminance:
                    {
                        // Get the X, Y
                        int x = aRecord.NextInt32();
                        int y = aRecord.NextInt32();
                        int width = aRecord.NextInt32();
                        int height = aRecord.NextInt32();

                        // get the length of the image bytes buffer
                        int dataSize = aRecord.NextInt32();

                        byte[] imageBytes = (byte[])aRecord;

                        // Create a memory stream from the imageBytes
                        MemoryStream ms = new MemoryStream(imageBytes, false);

                        // Create a pixelArray from the stream
                        if ((width != fGrayImage.Width) || (height != fGrayImage.Height))
                            fGrayImage = new PixelArray<Lumb>(width, height);

                        TargaLuminanceRLE rlc = new TargaLuminanceRLE();
                        rlc.Decode(ms, fGrayImage);

                        // And finally, call the local PixBlt function
                        PixBltLumb(fGrayImage, x, y);
                    }
                    break;

                case (int)UICommands.Showcursor:
                    {
                        ShowCursor();
                    }
                    break;

                case (int)UICommands.HideCursor:
                    {
                        HideCursor();
                    }
                    break;

                case (int)UICommands.MoveCursor:
                    {
                        int x = aRecord.NextInt32();
                        int y = aRecord.NextInt32();

                        MoveCursor(x, y);
                    }
                    break;



                default:
                    //if (CommandReceived != null)
                    //    CommandReceived(aRecord);
                    break;
            }
        }
    }
}