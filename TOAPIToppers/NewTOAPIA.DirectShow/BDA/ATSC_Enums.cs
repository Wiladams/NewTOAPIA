using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.DirectShow.BDA
{

    /// <summary>
    /// From ATSC_ETM_LOCATION_*
    /// </summary>
    public enum AtscEtmLocation
    {
        NotPresent = 0x00,
        InPtcForPsip = 0x01,
        InPtcForEvent = 0x02,
        Reserved = 0x03,
    }

}
