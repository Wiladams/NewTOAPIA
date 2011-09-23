

namespace NewTOAPIA.Modeling.Particles
{
    using NewTOAPIA.Drawing;
    using NewTOAPIA.GL;
    using NewTOAPIA.Graphics;

    using Real = System.Single;
    
    abstract public class ParticleSystem : IRenderable
    {
        #region Fields
        float3[] fPosition;
        float3[] fVelocity;
        float3[] fAcceleration;

        float[] fMass;
        float[] fMassInverse;

        protected int fNumParticles;

        protected Real fStep;
        protected Real fHalfStep;
        protected Real fSixthStep;
        double fAccumulatedTime;
        Vector3D fForce;

        // temporary storage for solver
        float3[] m_akPTmp, m_akDPTmp1, m_akDPTmp2, m_akDPTmp3, m_akDPTmp4;
        float3[] m_akVTmp, m_akDVTmp1, m_akDVTmp2, m_akDVTmp3, m_akDVTmp4;

        #endregion

        #region Constructor
        public ParticleSystem(int numParticles, Real step)
        {
            fNumParticles = numParticles;

            fPosition = new float3[numParticles];
            fVelocity = new float3[numParticles];
            fAcceleration = new float3[numParticles];
            fMass = new float[numParticles];
            fMassInverse = new float[numParticles];

            SetStep(step);

            // Setup variables used by the Runge-Kutta fourth-order solver
            m_akPTmp = new float3[numParticles];
            m_akDPTmp1 = new float3[numParticles];
            m_akDPTmp2 = new float3[numParticles];
            m_akDPTmp3 = new float3[numParticles];
            m_akDPTmp4 = new float3[numParticles];

            m_akVTmp = new float3[numParticles];
            m_akDVTmp1 = new float3[numParticles];
            m_akDVTmp2 = new float3[numParticles];
            m_akDVTmp3 = new float3[numParticles];
            m_akDVTmp4 = new float3[numParticles];
        }
        #endregion

        #region Properties
        public Vector3D Force
        {
            get { return fForce; }
            set { fForce = value; }
        }

        //public Particles Particles
        //{
        //    get { return fParticles; }
        //}

        public float[] Mass
        {
            get { return fMass; }
        }

        public float[] MassInverse
        {
            get { return fMassInverse; }
        }

        public int NumParticles
        {
            get { return fNumParticles; }
        }

        public float3[] Position
        {
            get { return fPosition; }
        }

        public float3[] Velocity
        {
            get { return fVelocity; }
        }

        #endregion

        #region Abstract Methods
        abstract protected float3 Acceleration (int i, Real fTime, float3[] position, float3[] velocity);
        abstract public void Render(GraphicsInterface gi);
        abstract protected void InitializeParticle(int index);
        #endregion

        #region Methods
        public void SetStep(Real step)
        {
            fStep = step;
            fHalfStep = step / 2;
            fSixthStep = step / 6;
        }

        public virtual void Update(Real fTime)
        {
            // Runge-Kutta fourth-order solver
            Real fHalfTime = fTime + fHalfStep;
            Real fFullTime = fTime + fStep;

            // first step
            int i;
            for (i = 0; i < fNumParticles; i++)
            {
                if (MassInverse[i] > (Real)0.0)
                {
                    m_akDPTmp1[i] = Velocity[i];
                    m_akDVTmp1[i] = Acceleration(i, fTime, Position, Velocity);
                }
            }
            for (i = 0; i < fNumParticles; i++)
            {
                if (MassInverse[i] > (Real)0.0)
                {
                    m_akPTmp[i] = Position[i] + fHalfStep * m_akDPTmp1[i];
                    m_akVTmp[i] = Velocity[i] + fHalfStep * m_akDVTmp1[i];
                }
                else
                {
                    m_akPTmp[i] = Position[i];
                    m_akVTmp[i] = Vector3D.Zero;
                }
            }

            // second step
            for (i = 0; i < fNumParticles; i++)
            {
                if (MassInverse[i] > (Real)0.0)
                {
                    m_akDPTmp2[i] = m_akVTmp[i];
                    m_akDVTmp2[i] = Acceleration(i, fHalfTime, m_akPTmp, m_akVTmp);
                }
            }
            for (i = 0; i < fNumParticles; i++)
            {
                if (MassInverse[i] > (Real)0.0)
                {
                    m_akPTmp[i] = Position[i] + fHalfStep * m_akDPTmp2[i];
                    m_akVTmp[i] = Velocity[i] + fHalfStep * m_akDVTmp2[i];
                }
                else
                {
                    m_akPTmp[i] = Position[i];
                    m_akVTmp[i] = float3.Zero;
                }
            }

            // third step
            for (i = 0; i < fNumParticles; i++)
            {
                if (MassInverse[i] > (Real)0.0)
                {
                    m_akDPTmp3[i] = m_akVTmp[i];
                    m_akDVTmp3[i] = Acceleration(i, fHalfTime, m_akPTmp, m_akVTmp);
                }
            }
            for (i = 0; i < fNumParticles; i++)
            {
                if (MassInverse[i] > (Real)0.0)
                {
                    m_akPTmp[i] = Position[i] + fStep * m_akDPTmp3[i];
                    m_akVTmp[i] = Velocity[i] + fStep * m_akDVTmp3[i];
                }
                else
                {
                    m_akPTmp[i] = Position[i];
                    m_akVTmp[i] = Vector3D.Zero;
                }
            }

            // fourth step
            for (i = 0; i < fNumParticles; i++)
            {
                if (MassInverse[i] > (Real)0.0)
                {
                    m_akDPTmp4[i] = m_akVTmp[i];
                    m_akDVTmp4[i] = Acceleration(i, fFullTime, m_akPTmp, m_akVTmp);
                }
            }
            for (i = 0; i < fNumParticles; i++)
            {
                if (MassInverse[i] > (Real)0.0)
                {
                    Position[i] += fSixthStep * (m_akDPTmp1[i] +
                        ((Real)2.0) * (m_akDPTmp2[i] + m_akDPTmp3[i]) + m_akDPTmp4[i]);
                    Velocity[i] += fSixthStep * (m_akDVTmp1[i] +
                        ((Real)2.0) * (m_akDVTmp2[i] + m_akDVTmp3[i]) + m_akDVTmp4[i]);
                }
            }
        }

        //public virtual int Emit(int numParticles)
        //{
        //    // create new particles, up to the amount of room we have
        //    // available based on maxParticles
        //    while ((numParticles > 0) && (fNumParticles < fMaxParticles))
        //    {
        //        // Initialize current particle, and increase the count
        //        InitializeParticle(fNumParticles);
        //        fNumParticles++;
        //        numParticles--;
        //    }

        //    return numParticles;
        //}

        //public virtual void InitializeSystem()
        //{
        //    fParticles = new Particles(fMaxParticles);

        //    fNumParticles = 0;
        //    fAccumulatedTime = 0;
        //}

        public virtual void KillSystem()
        {
            fNumParticles = 0;
        }
        #endregion
    }
}
