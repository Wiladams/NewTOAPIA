namespace Papyrus.Types
{
    public struct SizeG
    {
        private int fWidth;
        private int fHeight;

        public static readonly SizeG Empty = new SizeG();

        public SizeG(int width, int height)
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
            return "<SizeG width='" + Width.ToString() +
                "' height='" + Height.ToString() + "' />";
        }
    }
}