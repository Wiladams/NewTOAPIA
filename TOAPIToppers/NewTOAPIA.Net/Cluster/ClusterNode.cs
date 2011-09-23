using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewTOAPIA.Net
{
    public class ClusterNode
    {
        public Cluster Cluster { get; set; }

        public ClusterNode(Cluster clust)
        {
            Cluster = clust;
        }
    }
}
