
namespace NewTOAPIA.Drawing
{
    using System;
    using System.Collections.Generic;

    using NewTOAPIA.Graphics;

    //---------------------------------------------------------------path_base
    // A container to store vertices with their flags. 
    // A path consists of a number of contours separated with "move_to" 
    // commands. The path storage can keep and maintain more than one
    // path. 
    // To navigate to the beginning of a particular path, use rewind(path_id);
    // Where path_id is what start_new_path() returns. So, when you call
    // start_new_path() you need to store its return value somewhere else
    // to navigate to the path afterwards.
    //
    // See also: vertex_source concept
    //------------------------------------------------------------------------
    public class PathStorage : IVertexSource, IVertexDest
    {

        #region InternalVertexStorage
        private class VertexStorage : List<VertexWithCommand>
        {
            //List<VertexWithCommand> m_coord = new List<VertexWithCommand>();

            public VertexStorage()
            {
            }

            public void remove_all()
            {
                Clear();
            }

            public void free_all()
            {
                Clear();
            }

            //public int Count()
            //{
            //    return m_coord.Count;
            //}



            public void add_vertex(double x, double y, Path.FlagsAndCommand CommandAndFlags)
            {
                VertexWithCommand newVertex = new VertexWithCommand(x, y, CommandAndFlags);
                Add(newVertex);
            }

            public void modify_vertex(int idx, double x, double y)
            {
                VertexWithCommand oldVertex = this[idx];
                oldVertex.x = x;
                oldVertex.y = y;
                this[idx] = oldVertex;
            }

            public void modify_vertex(int idx, double x, double y, Path.FlagsAndCommand CommandAndFlags)
            {
                VertexWithCommand newVertex = new VertexWithCommand(x, y, CommandAndFlags);
                this[idx] = newVertex;
            }

            public void modify_command(int idx, Path.FlagsAndCommand CommandAndFlags)
            {
                VertexWithCommand oldVertex = this[idx];
                oldVertex.cmd = CommandAndFlags;
                this[idx] = oldVertex;
            }

            public void SwapVertices(int v1, int v2)
            {
                VertexWithCommand tmpVertex = this[v1];
                this[v1] = this[v2];
                this[v2] = tmpVertex;
            }


            public Path.FlagsAndCommand GetLastVertex(out double x, out double y)
            {
                if (Count > 0)
                {
                    VertexWithCommand vertex = this[Count - 1];
                    x = vertex.x;
                    y = vertex.y;
                    return vertex.cmd;
                }

                x = default(double);
                y = default(double);

                return Path.FlagsAndCommand.CommandStop;
            }

            public Path.FlagsAndCommand prev_vertex(out double x, out double y)
            {
                if (Count > 1)
                {
                    return GetVertex((int)(Count - 2), out x, out y);
                }

                x = default(double);
                y = default(double);

                return Path.FlagsAndCommand.CommandStop;
            }

            public double LastX()
            {
                if (Count > 1)
                {
                    int idx = (int)(Count - 1);
                    return this[idx].x;
                }

                return default(double);
            }

            public double LastY()
            {
                if (Count > 1)
                {
                    int idx = (int)(Count - 1);

                    return this[idx].y;
                }

                return default(double);
            }

            public Path.FlagsAndCommand last_command()
            {
                if (Count != 0)
                {
                    return this[Count - 1].cmd;
                }

                return Path.FlagsAndCommand.CommandStop;
            }

            public int total_vertices()
            {
                return Count;
            }

            public Path.FlagsAndCommand GetVertex(int idx, out double x, out double y)
            {
                VertexWithCommand vertex = this[idx];

                x = vertex.x;
                y = vertex.y;

                return vertex.cmd;
            }

            public Path.FlagsAndCommand GetCommand(int idx)
            {
                VertexWithCommand vertex = this[idx];

                return vertex.cmd;
            }
        }
        #endregion

        private VertexStorage m_vertices = new VertexStorage();
        //List<VertexWithCommand> m_vertices = new List<VertexWithCommand>();

        private int m_iterator;

        public PathStorage()
        {
        }

        public void add(Vector2D vertex)
        {
            throw new System.NotImplementedException();
        }

