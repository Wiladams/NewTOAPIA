using System;

using NewTOAPIA.GL;

namespace FBOImaging
{
    public static class ShaderStrings
    {
        public static string ConvolutionFrag = @"
uniform int Width;
uniform int Height;
uniform int Dist;
uniform mat3 Kernel;
uniform sampler2D Tex0;

void main (void)
{
	int i, j;
	vec2 coord;
	float contrib = 0.0;
	vec4 sum = vec4(0.0);

	// 3x3 convolution matrix
	for(i = -1; i <= 1; i++) 
		for(j = -1; j <= 1; j++) {
			coord = gl_TexCoord[0].st + vec2(float(i) * (1.0/float(Width)) * float(Dist), float(j) * (1.0/float(Height)) * float(Dist));
			sum += Kernel[i+1][j+1] * texture2D(Tex0, coord.xy);
			contrib += Kernel[i+1][j+1];
		}

	gl_FragColor = sum/contrib;
}
";

        public static string FixedFrag = @"
uniform sampler2D Tex0;

void main (void)
{
	gl_FragColor = texture2D(Tex0, gl_TexCoord[0].st);
}
";

        public static string FixedVert = @"
void main(void)
{

	gl_Position = ftransform();
	gl_TexCoord[0] = gl_MultiTexCoord0;
}
";
    }
}
