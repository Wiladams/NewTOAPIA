using System;
using System.Runtime.InteropServices;

namespace TOAPI.Types
{
    // -- TIMECAPS -- 
    [StructLayout(LayoutKind.Sequential)]
    public struct TIMECAPS
    {
        public uint wPeriodMin;
        public uint wPeriodMax;
    }
}