        public int size()
        {
            return m_vertices.Count;
        }

        public Vector2D this[int i]
        {
            get
            {
                throw new NotImplementedException("make this work");
            }
        }

        public void Clear() 
        {
            m_vertices.remove_all();
            m_iterator = 0; 
        }

        // Make path functions
        //--------------------------------------------------------------------
        public int StartNewPath()
        {
            if (!Path.is_stop(m_vertices.last_command()))
            {

                m_vertices.add_vertex(0.0, 0.0, Path.FlagsAndCommand.CommandStop);
            }

            return m_vertices.total_vertices();
        }


        public void rel_to_abs(ref double x, ref double y)
        {
            if (m_vertices.total_vertices() != 0)
            {
                double x2;
                double y2;
                if (Path.is_vertex(m_vertices.GetLastVertex(out x2, out y2)))
                {
                    x += x2;
                    y += y2;
                }
            }
        }

        public void MoveTo(double x, double y)
        {
            m_vertices.add_vertex(x, y, Path.FlagsAndCommand.CommandMoveTo);
        }

        public void MoveBy(double dx, double dy)
        {
            rel_to_abs(ref dx, ref dy);
            m_vertices.add_vertex(dx, dy, Path.FlagsAndCommand.CommandMoveTo);
        }

        public void LineTo(double x, double y)
        {
            m_vertices.add_vertex(x, y, Path.FlagsAndCommand.CommandLineTo);
        }

        public void line_rel(double dx, double dy)
        {
            rel_to_abs(ref dx, ref dy);
            m_vertices.add_vertex(dx, dy, Path.FlagsAndCommand.CommandLineTo);
        }

        public void hline_to(double x)
        {
            m_vertices.add_vertex(x, last_y(), Path.FlagsAndCommand.CommandLineTo);
        }

        public void hline_rel(double dx)
        {
            double dy = 0;
            rel_to_abs(ref dx, ref dy);
            m_vertices.add_vertex(dx, dy, Path.FlagsAndCommand.CommandLineTo);
        }

        public void vline_to(double y)
        {
            m_vertices.add_vertex(last_x(), y, Path.FlagsAndCommand.CommandLineTo);
        }

        public void vline_rel(double dy)
        {
            double dx = 0;
            rel_to_abs(ref dx, ref dy);
            m_vertices.add_vertex(dx, dy, Path.FlagsAndCommand.CommandLineTo);
        }

        /*
        public void arc_to(double rx, double ry,
                                   double angle,
                                   bool large_arc_flag,
                                   bool sweep_flag,
                                   double x, double y)
        {
            if(m_vertices.total_vertices() && is_vertex(m_vertices.last_command()))
            {
                double epsilon = 1e-30;
                double x0 = 0.0;
                double y0 = 0.0;
                m_vertices.last_vertex(&x0, &y0);

                rx = fabs(rx);
                ry = fabs(ry);

                // Ensure radii are valid
                //-------------------------
                if(rx < epsilon || ry < epsilon) 
                {
                    line_to(x, y);
                    return;
                }

                if(calc_distance(x0, y0, x, y) < epsilon)
                {
                    // If the endpoints (x, y) and (x0, y0) are identical, then this
                    // is equivalent to omitting the elliptical arc segment entirely.
                    return;
                }
                bezier_arc_svg a(x0, y0, rx, ry, angle, large_arc_flag, sweep_flag, x, y);
                if(a.radii_ok())
                {
                    join_path(a);
                }
                else
                {
                    line_to(x, y);
                }
            }
            else
            {
                move_to(x, y);
            }
        }

        public void arc_rel(double rx, double ry,
                                    double angle,
                                    bool large_arc_flag,
                                    bool sweep_flag,
                                    double dx, double dy)
        {
            rel_to_abs(&dx, &dy);
            arc_to(rx, ry, angle, large_arc_flag, sweep_flag, dx, dy);
        }
         */

        public void curve3(double x_ctrl, double y_ctrl,
                                   double x_to, double y_to)
        {
            m_vertices.add_vertex(x_ctrl, y_ctrl, Path.FlagsAndCommand.CommandCurve3);
            m_vertices.add_vertex(x_to, y_to, Path.FlagsAndCommand.CommandCurve3);
        }

