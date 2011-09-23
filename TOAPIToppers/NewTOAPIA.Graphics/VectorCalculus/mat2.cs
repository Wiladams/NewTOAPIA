namespace NewTOAPIA.Graphics
{
    using NewTOAPIA;

    public class mat2 : MatrixSquare
    {
        #region Constructors
        public mat2()
            :base(2)
        {
        }

        public mat2(double[,] x)
            : base(x)
        {
        }

        public mat2(IMatrix x)
            :base(2)
        {
            CopyFrom(x);
        }

        public mat2(vec2 r0, vec2 r1)
            : base(2)
        {
            this[0, 0] = r0.x; this[1, 0] = r0.y;
            this[0, 1] = r1.x; this[1, 1] = r1.y;
        }

        public mat2(float a00, float a10,
            float a01, float a11)
            :base(2)
        {
            this[0,0] = a00;
            this[1, 0] = a10;
            this[0, 1] = a01;
            this[1, 1] = a11;
        }

        public mat2(double a00, double a10,
            double a01, double a11)
            :base(2)
        {
            this[0, 0] = a00;
            this[1, 0] = a10;
            this[0, 1] = a01;
            this[1, 1] = a11;
        }

        #endregion

        #region Properties
        public static mat2 Identity
        {
            get { return new mat2(1, 0, 0, 1); }
        }

        public mat2 Inverse
        {
            get
            {
                double[,] x = GetInverse();
                mat2 r = new mat2(x);

                return r;
            }
        }

        public static mat2 Zero
        {
            get { return new mat2(0, 0, 0, 0); }
        }

        #endregion

        public vec2 this[int column]
        {
            get
            {
                vec2 r = new vec2(GetColumn(column));
                return r;
            }
        }
    }
}

