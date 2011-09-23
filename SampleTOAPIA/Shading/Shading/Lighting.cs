

namespace Shading
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using NewTOAPIA.Graphics.Processor;

    public class Lighting : GPShader
    {
        public void DirectionalLight(int i, vec3 normal, ref vec4 ambient, ref vec4 diffuse, ref vec4 specular)
        {
            float nDotVP;   // normal . light direction
            float nDotHV;   // normal . light half vector
            float pf;       // power factor

            nDotVP = max(0.0f, dot(normal,
                normalize(new vec3(LightSource[i].position))));
            nDotHV = max(0.0f, dot(normal, new vec3(LightSource[i].halfVector)));

            if (nDotVP == 0.0f)
                pf = 0.0f;
            else
                pf = pow(nDotHV, FrontMaterial.shininess);

            ambient += LightSource[i].ambient;
            diffuse += LightSource[i].diffuse * nDotVP;
            specular += LightSource[i].specular * pf;
        }
    }
}
