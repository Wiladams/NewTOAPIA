using System.Collections.Generic;

using TOAPI.OpenGL;

namespace NewTOAPIA.GL
{
    /// <summary>
    /// A GLRenderTarget is the encapsulation of a simple frame buffer object.  
    /// The simplest frame buffer has a color buffer, and a depth buffer.
    /// It is constructed given a width and height in pixels.
    /// 
    /// Usage:
    /// 
    /// GLRenderTarget myTarget = new GLRenderTarget(width, height);
    /// myTarget.Bind();
    /// // Do whatever rendering you want
    /// myTarget.Unbind();
    /// 
    /// The ColorBuffer property will contain the rendered color information.
    /// </summary>
    public class GLRenderTarget : IBindable
    {
        #region Fields
        int fWidth;
        int fHeight;

        GraphicsInterface fGI;

        GLFrameBufferObject fFrameBuffer;
        GLRenderBuffer fDepthBuffer;

        Dictionary<ColorBufferAttachPoint, GLTexture2D> fColorBuffers;
        #endregion

        #region Constructor
        public GLRenderTarget(GraphicsInterface gi)
        {
            fGI = gi;

            fColorBuffers = new Dictionary<ColorBufferAttachPoint, GLTexture2D>();

            fFrameBuffer = new GLFrameBufferObject(fGI);
        }

        /// <summary>
        /// This is the easiest constructor to use when you already have a texture, and you
        /// want to draw into it.  Just pass the texture in, and the RenderTarget will be
        /// constructed to match the size.
        /// </summary>
        /// <param name="gi"></param>
        /// <param name="texture"></param>
        public GLRenderTarget(GLTexture2D texture)
            : this(texture.GI)
        {
            fWidth = texture.Width;
            fHeight = texture.Height;

            FrameBuffer.Bind();

            // Create the render buffer to be attached as a depth
            // buffer.
            fDepthBuffer = new GLRenderBuffer(fGI);
            fDepthBuffer.Bind();
            fDepthBuffer.AllocateStorage(fWidth, fHeight, gl.GL_DEPTH_COMPONENT);
            
            fFrameBuffer.AttachDepthBuffer(fDepthBuffer);

            // Create the texture object to be attached as color buffer    
            AttachColorBuffer(texture, ColorBufferAttachPoint.Position0);

            FrameBuffer.Unbind();
        }

        /// <summary>
        /// Create a render target with the specified width and height in pixels.
        /// A color buffer and depth buffer will be attached.
        /// </summary>
        /// <param name="gi">The graphics interface used to construct the components.</param>
        /// <param name="width">The width of the RenderTarget in pixels.</param>
        /// <param name="height">The height of the RenderTarget in pixels.</param>
        public GLRenderTarget(GraphicsInterface gi, int width, int height)
            : this(gi)
        {
            fGI = gi;

            fWidth = width;
            fHeight = height;

            FrameBuffer.Bind();

            CreateDepthBuffer(width, height);

            // Create the texture object to be attached as color buffer            
            GLTexture2D colorbuffer = new GLTexture2D(fGI, width, height);
            AttachColorBuffer(colorbuffer, ColorBufferAttachPoint.Position0);

            FrameBuffer.Unbind();
        }

        #endregion

        #region Properties
        /// <summary>
        /// Returns the completeness status of the FrameBuffer.
        /// </summary>
        public FrameBufferStatus Completeness
        {
            get
            {
                int completeness = fGI.CheckFramebufferStatus(gl.GL_FRAMEBUFFER_EXT);
                return (FrameBufferStatus)completeness;
            }
        }

        /// <summary>
        /// ColorBuffer is a texture object that contains the color information 
        /// from the rendering process.
        /// </summary>
        public GLTexture2D ColorBuffer
        {
            get
            {
                return GetColorBuffer(ColorBufferAttachPoint.Position0);
            }
        }

        public GLFrameBufferObject FrameBuffer
        {
            get { return fFrameBuffer; }
        }
        #endregion

        #region Public Methods
        public void AttachColorBuffer(GLTexture2D colorbuffer, ColorBufferAttachPoint attachPosition)
        {
            fWidth = colorbuffer.Width;
            fHeight = colorbuffer.Height;

            Bind();
            colorbuffer.Bind();

            fFrameBuffer.AttachColorBuffer(colorbuffer, attachPosition);
            fColorBuffers.Add(attachPosition, colorbuffer);
        }

        public void CreateDepthBuffer()
        {
            if (0 == fWidth || 0 == fHeight)
                return;

            fFrameBuffer.Bind();
            CreateDepthBuffer(fWidth, fHeight);
        }

        void CreateDepthBuffer(int width, int height)
        {
            // Create the render buffer to be attached as a depth
            // buffer.
            fDepthBuffer = new GLRenderBuffer(fGI);
            fDepthBuffer.Bind();
            fDepthBuffer.AllocateStorage(width, height, gl.GL_DEPTH_COMPONENT);

            fFrameBuffer.AttachDepthBuffer(fDepthBuffer);
        }

        public GLTexture2D GetColorBuffer(ColorBufferAttachPoint attachPoint)
        {
            if (!fColorBuffers.ContainsKey(attachPoint))
                return null;

            else
                return fColorBuffers[attachPoint];
        }

        /// <summary>
        /// Make the RenderTarget the current target of rendering.
        /// </summary>
        public void Bind()
        {
            fFrameBuffer.Bind();
        }

        /// <summary>
        /// Switch back to the regular drawing context (typically the screen).
        /// </summary>
        public void Unbind()
        {
            fFrameBuffer.Unbind();
        }

        #endregion
    }
}
