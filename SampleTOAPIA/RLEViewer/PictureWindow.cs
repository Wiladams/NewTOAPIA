using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using TOAPI.Types;

using NewTOAPIA;
using NewTOAPIA.UI;
using NewTOAPIA.Drawing;
using NewTOAPIA.Imaging;

namespace TargaViewer
{
    class PictureWindow : Window
    {
        IPixelArray picture;
        PixelArray<BGRb> pixMap;
        PixelArray<BGRb> tPic;

        public PictureWindow(int locx, int locy)
            : base("Run Length Encoding Viewer", locx, locy, 640, 480)
        {
            //picture = NewTOAPIA.Drawing.TargaLoader.CreatePixelDataFromFile("marbles.tga");
            //picture = NewTOAPIA.Drawing.TargaLoader.CreatePixelDataFromFile("xing_b24.tga");
            //picture = NewTOAPIA.Drawing.TargaLoader.CreatePixelDataFromFile("xing_b32.tga");
            //picture = NewTOAPIA.Drawing.TargaLoader.CreatePixelDataFromFile("xing_t24.tga");
            //picture = NewTOAPIA.Drawing.TargaLoader.CreatePixelDataFromFile("xing_t32.tga");
            //picture = NewTOAPIA.Drawing.TargaLoader.CreatePixelDataFromFile("ctc24.tga");
            //picture = NewTOAPIA.Drawing.TargaLoader.CreatePixelDataFromFile("flag_t32.tga");
            //picture = NewTOAPIA.Drawing.TargaLoader.CreatePixelDataFromFile("flag_b32.tga");
            //picture = NewTOAPIA.Drawing.TargaLoader.CreatePixelDataFromFile("utc32.tga");
            //picture = NewTOAPIA.Drawing.TargaLoader.CreatePixelDataFromFile("utc24.tga");
            //picture = CreateColorTestImage(40,40);
            //picture = CreateSteppedTestImage(128, 32);
            picture = CreateBarsTestImage(10);

            // Perform the runlength encoding on the image
            pixMap = new PixelArray<BGRb>(picture);

            RLC rlc = new RLC();
            //NewTOAPIA.Imaging.RunLengthCodec rlc = new RunLengthCodec();
            MemoryStream ms = new MemoryStream();

            rlc.Encode(new PixelAccessorBGRb(pixMap), ms);
            byte[] imageBytes = ms.GetBuffer();


            //Console.WriteLine("Image Bytes: {0}", imageBytes.Length);

            // Now decode the bytes back into a new image
            ms.Seek(0, SeekOrigin.Begin);

            tPic = new PixelArray<BGRb>(pixMap.Width, pixMap.Height, pixMap.Orientation, new PixelInformation(PixelLayout.Bgr, PixelComponentType.Byte));
            PixelAccessorBGRb accessor = new PixelAccessorBGRb(tPic);
            rlc.Decode(ms, accessor);
        }

        PixelArray<BGRb> CreateSteppedTestImage(int width, int height)
        {
            PixelArray<BGRb> picture = new PixelArray<BGRb>(width, height);
            ColorRGBA black = new ColorRGBA(0, 0, 0);
            ColorRGBA white = new ColorRGBA(1, 1, 1);
            ColorRGBA red = new ColorRGBA(1, 0, 0);
            ColorRGBA blue = new ColorRGBA(0, 1, 0);
            ColorRGBA generic = new ColorRGBA(0, 0, 0);

            for (int y = 0; y < picture.Height; y++)
            {
                generic.Red = (float)y / picture.Height - 1.0f;
                generic.Green = 1.0f - (float)y / picture.Height;
                //generic.Blue = (float)y / picture.Height - 1.0f;

                for (int x = 0; x < picture.Width; x++)
                {
                    //if (x < width / 2)
                    //    picture.SetColor(x, y, red);
                    //else
                    //    picture.SetColor(x, y, white);

                    generic.Blue = (float)x / picture.Width - 1.0f;
                    picture.SetColor(x, y, generic);
                }
            }

            return picture;
        }

        PixelArray<BGRb> CreateBarsTestImage(int maxBar)
        {
            int width = 128;
            int height = 128;

            PixelArray<BGRb> picture = new PixelArray<BGRb>(width, height);
            ColorRGBA black = new ColorRGBA(0, 0, 0);
            ColorRGBA white = new ColorRGBA(1, 1, 1);
            ColorRGBA red = new ColorRGBA(1, 0, 0);
            ColorRGBA green = new ColorRGBA(0, 1, 0);
            ColorRGBA blue = new ColorRGBA(0, 0, 1);
            ColorRGBA generic = new ColorRGBA(0, 0, 0);

            for (int y = 0; y < picture.Height; y++)
            {
                int x = 0;
                for (int bars = 1; bars <= maxBar; bars++)
                {
                    for (int column =0; column< bars; column++)
                    {
                        picture.SetColor(x + column, y, red);
                    }
                    x += bars + 4;


                    //picture.ColorAccessor.SetColor(x, y, generic);
                    //generic.Blue = (float)x / picture.Width - 1.0f;
                }
                //generic.Red = (float)y / picture.Height - 1.0f;
                //generic.Green = 1.0f - (float)y / picture.Height;
                //generic.Blue = (float)y / picture.Height - 1.0f;
            }

            return picture;
        }

        PixelArray<BGRb> CreateColorTestImage(int width, int height)
        {
            PixelArray<BGRb> picture = new PixelArray<BGRb>(width, height);
            ColorRGBA black = new ColorRGBA(0, 0, 0);
            ColorRGBA white = new ColorRGBA(1, 1, 1);
            ColorRGBA red = new ColorRGBA(1, 0, 0);
            ColorRGBA green = new ColorRGBA(0, 1, 0);
            ColorRGBA blue = new ColorRGBA(0, 0, 1);
            ColorRGBA generic = new ColorRGBA(0, 0, 0);

            for (int y = 0; y < picture.Height; y++)
            {
                for (int x = 0; x < picture.Width; x++)
                {
                    //if (x < N / 2)
                    //    picture.ColorAccessor.SetColor(x, y, red);
                    //else
                    //    picture.ColorAccessor.SetColor(x, y, white);
                    picture.SetColor(x, y, generic);
                    //generic.Blue = (float)x / picture.Width - 1.0f;
                }
                //generic.Red = (float)y / picture.Height - 1.0f;
                //generic.Green = 1.0f - (float)y / picture.Height;
                generic.Blue = (float)y / picture.Height - 1.0f;
            }

            return picture;
        }

        public override void OnPaint(DrawEvent devent)
        {
            if ((null != picture))
            {
                // If we have an image, just draw it to the screen
                devent.GraphPort.PixBlt(picture, 10, 10);


                //devent.GraphPort.PixBlt(pixMap, 10+picture.Width + 20, 10);
                devent.GraphPort.PixBlt(tPic, 10 + picture.Width + 20, 10);
            }
        }

    }
}
