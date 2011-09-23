
namespace Cloth
{
    using System;
    using TOAPI.OpenGL;
 
    using NewTOAPIA.GL;
    using NewTOAPIA.Graphics;
    using NewTOAPIA;
    using NewTOAPIA.Modeling;

    public class ParticlesSystem : GLRenderable
    {
        public int nWidth;
        public int nHeight;
        public float size;
        public int uTile = 1;
        public int vTile = 1;

        public float3[] positions;
        protected float3[] oldPositions;
        protected float3[] forces;
        protected float3[] normals;
        protected float[,] uvMap;

        protected typeConstraint[] constraints;
        protected int numPart;
        protected float timeStep;
        public typeTriangle[] triangles;


        public ParticlesSystem(Resolution res, float timeStep)
        {
            nWidth = res.Columns;
            nHeight = res.Rows;

            ini(timeStep);
        }

        public void MainLoop()
        {
            calculateNormals();
            acumulateForces();
            verlet();
            satisfyConstraints();
        }

        protected override void RenderContent(GraphicsInterface gi)
        {
            int p1, p2, p3;

            for (int i = 0; i < triangles.Length; i++)
            {
                p1 = triangles[i].p1;
                p2 = triangles[i].p2;
                p3 = triangles[i].p3;

                gi.Color(1, 1, 1, 0.95f);
                gi.Drawing.Triangles.Begin();
                gi.Normal(normals[p1]);
                gi.TexCoord(uvMap[p1, 0], uvMap[p1, 1]);
                gi.Vertex(positions[p1]);

                gi.Normal(normals[p2]);
                gi.TexCoord(uvMap[p2, 0], uvMap[p2, 1]);
                gi.Vertex(positions[p2]);

                gi.Normal(normals[p3]);
                gi.TexCoord(uvMap[p3, 0], uvMap[p3, 1]);
                gi.Vertex(positions[p3]);

                gi.Drawing.Triangles.End();
            }
        }

        #region Vector Routines
        protected void vectorCopy(float3[] dst, float3[] src)
        {
            for (int i = 0; i < numPart; i++)
                dst[i] = src[i];
        }


        protected void calculateNormals()
        {
            float3 v1 = new float3();
            float3 v2 = new float3();
            float3 v;
            int p1, p2, p3;
            int i;

            for (i = 0; i < numPart; i++)
                normals[0] = float3.Zero;

            for (i = 0; i < triangles.Length; i++)
            {
                p1 = triangles[i].p1;
                p2 = triangles[i].p2;
                p3 = triangles[i].p3;

                v1 = positions[p2] - positions[p1];
                v2 = positions[p3] - positions[p1];

                v = float3.Cross(v1, v2);

                normals[p1] += v;
                normals[p2] += v;
                normals[p3] += v;

            }

            // Normalize the normals into unit vectors
            for (i = 0; i < numPart; i++)
                normals[i] = normals[i].Normalize;

        }
        #endregion

        #region Particle system
        public virtual void ini(float timeStep)
        {

            numPart = nWidth * nHeight;

            //Console.WriteLine("");
            //Console.WriteLine("Number of particles = {0}", numPart);

            oldPositions = new float3[numPart];
            forces = new float3[numPart];
            normals = new float3[numPart];
            uvMap = new float[numPart, 2];
            constraints = new typeConstraint[nHeight * (nWidth - 1) + nWidth * (nHeight - 1) +
                (nWidth - 1) * (nHeight - 1) * 2];
            triangles = new typeTriangle[(nWidth - 1) * (nHeight - 1) * 2];

            this.timeStep = timeStep;

            makeQuads();
            makeConstraints();
            makeTriangles();
        }


        protected virtual void verlet()
        {
            // | positions = 2*positions - oldPositions + forces * timeStep * timeStep
            // | oldPositions = positions

            float3[] newPositions = new float3[numPart];
            float timeStep_2 = timeStep * timeStep;
            for (int i = 0; i < numPart; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    newPositions[i][j] = 1.96f * positions[i][j] - 0.96f * oldPositions[i][j] + forces[i][j] * timeStep_2;
                }
            }
            vectorCopy(oldPositions, positions); //oldPositions = positions
            vectorCopy(positions, newPositions); //positions = newPositions
        }



