
namespace NewTOAPIA.Modeling.Particles
{
    using NewTOAPIA;
    using NewTOAPIA.Drawing;
    using NewTOAPIA.Graphics;

    public class Particles
    {
        Point3D[] fPosition;
        Vector3D[] fVelocity;
        Vector3D[] fAcceleration;

        float[] fMass;
        float[] fMassInverse;

        VertexAttributes[] fAttributes;

        //public Particles(VertexAttributes[] attributes, VertexBuffer vBuffer, float3[] locations, float[] sizes)
        //    : base(vBuffer, null)
        //{
        //    fAttributes = attributes;
        //    fLocations = locations;
        //    fSizes = sizes;
        //}

        public Particles(int maxParticles)
        {
            fPosition = new Point3D[maxParticles];
            fVelocity = new Vector3D[maxParticles];
            fAcceleration = new Vector3D[maxParticles];
            fMass = new float[maxParticles];
            fMassInverse = new float[maxParticles];
        }

        //public int ActiveQuantity
        //{
        //    get { return fActiveQuantity; }
        //    set
        //    {
        //        fActiveQuantity = value;
        //    }
        //}

        public Point3D[] Position
        {
            get { return fPosition; }
        }

        public Vector3D[] Velocity
        {
            get { return fVelocity; }
        }

        public Vector3D[] Acceleration
        {
            get { return fAcceleration; }
        }

        public float[] Mass
        {
            get { return fMass; }
        }

        public float[] MassInverse
        {
            get { return fMassInverse; }
        }
    }
}
