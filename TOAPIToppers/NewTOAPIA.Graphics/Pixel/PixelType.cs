namespace NewTOAPIA.Graphics
{
    public class PixelType
    {
        #region Byte Oriented
        public static PixelInformation BGRAb = new PixelInformation(PixelLayout.Bgra, PixelComponentType.UnsignedByte);
        public static PixelInformation BGRb = new PixelInformation(PixelLayout.Bgr, PixelComponentType.UnsignedByte);
        public static PixelInformation RGBAb = new PixelInformation(PixelLayout.Rgba, PixelComponentType.UnsignedByte);
        public static PixelInformation RGBb = new PixelInformation(PixelLayout.Rgb, PixelComponentType.UnsignedByte);
        public static PixelInformation Lumb = new PixelInformation(PixelLayout.Luminance, PixelComponentType.UnsignedByte);
        #endregion

        #region Short Oriented
        public static PixelInformation BGRAs = new PixelInformation(PixelLayout.Bgra, PixelComponentType.Short);
        public static PixelInformation BGRs = new PixelInformation(PixelLayout.Bgr, PixelComponentType.Short);
        public static PixelInformation RGBAs = new PixelInformation(PixelLayout.Rgba, PixelComponentType.Short);
        public static PixelInformation RGBs = new PixelInformation(PixelLayout.Rgb, PixelComponentType.Short);
        public static PixelInformation Lums = new PixelInformation(PixelLayout.Luminance, PixelComponentType.Short);
        #endregion

        #region Int Oriented
        public static PixelInformation BGRAi = new PixelInformation(PixelLayout.Bgra, PixelComponentType.Int);
        public static PixelInformation BGRi = new PixelInformation(PixelLayout.Bgr, PixelComponentType.Int);
        public static PixelInformation RGBAi = new PixelInformation(PixelLayout.Rgba, PixelComponentType.Int);
        public static PixelInformation RGBi = new PixelInformation(PixelLayout.Rgb, PixelComponentType.Int);
        public static PixelInformation Lumi = new PixelInformation(PixelLayout.Luminance, PixelComponentType.Int);
        #endregion

        #region Float Oriented
        public static PixelInformation BGRAf = new PixelInformation(PixelLayout.Bgra, PixelComponentType.Float);
        public static PixelInformation BGRf = new PixelInformation(PixelLayout.Bgr, PixelComponentType.Float);
        public static PixelInformation RGBAf = new PixelInformation(PixelLayout.Rgba, PixelComponentType.Float);
        public static PixelInformation RGBf = new PixelInformation(PixelLayout.Rgb, PixelComponentType.Float);
        public static PixelInformation Lumf = new PixelInformation(PixelLayout.Luminance, PixelComponentType.Float);
        #endregion

        #region Byte Oriented
        public static PixelInformation BGRAd = new PixelInformation(PixelLayout.Bgra, PixelComponentType.Double);
        public static PixelInformation BGRd = new PixelInformation(PixelLayout.Bgr, PixelComponentType.Double);
        public static PixelInformation RGBAd = new PixelInformation(PixelLayout.Rgba, PixelComponentType.Double);
        public static PixelInformation RGBd = new PixelInformation(PixelLayout.Rgb, PixelComponentType.Double);
        public static PixelInformation Lumd = new PixelInformation(PixelLayout.Luminance, PixelComponentType.Double);
        #endregion

    }
}