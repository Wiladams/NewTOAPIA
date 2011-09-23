﻿

namespace NewTOAPIA.Drawing
{
    using System;

    using NewTOAPIA.Graphics;

    //using filling_rule_e = agg_basics.filling_rule_e;
    using status = rasterizer_scanline_aa.status;
    //using poly_subpixel_scale_e = agg_basics.poly_subpixel_scale_e;

    //==================================================rasterizer_scanline_aa
    // Polygon rasterizer that is used to render filled polygons with 
    // high-quality Anti-Aliasing. Internally, by default, the class uses 
    // integer coordinates in format 24.8, i.e. 24 bits for integer part 
    // and 8 bits for fractional - see poly_subpixel_shift. This class can be 
    // used in the following  way:
    //
    // 1. filling_rule(filling_rule_e ft) - optional.
    //
    // 2. gamma() - optional.
    //
    // 3. reset()
    //
    // 4. move_to(x, y) / line_to(x, y) - make the polygon. One can create 
    //    more than one contour, but each contour must consist of at least 3
    //    vertices, i.e. move_to(x1, y1); line_to(x2, y2); line_to(x3, y3);
    //    is the absolute minimum of vertices that define a triangle.
    //    The algorithm does not check either the number of vertices nor
    //    coincidence of their coordinates, but in the worst case it just 
    //    won't draw anything.
    //    The orger of the vertices (clockwise or counterclockwise) 
    //    is important when using the non-zero filling rule (fill_non_zero).
    //    In this case the vertex order of all the contours must be the same
    //    if you want your intersecting polygons to be without "holes".
    //    You actually can use different vertices order. If the contours do not 
    //    intersect each other the order is not important anyway. If they do, 
    //    contours with the same vertex order will be rendered without "holes" 
    //    while the intersecting contours with different orders will have "holes".
    //
    // filling_rule() and gamma() can be called anytime before "sweeping".
    //------------------------------------------------------------------------
    public interface IRasterizer
    {
        int min_x();
        int min_y();
        int max_x();
        int max_y();

        void gamma(IGammaFunction gamma_function);

        bool sweep_scanline(IScanlineCache sl);
        void reset();
        void add_path(IVertexSource vs);
        void add_path(IVertexSource vs, int path_id);
        bool rewind_scanlines();
    }

    public sealed class rasterizer_scanline_aa : IRasterizer
    {
        private rasterizer_cells_aa m_outline;
        private IVectorClipper m_VectorClipper;
        private int[] m_gamma = new int[(int)aa_scale_e.aa_scale];
        private agg_basics.filling_rule_e m_filling_rule;
        private bool m_auto_close;
        private int m_start_x;
        private int m_start_y;
        private status m_status;
        private int m_scan_y;

        public enum status
        {
            status_initial,
            status_move_to,
            status_line_to,
            status_closed
        };

        public enum aa_scale_e
        {
            aa_shift = 8,
            aa_scale = 1 << aa_shift,
            aa_mask = aa_scale - 1,
            aa_scale2 = aa_scale * 2,
            aa_mask2 = aa_scale2 - 1
        };

        public rasterizer_scanline_aa()
            : this(new VectorClipper_DoClip())
        {
        }

        //--------------------------------------------------------------------
        public rasterizer_scanline_aa(IVectorClipper rasterizer_sl_clip)
        {
            m_outline = new rasterizer_cells_aa();
            m_VectorClipper = rasterizer_sl_clip;
            m_filling_rule = agg_basics.filling_rule_e.fill_non_zero;
            m_auto_close = true;
            m_start_x = 0;
            m_start_y = 0;
            m_status = status.status_initial;

            int i;
            for (i = 0; i < (int)aa_scale_e.aa_scale; i++) m_gamma[i] = i;
        }

        /*
        //--------------------------------------------------------------------
        public rasterizer_scanline_aa(IClipper rasterizer_sl_clip, IGammaFunction gamma_function)
        {
            m_outline = new rasterizer_cells_aa();
            m_clipper = rasterizer_sl_clip;
            m_filling_rule = filling_rule_e.fill_non_zero;
            m_auto_close = true;
            m_start_x = 0;
            m_start_y = 0;
            m_status = status.status_initial;

            gamma(gamma_function);
        }*/

        //--------------------------------------------------------------------
        public void reset()
        {
            m_outline.reset();
            m_status = status.status_initial;
        }

        public void reset_clipping()
        {
            reset();
            m_VectorClipper.reset_clipping();
        }

