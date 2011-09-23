
namespace TOAPI.Winsock
{
    public enum IPV4AddressClass : uint
    {
        ClassA = NetUtils.IPV4ClassAValue,
        ClassB = NetUtils.IPV4ClassBValue,
        ClassC = NetUtils.IPV4ClassCValue,
        ClassD = NetUtils.IPV4ClassDValue,
        ClassE = NetUtils.IPV4ClassEValue,
        Invalid = uint.MaxValue
    }

    public class NetUtils
    {
        public const uint IPV4ClassAMask = unchecked(0x80000000);
        public const uint IPV4ClassBMask = unchecked(0xC0000000);
        public const uint IPV4ClassCMask = unchecked(0xE0000000);
        public const uint IPV4ClassDMask = unchecked(0xF0000000);
        public const uint IPV4ClassEMask = unchecked(0xF0000000);


        public const uint IPV4ClassAValue = unchecked(0x00000000);
        public const uint IPV4ClassBValue = unchecked(0x80000000);
        public const uint IPV4ClassCValue = unchecked(0xC0000000);
        public const uint IPV4ClassDValue = unchecked(0xE0000000);
        public const uint IPV4ClassEValue = unchecked(0xF0000000);

        static IPV4AddressClass GetIPV4AddressClass(int anIPV4Address)
        {
            // First mask off the address to get the address class component
            uint maskedValue = ((uint)unchecked(anIPV4Address)) & IPV4ClassEMask;

            switch (maskedValue)
            {
                case IPV4ClassAValue:
                    return IPV4AddressClass.ClassA;
                case IPV4ClassBValue:
                    return IPV4AddressClass.ClassB;
                case IPV4ClassCValue:
                    return IPV4AddressClass.ClassC;
                case IPV4ClassDValue:
                    return IPV4AddressClass.ClassD;
                case IPV4ClassEValue:
                    return IPV4AddressClass.ClassE;
                default:
                    return IPV4AddressClass.Invalid;
            }
        }
    }
}
