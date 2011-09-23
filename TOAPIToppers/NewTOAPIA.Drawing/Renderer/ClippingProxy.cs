
namespace NewTOAPIA.Drawing
{
    using System;

    using NewTOAPIA.Graphics;

    //-----------------------------------------------------------ClippingPixelFormtProxy
    public class ImageClippingProxy : ImageProxy
    {
        private RectangleI m_clip_box;

        public const byte cover_full = 255;

        public ImageClippingProxy(IImage ren)
            : base(ren)
        {
            m_clip_box = new RectangleI(0, 0, (int)ren.Width() - 1, (int)ren.Height() - 1);
        }

        public override void LinkToImage(IImage ren)
        {
            base.LinkToImage(ren);
            m_clip_box = new RectangleI(0, 0, (int)ren.Width() - 1, (int)ren.Height() - 1);
        }

        //public IPixelFormat ren() { return m_ren; }

        public bool SetClippingBox(int x1, int y1, int x2, int y2)
        {
            RectangleI cb = new RectangleI(x1, y1, x2, y2);
            if (cb.Clip(new RectangleI(0, 0, (int)Width() - 1, (int)Height() - 1)))
            {
                m_clip_box = cb;
                return true;
            }
            m_clip_box.x1 = 1;
            m_clip_box.y1 = 1;
            m_clip_box.x2 = 0;
            m_clip_box.y2 = 0;

            return false;
        }

        public void reset_clipping(bool visibility)
        {
            if (visibility)
            {
                m_clip_box.x1 = 0;
                m_clip_box.y1 = 0;
                m_clip_box.x2 = (int)Width() - 1;
                m_clip_box.y2 = (int)Height() - 1;
            }
            else
            {
                m_clip_box.x1 = 1;
                m_clip_box.y1 = 1;
                m_clip_box.x2 = 0;
                m_clip_box.y2 = 0;
            }
        }

        public void clip_box_naked(int x1, int y1, int x2, int y2)
        {
            m_clip_box.x1 = x1;
            m_clip_box.y1 = y1;
            m_clip_box.x2 = x2;
            m_clip_box.y2 = y2;
        }

        public bool inbox(int x, int y)
        {
            return x >= m_clip_box.x1 && y >= m_clip_box.y1 &&
                   x <= m_clip_box.x2 && y <= m_clip_box.y2;
        }

        public RectangleI clip_box() { return m_clip_box; }
        int xmin() { return m_clip_box.x1; }
        int ymin() { return m_clip_box.y1; }
        int xmax() { return m_clip_box.x2; }
        int ymax() { return m_clip_box.y2; }

        public RectangleI bounding_clip_box() { return m_clip_box; }
        public int bounding_xmin() { return m_clip_box.x1; }
        public int bounding_ymin() { return m_clip_box.y1; }
        public int bounding_xmax() { return m_clip_box.x2; }
        public int bounding_ymax() { return m_clip_box.y2; }

        public void clear(IColorType in_c)
        {
            int y;
            RGBA_Bytes c = new RGBA_Bytes(in_c.R_Byte, in_c.G_Byte, in_c.B_Byte, in_c.A_Byte);
            if (Width() != 0)
            {
                for (y = 0; y < Height(); y++)
                {
                    base.copy_hline(0, (int)y, (int)Width(), c);
                }
            }
        }

        public override void copy_pixel(int x, int y, byte[] c, int ByteOffset)
        {
            if (inbox(x, y))
            {
                base.copy_pixel(x, y, c, ByteOffset);
            }
        }

        public override RGBA_Bytes pixel(int x, int y)
        {
            return inbox(x, y) ? base.pixel(x, y) : new RGBA_Bytes();
        }

        public override void copy_hline(int x1, int y, int x2, RGBA_Bytes c)
        {
            if (x1 > x2) { int t = (int)x2; x2 = (int)x1; x1 = t; }
            if (y > ymax()) return;
            if (y < ymin()) return;
            if (x1 > xmax()) return;
            if (x2 < xmin()) return;

            if (x1 < xmin()) x1 = xmin();
            if (x2 > xmax()) x2 = (int)xmax();

            base.copy_hline(x1, y, (int)(x2 - x1 + 1), c);
        }

