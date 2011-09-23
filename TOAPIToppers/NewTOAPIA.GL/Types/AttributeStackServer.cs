
namespace NewTOAPIA.GL
{
    public class AttributeStackServer : GIObject
    {
        public AttributeStackServer(GraphicsInterface gi)
            : base(gi)
        {
        }

        public void Pop()
        {
            GI.PopAttrib();
        }

        public void Push(AttribMask mask)
        {
            GI.PushAttrib(mask);
        }

    }
}
