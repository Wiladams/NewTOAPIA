using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewTOAPIA.Graphics
{
    public class mat4x3 : Matrix
    {
        public mat4x3()
            : base(4, 3)
        {
        }

        public mat4x3(IMatrix m)
            : base(4, 3)
        {
            CopyFrom(m);
        }
    }
}
