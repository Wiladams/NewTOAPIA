using System;

using NewTOAPIA;
using NewTOAPIA.Graphics;

namespace Cloth
{
    class FlagSystem : ParticlesSystem
    {
        public float x0 = 0.0f;
        public float y0 = -3.0f;
        public float z0 = 0.0f;

        // Two attach points for the flag
        private float3 fixed0 = new float3();
        private float3 fixed1 = new float3();
        private int indLastRow;

        private readonly float3 windForce = new float3(40.0f, 0, 0.1f);
        private readonly float3 gravForce = new float3(0, -9.81f, 0);       // acceleration in the negative y direction

        public double WindStrength = 1.0;
        public bool ApplyWind = true;

        public FlagSystem(Resolution res, float timeStep)
            : base(res, timeStep)
        {
        
        }

        /// <summary>
        /// We have wind and gravity.  
        /// We want them to be applied to the normal vectors of each vertex.
        /// </summary>
        protected override void acumulateForces()
        {
            float fn;

            for (int i = 0; i < numPart; i++)
            {
                if (ApplyWind)
                {
                    fn = WindStrength * windForce * normals[i];
                } 
                else
                    fn = 0;

                forces[i] = fn * normals[i] + gravForce;
            }
        }

        protected override void satisfyConstraints()
        {
            float3 d = new float3();
            float dLength;
            float diff;
            int p1, p2;


            for (int n = 0; n < 6; n++)
            {
                //verificar distancia entre particulas

                for (int i = 0; i < constraints.Length; i++)  //internal constraints
                {
                    // mejora: aproximacion por polinomio de Taylor de 1 orden

                    p1 = constraints[i].p1;
                    p2 = constraints[i].p2;

                    d[0] = positions[p2][0] - positions[p1][0];
                    d[1] = positions[p2][1] - positions[p1][1];
                    d[2] = positions[p2][2] - positions[p1][2];

                    dLength = (float)Math.Sqrt(d[0] * d[0] + d[1] * d[1] + d[2] * d[2]);
                    diff = (dLength - constraints[i].dist) / dLength;

                    positions[p1][0] += 0.5f * diff * d[0];
                    positions[p1][1] += 0.5f * diff * d[1];
                    positions[p1][2] += 0.5f * diff * d[2];

                    positions[p2][0] -= 0.5f * diff * d[0];
                    positions[p2][1] -= 0.5f * diff * d[1];
                    positions[p2][2] -= 0.5f * diff * d[2];
                }

                positions[0] = fixed0;
                positions[indLastRow] = fixed1;
            }
        }

        protected override void makeQuads()
        {
            int ind;

            positions = new float3[numPart];
            oldPositions = new float3[numPart];

            for (int i = 0; i < nHeight; i++)
                for (int j = 0; j < nWidth; j++)
                {
                    ind = i * nWidth + j;
                    positions[ind][0] = x0 + j * size;
                    positions[ind][1] = y0 + i * size;
                    positions[ind][2] = z0;
                }

            fixed0 = positions[0];

            indLastRow = (nHeight - 1) * nWidth;

            fixed1 = positions[indLastRow];

            vectorCopy(oldPositions, positions);
        }
    }
}