        public void SetVectorClipBox(RectangleD clippingRect)
        {
            SetVectorClipBox(clippingRect.x1, clippingRect.y1, clippingRect.x2, clippingRect.y2);
        }

        public void SetVectorClipBox(double x1, double y1, double x2, double y2)
        {
            reset();
            m_VectorClipper.clip_box(m_VectorClipper.upscale(x1), m_VectorClipper.upscale(y1),
                               m_VectorClipper.upscale(x2), m_VectorClipper.upscale(y2));
        }

        public void filling_rule(agg_basics.filling_rule_e filling_rule)
        {
            m_filling_rule = filling_rule;
        }

        public void auto_close(bool flag) { m_auto_close = flag; }

        //--------------------------------------------------------------------
        public void gamma(IGammaFunction gamma_function)
        {
            for (int i = 0; i < (int)aa_scale_e.aa_scale; i++)
            {
                m_gamma[i] = (int)agg_basics.uround(gamma_function.GetGamma((double)(i) / (int)aa_scale_e.aa_mask) * (int)aa_scale_e.aa_mask);
            }
        }

        /*
        //--------------------------------------------------------------------
        public int apply_gamma(int cover) 
        { 
        	return (int)m_gamma[cover];
        }
         */

        //--------------------------------------------------------------------
        void move_to(int x, int y)
        {
            if (m_outline.sorted()) reset();
            if (m_auto_close) close_polygon();
            m_VectorClipper.move_to(m_start_x = m_VectorClipper.downscale(x),
                              m_start_y = m_VectorClipper.downscale(y));
            m_status = status.status_move_to;
        }

        //------------------------------------------------------------------------
        void line_to(int x, int y)
        {
            m_VectorClipper.line_to(m_outline,
                              m_VectorClipper.downscale(x),
                              m_VectorClipper.downscale(y));
            m_status = status.status_line_to;
        }

        //------------------------------------------------------------------------
        public void move_to_d(double x, double y)
        {
            if (m_outline.sorted()) reset();
            if (m_auto_close) close_polygon();
            m_VectorClipper.move_to(m_start_x = m_VectorClipper.upscale(x),
                              m_start_y = m_VectorClipper.upscale(y));
            m_status = status.status_move_to;
        }

        //------------------------------------------------------------------------
        public void line_to_d(double x, double y)
        {
            m_VectorClipper.line_to(m_outline,
                              m_VectorClipper.upscale(x),
                              m_VectorClipper.upscale(y));
            m_status = status.status_line_to;
            //DebugFile.Print("x=" + x.ToString() + " y=" + y.ToString() + "\n");
        }

        public void close_polygon()
        {
            if (m_status == status.status_line_to)
            {
                m_VectorClipper.line_to(m_outline, m_start_x, m_start_y);
                m_status = status.status_closed;
            }
        }

        void add_vertex(double x, double y, Path.FlagsAndCommand PathAndFlags)
        {
            if (Path.is_move_to(PathAndFlags))
            {
                move_to_d(x, y);
            }
            else
            {
                if (Path.is_vertex(PathAndFlags))
                {
                    line_to_d(x, y);
                }
                else
                {
                    if (Path.is_close(PathAndFlags))
                    {
                        close_polygon();
                    }
                }
            }
        }
        //------------------------------------------------------------------------
        void edge(int x1, int y1, int x2, int y2)
        {
            if (m_outline.sorted()) reset();
            m_VectorClipper.move_to(m_VectorClipper.downscale(x1), m_VectorClipper.downscale(y1));
            m_VectorClipper.line_to(m_outline,
                              m_VectorClipper.downscale(x2),
                              m_VectorClipper.downscale(y2));
            m_status = status.status_move_to;
        }

        //------------------------------------------------------------------------
        void edge_d(double x1, double y1, double x2, double y2)
        {
            if (m_outline.sorted()) reset();
            m_VectorClipper.move_to(m_VectorClipper.upscale(x1), m_VectorClipper.upscale(y1));
            m_VectorClipper.line_to(m_outline,
                              m_VectorClipper.upscale(x2),
                              m_VectorClipper.upscale(y2));
            m_status = status.status_move_to;
        }

        //-------------------------------------------------------------------
        public void add_path(IVertexSource vs)
        {
            add_path(vs, 0);
        }

