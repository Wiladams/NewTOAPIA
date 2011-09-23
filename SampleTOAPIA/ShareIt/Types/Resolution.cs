using System;
using System.Collections.Generic;
using System.Text;

namespace ShowIt
{
    public struct Resolution
    {
        public readonly int Columns;
        public readonly int Rows;

        public Resolution(int columns, int rows)
        {
            Columns = columns;
            Rows = rows;
        }
    }
}
