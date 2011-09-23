namespace NewTOAPIA.Net.Udt
{
    using System;

    // UDT Sequence Number 0 - (2^31 - 1)

    // seqcmp: compare two seq#, considering the wraping
    // seqlen: length from the 1st to the 2nd seq#, including both
    // seqoff: offset from the 2nd to the 1st seq#
    // incseq: increase the seq# by 1
    // decseq: decrease the seq# by 1
    // incseq: increase the seq# by a given offset

    public class CSeqNo
    {
        public const Int32 m_iSeqNoTH = 0x3FFFFFFF;
        public const Int32 m_iMaxSeqNo = 0x7FFFFFFF;

        public static int seqcmp(Int32 seq1, Int32 seq2)
        {
            return (Math.Abs(seq1 - seq2) < m_iSeqNoTH) ? (seq1 - seq2) : (seq2 - seq1);
        }

        public static int seqlen(Int32 seq1, Int32 seq2)
        {
            return (seq1 <= seq2) ? (seq2 - seq1 + 1) : (seq2 - seq1 + m_iMaxSeqNo + 2);
        }

        public static int seqoff(Int32 seq1, Int32 seq2)
        {
            if (Math.Abs(seq1 - seq2) < m_iSeqNoTH)
                return seq2 - seq1;

            if (seq1 < seq2)
                return seq2 - seq1 - m_iMaxSeqNo - 1;

            return seq2 - seq1 + m_iMaxSeqNo + 1;
        }

        public static Int32 incseq(Int32 seq)
        {
            return (seq == m_iMaxSeqNo) ? 0 : seq + 1;
        }

        public static Int32 decseq(Int32 seq)
        {
            return (seq == 0) ? m_iMaxSeqNo : seq - 1;
        }

        public static Int32 incseq(Int32 seq, Int32 inc)
        {
            return (m_iMaxSeqNo - seq >= inc) ? seq + inc : seq - m_iMaxSeqNo + inc - 1;
        }

    }
}