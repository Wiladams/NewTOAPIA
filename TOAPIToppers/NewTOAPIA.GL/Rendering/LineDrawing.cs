
namespace NewTOAPIA.GL
{
    public class BracketLineDrawing : GLBracketDrawing
    {
        GLAspectLines fLineAspects;

        public BracketLineDrawing(GraphicsInterface gi, BeginMode mode)
            : base(gi, mode)
        {
            fLineAspects = new GLAspectLines(gi);
        }

        public float LineWidth
        {
            get { return fLineAspects.LineWidth; }
            set
            {
                fLineAspects.LineWidth = value;
            }
        }

        public GLFeatureLineSmooth Smoothing
        {
            get { return fLineAspects.Smoothing; }
        }
    }

    public class BracketLinesDrawing : BracketLineDrawing
    {
        public BracketLinesDrawing(GraphicsInterface gi)
            : base(gi, BeginMode.Lines)
        {
        }
    }

    public class BracketLineLoopDrawing : BracketLineDrawing
    {
        public BracketLineLoopDrawing(GraphicsInterface gi)
            : base(gi, BeginMode.LineLoop)
        {
        }
    }

    public class BracketLineStripDrawing : BracketLineDrawing
    {
        public BracketLineStripDrawing(GraphicsInterface gi)
            : base(gi, BeginMode.LineStrip)
        {
        }
    }
}
