using System;
using TOAPI.OpenGL;
using NewTOAPIA.GL;

	public class ParticlesSystem : GLRenderable
	{
        public int nWidth;
        public int nHeight;
        public float size;
        public int uTile = 1;
        public int vTile = 1;

        public float[,] positions;
        protected float[,] oldPositions;
        protected float[,] forces;
        protected float[,] normals;
        protected float[,] uvMap;

        protected typeConstraint[] constraints;
        protected int numPart;
        protected float timeStep;
        public typeTriangle[] triangles;



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
                gi.Begin(BeginMode.Triangles);
                gi.Normal(normals[p1, 0], normals[p1, 1], normals[p1, 2]);
                gi.TexCoord(uvMap[p1, 0], uvMap[p1, 1]);
                gi.Vertex(positions[p1, 0], positions[p1, 1], positions[p1, 2]);

                gi.Normal(normals[p2, 0], normals[p2, 1], normals[p2, 2]);
                gi.TexCoord(uvMap[p2, 0], uvMap[p2, 1]);
                gi.Vertex(positions[p2, 0], positions[p2, 1], positions[p2, 2]);

                gi.Normal(normals[p3, 0], normals[p3, 1], normals[p3, 2]);
                gi.TexCoord(uvMap[p3, 0], uvMap[p3, 1]);
                gi.Vertex(positions[p3, 0], positions[p3, 1], positions[p3, 2]);
                gi.End();
            }
        }

        #region Vector Routines
        protected void vectorCopy(float[,] v1, float[,] v2)
        {
            for (int i = 0; i < numPart; i++)
                for (int j = 0; j < 3; j++)
                    v1[i, j] = v2[i, j];
        }

        protected float[] prodVect(float[] v1, float[] v2)
        {
            return new float[3] {v1[1]*v2[2]-v1[2]*v2[1],
									v1[2]*v2[0]-v1[0]*v2[2],
									v1[0]*v2[1]-v1[1]*v2[0]};
        }

        protected void normalize(ref float[,] v)
        {
            float norm = 0;
            for (int i = 0; i < numPart; i++)
            {
                norm = 0;
                norm = (float)Math.Sqrt(v[i, 0] * v[i, 0] + v[i, 1] * v[i, 1] + v[i, 2] * v[i, 2]);
                v[i, 0] /= norm; v[i, 1] /= norm; v[i, 2] /= norm;
            }
        }

        protected void calculateNormals()
        {
            float[] v1 = new float[3];
            float[] v2 = new float[3];
            float[] v;
            int p1, p2, p3;
            int i, j;

            for (i = 0; i < numPart; i++)
                for (j = 0; j < 3; j++)
                    normals[i, j] = 0;

            for (i = 0; i < triangles.Length; i++)
            {
                p1 = triangles[i].p1;
                p2 = triangles[i].p2;
                p3 = triangles[i].p3;

                for (j = 0; j < 3; j++)
                {
                    v1[j] = positions[p2, j] - positions[p1, j];
                    v2[j] = positions[p3, j] - positions[p1, j];
                }

                v = prodVect(v1, v2);

                for (j = 0; j < 3; j++)
                {
                    normals[p1, j] += v[j];
                    normals[p2, j] += v[j];
                    normals[p3, j] += v[j];
                }
            }
            normalize(ref normals);
        }
        #endregion

        #region Particle system
        public virtual void ini(float timeStep)
        {

            numPart = nWidth * nHeight;

            //Console.WriteLine("");
            //Console.WriteLine("Number of particles = {0}", numPart);

            oldPositions = new float[numPart, 3];
            forces = new float[numPart, 3];
            normals = new float[numPart, 3];
            uvMap = new float[numPart, 2];
            //constraints		= new typeConstraint[nHeight*(nWidth-1)+nWidth*(nHeight-1)+
            //									(nWidth-1)*(nHeight-1)*2 + 
            //									 nHeight*(nWidth-2)+nWidth*(nHeight-2)];
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

            float[,] newPositions = new float[numPart, 3];
            float timeStep_2 = timeStep * timeStep;
            for (int i = 0; i < numPart; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    newPositions[i, j] = 1.96f * positions[i, j] - 0.96f * oldPositions[i, j] + forces[i, j] * timeStep_2;
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
            float[] d = new float[3];
            float dLength;
            float diff;
            int p1, p2;

            //mejora: definir una tolerancia al error y verificarlo en cada paso.

            for (int n = 0; n < 2; n++)
            {
                //verificar distancia entre particulas

                for (int i = 0; i < constraints.Length; i++)
                {
                    // mejora: aproximacion por polinomio de Taylor de 1 orden

                    p1 = constraints[i].p1;
                    p2 = constraints[i].p2;

                    d[0] = positions[p2, 0] - positions[p1, 0];
                    d[1] = positions[p2, 1] - positions[p1, 1];
                    d[2] = positions[p2, 2] - positions[p1, 2];

                    dLength = (float)Math.Sqrt(d[0] * d[0] + d[1] * d[1] + d[2] * d[2]);
                    diff = (dLength - constraints[i].dist) / dLength;

                    positions[p1, 0] += 0.5f * diff * d[0];
                    positions[p1, 1] += 0.5f * diff * d[1];
                    positions[p1, 2] += 0.5f * diff * d[2];

                    positions[p2, 0] -= 0.5f * diff * d[0];
                    positions[p2, 1] -= 0.5f * diff * d[1];
                    positions[p2, 2] -= 0.5f * diff * d[2];
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
            Console.WriteLine("Number of triangles: {0}", triangles.Length);
        }

        #endregion

    }
