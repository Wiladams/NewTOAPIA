using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.DirectShow.BDA
{
    /// <summary>
    /// Define possible values for a running_status field according to ETSI EN 300 468
    /// This enum doesn't exist in the c++ headers
    /// </summary>
    public enum RunningStatus : byte
    {
        Undefined = 0,
        NotRunning = 1,
        StartInAFewSeconds = 2,
        Pausing = 3,
        Running = 4,
        Reserved1 = 5,
        Reserved2 = 6,
        Reserved3 = 7
    }
}
