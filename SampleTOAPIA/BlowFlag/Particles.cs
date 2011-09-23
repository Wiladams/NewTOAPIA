using System;
using TOAPI.OpenGL;


	class particles : ParticlesSystem 
	{
        public float x0 = 0.0f;
        public float y0=-3.0f;
        public float z0 = 0.0f;
	
		private float[] fixed0 = new float[3];
		private float[] fixed1 = new float[3];
		private int indLastRow;

		private readonly float[]  windForce = new float[3] {40.0f, 0, 0.1f};
		private readonly float[]  gravForce = new float[3] {0, -9.81f, 0};

		public bool bWind = true;
        public double WindStrength = 0.0;
		
		public override void ini(float timeStep)
		{
			base.ini(timeStep);
		}

	
		protected override void acumulateForces()
		{
			float fn;

			for (int i=0; i<numPart; i++) 
			{
				if (bWind)
					fn = windForce[0] * normals[i,0] + 
						 windForce[1] * normals[i,1] + 
						 windForce[2] * normals[i,2];
				else
					fn= 0;

				forces[i,0] = fn * normals[i,0] + gravForce[0];
				forces[i,1] = fn * normals[i,1] + gravForce[1];
				forces[i,2] = fn * normals[i,2] + gravForce[2];
			}
		}
	
		protected override void satisfyConstraints()
		{
			float[] d = new float[3];
			float dLength;
			float diff;
			int p1, p2;
		

			for (int n=0; n<6; n++)
			{			
				//verificar distancia entre particulas
			
				for (int i=0; i<constraints.Length; i++)  //internal constraints
				{
					// mejora: aproximacion por polinomio de Taylor de 1 orden
				
					p1 = constraints[i].p1;
					p2 = constraints[i].p2; 
				
					d[0] = positions[p2,0] - positions[p1,0];
					d[1] = positions[p2,1] - positions[p1,1];
					d[2] = positions[p2,2] - positions[p1,2];
				
					dLength = (float)Math.Sqrt(d[0]*d[0] + d[1]*d[1] + d[2]*d[2]);
					diff = (dLength - constraints[i].dist)/dLength;

					positions[p1,0] += 0.5f*diff*d[0];
					positions[p1,1] += 0.5f*diff*d[1];
					positions[p1,2] += 0.5f*diff*d[2];
				
					positions[p2,0] -= 0.5f*diff*d[0];
					positions[p2,1] -= 0.5f*diff*d[1];
					positions[p2,2] -= 0.5f*diff*d[2];
				}

				positions[0,0] = fixed0[0];
				positions[0,1] = fixed0[1];
				positions[0,2] = fixed0[2];

				positions[indLastRow,0] = fixed1[0];
				positions[indLastRow,1] = fixed1[1];
				positions[indLastRow,2] = fixed1[2];
			}
		}
	
		protected override void makeQuads() 
		{
			//construye una cuadricula de nWidth x nHeight cuadros de tamaño size.
			//la esquina inferior izquierda sera (x0, y0, z0)
			//la quadricula sera paralela al plano XY con Z=Z0
		
			int ind;
		
			positions 		= new float[numPart,3];
			oldPositions 	= new float[numPart,3];
		
			for (int i=0; i<nHeight; i++)
				for (int j=0; j<nWidth; j++)
				{
					ind = i* nWidth + j;
					positions[ind,0] = x0 + j*size;
					positions[ind,1] = y0 + i*size;
					positions[ind,2] = z0;
				}

			fixed0[0] = positions[0,0];
			fixed0[1] = positions[0,1];
			fixed0[2] = positions[0,2];
	
			indLastRow = (nHeight-1)*nWidth;

			fixed1[0] = positions[indLastRow,0];
			fixed1[1] = positions[indLastRow,1];
			fixed1[2] = positions[indLastRow,2];

			vectorCopy(oldPositions, positions);
		}
	
	}