        public override void copy_vline(int x, int y1, int y2, RGBA_Bytes c)
        {
            if (y1 > y2) { int t = (int)y2; y2 = (int)y1; y1 = t; }
            if (x > xmax()) return;
            if (x < xmin()) return;
            if (y1 > ymax()) return;
            if (y2 < ymin()) return;

            if (y1 < ymin()) y1 = ymin();
            if (y2 > ymax()) y2 = (int)ymax();

            base.copy_vline(x, y1, (int)(y2 - y1 + 1), c);
        }

        public override void blend_hline(int x1, int y, int x2, RGBA_Bytes c, byte cover)
        {
            if (x1 > x2)
            {
                int t = (int)x2;
                x2 = x1;
                x1 = t;
            }
            if (y > ymax())
                return;
            if (y < ymin())
                return;
            if (x1 > xmax())
                return;
            if (x2 < xmin())
                return;

            if (x1 < xmin())
                x1 = xmin();
            if (x2 > xmax())
                x2 = xmax();

            base.blend_hline(x1, y, x2, c, cover);
        }

        public override void blend_vline(int x, int y1, int y2, RGBA_Bytes c, byte cover)
        {
            if (y1 > y2) { int t = y2; y2 = y1; y1 = t; }
            if (x > xmax()) return;
            if (x < xmin()) return;
            if (y1 > ymax()) return;
            if (y2 < ymin()) return;

            if (y1 < ymin()) y1 = ymin();
            if (y2 > ymax()) y2 = ymax();

            base.blend_vline(x, y1, y2, c, cover);
        }

        /*
        public void copy_bar(int x1, int y1, int x2, int y2, IColorType c)
        {
            rect_i rc(x1, y1, x2, y2);
            rc.normalize();
            if(rc.clip(clip_box()))
            {
                int y;
                for(y = rc.y1; y <= rc.y2; y++)
                {
                    m_ren->copy_hline(rc.x1, y, int(rc.x2 - rc.x1 + 1), c);
                }
            }
        }

        public void blend_bar(int x1, int y1, int x2, int y2, 
                       IColorType c, byte cover)
        {
            rect_i rc(x1, y1, x2, y2);
            rc.normalize();
            if(rc.clip(clip_box()))
            {
                int y;
                for(y = rc.y1; y <= rc.y2; y++)
                {
                    m_ren->blend_hline(rc.x1,
                                       y,
                                       int(rc.x2 - rc.x1 + 1), 
                                       c, 
                                       cover);
                }
            }
        }
         */

        public override void blend_solid_hspan(int x, int y, int in_len, RGBA_Bytes c, byte[] covers, int coversIndex)
        {
            int len = (int)in_len;
            if (y > ymax()) return;
            if (y < ymin()) return;

            if (x < xmin())
            {
                len -= xmin() - x;
                if (len <= 0) return;
                coversIndex += xmin() - x;
                x = xmin();
            }
            if (x + len > xmax())
            {
                len = xmax() - x + 1;
                if (len <= 0) return;
            }
            base.blend_solid_hspan(x, y, len, c, covers, coversIndex);
        }

        public override void blend_solid_vspan(int x, int y, int len, RGBA_Bytes c, byte[] covers, int coversIndex)
        {
            if (x > xmax()) return;
            if (x < xmin()) return;

            if (y < ymin())
            {
                len -= (ymin() - y);
                if (len <= 0) return;
                coversIndex += ymin() - y;
                y = ymin();
            }
            if (y + len > ymax())
            {
                len = (ymax() - y + 1);
                if (len <= 0) return;
            }
            base.blend_solid_vspan(x, y, len, c, covers, coversIndex);
        }

        public override void copy_color_hspan(int x, int y, int len, RGBA_Bytes[] colors, int colorsIndex)
        {
            if (y > ymax()) return;
            if (y < ymin()) return;

            if (x < xmin())
            {
                int d = xmin() - x;
                len -= d;
                if (len <= 0) return;
                colorsIndex += d;
                x = xmin();
            }
            if (x + len > xmax())
            {
                len = (xmax() - x + 1);
                if (len <= 0) return;
            }
            base.copy_color_hspan(x, y, len, colors, colorsIndex);
        }

