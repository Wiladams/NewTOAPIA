﻿namespace NewTOAPIA
{
    public class GPoint
    {
        private double[] c;

        #region Constructors
        public GPoint(int dimension)
        {
            c = new double[dimension];
        }

        public double this[int where]
        {
            get { return c[where]; }
            set { c[where] = value; }
        }
        
        public GPoint(double[] realValue)
        {
            c = new double[realValue.Length];
            for (int i = 0; i < realValue.Length; i++)
                c[i] = realValue[i];
        }
        #endregion

        public double[] Coordinate
        {
            get { return c; }
            set { c = value; }
        }
    }
}

