using System;

using NewTOAPIA;
using NewTOAPIA.GL;
using NewTOAPIA.GL.Imaging;

namespace QuadVideo
{
    public enum ConvolutionKernel
    {
        Identity = 0,
        Sharpen,
        Blur, 
        GaussianBlur,
        EdgeEnhance,
        EdgeDetect,
        Emboss,
        Sobell,
        Laplacian
    }

    class ConvolutionProcessor : UnaryTextureProcessor
    {
        int fDistance = 1;
        int curker = 6;   // Which kernel to use for the convolution filter
        float[][] kernels = {
		new float[]{ 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f, 0.0f}, /* Identity */
		new float[]{ 0.0f,-1.0f, 0.0f,-1.0f, 5.0f,-1.0f, 0.0f,-1.0f, 0.0f}, /* Sharpen */
		new float[]{ 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f}, /* Blur */
		new float[]{ 1.0f, 2.0f, 1.0f, 2.0f, 4.0f, 2.0f, 1.0f, 2.0f, 1.0f}, /* Gaussian blur */
		new float[]{ 0.0f, 0.0f, 0.0f,-1.0f, 1.0f, 0.0f, 0.0f, 0.0f, 0.0f}, /* Edge enhance */
		new float[]{ 1.0f, 1.0f, 1.0f, 1.0f, 8.0f, 1.0f, 1.0f, 1.0f, 1.0f},     // Edge detect
		new float[]{ 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f,-1.0f},     // Emboss
		new float[]{ 1.0f, 2.0f, 1.0f, 0.0f, 0.0f, 0.0f, -1.0f, -2.0f,-1.0f},   // Sobell
		new float[]{ -1.0f, -1.0f, -1.0f, -1.0f, 9.0f, -1.0f, -1.0f, -1.0f, -1.0f}    // Laplacian - sharpen
	    };

        public ConvolutionProcessor(GraphicsInterface gi, int width, int height, ConvolutionKernel whichKernel)
            :base(gi, width, height, Convolution_Frag)
        {
            curker = (int)whichKernel;
        }

        public int Distance
        {
            get { return fDistance; }
            set { fDistance = value; }
        }

        public int CurrentKernel
        {
            get { return curker; }
            set { curker = value; }
        }

        
        protected override void  SetUniformVariables()
        {

            // Set the necessary variables in the program.  We let the program
            // know the width and height of the buffer so the kernel knows how
            // far it can go to get the neighboring pixels.
            // The "Kernel" variable is the 3x3 matrix that represents the 
            // actual convolution filter.
            ShaderProgram["Width"].Set(Width);
            ShaderProgram["Height"].Set(Height);
            ShaderProgram["Dist"].Set(Distance);
            int loc = ShaderProgram.GetUniformLocation("Kernel");
            GI.UniformMatrix3(loc, 1, false, kernels[CurrentKernel]);
        }

        public static string Convolution_Frag = @"
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
    }

}
