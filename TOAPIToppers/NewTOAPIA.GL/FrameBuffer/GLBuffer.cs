
namespace NewTOAPIA.GL
{
    public class GLBuffer
    {
        ClearBufferMask fMask;
        GraphicsInterface fGI;

        public GLBuffer(GraphicsInterface gi, ClearBufferMask aMask)
        {
            fGI = gi;
            fMask = aMask;
        }

        public GraphicsInterface GI
        {
            get { return fGI; }
        }

        public virtual void Clear()
        {
            GI.Clear(fMask);
        }

    }
}
