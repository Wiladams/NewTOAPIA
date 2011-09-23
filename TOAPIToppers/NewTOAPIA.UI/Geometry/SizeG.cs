namespace Papyrus.Types
{
    public struct Size
    {
        private int fWidth;
        private int fHeight;

        public static readonly Size Empty = new Size();

        public Size(int width, int height)
        {
            this.fWidth = width;
            this.fHeight = height;
        }

        public int Width
        {
            get { return this.fWidth; }
            set { this.fWidth = value; }
        }

        public int Height
        {
            get { return fHeight; }
            set { fHeight = value; }
        }

        public override string ToString()
        {
            return "<Size width='" + Width.ToString() +
                "' height='" + Height.ToString() + "' />";
        }
    }
}