        public override void copy_color_vspan(int x, int y, int len, RGBA_Bytes[] colors, int colorsIndex)
        {
            if (x > xmax()) return;
            if (x < xmin()) return;

            if (y < ymin())
            {
                int d = ymin() - y;
                len -= d;
                if (len <= 0) return;
                colorsIndex += d;
                y = ymin();
            }
            if (y + len > ymax())
            {
                len = (ymax() - y + 1);
                if (len <= 0) return;
            }
            base.copy_color_vspan(x, y, len, colors, colorsIndex);
        }

        public override void blend_color_hspan(int x, int y, int in_len, RGBA_Bytes[] colors, int colorsIndex, byte[] covers, int coversIndex, bool firstCoverForAll)
        {
            int len = (int)in_len;
            if (y > ymax())
                return;
            if (y < ymin())
                return;

            if (x < xmin())
            {
                int d = xmin() - x;
                len -= d;
                if (len <= 0) return;
                if (covers != null) coversIndex += d;
                colorsIndex += d;
                x = xmin();
            }
            if (x + len - 1 > xmax())
            {
                len = xmax() - x + 1;
                if (len <= 0) return;
            }

            base.blend_color_hspan(x, y, len, colors, colorsIndex, covers, coversIndex, firstCoverForAll);
        }

        public void copy_from(IImage src)
        {
            copy_from(src, new RectangleI(0, 0, (int)src.Width(), (int)src.Height()), 0, 0);
        }

        /*
        public override void CopyFrom(IImage from, int xdst, int ydst, int xsrc, int ysrc, int len)
        {
            throw new System.NotImplementedException();
        }
         */

        public override void SetPixelFromColor(byte[] p, IColorType c)
        {
            throw new System.NotImplementedException();
        }

        public void copy_from(IImage src,
                       RectangleI rect_src_ptr,
                       int dx,
                       int dy)
        {
            RectangleI rsrc = new RectangleI(rect_src_ptr.x1, rect_src_ptr.y1, rect_src_ptr.x2 + 1, rect_src_ptr.y2 + 1);

            // Version with xdst, ydst (absolute positioning)
            //rect_i rdst(xdst, ydst, xdst + rsrc.x2 - rsrc.x1, ydst + rsrc.y2 - rsrc.y1);

            // Version with dx, dy (relative positioning)
            RectangleI rdst = new RectangleI(rsrc.x1 + dx, rsrc.y1 + dy, rsrc.x2 + dx, rsrc.y2 + dy);

            RectangleI rc = clip_rect_area(ref rdst, ref rsrc, (int)src.Width(), (int)src.Height());

            if (rc.x2 > 0)
            {
                int incy = 1;
                if (rdst.y1 > rsrc.y1)
                {
                    rsrc.y1 += rc.y2 - 1;
                    rdst.y1 += rc.y2 - 1;
                    incy = -1;
                }
                int getDistanceBetweenPixelsInclusive = src.GetDistanceBetweenPixelsInclusive();
                while (rc.y2 > 0)
                {
                    base.CopyFrom(src,
                                     rdst.x1, rdst.y1,
                                     rsrc.x1, rsrc.y1,
                                     rc.x2 * getDistanceBetweenPixelsInclusive);
                    rdst.y1 += incy;
                    rsrc.y1 += incy;
                    --rc.y2;
                }
            }
        }

        public RectangleI clip_rect_area(ref RectangleI dst, ref RectangleI src, int wsrc, int hsrc)
        {
            RectangleI rc = new RectangleI(0, 0, 0, 0);
            RectangleI cb = clip_box();
            ++cb.x2;
            ++cb.y2;

            if (src.x1 < 0)
            {
                dst.x1 -= src.x1;
                src.x1 = 0;
            }
            if (src.y1 < 0)
            {
                dst.y1 -= src.y1;
                src.y1 = 0;
            }

            if (src.x2 > wsrc) src.x2 = wsrc;
            if (src.y2 > hsrc) src.y2 = hsrc;

            if (dst.x1 < cb.x1)
            {
                src.x1 += cb.x1 - dst.x1;
                dst.x1 = cb.x1;
            }
            if (dst.y1 < cb.y1)
            {
                src.y1 += cb.y1 - dst.y1;
                dst.y1 = cb.y1;
            }

            if (dst.x2 > cb.x2) dst.x2 = cb.x2;
            if (dst.y2 > cb.y2) dst.y2 = cb.y2;

            rc.x2 = dst.x2 - dst.x1;
            rc.y2 = dst.y2 - dst.y1;

            if (rc.x2 > src.x2 - src.x1) rc.x2 = src.x2 - src.x1;
            if (rc.y2 > src.y2 - src.y1) rc.y2 = src.y2 - src.y1;
            return rc;
        }

