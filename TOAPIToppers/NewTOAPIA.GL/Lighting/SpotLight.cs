
namespace NewTOAPIA.GL
{
    using NewTOAPIA.Drawing;
    using NewTOAPIA.Graphics;

    public class SpotLight : GLLight
    {
        GLAffectLightSpot fSpotEffect;

        public SpotLight(GraphicsInterface gi, GLLightName aName, Point3D position, ColorRGBA aColor, Vector3f direction, float cutoff, float exponent)
            : base(gi, aName, position, aColor)
        {
            fSpotEffect = new GLAffectLightSpot(direction, cutoff, exponent);
        }

        public GLAffectLightSpot SpotEffect
        {
            get
            {
                return fSpotEffect;
            }

            set
            {
                fSpotEffect = value;
            }
        }

        public override void Realize()
        {
            base.Realize();

            fSpotEffect.RealizeForLight(fLightName);
        }
    }
}
