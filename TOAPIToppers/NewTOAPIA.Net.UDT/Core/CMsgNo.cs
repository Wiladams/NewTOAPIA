namespace NewTOAPIA.Net.Udt
{
    using System;

    // UDT Message Number: 0 - (2^29 - 1)

    public class CMsgNo
    {
        public const Int32 m_iMsgNoTH = 0xFFFFFFF;   // threshold for comparing msg. no.
        public const Int32 m_iMaxMsgNo = 0x1FFFFFFF;     // maximum message number used in UDT

        public static int msgcmp(Int32 msgno1, Int32 msgno2)
        { return (Math.Abs(msgno1 - msgno2) < m_iMsgNoTH) ? (msgno1 - msgno2) : (msgno2 - msgno1); }

        public static int msglen(Int32 msgno1, Int32 msgno2)
        { return (msgno1 <= msgno2) ? (msgno2 - msgno1 + 1) : (msgno2 - msgno1 + m_iMaxMsgNo + 2); }

        public static int msgoff(Int32 msgno1, Int32 msgno2)
        {
            if (Math.Abs(msgno1 - msgno2) < m_iMsgNoTH)
                return msgno2 - msgno1;

            if (msgno1 < msgno2)
                return msgno2 - msgno1 - m_iMaxMsgNo - 1;

            return msgno2 - msgno1 + m_iMaxMsgNo + 1;
        }

        public static Int32 incmsg(Int32 msgno)
        { return (msgno == m_iMaxMsgNo) ? 0 : msgno + 1; }

    }
}