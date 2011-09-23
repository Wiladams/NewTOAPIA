namespace NewTOAPIA.Graphics.Raytrace
{
    using NewTOAPIA.Graphics;

    public abstract class Sampler2D : Sampler
    {
        //public int Width { get; protected set; }
        //public int Height { get; protected set; }

        public override int Dimensions
        {
            get
            {
                return 2;
            }
        }

        public abstract IPixel GetPixel(int x, int y);

    }
}