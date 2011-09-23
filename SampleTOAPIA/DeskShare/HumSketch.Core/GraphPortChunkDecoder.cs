using System;
using System.IO;
using System.Drawing;

using NewTOAPIA;
using NewTOAPIA.Drawing;
using NewTOAPIA.Imaging;
using NewTOAPIA.Net.Rtp;

//using TOAPI.GDI32;
//using TOAPI.Types;

namespace DistributedDesktop
{
    public class GraphPortChunkDecoder : UIPortDelegate
    {
        GDIDIBSection fPixMap;
        PixelArray<Lumb> fGrayImage;

        PayloadChannel fChannel;

        #region Constructor
        public GraphPortChunkDecoder(GDIDIBSection pixMap, PayloadChannel channel)
        {
            fGrayImage = new PixelArray<Lumb>(pixMap.Width, pixMap.Height);
            fChannel = channel;
            fChannel.FrameReceivedEvent += FrameReceived;

            fPixMap = pixMap;
        }
        #endregion

        public GDIDIBSection PixelBuffer
        {
            get { return fPixMap; }
        }

        private void FrameReceived(object sender, RtpStream.FrameReceivedEventArgs ea)
        {
            ReceiveChunk(ea.Frame);
        }

        public virtual void ReceiveChunk(BufferChunk aRecord)
        {
            // First read out the record type
            int recordType = aRecord.NextInt32();

            // Then deserialize the rest from there
            switch (recordType)
            {

                //case (int)UICommands.PixBlt:
                //    {
                //        // Get the X, Y
                //        int x = aRecord.NextInt32();
                //        int y = aRecord.NextInt32();

                //        // get the length of the image bytes buffer
                //        int dataSize = aRecord.NextInt32();

                //        byte[] imageBytes = (byte[])aRecord;

                //        // Create a memory stream from the imageBytes
                //        MemoryStream ms = new MemoryStream(imageBytes, false);

                //        // Create a pixelArray from the stream
                //        Bitmap bm = (Bitmap)Bitmap.FromStream(ms);

                //        PixelArray<BGRAb> pixMap = PixelBufferHelper.CreatePixelArrayFromBitmap(bm);
                        
                //        // And finally, call the PixBlt function
                //        PixBltBGRAb(pixMap, x, y);
                //    }
                //    break;

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
                            fPixMap = new GDIDIBSection(width, height);

                        TargaRunLengthCodec rlc = new TargaRunLengthCodec();
                        PixelAccessor<BGRb> accessor = new PixelAccessor<BGRb>(fPixMap.Width, fPixMap.Height, fPixMap.Orientation, fPixMap.Pixels, fPixMap.BytesPerRow);
                        rlc.Decode(ms, accessor);

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




                default:
                    //if (CommandReceived != null)
                    //    CommandReceived(aRecord);
                    break;
            }
        }
    }
}