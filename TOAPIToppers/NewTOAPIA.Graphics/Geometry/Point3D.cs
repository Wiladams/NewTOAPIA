
namespace NewTOAPIA.Graphics
{
    using System;

    public struct Point3D
    {
        #region Fields
        public double x, y, z, w;   // tuple.w == 1 always

        #endregion

        #region Constructors
        public Point3D(double x_, double y_)
        {
            x = x_;
            y = y_;
            z = 0;
            w = 1;
        }

        public Point3D(double x_, double y_, double z_)
        {
            x = x_;
            y = y_;
            z = z_;
            w = 1;
        }

        public Point3D(double3 values)
        {
            x = values.x;
            y = values.y;
            z = values.z;
            w = 1;
        }
        #endregion

        #region Methods
        public double DistanceSquared(Point3D rhs)
        {
            return ((x - rhs.x) * (x - rhs.x) +
                (y - rhs.y) * (y - rhs.y) +
                (z - rhs.z) * (z - rhs.z));
        }

        public double Distance(Point3D rhs)
        {
            return Math.Sqrt(DistanceSquared(rhs));
        }
        #endregion

        #region Operator Overloading
        public static Point3D operator+ (Point3D pt, Vector3D vec)
        {
            Point3D newPoint = new Point3D(pt.x+vec.x, pt.y + vec.y, pt.z+vec.z);

            return newPoint;
        }

        public static Point3D operator -(Point3D pt, Vector3D vec)
        {
            Point3D newPoint = new Point3D(pt.x - vec.x, pt.y - vec.y, pt.z - vec.z);
            
            return newPoint;
        }

        public static Vector3D operator -(Point3D pt, Point3D pt2)
        {
            Vector3D newVector = new Vector3D(pt.x - pt2.x, pt.y - pt2.y, pt.z - pt2.z);
            
            return newVector;
        }

        public static Point3D operator * (Point3D a, double c)
        {
            return new Point3D(a.x * c, a.y * c, a.z * c);
        }

        public static Point3D operator *(double c, Point3D a)
        {
            return new Point3D(a.x * c, a.y * c, a.z * c);
        }
        #endregion

        #region Type Casting
        public static implicit operator float3(Point3D pt)
        {
            return new float3(pt.x, pt.y, pt.z);
        }

        public static explicit operator float[](Point3D pt)
        {
            float[] values = { (float)pt.x, (float)pt.y, (float)pt.z, 1.0f };
            return values;
        }

        public static implicit operator Point3D(double3 vec)
        {
            return new Point3D(vec);
        }

        public static implicit operator double4(Point3D pt)
        {
            return new double4(pt.x, pt.y, pt.z, pt.w);
        }
        #endregion

    }
}
