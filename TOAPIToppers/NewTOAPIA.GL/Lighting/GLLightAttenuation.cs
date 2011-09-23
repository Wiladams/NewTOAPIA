
namespace NewTOAPIA.GL
{
    public struct GLLightAttenuation : IAffectLight
    {
        // Attenuation factors
        float fAttenConstant;
        float fAttenLinear;
        float fAttenQuadratic;

        public static GLLightAttenuation GetDefault()
        {
            return new GLLightAttenuation(0f, 0f, 2.0f);
        }

        public GLLightAttenuation(float constant, float linear, float quadratic)
        {
            fAttenConstant = constant;
            fAttenLinear = linear;
            fAttenQuadratic = quadratic;
        }

        public void RealizeForLight(GLLightName aLight)
        {
            GL.Light(aLight, LightParameter.ConstantAttenuation, fAttenConstant);
            GL.Light(aLight, LightParameter.LinearAttenuation, fAttenLinear);
            GL.Light(aLight, LightParameter.QuadraticAttenuation, fAttenQuadratic);
        }

        public override string ToString()
        {
            return string.Format("<GLLightAttenuation constant='{0}', linear='{1}', quadratic='{2}'/>",
                fAttenConstant, fAttenLinear, fAttenQuadratic);
        }
    }
}
