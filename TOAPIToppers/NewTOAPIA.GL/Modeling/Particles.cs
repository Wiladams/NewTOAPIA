using NewTOAPIA;

namespace NewTOAPIA.Modeling
{
    public class Particles
    {
        float3[] fPosition;
        float3[] fVelocity;
        float3[] fAcceleration;

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
            fPosition = new float3[maxParticles];
            fVelocity = new float3[maxParticles];
            fAcceleration = new float3[maxParticles];
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

        public float3[] Position
        {
            get { return fPosition; }
        }

        public float3[] Velocity
        {
            get { return fVelocity; }
        }

        public float3[] Acceleration
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
