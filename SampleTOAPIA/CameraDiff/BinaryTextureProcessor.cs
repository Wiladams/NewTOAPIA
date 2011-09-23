using System;


//using TOAPI.OpenGL;

namespace NewTOAPIA.GL.Imaging
{
    /// <summary>
    /// This class serves as a base class for all image processors that follow a
    /// particular pattern.  That pattern is there is a single fragment shader that
    /// is executed across the entirety of the image.  Grayscale conversion would 
    /// be one example.
    /// 
    /// I takes care of drawing into a Quad to ensure each texel of the source image
    /// is operated on.
    /// 
    /// A subclasser need only supply the fragment shader string.  This class will allocate
    /// a texture object based on the width and height specified and return that texture
    /// object to the caller during the ProcessImage() call.
    /// </summary>
    public class BinaryTextureProcessor : GIObject, IBinaryTextureProcessor
    {
        #region Fields
        int fWidth;
        int fHeight;

        GLTexture2D fOutputTexture;
        GLRenderTarget fOutputTarget;

        GLTexture2D fBaseTexture;
        GLTexture2D fBlendTexture;

        GLTextureUnit fBaseTextureUnit;
        GLTextureUnit fBlendTextureUnit;

        string fFragmentShaderString;
        GLSLShaderProgram fShaderProgram;

        #endregion

        #region Constructor
        public BinaryTextureProcessor(GraphicsInterface gi, int width, int height)
            : this(gi, width, height, UnaryTextureProcessor.FixedFrag)
        {
        }

        public BinaryTextureProcessor(GraphicsInterface gi, int width, int height, string fragmentString)
            : base(gi)
        {
            fWidth = width;
            fHeight = height;

            fFragmentShaderString = fragmentString;

            fBaseTextureUnit = GLTextureUnit.GetTextureUnit(gi, TextureUnitID.Unit1);
            fBlendTextureUnit = GLTextureUnit.GetTextureUnit(gi, TextureUnitID.Unit0);
        }
        #endregion

        protected virtual GLTexture2D CreateOutputTexture(int width, int height)
        {
            GLTexture2D aTexture = new GLTexture2D(GI, width, height, TextureInternalFormat.Rgb, TexturePixelFormat.Bgr, PixelComponentType.UnsignedByte, IntPtr.Zero, false);

            return aTexture;
        }

        public GLRenderTarget CreateRenderTarget()
        {
            fOutputTarget = new GLRenderTarget(GI);
            fOutputTarget.AttachColorBuffer(OutputTexture, ColorBufferAttachPoint.Position0);
            fOutputTarget.Unbind();

            return fOutputTarget;
        }

        #region Properties
        protected GLTexture2D BaseTexture
        {
            get { return fBaseTexture; }
        }

        protected GLTexture2D BlendTexture
        {
            get { return fBlendTexture; }
        }

        public GLRenderTarget OutputTarget
        {
            get
            {
                if (null == fOutputTarget)
                {
                    fOutputTarget = CreateRenderTarget();
                }
                return fOutputTarget;
            }

            set
            {
                fOutputTarget = value;
            }
        }

        public GLTexture2D OutputTexture
        {
            get
            {
                if (null == fOutputTexture)
                {
                    fOutputTexture = CreateOutputTexture(fWidth, fHeight);
                }
                return fOutputTexture;
            }
        }

        public virtual GLSLShaderProgram ShaderProgram
        {
            get
            {
                if (null == fShaderProgram)
                {
                    fShaderProgram = GLSLShaderProgram.CreateUsingVertexAndFragmentStrings(GI, FixedVert, fFragmentShaderString);
                }
                return fShaderProgram;
            }

            set
            {
                fShaderProgram = value;
            }
        }

        public GLTextureUnit BaseTextureUnit
        {
            get { return fBaseTextureUnit; }
        }

        public GLTextureUnit BlendTextureUnit
        {
            get { return fBlendTextureUnit; }
        }

        public int Width
        {
            get { return fWidth; }
        }

        public int Height
        {
            get { return fHeight; }
        }
        #endregion

        #region Object Binding
        #region Binding Render Target
        protected virtual void BindRenderTarget()
        {
            if (null != OutputTarget)
            {
                OutputTarget.Bind();
            }
        }

        protected virtual void UnbindRenderTarget()
        {
            if (null != OutputTarget)
                OutputTarget.Unbind();
        }
        #endregion

        #region Binding Shader Program
        protected virtual void SetUniformVariables()
        {
        }

        protected virtual void BindShaderProgram()
        {
            ShaderProgram.Bind();
            SetUniformVariables();
        }

        protected virtual void UnbindShaderProgram()
        {
            ShaderProgram.Unbind();
        }
        #endregion

        #region Binding Base Texture
        protected virtual void BindTextures()
        {
            GI.Features.Texturing2D.Disable();

            BindTexture(fBaseTextureUnit, fBaseTexture);
            BindTexture(fBlendTextureUnit, fBlendTexture);
        }

        protected virtual void UnbindTextures()
        {
            fBlendTextureUnit.Unbind();
            fBaseTextureUnit.Unbind();
        }

        protected virtual void BindTexture(GLTextureUnit aUnit, GLTexture aTexture)
        {
            // We want to turn off filtering.  We do that by using 'nearest'
            // we also clamp the coordinates instead of replicating at the edges
            aUnit.Bind();
            aTexture.Bind();
            GI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureMinFilter, TextureMinFilter.Nearest);
            GI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureMagFilter, TextureMagFilter.Nearest);
            GI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureWrapS, TextureWrapMode.Clamp);
            GI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureWrapT, TextureWrapMode.Clamp);
        }

        #endregion
        #endregion

        #region Doing the drawing
        protected virtual void RenderGeometry()
        {
            // Set the viewport to be the whole texture
            GI.Viewport(0, 0, BaseTexture.Width, BaseTexture.Height);

            GI.MatrixMode(MatrixMode.Projection);
            GI.LoadIdentity();

            GI.Ortho(0, BaseTexture.Width, 0, BaseTexture.Height, -1.0, 1.0);

            GI.Drawing.Quads.Begin();
            {
                float left = 0;
                float bottom = 0;
                float right = BaseTexture.Width;
                float top = BaseTexture.Height;

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
        }
        #endregion

        /// <summary>
        /// Peform conversion to grayscale
        /// </summary>
        /// <param name="baseTexture"></param>
        /// <returns></returns>
        public virtual GLTexture2D ProcessTwoTextures(GLTexture2D baseTexture, GLTexture2D blendTexture)
        {
            fBaseTexture = baseTexture;
            fBlendTexture = blendTexture;

            // First draw the current texture into an offscreen buffer
            // just so we can do the color separation.
            BindRenderTarget();
            BindTextures();
            BindShaderProgram();

            RenderGeometry();

            UnbindShaderProgram();
            UnbindTextures();
            UnbindRenderTarget();

            return OutputTexture;
        }

        #region Useful Shader Strings
        public static string FixedVert = @"
//#extension GL_ARB_draw_buffers : enable

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
uniform sampler2D Tex1;

void main (void)
{
	gl_FragColor = texture2D(Tex0, gl_TexCoord[0].st);
}
";
        #endregion
    }
}
