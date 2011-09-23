

namespace NewTOAPIA.Drawing
{
    using System;
    using System.Collections.Generic;
    using NewTOAPIA.Graphics;


//--------------------------------------------------------vertex_dist_cmd
// Save as the above but with additional "command" value
    struct VertexWithCommand
    {
        public static VertexWithCommand Empty = new VertexWithCommand();

        public double x;
        public double y;

        public Path.FlagsAndCommand cmd;

        public VertexWithCommand(double x_, double y_, Path.FlagsAndCommand cmd_)
        {
            x = x_;
            y = y_;

            cmd = cmd_;
        }
    }


    //-------------------------------------------------------------vertex_dist
    // Vertex (x, y) with the distance to the next one. The last vertex has 
    // distance between the last and the first points if the polygon is closed
    // and 0.0 if it's a polyline.
    public struct VertexWithDistance
    {
        public const double vertex_dist_epsilon = 1e-14;

        public double x;
        public double y;
        public double dist;

        public VertexWithDistance(double x_, double y_)
        {
            x = x_;
            y = y_;
            dist = 0.0;
        }

        public bool IsEqual(VertexWithDistance val)
        {
            bool ret = (dist = Vector2D.calc_distance(x, y, val.x, val.y)) > vertex_dist_epsilon;
            if (!ret) 
                dist = 1.0 / vertex_dist_epsilon;
            
            return ret;
        }
    }


    //----------------------------------------------------------vertex_sequence
    // Modified agg::pod_vector. The data is interpreted as a sequence 
    // of vertices. It means that the type T must expose:
    //
    // bool T::operator() (const T& val)
    // 
    // that is called every time a new vertex is being added. The main purpose
    // of this operator is the possibility to calculate some values during 
    // adding and to return true if the vertex fits some criteria or false if
    // it doesn't. In the last case the new vertex is not added. 
    // 
    // The simple example is filtering coinciding vertices with calculation 
    // of the distance between the current and previous ones:
    //
    //    struct vertex_dist
    //    {
    //        double   x;
    //        double   y;
    //        double   dist;
    //
    //        vertex_dist() {}
    //        vertex_dist(double x_, double y_) :
    //            x(x_),
    //            y(y_),
    //            dist(0.0)
    //        {
    //        }
    //
    //        bool operator () (const vertex_dist& val)
    //        {
    //            return (dist = calc_distance(x, y, val.x, val.y)) > EPSILON;
    //        }
    //    };
    //
    // Function close() calls this operator and removes the last vertex if 
    // necessary.
    //------------------------------------------------------------------------
    public class VertexSequence : List<VertexWithDistance>
    {
        public void AddVertex(VertexWithDistance val)
        {
            if (base.Count > 1)
            {
                if (!this[base.Count - 2].IsEqual(this[base.Count - 1]))
                {
                    base.RemoveAt(Count - 1);
                }
            }
            base.Add(val);
        }

        public void RemoveLast()
        {
            base.RemoveAt(Count - 1);
        }

        public void modify_last(VertexWithDistance val)
        {
            base.RemoveAt(Count - 1);
            AddVertex(val);
        }

        public void close(bool closed)
        {
            while (base.Count > 1)
            {
                if (this[base.Count - 2].IsEqual(this[base.Count - 1])) 
                    break;
                
                VertexWithDistance t = this[base.Count - 1];
                base.RemoveAt(Count - 1);
                modify_last(t);
            }

            if (closed)
            {
                while (base.Count > 1)
                {
                    if (this[base.Count - 1].IsEqual(this[0])) 
                        break;
                    
                    base.RemoveAt(Count - 1);
                }
            }
        }

        internal VertexWithDistance prev(int idx)
        {
            return this[(idx + Count - 1) % Count];
        }

        internal VertexWithDistance curr(int idx)
        {
            return this[idx];
        }

        internal VertexWithDistance next(int idx)
        {
            return this[(idx + 1) % Count];
        }
    }




}

//#endif
