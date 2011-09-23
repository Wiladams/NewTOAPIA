
namespace NewTOAPIA.Modeling
{
    public class TriangleMesh : Geometry
    {
        public TriangleMesh(VertexBuffer vBuffer, IndexBuffer iBuffer)
            :base(vBuffer, iBuffer)
        {
        }

        public int TriangleCount
        {
            get
            {
                int count = Indices.Count / 3;
                return count;
            }
        }

        public void GenerateNormals()
        {
        }

        protected virtual void UpdateModelNormals()
        {
        }
    }
}
