using TOAPI.OpenGL;

namespace NewTOAPIA.GL
{
    /// <summary>
    /// This is the status reported when you ask whether a frame buffer
    /// is complete or not.  'Complete' means everything is ok, and you 
    /// can use the framebuffer.  Anything else indicates there is a 
    /// problem to be solved.
    /// </summary>
    public enum FrameBufferStatus
    {
        Complete = gl.GL_FRAMEBUFFER_COMPLETE_EXT,

        IncompleteAttachment = gl.GL_FRAMEBUFFER_INCOMPLETE_ATTACHMENT_EXT,
        IncompleteAttachmentMissing = gl.GL_FRAMEBUFFER_INCOMPLETE_MISSING_ATTACHMENT_EXT,
        IncompleteDimensions = gl.GL_FRAMEBUFFER_INCOMPLETE_DIMENSIONS_EXT,
        IncompleteDrawingBuffer = gl.GL_FRAMEBUFFER_INCOMPLETE_DRAW_BUFFER_EXT,
        IncompleteDuplicateAttachment = gl.GL_FRAMEBUFFER_INCOMPLETE_DUPLICATE_ATTACHMENT_EXT,
        IncompleteFormats = gl.GL_FRAMEBUFFER_INCOMPLETE_FORMATS_EXT,
        IncompleteReadBuffer = gl.GL_FRAMEBUFFER_INCOMPLETE_READ_BUFFER_EXT,

        Unsupported = gl.GL_FRAMEBUFFER_UNSUPPORTED_EXT
    }

    public enum ColorBufferAttachPoint
    {
        Position0 = gl.GL_COLOR_ATTACHMENT0_EXT,
        Position1 = gl.GL_COLOR_ATTACHMENT1_EXT,
        Position2 = gl.GL_COLOR_ATTACHMENT2_EXT,
        Position3 = gl.GL_COLOR_ATTACHMENT3_EXT,
        Position4 = gl.GL_COLOR_ATTACHMENT4_EXT,
        Position5 = gl.GL_COLOR_ATTACHMENT5_EXT,
        Position6 = gl.GL_COLOR_ATTACHMENT6_EXT,
        Position7 = gl.GL_COLOR_ATTACHMENT7_EXT,
        Position8 = gl.GL_COLOR_ATTACHMENT8_EXT,
        Position9 = gl.GL_COLOR_ATTACHMENT9_EXT,
        Position10 = gl.GL_COLOR_ATTACHMENT10_EXT,
        Position11 = gl.GL_COLOR_ATTACHMENT11_EXT,
        Position12 = gl.GL_COLOR_ATTACHMENT12_EXT,
        Position13 = gl.GL_COLOR_ATTACHMENT13_EXT,
        Position14 = gl.GL_COLOR_ATTACHMENT14_EXT,
        Position15 = gl.GL_COLOR_ATTACHMENT15_EXT,
    }

    public class GLFrameBufferObject : IBindable
    {
        int fBufferID;
        GraphicsInterface fGI;

        #region Constructors
        public GLFrameBufferObject(GraphicsInterface gi)
        {
            fGI = gi;
            fBufferID = GetNewID(gi);
        }
        #endregion

        #region Public Methods
        #region Binding
        public void Bind()
        {
            GI.BindFramebuffer(gl.GL_FRAMEBUFFER_EXT, BufferID);
        }

        public void Unbind()
        {
            GI.BindFramebuffer(gl.GL_FRAMEBUFFER_EXT, 0);
        }
        #endregion

        #region Attach
        #region Attach Color Buffer
        public void AttachColorBuffer(GLTexture2D colorbuffer)
        {
            GI.FramebufferTexture2D(gl.GL_FRAMEBUFFER_EXT, ColorBufferAttachPoint.Position0, gl.GL_TEXTURE_2D, (int)colorbuffer.TextureID, 0);
        }

        public void AttachColorBuffer(GLTexture2D colorbuffer, ColorBufferAttachPoint attachPosition)
        {
            GI.FramebufferTexture2D(gl.GL_FRAMEBUFFER_EXT, attachPosition, gl.GL_TEXTURE_2D, (int)colorbuffer.TextureID, 0);
        }
        #endregion

        /// <summary>
        /// Attach a RenderBuffer as the depth buffer of the FrameBuffer set.
        /// </summary>
        /// <param name="depthbuffer"></param>
        public void AttachDepthBuffer(GLRenderBuffer depthbuffer)
        {
            GI.FramebufferRenderbuffer(gl.GL_FRAMEBUFFER_EXT, gl.GL_DEPTH_ATTACHMENT_EXT, gl.GL_RENDERBUFFER_EXT, depthbuffer.BufferID);
        }

