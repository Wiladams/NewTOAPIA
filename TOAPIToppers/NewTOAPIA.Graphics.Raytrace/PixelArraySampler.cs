namespace NewTOAPIA.Graphics.Raytrace
{
    using NewTOAPIA.Graphics;

    public class PixelArraySampler : Sampler2D
    {
        IRetrievePixel PixelArray { get; set; }

        public PixelArraySampler(IRetrievePixel pixArray)
        {
            PixelArray = pixArray;
        }

        public override IPixel GetPixel(int x, int y)
        {
            return PixelArray.GetPixel(x, y);
        }

    }
}