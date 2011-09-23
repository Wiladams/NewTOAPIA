

namespace NewTOAPIA.Modeling
{
    using NewTOAPIA.GL;

    public class GLRenderable : Node, IRenderable
    {
        protected virtual void BeginRender(GraphicsInterface gi)
        {
        }

        protected virtual void EndRender(GraphicsInterface gi)
        {
        }

        protected virtual void RenderContent(GraphicsInterface gi)
        {
        }

        public virtual void Render(GraphicsInterface gi)
        {
            BeginRender(gi);
            RenderContent(gi);
            EndRender(gi);
        }
    }
}
