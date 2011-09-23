using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shading.Stripes
{
    using NewTOAPIA.Graphics;
    using NewTOAPIA.Graphics.Processor;

    public class StripesVertex : GPVertexShader
    {
        // uniform
        [gpuniform]
        public vec3 LightPosition;
        [gpuniform]
        public vec3 LightColor;
        [gpuniform]
        public vec3 EyePosition;
        [gpuniform]
        public vec3 Specular;
        [gpuniform]
        public vec3 Ambient;
        [gpuniform]
        public float Kd;

        // uniform
        [gpuniform]
        public mat4 MVMatrix;
        [gpuniform]
        public mat4 MVPMatrix;
        [gpuniform]
        public mat3 NormalMatrix;

        // in
        [gpin]
        public vec4 MCVertex;
        [gpin]
        public vec3 MCNormal;
        [gpin]
        public vec2 TexCoord0;

        // out
        [gpout]
        public vec3 DiffuseColor;
        [gpout]
        public vec3 SpecularColor;
        [gpout]
        public float TexCoord;

        public override void main()
        {
            vec3 ecPosition = new vec3(MVMatrix * MCVertex);
            vec3 tnorm = normalize(NormalMatrix * MCNormal);
            vec3 lightVec = normalize(LightPosition - ecPosition);
            vec3 viewVec = normalize(EyePosition - ecPosition);
            vec3 hvec = normalize(viewVec + lightVec);

            float spec = clamp(dot(hvec, tnorm), 0, 1);
            spec = pow(spec, 16);

            DiffuseColor = LightColor * new vec3(Kd * dot(lightVec, tnorm));
            DiffuseColor = clamp(Ambient + DiffuseColor, 0, 1);
            SpecularColor = clamp((LightColor * Specular * spec), 0, 1);

            TexCoord = TexCoord0.t;
            GPPosition = MVPMatrix * MCVertex;
        }
    }
}
