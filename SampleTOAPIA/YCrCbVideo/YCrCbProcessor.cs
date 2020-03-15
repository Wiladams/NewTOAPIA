

namespace QuadVideo
{
    using System;

    using TOAPI.OpenGL;

    using NewTOAPIA.GL;
    //using NewTOAPIA.Media.GL;

    public class YCrCbProcessor
    {
        GLTexture2D fYTexture;
        GLTexture2D fCrTexture;
        GLTexture2D fCbTexture;

        GLRenderTarget fYCrCbTarget;

        GLSLShaderProgram fYCrCbChannelSep;
        GraphicsInterface fGI;
        int fWidth;
        int fHeight;

        public YCrCbProcessor(GraphicsInterface gi, int width, int height)
        {
            fGI = gi;

            fWidth = width;
            fHeight = height;

            // Create the texture objects that will receive the output
            fYTexture = new GLTexture2D(GI, width, height, TextureInternalFormat.Luminance, TexturePixelFormat.Luminance, PixelType.Byte, IntPtr.Zero, false);
            fCrTexture = new GLTexture2D(GI, width, height, TextureInternalFormat.Luminance, TexturePixelFormat.Luminance, PixelType.Byte, IntPtr.Zero, false);
            fCbTexture = new GLTexture2D(GI, width, height, TextureInternalFormat.Luminance, TexturePixelFormat.Luminance, PixelType.Byte, IntPtr.Zero, false);

            // Setup the render target that has 3 color channels for Multi Render Target
            // output in a shader using gl_FragData[n]
            fYCrCbTarget = new GLRenderTarget(GI);
            fYCrCbTarget.AttachColorBuffer(fYTexture, ColorBufferAttachPoint.Position0);
            fYCrCbTarget.AttachColorBuffer(fCrTexture, ColorBufferAttachPoint.Position1);
            fYCrCbTarget.AttachColorBuffer(fCbTexture, ColorBufferAttachPoint.Position2);
            fYCrCbTarget.Unbind();

            // Create the shader program that does the actual separation
            fYCrCbChannelSep = GLSLShaderProgram.CreateUsingVertexAndFragmentStrings(GI, FixedVert, YCrCb_Frag);
        }

        #region Properties
        public GraphicsInterface GI
        {
            get { return fGI; }
        }

        public GLTexture YChannel
        {
            get { return fYTexture; }
        }

        public GLTexture CrChannel
        {
            get { return fCrTexture; }
        }

        public GLTexture CbChannel
        {
            get { return fCbTexture; }
        }
        #endregion

        #region YCrCb One Pass Separation
        /// <summary>
        /// Perform Color separation in one pass.  There are a total of 4 texture
        /// objects involved, a RenderTarget, and fragment shader.
        /// 
        /// The source texture, is assumed to be the video texture.  That is what
        /// will be rendered as a quad into the RenderTarget.
        /// 
        /// The Y, Cr, and Cb textures have been attached to the color buffer attachment
        /// points of the RenderTarget.
        /// 
        /// The shader program simply takes the source fragment, and performs the
        /// Y, Cr, Cb separation calculations and places the output into the appropriate
        /// color buffers.
        /// 
        /// And the end, the Y, Cr, Cb texture objects contain the results.
        /// 
        /// All done in one pass, using a shader program, in the time that it takes to 
        /// render a single quad into an offscreen buffer.
        /// </summary>
        public void SeparateChannels(GLTexture2D currentTexture)
        {
            GLSLShaderProgram currentProgram = fYCrCbChannelSep;
            GLRenderTarget currentTarget = fYCrCbTarget;

            int[] buffers = { gl.GL_COLOR_ATTACHMENT0_EXT, 
		     gl.GL_COLOR_ATTACHMENT1_EXT, 
		     gl.GL_COLOR_ATTACHMENT2_EXT};

            // First draw the current texture into an offscreen buffer
            // just so we can do the color separation.
            currentTarget.Bind();
            {
                GI.DrawBuffers(3, buffers);


                GI.Features.Texturing2D.Enable();

                // We want replace mode because we don't care about
                // the values that are currently in place, we just want
                // to replace them.
                GI.TexEnv(TextureEnvModeParam.Replace);


                // Set the viewport to be the whole texture
                GI.Viewport(0, 0, currentTexture.Width, currentTexture.Height);

                GI.MatrixMode(MatrixMode.Projection);
                GI.LoadIdentity();

                GI.Ortho(0, currentTexture.Width, 0, currentTexture.Height, -1.0, 1.0);
                //GI.FrontFace(FrontFaceDirection.Cw);

                // The current program is the channel separation program.  it will change
                // depending on which channel we are currently separating.
                currentProgram.Bind();
                {
                    // We want to turn off filtering.  We do that by using 'nearest'
                    // we also clamp the coordinates instead of replicating at the edges
                    currentTexture.Bind();
                    GI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureMinFilter, TextureMinFilter.Nearest);
                    GI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureMagFilter, TextureMagFilter.Nearest);
                    GI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureWrapS, TextureWrapMode.Clamp);
                    GI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureWrapT, TextureWrapMode.Clamp);