        protected virtual void acumulateForces()
        {
        }

        protected virtual void satisfyConstraints()
        {
            float3 d = new float3();
            float dLength;
            float diff;
            int p1, p2;


            for (int n = 0; n < 2; n++)
            {

                for (int i = 0; i < constraints.Length; i++)
                {

                    p1 = constraints[i].p1;
                    p2 = constraints[i].p2;

                    d = positions[p2] - positions[p1];

                    dLength = (float)Math.Sqrt(d[0] * d[0] + d[1] * d[1] + d[2] * d[2]);
                    diff = (dLength - constraints[i].dist) / dLength;

                    positions[p1][0] += 0.5f * diff * d[0];
                    positions[p1][1] += 0.5f * diff * d[1];
                    positions[p1][2] += 0.5f * diff * d[2];

                    positions[p2][0] -= 0.5f * diff * d[0];
                    positions[p2][1] -= 0.5f * diff * d[1];
                    positions[p2][2] -= 0.5f * diff * d[2];
                }
            }
        }

        protected virtual void makeQuads()
        {

        }

        protected virtual void makeConstraints()
        {

            int k = 0;

            float sizeDiag = size * (float)Math.Sqrt(2);

            for (int i = 0; i < nHeight; i++)
                for (int j = 0; j < nWidth; j++)
                {
                    int ind = i * nWidth + j;
                    //construccion de la matriz de restricciones (distancias relativas entre puntos)
                    //izquierda
                    if (j > 0)
                    {
                        constraints[k].p1 = ind;
                        constraints[k].p2 = ind - 1;
                        constraints[k++].dist = size;
                    }
                    //arriba
                    if (i < nHeight - 1)
                    {
                        constraints[k].p1 = ind;
                        constraints[k].p2 = (i + 1) * nWidth + j;
                        constraints[k++].dist = size;
                    }

                    //diagonal derecha
                    if (i < nHeight - 1 && j < nWidth - 1)
                    {
                        constraints[k].p1 = ind;
                        constraints[k].p2 = (i + 1) * nWidth + j + 1;
                        constraints[k++].dist = sizeDiag;
                    }

                    //diagonal izquierda
                    if (j > 0 && i < nHeight - 1)
                    {
                        constraints[k].p1 = ind;
                        constraints[k].p2 = (i + 1) * nWidth + j - 1;
                        constraints[k++].dist = sizeDiag;
                    }

                    /*//2 arriba
                    if (i< nHeight - 2){
                        constraints[k].p1 = ind;
                        constraints[k].p2 = (i+2)* nWidth + j;
                        constraints[k++].dist = 2*size;
                    }
				
                    //2 izquierda
                    if (j > 1) {
                        constraints[k].p1 = ind;
                        constraints[k].p2 = ind-2;
                        constraints[k++].dist = 2*size;
                    }*/
                }
        }



        protected virtual void makeTriangles()
        {
            // Construction of the matrix of triangles
            int m = 0;
            int ind;

            for (int i = 0; i < nHeight; i++)
                for (int j = 0; j < nWidth; j++)
                {
                    ind = i * nWidth + j;
                    if (i < nHeight - 1 && j < nWidth - 1)
                    {
                        triangles[m].p1 = ind;
                        triangles[m].p2 = ind + 1;
                        triangles[m++].p3 = (i + 1) * nWidth + j;
                    }

                    if (i > 0 && j > 0)
                    {
                        triangles[m].p1 = ind;
                        triangles[m].p2 = ind - 1;
                        triangles[m++].p3 = (i - 1) * nWidth + j;
                    }

                    //coordenadas uv
                    uvMap[ind, 0] = (float)(j * uTile) / (float)(nWidth - 1);
                    uvMap[ind, 1] = (float)(i * vTile) / (float)(nHeight - 1);
                }
            //Console.WriteLine("Number of triangles: {0}", triangles.Length);
        }

        #endregion

    }
}