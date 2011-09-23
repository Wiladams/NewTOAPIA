using System;


namespace NewTOAPIA.GL.Imaging
{
    using NewTOAPIA.GL;
    using TOAPI.OpenGL;

    public class RGBToGray : UnaryTextureProcessor
    {                
        public RGBToGray(GraphicsInterface gi, int width, int height)
            : base(gi, width, height, Gray_Frag)
        {
        }


        /// <summary>
        /// This fragment shader will separate an incoming texture, as 
        /// specified in the tex0 uniform variable, into a Y, Cr, and Cb
        /// channel.  Its operation is dependent on the grahics driver support
        /// of the GL_ARB_draw_buffers extension.  This will not operate on 
        /// all machines.
        /// </summary>
        public static string Gray_Frag = @"
uniform sampler2D Base;

const vec3 lumCoeff = vec3(.299, .587, 0.114);

void main(void)
{
    vec4 color = texture2D(Base, gl_TexCoord[0].st);

    float Y = dot(color.rgb, lumCoeff);    
   
    gl_FragColor = vec4(Y,Y,Y, 1.0);
}
";
    }
}
