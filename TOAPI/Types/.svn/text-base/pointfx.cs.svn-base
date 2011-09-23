using System;
using System.Runtime.InteropServices;

namespace TOAPI.Types
{
    [StructLayout(LayoutKind.Sequential)]
    public struct POINTFX
    {
        public FIXED x;
        public FIXED y;

        public static bool operator ==(POINTFX a, POINTFX b)
        {
            return (a.x == b.x && a.y == b.y);
        }

        public static bool operator !=(POINTFX a, POINTFX b)
        {
            return (a.x != b.x || a.y != b.y);
        }

        public override bool Equals(object obj)
        {
            return (x == ((POINTFX)obj).x && y == ((POINTFX)obj).y);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
