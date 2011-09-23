


namespace NewTOAPIA.Drawing
{
    using NewTOAPIA.Graphics;

    //using poly_subpixel_scale_e = agg_basics.poly_subpixel_scale_e;

    //--------------------------------------------------------poly_max_coord_e
    enum poly_max_coord_e
    {
        poly_max_coord = (1 << 30) - 1 //----poly_max_coord
    }

    public interface IVectorClipper
    {
        int upscale(double v);
        int downscale(int v);

        void clip_box(int x1, int y1, int x2, int y2);

        void reset_clipping();

        void move_to(int x1, int y1);
        void line_to(rasterizer_cells_aa ras, int x2, int y2);
    }

    //------------------------------------------------------rasterizer_sl_clip
    class VectorClipper_DoClip : IVectorClipper
    {
        int mul_div(double a, double b, double c)
        {
            return agg_basics.iround(a * b / c);
        }
        int xi(int v) { return v; }
        int yi(int v) { return v; }
        public int upscale(double v) { return agg_basics.iround(v * (int)agg_basics.poly_subpixel_scale_e.poly_subpixel_scale); }
        public int downscale(int v) { return v; }

        //--------------------------------------------------------------------
        public VectorClipper_DoClip()
        {
            m_clip_box = new RectangleI(0, 0, 0, 0);
            m_x1 = (0);
            m_y1 = (0);
            m_f1 = (0);
            m_clipping = (false);
        }

        //--------------------------------------------------------------------
        public void reset_clipping()
        {
            m_clipping = false;
        }

        //--------------------------------------------------------------------
        public void clip_box(int x1, int y1, int x2, int y2)
        {
            m_clip_box = new RectangleI(x1, y1, x2, y2);
            m_clipping = true;
        }

        //--------------------------------------------------------------------
        public void move_to(int x1, int y1)
        {
            m_x1 = x1;
            m_y1 = y1;
            if (m_clipping) m_f1 = ClipLiangBarsky.clipping_flags(x1, y1, m_clip_box);
        }

        //------------------------------------------------------------------------
        private void line_clip_y(rasterizer_cells_aa ras,
                                    int x1, int y1,
                                    int x2, int y2,
                                    int f1, int f2)
        {
            f1 &= 10;
            f2 &= 10;
            if ((f1 | f2) == 0)
            {
                // Fully visible
                ras.line(x1, y1, x2, y2);
            }
            else
            {
                if (f1 == f2)
                {
                    // Invisible by Y
                    return;
                }

                int tx1 = x1;
                int ty1 = y1;
                int tx2 = x2;
                int ty2 = y2;

                if ((f1 & 8) != 0) // y1 < clip.y1
                {
                    tx1 = x1 + mul_div(m_clip_box.y1 - y1, x2 - x1, y2 - y1);
                    ty1 = m_clip_box.y1;
                }

                if ((f1 & 2) != 0) // y1 > clip.y2
                {
                    tx1 = x1 + mul_div(m_clip_box.y2 - y1, x2 - x1, y2 - y1);
                    ty1 = m_clip_box.y2;
                }

                if ((f2 & 8) != 0) // y2 < clip.y1
                {
                    tx2 = x1 + mul_div(m_clip_box.y1 - y1, x2 - x1, y2 - y1);
                    ty2 = m_clip_box.y1;
                }

                if ((f2 & 2) != 0) // y2 > clip.y2
                {
                    tx2 = x1 + mul_div(m_clip_box.y2 - y1, x2 - x1, y2 - y1);
                    ty2 = m_clip_box.y2;
                }

                ras.line(tx1, ty1, tx2, ty2);
            }
        }

