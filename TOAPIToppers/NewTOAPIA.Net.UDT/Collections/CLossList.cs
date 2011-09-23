namespace NewTOAPIA.Net.Udt
{
    public class CLossList
    {
        protected int[] m_piData1;  // sequence number starts
        protected int[] m_piData2;  // seqnence number ends
        protected int[] m_piNext;   // next node in the list

        int m_iSize;                         // size of the static array
        int m_iHead;                         // first node
        int m_iLength;                       // loss length

        public CLossList(int size)
        {
            m_iSize = size;
            m_iHead = -1;
            m_iLength = 0;

            m_piData1 = new Int32[m_iSize];
            m_piData2 = new Int32[m_iSize];
            m_piNext = new int[m_iSize];

            // -1 means there is no data in the node
            for (int i = 0; i < size; ++i)
            {
                m_piData1[i] = -1;
                m_piData2[i] = -1;
            }
        }

        public virtual int insert(int seqno1, int seqno2)
        {
            return 0;
        }

        public virtual bool remove(int seqno)
        {
            return false;
        }

        // Functionality:
        //    Read the loss length.
        // Parameters:
        //    None.
        // Returned value:
        //    the length of the list.

        public virtual int getLossLength()
        {
            return m_iLength;
        }
    }
}