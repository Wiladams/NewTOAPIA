namespace Shading
{
    using System.Runtime.InteropServices;

    using NewTOAPIA.Graphics;
    using NewTOAPIA.Graphics.Processor;

    public class BrickVertexShader : GPVertexShader
    {
        // in
        [gpin]
        public vec4 MCvertex;
        [gpin]
        public vec3 MCnormal;

        // uniform
        [gpuniform]
        public mat4 MVMatrix;
        [gpuniform]
        public mat4 MVPMatrix;
        [gpuniform]
        public mat3 NormalMatrix;

        // uniform
        [gpuniform]
        public vec3 LightPosition;

        const float SpecularContribution = 0.3f;
        const float DiffuseContribution = 1.0f - SpecularContribution;

        // out
        [gpout]
        public float LightIntensity;
        [gpout]
        public vec2 MCposition;

        
        public override void main()
        {
            vec3 ecPosition = new vec3(MVMatrix * MCvertex);
            vec3 tnorm = normalize(NormalMatrix * MCnormal);
            vec3 lightVec = normalize(LightPosition - ecPosition);
            vec3 reflectVec = reflect(-lightVec, tnorm);
            vec3 viewVec = normalize(-ecPosition);
            float diffuse = max(dot(lightVec, tnorm), 0.0f);
            float spec = 0.0f;

            if (diffuse > 0.0f)
            {
                spec = max(dot(reflectVec, viewVec), 0.0f);
                spec = pow(spec, 16.0f);
            }

            LightIntensity = DiffuseContribution * diffuse +
                    SpecularContribution * spec;

            MCposition = new vec2(MCvertex);

            GPPosition = new vec3(MVPMatrix * MCvertex);
        }
    }
}