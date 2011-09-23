
namespace NewTOAPIA.Drawing
{
    using System;

    using image_subpixel_scale_e = ImageFilterLookUpTable.image_subpixel_scale_e;
    using image_filter_scale_e = ImageFilterLookUpTable.image_filter_scale_e;


    //==============================================span_image_filter_rgba_nn
    public class span_image_filter_rgba_nn : span_image_filter
    {
        const int base_shift = 8;
        const int base_scale = (int)(1 << base_shift);
        const int base_mask = base_scale - 1;

        public span_image_filter_rgba_nn(IImageBufferAccessor src, ISpanInterpolator inter)
            : base(src, inter, null)
        {
        }

        public override void generate(RGBA_Bytes[] span, int spanIndex, int x, int y, int len)
        {
            IImage SourceRenderingBuffer = base.source().DestImage;
            ISpanInterpolator spanInterpolator = base.interpolator();
            spanInterpolator.begin(x + base.filter_dx_dbl(), y + base.filter_dy_dbl(), len);
            do
            {
                int x_hr;
                int y_hr;
                spanInterpolator.coordinates(out x_hr, out y_hr);
                int x_lr = x_hr >> (int)image_subpixel_scale_e.image_subpixel_shift;
                int y_lr = y_hr >> (int)image_subpixel_scale_e.image_subpixel_shift;
                int bufferIndex;
                byte[] fg_ptr = SourceRenderingBuffer.GetPixelPointerXY(y_lr, x_lr, out bufferIndex);
                span[spanIndex].m_R = fg_ptr[ImageBuffer.OrderR];
                span[spanIndex].m_G = fg_ptr[ImageBuffer.OrderG];
                span[spanIndex].m_B = fg_ptr[ImageBuffer.OrderB];
                span[spanIndex].m_A = fg_ptr[ImageBuffer.OrderA];
                ++spanIndex;
                spanInterpolator.Next();
            } while (--len != 0);
        }
    };

    //=========================================span_image_filter_rgba_bilinear
    public class span_image_filter_rgba_bilinear : span_image_filter
    {
        const int base_shift = 8;
        const int base_scale = (int)(1 << base_shift);
        const int base_mask = base_scale - 1;

        public span_image_filter_rgba_bilinear(IImageBufferAccessor src, ISpanInterpolator inter)
            : base(src, inter, null)
        {
        }

#if use_timers
            static CNamedTimer Generate_Span = new CNamedTimer("Generate_Span rgba");
#endif
#if false
            public void generate(out RGBA_Bytes destPixel, int x, int y)
            {
                base.interpolator().begin(x + base.filter_dx_dbl(), y + base.filter_dy_dbl(), 1);

                int* fg = stackalloc int[4];

                byte* fg_ptr;

                IImage imageSource = base.source().DestImage;
                int maxx = (int)imageSource.Width() - 1;
                int maxy = (int)imageSource.Height() - 1;
                ISpanInterpolator spanInterpolator = base.interpolator();

                unchecked
                {
                    int x_hr;
                    int y_hr;

                    spanInterpolator.coordinates(out x_hr, out y_hr);

                    x_hr -= base.filter_dx_int();
                    y_hr -= base.filter_dy_int();

                    int x_lr = x_hr >> (int)image_subpixel_scale_e.image_subpixel_shift;
                    int y_lr = y_hr >> (int)image_subpixel_scale_e.image_subpixel_shift;

                    int weight;

                    fg[0] = fg[1] = fg[2] = fg[3] = (int)image_subpixel_scale_e.image_subpixel_scale * (int)image_subpixel_scale_e.image_subpixel_scale / 2;

                    x_hr &= (int)image_subpixel_scale_e.image_subpixel_mask;
                    y_hr &= (int)image_subpixel_scale_e.image_subpixel_mask;

                    fg_ptr = imageSource.GetPixelPointerY(y_lr) + (x_lr * 4);

                    weight = (int)(((int)image_subpixel_scale_e.image_subpixel_scale - x_hr) *
                             ((int)image_subpixel_scale_e.image_subpixel_scale - y_hr));
                    fg[0] += weight * fg_ptr[0];
                    fg[1] += weight * fg_ptr[1];
                    fg[2] += weight * fg_ptr[2];
                    fg[3] += weight * fg_ptr[3];

                    weight = (int)(x_hr * ((int)image_subpixel_scale_e.image_subpixel_scale - y_hr));
                    fg[0] += weight * fg_ptr[4];
                    fg[1] += weight * fg_ptr[5];
                    fg[2] += weight * fg_ptr[6];
                    fg[3] += weight * fg_ptr[7];

                    ++y_lr;
                    fg_ptr = imageSource.GetPixelPointerY(y_lr) + (x_lr * 4);

                    weight = (int)(((int)image_subpixel_scale_e.image_subpixel_scale - x_hr) * y_hr);
                    fg[0] += weight * fg_ptr[0];
                    fg[1] += weight * fg_ptr[1];
                    fg[2] += weight * fg_ptr[2];
                    fg[3] += weight * fg_ptr[3];

                    weight = (int)(x_hr * y_hr);
                    fg[0] += weight * fg_ptr[4];
                    fg[1] += weight * fg_ptr[5];
                    fg[2] += weight * fg_ptr[6];
                    fg[3] += weight * fg_ptr[7];

                    fg[0] >>= (int)image_subpixel_scale_e.image_subpixel_shift * 2;
                    fg[1] >>= (int)image_subpixel_scale_e.image_subpixel_shift * 2;
                    fg[2] >>= (int)image_subpixel_scale_e.image_subpixel_shift * 2;
                    fg[3] >>= (int)image_subpixel_scale_e.image_subpixel_shift * 2;

                    destPixel.m_R = (byte)fg[OrderR];
                    destPixel.m_G = (byte)fg[OrderG];
                    destPixel.m_B = (byte)fg[ImageBuffer.OrderB];
                    destPixel.m_A = (byte)fg[OrderA];
                }
            }
#endif

