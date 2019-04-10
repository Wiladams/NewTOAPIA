namespace NewTOAPIA.Graphics
{
    public struct Size2D
    {
        public double width;
        public double height;

        public Size2D(double width_, double height_)
        {
            width = width_;
            height = height_;
        }

        public double Width
        {
            get { return this.width; }
            set { this.width = value; }
        }

        public double Height
        {
            get { return this.height; }
            set { this.height = value; }
        }
    }

    public struct Size2F
    {
        public double width;
        public double height;

        public Size2F(double width_, double height_)
        {
            width = width_;
            height = height_;
        }
    }
}