        public override void blend_color_vspan(int x, int y, int len, RGBA_Bytes[] colors, int colorsIndex, byte[] covers, int coversIndex, bool firstCoverForAll)
        {
            if (x > xmax()) return;
            if (x < xmin()) return;

            if (y < ymin())
            {
                int d = ymin() - y;
                len -= d;
                if (len <= 0) return;
                if (covers != null) coversIndex += d;
                colorsIndex += d;
                y = ymin();
            }
            if (y + len > ymax())
            {
                len = (ymax() - y + 1);
                if (len <= 0) return;
            }
            base.blend_color_vspan(x, y, len, colors, colorsIndex, covers, coversIndex, firstCoverForAll);
        }

        /*
        //template<class SrcPixelFormatRenderer>
        public void blend_from(rendering_buffer src)
        {
            blend_from(src, 0, 0, 0, agg::cover_full)

        }

        public void blend_from(rendering_buffer src, 
                        ref rect_i rect_src_ptr, 
                        int dx, 
                        int dy,
                        byte cover)
        {
            rect_i rsrc(0, 0, src.width(), src.height());
            if(rect_src_ptr)
            {
                rsrc.x1 = rect_src_ptr->x1; 
                rsrc.y1 = rect_src_ptr->y1;
                rsrc.x2 = rect_src_ptr->x2 + 1;
                rsrc.y2 = rect_src_ptr->y2 + 1;
            }

            // Version with xdst, ydst (absolute positioning)
            //rect_i rdst(xdst, ydst, xdst + rsrc.x2 - rsrc.x1, ydst + rsrc.y2 - rsrc.y1);

            // Version with dx, dy (relative positioning)
            rect_i rdst(rsrc.x1 + dx, rsrc.y1 + dy, rsrc.x2 + dx, rsrc.y2 + dy);
            rect_i rc = clip_rect_area(rdst, rsrc, src.width(), src.height());

            if(rc.x2 > 0)
            {
                int incy = 1;
                if(rdst.y1 > rsrc.y1)
                {
                    rsrc.y1 += rc.y2 - 1;
                    rdst.y1 += rc.y2 - 1;
                    incy = -1;
                }
                while(rc.y2 > 0)
                {
                    typename SrcPixelFormatRenderer::row_data rw = src.row(rsrc.y1);
                    if(rw.ptr)
                    {
                        int x1src = rsrc.x1;
                        int x1dst = rdst.x1;
                        int len   = rc.x2;
                        if(rw.x1 > x1src)
                        {
                            x1dst += rw.x1 - x1src;
                            len   -= rw.x1 - x1src;
                            x1src  = rw.x1;
                        }
                        if(len > 0)
                        {
                            if(x1src + len-1 > rw.x2)
                            {
                                len -= x1src + len - rw.x2 - 1;
                            }
                            if(len > 0)
                            {
                                m_ren->blend_from(src,
                                                  x1dst, rdst.y1,
                                                  x1src, rsrc.y1,
                                                  len,
                                                  cover);
                            }
                        }
                    }
                    rdst.y1 += incy;
                    rsrc.y1 += incy;
                    --rc.y2;
                }
            }
        }

        //template<class SrcPixelFormatRenderer>
        public void blend_from_color(rendering_buffer src, 
                              IColorType color)
        {
            blend_from_color(src, color, h0, 0, 0, agg::cover_full)
        }
        public void blend_from_color(rendering_buffer src, 
                              IColorType color,
                              ref rect_i rect_src_ptr, 
                              int dx, 
                              int dy,
                              byte cover)
        {
            rect_i rsrc(0, 0, src.width(), src.height());
            if(rect_src_ptr)
            {
                rsrc.x1 = rect_src_ptr->x1; 
                rsrc.y1 = rect_src_ptr->y1;
                rsrc.x2 = rect_src_ptr->x2 + 1;
                rsrc.y2 = rect_src_ptr->y2 + 1;
            }

            // Version with xdst, ydst (absolute positioning)
            //rect_i rdst(xdst, ydst, xdst + rsrc.x2 - rsrc.x1, ydst + rsrc.y2 - rsrc.y1);

            // Version with dx, dy (relative positioning)
            rect_i rdst(rsrc.x1 + dx, rsrc.y1 + dy, rsrc.x2 + dx, rsrc.y2 + dy);
            rect_i rc = clip_rect_area(rdst, rsrc, src.width(), src.height());

            if(rc.x2 > 0)
            {
                int incy = 1;
                if(rdst.y1 > rsrc.y1)
                {
                    rsrc.y1 += rc.y2 - 1;
                    rdst.y1 += rc.y2 - 1;
                    incy = -1;
                }
                while(rc.y2 > 0)
                {
                    typename SrcPixelFormatRenderer::row_data rw = src.row(rsrc.y1);
                    if(rw.ptr)
                    {
                        int x1src = rsrc.x1;
                        int x1dst = rdst.x1;
                        int len   = rc.x2;
                        if(rw.x1 > x1src)
                        {
                            x1dst += rw.x1 - x1src;
                            len   -= rw.x1 - x1src;
                            x1src  = rw.x1;
                        }
                        if(len > 0)
                        {
                            if(x1src + len-1 > rw.x2)
                            {
                                len -= x1src + len - rw.x2 - 1;
                            }
                            if(len > 0)
                            {
                                m_ren->blend_from_color(src,
                                                        color,
                                                        x1dst, rdst.y1,
                                                        x1src, rsrc.y1,
                                                        len,
                                                        cover);
                            }
                        }
                    }
                    rdst.y1 += incy;
                    rsrc.y1 += incy;
                    --rc.y2;
                }
            }
        }

    /*
        //template<class SrcPixelFormatRenderer>
        public void blend_from_lut(rendering_buffer src, IColorType color_lut)
        {
            blend_from_lut(rendering_buffer src, IColorType color_lut, 0, 0, 0, agg::cover_full);
        }
        public void blend_from_lut(rendering_buffer src, 
                            IColorType color_lut,
                            ref rect_i rect_src_ptr, 
                            int dx, 
                            int dy,
                            byte cover)
        {
            rect_i rsrc(0, 0, src.width(), src.height());
            if(rect_src_ptr)
            {
                rsrc.x1 = rect_src_ptr->x1; 
                rsrc.y1 = rect_src_ptr->y1;
                rsrc.x2 = rect_src_ptr->x2 + 1;
                rsrc.y2 = rect_src_ptr->y2 + 1;
            }

            // Version with xdst, ydst (absolute positioning)
            //rect_i rdst(xdst, ydst, xdst + rsrc.x2 - rsrc.x1, ydst + rsrc.y2 - rsrc.y1);

            // Version with dx, dy (relative positioning)
            rect_i rdst(rsrc.x1 + dx, rsrc.y1 + dy, rsrc.x2 + dx, rsrc.y2 + dy);
            rect_i rc = clip_rect_area(rdst, rsrc, src.width(), src.height());

            if(rc.x2 > 0)
            {
                int incy = 1;
                if(rdst.y1 > rsrc.y1)
                {
                    rsrc.y1 += rc.y2 - 1;
                    rdst.y1 += rc.y2 - 1;
                    incy = -1;
                }
                while(rc.y2 > 0)
                {
                    typename SrcPixelFormatRenderer::row_data rw = src.row(rsrc.y1);
                    if(rw.ptr)
                    {
                        int x1src = rsrc.x1;
                        int x1dst = rdst.x1;
                        int len   = rc.x2;
                        if(rw.x1 > x1src)
                        {
                            x1dst += rw.x1 - x1src;
                            len   -= rw.x1 - x1src;
                            x1src  = rw.x1;
                        }
                        if(len > 0)
                        {
                            if(x1src + len-1 > rw.x2)
                            {
                                len -= x1src + len - rw.x2 - 1;
                            }
                            if(len > 0)
                            {
                                m_ren->blend_from_lut(src,
                                                      color_lut,
                                                      x1dst, rdst.y1,
                                                      x1src, rsrc.y1,
                                                      len,
                                                      cover);
                            }
                        }
                    }
                    rdst.y1 += incy;
                    rsrc.y1 += incy;
                    --rc.y2;
                }
            }
        }
     */
    }
}
