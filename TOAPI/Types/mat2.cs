using System;
using System.Runtime.InteropServices;

namespace TOAPI.Types
{
    [StructLayout(LayoutKind.Sequential)]
    public class MAT2
    {
        public FIXED eM11;
        public FIXED eM12;
        public FIXED eM21;
        public FIXED eM22;

        public MAT2()
        {
            eM11 = new FIXED(1, 0);
            eM12 = new FIXED(0, 0);
            eM21 = new FIXED(0, 0);
            eM22 = new FIXED(1, 0);
        }
    }
}
