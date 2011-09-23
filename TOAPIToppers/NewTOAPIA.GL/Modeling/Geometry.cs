
namespace NewTOAPIA.Modeling
{
    using System;
    using System.Collections.Generic;

    public class Geometry : Spacial
    {
        public enum GeometryType
        {
            Points,         // A set of points
            Lines,          // A set of disjoint lines
            LineStrip,      // A set of connected lines
            LineLoop,       // A set of connected lines that join
            TriangleMesh,   // A set of disjoint triangles
            MaxQuantity,
        }

        internal GeometryType fGeometryType;

        VertexBuffer fVertexBuffer;
        IndexBuffer fIndexBuffer;
        BoundingVolume fBoundingVolume;
        List<Effect> fEffects;

        internal GlobalStateManager fGlobalState;

        protected Geometry(VertexBuffer vBuffer, IndexBuffer iBuffer)
        {
            fGlobalState = new GlobalStateManager();

            fVertexBuffer = vBuffer;
            fIndexBuffer = iBuffer;
        }

        public BoundingVolume BoundingVolume
        {
            get { return fBoundingVolume; }
        }

        public VertexBuffer Vertices
        {
            get { return fVertexBuffer; }
            set { fVertexBuffer = value; }
        }

        public IndexBuffer Indices
        {
            get { return fIndexBuffer; }
            set { fIndexBuffer = value; }
        }

        public GeometryType TypeOfGeometry
        {
            get { return fGeometryType; }
            protected set { fGeometryType = value; }
        }

        public List<Effect> Effects
        {
            get
            {
                if (null == fEffects)
                {
                    fEffects = new List<Effect>();
                }
                return fEffects;
            }
        }
    }
}
