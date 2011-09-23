namespace NewTOAPIA.Graphics.Raytrace
{
    using System;
    using NewTOAPIA.Graphics;

    public abstract class Sampler1D : Sampler
    {

        public override int Dimensions
        {
            get
            {
                return 1;
            }
        }

        public abstract BGRAb GetPixel(int x);

    }
}