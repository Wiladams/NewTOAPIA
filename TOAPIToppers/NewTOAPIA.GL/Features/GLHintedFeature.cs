
namespace NewTOAPIA.GL
{
    public class GLHintedFeature : GLFeature
    {
        HintTarget fHintTarget;
        HintMode fHintMode;

        public GLHintedFeature(GraphicsInterface gi, GLOption aFeature, HintTarget aTarget)
            : base(gi, aFeature)
        {
            fHintMode = HintMode.DontCare;
            fHintTarget = aTarget;
        }

        public void Enable(HintMode aMode)
        {
            Enable();
            Hint = aMode;
        }

        public HintMode Hint
        {
            get { return fHintMode; }
            set
            {
                SetHint(value);
            }
        }

        void SetHint(HintMode mode)
        {
            fHintMode = mode;
            GI.Hint(fHintTarget, mode);
        }
    }


    public class GLFeatureLineSmooth : GLHintedFeature
    {
        public GLFeatureLineSmooth(GraphicsInterface gi)
            : base(gi, GLOption.LineSmooth, HintTarget.LineSmoothHint)
        {
        }
    }

    //public class GLFeaturePerspectiveCorrection : GLHintedFeature
    //{
    //    public GLFeaturePerspectiveCorrection()
    //        : base(GLOption.PerspectiveCorrection, HintTarget.PerspectiveCorrectionHint)
    //    {
    //    }
    //}

    public class GLFeaturePointSmooth : GLHintedFeature
    {
        public GLFeaturePointSmooth(GraphicsInterface gi)
            : base(gi, GLOption.PointSmooth, HintTarget.PointSmoothHint)
        {
        }
    }
   
    public class GLFeaturePolygonSmooth : GLHintedFeature
    {
        public GLFeaturePolygonSmooth(GraphicsInterface gi)
            : base(gi, GLOption.PolygonSmooth, HintTarget.PolygonSmoothHint)
        {
        }
    }
}
