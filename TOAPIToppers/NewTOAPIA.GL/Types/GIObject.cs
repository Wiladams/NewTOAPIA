
namespace NewTOAPIA.GL
{
    public class GIObject
    {
        GraphicsInterface fGI;

        public GIObject(GraphicsInterface gi)
        {
            fGI = gi;
        }

        public GraphicsInterface GI
        {
            get { return fGI; }
        }
    }
}
