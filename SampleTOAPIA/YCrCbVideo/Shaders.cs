using System;

using NewTOAPIA.GL;

namespace Shaders
{
    /// <summary>
    /// The ShaderStrings contains a number of shader programs, primarily Fragment shaders.
    /// The typical ShaderProgram would use the FixedVert string for the vertex part, and then
    /// whatever code you want to use in the fragement part.
    /// 
    /// These shaders are good for 'post processing' a texture image.  They work well when there
    /// is nothing more required from a shader than to apply some operation on a per pixel basis.
    /// Drawing a screen aligned quad will get every pixel accessed and sent to the fragment shader.
    /// </summary>
    public static class ShaderStrings
    {
        #region Passthrough filters
        public static string FixedVert = @"
#extension GL_ARB_draw_buffers : enable

void main(void)
{

	gl_Position = ftransform();
	gl_TexCoord[0] = gl_MultiTexCoord0;
    gl_FrontColor = gl_Color;
}
";

        /// <summary>
        /// This string represents a fragment shader that is a pass-through when you have a texture object.
        /// </summary>
        public static string FixedFrag = @"
uniform sampler2D Tex0;

void main (void)
{
	gl_FragColor = texture2D(Tex0, gl_TexCoord[0].st);
}
";
        #endregion

        #region Colorizers
        /// <summary>
        /// A very simple sepia tone shader.  It will turn the image to 
        /// grayscale, and apply a pinkish tint to it.
        /// </summary>
        public const string sepia_fs = @"
// sepia.fs
//
// convert RGB to sepia tone
uniform sampler2D tex0;

void main(void)
{
    vec4 color = texture2D(tex0, gl_TexCoord[0].xy);
    
    // Convert to grayscale using NTSC conversion weights
    float gray = dot(color.rgb, vec3(0.299, 0.587, 0.114));

    // convert grayscale to sepia
    gl_FragColor = vec4(gray * vec3(1.2, 1.0, 0.8), 1.0);
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

        public const string redcolorizer_fs = @"
// redcolorizer.fs
//
// convert luminance to red
uniform sampler2D tex0;

void main(void)
{
    vec4 color = texture2D(tex0, gl_TexCoord[0].st);
    
    gl_FragColor = vec4(color.r, 0 , 0, 1.0);
}
";

        public const string greencolorizer_fs = @"
// greencolorizer.fs
//
// convert luminance to green
uniform sampler2D tex0;

void main(void)
{
    vec4 color = texture2D(tex0, gl_TexCoord[0].st);
    
    gl_FragColor = vec4(0, color.r , 0, 1.0);
}
";

        public const string bluecolorizer_fs = @"
// bluecolorizer.fs
//
// convert luminance to blue
uniform sampler2D tex0;

void main(void)
{
    vec4 color = texture2D(tex0, gl_TexCoord[0].st);
    
    gl_FragColor = vec4(0, 0 , color.r, 1.0);
}
";
        #endregion

        #region Channel Separators
        #region Multipass separators
        /// <summary>
        /// For doing YCrCb conversion, the following is used
        /// Ey = 0.299R+0.587G+0.114B
        /// Ecr = 0.713(R - Ey) = 0.500R-0.419G-0.081B
        /// Ecb = 0.564(B - Ey) = -0.169R-0.331G+0.500B
        /// </summary>
        public const string Ychannel_fs = @"
// Ychannel.fs
//
// Create Y channel of YCrCb from RGB, according to CCIR 601
uniform sampler2D tex0;

void main(void)
{
    vec4 color = texture2D(tex0, gl_TexCoord[0].st);
    const vec3 lumCoeff = vec3(.299, .587, 0.114);
    float Y = dot(color.rgb, lumCoeff);

    gl_FragColor = vec4(Y, Y, Y, 1.0);
}
";

        public const string Crchannel_fs = @"
// Crchannel_fs.fs
//
// Create Cr channel of YCrCb from RGB, according to CCIR 601
uniform sampler2D tex0;

void main(void)
{
    vec4 color = texture2D(tex0, gl_TexCoord[0].xy);

    float Cr = 0.500*color.r-0.419*color.g-0.081*color.b;

    gl_FragColor = vec4(Cr, Cr, Cr, 1.0);
}
";

        public const string Cbchannel_fs = @"
// Cbchannel.fs
//
// Create Cb channel of YCrCb from RGB, according to CCIR 601
// Ecb = 0.564(B - Ey) = -0.169R-0.331G+0.500B
uniform sampler2D tex0;

void main(void)
{
    vec4 color = texture2D(tex0, gl_TexCoord[0].xy);

    float Cb = -0.169*color.r-0.331*color.g+0.500*color.b;

    gl_FragColor = vec4(Cb, Cb, Cb, 1.0);
}
";





        #endregion

        #region Single pass separators
        /// <summary>
        /// This fragment shader will separate an incoming texture, as 
        /// specified in the tex0 uniform variable, into a Y, Cr, and Cb
        /// channel (Rec 601-1 specification).  Its operation is dependent 
        /// on the grahics driver support of the GL_ARB_draw_buffers extension.  
        /// This will not operate on all machines.
        /// </summary>
        public static string YCrCb_Frag = @"
//#version 110
#extension GL_ARB_draw_buffers : enable
uniform sampler2D tex0;

void main(void)
{
    vec4 color = texture2D(tex0, gl_TexCoord[0].st);
    const vec3 lumCoeff = vec3(.299, .587, 0.114);

    // Convert to grayscale using NTSC conversion weights
    float Y = dot(color.rgb, lumCoeff);
    //float Y = 0.299*color.r + 0.587 * color.g + 0.114*color.b;
    
    float Cr = 0.500*color.r-0.419*color.g-0.081*color.b;

    float Cb = -0.169*color.r-0.331*color.g+0.500*color.b;
   
   gl_FragData[0] = vec4(Y, Y, Y, 1.0);
   gl_FragData[1] = vec4(Cr, Cr, Cr, 1.0);
   gl_FragData[2] = vec4(Cb, Cb, Cb, 1.0);
}
";

        public static string RGBSeparator_Frag = @"
// This seems to be highly dependent on the hardware
// implementation.
#extension GL_ARB_draw_buffers : enable

uniform sampler2D tex0;

void main()
{
    vec4 color = texture2D(tex0, gl_TexCoord[0].xy);
    float gray = dot(color.rgb, vec3(0.299, 0.587, 0.114));

    gl_FragData[0] = vec4(1.0, 0.0, 0.0, 1.0);
	gl_FragData[1] = vec4(gray, gray, gray, 1.0);
    gl_FragData[2] = vec4(0.0, 0.0, 1.0, 1.0);
}

";
        #endregion
        #endregion
    }
}
