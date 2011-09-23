using System;
using System.Runtime.InteropServices;

namespace TOAPI.Types
{
    [Serializable]
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct POINT : IEquatable<POINT>
    {
        public int X;
        public int Y;

        public POINT(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public bool Equals(POINT rhs)
        {
            return (rhs.X == this.X) && (rhs.Y == this.Y);
        }
    }
}