        public override void generate(RGBA_Bytes[] span, int spanIndex, int x, int y, int len)
        {
            throw new NotImplementedException(); /*
#if use_timers
                Generate_Span.Start();
#endif
                base.interpolator().begin(x + base.filter_dx_dbl(), y + base.filter_dy_dbl(), len);

                byte[] fg = stackalloc int[4];

                IImage sourceImage = source().DestImage;
                byte[] fg_ptr = sourceImage.ByteBuffer;

                int maxx = (int)sourceImage.Width() - 1;
                int maxy = (int)sourceImage.Height() - 1;
                ISpanInterpolator spanInterpolator = base.interpolator();

                unchecked
                {
                    do
                    {
                        int x_hr;
                        int y_hr;

                        spanInterpolator.coordinates(out x_hr, out y_hr);

                        x_hr -= base.filter_dx_int();
                        y_hr -= base.filter_dy_int();

                        int x_lr = x_hr >> (int)image_subpixel_scale_e.image_subpixel_shift;
                        int y_lr = y_hr >> (int)image_subpixel_scale_e.image_subpixel_shift;

                        int weight;

                        fg[0] = fg[1] = fg[2] = fg[3] = (int)image_subpixel_scale_e.image_subpixel_scale * (int)image_subpixel_scale_e.image_subpixel_scale / 2;

                        x_hr &= (int)image_subpixel_scale_e.image_subpixel_mask;
                        y_hr &= (int)image_subpixel_scale_e.image_subpixel_mask;

                        fg_ptr = sourceImage.GetPixelPointerY(y_lr) + (x_lr * 4);

                        weight = (int)(((int)image_subpixel_scale_e.image_subpixel_scale - x_hr) *
                                 ((int)image_subpixel_scale_e.image_subpixel_scale - y_hr));
                        fg[0] += weight * fg_ptr[0];
                        fg[1] += weight * fg_ptr[1];
                        fg[2] += weight * fg_ptr[2];
                        fg[3] += weight * fg_ptr[3];

                        weight = (int)(x_hr * ((int)image_subpixel_scale_e.image_subpixel_scale - y_hr));
                        fg[0] += weight * fg_ptr[4];
                        fg[1] += weight * fg_ptr[5];
                        fg[2] += weight * fg_ptr[6];
                        fg[3] += weight * fg_ptr[7];

                        ++y_lr;
                        fg_ptr = sourceImage.GetPixelPointerY(y_lr) + (x_lr * 4);

                        weight = (int)(((int)image_subpixel_scale_e.image_subpixel_scale - x_hr) * y_hr);
                        fg[0] += weight * fg_ptr[0];
                        fg[1] += weight * fg_ptr[1];
                        fg[2] += weight * fg_ptr[2];
                        fg[3] += weight * fg_ptr[3];

                        weight = (int)(x_hr * y_hr);
                        fg[0] += weight * fg_ptr[4];
                        fg[1] += weight * fg_ptr[5];
                        fg[2] += weight * fg_ptr[6];
                        fg[3] += weight * fg_ptr[7];

                        fg[0] >>= (int)image_subpixel_scale_e.image_subpixel_shift * 2;
                        fg[1] >>= (int)image_subpixel_scale_e.image_subpixel_shift * 2;
                        fg[2] >>= (int)image_subpixel_scale_e.image_subpixel_shift * 2;
                        fg[3] >>= (int)image_subpixel_scale_e.image_subpixel_shift * 2;

                        (*span).m_R = (byte)fg[OrderR];
                        (*span).m_G = (byte)fg[OrderG];
                        (*span).m_B = (byte)fg[ImageBuffer.OrderB];
                        (*span).m_A = (byte)fg[OrderA];
                        ++span;
                        spanInterpolator.Next();

                    } while (--len != 0);
                }
#if use_timers
                Generate_Span.Stop();
#endif
                                                      */
        }
    };


    //====================================span_image_filter_rgba_bilinear_clip
    public class span_image_filter_rgba_bilinear_clip : span_image_filter
    {
        private RGBA_Bytes m_OutsideSourceColor;

        const int base_shift = 8;
        const int base_scale = (int)(1 << base_shift);
        const int base_mask = base_scale - 1;

        public span_image_filter_rgba_bilinear_clip(IImageBufferAccessor src,
            IColorType back_color, ISpanInterpolator inter)
            : base(src, inter, null)
        {
            m_OutsideSourceColor = back_color.GetAsRGBA_Bytes();
        }

