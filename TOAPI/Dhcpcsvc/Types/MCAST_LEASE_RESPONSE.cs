using System;

namespace TOAPI.Dhcpcsvc
{
    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct MCAST_LEASE_RESPONSE
    {
        public int LeaseStartTime;          /// time_t->__time32_t->int
        public int LeaseEndTime;            /// time_t->__time32_t->int
        public IPNG_ADDRESS ServerAddress;  /// IPNG_ADDRESS->_IPNG_ADDRESS
        public ushort AddrCount;            /// WORD->unsigned short
        public System.IntPtr pAddrBuf;      /// PBYTE->BYTE*
    }
}
