using System;
using System.Collections.Generic;
using System.Text;

namespace QuadVideo
{
    class Shaders
    {
        public static string FixedVert = @"
void main(void)
{
	gl_Position = ftransform();
	gl_TexCoord[0] = gl_MultiTexCoord0;
    gl_FrontColor = gl_Color;
}
";
        public static string Difference_Frag = @"
uniform sampler2D Background;
uniform sampler2D Blend;

void main(void)
{
    vec4 baseTexel = texture2D(Background, gl_TexCoord[0].xy);
    vec4 blendTexel = texture2D(Blend, gl_TexCoord[0].xy);

    vec3 result = baseTexel - blendTexel;
    gl_FragColor = vec4(result, 1.0);

    //gl_FragColor = vec4(gl_Color.rgb, 1.0);
    //gl_FragColor = baseTexel;
    //gl_FragColor = blendTexel;
}
";

        public static string Fade_Frag = @"
uniform sampler2D Base;
uniform float Opacity;

void main(void)
{
    vec4 baseTexel = texture2D(Base, gl_TexCoord[0].xy);
    vec4 blendTexel = gl_Color;
    

    gl_FragColor = mix(baseTexel, blendTexel, Opacity);
}
";

        /// <summary>
        /// The grayscale colorizer takes a single input value, the red channel, and
        /// replicates that value to the other components.  This value becomes the
        /// color output value.
        /// 
        /// This is a good shader to use in cases where the texture is such as luminance,
        /// or intensity, and you want to represent the value as a gray image.
        /// </summary>
        public const string graycolorizer_fs = @"
// graycolorizer.fs
//
// convert luminance to rgb
uniform sampler2D tex0;

void main(void)
{
    vec4 color = texture2D(tex0, gl_TexCoord[0].xy);
    
    // take a color value that can be found in the red channel,
    // replicate it across the other components, and output
    // the result.
    gl_FragColor = vec4(color.r, color.r, color.r, 1.0);
}
";

        public static string Hatch_Frag = @"
//
// Fragment shader for procedurally generated hatching or 'woodcut' appearance.
//
// This is an OpenGL 2.0 implementation of Scott F. Johnston's 'Mock Media'
// (from 'Advanced RenderMan: Beyond the Companion' SIGGRAPH 98 Course Notes)
//
// Author: Bert Freudenberg <bert@isg.cs.uni-magdeburg.de>
//
// Copyright (c) 2002-2003 3Dlabs, Inc. 
//
// See 3Dlabs-License.txt for license information
//

const float frequency = 1.0;

varying vec3  ObjPos;               // object space coord (noisy)
varying float V;                    // generic varying
varying float LightIntensity;

uniform sampler3D Noise;            // value of Noise = 3;
uniform float Swidth;               // relative width of stripes = 16.0

void main()
{
    float dp       = length(vec2(dFdx(V * Swidth), dFdy(V * Swidth)));
    float logdp    = -log2(dp);
    float ilogdp   = floor(logdp);
    float stripes  = exp2(ilogdp);

    float noise    = texture3D(Noise, ObjPos).x;

    float sawtooth = fract((V + noise * 0.1) * frequency * stripes);
    float triangle = abs(2.0 * sawtooth - 1.0);

    // adjust line width
    float transition = logdp - ilogdp;

    // taper ends
    triangle = abs((1.0 + transition) * triangle - transition);

    const float edgew = 0.2;            // width of smooth step

    float edge0  = clamp(LightIntensity - edgew, 0.0, 1.0);
    float edge1  = clamp(LightIntensity, 0.0, 1.0);
    float square = 1.0 - smoothstep(edge0, edge1, triangle);

    gl_FragColor = vec4(vec3(square), 1.0);
}
        ";

        public static string Hatch_Vert = @"
//
// Vertex shader for procedurally generated hatching or 'woodcut' appearance.
//
// This is an OpenGL 2.0 implementation of Scott F. Johnston's 'Mock Media'
// (from 'Advanced RenderMan: Beyond the Companion' SIGGRAPH 98 Course Notes)
//
// Author: Bert Freudenberg <bert@isg.cs.uni-magdeburg.de>
//
// Copyright (c) 2002-2003 3Dlabs, Inc. 
//
// See 3Dlabs-License.txt for license information
//

uniform vec3  LightPosition;
uniform float Time;

varying vec3  ObjPos;
varying float V;
varying float LightIntensity;
 
void main()
{
    ObjPos          = (vec3(gl_Vertex) + vec3(0.0, 0.0, Time)) * 0.2;
 
    vec3 pos        = vec3(gl_ModelViewMatrix * gl_Vertex);
    vec3 tnorm      = normalize(gl_NormalMatrix * gl_Normal);
    vec3 lightVec   = normalize(LightPosition - pos);

    LightIntensity  = max(dot(lightVec, tnorm), 0.0);

    V = gl_MultiTexCoord0.t;  // try .s for vertical stripes

    gl_Position = ftransform();
}
        ";
    }
}
