using System;
using System.Runtime.InteropServices;

namespace TOAPI.Types
{
    [StructLayout(LayoutKind.Sequential)]
    public struct FIXED
    {
        // Do not change the order of these
        public ushort fract;
        public short val;

        public FIXED(short value, ushort fraction)
        {
            val = value;
            fract = fraction;
        }

        public float ToSingle()
        {
            return (float)val + ((float)fract) / 65536f;
        }

        public static bool operator ==(FIXED a, FIXED b)
        {
            return (a.val == b.val && a.fract == b.fract);
        }

        public static bool operator !=(FIXED a, FIXED b)
        {
            return (a.val != b.val || a.fract != b.fract);
        }

        public override bool Equals(object obj)
        {
            return (val == ((FIXED)obj).val && fract == ((FIXED)obj).fract);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
