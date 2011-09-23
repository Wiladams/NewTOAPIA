namespace NewTOAPIA.Net.Udt
{
    using System;

    public class CUDTException : Exception
    {
        int m_iMajor;       // major exception categories
        // 0: correct condition
        // 1: network setup exception
        // 2: network connection broken
        // 3: memory exception
        // 4: file exception
        // 5: method not supported
        // 6+: undefined error

        int m_iMinor;       // for specific error reasons
        int m_iErrno;       // errno returned by the system if there is any
        string m_strMsg;    // text error message

        public CUDTException()
            : this(0, 0, -1)
        {
        }

        public CUDTException(int major, int minor, int err)
        {
            m_iMajor = major;
            m_iMinor = minor;

            // if (-1 == err)
            //      m_iErrno = GetLastError();
            //else
            //   m_iErrno = err;
        }

        public CUDTException(CUDTException e)
        {
            m_iMajor = (e.m_iMajor);
            m_iMinor = (e.m_iMinor);
            m_iErrno = (e.m_iErrno);
        }

        public string getErrorMessage()
        {
            // translate "Major:Minor" code into text message.

            switch (m_iMajor)
            {
                case 0:
                    m_strMsg = "Success";
                    break;

                case 1:
                    m_strMsg = "Connection setup failure";

                    switch (m_iMinor)
                    {
                        case 1:
                            m_strMsg += ": connection time out";
                            break;

                        case 2:
                            m_strMsg += ": connection rejected";
                            break;

                        case 3:
                            m_strMsg += ": unable to create/configure UDP socket";
                            break;

                        case 4:
                            m_strMsg += ": abort for security reasons";
                            break;

                        default:
                            break;
                    }

                    break;

                case 2:
                    switch (m_iMinor)
                    {
                        case 1:
                            m_strMsg = "Connection was broken";
                            break;

                        case 2:
                            m_strMsg = "Connection does not exist";
                            break;

                        default:
                            break;
                    }

                    break;

                case 3:
                    m_strMsg = "System resource failure";

                    switch (m_iMinor)
                    {
                        case 1:
                            m_strMsg += ": unable to create new threads";
                            break;

                        case 2:
                            m_strMsg += ": unable to allocate buffers";
                            break;

                        default:
                            break;
                    }

                    break;

                case 4:
                    m_strMsg = "File system failure";

                    switch (m_iMinor)
                    {
                        case 1:
                            m_strMsg += ": cannot seek read position";
                            break;

                        case 2:
                            m_strMsg += ": failure in read";
                            break;

                        case 3:
                            m_strMsg += ": cannot seek write position";
                            break;

                        case 4:
                            m_strMsg += ": failure in write";
                            break;

                        default:
                            break;
                    }

                    break;

                case 5:
                    m_strMsg = "Operation not supported";

                    switch (m_iMinor)
                    {
                        case 1:
                            m_strMsg += ": Cannot do this operation on a BOUND socket";
                            break;

                        case 2:
                            m_strMsg += ": Cannot do this operation on a CONNECTED socket";
                            break;

                        case 3:
                            m_strMsg += ": Bad parameters";
                            break;

                        case 4:
                            m_strMsg += ": Invalid socket ID";
                            break;

                        case 5:
                            m_strMsg += ": Cannot do this operation on an UNBOUND socket";
                            break;

                        case 6:
                            m_strMsg += ": Socket is not in listening state";
                            break;

                        case 7:
                            m_strMsg += ": Listen/accept is not supported in rendezous connection setup";
                            break;

                        case 8:
                            m_strMsg += ": Cannot call connect on UNBOUND socket in rendezvous connection setup";
                            break;

                        case 9:
                            m_strMsg += ": This operation is not supported in SOCK_STREAM mode";
                            break;

                        case 10:
                            m_strMsg += ": This operation is not supported in SOCK_DGRAM mode";
                            break;

                        case 11:
                            m_strMsg += ": Another socket is already listening on the same port";
                            break;

                        case 12:
                            m_strMsg += ": Message is too large to send (it must be less than the UDT send buffer size)";
                            break;

                        default:
                            break;
                    }

                    break;

                case 6:
                    m_strMsg = "Non-blocking call failure";

                    switch (m_iMinor)
                    {
                        case 1:
                            m_strMsg += ": no buffer available for sending";
                            break;

                        case 2:
                            m_strMsg += ": no data available for reading";
                            break;

                        default:
                            break;
                    }

                    break;

                default:
                    m_strMsg = "Unknown error";
                    break;
            }

            // Adding "errno" information
            if ((0 != m_iMajor) && (0 < m_iErrno))
            {
                m_strMsg += ": ";
                //LPVOID lpMsgBuf;
                //FormatMessage(FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM | FORMAT_MESSAGE_IGNORE_INSERTS, null, m_iErrno, MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT), (LPTSTR)&lpMsgBuf, 0, null);
                //m_strMsg += (char*)lpMsgBuf;
                //LocalFree(lpMsgBuf);
            }

            return m_strMsg;
        }

        int getErrorCode()
        {
            return m_iMajor * 1000 + m_iMinor;
        }

        void clear()
        {
            m_iMajor = 0;
            m_iMinor = 0;
            m_iErrno = 0;
        }

        public const int SUCCESS = 0;
        public const int ECONNSETUP = 1000;
        public const int ENOSERVER = 1001;
        public const int ECONNREJ = 1002;
        public const int ESOCKFAIL = 1003;
        public const int ESECFAIL = 1004;
        public const int ECONNFAIL = 2000;
        public const int ECONNLOST = 2001;
        public const int ENOCONN = 2002;
        public const int ERESOURCE = 3000;
        public const int ETHREAD = 3001;
        public const int ENOBUF = 3002;
        public const int EFILE = 4000;
        public const int EINVRDOFF = 4001;
        public const int ERDPERM = 4002;
        public const int EINVWROFF = 4003;
        public const int EWRPERM = 4004;
        public const int EINVOP = 5000;
        public const int EBOUNDSOCK = 5001;
        public const int ECONNSOCK = 5002;
        public const int EINVPARAM = 5003;
        public const int EINVSOCK = 5004;
        public const int EUNBOUNDSOCK = 5005;
        public const int ENOLISTEN = 5006;
        public const int ERDVNOSERV = 5007;
        public const int ERDVUNBOUND = 5008;
        public const int ESTREAMILL = 5009;
        public const int EDGRAMILL = 5010;
        public const int EDUPLISTEN = 5011;
        public const int ELARGEMSG = 5012;
        public const int EASYNCFAIL = 6000;
        public const int EASYNCSND = 6001;
        public const int EASYNCRCV = 6002;
        public const int EUNKNOWN = -1;
    }
}