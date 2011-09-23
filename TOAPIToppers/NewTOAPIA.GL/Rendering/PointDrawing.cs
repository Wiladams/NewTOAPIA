
namespace NewTOAPIA.GL
{
    public class BracketPointDrawing : GLBracketDrawing
    {
        GLAspectPoints fPointsAspect;

        public BracketPointDrawing(GraphicsInterface gi)
            : base(gi, BeginMode.Points)
        {
            fPointsAspect = new GLAspectPoints(gi);
        }

        public float PointSize
        {
            get { return fPointsAspect.PointSize; }

            set
            {
                fPointsAspect.PointSize = value;
            }
        }

        public GLFeaturePointSmooth Smoothing
        {
            get { return fPointsAspect.Smoothing; }
        }
    }
}
