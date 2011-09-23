using System;

namespace NewTOAPIA
{
    public class MatrixSquare : Matrix
    {
        private double d;

        #region Constructors
        public MatrixSquare(int n)
            : base(n, n)
        {
        }

        public MatrixSquare(double[,] x)
            : base(x)
        {
            m = x.GetLength(0);
            n = m;
        }
        #endregion Constructors


        public static IMatrix ComponentMultiply(MatrixSquare x, MatrixSquare y)
        {
            Matrix r = new Matrix(x.ColumnDimension, y.RowDimension);

            for (int row = 0; row < x.RowDimension; row++)
                for (int column = 0; column < y.ColumnDimension; column++)
                {
                    r[column, row] = x[column, row] * y[column, row];
                }

            return r;
        }

        public void SetDiagonal(double dValue)
        {
            for (int i = 0; i < ColumnDimension; i++)
                el[i, i] = dValue;
        }

        public void MakeIdentity()
        {
            Clear();
            SetDiagonal(1);
        }

        private double GetDeterminant()
        {
            LUdcmp obj = new LUdcmp(el);
            d = obj.det();

            return d;
        }


        public double[,] GetInverse()
        {
            LUdcmp obj = new LUdcmp(el);
            double[,] x = obj.inverse();

            return x;
        }
    }

    class LUdcmp
    {
        int n;
        private double[,] lu;
        private int[] indx;
        private double d;
        private double[,] aref;

        public double[,] LU
        {
            get { return lu; }  // output
        }

        public int[] RowPermutation // output
        {
            get { return indx; }
        
        }
        public double Sign // output
        {
            get { return d; }
        }

        public LUdcmp(double[,] a)   // a input
        {
            n = a.GetLength(0);
            lu = new double[n, n];
            for (int ir = 0; ir < n; ir++)
                for (int ic = 0; ic < n; ic++) lu[ir, ic] = a[ir, ic];
            aref = a;
            indx = new int[n];
            double TINY = 1.0e-40;
            int i, imax = 0, j, k;
            double big, temp;
            double[] vv = new Double[n];
            d = 1.0;
            
            for (i = 0; i < n; i++)
            {
                big = 0.0;
                for (j = 0; j < n; j++)
                    if ((temp = Math.Abs(lu[i, j])) > big) 
                        big = temp;
                
                if (big == 0.0)
                    throw new Exception("Singular matrix in LUdcmp");  // "Singular matrix in LUdcmp"

                vv[i] = 1.0 / big;
            }

            for (k = 0; k < n; k++)
            {
                big = 0.0;
                for (i = k; i < n; i++)
                {
                    temp = vv[i] * Math.Abs(lu[i, k]);
                    if (temp > big)
                    {
                        big = temp;
                        imax = i;
                    }
                }
            
                if (k != imax)
                {
                    for (j = 0; j < n; j++)
                    {
                        temp = lu[imax, j];
                        lu[imax, j] = lu[k, j];
                        lu[k, j] = temp;
                    }
                    d = -d;
                    vv[imax] = vv[k];
                }
                
                indx[k] = imax;
                
                if (lu[k, k] == 0.0) 
                    lu[k, k] = TINY;
                
                for (i = k + 1; i < n; i++)
                {
                    temp = lu[i, k] /= lu[k, k];
                    for (j = k + 1; j < n; j++) lu[i, j] -= temp * lu[k, j];
                }
            }
        }

        public void solve(double[] b, double[] x) // b input and x output
        {
            int i, ii = 0, ip, j;
            double sum;
            
            if (b.Length != n || x.Length != n)
                throw new Exception("Bad sizes, must be square");

            for (i = 0; i < n; i++) 
                x[i] = b[i];
            
            for (i = 0; i < n; i++)
            {
                ip = indx[i];
                sum = b[ip];
                x[ip] = x[i];
                if (ii != 0) for (j = ii - 1; j < i; j++) sum -= lu[i, j] * x[j];
                else if (sum != 0.0) ii = i + 1;
                x[i] = sum;
            }
            
            for (i = n - 1; i >= 0; i--)
            {
                sum = x[i];
                
                for (j = i + 1; j < n; j++) 
                    sum -= lu[i, j] * x[j];
                
                x[i] = sum / lu[i, i];
            }
        }

        public void solve(double[,] b, double[,] x) // b input and x output
        {
            int i, j, m = b.GetLength(1);
            if (b.GetLength(0) != n || x.GetLength(0) != n || b.GetLength(1) != x.GetLength(1)) 
                throw new Exception("Bad sizes");

            double[] xx = new double[n];
            
            for (j = 0; j < m; j++)
            {
                for (i = 0; i < n; i++) 
                    xx[i] = b[i, j];
                
                solve(xx, xx);
                
                for (i = 0; i < n; i++) 
                    x[i, j] = xx[i];
            }
        }

        public double[,] inverse()
        {
            double[,] ainv = new double[n, n];
            
            for (int i = 0; i < n; i++) 
                ainv[i, i] = 1.0;
            
            solve(ainv, ainv);
            
            return ainv;
        }

        public double det()
        {
            double dd = d;
            
            for (int i = 0; i < n; i++) 
                dd *= lu[i, i];

            return dd;
        }

        public void mprove(double[] b, double[] x)
        {
            int j, i;
            double sdp;
            double[] r = new Double[n];

            for (i = 0; i < n; i++)
            {
                sdp = -b[i];
                for (j = 0; j < n; j++) sdp += aref[i, j] * x[j];
                r[i] = sdp;
            }
            
            solve(r, r);
            
            for (i = 0; i < n; i++) 
                x[i] -= r[i];
        }
    }
}

