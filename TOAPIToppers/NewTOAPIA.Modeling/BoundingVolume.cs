

namespace NewTOAPIA.Modeling
{
    using NewTOAPIA.Graphics;

    public abstract class BoundingVolume
    {
        #region Properties
        abstract public float3 Center{get;set;}
        abstract public float Radius { get; set; }
        #endregion

        #region Methods
        abstract public void Compute(float3[] vertices);

        abstract public  void TransformBy(Transformation aTransform, out BoundingVolume result);

        abstract public int WhichSide(Plane3f aPlane);

        abstract public bool TestIntersection(float3 origin, float3 direction);

        abstract public bool TestIntersection(BoundingVolume input);

        abstract public void CopyFrom(BoundingVolume aVolume);

        abstract public void GrowToContain(BoundingVolume aVolume);

        abstract public bool Contains(float3 aPoint);
        #endregion
    }
}
