
namespace NewTOAPIA.Drawing
{
    using NewTOAPIA.Graphics;

    class point_d_vector : VectorPOD<Vector2D>, IVertexDest
    {

    }

    //============================================================vcgen_stroke
    class vcgen_stroke : IGenerator
    {
        enum status_e
        {
            initial,
            ready,
            cap1,
            cap2,
            outline1,
            close_first,
            outline2,
            out_vertices,
            end_poly1,
            end_poly2,
            stop
        }

        math_stroke m_stroker;

        VertexSequence m_src_vertices;
        point_d_vector m_out_vertices;

        double m_shorten;
        int m_closed;
        status_e m_status;
        status_e m_prev_status;

        int m_src_vertex;
        int m_out_vertex;


        public vcgen_stroke()
        {
            m_stroker = new math_stroke();
            m_src_vertices = new VertexSequence();
            m_out_vertices = new point_d_vector();
            m_status = status_e.initial;
        }

        public void line_cap(math_stroke.line_cap_e lc) { m_stroker.line_cap(lc); }
        public void line_join(math_stroke.line_join_e lj) { m_stroker.line_join(lj); }
        public void inner_join(math_stroke.inner_join_e ij) { m_stroker.inner_join(ij); }

        public math_stroke.line_cap_e line_cap() { return m_stroker.line_cap(); }
        public math_stroke.line_join_e line_join() { return m_stroker.line_join(); }
        public math_stroke.inner_join_e inner_join() { return m_stroker.inner_join(); }

        public void width(double w) { m_stroker.width(w); }
        public void miter_limit(double ml) { m_stroker.miter_limit(ml); }
        public void miter_limit_theta(double t) { m_stroker.miter_limit_theta(t); }
        public void inner_miter_limit(double ml) { m_stroker.inner_miter_limit(ml); }
        public void approximation_scale(double approx_scale) { m_stroker.approximation_scale(approx_scale); }

        public double width() { return m_stroker.width(); }
        public double miter_limit() { return m_stroker.miter_limit(); }
        public double inner_miter_limit() { return m_stroker.inner_miter_limit(); }
        public double approximation_scale() { return m_stroker.approximation_scale(); }

        public void shorten(double s) { m_shorten = s; }
        public double shorten() { return m_shorten; }

        // Vertex Generator Interface
        public void remove_all()
        {
            m_src_vertices.Clear();
            m_closed = 0;
            m_status = status_e.initial;
        }
        public void add_vertex(double x, double y, Path.FlagsAndCommand cmd)
        {
            m_status = status_e.initial;
            if (Path.is_move_to(cmd))
            {
                m_src_vertices.modify_last(new VertexWithDistance(x, y));
            }
            else
            {
                if (Path.is_vertex(cmd))
                {
                    m_src_vertices.Add(new VertexWithDistance(x, y));
                }
                else
                {
                    m_closed = (int)Path.get_close_flag(cmd);
                }
            }
        }

        // Vertex Source Interface
        public void rewind(int idx)
        {
            if (m_status == status_e.initial)
            {
                m_src_vertices.close(m_closed != 0);
                Path.shorten_path(m_src_vertices, m_shorten, m_closed);
                if (m_src_vertices.Count < 3) m_closed = 0;
            }
            m_status = status_e.ready;
            m_src_vertex = 0;
            m_out_vertex = 0;
        }

