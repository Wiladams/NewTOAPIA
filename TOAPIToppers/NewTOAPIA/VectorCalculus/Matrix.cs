

namespace NewTOAPIA
{
    public interface IMatrix
    {
        int RowDimension { get; }
        int ColumnDimension { get; }

        double GetElement(int column, int row);
        IVector GetColumn(int column);

        double this[int column, int row] { get; set; }

        IMatrix Transpose { get; }

    }

    public class Matrix : IMatrix
    {
        protected double[,] el;
        protected int m, n;

        public int ColumnDimension
        {
            get { return m; }
        }

        public int RowDimension
        {
            get { return n; }
        }

        #region Constructors
        public Matrix(IMatrix x)
        {
            m = x.ColumnDimension;
            n = x.RowDimension;
            el = new double[n, m];

            for (int row = 0; row < n; row++)
                for (int column = 0; column < m; column++)
                    el[row, column] = x[row, column];

        }

        public Matrix(double[,] x)
        {
            m = x.GetLength(1); // Columns
            n = x.GetLength(0); // Rows
            el = new double[n,m];

            for (int row = 0; row < n; row++)
                for (int column = 0; column < m; column++)
                    el[row,column] = x[row,column];
        }

        public Matrix(int columnDimension, int rowDimension)
        {
            m = columnDimension;
            n = rowDimension;
            el = new double[n,m];
        }
        #endregion

        public void CopyFrom(IMatrix x)
        {
            for (int row = 0; row < n; row++)
                for (int column = 0; column < m; column++)
                    el[row, column] = x[row, column];
        }



        #region Element Accessors
        public IVector GetColumn(int columnNumber)
        {
            if (columnNumber >= ColumnDimension)
                return null;

            Vector columnVector = new Vector(RowDimension);

            for (int r = 0; r < RowDimension; r++)
                columnVector[r] = GetElement(columnNumber,r);

            return columnVector;
        }

        public void SetColumn(int columnNumber, IVector columnVector)
        {
            if (columnNumber >= ColumnDimension)
                return ;

            if (columnVector.Dimension != RowDimension)
                return;

            for (int r = 0; r < RowDimension; r++)
                SetElement(columnNumber,r, columnVector[r]); 

        }

        public IVector GetRow(int rowNumber)
        {
            if (rowNumber >= RowDimension)
                return null;

            Vector rowVector = new Vector(ColumnDimension);
            
            for (int c = 0; c < ColumnDimension; c++)
                rowVector[c] = GetElement(c,rowNumber);

            return rowVector;
        }

        public void SetRow(int rowNumber, IVector rowVector)
        {
            if (rowNumber >= RowDimension)
                return;

            if (rowVector.Dimension != ColumnDimension)
                return;

            for (int c = 0; c < ColumnDimension; c++)
                SetElement(c, rowNumber,rowVector[c]);

        }

        public double GetElement(int column, int row)
        {
            return el[row, column];
        }

        public void SetElement(int row, int column, double elem)
        {
            el[row, column] = elem;
        }

        public double this[int column, int row]
        {
            get { return el[row, column]; }
            set { el[row, column] = value; }
        }
        #endregion Element Accessors

        public IMatrix Transpose
        {
            get
            {
                Matrix ma = new Matrix(n, m);
                for (int k = 0; k < m; k++)
                    for (int kk = 0; kk < n; kk++)
                        ma.SetElement(k,kk, el[kk,k]);

                return ma;
            }
        }

        #region Methods
        public void Clear()
        {
            for (int row = 0; row < RowDimension; row++)
                for (int column = 0; column < ColumnDimension; column++)
                {
                        SetElement(column, row, 0);
                }

        }

        #endregion Methods

        #region Operator overloads
        public static Matrix operator *(double a, Matrix c)
        {
            Matrix b = new Matrix(c.RowDimension, c.ColumnDimension);
            for (int k = 0; k < c.RowDimension; k++)
                for (int j = 0; j < c.ColumnDimension; j++)
                    b.SetElement(j,k, a * c.GetElement(j,k));

            return b;
        }

        public static Matrix operator *(Matrix c, double a)
        {
            return a * c;
        }

        public static Matrix operator /(Matrix c, double a)
        {
            double s = 1 / a;
            return c * s;
        }

        public static Matrix operator +(Matrix a, Matrix c)
        {
            Matrix b = new Matrix(a.RowDimension, a.ColumnDimension);
            for (int k = 0; k < a.RowDimension; k++)
                for (int j = 0; j < a.ColumnDimension; j++)
                    b.SetElement(j,k, a.GetElement(j,k) + c.GetElement(j,k));

            return b;
        }
        
        public static Matrix operator -(Matrix a, Matrix c)
        {
            Matrix b = new Matrix(a.RowDimension, a.ColumnDimension);
            for (int k = 0; k < a.RowDimension; k++)
                for (int j = 0; j < a.ColumnDimension; j++)
                    b.SetElement(j, k, a.GetElement(j, k) - c.GetElement(j, k));
            return b;
        }

        public static IVector Multiply (Matrix a, IVector c)
        {
            Vector b = new Vector(a.RowDimension);
            for (int k = 0; k < b.Dimension; k++)
            {
                double sum = 0.0;
                for (int s = 0; s < c.Dimension; s++)
                    sum += a.GetElement(s,k) * c[s];
                b[k] = sum;
            }
            return b;
        }
        
        public static IVector operator *(IVector c, Matrix a)
        {
            Vector b = new Vector(a.ColumnDimension);
            for (int k = 0; k < b.Dimension; k++)
            {
                double sum = 0.0;
                for (int s = 0; s < c.Dimension; s++)
                    sum += c[s] * a.GetElement(k,s);
                b[k] = sum;
            }
            return b;
        }

        public static Matrix operator *(Matrix a, Matrix c)
        {
            Matrix b = new Matrix(a.RowDimension, c.ColumnDimension);
            for (int k = 0; k < b.RowDimension; k++)
                for (int j = 0; j < b.ColumnDimension; j++)
                {
                    double sum = 0.0;
                    for (int s = 0; s < a.ColumnDimension; s++)
                        sum += a.GetElement(s,k) * c.GetElement(j,s);
                    b.SetElement(j,k, sum);
                }
            return b;
        }
        #endregion

        public override string ToString()
        {
            string res = "";
            for (int i = 0; i < this.RowDimension; i++)
            {
                for (int j = 0; j < this.ColumnDimension; j++)
                    res += GetElement(j,i).ToString() + "     ";
                res += "\r\n";
            }
            res += "\r\n";
            return res;
        }
    }
}

