using System;
using System.Collections.Generic;
using System.Text;

using NewTOAPIA.GL;

namespace MS3D
{
    public class MS3DMesh3D : Mesh3D
    {
        public MS3DMesh3D(MS3DModel aModel)
            : base(NewTOAPIA.GL.BeginMode.Triangles)
        {
            // Copy the vertices
            MS3DVertex[] modelVertices = aModel.Vertices;
            List<Vector3f> meshVertices = new List<Vector3f>();
            foreach (MS3DVertex vert in modelVertices)
            {
                Vector3f vec = new Vector3f(vert.vertex.x, vert.vertex.y, vert.vertex.z);
                meshVertices.Add(vec);
            }
            this.Vertices = meshVertices.ToArray();

            // Copy the indices
            List<MS3DTriangle> modelTriangles = aModel.Triangles;
            List<int> meshIndices = new List<int>();
            List<Vector3f> meshNormals = new List<Vector3f>();
            foreach (MS3DTriangle tri in modelTriangles)
            {
                meshIndices.Add((int)tri.vertexIndices[0]);
                meshIndices.Add((int)tri.vertexIndices[1]);
                meshIndices.Add((int)tri.vertexIndices[2]);

                meshNormals.Add(new Vector3f(tri.vertexNormals[0]));
                meshNormals.Add(new Vector3f(tri.vertexNormals[1]));
                meshNormals.Add(new Vector3f(tri.vertexNormals[2]));

            }
            this.Indices = meshIndices.ToArray();
            this.Normals = meshNormals.ToArray();

            this.fUseNormals = true;

        }
    }
}
