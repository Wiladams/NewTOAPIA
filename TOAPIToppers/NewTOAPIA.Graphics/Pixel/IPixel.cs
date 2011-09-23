namespace NewTOAPIA.Graphics
{
    using System.IO;

    public interface IPixel : IPixelInformation
    {
        //RGBA_Doubles GetAsRGBA_Doubles();
        RGBAb ToRGBAb();

        //RGBA_Bytes gradient(RGBA_Bytes c, double k);

        int Red { get; set; }
        int Green { get; set; }
        int Blue { get; set; }
        int Alpha { get; set; }

        ColorRGBA GetColor();
        void SetColor(ColorRGBA color);

        byte[] GetBytes();
        void SetBytes(byte[] bytes);
        void SetBytes(byte[] bytes, int startIndex);
    }
}