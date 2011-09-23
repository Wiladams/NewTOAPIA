namespace NewTOAPIA.Net.Udt
{
    using System;


    // UDT ACK Sub-sequence Number: 0 - (2^31 - 1)

    public class CAckNo
    {
        public const int m_iMaxAckSeqNo = 0x7FFFFFFF;  // maximum ACK sub-sequence number used in UDT

        internal int fNumber;

        public CAckNo(int aNumber)
        {
            fNumber = aNumber;
        }

        public static Int32 incack(Int32 ackno)
        {
            return (ackno == m_iMaxAckSeqNo) ? 0 : ackno + 1;
        }

        public static implicit operator int(CAckNo aNumber)
        {
            return aNumber.fNumber;
        }

        public static explicit operator CAckNo(int aNumber)
        {
            return new CAckNo(aNumber);
        }

        public static CAckNo operator ++ (CAckNo aNumber)
        {
            Int32 newValue = (aNumber.fNumber == CAckNo.m_iMaxAckSeqNo) ? 0 : aNumber.fNumber + 1;
            return new CAckNo(newValue);
        }
    }
}