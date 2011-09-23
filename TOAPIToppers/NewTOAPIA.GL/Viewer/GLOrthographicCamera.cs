
namespace NewTOAPIA.GL
{
    public class GLOrthographicCamera : GLProjectionCamera
    {

        public GLOrthographicCamera(GraphicsInterface gi)
            :base(gi)
        {
        }


        public override void OnRealize()
        {
            // left, right, bottom, top, near, far
            GL.Ortho(fFrustum.Left, fFrustum.Right, fFrustum.Bottom, fFrustum.Top, Near, Far);
        }
    }
}