        public IColorType background_color() { return m_OutsideSourceColor; }
        public void background_color(IColorType v) { m_OutsideSourceColor = v.GetAsRGBA_Bytes(); }


#if use_timers
            static CNamedTimer Generate_Span = new CNamedTimer("Generate_Span rgba clip");
#endif
        public override void generate(RGBA_Bytes[] span, int spanIndex, int x, int y, int len)
        {
            /*#if use_timers
                        Generate_Span.Start();
            #endif*/
            base.interpolator().begin(x + base.filter_dx_dbl(), y + base.filter_dy_dbl(), len);

            int[] accumulatedColor = new int[4];

            int back_r = m_OutsideSourceColor.m_R;
            int back_g = m_OutsideSourceColor.m_G;
            int back_b = m_OutsideSourceColor.m_B;
            int back_a = m_OutsideSourceColor.m_A;

            int bufferIndex;
            byte[] fg_ptr;

            ImageBuffer SourceRenderingBuffer = (ImageBuffer)base.source().DestImage;
            int distanceBetweenPixelsInclusive = base.source().DestImage.GetDistanceBetweenPixelsInclusive();
            int maxx = (int)SourceRenderingBuffer.Width() - 1;
            int maxy = (int)SourceRenderingBuffer.Height() - 1;
            ISpanInterpolator spanInterpolator = base.interpolator();

            unchecked
            {
                do
                {
                    int x_hr;
                    int y_hr;

                    spanInterpolator.coordinates(out x_hr, out y_hr);

                    x_hr -= base.filter_dx_int();
                    y_hr -= base.filter_dy_int();

                    int x_lr = x_hr >> (int)image_subpixel_scale_e.image_subpixel_shift;
                    int y_lr = y_hr >> (int)image_subpixel_scale_e.image_subpixel_shift;
                    int weight;

                    if (x_lr >= 0 && y_lr >= 0 &&
                       x_lr < maxx && y_lr < maxy)
                    {
                        accumulatedColor[0] =
                        accumulatedColor[1] =
                        accumulatedColor[2] =
                        accumulatedColor[3] = (int)image_subpixel_scale_e.image_subpixel_scale * (int)image_subpixel_scale_e.image_subpixel_scale / 2;

                        x_hr &= (int)image_subpixel_scale_e.image_subpixel_mask;
                        y_hr &= (int)image_subpixel_scale_e.image_subpixel_mask;

                        fg_ptr = SourceRenderingBuffer.GetPixelPointerXY(x_lr, y_lr, out bufferIndex);

                        weight = (((int)image_subpixel_scale_e.image_subpixel_scale - x_hr) *
                                 ((int)image_subpixel_scale_e.image_subpixel_scale - y_hr));
                        accumulatedColor[0] += weight * fg_ptr[bufferIndex + ImageBuffer.OrderR];
                        accumulatedColor[1] += weight * fg_ptr[bufferIndex + ImageBuffer.OrderG];
                        accumulatedColor[2] += weight * fg_ptr[bufferIndex + ImageBuffer.OrderB];
                        accumulatedColor[3] += weight * fg_ptr[bufferIndex + ImageBuffer.OrderA];

                        bufferIndex += distanceBetweenPixelsInclusive;
                        weight = (x_hr * ((int)image_subpixel_scale_e.image_subpixel_scale - y_hr));
                        accumulatedColor[0] += weight * fg_ptr[bufferIndex + ImageBuffer.OrderR];
                        accumulatedColor[1] += weight * fg_ptr[bufferIndex + ImageBuffer.OrderG];
                        accumulatedColor[2] += weight * fg_ptr[bufferIndex + ImageBuffer.OrderB];
                        accumulatedColor[3] += weight * fg_ptr[bufferIndex + ImageBuffer.OrderA];

                        ++y_lr;
                        fg_ptr = SourceRenderingBuffer.GetPixelPointerXY(x_lr, y_lr, out bufferIndex);

                        weight = (((int)image_subpixel_scale_e.image_subpixel_scale - x_hr) * y_hr);
                        accumulatedColor[0] += weight * fg_ptr[bufferIndex + ImageBuffer.OrderR];
                        accumulatedColor[1] += weight * fg_ptr[bufferIndex + ImageBuffer.OrderG];
                        accumulatedColor[2] += weight * fg_ptr[bufferIndex + ImageBuffer.OrderB];
                        accumulatedColor[3] += weight * fg_ptr[bufferIndex + ImageBuffer.OrderA];

                        bufferIndex += distanceBetweenPixelsInclusive;
                        weight = (x_hr * y_hr);
                        accumulatedColor[0] += weight * fg_ptr[bufferIndex + ImageBuffer.OrderR];
                        accumulatedColor[1] += weight * fg_ptr[bufferIndex + ImageBuffer.OrderG];
                        accumulatedColor[2] += weight * fg_ptr[bufferIndex + ImageBuffer.OrderB];
                        accumulatedColor[3] += weight * fg_ptr[bufferIndex + ImageBuffer.OrderA];

                        accumulatedColor[0] >>= (int)image_subpixel_scale_e.image_subpixel_shift * 2;
                        accumulatedColor[1] >>= (int)image_subpixel_scale_e.image_subpixel_shift * 2;
                        accumulatedColor[2] >>= (int)image_subpixel_scale_e.image_subpixel_shift * 2;
                        accumulatedColor[3] >>= (int)image_subpixel_scale_e.image_subpixel_shift * 2;
                    }
                    else
                    {
                        if (x_lr < -1 || y_lr < -1 ||
                           x_lr > maxx || y_lr > maxy)
                        {
                            accumulatedColor[0] = back_r;
                            accumulatedColor[1] = back_g;
                            accumulatedColor[2] = back_b;
                            accumulatedColor[3] = back_a;
                        }
                        else
                        {
                            accumulatedColor[0] =
                            accumulatedColor[1] =
                            accumulatedColor[2] =
                            accumulatedColor[3] = (int)image_subpixel_scale_e.image_subpixel_scale * (int)image_subpixel_scale_e.image_subpixel_scale / 2;

                            x_hr &= (int)image_subpixel_scale_e.image_subpixel_mask;
                            y_hr &= (int)image_subpixel_scale_e.image_subpixel_mask;

                            weight = (((int)image_subpixel_scale_e.image_subpixel_scale - x_hr) *
                                     ((int)image_subpixel_scale_e.image_subpixel_scale - y_hr));
                            BlendInFilterPixel(accumulatedColor, back_r, back_g, back_b, back_a, SourceRenderingBuffer, maxx, maxy, x_lr, y_lr, weight);

                            x_lr++;

                            weight = (x_hr * ((int)image_subpixel_scale_e.image_subpixel_scale - y_hr));
                            BlendInFilterPixel(accumulatedColor, back_r, back_g, back_b, back_a, SourceRenderingBuffer, maxx, maxy, x_lr, y_lr, weight);

                            x_lr--;
                            y_lr++;

                            weight = (((int)image_subpixel_scale_e.image_subpixel_scale - x_hr) * y_hr);
                            BlendInFilterPixel(accumulatedColor, back_r, back_g, back_b, back_a, SourceRenderingBuffer, maxx, maxy, x_lr, y_lr, weight);

                            x_lr++;

                            weight = (x_hr * y_hr);
                            BlendInFilterPixel(accumulatedColor, back_r, back_g, back_b, back_a, SourceRenderingBuffer, maxx, maxy, x_lr, y_lr, weight);

                            accumulatedColor[0] >>= (int)image_subpixel_scale_e.image_subpixel_shift * 2;
                            accumulatedColor[1] >>= (int)image_subpixel_scale_e.image_subpixel_shift * 2;
                            accumulatedColor[2] >>= (int)image_subpixel_scale_e.image_subpixel_shift * 2;
                            accumulatedColor[3] >>= (int)image_subpixel_scale_e.image_subpixel_shift * 2;
                        }
                    }

                    span[spanIndex].m_R = (byte)accumulatedColor[0];
                    span[spanIndex].m_G = (byte)accumulatedColor[1];
                    span[spanIndex].m_B = (byte)accumulatedColor[2];
                    span[spanIndex].m_A = (byte)accumulatedColor[3];
                    ++spanIndex;
                    spanInterpolator.Next();
                } while (--len != 0);
            }
#if use_timers
                Generate_Span.Stop();
#endif
        }

