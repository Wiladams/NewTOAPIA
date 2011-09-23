
namespace NewTOAPIA.Modeling
{
    public class PolyLine : Geometry
    {
        int fActiveQuantity;
        bool fIsClosed;
        bool fIsContiguous;

        public PolyLine(VertexBuffer vBuffer, bool closed, bool contiguous)
            : base(vBuffer, null)
        {
            fIsClosed = closed;
            fIsContiguous = contiguous;

            SetGeometryType();
        }

        public int ActiveQuantity
        {
            get { return fActiveQuantity; }
            set { fActiveQuantity = value; }
        }

        public bool Closed
        {
            get { return fIsClosed; }
            set { fIsClosed = true; }
        }

        public bool Continuous
        {
            get { return fIsContiguous; }
            set { fIsContiguous = value; }
        }

        void SetGeometryType()
        {
            if (fIsContiguous)
            {
                if (fIsClosed)
                {
                    if (TypeOfGeometry != GeometryType.LineLoop)
                    {
                        // Increase the index quantity to account for closing the loop
                        Indices.IncreaseCount();
                    }
                    fGeometryType = GeometryType.LineLoop;
                }
                else
                {
                    if (TypeOfGeometry == GeometryType.LineLoop)
                    {
                        Indices.DecreaseCount();
                    }
                    fGeometryType = GeometryType.LineStrip;
                }
            }
            else
            {
                if (TypeOfGeometry == GeometryType.LineLoop)
                {
                    Indices.DecreaseCount();
                }
                TypeOfGeometry = GeometryType.Lines;
            }
        }
    }
}
