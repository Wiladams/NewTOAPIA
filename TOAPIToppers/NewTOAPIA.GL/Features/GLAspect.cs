
namespace NewTOAPIA.GL
{
    public class GLAspect : IRealizable
    {
        GraphicsInterface fGI;

        public GLAspect(GraphicsInterface gi)
        {
            fGI = gi;
        }

        public GraphicsInterface GI
        {
            get { return fGI; }
        }

        public virtual void Realize()
        {
        }
    }
}
