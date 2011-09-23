
namespace NewTOAPIA.GL
{
    public class GLDepthBuffer : GLBuffer
    {
        double fDepth;

        public GLDepthBuffer(GraphicsInterface gi)
            : base(gi, ClearBufferMask.DepthBufferBit)
        {
        }

        public virtual double Depth
        {
            get { return fDepth; }
            set
            {
                SetDepth(value);
            }
        }

        public virtual void SetDepth(double aDepth)
        {
            fDepth = aDepth;
            GI.ClearDepth(aDepth);
        }
    }
}
