
namespace NewTOAPIA.GL
{
    using NewTOAPIA.Drawing;
    using NewTOAPIA.Graphics;

    /// <summary>
    /// This is the base class for all cameras.  
    /// What they all have in common is: 
    ///     Location, LookAt, Up
    /// Beyond these basics, they apply their own transforms on a scene.
    /// </summary>
    public class GLProjectionCamera : Camera
    {
        protected GLFrustum fFrustum;

        //private Vector3D fRightVector;

        //private float3 fRotation;

        float fNearPlaneDistance;
        float fFarPlaneDistance;

        /// <summary>
        /// By default, the camera is located at the origin, looking
        /// down the -z axis, with the y-axis as the up vector.
        /// </summary>
        public GLProjectionCamera(GraphicsInterface gi)
            :this(gi, new Point3D(0,0,0), new Point3D(0,0,-1),new Vector3D(0,1,0), 0.1f, 1000.0f)
        {

        }

        public GLProjectionCamera(GraphicsInterface gi, Point3D location, Point3D lookAt, Vector3D up, float nearPlane, float farPlane)
        :base(gi, location, lookAt, up)
        {
            fFrustum = new GLFrustum(-1, 1, 1, -1, -1, 1);

            fNearPlaneDistance = nearPlane;
            fFarPlaneDistance = farPlane;
        }


        public GLFrustum Frustum
        {
            get { return fFrustum; }
            set
            {
                SetFrustum(value);
            }
        }

        public virtual void SetFrustum(GLFrustum frustum)
        {
            fFrustum = frustum;
            fNearPlaneDistance = frustum.Near;
            fFarPlaneDistance = frustum.Far;

            Changed();
        }





        public virtual float Far
        {
            get { return fFarPlaneDistance;}
            set
            {
                SetFar(value);
            }
        }

        public virtual void SetFar(float farPlane)
        {
            fFarPlaneDistance = farPlane;
            Changed();
        }

        public virtual float Near
        {
            get {return fNearPlaneDistance;}
            set {
                SetNear(value);
            }
        }

        public virtual void SetNear(float nearPlane)
        {
            fNearPlaneDistance = nearPlane;
            Changed();
        }


        public override void Realize()
        {
            GI.MatrixMode(MatrixMode.Projection);
            GI.LoadIdentity();

            OnRealize();

            GI.MatrixMode(MatrixMode.Modelview);
            GI.LoadIdentity();
            
            GI.Glu.LookAt(Position.x, Position.y, Position.z,
                LookAt.x, LookAt.y, LookAt.z,
                Up.x, Up.y, Up.z);

        }

        public virtual void OnRealize()
        {
        }
    }
}
