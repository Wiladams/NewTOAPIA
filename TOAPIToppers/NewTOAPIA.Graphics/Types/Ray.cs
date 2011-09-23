namespace NewTOAPIA.Graphics
{
    public class Ray
    {
        public Point3D     origin;
        public Vector3D    direction;

        public Ray()
        {
        }

        public Ray(Point3D o, Vector3D d)
        {
            origin = o;
            direction = d;
        }
    }
}