using TOAPI.OpenGL;

namespace NewTOAPIA.GL
{
    public class GLBufferList
    {
        private GLColorBuffer fColorBuffer;
        private GLDepthBuffer fDepthBuffer;
        private GLStencilBuffer fStencilBuffer;
        private GLAccumBuffer fAccumBuffer;

        public GLBufferList(GraphicsInterface gi)
        {
            fColorBuffer = new GLColorBuffer(gi);
            fDepthBuffer = new GLDepthBuffer(gi);
            fStencilBuffer = new GLStencilBuffer(gi);
            fAccumBuffer = new GLAccumBuffer(gi);
        }

        public GLColorBuffer ColorBuffer
        {
            get { return fColorBuffer; }
        }

        public GLDepthBuffer DepthBuffer
        {
            get { return fDepthBuffer; }
        }

        public GLStencilBuffer StencilBuffer
        {
            get { return fStencilBuffer; }
        }

        public GLAccumBuffer AccumulationBuffer
        {
            get { return fAccumBuffer; }
        }
    }
}
