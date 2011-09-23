﻿namespace NewTOAPIA.Drawing
{
    using System;
    using NewTOAPIA.Graphics;


    public class math_stroke
    {
        public enum line_cap_e
        {
            butt_cap,
            square_cap,
            round_cap
        }

        public enum line_join_e
        {
            miter_join = 0,
            miter_join_revert = 1,
            round_join = 2,
            bevel_join = 3,
            miter_join_round = 4
        }

        public enum inner_join_e
        {
            inner_bevel,
            inner_miter,
            inner_jag,
            inner_round
        }
        double m_width;
        double m_width_abs;
        double m_width_eps;
        int m_width_sign;
        double m_miter_limit;
        double m_inner_miter_limit;
        double m_approx_scale;
        line_cap_e m_line_cap;
        line_join_e m_line_join;
        inner_join_e m_inner_join;




        public math_stroke()
        {
            m_width = 0.5;
            m_width_abs = 0.5;
            m_width_eps = 0.5 / 1024.0;
            m_width_sign = 1;
            m_miter_limit = 4.0;
            m_inner_miter_limit = 1.01;
            m_approx_scale = 1.0;
            m_line_cap = line_cap_e.butt_cap;
            m_line_join = line_join_e.miter_join;
            m_inner_join = inner_join_e.inner_miter;
        }

        public void line_cap(line_cap_e lc) { m_line_cap = lc; }
        public void line_join(line_join_e lj) { m_line_join = lj; }
        public void inner_join(inner_join_e ij) { m_inner_join = ij; }

        public line_cap_e line_cap() { return m_line_cap; }
        public line_join_e line_join() { return m_line_join; }
        public inner_join_e inner_join() { return m_inner_join; }

        public void width(double w)
        {
            m_width = w * 0.5;
            if (m_width < 0)
            {
                m_width_abs = -m_width;
                m_width_sign = -1;
            }
            else
            {
                m_width_abs = m_width;
                m_width_sign = 1;
            }
            m_width_eps = m_width / 1024.0;
        }

        public void miter_limit(double ml) { m_miter_limit = ml; }
        public void miter_limit_theta(double t)
        {
            m_miter_limit = 1.0 / Math.Sin(t * 0.5);
        }

        public void inner_miter_limit(double ml) { m_inner_miter_limit = ml; }
        public void approximation_scale(double aproxScale) { m_approx_scale = aproxScale; }

        public double width() { return m_width * 2.0; }
        public double miter_limit() { return m_miter_limit; }
        public double inner_miter_limit() { return m_inner_miter_limit; }
        public double approximation_scale() { return m_approx_scale; }

        public void calc_cap(IVertexDest vc, VertexWithDistance v0, VertexWithDistance v1, double len)
        {
            vc.Clear();

            double dx1 = (v1.y - v0.y) / len;
            double dy1 = (v1.x - v0.x) / len;
            double dx2 = 0;
            double dy2 = 0;

            dx1 *= m_width;
            dy1 *= m_width;

            if (m_line_cap != line_cap_e.round_cap)
            {
                if (m_line_cap == line_cap_e.square_cap)
                {
                    dx2 = dy1 * m_width_sign;
                    dy2 = dx1 * m_width_sign;
                }
                add_vertex(vc, v0.x - dx1 - dx2, v0.y + dy1 - dy2);
                add_vertex(vc, v0.x + dx1 - dx2, v0.y - dy1 - dy2);
            }
            else
            {
                double da = Math.Acos(m_width_abs / (m_width_abs + 0.125 / m_approx_scale)) * 2;
                double a1;
                int i;
                int n = (int)(Math.PI / da);

                da = Math.PI / (n + 1);
                add_vertex(vc, v0.x - dx1, v0.y + dy1);
                if (m_width_sign > 0)
                {
                    a1 = Math.Atan2(dy1, -dx1);
                    a1 += da;
                    for (i = 0; i < n; i++)
                    {
                        add_vertex(vc, v0.x + Math.Cos(a1) * m_width,
                                       v0.y + Math.Sin(a1) * m_width);
                        a1 += da;
                    }
                }
                else
                {
                    a1 = Math.Atan2(-dy1, dx1);
                    a1 -= da;
                    for (i = 0; i < n; i++)
                    {
                        add_vertex(vc, v0.x + Math.Cos(a1) * m_width,
                                       v0.y + Math.Sin(a1) * m_width);
                        a1 -= da;
                    }
                }
                add_vertex(vc, v0.x + dx1, v0.y - dy1);
            }
        }

        public void calc_join(IVertexDest vc, VertexWithDistance v0,
                                        VertexWithDistance v1,
                                        VertexWithDistance v2,
                                        double len1,
                                        double len2)
        {
            double dx1 = m_width * (v1.y - v0.y) / len1;
            double dy1 = m_width * (v1.x - v0.x) / len1;
            double dx2 = m_width * (v2.y - v1.y) / len2;
            double dy2 = m_width * (v2.x - v1.x) / len2;

            vc.Clear();

            double cp = agg_math.cross_product(v0.x, v0.y, v1.x, v1.y, v2.x, v2.y);
            if (cp != 0 && (cp > 0) == (m_width > 0))
            {
                // Inner join
                //---------------
                double limit = ((len1 < len2) ? len1 : len2) / m_width_abs;
                if (limit < m_inner_miter_limit)
                {
                    limit = m_inner_miter_limit;
                }

                switch (m_inner_join)
                {
                    default: // inner_bevel
                        add_vertex(vc, v1.x + dx1, v1.y - dy1);
                        add_vertex(vc, v1.x + dx2, v1.y - dy2);
                        break;

                    case inner_join_e.inner_miter:
                        calc_miter(vc,
                                   v0, v1, v2, dx1, dy1, dx2, dy2,
                                   line_join_e.miter_join_revert,
                                   limit, 0);
                        break;

                    case inner_join_e.inner_jag:
                    case inner_join_e.inner_round:
                        cp = (dx1 - dx2) * (dx1 - dx2) + (dy1 - dy2) * (dy1 - dy2);
                        if (cp < len1 * len1 && cp < len2 * len2)
                        {
                            calc_miter(vc,
                                       v0, v1, v2, dx1, dy1, dx2, dy2,
                                       line_join_e.miter_join_revert,
                                       limit, 0);
                        }
                        else
                        {
                            if (m_inner_join == inner_join_e.inner_jag)
                            {
                                add_vertex(vc, v1.x + dx1, v1.y - dy1);
                                add_vertex(vc, v1.x, v1.y);
                                add_vertex(vc, v1.x + dx2, v1.y - dy2);
                            }
                            else
                            {
                                add_vertex(vc, v1.x + dx1, v1.y - dy1);
                                add_vertex(vc, v1.x, v1.y);
                                calc_arc(vc, v1.x, v1.y, dx2, -dy2, dx1, -dy1);
                                add_vertex(vc, v1.x, v1.y);
                                add_vertex(vc, v1.x + dx2, v1.y - dy2);
                            }
                        }
                        break;
                }
            }
            else
            {
                // Outer join
                //---------------

                // Calculate the distance between v1 and 
                // the central point of the bevel line segment
                //---------------
                double dx = (dx1 + dx2) / 2;
                double dy = (dy1 + dy2) / 2;
                double dbevel = Math.Sqrt(dx * dx + dy * dy);

                if (m_line_join == line_join_e.round_join || m_line_join == line_join_e.bevel_join)
                {
                    // This is an optimization that reduces the number of points 
                    // in cases of almost collinear segments. If there's no
                    // visible difference between bevel and miter joins we'd rather
                    // use miter join because it adds only one point instead of two. 
                    //
                    // Here we calculate the middle point between the bevel points 
                    // and then, the distance between v1 and this middle point. 
                    // At outer joins this distance always less than stroke width, 
                    // because it's actually the height of an isosceles triangle of
                    // v1 and its two bevel points. If the difference between this
                    // width and this value is small (no visible bevel) we can 
                    // add just one point. 
                    //
                    // The constant in the expression makes the result approximately 
                    // the same as in round joins and caps. You can safely comment 
                    // out this entire "if".
                    //-------------------
                    if (m_approx_scale * (m_width_abs - dbevel) < m_width_eps)
                    {
                        if (agg_math.calc_intersection(v0.x + dx1, v0.y - dy1,
                                             v1.x + dx1, v1.y - dy1,
                                             v1.x + dx2, v1.y - dy2,
                                             v2.x + dx2, v2.y - dy2,
                                             out dx, out dy))
                        {
                            add_vertex(vc, dx, dy);
                        }
                        else
                        {
                            add_vertex(vc, v1.x + dx1, v1.y - dy1);
                        }
                        return;
                    }
                }

                switch (m_line_join)
                {
                    case line_join_e.miter_join:
                    case line_join_e.miter_join_revert:
                    case line_join_e.miter_join_round:
                        calc_miter(vc,
                                   v0, v1, v2, dx1, dy1, dx2, dy2,
                                   m_line_join,
                                   m_miter_limit,
                                   dbevel);
                        break;

                    case line_join_e.round_join:
                        calc_arc(vc, v1.x, v1.y, dx1, -dy1, dx2, -dy2);
                        break;

                    default: // Bevel join
                        add_vertex(vc, v1.x + dx1, v1.y - dy1);
                        add_vertex(vc, v1.x + dx2, v1.y - dy2);
                        break;
                }
            }
        }

        private void add_vertex(IVertexDest vc, double x, double y)
        {
            vc.add(new Vector2D(x, y));
        }

        void calc_arc(IVertexDest vc,
                      double x, double y,
                      double dx1, double dy1,
                      double dx2, double dy2)
        {
            double a1 = Math.Atan2(dy1 * m_width_sign, dx1 * m_width_sign);
            double a2 = Math.Atan2(dy2 * m_width_sign, dx2 * m_width_sign);
            double da = a1 - a2;
            int i, n;

            da = Math.Acos(m_width_abs / (m_width_abs + 0.125 / m_approx_scale)) * 2;

            add_vertex(vc, x + dx1, y + dy1);
            if (m_width_sign > 0)
            {
                if (a1 > a2) a2 += 2 * Math.PI;
                n = (int)((a2 - a1) / da);
                da = (a2 - a1) / (n + 1);
                a1 += da;
                for (i = 0; i < n; i++)
                {
                    add_vertex(vc, x + Math.Cos(a1) * m_width, y + Math.Sin(a1) * m_width);
                    a1 += da;
                }
            }
            else
            {
                if (a1 < a2) a2 -= 2 * Math.PI;
                n = (int)((a1 - a2) / da);
                da = (a1 - a2) / (n + 1);
                a1 -= da;
                for (i = 0; i < n; i++)
                {
                    add_vertex(vc, x + Math.Cos(a1) * m_width, y + Math.Sin(a1) * m_width);
                    a1 -= da;
                }
            }
            add_vertex(vc, x + dx2, y + dy2);
        }

        void calc_miter(IVertexDest vc,
                        VertexWithDistance v0,
                        VertexWithDistance v1,
                        VertexWithDistance v2,
                        double dx1, double dy1,
                        double dx2, double dy2,
                        line_join_e lj,
                        double mlimit,
                        double dbevel)
        {
            double xi = v1.x;
            double yi = v1.y;
            double di = 1;
            double lim = m_width_abs * mlimit;
            bool miter_limit_exceeded = true; // Assume the worst
            bool intersection_failed = true; // Assume the worst

            if (agg_math.calc_intersection(v0.x + dx1, v0.y - dy1,
                                 v1.x + dx1, v1.y - dy1,
                                 v1.x + dx2, v1.y - dy2,
                                 v2.x + dx2, v2.y - dy2,
                                 out xi, out yi))
            {
                // Calculation of the intersection succeeded
                //---------------------
                di = agg_math.calc_distance(v1.x, v1.y, xi, yi);
                if (di <= lim)
                {
                    // Inside the miter limit
                    //---------------------
                    add_vertex(vc, xi, yi);
                    miter_limit_exceeded = false;
                }
                intersection_failed = false;
            }
            else
            {
                // Calculation of the intersection failed, most probably
                // the three points lie one straight line. 
                // First check if v0 and v2 lie on the opposite sides of vector: 
                // (v1.x, v1.y) -> (v1.x+dx1, v1.y-dy1), that is, the perpendicular
                // to the line determined by vertices v0 and v1.
                // This condition determines whether the next line segments continues
                // the previous one or goes back.
                //----------------
                double x2 = v1.x + dx1;
                double y2 = v1.y - dy1;
                if ((agg_math.cross_product(v0.x, v0.y, v1.x, v1.y, x2, y2) < 0.0) ==
                   (agg_math.cross_product(v1.x, v1.y, v2.x, v2.y, x2, y2) < 0.0))
                {
                    // This case means that the next segment continues 
                    // the previous one (straight line)
                    //-----------------
                    add_vertex(vc, v1.x + dx1, v1.y - dy1);
                    miter_limit_exceeded = false;
                }
            }

            if (miter_limit_exceeded)
            {
                // Miter limit exceeded
                //------------------------
                switch (lj)
                {
                    case line_join_e.miter_join_revert:
                        // For the compatibility with SVG, PDF, etc, 
                        // we use a simple bevel join instead of
                        // "smart" bevel
                        //-------------------
                        add_vertex(vc, v1.x + dx1, v1.y - dy1);
                        add_vertex(vc, v1.x + dx2, v1.y - dy2);
                        break;

                    case line_join_e.miter_join_round:
                        calc_arc(vc, v1.x, v1.y, dx1, -dy1, dx2, -dy2);
                        break;

                    default:
                        // If no miter-revert, calculate new dx1, dy1, dx2, dy2
                        //----------------
                        if (intersection_failed)
                        {
                            mlimit *= m_width_sign;
                            add_vertex(vc, v1.x + dx1 + dy1 * mlimit,
                                           v1.y - dy1 + dx1 * mlimit);
                            add_vertex(vc, v1.x + dx2 - dy2 * mlimit,
                                           v1.y - dy2 - dx2 * mlimit);
                        }
                        else
                        {
                            double x1 = v1.x + dx1;
                            double y1 = v1.y - dy1;
                            double x2 = v1.x + dx2;
                            double y2 = v1.y - dy2;
                            di = (lim - dbevel) / (di - dbevel);
                            add_vertex(vc, x1 + (xi - x1) * di,
                                           y1 + (yi - y1) * di);
                            add_vertex(vc, x2 + (xi - x2) * di,
                                           y2 + (yi - y2) * di);
                        }
                        break;
                }
            }
        }
    }
}