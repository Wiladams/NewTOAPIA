
namespace NewTOAPIA.GL
{
    public struct GLFrustum
    {
        public float Left;
        public float Top;
        public float Right;
        public float Bottom;
        public float Near;
        public float Far;

        public GLFrustum(float left, float top, float right, float bottom, float near, float far)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
            Near = near;
            Far = far;
        }
    }
}
