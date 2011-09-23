
namespace NewTOAPIA.GL
{
    using NewTOAPIA.Graphics;

    public struct GLAffectLightSpot : IAffectLight
    {
        float fSpotCutoff;      // cutoff
        float fSpotExponent;    // exponent
        Vector3f fSpotDirection; // direction

        public GLAffectLightSpot(Vector3f direction, float cutoff, float exponent)
        {
            fSpotDirection = direction;
            fSpotCutoff = cutoff;
            fSpotExponent = exponent;
        }

        public void RealizeForLight(GLLightName aLight)
        {
            GL.Light(aLight, LightParameter.SpotCutoff, fSpotCutoff);
            GL.Light(aLight, LightParameter.SpotExponent, fSpotExponent);
            GL.Light(aLight, LightParameter.SpotDirection, (float[])fSpotDirection);
        }

        public override string ToString()
        {
            return string.Format("<GLAffectLightSpot cutoff='{0}', exponent='{1}'>{2}</GLAffectLightSpot>",
                fSpotCutoff, fSpotExponent, fSpotDirection.ToString());
        }
    }
}
