
namespace NewTOAPIA.GL
{
    public class GLFeatureBlend : GLFeature
    {
        GLBlendFunc fBlendFactors;

        public GLFeatureBlend(GraphicsInterface gi)
            : base (gi, GLOption.Blend)
        {
        }

        public BlendEquation Equation
        {
            set
            {
                SetEquation(value);
            }
        }

        public void SetEquation(BlendEquation equation)
        {
            GI.BlendEquation(equation);
        }

        public virtual GLBlendFunc BlendFactors
        {
        //    get { return fBlendFunction; }
            set
            {
                SetBlendFactors(value);
            }
        }

        public void SetBlendFactors(GLBlendFunc factors)
        {
            fBlendFactors = factors;
            GI.BlendFunc(fBlendFactors.SourceBlendFactor, fBlendFactors.DestinationBlendFactor);
        }
    }
}
