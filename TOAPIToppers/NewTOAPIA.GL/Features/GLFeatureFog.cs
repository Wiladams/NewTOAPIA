
namespace NewTOAPIA.GL
{
    using NewTOAPIA.Drawing;
    using NewTOAPIA.Graphics;

    public class GLFeatureFog : GLHintedFeature
    {
        public GLFeatureFog(GraphicsInterface gi)
            : base(gi, GLOption.Fog, HintTarget.FogHint)
        {
        }

        public ColorRGBA Color
        {
            get {
                return GI.State.FogColor;
            }
            set
            {
                SetColor(value);
            }
        }

        public void SetColor(ColorRGBA colorValue)
        {
            GI.Fogfv(FogParameter.FogColor, (float[])colorValue);
        }


        public float End
        {
            get 
            {
                return GI.State.FogEnd;
            }
            set
            {
                SetEnd(value);
            }
        }

        public void SetEnd(float endValue)
        {
            GI.Fogf(FogParameter.FogEnd, endValue);
        }

        public int Index
        {
            get {
                return GI.State.FogIndex;
            }
            set
            {
                SetIndex(value);
            }
        }

        public void SetIndex(int indexValue)
        {
            GI.Fogf(FogParameter.FogIndex, indexValue);
            //GI.Fogi(FogParameter.FogIndex, indexValue);
        }

        public FogMode Mode
        {
            get {
                return GI.State.FogMode;
            }
            set
            {
                SetMode(value);
            }
        }

        public void SetMode(FogMode modeValue)
        {
            GI.Fogf(FogParameter.FogMode, (int)modeValue);
        }

        public float Start
        {
            get
            {
                return GI.State.FogStart;
            }

            set
            {
                SetStart(value);
            }
        }

        public void SetStart(float startValue)
        {
            GI.Fogf(FogParameter.FogStart, startValue);
        }
    
    }

}
