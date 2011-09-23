using System;

namespace NewTOAPIA.Graphics
{
    public struct Vector3D
    {
        public static Vector3D Zero = new Vector3D();

        #region Fields
        public double x, y, z, w;

        #endregion

        #region Constructors
        public Vector3D(double value)
        {
            x = value;
            y = value;
            z = value;
            w = 0;
        }

        public Vector3D(double3 values)
        {
            x = values.x;
            y = values.y;
            z = values.z;
            w = 0;
        }

        public Vector3D(double x_, double y_)
        {
            x = x_;
            y = y_;
            z = 0;
            w = 0;
        }

        public Vector3D(double x_, double y_, double z_)
        {
            x = x_;
            y = y_;
            z = z_;
            w = 0;
        }

        public Vector3D(Vector3D rhs)
        {
            x = rhs.x;
            y = rhs.y;
            z = rhs.z;
            w = 0;
        }

        public Vector3D(Point3D rhs)
        {
            x = rhs.x;
            y = rhs.y;
            z = rhs.z;
            w = 0;
        }

        public Vector3D(Normal3D rhs)
        {
            x = rhs.x;
            y = rhs.y;
            z = rhs.z;
            w = 0;
        }
        #endregion

        #region Methods
        public Vector3D Cross(Vector3D v)
        {
            return new Vector3D(y * v.z - z * v.y, z * v.x - x * v.z, x * v.y - y * v.x);
        }

        public double Dot(Vector3D rhs)
        {
            return x * rhs.x + y * rhs.y + z * rhs.z;
        }

        public double LengthSquared
        {
            get { return (x * x + y * y + z * z); }
        }

        public double Length
        {
            get { return Math.Sqrt(LengthSquared); }
        }
        #endregion

        #region Operator Overloading
        // Cross Product
        public static Vector3D operator ^(Vector3D a0, Vector3D a1)
        {
            return new Vector3D(a0.y * a1.z - a0.z * a1.y, a0.z * a1.x - a0.x * a1.z, a0.x * a1.y - a0.y * a1.x);
        }

        public static double operator *(Vector3D u, Vector3D v)
        {
            return u.x * v.x + u.y * v.y + u.z * v.z;
        }

        public static Vector3D operator +(Vector3D U, Vector3D V)
        {
            Vector3D newVector = new Vector3D(U.x + V.x, U.y + V.y, U.z + V.z);
            return newVector;
        }

        public static Vector3D operator +(Vector3D U, Normal3D n)
        {
            Vector3D newVector = new Vector3D(U.x + n.x, U.y + n.y, U.z + n.z);
            return newVector;
        }

        public static Vector3D operator +(Normal3D n, Vector3D U)
        {
            Vector3D newVector = new Vector3D(U.x + n.x, U.y + n.y, U.z + n.z);
            return newVector;
        }

        public static Vector3D operator -(Vector3D U, Vector3D V)
        {
            Vector3D newVector = new Vector3D(U.x - V.x, U.y - V.y, U.z - V.z);
            return newVector;
        }

        public static Vector3D operator *(Vector3D vec, double c)
        {
            Vector3D newVector = new Vector3D(vec.x*c, vec.y * c, vec.z*c);
            return newVector;
        }

        public static Vector3D operator *(double c, Vector3D vec)
        {
            Vector3D newVector = new Vector3D(vec.x * c, vec.y * c, vec.z * c);
            return newVector;
        }

        public static Vector3D operator /(Vector3D vec, double c)
        {
            Vector3D newVector = new Vector3D(vec.x / c, vec.y / c, vec.z / c);
            return newVector;
        }

        #region Type Casting
        public static implicit operator double3(Vector3D vec)
        {
            return new double3(vec.x, vec.y, vec.z);
        }

        public static implicit operator float3(Vector3D vec)
        {
            return new float3((float)vec.x, (float)vec.y, (float)vec.z);
        }

        public static implicit operator Vector3D(double3 vec)
        {
            return new Vector3D(vec);
        }

        public static explicit operator double4(Vector3D vec)
        {
            return new double4(vec.x, vec.y, vec.z, 0);
        }
        #endregion
        #endregion
    }
}
