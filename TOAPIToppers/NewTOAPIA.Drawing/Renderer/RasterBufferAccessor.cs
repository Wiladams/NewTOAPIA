//----------------------------------------------------------------------------
// Anti-Grain Geometry - Version 2.4
// Copyright (C) 2002-2005 Maxim Shemanarev (http://www.antigrain.com)
//
// C# Port port by: Lars Brubaker
//                  larsbrubaker@gmail.com
// Copyright (C) 2007
//
// Permission to copy, use, modify, sell and distribute this software 
// is granted provided this copyright notice appears in all copies. 
// This software is provided "as is" without express or implied
// warranty, and with no claim as to its suitability for any purpose.
//
//----------------------------------------------------------------------------
// Contact: mcseem@antigrain.com
//          mcseemagg@yahoo.com
//          http://www.antigrain.com
//----------------------------------------------------------------------------

namespace NewTOAPIA.Drawing
{
    using System.Runtime.InteropServices;

    public interface IImageBufferAccessor
    {
        byte[] span(int x, int y, int len, out int bufferIndex);
        byte[] next_x(out int bufferByteOffset);
        byte[] next_y(out int bufferByteOffset);

        IImage DestImage
        {
            get;
        }
    };

    public class ImageBufferAccessorCommon : IImageBufferAccessor
    {
        protected IImage m_pixf;
        protected int m_x, m_x0, m_y;
        protected byte[] m_Buffer;
        protected int m_CurrentIndex;
        int m_Width;

        public ImageBufferAccessorCommon(IImage pixf)
        {
            attach(pixf);
        }

        void attach(IImage pixf)
        {
            m_pixf = pixf;
            m_Buffer = m_pixf.GetBuffer(out m_CurrentIndex);
            m_Width = m_pixf.Width();
        }

        public IImage DestImage
        {
            get
            {
                return m_pixf;
            }
        }

        private byte[] pixel(out int bufferByteOffset)
        {
            int x = m_x;
            int y = m_y;
            unchecked
            {
                if ((uint)x >= (uint)m_pixf.Width())
                {
                    if (x < 0)
                    {
                        x = 0;
                    }
                    else
                    {
                        x = (int)m_pixf.Width() - 1;
                    }
                }

                if ((uint)y >= (uint)m_pixf.Height())
                {
                    if (y < 0)
                    {
                        y = 0;
                    }
                    else
                    {
                        y = (int)m_pixf.Height() - 1;
                    }
                }
            }

            return m_pixf.GetPixelPointerXY(x, y, out bufferByteOffset);
        }

        public byte[] span(int x, int y, int len, out int bufferOffset)
        {
            m_x = m_x0 = x;
            m_y = y;
            unchecked
            {
                if ((uint)y < (uint)m_pixf.Height()
                    && x >= 0 && x + len <= (int)m_pixf.Width())
                {
                    m_Buffer = m_pixf.GetPixelPointerXY(x, y, out bufferOffset);
                    m_CurrentIndex = bufferOffset;
                    return m_Buffer;
                }
            }

            m_CurrentIndex = -1;
            return pixel(out bufferOffset);
        }

        public byte[] next_x(out int bufferOffset)
        {
            // this is the code (managed) that the original agg used.  
            // It looks like it doesn't check x but, It should be a bit faster and is valid 
            // because "span" checked the whole length for good x.
            if (m_CurrentIndex != -1)
            {
                bufferOffset = ++m_CurrentIndex;
                return m_Buffer;
            }
            ++m_x;
            return pixel(out bufferOffset);
        }

        public byte[] next_y(out int bufferOffset)
        {
            ++m_y;
            m_x = m_x0;
            if (m_CurrentIndex != -1
                && (uint)m_y < (uint)m_pixf.Height())
            {
                m_pixf.GetPixelPointerXY(m_x, m_y, out m_CurrentIndex);
                bufferOffset = m_CurrentIndex; ;
                return m_Buffer;
            }

            m_CurrentIndex = -1;
            return pixel(out bufferOffset);
        }
    };

    public sealed class ImageBufferAccessorClip : ImageBufferAccessorCommon
    {
        byte[] m_OutsideBufferColor;

        public ImageBufferAccessorClip(IImage sourceImage, RGBA_Bytes bk)
            : base(sourceImage)
        {
            m_OutsideBufferColor = new byte[4];
            m_OutsideBufferColor[0] = bk.m_R;
            m_OutsideBufferColor[1] = bk.m_G;
            m_OutsideBufferColor[2] = bk.m_B;
            m_OutsideBufferColor[3] = bk.m_A;
        }

