using System;
using System.Collections.Generic;
using System.Text;

namespace QuadVideo
{
    /// <summary>
    /// This object represents a simple row/column pair.  it is no more
    /// complex than the POINT object found in typical Win32 calls, but
    /// it's name implies the representation of discrete resolution.
    /// 
    /// </summary>
    public struct Resolution
    {
        public int Columns;
        public int Rows;

        public Resolution(int columns, int rows)
        {
            Columns = columns;
            Rows = rows;
        }
    }
}
