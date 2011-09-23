namespace NewTOAPIA.Graphics
{
    public struct Normal3D
    {
        #region Fields
        public double x;
        public double y;
        public double z;
        #endregion

        #region Constructors
        public Normal3D(double arg)
        {
            x = arg;
            y = arg;
            z = arg;
        }

        public Normal3D(double x_, double y_, double z_)
        {
            x = x_;
            y = y_;
            z = z_;
        }
        #endregion

        #region Operator Overloads
        public static Normal3D operator +(Normal3D n, Normal3D m)
        {
            Normal3D newVector = new Normal3D(n.x + m.x, n.y + m.y, n.z + m.z);
            return newVector;
        }

        public static double operator *(Normal3D n, Vector3D u)
        {
            return n.x * u.x + n.y * u.y + n.z * u.z;
        }

        public static double operator *(Vector3D u, Normal3D n)
        {
            return n.x * u.x + n.y * u.y + n.z * u.z;
        }

        public static Normal3D operator *(double a, Normal3D n)
        {
            return new Normal3D(n.x * a, n.y * a, n.z * a);
        }

        public static Normal3D operator *(Normal3D n, double a)
        {
            return new Normal3D(n.x * a, n.y * a, n.z * a);
        }
        #endregion
    }
}