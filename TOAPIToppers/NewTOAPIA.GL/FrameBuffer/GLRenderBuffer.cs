using TOAPI.OpenGL;

namespace NewTOAPIA.GL
{
    using NewTOAPIA;

    public class GLRenderBuffer : IBindable
    {
        int fBufferID;
        GraphicsInterface fGI;

        #region Constructors
        public GLRenderBuffer(GraphicsInterface gi)
        {
            fGI = gi;
            fBufferID = GetNewID(gi);
        }

        public GLRenderBuffer(GraphicsInterface gi, int width, int height, int format)
            : this(gi)
        {
            Bind();
            GI.RenderbufferStorage(gl.GL_RENDERBUFFER_EXT, format, width, height);
            Unbind();
        }
        #endregion

        #region Public Methods
        public void AllocateStorage(int width, int height, int format)
        {
            GI.RenderbufferStorage(gl.GL_RENDERBUFFER_EXT, format, width, height);            
        }

        public void Bind()
        {
            GI.BindRenderbuffer(gl.GL_RENDERBUFFER_EXT, BufferID);
        }

        public void Unbind()
        {
            GI.BindRenderbuffer(gl.GL_RENDERBUFFER_EXT, 0);
        }
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

            gi.GenRenderbuffers(1, IDs);

            return IDs[0];
        }
        #endregion
    }
}
