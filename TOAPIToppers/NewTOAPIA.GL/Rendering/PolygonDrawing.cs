
namespace NewTOAPIA.GL
{
    public class BracketPolygonDrawing : GLBracketDrawing
    {
        GLAspectPolygon fPolyAspects;

        public BracketPolygonDrawing(GraphicsInterface gi)
            : base(gi, BeginMode.Polygon)
        {
            fPolyAspects = new GLAspectPolygon(gi);
        }

        public void Offset(float factor, float units)
        {
            GI.PolygonOffset(factor, units);
        }

        public GLFeaturePolygonSmooth Smoothing
        {
            get { return fPolyAspects.Smoothing; }
        }
    }
}