        public void curve3_rel(double dx_ctrl, double dy_ctrl, double dx_to, double dy_to)
        {
            rel_to_abs(ref dx_ctrl, ref dy_ctrl);
            rel_to_abs(ref dx_to, ref dy_to);
            m_vertices.add_vertex(dx_ctrl, dy_ctrl, Path.FlagsAndCommand.CommandCurve3);
            m_vertices.add_vertex(dx_to, dy_to, Path.FlagsAndCommand.CommandCurve3);
        }

        public void curve3(double x_to, double y_to)
        {
            double x0;
            double y0;
            if (Path.is_vertex(m_vertices.GetLastVertex(out x0, out y0)))
            {
                double x_ctrl;
                double y_ctrl;
                Path.FlagsAndCommand cmd = m_vertices.prev_vertex(out x_ctrl, out y_ctrl);
                if (Path.is_curve(cmd))
                {
                    x_ctrl = x0 + x0 - x_ctrl;
                    y_ctrl = y0 + y0 - y_ctrl;
                }
                else
                {
                    x_ctrl = x0;
                    y_ctrl = y0;
                }
                curve3(x_ctrl, y_ctrl, x_to, y_to);
            }
        }

        public void curve3_rel(double dx_to, double dy_to)
        {
            rel_to_abs(ref dx_to, ref dy_to);
            curve3(dx_to, dy_to);
        }

        public void curve4(double x_ctrl1, double y_ctrl1,
                                   double x_ctrl2, double y_ctrl2,
                                   double x_to, double y_to)
        {
            m_vertices.add_vertex(x_ctrl1, y_ctrl1, Path.FlagsAndCommand.CommandCurve4);
            m_vertices.add_vertex(x_ctrl2, y_ctrl2, Path.FlagsAndCommand.CommandCurve4);
            m_vertices.add_vertex(x_to, y_to, Path.FlagsAndCommand.CommandCurve4);
        }

        public void curve4_rel(double dx_ctrl1, double dy_ctrl1,
                                       double dx_ctrl2, double dy_ctrl2,
                                       double dx_to, double dy_to)
        {
            rel_to_abs(ref dx_ctrl1, ref dy_ctrl1);
            rel_to_abs(ref dx_ctrl2, ref dy_ctrl2);
            rel_to_abs(ref dx_to, ref dy_to);
            m_vertices.add_vertex(dx_ctrl1, dy_ctrl1, Path.FlagsAndCommand.CommandCurve4);
            m_vertices.add_vertex(dx_ctrl2, dy_ctrl2, Path.FlagsAndCommand.CommandCurve4);
            m_vertices.add_vertex(dx_to, dy_to, Path.FlagsAndCommand.CommandCurve4);
        }

        public void curve4(double x_ctrl2, double y_ctrl2,
                                   double x_to, double y_to)
        {
            double x0;
            double y0;
            if (Path.is_vertex(GetLastVertex(out x0, out y0)))
            {
                double x_ctrl1;
                double y_ctrl1;
                Path.FlagsAndCommand cmd = prev_vertex(out x_ctrl1, out y_ctrl1);
                if (Path.is_curve(cmd))
                {
                    x_ctrl1 = x0 + x0 - x_ctrl1;
                    y_ctrl1 = y0 + y0 - y_ctrl1;
                }
                else
                {
                    x_ctrl1 = x0;
                    y_ctrl1 = y0;
                }
                curve4(x_ctrl1, y_ctrl1, x_ctrl2, y_ctrl2, x_to, y_to);
            }
        }

        public void curve4_rel(double dx_ctrl2, double dy_ctrl2,
                                       double dx_to, double dy_to)
        {
            rel_to_abs(ref dx_ctrl2, ref dy_ctrl2);
            rel_to_abs(ref dx_to, ref dy_to);
            curve4(dx_ctrl2, dy_ctrl2, dx_to, dy_to);
        }

        public int total_vertices()
        {
            return m_vertices.Count;
        }

        public Path.FlagsAndCommand GetLastVertex(out double x, out double y)
        {
            return m_vertices.GetLastVertex(out x, out y);
        }