        public Path.FlagsAndCommand vertex(ref double x, ref double y)
        {
            Path.FlagsAndCommand cmd = Path.FlagsAndCommand.CommandLineTo;
            while (!Path.is_stop(cmd))
            {
                switch (m_status)
                {
                    case status_e.initial:
                        rewind(0);
                        goto case status_e.ready;

                    case status_e.ready:
                        if (m_src_vertices.Count < 2 + (m_closed != 0 ? 1 : 0))
                        {
                            cmd = Path.FlagsAndCommand.CommandStop;
                            break;
                        }
                        m_status = (m_closed != 0) ? vcgen_stroke.status_e.outline1 : vcgen_stroke.status_e.cap1;
                        cmd = Path.FlagsAndCommand.CommandMoveTo;
                        m_src_vertex = 0;
                        m_out_vertex = 0;
                        break;

                    case status_e.cap1:
                        m_stroker.calc_cap(m_out_vertices, m_src_vertices[0], m_src_vertices[1],
                            m_src_vertices[0].dist);
                        m_src_vertex = 1;
                        m_prev_status = vcgen_stroke.status_e.outline1;
                        m_status = vcgen_stroke.status_e.out_vertices;
                        m_out_vertex = 0;
                        break;

                    case status_e.cap2:
                        m_stroker.calc_cap(m_out_vertices,
                            m_src_vertices[m_src_vertices.Count - 1],
                            m_src_vertices[m_src_vertices.Count - 2],
                            m_src_vertices[m_src_vertices.Count - 2].dist);
                        m_prev_status = vcgen_stroke.status_e.outline2;
                        m_status = vcgen_stroke.status_e.out_vertices;
                        m_out_vertex = 0;
                        break;

                    case status_e.outline1:
                        if (m_closed != 0)
                        {
                            if (m_src_vertex >= m_src_vertices.Count)
                            {
                                m_prev_status = vcgen_stroke.status_e.close_first;
                                m_status = vcgen_stroke.status_e.end_poly1;
                                break;
                            }
                        }
                        else
                        {
                            if (m_src_vertex >= m_src_vertices.Count - 1)
                            {
                                m_status = vcgen_stroke.status_e.cap2;
                                break;
                            }
                        }
                        m_stroker.calc_join(m_out_vertices,
                            m_src_vertices.prev(m_src_vertex),
                            m_src_vertices.curr(m_src_vertex),
                            m_src_vertices.next(m_src_vertex),
                            m_src_vertices.prev(m_src_vertex).dist,
                            m_src_vertices.curr(m_src_vertex).dist);
                        ++m_src_vertex;
                        m_prev_status = m_status;
                        m_status = vcgen_stroke.status_e.out_vertices;
                        m_out_vertex = 0;
                        break;

                    case status_e.close_first:
                        m_status = vcgen_stroke.status_e.outline2;
                        cmd = Path.FlagsAndCommand.CommandMoveTo;
                        goto case status_e.outline2;

                    case status_e.outline2:
                        if (m_src_vertex <= (m_closed == 0 ? 1 : 0))
                        {
                            m_status = vcgen_stroke.status_e.end_poly2;
                            m_prev_status = vcgen_stroke.status_e.stop;
                            break;
                        }

                        --m_src_vertex;
                        m_stroker.calc_join(m_out_vertices,
                            m_src_vertices.next(m_src_vertex),
                            m_src_vertices.curr(m_src_vertex),
                            m_src_vertices.prev(m_src_vertex),
                            m_src_vertices.curr(m_src_vertex).dist,
                            m_src_vertices.prev(m_src_vertex).dist);

                        m_prev_status = m_status;
                        m_status = vcgen_stroke.status_e.out_vertices;
                        m_out_vertex = 0;
                        break;

                    case status_e.out_vertices:
                        if (m_out_vertex >= m_out_vertices.size())
                        {
                            m_status = m_prev_status;
                        }
                        else
                        {
                            Vector2D c = m_out_vertices[(int)m_out_vertex++];
                            x = c.x;
                            y = c.y;
                            return cmd;
                        }
                        break;

                    case status_e.end_poly1:
                        m_status = m_prev_status;
                        return Path.FlagsAndCommand.CommandEndPoly
                            | Path.FlagsAndCommand.FlagClose
                            | Path.FlagsAndCommand.FlagCCW;

                    case status_e.end_poly2:
                        m_status = m_prev_status;
                        return Path.FlagsAndCommand.CommandEndPoly
                            | Path.FlagsAndCommand.FlagClose
                            | Path.FlagsAndCommand.FlagCW;

                    case status_e.stop:
                        cmd = Path.FlagsAndCommand.CommandStop;
                        break;
                }
            }
            return cmd;
        }
    }
}