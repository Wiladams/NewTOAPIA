using System;
using System.Collections.Generic;
using System.Text;

namespace TOAPI.Dhcpcsvc
{
    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct MCAST_SCOPE_CTX
    {
        public IPNG_ADDRESS ScopeID;    /// IPNG_ADDRESS->_IPNG_ADDRESS
        public IPNG_ADDRESS Interface;  /// IPNG_ADDRESS->_IPNG_ADDRESS
        public IPNG_ADDRESS ServerID;   /// IPNG_ADDRESS->_IPNG_ADDRESS
    }
}