        public Path.FlagsAndCommand prev_vertex(out double x, out double y)
        {
            return m_vertices.prev_vertex(out x, out y);
        }

        public double last_x()
        {
            return m_vertices.LastX();
        }

        public double last_y()
        {
            return m_vertices.LastY();
        }

        public Path.FlagsAndCommand vertex(int idx, out double x, out double y)
        {
            return m_vertices.GetVertex(idx, out x, out y);
        }

        public Path.FlagsAndCommand command(int idx)
        {
            return m_vertices.GetCommand(idx);
        }

        public void modify_vertex(int idx, double x, double y)
        {
            m_vertices.modify_vertex(idx, x, y);
        }

        public void modify_vertex(int idx, double x, double y, Path.FlagsAndCommand PathAndFlags)
        {
            m_vertices.modify_vertex(idx, x, y, PathAndFlags);
        }

        public void modify_command(int idx, Path.FlagsAndCommand PathAndFlags)
        {
            m_vertices.modify_command(idx, PathAndFlags);
        }

        public virtual void rewind(int path_id)
        {
            m_iterator = path_id;
        }

        public Path.FlagsAndCommand vertex(out double x, out double y)
        {
            if (m_iterator >= m_vertices.total_vertices())
            {
                x = 0;
                y = 0;
                return Path.FlagsAndCommand.CommandStop;
            }

            return m_vertices.GetVertex(m_iterator++, out x, out y);
        }

        // Arrange the orientation of a polygon, all polygons in a path, 
        // or in all paths. After calling arrange_orientations() or 
        // arrange_orientations_all_paths(), all the polygons will have 
        // the same orientation, i.e. path_flags_cw or path_flags_ccw
        //--------------------------------------------------------------------
        public int arrange_polygon_orientation(int start, Path.FlagsAndCommand orientation)
        {
            if (orientation == Path.FlagsAndCommand.FlagNone) return start;

            // Skip all non-vertices at the beginning
            while (start < m_vertices.total_vertices() &&
                  !Path.is_vertex(m_vertices.GetCommand(start))) ++start;

            // Skip all insignificant move_to
            while (start + 1 < m_vertices.total_vertices() &&
                  Path.is_move_to(m_vertices.GetCommand(start)) &&
                  Path.is_move_to(m_vertices.GetCommand(start + 1))) ++start;

            // Find the last vertex
            int end = start + 1;
            while (end < m_vertices.total_vertices() &&
                  !Path.is_next_poly(m_vertices.GetCommand(end))) ++end;

            if (end - start > 2)
            {
                if (perceive_polygon_orientation(start, end) != orientation)
                {
                    // Invert polygon, set orientation flag, and skip all end_poly
                    invert_polygon(start, end);
                    Path.FlagsAndCommand PathAndFlags;
                    while (end < m_vertices.total_vertices() &&
                          Path.is_end_poly(PathAndFlags = m_vertices.GetCommand(end)))
                    {
                        m_vertices.modify_command(end++, PathAndFlags | orientation);// Path.set_orientation(cmd, orientation));
                    }
                }
            }
            return end;
        }

        public int arrange_orientations(int start, Path.FlagsAndCommand orientation)
        {
            if (orientation != Path.FlagsAndCommand.FlagNone)
            {
                while (start < m_vertices.total_vertices())
                {
                    start = arrange_polygon_orientation(start, orientation);
                    if (Path.is_stop(m_vertices.GetCommand(start)))
                    {
                        ++start;
                        break;
                    }
                }
            }
            return start;
        }

        public void arrange_orientations_all_paths(Path.FlagsAndCommand orientation)
        {
            if (orientation != Path.FlagsAndCommand.FlagNone)
            {
                int start = 0;
                while (start < m_vertices.total_vertices())
                {
                    start = arrange_orientations(start, orientation);
                }
            }
        }

        // Flip all vertices horizontally or vertically, 
        // between x1 and x2, or between y1 and y2 respectively
        //--------------------------------------------------------------------
        public void flip_x(double x1, double x2)
        {
            int i;
            double x, y;
            for (i = 0; i < m_vertices.total_vertices(); i++)
            {
                Path.FlagsAndCommand PathAndFlags = m_vertices.GetVertex(i, out x, out y);
                if (Path.is_vertex(PathAndFlags))
                {
                    m_vertices.modify_vertex(i, x2 - x + x1, y);
                }
            }
        }