        //--------------------------------------------------------------------
        public void line_to(rasterizer_cells_aa ras, int x2, int y2)
        {
            if (m_clipping)
            {
                int f2 = ClipLiangBarsky.clipping_flags(x2, y2, m_clip_box);

                if ((m_f1 & 10) == (f2 & 10) && (m_f1 & 10) != 0)
                {
                    // Invisible by Y
                    m_x1 = x2;
                    m_y1 = y2;
                    m_f1 = f2;
                    return;
                }

                int x1 = m_x1;
                int y1 = m_y1;
                int f1 = m_f1;
                int y3, y4;
                int f3, f4;

                switch (((f1 & 5) << 1) | (f2 & 5))
                {
                    case 0: // Visible by X
                        line_clip_y(ras, x1, y1, x2, y2, f1, f2);
                        break;

                    case 1: // x2 > clip.x2
                        y3 = y1 + mul_div(m_clip_box.x2 - x1, y2 - y1, x2 - x1);
                        f3 = ClipLiangBarsky.clipping_flags_y(y3, m_clip_box);
                        line_clip_y(ras, x1, y1, m_clip_box.x2, y3, f1, f3);
                        line_clip_y(ras, m_clip_box.x2, y3, m_clip_box.x2, y2, f3, f2);
                        break;

                    case 2: // x1 > clip.x2
                        y3 = y1 + mul_div(m_clip_box.x2 - x1, y2 - y1, x2 - x1);
                        f3 = ClipLiangBarsky.clipping_flags_y(y3, m_clip_box);
                        line_clip_y(ras, m_clip_box.x2, y1, m_clip_box.x2, y3, f1, f3);
                        line_clip_y(ras, m_clip_box.x2, y3, x2, y2, f3, f2);
                        break;

                    case 3: // x1 > clip.x2 && x2 > clip.x2
                        line_clip_y(ras, m_clip_box.x2, y1, m_clip_box.x2, y2, f1, f2);
                        break;

                    case 4: // x2 < clip.x1
                        y3 = y1 + mul_div(m_clip_box.x1 - x1, y2 - y1, x2 - x1);
                        f3 = ClipLiangBarsky.clipping_flags_y(y3, m_clip_box);
                        line_clip_y(ras, x1, y1, m_clip_box.x1, y3, f1, f3);
                        line_clip_y(ras, m_clip_box.x1, y3, m_clip_box.x1, y2, f3, f2);
                        break;

                    case 6: // x1 > clip.x2 && x2 < clip.x1
                        y3 = y1 + mul_div(m_clip_box.x2 - x1, y2 - y1, x2 - x1);
                        y4 = y1 + mul_div(m_clip_box.x1 - x1, y2 - y1, x2 - x1);
                        f3 = ClipLiangBarsky.clipping_flags_y(y3, m_clip_box);
                        f4 = ClipLiangBarsky.clipping_flags_y(y4, m_clip_box);
                        line_clip_y(ras, m_clip_box.x2, y1, m_clip_box.x2, y3, f1, f3);
                        line_clip_y(ras, m_clip_box.x2, y3, m_clip_box.x1, y4, f3, f4);
                        line_clip_y(ras, m_clip_box.x1, y4, m_clip_box.x1, y2, f4, f2);
                        break;

                    case 8: // x1 < clip.x1
                        y3 = y1 + mul_div(m_clip_box.x1 - x1, y2 - y1, x2 - x1);
                        f3 = ClipLiangBarsky.clipping_flags_y(y3, m_clip_box);
                        line_clip_y(ras, m_clip_box.x1, y1, m_clip_box.x1, y3, f1, f3);
                        line_clip_y(ras, m_clip_box.x1, y3, x2, y2, f3, f2);
                        break;

                    case 9:  // x1 < clip.x1 && x2 > clip.x2
                        y3 = y1 + mul_div(m_clip_box.x1 - x1, y2 - y1, x2 - x1);
                        y4 = y1 + mul_div(m_clip_box.x2 - x1, y2 - y1, x2 - x1);
                        f3 = ClipLiangBarsky.clipping_flags_y(y3, m_clip_box);
                        f4 = ClipLiangBarsky.clipping_flags_y(y4, m_clip_box);
                        line_clip_y(ras, m_clip_box.x1, y1, m_clip_box.x1, y3, f1, f3);
                        line_clip_y(ras, m_clip_box.x1, y3, m_clip_box.x2, y4, f3, f4);
                        line_clip_y(ras, m_clip_box.x2, y4, m_clip_box.x2, y2, f4, f2);
                        break;

                    case 12: // x1 < clip.x1 && x2 < clip.x1
                        line_clip_y(ras, m_clip_box.x1, y1, m_clip_box.x1, y2, f1, f2);
                        break;
                }
                m_f1 = f2;
            }
            else
            {
                ras.line(m_x1, m_y1,
                         x2, y2);
            }
            m_x1 = x2;
            m_y1 = y2;
        }

        private RectangleI m_clip_box;
        private int m_x1;
        private int m_y1;
        private int m_f1;
        private bool m_clipping;
    }

    //---------------------------------------------------rasterizer_sl_no_clip
    class VectorClipper_NoClip : IVectorClipper
    {
        public VectorClipper_NoClip()
        {

        }

        public int upscale(double v) { return agg_basics.iround(v * (int)agg_basics.poly_subpixel_scale_e.poly_subpixel_scale); }
        public int downscale(int v) { return v; }

        public void reset_clipping() { }
        public void clip_box(int x1, int y1, int x2, int y2) { }
        public void move_to(int x1, int y1) { m_x1 = x1; m_y1 = y1; }

        public void line_to(rasterizer_cells_aa ras, int x2, int y2)
        {
            ras.line(m_x1, m_y1, x2, y2);
            m_x1 = x2;
            m_y1 = y2;
        }

        private int m_x1;
        private int m_y1;
    }
}