        public void add_path(IVertexSource vs, int path_id)
        {
            double x = 0;
            double y = 0;

            Path.FlagsAndCommand PathAndFlags;
            vs.rewind(path_id);
            if (m_outline.sorted())
            {
                reset();
            }

            while (!Path.is_stop(PathAndFlags = vs.vertex(out x, out y)))
            {
                add_vertex(x, y, PathAndFlags);
            }

            //DebugFile.Print("Test");
        }

        //--------------------------------------------------------------------
        public int min_x() { return m_outline.min_x(); }
        public int min_y() { return m_outline.min_y(); }
        public int max_x() { return m_outline.max_x(); }
        public int max_y() { return m_outline.max_y(); }

        //--------------------------------------------------------------------
        void sort()
        {
            if (m_auto_close) close_polygon();
            m_outline.sort_cells();
        }

        //------------------------------------------------------------------------
        public bool rewind_scanlines()
        {
            if (m_auto_close) close_polygon();
            m_outline.sort_cells();
            if (m_outline.total_cells() == 0)
            {
                return false;
            }
            m_scan_y = m_outline.min_y();
            return true;
        }

        //------------------------------------------------------------------------
        bool navigate_scanline(int y)
        {
            if (m_auto_close) close_polygon();
            m_outline.sort_cells();
            if (m_outline.total_cells() == 0 ||
               y < m_outline.min_y() ||
               y > m_outline.max_y())
            {
                return false;
            }
            m_scan_y = y;
            return true;
        }

        //--------------------------------------------------------------------
        public int calculate_alpha(int area)
        {
            int cover = area >> ((int)agg_basics.poly_subpixel_scale_e.poly_subpixel_shift * 2 + 1 - (int)aa_scale_e.aa_shift);

            if (cover < 0) cover = -cover;
            if (m_filling_rule == agg_basics.filling_rule_e.fill_even_odd)
            {
                cover &= (int)aa_scale_e.aa_mask2;
                if (cover > (int)aa_scale_e.aa_scale)
                {
                    cover = (int)aa_scale_e.aa_scale2 - cover;
                }
            }
            if (cover > (int)aa_scale_e.aa_mask) cover = (int)aa_scale_e.aa_mask;
            return (int)m_gamma[cover];
        }

#if use_timers
        static CNamedTimer SweepSacanLine = new CNamedTimer("SweepSacanLine");
#endif
        //--------------------------------------------------------------------
        public bool sweep_scanline(IScanlineCache sl)
        {
#if use_timers
            SweepSacanLine.Start();
#endif
            for (; ; )
            {
                if (m_scan_y > m_outline.max_y())
                {
#if use_timers
                    SweepSacanLine.Stop();
#endif
                    return false;
                }

                sl.reset_spans();
                int num_cells = (int)m_outline.scanline_num_cells(m_scan_y);
                cell_aa[] cells;
                int Offset;
                m_outline.scanline_cells(m_scan_y, out cells, out Offset);
                int cover = 0;

                while (num_cells != 0)
                {
                    cell_aa cur_cell = cells[Offset];
                    int x = cur_cell.x;
                    int area = cur_cell.area;
                    int alpha;

                    cover += cur_cell.cover;

                    //accumulate all cells with the same X
                    while (--num_cells != 0)
                    {
                        Offset++;
                        cur_cell = cells[Offset];
                        if (cur_cell.x != x)
                        {
                            break;
                        }

                        area += cur_cell.area;
                        cover += cur_cell.cover;
                    }

                    if (area != 0)
                    {
                        alpha = calculate_alpha((cover << ((int)agg_basics.poly_subpixel_scale_e.poly_subpixel_shift + 1)) - area);
                        if (alpha != 0)
                        {
                            sl.add_cell(x, alpha);
                        }
                        x++;
                    }

                    if ((num_cells != 0) && (cur_cell.x > x))
                    {
                        alpha = calculate_alpha(cover << ((int)agg_basics.poly_subpixel_scale_e.poly_subpixel_shift + 1));
                        if (alpha != 0)
                        {
                            sl.add_span(x, (cur_cell.x - x), alpha);
                        }
                    }
                }

                if (sl.num_spans() != 0) break;
                ++m_scan_y;
            }

            sl.finalize(m_scan_y);
            ++m_scan_y;
#if use_timers
            SweepSacanLine.Stop();
#endif
            return true;
        }

        //--------------------------------------------------------------------
        bool hit_test(int tx, int ty)
        {
            if (!navigate_scanline(ty)) return false;
            //scanline_hit_test sl(tx);
            //sweep_scanline(sl);
            //return sl.hit();
            return true;
        }
    };
}
