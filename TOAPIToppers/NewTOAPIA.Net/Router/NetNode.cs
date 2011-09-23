using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewTOAPIA.Net.Router
{

    public class NetVertex
    {
        public int ID;
        public List<NetVertex> Neighbors = new List<NetVertex>();

        public NetVertex(int id)
        {
            this.ID = id;
        }

        public void AddNeighbor(NetVertex neighbor)
        {
            Neighbors.Add(neighbor);
        }
    }

    public class NodeGraph
    {
        // Dijkstra's algorithm
        public static Dictionary<NetVertex, int> CreateShortestPathGraph(List<NetVertex> Graph, NetVertex source)
        {
            Dictionary<NetVertex, int> dist = new Dictionary<NetVertex, int>();
            Dictionary<NetVertex, NetVertex> previous = new Dictionary<NetVertex, NetVertex>();

            // 1 - Assign each node a distance value.  
            //     Set it to zero for the initial node, infinity for all other nodes.
            foreach (NetVertex v in Graph)
            {
                dist[v] = int.MaxValue;
                previous[v] = null;
            }
            dist[source] = 0;

            List<NetVertex> Q = new List<NetVertex>(Graph);
            while (Q.Count > 0)
            {
                NetVertex u = null;
                if (dist[u] == int.MaxValue)
                    break;
                Q.Remove(u);
                foreach(NetVertex v in u.Neighbors)
                {
                    int alt = 0;
                    //alt = dist[u] + dist_between(u,v);
                    if (alt < dist[v])
                    {
                        dist[v] = alt;
                        previous[v] = u;
                    }
                }
            }

            return dist;

            // 2 - Mark all nodes as unvisited.  Set initial node as current
            // 3 - For current node,
            //    consider all its unvisited neighbors and calculate their distance (from the initial node).
            // 4 - When we are done considering all neighbors of the current node, mark it as visited.
            //     A visited node will never be checked again
            //     its distance recorded now is final and minimal
            // 5 - Set the unvisited node with the smallest distance (from the initial node) as the next "current node"
            //     Continue from step 3.
        }
    }
}
