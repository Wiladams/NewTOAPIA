using System;


using NewTOAPIA;
using NewTOAPIA.UI;
using NewTOAPIA.Drawing;
using NewTOAPIA.Imaging;

namespace TargaViewer
{
    class PictureWindow : Window
    {
        IPixelArray picture;
        IPixelArray copyBuffer;

        public PictureWindow(int x, int y)
            : base("Targa Viewer", x, y, 320, 600)
        {
            //picture = TargaLoader.CreatePixelArrayFromFile("marbles.tga");
            //picture = TargaLoader.CreatePixelArrayFromFile("xing_b24.tga");
            //picture = TargaLoader.CreatePixelArrayFromFile("xing_b32.tga");
            //picture = TargaLoader.CreatePixelArrayFromFile("xing_t24.tga");
            //picture = TargaLoader.CreatePixelArrayFromFile("xing_t32.tga");
            //picture = TargaLoader.CreatePixelArrayFromFile("ctc24.tga");
            //picture = TargaLoader.CreatePixelArrayFromFile("flag_t32.tga");
            //picture = TargaLoader.CreatePixelArrayFromFile("flag_b32.tga");
            //picture = TargaLoader.CreatePixelDataFromFile("utc32.tga");
            //picture = TargaLoader.CreatePixelDataFromFile("utc24.tga");

            // Create a copy of the original
            copyBuffer = (IPixelArray)picture.Clone();


            //UnaryColorOperations.SetLuminance op = new UnaryColorOperations.SetLuminance();
            //UnaryColorOperations.SetColor op = new UnaryColorOperations.SetColor(new ColorRGBA(0.5f,1,0.5f));
            //UnaryColorOperations.NOT op = new UnaryColorOperations.NOT();
            //op.PerformUnaryOperation(picture.ColorAccessor);

            // Create an alpha fade from left to right
            PixelArray<BGRAb> alphaBuffer = CreateAlphaMask(picture.Width, picture.Height);
            IBinaryColorOperator binop = new ApplySourceAlpha();
            copyBuffer.ApplyBinaryColorOperator(binop, alphaBuffer);

            // Subtract the alpha fade from the copied image and display the copy
            //binop = new ColorBinaryOperations.SubtractSource();
            //binop.PerformBinaryOperation(copyBuffer.ColorAccessor, picture);

        }

        PixelArray<BGRAb> CreateAlphaMask(int width, int height)
        {
            PixelArray<BGRAb> alphaMask = new PixelArray<BGRAb>(width, height);
            for (int row = 0; row < height; row++)
                for (int column = 0; column < width; column++)
                {
                    BGRAb pixel = alphaMask.RetrievePixel(column, row);
                    float fraction = (float)column / copyBuffer.Width;
                    pixel.Alpha = (byte)(255 - fraction * 255);
                    alphaMask.AssignPixel(column, row, pixel);
                }

            return alphaMask;
        }

        public override void OnPaint(DrawEvent devent)
        {
            if ((null != picture))
            {
                // Draw the original and modified image to the screen
                devent.GraphPort.PixBlt(picture, 0, 0);
                devent.GraphPort.PixBlt(copyBuffer, 0, picture.Height + 10);
            }
        }

    }
}
