using System;

namespace NewTOAPIA.GL
{
    public class GLPerspectiveCamera : GLProjectionCamera
    {
        const double M_PId = 3.1415926535897932384626433832795029;

        float fFieldOfView;
        float fAspectRatio;

        public GLPerspectiveCamera(GraphicsInterface gi)
            :base(gi)
        {
            fAspectRatio = 4 / 3;
            fFieldOfView = 45.0f;
        }

        public float AspectRatio
        {
            get { return fAspectRatio; }
            set
            {
                SetAspectRatio(value);
            }
        }

        public void SetAspectRatio(float ratio)
        {
            fAspectRatio = ratio;
            Changed();
        }

        public float FieldOfView
        {
            get { return fFieldOfView; }
            set {
                SetFieldOfView(value);
            }
        }

        public void SetFieldOfView(float fov)
        {
            fFieldOfView = fov;
            Changed();
        }

        public override void OnRealize()
        {
            Perspective(FieldOfView, AspectRatio, Near, Far);
        }

        /// <summary>
        /// This routine replaces the Glu.Perspective call.  It just calculates the viewing
        /// frustum directly, and then calls the glFrustum function.
        /// 
        /// Why bother?  So we don't need to rely on the glu parts of the library.
        /// </summary>
        /// <param name="fovY">Field Of View in degrees</param>
        /// <param name="aspect">Aspect Ratio</param>
        /// <param name="zNear">Near plane</param>
        /// <param name="zFar">Far plane</param>
        void Perspective(double fovY, double aspect, double zNear, double zFar)
        {
            double fW, fH;

            fH = Math.Tan((fovY / 2) / 180 * M_PId) * zNear;
            fW = fH * aspect;

            GI.Frustum(-fW, fW, -fH, fH, zNear, zFar);
        }

    }
}
