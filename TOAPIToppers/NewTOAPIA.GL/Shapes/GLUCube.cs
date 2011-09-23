

namespace NewTOAPIA.GL
{
    public enum CubeStyle
    {
        WireFrame = BeginMode.LineLoop,
        Solid = BeginMode.Quads
    }

    public class GLUCube : GLRenderable
    {
        float fCubeSize;
        CubeStyle fStyle;

        // Normals
        static float [][]n = {
            new float[]{-1.0f, 0.0f, 0.0f},
            new float[]{0.0f, 1.0f, 0.0f},
            new float[]{1.0f, 0.0f, 0.0f},
            new float[]{0.0f, -1.0f, 0.0f},
            new float[]{0.0f, 0.0f, 1.0f},
            new float[]{0.0f, 0.0f, -1.0f}
        };

        // The 6 faces
        static int[][] faces = {
            new int[]{0, 1, 2, 3},
            new int[]{3, 2, 6, 7},
            new int[]{7, 6, 5, 4},
            new int[]{4, 5, 1, 0},
            new int[]{5, 6, 2, 1},
            new int[]{7, 4, 0, 3}
        };
        
        //private float[,] v = new float[8, 3];
        private float[][] v;


        public GLUCube(float cubesize, CubeStyle aStyle)
        {
            fStyle = aStyle;
            Size = cubesize;
        }

        public float Size
        {
            get { return fCubeSize; }
            set
            {
                SetSize(value);
            }
        }

        void SetSize(float size)
        {
            fCubeSize = size;

            //v[0,0] = v[1,0] = v[2,0] = v[3,0] = (float)(-size / 2);
            //v[4,0] = v[5,0] = v[6,0] = v[7,0] = (float)(size / 2);
            //v[0,1] = v[1,1] = v[4,1] = v[5,1] = (float)(-size / 2);
            //v[2,1] = v[3,1] = v[6,1] = v[7,1] = (float)(size / 2);
            //v[0,2] = v[3,2] = v[4,2] = v[7,2] = (float)(-size / 2);
            //v[1,2] = v[2,2] = v[5,2] = v[6,2] = (float)(size / 2);
            v = new float[][]{
                new float[]{(float)(-size / 2),(float)(-size / 2),(float)(-size / 2)},
                new float[]{(float)(-size / 2),(float)(-size / 2),(float)(size / 2)},
                new float[]{(float)(-size / 2),(float)(size / 2),(float)(size / 2)},
                new float[]{(float)(-size / 2),(float)(size / 2),(float)(-size / 2)},
                new float[]{(float)(size / 2),(float)(-size / 2),(float)(-size / 2)},
                new float[]{(float)(size / 2),(float)(-size / 2),(float)(size / 2)},
                new float[]{(float)(size / 2),(float)(size / 2),(float)(size / 2)},
                new float[]{(float)(size / 2),(float)(size / 2),(float)(-size / 2)}
            };
        }

        protected override void  RenderContent(GraphicsInterface gi)
        {
            int i;

            for (i = 5; i >= 0; i--) 
            {
                gi.Begin((BeginMode)fStyle);
                gi.Normal(n[i]);
                //gl.glNormal3fv(n[i]);
                gi.Vertex3(v[faces[i][0]]);
                gi.Vertex3(v[faces[i][1]]);
                gi.Vertex3(v[faces[i][2]]);
                gi.Vertex3(v[faces[i][3]]);
                gi.End();
            }
        }
    }
}
