

namespace Processing
{
    public class PGraphics
    {
        PRenderer Renderer { get; set; }

        public PGraphics()
        {
        }

        public PGraphics(int width, int height, PRenderer renderer_)
        {
            Renderer = renderer_;
        }

        public virtual void beginDraw()
        {
        }

        public virtual void endDraw()
        {
        }
    }
}
