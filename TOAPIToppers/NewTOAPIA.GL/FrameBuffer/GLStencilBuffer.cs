
namespace NewTOAPIA.GL
{
    public class GLStencilBuffer : GLBuffer
    {
        int fStencil;

        public GLStencilBuffer(GraphicsInterface gi)
            : base(gi, ClearBufferMask.StencilBufferBit)
        {
        }

        public int Stencil
        {
            get { return fStencil; }
            set
            {
                SetStencil(value);
            }
        }

        public void SetStencil(int stencil)
        {
            fStencil = stencil;
            GI.ClearStencil(stencil);
        }
    }
}
