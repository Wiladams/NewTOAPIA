using System;
using System.Collections.Generic;
using System.Text;

using NewTOAPIA;
using NewTOAPIA.GL;
//using TOAPI.OpenGL;

namespace QuadVideo
{
    public class DissolveProcessor
    {
        GLRenderTarget fOutputTarget;
        GLTexture2D fOutputTexture;

        GLSLShaderProgram fDissolveShader;
        GraphicsInterface fGI;
        int fWidth;
        int fHeight;
        float fNoiseScale;

        public DissolveProcessor(GraphicsInterface gi, int width, int height, float noiseScale)
        {
            fGI = gi;

            fWidth = width;
            fHeight = height;
            fNoiseScale = noiseScale;

            // Create the texture objects that will receive the output
            fOutputTexture = new GLTexture2D(GI, width, height, TextureInternalFormat.Rgba, TexturePixelFormat.Bgr, PixelComponentType.Byte, IntPtr.Zero, false);
            // Setup the render target that has 3 color channels for Multi Render Target
            // output in a shader using gl_FragData[n]
            fOutputTarget = new GLRenderTarget(GI);
            fOutputTarget.AttachColorBuffer(fOutputTexture, ColorBufferAttachPoint.Position0);
            fOutputTarget.Unbind();

            // Create the shader program that does the actual separation
            fDissolveShader = GLSLShaderProgram.CreateUsingVertexAndFragmentStrings(GI, FixedVert, Dissolve_Frag);
        }

        #region Properties
        public GraphicsInterface GI
        {
            get { return fGI; }
        }

        public GLTexture OutputTexture
        {
            get { return fOutputTexture; }
        }

        GLSLShaderProgram Program
        {
            get { return fDissolveShader; }
        }
        #endregion

        #region Dissolve
        /// <summary>
        /// Perform a dissolve between a 'base' image and a 'blend' image.
        /// </summary>
        /// <param name="currentTexture"></param>
        public void ProcessImages(GLTexture2D baseImage, GLTexture2D blendImage, float opacity)
        {
            GI.Features.Texturing2D.Enable();

            // Bind the inputs to the texture units
            GLTextureUnit baseUnit = GLTextureUnit.GetTextureUnit(fGI, TextureUnitID.Unit1);
            GLTextureUnit blendUnit = GLTextureUnit.GetTextureUnit(fGI, TextureUnitID.Unit2);

            baseUnit.AssignTexture(baseImage);
            blendUnit.AssignTexture(blendImage);
            //blendUnit.Unbind();


                // We want replace mode because we don't care about
                // the values that are currently in place, we just want
                // to replace them.
                GI.TexEnv(TextureEnvModeParam.Replace);


                // Set the viewport to be the whole texture
                GI.Viewport(0, 0, fWidth, fHeight);

                GI.MatrixMode(MatrixMode.Projection);
                GI.LoadIdentity();

                GI.Ortho(0, fWidth, 0, fHeight, -1.0, 1.0);
                //GI.FrontFace(FrontFaceDirection.Cw);

                // The current program is the channel separation program.  it will change
                // depending on which channel we are currently separating.
                Program.Bind();
                {
                    // set the program's input textures
                    Program["Base"].Set((int)baseUnit.OrdinalForShaders);
                    Program["Blend"].Set((int)blendUnit.OrdinalForShaders);
                    //Program["NoiseScale"].Set((float)fNoiseScale);
                    Program["Opacity"].Set((float)opacity);

                    // We want to turn off filtering.  We do that by using 'nearest'
                    // we also clamp the coordinates instead of replicating at the edges
                    GI.Features.Texturing2D.Enable();
                    //blendImage.Bind();
                    //GI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureMinFilter, TextureMinFilter.Linear);
                    //GI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureMagFilter, TextureMagFilter.Linear);
                    //GI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureWrapS, TextureWrapMode.Clamp);
                    //GI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureWrapT, TextureWrapMode.Clamp);

                    GI.Drawing.Quads.Begin();
                    {
                        float left = 0;
                        float bottom = 0;
                        float right = blendImage.Width;
                        float top = blendImage.Height;

                        // Left bottom
                        GI.TexCoord(0.0f, 0.0f);
                        GI.Vertex(left, bottom, 0.0f);

                        // Left top
                        GI.TexCoord(0.0f, 1.0f);
                        GI.Vertex(left, top, 0.0f);

                        // Right top
                        GI.TexCoord(1.0f, 1.0f);
                        GI.Vertex(right, top, 0.0f);

                        // Right bottom
                        GI.TexCoord(1.0f, 0.0f);
                        GI.Vertex(right, bottom, 0.0f);
                    }
                    GI.Drawing.Quads.End();

                    //blendImage.Unbind();
                }
                Program.Unbind();

        }
        #endregion

        public static string FixedVert = @"
void main(void)
{
	gl_Position = ftransform();
	gl_TexCoord[0] = gl_MultiTexCoord0;
    gl_FrontColor = gl_Color;
}
";

        /// <summary>
        /// This fragment shader will separate an incoming texture, as 
        /// specified in the tex0 uniform variable, into a Y, Cr, and Cb
        /// channel.  Its operation is dependent on the grahics driver support
        /// of the GL_ARB_draw_buffers extension.  This will not operate on 
        /// all machines.
        /// </summary>
        public static string Dissolve_Frag = @"
uniform sampler2D Base;
uniform sampler2D Blend;
uniform float Opacity;
//uniform float NoiseScale;
const float NoiseScale = 1.2;

void main(void)
{
    float noise = (noise1(vec2(gl_TexCoord[0] * NoiseScale)) + 1.0) * 0.5;

    vec4 baseTexel = texture2D(Base, gl_TexCoord[0].xy);
    vec4 blendTexel = texture2D(Blend, gl_TexCoord[0].xy);
    
    //vec4 result = (noise < Opacity) ? blendTexel : baseTexel;
    vec4 result = blendTexel;

    gl_FragColor = mix(baseTexel, result, Opacity);
}
";



    }
}
