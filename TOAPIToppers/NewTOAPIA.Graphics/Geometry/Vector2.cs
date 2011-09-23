using System;

namespace NewTOAPIA.Graphics
{

    public struct Vector2d
    {
        double[] fArray;

        public Vector2d(double[] v)
        {
            fArray = v;
        }

        public Vector2d(double a, double b)
        {
            fArray = new double[] { a, b };
        }

        public double this[int i]
        {
            get
            {
                return fArray[i];
            }
            set
            {
                fArray[i] = value;
            }
        }

        public static explicit operator double[](Vector2d aVector)
        {
            return aVector.fArray;
        }
    }


    //public struct Vector2f
    //{
    //    public float x;
    //    public float y;

    //    public Vector2f(float x, float y)
    //    {
    //        this.x = x;
    //        this.y = y;
    //    }

    //    public float this[int i]
    //    {
    //        get
    //        {
    //            float retValue = 0.0f;

    //            switch (i)
    //            {
    //                case 0:
    //                    retValue = x;
    //                    break;
    //                case 1:
    //                    retValue = y;
    //                    break;
    //                default:
    //                    throw new ArgumentOutOfRangeException("indexer", i, "Value must be between 0 and 1 inclusive.");
    //            }

    //            return retValue;
    //        }
    //    }

    //    public static explicit operator float[](Vector2f vec)
    //    {
    //        return new float[] { vec.x, vec.y };
    //    }
    //}

    public struct Vector2i
    {
        public int x;
        public int y;

        public Vector2i(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static explicit operator int[](Vector2i vec)
        {
            return new int[] { vec.x, vec.y };
        }
    }

    //public struct Vector2s
    //{
    //    short[] fArray;


    //    public Vector2s(short a, short b)
    //    {
    //        fArray = new short[] { a, b };
    //    }

    //    public short this[int i]
    //    {
    //        get
    //        {
    //            return fArray[i];
    //        }
    //        set
    //        {
    //            fArray[i] = value;
    //        }
    //    }

    //    public static explicit operator short[](Vector2s aVector)
    //    {
    //        return aVector.fArray;
    //    }
    //}
}