        private byte[] pixel(out int bufferByteOffset)
        {
            unchecked
            {
                if (((uint)m_x < (uint)m_pixf.Width())
                    && ((uint)m_y < (uint)m_pixf.Height()))
                {
                    return m_pixf.GetPixelPointerXY(m_x, m_y, out bufferByteOffset);
                }
            }

            bufferByteOffset = 0;
            return m_OutsideBufferColor;
        }

        //public void background_color(IColorType bk)
        //{
        //  m_pixf.make_pix(m_pBackBufferColor, bk);
        //}
    };

    /*
        //--------------------------------------------------image_accessor_no_clip
        template<class PixFmt> class image_accessor_no_clip
        {
        public:
            typedef PixFmt   pixfmt_type;
            typedef typename pixfmt_type::color_type color_type;
            typedef typename pixfmt_type::order_type order_type;
            typedef typename pixfmt_type::value_type value_type;
            enum pix_width_e { pix_width = pixfmt_type::pix_width };

            image_accessor_no_clip() {}
            explicit image_accessor_no_clip(pixfmt_type& pixf) : 
                m_pixf(&pixf) 
            {}

            void attach(pixfmt_type& pixf)
            {
                m_pixf = &pixf;
            }

            byte* span(int x, int y, int)
            {
                m_x = x;
                m_y = y;
                return m_pix_ptr = m_pixf->pix_ptr(x, y);
            }

            byte* next_x()
            {
                return m_pix_ptr += pix_width;
            }

            byte* next_y()
            {
                ++m_y;
                return m_pix_ptr = m_pixf->pix_ptr(m_x, m_y);
            }

        private:
            pixfmt_type* m_pixf;
            int                m_x, m_y;
            byte*       m_pix_ptr;
        };
     */

    public sealed class ImageBufferAccessorClamp : ImageBufferAccessorCommon
    {
        public ImageBufferAccessorClamp(IImage pixf)
            : base(pixf)
        {
        }

