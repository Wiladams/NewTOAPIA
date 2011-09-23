
namespace NewTOAPIA.GL
{
    public class GLPolyDrawStyle : GLAspect
    {
        PolygonMode fMode;
        GLFace fFace;

        public GLPolyDrawStyle(GraphicsInterface gi)
            :base(gi)
        {
            fMode = PolygonMode.Line;
            fFace = GLFace.Front;
        }

        public GLPolyDrawStyle(GraphicsInterface gi, PolygonMode aMode, GLFace aFace)
            :base(gi)
        {
            fMode = aMode;
            fFace = aFace;
        }

        public virtual GLFace Face
        {
            get { return fFace; }
            set
            {
                SetFace(value);
            }
        }

        public virtual void SetFace(GLFace aFace)
        {
            fFace = aFace;
            Realize();
        }

        public virtual PolygonMode Mode
        {
            get { return fMode; }
            set
            {
                SetMode(value);
            }
        }

        public virtual void SetMode(PolygonMode aMode)
        {
            fMode = aMode;
            Realize();
        }

        public override void Realize()
        {
            GL.PolygonMode(fFace, fMode);
        }
    }

    public class PolyPointMode : GLPolyDrawStyle
    {
        public PolyPointMode(GraphicsInterface gi)
            : base(gi, PolygonMode.Point, GLFace.Front)
        {
        }
    }

    public class PolyLineMode : GLPolyDrawStyle
    {
        public PolyLineMode(GraphicsInterface gi)
            : base(gi, PolygonMode.Line, GLFace.FrontAndBack)
        {
        }
    }

    public class PolyFillMode : GLPolyDrawStyle
    {
        public PolyFillMode(GraphicsInterface gi)
            : base(gi, PolygonMode.Fill, GLFace.Front)
        {
        }
    }

}