        #region Attach1DTexture
        public void Attach1DTexture(ColorBufferAttachPoint attachType, GLTexture2D tex, int mipLevel)
        {
            GI.FramebufferTexture1D(gl.GL_FRAMEBUFFER_EXT, attachType, gl.GL_TEXTURE_1D, (int)tex.TextureID, mipLevel);
        }

        public void Attach1DTexture(ColorBufferAttachPoint attachType, GLTexture2D tex)
        {
            GI.FramebufferTexture1D(gl.GL_FRAMEBUFFER_EXT, attachType, gl.GL_TEXTURE_1D, (int)tex.TextureID, 0);
        }

        public void Attach1DTexture(GLTexture2D tex)
        {
            GI.FramebufferTexture1D(gl.GL_FRAMEBUFFER_EXT, ColorBufferAttachPoint.Position0, (int)gl.GL_TEXTURE_1D, (int)tex.TextureID, 0);
        }
        #endregion

        #region Attach2DTexture
        public void Attach2DTexture(ColorBufferAttachPoint attachType, GLTexture2D tex, int mipLevel)
        {
            GI.FramebufferTexture2D(gl.GL_FRAMEBUFFER_EXT, attachType, gl.GL_TEXTURE_2D, (int)tex.TextureID, mipLevel);
        }

        public void Attach2DTexture(ColorBufferAttachPoint attachType, GLTexture2D tex)
        {
            GI.FramebufferTexture2D(gl.GL_FRAMEBUFFER_EXT, attachType, gl.GL_TEXTURE_2D, (int)tex.TextureID, 0);
        }

        public void Attach2DTexture(GLTexture2D tex)
        {
            GI.FramebufferTexture2D(gl.GL_FRAMEBUFFER_EXT, ColorBufferAttachPoint.Position0, gl.GL_TEXTURE_2D, (int)tex.TextureID, 0);
        }
        #endregion

        #region Attach3DTexture
        public void Attach3DTexture(GLTexture2D tex, ColorBufferAttachPoint attachType, int mipLevel, int zOffset)
        {
            GI.FramebufferTexture3D(gl.GL_FRAMEBUFFER_EXT, attachType, gl.GL_TEXTURE_3D, (int)tex.TextureID, mipLevel, zOffset);
        }

        public void Attach3DTexture(GLTexture2D tex, ColorBufferAttachPoint attachType, int mipLevel)
        {
            GI.FramebufferTexture3D(gl.GL_FRAMEBUFFER_EXT, attachType, gl.GL_TEXTURE_3D, (int)tex.TextureID, mipLevel, 0);
        }

        public void Attach3DTexture(GLTexture2D tex, ColorBufferAttachPoint attachType)
        {
            GI.FramebufferTexture3D(gl.GL_FRAMEBUFFER_EXT, attachType, gl.GL_TEXTURE_3D, (int)tex.TextureID, 0, 0);
        }

        public void Attach3DTexture(GLTexture2D tex)
        {
            GI.FramebufferTexture3D(gl.GL_FRAMEBUFFER_EXT, ColorBufferAttachPoint.Position0, gl.GL_TEXTURE_3D, (int)tex.TextureID, 0, 0);
        }
        #endregion

        #region AttachRenderBuffer
        public void AttachRenderBuffer(GLRenderBuffer rendBuffer, int attachtype)
        {
            GI.FramebufferRenderbuffer(gl.GL_FRAMEBUFFER_EXT, attachtype, gl.GL_RENDERBUFFER_EXT, rendBuffer.BufferID);
        }

        public void AttachRenderBuffer(GLRenderBuffer rendBuffer)
        {
            GI.FramebufferRenderbuffer(gl.GL_FRAMEBUFFER_EXT, gl.GL_COLOR_ATTACHMENT0_EXT, gl.GL_RENDERBUFFER_EXT, rendBuffer.BufferID);
        }
        #endregion
        #endregion

        #endregion

        #region Properties
        public int BufferID
        {
            get { return fBufferID; }
        }

        public GraphicsInterface GI
        {
            get { return fGI; }
        }

        #endregion
       
        #region Public Static Methods
        public static int GetNewID(GraphicsInterface gi)
        {
            int[] IDs = new int[1];

            gi.GenFramebuffers(1, IDs);

            return IDs[0];
        }
        #endregion
    }
}
