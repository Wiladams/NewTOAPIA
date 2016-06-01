

namespace NewTOAPIA
{
    using System;

    public interface IVector
    {
        int Dimension { get; }
        double Length { get; }
        double this[int index] { get; set; }
    }

    public class Vector : IVector
    {
        protected double[] el;
        protected int n;


        #region Constructors
        public Vector()
        {
        }
        
        public Vector(GPoint p)
        {
            el = new double[p.Coordinate.Length];
            for (int k = 0; k < p.Coordinate.Length; k++)
                el[k] = p.Coordinate[k];
            n = p.Coordinate.Length;
        }

        public Vector(GPoint p, GPoint q)
        {
            el = new double[p.Coordinate.Length];
            for (int k = 0; k < p.Coordinate.Length; k++)
                el[k] = q.Coordinate[k] - p.Coordinate[k];
            n = p.Coordinate.Length;
        }

        public Vector(double[] x)
        {
            el = new double[x.Length];
            for (int k = 0; k < x.Length; k++) el[k] = x[k];
            n = x.Length;
        }

        public Vector(int dimension)
        {
            el = new double[dimension];
            n = dimension;
        }
        #endregion

        public double this[int where]
        {
            get { return el[where]; }
            set { el[where] = value; }
        }

        public int Dimension
        {
            get { return n; }
        }

        public double Norm
        {
            get
            {
                double sq = 0.0;
                foreach (double k in el) 
                    sq += k * k;
                
                return Math.Sqrt(sq);
            }
        }

        public double Length
        {
            get { return this.Norm; }
        }

        #region Operator overloads
        public static Vector operator *(double a, Vector c)
        {
            Vector b = new Vector(c.Dimension);
            for (int k = 0; k < c.Dimension; k++) b[k] = a * c[k];

            return b;
        }

        public static Vector operator *(Vector c, double a)
        {
            return a * c;
        }
        
        public static Vector operator +(Vector a, Vector c)
        {
            Vector b = new Vector(a.Dimension);
        
            for (int k = 0; k < c.Dimension; k++)
                b[k] = a[k] + c[k];
            
            return b;
        }
        
        public static Vector operator -(Vector a, Vector c)
        {
            Vector b = new Vector(a.Dimension);
            for (int k = 0; k < c.Dimension; k++)
                b[k] = a[k] - c[k];
            return b;
        }


        // Cross product
        public static Vector operator %(Vector a, Vector b)  // CrossProduct
        {
            Vector c = new Vector(3);
            c[0] = a[1] * b[2] - a[2] * b[1];
            c[1] = a[2] * b[0] - a[0] * b[2];
            c[2] = a[0] * b[1] - a[1] * b[0];
            return c;
        }
        #endregion

        // Dot product
        public static double operator *(Vector a, Vector c)
        {
            double sum = 0.0;
            for (int k = 0; k < a.Dimension; k++)
                sum += a[k] * c[k];

            return sum;
        }

        public Vector OrthogonalProjection(Vector on)
        {
            return this * on / on.Norm / on.Norm * on;
        }


        public override string ToString()
        {
            string res = "";
            for (int i = 0; i < this.Dimension; i++)
            {
                res += this[i].ToString() + "\r\n";
            }
            return res;
        }
    }
}