                    currentProgram["tex0"].Set((int)0);

                    GI.Drawing.Quads.Begin();
                    {
                        float left = 0;
                        float bottom = 0;
                        float right = currentTexture.Width;
                        float top = currentTexture.Height;

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

                    currentTexture.Unbind();
                }
                currentProgram.Unbind();

            }
            currentTarget.Unbind();

        }
        #endregion

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
        /// This fragment shader will separate an incoming texture, as 
        /// specified in the tex0 uniform variable, into a Y, Cr, and Cb
        /// channel.  Its operation is dependent on the grahics driver support
        /// of the GL_ARB_draw_buffers extension.  This will not operate on 
        /// all machines.
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
    //float Cr = (color.b-Y)*0.565;

    float Cb = -0.169*color.r-0.331*color.g+0.500*color.b;
    //float Cb = (color.r-Y)*0.713;

   
   gl_FragData[0] = vec4(Y, Y, Y, 1.0);
   gl_FragData[1] = vec4(Cr, Cr, Cr, 1.0);
   gl_FragData[2] = vec4(Cb, Cb, Cb, 1.0);
}
";

        #region YCrCb Three Pass Separation
        //void ProcessImage(GLTexture currentTexture)
        //{
        //    GLSLShaderProgram currentProgram = null;
        //    GLRenderTarget currentTarget = null;


        //    for (int ctr = 0; ctr < 3; ctr++)
        //    {
        //        switch (ctr)
        //        {
        //            case 0:
        //                currentProgram = fYChannelSep;
        //                currentTarget = fYTarget;
        //                break;

        //            case 1:
        //                currentProgram = fUChannelSep;
        //                currentTarget = fCrTarget;
        //                break;

        //            case 2:
        //                currentProgram = fVChannelSep;
        //                currentTarget = fCbTarget;
        //                break;
        //        }

        //        // First draw the current texture into an offscreen buffer
        //        // just so we can do the color separation.
        //        currentTarget.Bind();
        //        {
        //            // Set the viewport to be the whole texture
        //            GI.Viewport(0, 0, currentTexture.Width, currentTexture.Height);

        //            GI.MatrixMode(MatrixMode.Projection);
        //            GI.LoadIdentity();

        //            GI.Ortho(0, currentTexture.Width, 0, currentTexture.Height, -1.0, 1.0);

        //            // The current program is the channel separation program.  it will change
        //            // depending on which channel we are currently separating.
        //            currentProgram.Bind();
        //            {
        //                currentTexture.Bind();
        //                currentProgram["tex0"].Set((int)0);

        //                GI.Drawing.Quads.Begin();
        //                {
        //                    float left = 0;
        //                    float bottom = 0;
        //                    float right = currentTexture.Width;
        //                    float top = currentTexture.Height;

        //                    // Left bottom
        //                    GI.TexCoord(0.0f, 0.0f);
        //                    GI.Vertex(left, bottom, 0.0f);

        //                    // Left top
        //                    GI.TexCoord(0.0f, 1.0f);
        //                    GI.Vertex(left, top, 0.0f);

        //                    // Right top
        //                    GI.TexCoord(1.0f, 1.0f);
        //                    GI.Vertex(right, top, 0.0f);

        //                    // Right bottom
        //                    GI.TexCoord(1.0f, 0.0f);
        //                    GI.Vertex(right, bottom, 0.0f);
        //                }
        //                GI.Drawing.Quads.End();

        //                currentTexture.Unbind();
        //            }
        //            currentProgram.Unbind();

        //        }
        //        currentTarget.Unbind();
        //    }
        //}
        #endregion
    }
}
