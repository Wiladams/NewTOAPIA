namespace NewTOAPIA.Net
{
    using System;

    // UDT Sequence Number 0 - (2^31 - 1)

    // seqcmp: compare two seq#, considering the wraping
    // seqlen: length from the 1st to the 2nd seq#, including both
    // seqoff: offset from the 2nd to the 1st seq#
    // incseq: increase the seq# by 1
    // decseq: decrease the seq# by 1
    // incseq: increase the seq# by a given offset

    public struct SequenceNumber : IComparable<SequenceNumber>
    {
        public const Int32 SeqNoTH = 0x3FFFFFFF;
        public const Int32 MaxSeqNo = 0x7FFFFFFF;

        int fNumber;

        #region Constructors
        //public SequenceNumber()
        //    :this(new Random().Next(MaxSeqNo))
        //{
        //}

        public SequenceNumber(int seed)
        {
            fNumber = seed;
        }
        #endregion

        #region IComparable
        public int CompareTo(SequenceNumber other)
        {
            return (Math.Abs(fNumber - other.fNumber) < SeqNoTH) ? (fNumber - other.fNumber) : (other.fNumber - fNumber);
        }
        #endregion

        public static int seqoff(Int32 seq1, Int32 seq2)
        {
            if (Math.Abs(seq1 - seq2) < SeqNoTH)
                return seq2 - seq1;

            if (seq1 < seq2)
                return seq2 - seq1 - MaxSeqNo - 1;

            return seq2 - seq1 + MaxSeqNo + 1;
        }

        public static SequenceNumber operator++(SequenceNumber aSeq)
        {
            int aNumber = (aSeq.fNumber == MaxSeqNo) ? 0 : aSeq.fNumber + 1;
            return new SequenceNumber(aNumber);
        }

        public static SequenceNumber operator--(SequenceNumber aSeq)
        {
            int aNumber = (aSeq.fNumber == 0) ? MaxSeqNo : aSeq.fNumber - 1;
            return new SequenceNumber(aNumber);
        }

        public static SequenceNumber operator+(SequenceNumber aSeq, Int32 inc)
        {
            int aNumber = (MaxSeqNo - aSeq.fNumber >= inc) ? aSeq.fNumber + inc : aSeq.fNumber - MaxSeqNo + inc - 1;
            return new SequenceNumber(aNumber);
        }

        public static int operator -(SequenceNumber seq1, SequenceNumber seq2)
        {
            return (seq1.fNumber <= seq2.fNumber) ? (seq2.fNumber - seq1.fNumber + 1) : (seq2.fNumber - seq1.fNumber + MaxSeqNo + 2);
        }

        #region Type Operators
        public static implicit operator int(SequenceNumber aSequence)
        {
            return aSequence.fNumber;
        }

        public static explicit operator SequenceNumber(int aNumber)
        {
            return new SequenceNumber(aNumber);
        }
        #endregion
    }
}