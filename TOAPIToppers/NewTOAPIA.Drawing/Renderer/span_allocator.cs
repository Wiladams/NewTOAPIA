
namespace NewTOAPIA.Drawing
{
    //----------------------------------------------------------span_allocator
    public class span_allocator
    {
        private ArrayPOD<RGBA_Bytes> m_span;

        public span_allocator()
        {
            m_span = new ArrayPOD<RGBA_Bytes>(255);
        }

        //--------------------------------------------------------------------
        public ArrayPOD<RGBA_Bytes> allocate(int span_len)
        {
            if (span_len > m_span.size())
            {
                // To reduce the number of reallocs we align the 
                // span_len to 256 color elements. 
                // Well, I just like this number and it looks reasonable.
                //-----------------------
                m_span.resize((((int)span_len + 255) >> 8) << 8);
            }
            return m_span;
        }

        public ArrayPOD<RGBA_Bytes> span() { return m_span; }
        public int max_span_len() { return m_span.size(); }
    };
}
