
namespace NewTOAPIA.GL
{
    public class GLAspectPoints : GLAspect
    {
        GLState fState;
        GLFeaturePointSmooth fSmoothing;

        public GLAspectPoints(GraphicsInterface gi)
            :base(gi)
        {
            fState = new GLState(gi);
            fSmoothing = new GLFeaturePointSmooth(gi);
        }

        public float PointGranularity
        {
            get { return fState.PointSizeGranularity; }
        }

        public float PointRange
        {
            get { return fState.PointSizeRange; }
        }

        public float PointSize
        {
            get { return fState.PointSize; }
            set
            {
                fState.PointSize = value;
            }
        }

        public GLFeaturePointSmooth Smoothing
        {
            get { return fSmoothing; }
        }
    }
}