        public void flip_y(double y1, double y2)
        {
            int i;
            double x, y;
            for (i = 0; i < m_vertices.total_vertices(); i++)
            {
                Path.FlagsAndCommand PathAndFlags = m_vertices.GetVertex(i, out x, out y);
                if (Path.is_vertex(PathAndFlags))
                {
                    m_vertices.modify_vertex(i, x, y2 - y + y1);
                }
            }
        }

        public void end_poly()
        {
            close_polygon(Path.FlagsAndCommand.FlagClose);
        }

        public void end_poly(Path.FlagsAndCommand flags)
        {
            if (Path.is_vertex(m_vertices.last_command()))
            {
                m_vertices.add_vertex(0.0, 0.0, Path.FlagsAndCommand.CommandEndPoly | flags);
            }
        }


        public void ClosePolygon()
        {
            close_polygon(Path.FlagsAndCommand.FlagNone);
        }

        public void close_polygon(Path.FlagsAndCommand flags)
        {
            end_poly(Path.FlagsAndCommand.FlagClose | flags);
        }

        // Concatenate path. The path is added as is.
        public void concat_path(IVertexSource vs)
        {
            concat_path(vs, 0);
        }

        public void concat_path(IVertexSource vs, int path_id)
        {
            double x, y;
            Path.FlagsAndCommand PathAndFlags;
            vs.rewind(path_id);
            while (!Path.is_stop(PathAndFlags = vs.vertex(out x, out y)))
            {
                m_vertices.add_vertex(x, y, PathAndFlags);
            }
        }

        //--------------------------------------------------------------------
        // Join path. The path is joined with the existing one, that is, 
        // it behaves as if the pen of a plotter was always down (drawing)
        //template<class VertexSource> 
        public void join_path(PathStorage vs)
        {
            join_path(vs, 0);

        }

        public void join_path(PathStorage vs, int path_id)
        {
            double x, y;
            vs.rewind(path_id);
            Path.FlagsAndCommand PathAndFlags = vs.vertex(out x, out y);
            if (!Path.is_stop(PathAndFlags))
            {
                if (Path.is_vertex(PathAndFlags))
                {
                    double x0, y0;
                    Path.FlagsAndCommand PathAndFlags0 = GetLastVertex(out x0, out y0);
                    if (Path.is_vertex(PathAndFlags0))
                    {
                        if (Vector2D.calc_distance(x, y, x0, y0) > VertexWithDistance.vertex_dist_epsilon)
                        {
                            if (Path.is_move_to(PathAndFlags)) PathAndFlags = Path.FlagsAndCommand.CommandLineTo;
                            m_vertices.add_vertex(x, y, PathAndFlags);
                        }
                    }
                    else
                    {
                        if (Path.is_stop(PathAndFlags0))
                        {
                            PathAndFlags = Path.FlagsAndCommand.CommandMoveTo;
                        }
                        else
                        {
                            if (Path.is_move_to(PathAndFlags)) PathAndFlags = Path.FlagsAndCommand.CommandLineTo;
                        }
                        m_vertices.add_vertex(x, y, PathAndFlags);
                    }
                }
                while (!Path.is_stop(PathAndFlags = vs.vertex(out x, out y)))
                {
                    m_vertices.add_vertex(x, y, Path.is_move_to(PathAndFlags) ?
                                                    Path.FlagsAndCommand.CommandLineTo :
                                                    PathAndFlags);
                }
            }
        }

        /*
        // Concatenate polygon/polyline. 
        //--------------------------------------------------------------------
        void concat_poly(T* data, int num_points, bool closed)
        {
            poly_plain_adaptor<T> poly(data, num_points, closed);
            concat_path(poly);
        }

        // Join polygon/polyline continuously.
        //--------------------------------------------------------------------
        void join_poly(T* data, int num_points, bool closed)
        {
            poly_plain_adaptor<T> poly(data, num_points, closed);
            join_path(poly);
        }
         */