        private void BlendInFilterPixel(int[] accumulatedColor, int back_r, int back_g, int back_b, int back_a, IImage SourceRenderingBuffer, int maxx, int maxy, int x_lr, int y_lr, int weight)
        {
            byte[] fg_ptr;
            unchecked
            {
                if ((uint)x_lr <= (uint)maxx && (uint)y_lr <= (uint)maxy)
                {
                    int bufferIndex;
                    fg_ptr = SourceRenderingBuffer.GetPixelPointerXY(x_lr, y_lr, out bufferIndex);

                    accumulatedColor[0] += weight * fg_ptr[bufferIndex + ImageBuffer.OrderR];
                    accumulatedColor[1] += weight * fg_ptr[bufferIndex + ImageBuffer.OrderG];
                    accumulatedColor[2] += weight * fg_ptr[bufferIndex + ImageBuffer.OrderB];
                    accumulatedColor[3] += weight * fg_ptr[bufferIndex + ImageBuffer.OrderA];
                }
                else
                {
                    accumulatedColor[0] += back_r * weight;
                    accumulatedColor[1] += back_g * weight;
                    accumulatedColor[2] += back_b * weight;
                    accumulatedColor[3] += back_a * weight;
                }
            }
        }
    };

    /*


    //==============================================span_image_filter_rgba_2x2
    //template<class Source, class Interpolator> 
    public class span_image_filter_rgba_2x2 : span_image_filter//<Source, Interpolator>
    {
        //typedef Source source_type;
        //typedef typename source_type::color_type color_type;
        //typedef typename source_type::order_type order_type;
        //typedef Interpolator interpolator_type;
        //typedef span_image_filter<source_type, interpolator_type> base_type;
        //typedef typename color_type::value_type value_type;
        //typedef typename color_type::calc_type calc_type;
        enum base_scale_e
        {
            base_shift = 8, //color_type::base_shift,
            base_mask  = 255,//color_type::base_mask
        };

        //--------------------------------------------------------------------
        public span_image_filter_rgba_2x2() {}
        public span_image_filter_rgba_2x2(pixfmt_alpha_blend_bgra32 src, 
                                   interpolator_type inter,
                                   ImageFilterLookUpTable filter) :
            base(src, inter, filter) 
        {}


        //--------------------------------------------------------------------
        public void generate(color_type* span, int x, int y, unsigned len)
        {
            base.interpolator().begin(x + base.filter_dx_dbl(), 
                                            y + base.filter_dy_dbl(), len);

            calc_type fg[4];

            byte *fg_ptr;
            int16* weight_array = base.filter().weight_array() + 
                                        ((base.filter().diameter()/2 - 1) << 
                                          image_subpixel_shift);

            do
            {
                int x_hr;
                int y_hr;

                base.interpolator().coordinates(&x_hr, &y_hr);

                x_hr -= base.filter_dx_int();
                y_hr -= base.filter_dy_int();

                int x_lr = x_hr >> image_subpixel_shift;
                int y_lr = y_hr >> image_subpixel_shift;

                unsigned weight;
                fg[0] = fg[1] = fg[2] = fg[3] = (int)image_filter_scale_e.image_filter_scale / 2;

                x_hr &= image_subpixel_mask;
                y_hr &= image_subpixel_mask;

                fg_ptr = base.source().span(x_lr, y_lr, 2);
                weight = (weight_array[x_hr + image_subpixel_scale] * 
                          weight_array[y_hr + image_subpixel_scale] + 
                          (int)image_filter_scale_e.image_filter_scale / 2) >> 
                          image_filter_shift;
                fg[0] += weight * *fg_ptr++;
                fg[1] += weight * *fg_ptr++;
                fg[2] += weight * *fg_ptr++;
                fg[3] += weight * *fg_ptr;

                fg_ptr = base.source().next_x();
                weight = (weight_array[x_hr] * 
                          weight_array[y_hr + image_subpixel_scale] + 
                          (int)image_filter_scale_e.image_filter_scale / 2) >> 
                          image_filter_shift;
                fg[0] += weight * *fg_ptr++;
                fg[1] += weight * *fg_ptr++;
                fg[2] += weight * *fg_ptr++;
                fg[3] += weight * *fg_ptr;

                fg_ptr = base.source().next_y();
                weight = (weight_array[x_hr + image_subpixel_scale] * 
                          weight_array[y_hr] + 
                          (int)image_filter_scale_e.image_filter_scale / 2) >> 
                          image_filter_shift;
                fg[0] += weight * *fg_ptr++;
                fg[1] += weight * *fg_ptr++;
                fg[2] += weight * *fg_ptr++;
                fg[3] += weight * *fg_ptr;

                fg_ptr = base.source().next_x();
                weight = (weight_array[x_hr] * 
                          weight_array[y_hr] + 
                          (int)image_filter_scale_e.image_filter_scale / 2) >> 
                          image_filter_shift;
                fg[0] += weight * *fg_ptr++;
                fg[1] += weight * *fg_ptr++;
                fg[2] += weight * *fg_ptr++;
                fg[3] += weight * *fg_ptr;

                fg[0] >>= image_filter_shift;
                fg[1] >>= image_filter_shift;
                fg[2] >>= image_filter_shift;
                fg[3] >>= image_filter_shift;

                if(fg[ImageBuffer.OrderA] > base_mask)         fg[ImageBuffer.OrderA] = base_mask;
                if(fg[ImageBuffer.OrderR] > fg[ImageBuffer.OrderA]) fg[ImageBuffer.OrderR] = fg[ImageBuffer.OrderA];
                if(fg[ImageBuffer.OrderG] > fg[ImageBuffer.OrderA]) fg[ImageBuffer.OrderG] = fg[ImageBuffer.OrderA];
                if(fg[ImageBuffer.OrderB] > fg[ImageBuffer.OrderA]) fg[ImageBuffer.OrderB] = fg[ImageBuffer.OrderA];

                span->r = (byte)fg[ImageBuffer.OrderR];
                span->g = (byte)fg[ImageBuffer.OrderG];
                span->b = (byte)fg[ImageBuffer.OrderB];
                span->a = (byte)fg[ImageBuffer.OrderA];
                ++span;
                ++base.interpolator();

            } while(--len);
        }
    };



    //==================================================span_image_filter_rgba
    //template<class Source, class Interpolator> 
    public class span_image_filter_rgba : span_image_filter//<Source, Interpolator>
    {
        //typedef Source source_type;
        //typedef typename source_type::color_type color_type;
        //typedef typename source_type::order_type order_type;
        //typedef Interpolator interpolator_type;
        //typedef span_image_filter<source_type, interpolator_type> base_type;
        //typedef typename color_type::value_type value_type;
        //typedef typename color_type::calc_type calc_type;
        enum base_scale_e
        {
            base_shift = 8, //color_type::base_shift,
            base_mask  = 255,//color_type::base_mask
        };

        //--------------------------------------------------------------------
        public span_image_filter_rgba() {}
        public span_image_filter_rgba(pixfmt_alpha_blend_bgra32 src, 
                               interpolator_type inter,
                               ImageFilterLookUpTable filter) :
            base(src, inter, &filter) 
        {}

        //--------------------------------------------------------------------
        public void generate(color_type* span, int x, int y, unsigned len)
        {
            base.interpolator().begin(x + base.filter_dx_dbl(), 
                                            y + base.filter_dy_dbl(), len);

            int fg[4];
            byte *fg_ptr;

            unsigned     diameter     = base.filter().diameter();
            int          start        = base.filter().start();
            int16* weight_array = base.filter().weight_array();

            int x_count; 
            int weight_y;

            do
            {
                base.interpolator().coordinates(&x, &y);

                x -= base.filter_dx_int();
                y -= base.filter_dy_int();

                int x_hr = x; 
                int y_hr = y; 

                int x_lr = x_hr >> image_subpixel_shift;
                int y_lr = y_hr >> image_subpixel_shift;

                fg[0] = fg[1] = fg[2] = fg[3] = (int)image_filter_scale_e.image_filter_scale / 2;

                int x_fract = x_hr & image_subpixel_mask;
                unsigned y_count = diameter;

                y_hr = image_subpixel_mask - (y_hr & image_subpixel_mask);
                fg_ptr = base.source().span(x_lr + start, 
                                                                     y_lr + start, 
                                                                     diameter);
                for(;;)
                {
                    x_count  = diameter;
                    weight_y = weight_array[y_hr];
                    x_hr = image_subpixel_mask - x_fract;
                    for(;;)
                    {
                        int weight = (weight_y * weight_array[x_hr] + 
                                     (int)image_filter_scale_e.image_filter_scale / 2) >> 
                                     image_filter_shift;

                        fg[0] += weight * *fg_ptr++;
                        fg[1] += weight * *fg_ptr++;
                        fg[2] += weight * *fg_ptr++;
                        fg[3] += weight * *fg_ptr;

                        if(--x_count == 0) break;
                        x_hr  += image_subpixel_scale;
                        fg_ptr = base.source().next_x();
                    }

                    if(--y_count == 0) break;
                    y_hr  += image_subpixel_scale;
                    fg_ptr = base.source().next_y();
                }

                fg[0] >>= image_filter_shift;
                fg[1] >>= image_filter_shift;
                fg[2] >>= image_filter_shift;
                fg[3] >>= image_filter_shift;

                if(fg[0] < 0) fg[0] = 0;
                if(fg[1] < 0) fg[1] = 0;
                if(fg[2] < 0) fg[2] = 0;
                if(fg[3] < 0) fg[3] = 0;

                if(fg[ImageBuffer.OrderA] > base_mask)         fg[ImageBuffer.OrderA] = base_mask;
                if(fg[ImageBuffer.OrderR] > fg[ImageBuffer.OrderA]) fg[ImageBuffer.OrderR] = fg[ImageBuffer.OrderA];
                if(fg[ImageBuffer.OrderG] > fg[ImageBuffer.OrderA]) fg[ImageBuffer.OrderG] = fg[ImageBuffer.OrderA];
                if(fg[ImageBuffer.OrderB] > fg[ImageBuffer.OrderA]) fg[ImageBuffer.OrderB] = fg[ImageBuffer.OrderA];

                span->r = (byte)fg[ImageBuffer.OrderR];
                span->g = (byte)fg[ImageBuffer.OrderG];
                span->b = (byte)fg[ImageBuffer.OrderB];
                span->a = (byte)fg[ImageBuffer.OrderA];
                ++span;
                ++base.interpolator();

            } while(--len);
        }
    };

    //========================================span_image_resample_rgba_affine
    public class span_image_resample_rgba_affine : span_image_resample_affine
    {
        //typedef Source source_type;
        //typedef typename source_type::color_type color_type;
        //typedef typename source_type::order_type order_type;
        //typedef span_image_resample_affine<source_type> base_type;
        //typedef typename base.interpolator_type interpolator_type;
        //typedef typename color_type::value_type value_type;
        //typedef typename color_type::long_type long_type;
        enum base_scale_e
        {
            base_shift      = 8, //color_type::base_shift,
            base_mask       = 255,//color_type::base_mask,
            downscale_shift = image_filter_shift
        };

        //--------------------------------------------------------------------
        public span_image_resample_rgba_affine() {}
        public span_image_resample_rgba_affine(pixfmt_alpha_blend_bgra32 src, 
                                        interpolator_type inter,
                                        ImageFilterLookUpTable filter) :
            base(src, inter, filter) 
        {}


        //--------------------------------------------------------------------
        public void generate(color_type* span, int x, int y, unsigned len)
        {
            base.interpolator().begin(x + base.filter_dx_dbl(), 
                                            y + base.filter_dy_dbl(), len);

            long_type fg[4];

            int diameter     = base.filter().diameter();
            int filter_scale = diameter << image_subpixel_shift;
            int radius_x     = (diameter * base.m_rx) >> 1;
            int radius_y     = (diameter * base.m_ry) >> 1;
            int len_x_lr     = 
                (diameter * base.m_rx + image_subpixel_mask) >> 
                    image_subpixel_shift;

            int16* weight_array = base.filter().weight_array();

            do
            {
                base.interpolator().coordinates(&x, &y);

                x += base.filter_dx_int() - radius_x;
                y += base.filter_dy_int() - radius_y;

                fg[0] = fg[1] = fg[2] = fg[3] = (int)image_filter_scale_e.image_filter_scale / 2;

                int y_lr = y >> image_subpixel_shift;
                int y_hr = ((image_subpixel_mask - (y & image_subpixel_mask)) * 
                                base.m_ry_inv) >> 
                                    image_subpixel_shift;
                int total_weight = 0;
                int x_lr = x >> image_subpixel_shift;
                int x_hr = ((image_subpixel_mask - (x & image_subpixel_mask)) * 
                                base.m_rx_inv) >> 
                                    image_subpixel_shift;

                int x_hr2 = x_hr;
                byte* fg_ptr = base.source().span(x_lr, y_lr, len_x_lr);
                for(;;)
                {
                    int weight_y = weight_array[y_hr];
                    x_hr = x_hr2;
                    for(;;)
                    {
                        int weight = (weight_y * weight_array[x_hr] + 
                                     (int)image_filter_scale_e.image_filter_scale / 2) >> 
                                     downscale_shift;

                        fg[0] += *fg_ptr++ * weight;
                        fg[1] += *fg_ptr++ * weight;
                        fg[2] += *fg_ptr++ * weight;
                        fg[3] += *fg_ptr++ * weight;
                        total_weight += weight;
                        x_hr  += base.m_rx_inv;
                        if(x_hr >= filter_scale) break;
                        fg_ptr = base.source().next_x();
                    }
                    y_hr += base.m_ry_inv;
                    if(y_hr >= filter_scale) break;
                    fg_ptr = base.source().next_y();
                }

                fg[0] /= total_weight;
                fg[1] /= total_weight;
                fg[2] /= total_weight;
                fg[3] /= total_weight;

                if(fg[0] < 0) fg[0] = 0;
                if(fg[1] < 0) fg[1] = 0;
                if(fg[2] < 0) fg[2] = 0;
                if(fg[3] < 0) fg[3] = 0;

                if(fg[ImageBuffer.OrderA] > base_mask)         fg[ImageBuffer.OrderA] = base_mask;
                if(fg[ImageBuffer.OrderR] > fg[ImageBuffer.OrderA]) fg[ImageBuffer.OrderR] = fg[ImageBuffer.OrderA];
                if(fg[ImageBuffer.OrderG] > fg[ImageBuffer.OrderA]) fg[ImageBuffer.OrderG] = fg[ImageBuffer.OrderA];
                if(fg[ImageBuffer.OrderB] > fg[ImageBuffer.OrderA]) fg[ImageBuffer.OrderB] = fg[ImageBuffer.OrderA];

                span->r = (byte)fg[ImageBuffer.OrderR];
                span->g = (byte)fg[ImageBuffer.OrderG];
                span->b = (byte)fg[ImageBuffer.OrderB];
                span->a = (byte)fg[ImageBuffer.OrderA];

                ++span;
                ++base.interpolator();
            } while(--len);
        }
    };
     */

