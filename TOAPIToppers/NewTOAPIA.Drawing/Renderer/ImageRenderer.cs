namespace NewTOAPIA.Drawing
{
    public class ImageRenderer : Renderer
    {
        ImageBuffer m_Owner;

        internal ImageRenderer(ImageBuffer owner)
            : base()
        {
            m_Owner = owner;

            rasterizer_scanline_aa rasterizer = new rasterizer_scanline_aa();
            ImageClippingProxy imageClippingProxy = new ImageClippingProxy(owner);

            Initialize(imageClippingProxy, rasterizer);
            ScanlineCache = new scanline_packed_8();
        }
    }

}