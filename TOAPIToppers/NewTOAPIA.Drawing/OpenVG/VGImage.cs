namespace NewTOAPIA.Drawing
{
    using VGbitfield = System.UInt32;
    using VGint = System.Int32;

    public class VGImage
    {
        public VGImageFormat Format {get; set;}
        public int Width {get; private set;}
        public int Height {get; private set;}


        public VGImage(VGImageFormat format, int width, int height, VGbitfield allowedQuality)
        {
            this.Format = format;
            this.Width = width;
            this.Height = height;
        }

        public virtual void ClearImage(int x, int y, int width, int height, uint color)
        {
        }

    }
}