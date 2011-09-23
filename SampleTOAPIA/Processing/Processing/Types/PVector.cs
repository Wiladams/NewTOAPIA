using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Processing
{
    using NewTOAPIA;

    public class PVector : Vector
    {
        public PVector()
            :base(3)
        {
        }

        public PVector(float x, float y)
            : base(3)
        {
            this[0] = x;
            this[1] = y;
        }

        public PVector(float x, float y, float z)
            : base(3)
        {
            this[0] = x;
            this[1] = y;
            this[2] = z;
        }
    }
}
