using System;


namespace RingCamView
{
    using System.Collections.Generic;
    using System.Drawing;

    using NewTOAPIA.GL;
    
    public class CylinderSection : Mesh3D
    {
        #region Internal Fields
        float fRadius;
        float fHeight;
        float fArcDegrees;

        int fStacks;
        int fSlices;

        RectangleF fTextureShard;

        #endregion

        #region Constructor
        public CylinderSection(GraphicsInterface gi, float radius, float height, float arcDeg, int stacks, int slices)
            : this(gi, radius, height, arcDeg, stacks, slices, new RectangleF(0.0f, 0.0f, 1.0f, 1.0f))
        {
        }

        public CylinderSection(GraphicsInterface gi, float radius, float height, float arcDeg, int stacks, int slices, RectangleF textureShard)
            : base(gi, BeginMode.Triangles)
        {
            fRadius = radius;
            fHeight = height;
            fArcDegrees = arcDeg;
            fStacks = stacks;
            fSlices = slices;
            fTextureShard = textureShard;

            Triangulate();

            fUseNormals = true;
            fUseTexture = true;
        }
        #endregion

        #region Properties
        public float Length
        {
            get { return fHeight; }
        }

        public float Radius
        {
            get { return fRadius; }
        }

        public int Stacks
        {
            get { return fStacks; }
        }

        public int Slices
        {
            get { return fSlices; }
        }
        #endregion

        protected void Triangulate()
        {
            float stackHeight = Length / Stacks;
            float radianIncr = MathUtils.DegreesToRadians(fArcDegrees / Slices);

            // Construct the collections we'll use
            List<Vector3f> vertices = new List<Vector3f>();
            List<Vector3f> normals = new List<Vector3f>();
            List<int> indices = new List<int>();
            List<TextureCoordinates> textures = new List<TextureCoordinates>();


            // Length of the cylinder: Fill in the collections.
            for (int stack = 0; stack <= Stacks; stack++)
            {
                double y = Length - stack * stackHeight;
                int top =  (stack + 0) * (Slices + 1);
                int bot =  (stack + 1) * (Slices + 1);
                double theta = MathUtils.DegreesToRadians(180 - ((float)fArcDegrees / 2));
                
                for (int slice = 0; slice <= Slices; slice++)
                {
                    double x = Radius * Math.Sin(theta);
                    double z = Radius * Math.Cos(theta) + Radius;
                    vertices.Add(new Vector3f((float)x, (float)y, (float)z));

                    float txCoord = fTextureShard.Right - fTextureShard.Width * (float)slice/Slices;
                    float tyCoord = 1.0f - (fTextureShard.Height * (float)stack / Stacks);
                    textures.Add(new TextureCoordinates(txCoord, tyCoord));
                    normals.Add(new Vector3f((float)x, (float)y, (float)-z));

                    if (stack < Stacks && slice < Slices)
                    {
                        indices.Add(top + slice);
                        indices.Add(bot + slice);
                        indices.Add(top + slice + 1);

                        indices.Add(top + slice + 1);
                        indices.Add(bot + slice);
                        indices.Add(bot + slice + 1);
                    }

                    theta += radianIncr;
                }
            }


            this.SetVertices(vertices.ToArray());
            this.Normals = normals.ToArray();
            this.SetIndices(indices.ToArray());
            this.SetTextureCoordinates(textures.ToArray());
        }
    }
}

