using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shading.Mandelbrot
{
    using NewTOAPIA.Graphics;
    using NewTOAPIA.Graphics.Processor;

    public class MandelbrotVertex : GPVertexShader
    {
        // uniform
        public mat4 MVMatrix;
        public mat4 MVPMatrix;
        public mat3 NormalMatrix;

        // uniform
        public vec3 LightPosition;
        public float SpecularContribution;
        public float DiffuseContribution;
        public float Shininess;

        // in
        vec4 MCVertex;
        vec3 MCNormal;
        vec3 TexCoord0;

        // out
        float LightIntensity;
        vec3 Position;

        public override void main()
        {
            vec3 ecPosition = new vec3(MVMatrix * MCVertex);
            vec3 tnorm = normalize(NormalMatrix * MCNormal);
            vec3 lightVec = normalize(LightPosition - ecPosition);
            vec3 reflectVec = reflect(-lightVec, tnorm);
            vec3 viewVec = normalize(-ecPosition);
            float spec = max(dot(reflectVec, viewVec), 0);
            spec = pow(spec, Shininess);
            LightIntensity = DiffuseContribution * 
                max(dot(lightVec, tnorm), 0) +
                SpecularContribution * spec;

            Position = TexCoord0 * 5 - 2.5f;
            GPPosition = MVPMatrix * MCVertex;
        }
    }
}
