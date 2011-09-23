﻿
namespace NewTOAPIA.Drawing
{
    using System;
    using NewTOAPIA.Graphics;

    using image_subpixel_scale_e = ImageFilterLookUpTable.image_subpixel_scale_e;

    public interface ISpanGenerator
    {
        void prepare();
        void generate(RGBA_Bytes[] span, int spanIndex, int x, int y, int len);
    };

    public abstract class span_image_filter : ISpanGenerator
    {
        private IImageBufferAccessor m_ImageBufferAccessor;
        private ISpanInterpolator m_interpolator;
        protected ImageFilterLookUpTable m_filter;
        private double m_dx_dbl;
        private double m_dy_dbl;
        private int m_dx_int;
        private int m_dy_int;

        public span_image_filter() { }
        public span_image_filter(IImageBufferAccessor src,
            ISpanInterpolator interpolator)
            : this(src, interpolator, null)
        {

        }

        public span_image_filter(IImageBufferAccessor src,
            ISpanInterpolator interpolator, ImageFilterLookUpTable filter)
        {
            m_ImageBufferAccessor = src;
            m_interpolator = interpolator;
            m_filter = (filter);
            m_dx_dbl = (0.5);
            m_dy_dbl = (0.5);
            m_dx_int = ((int)image_subpixel_scale_e.image_subpixel_scale / 2);
            m_dy_int = ((int)image_subpixel_scale_e.image_subpixel_scale / 2);
        }
        public void attach(IImageBufferAccessor v) { m_ImageBufferAccessor = v; }

        public abstract void generate(RGBA_Bytes[] span, int spanIndex, int x, int y, int len);

        public IImageBufferAccessor source() { return m_ImageBufferAccessor; }
        public ImageFilterLookUpTable filter() { return m_filter; }
        public int filter_dx_int() { return (int)m_dx_int; }
        public int filter_dy_int() { return (int)m_dy_int; }
        public double filter_dx_dbl() { return m_dx_dbl; }
        public double filter_dy_dbl() { return m_dy_dbl; }

        public void interpolator(ISpanInterpolator v) { m_interpolator = v; }
        public void filter(ImageFilterLookUpTable v) { m_filter = v; }
        public void filter_offset(double dx, double dy)
        {
            m_dx_dbl = dx;
            m_dy_dbl = dy;
            m_dx_int = (int)agg_basics.iround(dx * (int)image_subpixel_scale_e.image_subpixel_scale);
            m_dy_int = (int)agg_basics.iround(dy * (int)image_subpixel_scale_e.image_subpixel_scale);
        }
        public void filter_offset(double d) { filter_offset(d, d); }

        public ISpanInterpolator interpolator() { return m_interpolator; }

        public void prepare() { }
    };

    /*


    //==============================================span_image_resample_affine
    //template<class Source> 
    public class span_image_resample_affine : 
        span_image_filter//<Source, span_interpolator_linear<trans_affine> >
    {
        //typedef Source IImageAccessor;
        //typedef span_interpolator_linear<trans_affine> ISpanInterpolator;
        //typedef span_image_filter<source_type, ISpanInterpolator> base_type;

        //--------------------------------------------------------------------
        public span_image_resample_affine()
        {
            m_scale_limit=(200.0);
            m_blur_x=(1.0);
            m_blur_y=(1.0);
        }

        //--------------------------------------------------------------------
        public span_image_resample_affine(IImageAccessor src, 
                                   ISpanInterpolator inter,
                                   ImageFilterLookUpTable filter) : base(src, inter, filter)
        {
            m_scale_limit(200.0);
            m_blur_x(1.0);
            m_blur_y(1.0);
        }


        //--------------------------------------------------------------------
        public int  scale_limit() { return uround(m_scale_limit); }
        public void scale_limit(int v)  { m_scale_limit = v; }

        //--------------------------------------------------------------------
        public double blur_x() { return m_blur_x; }
        public double blur_y() { return m_blur_y; }
        public void blur_x(double v) { m_blur_x = v; }
        public void blur_y(double v) { m_blur_y = v; }
        public void blur(double v) { m_blur_x = m_blur_y = v; }

        //--------------------------------------------------------------------
        public void prepare() 
        {
            double scale_x;
            double scale_y;

            base_type::interpolator().transformer().scaling_abs(&scale_x, &scale_y);

            if(scale_x * scale_y > m_scale_limit)
            {
                scale_x = scale_x * m_scale_limit / (scale_x * scale_y);
                scale_y = scale_y * m_scale_limit / (scale_x * scale_y);
            }

            if(scale_x < 1) scale_x = 1;
            if(scale_y < 1) scale_y = 1;

            if(scale_x > m_scale_limit) scale_x = m_scale_limit;
            if(scale_y > m_scale_limit) scale_y = m_scale_limit;

            scale_x *= m_blur_x;
            scale_y *= m_blur_y;

            if(scale_x < 1) scale_x = 1;
            if(scale_y < 1) scale_y = 1;

            m_rx     = uround(    scale_x * (double)(image_subpixel_scale));
            m_rx_inv = uround(1.0/scale_x * (double)(image_subpixel_scale));

            m_ry     = uround(    scale_y * (double)(image_subpixel_scale));
            m_ry_inv = uround(1.0/scale_y * (double)(image_subpixel_scale));
        }

        protected int m_rx;
        protected int m_ry;
        protected int m_rx_inv;
        protected int m_ry_inv;

        private double m_scale_limit;
        private double m_blur_x;
        private double m_blur_y;
    };

     */


