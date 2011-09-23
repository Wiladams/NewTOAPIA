using System;
using System.Collections.Generic;
using System.Text;

namespace TOAPI.Dhcpcsvc
{
    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct MCAST_LEASE_REQUEST
    {

        /// time_t->__time32_t->int
        public int LeaseStartTime;

        /// time_t->__time32_t->int
        public int MaxLeaseStartTime;

        /// DWORD->unsigned int
        public uint LeaseDuration;

        /// DWORD->unsigned int
        public uint MinLeaseDuration;

        /// IPNG_ADDRESS->_IPNG_ADDRESS
        public IPNG_ADDRESS ServerAddress;

        /// WORD->unsigned short
        public ushort MinAddrCount;

        /// WORD->unsigned short
        public ushort AddrCount;

        /// PBYTE->BYTE*
        public System.IntPtr pAddrBuf;
    }
}