        private byte[] pixel(out int bufferByteOffset)
        {
            int x = m_x;
            int y = m_y;
            unchecked
            {
                if ((uint)x >= (uint)m_pixf.Width())
                {
                    if (x < 0)
                    {
                        x = 0;
                    }
                    else
                    {
                        x = (int)m_pixf.Width() - 1;
                    }
                }

                if ((uint)y >= (uint)m_pixf.Height())
                {
                    if (y < 0)
                    {
                        y = 0;
                    }
                    else
                    {
                        y = (int)m_pixf.Height() - 1;
                    }
                }
            }

            return m_pixf.GetPixelPointerXY(x, y, out bufferByteOffset);
        }
    };
    /*

        //-----------------------------------------------------image_accessor_wrap
        template<class PixFmt, class WrapX, class WrapY> class image_accessor_wrap
        {
        public:
            typedef PixFmt   pixfmt_type;
            typedef typename pixfmt_type::color_type color_type;
            typedef typename pixfmt_type::order_type order_type;
            typedef typename pixfmt_type::value_type value_type;
            enum pix_width_e { pix_width = pixfmt_type::pix_width };

            image_accessor_wrap() {}
            explicit image_accessor_wrap(pixfmt_type& pixf) : 
                m_pixf(&pixf), 
                m_wrap_x(pixf.Width()), 
                m_wrap_y(pixf.Height())
            {}

            void attach(pixfmt_type& pixf)
            {
                m_pixf = &pixf;
            }

            byte* span(int x, int y, int)
            {
                m_x = x;
                m_row_ptr = m_pixf->row_ptr(m_wrap_y(y));
                return m_row_ptr + m_wrap_x(x) * pix_width;
            }

            byte* next_x()
            {
                int x = ++m_wrap_x;
                return m_row_ptr + x * pix_width;
            }

            byte* next_y()
            {
                m_row_ptr = m_pixf->row_ptr(++m_wrap_y);
                return m_row_ptr + m_wrap_x(m_x) * pix_width;
            }

        private:
            pixfmt_type* m_pixf;
            byte*       m_row_ptr;
            int                m_x;
            WrapX              m_wrap_x;
            WrapY              m_wrap_y;
        };




        //--------------------------------------------------------wrap_mode_repeat
        class wrap_mode_repeat
        {
        public:
            wrap_mode_repeat() {}
            wrap_mode_repeat(int size) : 
                m_size(size), 
                m_add(size * (0x3FFFFFFF / size)),
                m_value(0)
            {}

            int operator() (int v)
            { 
                return m_value = (int(v) + m_add) % m_size; 
            }

            int operator++ ()
            {
                ++m_value;
                if(m_value >= m_size) m_value = 0;
                return m_value;
            }
        private:
            int m_size;
            int m_add;
            int m_value;
        };


        //---------------------------------------------------wrap_mode_repeat_pow2
        class wrap_mode_repeat_pow2
        {
        public:
            wrap_mode_repeat_pow2() {}
            wrap_mode_repeat_pow2(int size) : m_value(0)
            {
                m_mask = 1;
                while(m_mask < size) m_mask = (m_mask << 1) | 1;
                m_mask >>= 1;
            }
            int operator() (int v)
            { 
                return m_value = int(v) & m_mask;
            }
            int operator++ ()
            {
                ++m_value;
                if(m_value > m_mask) m_value = 0;
                return m_value;
            }
        private:
            int m_mask;
            int m_value;
        };


        //----------------------------------------------wrap_mode_repeat_auto_pow2
        class wrap_mode_repeat_auto_pow2
        {
        public:
            wrap_mode_repeat_auto_pow2() {}
            wrap_mode_repeat_auto_pow2(int size) :
                m_size(size),
                m_add(size * (0x3FFFFFFF / size)),
                m_mask((m_size & (m_size-1)) ? 0 : m_size-1),
                m_value(0)
            {}

            int operator() (int v) 
            { 
                if(m_mask) return m_value = int(v) & m_mask;
                return m_value = (int(v) + m_add) % m_size;
            }
            int operator++ ()
            {
                ++m_value;
                if(m_value >= m_size) m_value = 0;
                return m_value;
            }

        private:
            int m_size;
            int m_add;
            int m_mask;
            int m_value;
        };


        //-------------------------------------------------------wrap_mode_reflect
        class wrap_mode_reflect
        {
        public:
            wrap_mode_reflect() {}
            wrap_mode_reflect(int size) : 
                m_size(size), 
                m_size2(size * 2),
                m_add(m_size2 * (0x3FFFFFFF / m_size2)),
                m_value(0)
            {}

            int operator() (int v)
            { 
                m_value = (int(v) + m_add) % m_size2;
                if(m_value >= m_size) return m_size2 - m_value - 1;
                return m_value;
            }

            int operator++ ()
            {
                ++m_value;
                if(m_value >= m_size2) m_value = 0;
                if(m_value >= m_size) return m_size2 - m_value - 1;
                return m_value;
            }
        private:
            int m_size;
            int m_size2;
            int m_add;
            int m_value;
        };



        //--------------------------------------------------wrap_mode_reflect_pow2
        class wrap_mode_reflect_pow2
        {
        public:
            wrap_mode_reflect_pow2() {}
            wrap_mode_reflect_pow2(int size) : m_value(0)
            {
                m_mask = 1;
                m_size = 1;
                while(m_mask < size) 
                {
                    m_mask = (m_mask << 1) | 1;
                    m_size <<= 1;
                }
            }
            int operator() (int v)
            { 
                m_value = int(v) & m_mask;
                if(m_value >= m_size) return m_mask - m_value;
                return m_value;
            }
            int operator++ ()
            {
                ++m_value;
                m_value &= m_mask;
                if(m_value >= m_size) return m_mask - m_value;
                return m_value;
            }
        private:
            int m_size;
            int m_mask;
            int m_value;
        };



        //---------------------------------------------wrap_mode_reflect_auto_pow2
        class wrap_mode_reflect_auto_pow2
        {
        public:
            wrap_mode_reflect_auto_pow2() {}
            wrap_mode_reflect_auto_pow2(int size) :
                m_size(size),
                m_size2(size * 2),
                m_add(m_size2 * (0x3FFFFFFF / m_size2)),
                m_mask((m_size2 & (m_size2-1)) ? 0 : m_size2-1),
                m_value(0)
            {}

            int operator() (int v) 
            { 
                m_value = m_mask ? int(v) & m_mask : 
                                  (int(v) + m_add) % m_size2;
                if(m_value >= m_size) return m_size2 - m_value - 1;
                return m_value;            
            }
            int operator++ ()
            {
                ++m_value;
                if(m_value >= m_size2) m_value = 0;
                if(m_value >= m_size) return m_size2 - m_value - 1;
                return m_value;
            }

        private:
            int m_size;
            int m_size2;
            int m_add;
            int m_mask;
            int m_value;
        };
     */
}