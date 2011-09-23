
namespace NewTOAPIA.GL
{
    public class GLAspectList
    {
        public GLAspectPoints fPointsAspect;
        public GLAspectLines fLinesAspect;

        public GLAspectList(GraphicsInterface gi)
        {
            fLinesAspect = new GLAspectLines(gi);
            fPointsAspect = new GLAspectPoints(gi);
        }

        public GLAspectLines Lines
        {
            get { return fLinesAspect; }
        }

        public GLAspectPoints Points
        {
            get { return fPointsAspect; }
        }
    }
}
