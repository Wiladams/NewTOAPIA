namespace NewTOAPIA.Graphics
{
    #region unused
    // A point set with multiple dimensions
    public class XDPoint
    {
        private double[] c;

        #region Constructors
        public XDPoint(int dimension)
        {
            c = new double[dimension];
        }
                
        public XDPoint(double[] realValue)
        {
            c = new double[realValue.Length];
            for (int i = 0; i < realValue.Length; i++)
                c[i] = realValue[i];
        }
        #endregion

        public double this[int where]
        {
            get { return c[where]; }
            set { c[where] = value; }
        }
        
        public double[] Coordinate
        {
            get { return c; }
            set { c = value; }
        }
    }

    #endregion
}