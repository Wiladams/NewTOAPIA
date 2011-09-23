
namespace NewTOAPIA.GL
{
    using NewTOAPIA.Graphics;

    public class GLAspectLines : GLAspect
    {
        GLState fState;
        GLFeatureLineSmooth fSmoothing;

        public GLAspectLines(GraphicsInterface gi)
            :base(gi)
        {
            fState = new GLState(gi);
            fSmoothing = new GLFeatureLineSmooth(gi);
        }

        public float LineWidth
        {
            get { return fState.LineWidth; }
            set
            {
                fState.LineWidth = value;
            }
        }

        public float LineWidthGranularity
        {
            get { return fState.LineWidthGranularity; }
        }

        public vec2 LineWidthRange
        {
            get { return fState.LineWidthRange; }
        }

        public GLFeatureLineSmooth Smoothing
        {
            get { return fSmoothing; }
        }
    }
}
