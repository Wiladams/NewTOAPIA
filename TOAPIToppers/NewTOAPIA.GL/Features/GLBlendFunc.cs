
namespace NewTOAPIA.GL
{
    public class GLBlendFunc : GLAspect
    {
        BlendingFactorSrc fSrcBlendFactor;
        BlendingFactorDest fDstBlendFactor;

        public GLBlendFunc(GraphicsInterface gi, BlendingFactorSrc srcBlend, BlendingFactorDest dstBlend)
            :base(gi)
        {
            fSrcBlendFactor = srcBlend;
            fDstBlendFactor = dstBlend;
        }

        public virtual BlendingFactorSrc SourceBlendFactor
        {
            get {return fSrcBlendFactor;}
            set {
                SetSourceBlendFactor(value);
            }
        }

        public virtual BlendingFactorDest DestinationBlendFactor
        {
            get {return fDstBlendFactor;}
            set {
                SetDestinationBlendFactor(value);
            }
        }

        public virtual void SetSourceBlendFactor(BlendingFactorSrc aFactor)
        {
            fSrcBlendFactor = aFactor;
            Realize();
        }

        public virtual void SetDestinationBlendFactor(BlendingFactorDest aFactor)
        {
            fDstBlendFactor = aFactor;
            Realize();
        }

        public override void  Realize()
        {
            GL.BlendFunc(fSrcBlendFactor, fDstBlendFactor);
        }

    }
}
