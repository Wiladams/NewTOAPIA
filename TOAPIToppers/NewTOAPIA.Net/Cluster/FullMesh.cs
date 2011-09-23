using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewTOAPIA.Net
{
    public class FullMesh : Cluster
    {
        public FullMesh(Uri name)
            : base(name, ClusterState.FullMesh)
        {
        }
    }
}