        //--------------------------------------------------------------------
        public void translate(double dx, double dy)
        {
            translate(dx, dy, 0);
        }

        public void translate(double dx, double dy, int path_id)
        {
            int num_ver = m_vertices.total_vertices();
            for (; path_id < num_ver; path_id++)
            {
                double x, y;
                Path.FlagsAndCommand PathAndFlags = m_vertices.GetVertex(path_id, out x, out y);
                if (Path.is_stop(PathAndFlags)) break;
                if (Path.is_vertex(PathAndFlags))
                {
                    x += dx;
                    y += dy;
                    m_vertices.modify_vertex(path_id, x, y);
                }
            }
        }

        public void translate_all_paths(double dx, double dy)
        {
            int idx;
            int num_ver = m_vertices.total_vertices();
            for (idx = 0; idx < num_ver; idx++)
            {
                double x, y;
                if (Path.is_vertex(m_vertices.GetVertex(idx, out x, out y)))
                {
                    x += dx;
                    y += dy;
                    m_vertices.modify_vertex(idx, x, y);
                }
            }
        }

        //--------------------------------------------------------------------
        public void transform(Affine trans)
        {
            transform(trans, 0);
        }

        public void transform(Affine trans, int path_id)
        {
            int num_ver = m_vertices.total_vertices();
            for (; path_id < num_ver; path_id++)
            {
                double x, y;
                Path.FlagsAndCommand PathAndFlags = m_vertices.GetVertex(path_id, out x, out y);
                if (Path.is_stop(PathAndFlags)) break;
                if (Path.is_vertex(PathAndFlags))
                {
                    trans.Transform(ref x, ref y);
                    m_vertices.modify_vertex(path_id, x, y);
                }
            }
        }

        //--------------------------------------------------------------------
        public void transform_all_paths(Affine trans)
        {
            int idx;
            int num_ver = m_vertices.total_vertices();
            for (idx = 0; idx < num_ver; idx++)
            {
                double x, y;
                if (Path.is_vertex(m_vertices.GetVertex(idx, out x, out y)))
                {
                    trans.Transform(ref x, ref y);
                    m_vertices.modify_vertex(idx, x, y);
                }
            }
        }

        public void invert_polygon(int start)
        {
            // Skip all non-vertices at the beginning
            while (start < m_vertices.total_vertices() &&
                  !Path.is_vertex(m_vertices.GetCommand(start))) ++start;

            // Skip all insignificant move_to
            while (start + 1 < m_vertices.total_vertices() &&
                  Path.is_move_to(m_vertices.GetCommand(start)) &&
                  Path.is_move_to(m_vertices.GetCommand(start + 1))) ++start;

            // Find the last vertex
            int end = start + 1;
            while (end < m_vertices.total_vertices() &&
                  !Path.is_next_poly(m_vertices.GetCommand(end))) ++end;

            invert_polygon(start, end);
        }

        private Path.FlagsAndCommand perceive_polygon_orientation(int start, int end)
        {
            // Calculate signed area (double area to be exact)
            //---------------------
            int np = end - start;
            double area = 0.0;
            int i;
            for (i = 0; i < np; i++)
            {
                double x1, y1, x2, y2;
                m_vertices.GetVertex(start + i, out x1, out y1);
                m_vertices.GetVertex(start + (i + 1) % np, out x2, out y2);
                area += x1 * y2 - y1 * x2;
            }
            return (area < 0.0) ? Path.FlagsAndCommand.FlagCW : Path.FlagsAndCommand.FlagCCW;
        }

        private void invert_polygon(int start, int end)
        {
            int i;
            Path.FlagsAndCommand tmp_PathAndFlags = m_vertices.GetCommand(start);

            --end; // Make "end" inclusive

            // Shift all commands to one position
            for (i = start; i < end; i++)
            {
                m_vertices.modify_command(i, m_vertices.GetCommand(i + 1));
            }

            // Assign starting command to the ending command
            m_vertices.modify_command(end, tmp_PathAndFlags);

            // Reverse the polygon
            while (end > start)
            {
                m_vertices.SwapVertices(start++, end--);
            }
        }
    }
}