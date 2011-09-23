using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewTOAPIA.Net
{
    using System.Net;

    public class MulticastMesh : FullMesh
    {
        IPAddress fAddress;
        short fControlPort;
        short fDataPort;

        public MulticastMesh(Uri resourceLocator, IPAddress addr, short ctlPort, short datPort)
            : base(resourceLocator)
        {
            fAddress = addr;
            fControlPort = ctlPort;
            fDataPort = datPort;
        }

        public override ClusterNode JoinCluster()
        {
            MulticastNode node = new MulticastNode(this);
            return node;
        }

        public override void LeaveCluster(ClusterNode node)
        {
            return;
        }
    }
}
