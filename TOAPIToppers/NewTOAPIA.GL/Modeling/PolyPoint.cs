

namespace NewTOAPIA.Modeling
{
    public class PolyPoint : Geometry
    {
        int fActiveQuantity;

        public PolyPoint(VertexBuffer aVertexBuffer)
            :base(aVertexBuffer, null)
        {
            fGeometryType = GeometryType.Points;

            int quantity = aVertexBuffer.Count;
            fActiveQuantity = quantity;

            IndexBuffer newIndices = new IndexBuffer(quantity);
            newIndices.SetDefaultIndices();

            Indices = newIndices;
        }

        public int ActiveQuantity
        {
            get { return fActiveQuantity; }
            set
            {
                fActiveQuantity = value;
            }
        }
    }
}
