
namespace NewTOAPIA.GL
{
 
    public class GLHint : GLAspect
    {
        HintTarget fHintTarget;
        HintMode fHintMode;

        public GLHint(GraphicsInterface gi, HintTarget aTarget, HintMode aMode)
            :base(gi)
        {
            fHintTarget = aTarget;
            fHintMode = aMode;
        }

        public HintMode Mode
        {
            get { return fHintMode;}
            set {
                SetMode(value);
            }
        }

        public void SetMode(HintMode aMode)
        {
            fHintMode = aMode;
            Realize();
        }

        public virtual void DontCare()
        {
            SetMode(HintMode.DontCare);
        }

        public virtual void MakeNicest()
        {
            SetMode(HintMode.Nicest);
        }

        public virtual void MakeFastest()
        {
            SetMode(HintMode.Fastest);
        }

        public override void  Realize()
        {
            GL.Hint(fHintTarget, fHintMode);
        }

        public override string ToString()
        {
            return string.Format("<Hint><HintTarget name='{0}'/><HintMode name='{1}'/></Hint>",
                fHintTarget, fHintMode);
        }
    }

    public class HintPerspectiveCorrection : GLHint
    {
        public HintPerspectiveCorrection(GraphicsInterface gi)
            : base(gi, HintTarget.PerspectiveCorrectionHint, HintMode.DontCare)
        {
        }
    }

    public class HintPointSmooth : GLHint
    {
        public HintPointSmooth(GraphicsInterface gi)
            : base(gi, HintTarget.PointSmoothHint, HintMode.DontCare)
        {
        }
    }

    public class HintLineSmooth : GLHint
    {
        public HintLineSmooth(GraphicsInterface gi)
            : base(gi, HintTarget.LineSmoothHint, HintMode.DontCare)
        {
        }
    }

    public class HintPolygonSmooth : GLHint
    {
        public HintPolygonSmooth(GraphicsInterface gi)
            : base(gi, HintTarget.PolygonSmoothHint, HintMode.DontCare)
        {
        }
    }

    public class HintFog : GLHint
    {
        public HintFog(GraphicsInterface gi)
            : base(gi,HintTarget.FogHint, HintMode.DontCare)
        {
        }
    }
}