    //=====================================================span_image_resample
    public abstract class span_image_resample
        : span_image_filter
    {
        public span_image_resample(IImageBufferAccessor src,
                            ISpanInterpolator inter,
                            ImageFilterLookUpTable filter)
            : base(src, inter, filter)
        {
            m_scale_limit = (20);
            m_blur_x = ((int)image_subpixel_scale_e.image_subpixel_scale);
            m_blur_y = ((int)image_subpixel_scale_e.image_subpixel_scale);
        }

        //public abstract void prepare();
        //public abstract unsafe void generate(rgba8* span, int x, int y, int len);

        //--------------------------------------------------------------------
        int scale_limit() { return m_scale_limit; }
        void scale_limit(int v) { m_scale_limit = v; }

        //--------------------------------------------------------------------
        double blur_x() { return (double)(m_blur_x) / (double)((int)image_subpixel_scale_e.image_subpixel_scale); }
        double blur_y() { return (double)(m_blur_y) / (double)((int)image_subpixel_scale_e.image_subpixel_scale); }
        void blur_x(double v) { m_blur_x = (int)agg_basics.uround(v * (double)((int)image_subpixel_scale_e.image_subpixel_scale)); }
        void blur_y(double v) { m_blur_y = (int)agg_basics.uround(v * (double)((int)image_subpixel_scale_e.image_subpixel_scale)); }
        public void blur(double v)
        {
            m_blur_x = m_blur_y = (int)agg_basics.uround(v * (double)((int)image_subpixel_scale_e.image_subpixel_scale));
        }

        protected void adjust_scale(ref int rx, ref int ry)
        {
            if (rx < (int)image_subpixel_scale_e.image_subpixel_scale) rx = (int)image_subpixel_scale_e.image_subpixel_scale;
            if (ry < (int)image_subpixel_scale_e.image_subpixel_scale) ry = (int)image_subpixel_scale_e.image_subpixel_scale;
            if (rx > (int)image_subpixel_scale_e.image_subpixel_scale * m_scale_limit)
            {
                rx = (int)image_subpixel_scale_e.image_subpixel_scale * m_scale_limit;
            }
            if (ry > (int)image_subpixel_scale_e.image_subpixel_scale * m_scale_limit)
            {
                ry = (int)image_subpixel_scale_e.image_subpixel_scale * m_scale_limit;
            }
            rx = (rx * m_blur_x) >> (int)image_subpixel_scale_e.image_subpixel_shift;
            ry = (ry * m_blur_y) >> (int)image_subpixel_scale_e.image_subpixel_shift;
            if (rx < (int)image_subpixel_scale_e.image_subpixel_scale) rx = (int)image_subpixel_scale_e.image_subpixel_scale;
            if (ry < (int)image_subpixel_scale_e.image_subpixel_scale) ry = (int)image_subpixel_scale_e.image_subpixel_scale;
        }

        int m_scale_limit;
        int m_blur_x;
        int m_blur_y;
    }
}
