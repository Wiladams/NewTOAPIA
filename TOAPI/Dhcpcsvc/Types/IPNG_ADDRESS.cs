using System;
using System.Runtime.InteropServices;

namespace TOAPI.Dhcpcsvc
{

    [StructLayoutAttribute(LayoutKind.Explicit)]
    public struct IPNG_ADDRESS
    {
        /// DWORD->unsigned int
        [FieldOffsetAttribute(0)]
        public uint IpAddrV4;

        /// BYTE[16]
        /// 
        //[FieldOffsetAttribute(0)]
        //public IpAddrV6_Struct IpAddrV6;
    }

    public struct IpAddrV6_Struct
    {
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
        public byte[] IpAddrV6;

        public void Init()
        {
            IpAddrV6 = new byte[16];
        }
    }
}