    //==============================================span_image_resample_rgba
    public class span_image_resample_rgba
        : span_image_resample
    {
        private const int base_mask = 255;
        private const int downscale_shift = (int)ImageFilterLookUpTable.image_filter_scale_e.image_filter_shift;

        //--------------------------------------------------------------------
        public span_image_resample_rgba(IImageBufferAccessor src,
                            ISpanInterpolator inter,
                            ImageFilterLookUpTable filter) :
            base(src, inter, filter)
        {
            if (src.DestImage.GetBlender().NumPixelBits != 32)
            {
                throw new System.FormatException("You have to use a rgba blender with span_image_resample_rgba");
            }
        }

        //--------------------------------------------------------------------
        public override void generate(RGBA_Bytes[] span, int spanIndex, int x, int y, int len)
        {
            ISpanInterpolator spanInterpolator = base.interpolator();
            spanInterpolator.begin(x + base.filter_dx_dbl(), y + base.filter_dy_dbl(), len);

            int[] fg = new int[4];

            byte[] fg_ptr;
            int[] weightArray = filter().weight_array();
            int diameter = (int)base.filter().diameter();
            int filter_scale = diameter << (int)image_subpixel_scale_e.image_subpixel_shift;

            int[] weight_array = weightArray;

            do
            {
                int rx;
                int ry;
                int rx_inv = (int)image_subpixel_scale_e.image_subpixel_scale;
                int ry_inv = (int)image_subpixel_scale_e.image_subpixel_scale;
                spanInterpolator.coordinates(out x, out y);
                spanInterpolator.local_scale(out rx, out ry);
                base.adjust_scale(ref rx, ref ry);

                rx_inv = (int)image_subpixel_scale_e.image_subpixel_scale * (int)image_subpixel_scale_e.image_subpixel_scale / rx;
                ry_inv = (int)image_subpixel_scale_e.image_subpixel_scale * (int)image_subpixel_scale_e.image_subpixel_scale / ry;

                int radius_x = (diameter * rx) >> 1;
                int radius_y = (diameter * ry) >> 1;
                int len_x_lr =
                    (diameter * rx + (int)image_subpixel_scale_e.image_subpixel_mask) >>
                        (int)(int)image_subpixel_scale_e.image_subpixel_shift;

                x += base.filter_dx_int() - radius_x;
                y += base.filter_dy_int() - radius_y;

                fg[0] = fg[1] = fg[2] = fg[3] = (int)image_filter_scale_e.image_filter_scale / 2;

                int y_lr = y >> (int)(int)image_subpixel_scale_e.image_subpixel_shift;
                int y_hr = (((int)image_subpixel_scale_e.image_subpixel_mask - (y & (int)image_subpixel_scale_e.image_subpixel_mask)) *
                               ry_inv) >> (int)(int)image_subpixel_scale_e.image_subpixel_shift;
                int total_weight = 0;
                int x_lr = x >> (int)(int)image_subpixel_scale_e.image_subpixel_shift;
                int x_hr = (((int)image_subpixel_scale_e.image_subpixel_mask - (x & (int)image_subpixel_scale_e.image_subpixel_mask)) *
                               rx_inv) >> (int)(int)image_subpixel_scale_e.image_subpixel_shift;
                int x_hr2 = x_hr;
                int sourceIndex;
                fg_ptr = base.source().span(x_lr, y_lr, len_x_lr, out sourceIndex);

                for (; ; )
                {
                    int weight_y = weight_array[y_hr];
                    x_hr = x_hr2;
                    for (; ; )
                    {
                        int weight = (weight_y * weight_array[x_hr] +
                                     (int)image_filter_scale_e.image_filter_scale / 2) >>
                                     downscale_shift;
                        fg[0] += fg_ptr[sourceIndex + ImageBuffer.OrderR] * weight;
                        fg[1] += fg_ptr[sourceIndex + ImageBuffer.OrderG] * weight;
                        fg[2] += fg_ptr[sourceIndex + ImageBuffer.OrderB] * weight;
                        fg[3] += fg_ptr[sourceIndex + ImageBuffer.OrderA] * weight;
                        total_weight += weight;
                        x_hr += rx_inv;
                        if (x_hr >= filter_scale) break;
                        fg_ptr = base.source().next_x(out sourceIndex);
                    }
                    y_hr += ry_inv;
                    if (y_hr >= filter_scale)
                    {
                        break;
                    }

                    fg_ptr = base.source().next_y(out sourceIndex);
                }

                fg[0] /= total_weight;
                fg[1] /= total_weight;
                fg[2] /= total_weight;
                fg[3] /= total_weight;

                if (fg[0] < 0) fg[0] = 0;
                if (fg[1] < 0) fg[1] = 0;
                if (fg[2] < 0) fg[2] = 0;
                if (fg[3] < 0) fg[3] = 0;

                if (fg[0] > base_mask) fg[0] = base_mask;
                if (fg[1] > base_mask) fg[1] = base_mask;
                if (fg[2] > base_mask) fg[2] = base_mask;
                if (fg[3] > base_mask) fg[3] = base_mask;

                span[spanIndex].m_R = (byte)fg[0];
                span[spanIndex].m_G = (byte)fg[1];
                span[spanIndex].m_B = (byte)fg[2];
                span[spanIndex].m_A = (byte)fg[3];

                spanIndex++;
                interpolator().Next();
            } while (--len != 0);
        }
        /*
                    ISpanInterpolator spanInterpolator = base.interpolator();
                    spanInterpolator.begin(x + base.filter_dx_dbl(), y + base.filter_dy_dbl(), len);

                    int* fg = stackalloc int[4];

                    byte* fg_ptr;
                    fixed (int* pWeightArray = filter().weight_array())
                    {
                        int diameter = (int)base.filter().diameter();
                        int filter_scale = diameter << (int)image_subpixel_scale_e.image_subpixel_shift;

                        int* weight_array = pWeightArray;

                        do
                        {
                            int rx;
                            int ry;
                            int rx_inv = (int)image_subpixel_scale_e.image_subpixel_scale;
                            int ry_inv = (int)image_subpixel_scale_e.image_subpixel_scale;
                            spanInterpolator.coordinates(out x, out y);
                            spanInterpolator.local_scale(out rx, out ry);
                            base.adjust_scale(ref rx, ref ry);

                            rx_inv = (int)image_subpixel_scale_e.image_subpixel_scale * (int)image_subpixel_scale_e.image_subpixel_scale / rx;
                            ry_inv = (int)image_subpixel_scale_e.image_subpixel_scale * (int)image_subpixel_scale_e.image_subpixel_scale / ry;

                            int radius_x = (diameter * rx) >> 1;
                            int radius_y = (diameter * ry) >> 1;
                            int len_x_lr =
                                (diameter * rx + (int)image_subpixel_scale_e.image_subpixel_mask) >>
                                    (int)(int)image_subpixel_scale_e.image_subpixel_shift;

                            x += base.filter_dx_int() - radius_x;
                            y += base.filter_dy_int() - radius_y;

                            fg[0] = fg[1] = fg[2] = fg[3] = (int)image_filter_scale_e.image_filter_scale / 2;

                            int y_lr = y >> (int)(int)image_subpixel_scale_e.image_subpixel_shift;
                            int y_hr = (((int)image_subpixel_scale_e.image_subpixel_mask - (y & (int)image_subpixel_scale_e.image_subpixel_mask)) * 
                                           ry_inv) >>
                                               (int)(int)image_subpixel_scale_e.image_subpixel_shift;
                            int total_weight = 0;
                            int x_lr = x >> (int)(int)image_subpixel_scale_e.image_subpixel_shift;
                            int x_hr = (((int)image_subpixel_scale_e.image_subpixel_mask - (x & (int)image_subpixel_scale_e.image_subpixel_mask)) * 
                                           rx_inv) >>
                                               (int)(int)image_subpixel_scale_e.image_subpixel_shift;
                            int x_hr2 = x_hr;
                            fg_ptr = base.source().span(x_lr, y_lr, (int)len_x_lr);

                            for(;;)
                            {
                                int weight_y = weight_array[y_hr];
                                x_hr = x_hr2;
                                for(;;)
                                {
                                    int weight = (weight_y * weight_array[x_hr] +
                                                 (int)image_filter_scale_e.image_filter_scale / 2) >> 
                                                 downscale_shift;
                                    fg[0] += *fg_ptr++ * weight;
                                    fg[1] += *fg_ptr++ * weight;
                                    fg[2] += *fg_ptr++ * weight;
                                    fg[3] += *fg_ptr++ * weight;
                                    total_weight += weight;
                                    x_hr  += rx_inv;
                                    if(x_hr >= filter_scale) break;
                                    fg_ptr = base.source().next_x();
                                }
                                y_hr += ry_inv;
                                if (y_hr >= filter_scale)
                                {
                                    break;
                                }

                                fg_ptr = base.source().next_y();
                            }

                            fg[0] /= total_weight;
                            fg[1] /= total_weight;
                            fg[2] /= total_weight;
                            fg[3] /= total_weight;

                            if(fg[0] < 0) fg[0] = 0;
                            if(fg[1] < 0) fg[1] = 0;
                            if(fg[2] < 0) fg[2] = 0;
                            if(fg[3] < 0) fg[3] = 0;

                            if(fg[0] > fg[0]) fg[0] = fg[0];
                            if(fg[1] > fg[1]) fg[1] = fg[1];
                            if(fg[2] > fg[2]) fg[2] = fg[2];
                            if (fg[3] > base_mask) fg[3] = base_mask;

                            span->R_Byte = (byte)fg[ImageBuffer.OrderR];
                            span->G_Byte = (byte)fg[ImageBuffer.OrderG];
                            span->B_Byte = (byte)fg[ImageBuffer.OrderB];
                            span->A_Byte = (byte)fg[ImageBuffer.OrderA];

                            ++span;
                            interpolator().Next();
                        } while(--len != 0);
                    }
                                                              */
    }
}


//#endif



