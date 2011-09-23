
namespace NewTOAPIA.GL
{
    public class HyperbolicInterpolation : GLHint, IEnable
    {
        public HyperbolicInterpolation(GraphicsInterface gi)
            : base(gi, HintTarget.PerspectiveCorrectionHint, HintMode.Nicest)
        {
        }

        public virtual void Enable()
        {
            SetMode(HintMode.Nicest);
        }

        public virtual void Disable()
        {
            SetMode(HintMode.DontCare);
        }
    }
}
