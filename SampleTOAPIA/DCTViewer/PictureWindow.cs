using System;
using System.Collections.Generic;
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
        PixelArray picture;
        PixelBuffer pBuffer;
        PixelArray<BGRb> tPic;

        public PictureWindow(int x, int y)
            : base("Targa Viewer", x, y, 320, 600)
        {
            //picture = NewTOAPIA.Drawing.TargaLoader.CreatePixelDataFromFile("marbles.tga");
            //picture = NewTOAPIA.Drawing.TargaLoader.CreatePixelDataFromFile("xing_b24.tga");
            //picture = NewTOAPIA.Drawing.TargaLoader.CreatePixelDataFromFile("xing_b32.tga");
            //picture = NewTOAPIA.Drawing.TargaLoader.CreatePixelDataFromFile("xing_t24.tga");
            //picture = NewTOAPIA.Drawing.TargaLoader.CreatePixelDataFromFile("xing_t32.tga");
            //picture = NewTOAPIA.Drawing.TargaLoader.CreatePixelDataFromFile("ctc24.tga");
            picture = NewTOAPIA.Drawing.TargaLoader.CreatePixelDataFromFile("flag_t32.tga");
            //picture = NewTOAPIA.Drawing.TargaLoader.CreatePixelDataFromFile("flag_b32.tga");
            //picture = NewTOAPIA.Drawing.TargaLoader.CreatePixelDataFromFile("utc32.tga");
            //picture = NewTOAPIA.Drawing.TargaLoader.CreatePixelDataFromFile("utc24.tga");

            //pBuffer = new PixelBuffer(picture);

            PixelArray<BGRb> picBuffer = new PixelArray<BGRb>(picture.Width, picture.Height, picture.Orientation, new PixelInformation(PixelLayout.Bgr, PixelComponentType.Byte));
            picBuffer.ColorAccessor.PixBlt(picture, 0, 0);

            DCT dct = new DCT();
            PixelArray<RGBd> coeff = dct.ForwardDCT(picBuffer);

            tPic = dct.InverseDCT(coeff);



        }

        public override void OnPaint(DrawEvent devent)
        {
            if ((null != picture))
            {
                // If we have an image, just draw it to the screen
                devent.GraphPort.PixBlt(picture, 0, 0);


                devent.GraphPort.PixBlt(tPic, picture.Width + 20, 0);
            }
        }

    }
}
