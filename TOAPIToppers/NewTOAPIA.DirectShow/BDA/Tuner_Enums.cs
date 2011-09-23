using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.DirectShow.BDA
{
    /// <summary>
    /// From AMTunerModeType
    /// </summary>
    [Flags]
    public enum AMTunerModeType
    {
        Default = 0x0000,
        TV = 0x0001,
        FMRadio = 0x0002,
        AMRadio = 0x0004,
        Dss = 0x0008,
        DTV = 0x0010
    }

    /// <summary>
    /// From TunerInputType
    /// </summary>
    public enum TunerInputType
    {
        Cable,
        Antenna
    }
}
