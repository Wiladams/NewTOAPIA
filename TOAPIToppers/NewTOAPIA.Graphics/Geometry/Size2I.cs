namespace NewTOAPIA.Graphics
{
    public struct Size2I
    {
        public int width;
        public int height;

        public Size2I(int width_, int height_)
        {
            width = width_;
            height = height_;
        }

        public int Width
        {
            get { return this.width; }
        }

        public int Height
        {
            get { return this.height; }
        }
    }
}