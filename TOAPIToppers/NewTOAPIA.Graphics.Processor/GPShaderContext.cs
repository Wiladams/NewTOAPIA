namespace NewTOAPIA.Graphics.Processor
{
    using NewTOAPIA.Graphics;

    public class GPShaderContext
    {
        const int MODELVIEW = 0;
        const int PROJECTION = 1;

        int CurrentMode = MODELVIEW;
        mat4 ModelviewMatrix = new mat4(1);
        mat4 ProjectionMatrix = new mat4(1);
        mat4 CurrentMatrix = new mat4(1);

        public vec4 VertexPosition { get; set; }

        public void MatrixMode(int mode)
        {
            if (mode != CurrentMode)
            {
                switch (mode)
                {
                    case MODELVIEW:
                        {
                            ProjectionMatrix = CurrentMatrix;
                            CurrentMatrix = ModelviewMatrix;
                            CurrentMode = MODELVIEW;
                            break;
                        }

                    case PROJECTION:
                        {
                            ModelviewMatrix = CurrentMatrix;
                            CurrentMatrix = ProjectionMatrix;
                            CurrentMode = PROJECTION;
                            break;
                        }

                    default:
                        break;
                }
            }
        }

        public void LoadIdentity()
        {
            CurrentMatrix = Matrix4Util.IdentityMatrix();
        }

        public void LoadMatrix(mat4 m)
        {
            CurrentMatrix = m;
        }

        public void MultMatrix(mat4 m)
        {
            CurrentMatrix = (mat4)(m * CurrentMatrix);
        }

        public void Scale(vec3 s)
        {
            CurrentMatrix = (mat4)(Matrix4Util.ScaleMatrix(ref s) * CurrentMatrix);
        }

        public void Translate(vec3 t)
        {
            CurrentMatrix = (mat4)(Matrix4Util.Translate(ref t) * CurrentMatrix);
        }

        public void Rotate(float angle, vec3 axis)
        {
            CurrentMatrix = (mat4)(Matrix4Util.RotateMatrix(angle, axis) * CurrentMatrix);
        }

        public void Ortho(float left, float right,
            float bottom, float top,
            float zNear, float zFar)
        {
            CurrentMatrix = (mat4)(Matrix4Util.OrthoMatrix(left, right, bottom, top, zNear, zFar) * CurrentMatrix);
        }

        public void Frustum(float left, float right,
            float bottom, float top,
            float zNear, float zFar)
        {
            CurrentMatrix = (mat4)(Matrix4Util.FrustumMatrix(left, right, bottom, top, zNear, zFar) *
                CurrentMatrix);
        }
    }
}