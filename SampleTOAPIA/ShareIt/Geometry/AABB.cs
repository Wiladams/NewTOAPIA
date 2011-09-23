using System;
using System.Collections.Generic;
using System.Text;

using NewTOAPIA;

namespace ShowIt
{
    public struct AABB
    {
        public Vector3D size;
        public Point3D position;

        public AABB(Vector3D size, Point3D position)
        {
            this.size = size;
            this.position = position;
        }

    }
}
