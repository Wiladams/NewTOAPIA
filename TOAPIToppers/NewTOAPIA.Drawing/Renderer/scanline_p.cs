
namespace NewTOAPIA.Drawing
{
    using System;

    //=============================================================scanline_p8
    // 
    // This is a general purpose scanline container which supports the interface 
    // used in the rasterizer::render(). See description of scanline_u8
    // for details.
    // 
    //------------------------------------------------------------------------
    public sealed class scanline_packed_8 : IScanlineCache
    {
        private int m_last_x;
        private int m_y;
        private byte[] m_covers;
        private int m_cover_index;
        private ScanlineSpan[] m_spans;
        private int m_span_index;
        private int m_interator_index;

        public ScanlineSpan GetNextScanlineSpan()
        {
            m_interator_index++;
            return m_spans[m_interator_index - 1];
        }

        public scanline_packed_8()
        {
            m_last_x = 0x7FFFFFF0;
            m_covers = new byte[1000];
            m_spans = new ScanlineSpan[1000];
        }

        //--------------------------------------------------------------------
        public void reset(int min_x, int max_x)
        {
            int max_len = max_x - min_x + 3;
            if (max_len > m_spans.Length)
            {
                m_spans = new ScanlineSpan[max_len];
                m_covers = new byte[max_len];
            }
            m_last_x = 0x7FFFFFF0;
            m_cover_index = 0;
            m_span_index = 0;
            m_spans[m_span_index].len = 0;
        }

        //--------------------------------------------------------------------
        public void add_cell(int x, int cover)
        {
            m_covers[m_cover_index] = (byte)cover;
            if (x == m_last_x + 1 && m_spans[m_span_index].len > 0)
            {
                m_spans[m_span_index].len++;
            }
            else
            {
                m_span_index++;
                m_spans[m_span_index].cover_index = m_cover_index;
                m_spans[m_span_index].x = (short)x;
                m_spans[m_span_index].len = 1;
            }
            m_last_x = x;
            m_cover_index++;
        }

        //--------------------------------------------------------------------
        public void add_cells(int x, int len, byte[] covers, int coversIndex)
        {
            for (int i = 0; i < len; i++)
            {
                m_covers[m_cover_index + i] = covers[i];
            }

            if (x == m_last_x + 1 && m_spans[m_span_index].len > 0)
            {
                m_spans[m_span_index].len += (short)len;
            }
            else
            {
                m_span_index++;
                m_spans[m_span_index].cover_index = m_cover_index;
                m_spans[m_span_index].x = (short)x;
                m_spans[m_span_index].len = (short)len;
            }

            m_cover_index += len;
            m_last_x = x + (int)len - 1;
        }

        //--------------------------------------------------------------------
        public void add_span(int x, int len, int cover)
        {
            if (x == m_last_x + 1
                && m_spans[m_span_index].len < 0
                && cover == m_spans[m_span_index].cover_index)
            {
                m_spans[m_span_index].len -= (short)len;
            }
            else
            {
                m_covers[m_cover_index] = (byte)cover;
                m_span_index++;
                m_spans[m_span_index].cover_index = m_cover_index++;
                m_spans[m_span_index].x = (short)x;
                m_spans[m_span_index].len = (short)(-(int)(len));
            }
            m_last_x = x + (int)len - 1;
        }

        //--------------------------------------------------------------------
        public void finalize(int y)
        {
            m_y = y;
        }

        //--------------------------------------------------------------------
        public void reset_spans()
        {
            m_last_x = 0x7FFFFFF0;
            m_cover_index = 0;
            m_span_index = 0;
            m_spans[m_span_index].len = 0;
        }

        public int y() { return m_y; }
        public int num_spans() { return (int)m_span_index; }
        public ScanlineSpan Begin()
        {
            m_interator_index = 1;
            return GetNextScanlineSpan();
        }

        public byte[] GetCovers()
        {
            return m_covers;
        }
    }
}
