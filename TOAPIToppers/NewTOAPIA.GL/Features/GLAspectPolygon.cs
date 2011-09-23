
namespace NewTOAPIA.GL
{
    public class GLAspectPolygon : GLAspect
    {
        GLState fState;
        GLFeaturePolygonSmooth fSmoothing;

        public GLAspectPolygon(GraphicsInterface gi)
            : base(gi)
        {
            fState = new GLState(gi);
            fSmoothing = new GLFeaturePolygonSmooth(gi);
        }

        public GLFeaturePolygonSmooth Smoothing
        {
            get { return fSmoothing; }
        }
    }
}
