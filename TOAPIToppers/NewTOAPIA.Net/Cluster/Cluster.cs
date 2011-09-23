using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewTOAPIA.Net
{
    public enum ClusterState
    {
        Cluster,
        HubSpoke,
        Star,
        SpanningTree,
        Spanner,
        FullMesh
    }

    public class Cluster
    {
        public Uri Name { get; set; }

        public ClusterState State { get; private set; }

        protected Cluster(Uri resourceName, ClusterState state)
        {
            Name = resourceName;
            State = state;
        }

        public virtual ClusterNode JoinCluster()
        {
            return null;
        }

        public virtual void LeaveCluster(ClusterNode node)
        {
        }

    }
}
