using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewTOAPIA.Graphics
{
    public class mat3x4 : Matrix
    {
        public mat3x4()
            : base(3, 4)
        {
        }

        public mat3x4(IMatrix m)
            : base(3, 4)
        {
            CopyFrom(m);
        }
    }
}
