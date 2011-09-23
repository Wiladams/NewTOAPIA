

namespace NewTOAPIA.Drawing
{
    using NewTOAPIA.Graphics;

    //----------------------------------------------------------conv_transform
    public class conv_transform : IVertexSource
    {
        private IVertexSource m_VertexSource;
        private ITransform m_Transform;

        public conv_transform(IVertexSource VertexSource, ITransform InTransform)
        {
            m_VertexSource = VertexSource;
            m_Transform = InTransform;
        }

        public void attach(IVertexSource VertexSource) { m_VertexSource = VertexSource; }

        public void rewind(int path_id)
        {
            m_VertexSource.rewind(path_id);
        }

        public Path.FlagsAndCommand vertex(out double x, out double y)
        {
            Path.FlagsAndCommand cmd = m_VertexSource.vertex(out x, out y);
            if (Path.is_vertex(cmd))
            {
                m_Transform.Transform(ref x, ref y);
            }
            return cmd;
        }

        public void transformer(ITransform InTransform)
        {
            m_Transform = InTransform;
        }
    }
}