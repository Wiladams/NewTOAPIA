using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.DirectShow
{
    /// <summary>
    /// From MPEG_REQUEST_TYPE
    /// </summary>
    public enum MPEGRequestType
    {
        // Fields
        PES_STREAM = 6,
        SECTION = 1,
        SECTION_ASYNC = 2,
        SECTIONS_STREAM = 5,
        TABLE = 3,
        TABLE_ASYNC = 4,
        TS_STREAM = 7,
        START_MPE_STREAM = 8,
        UNKNOWN = 0
    }

    /// <summary>
    /// From MPEG_CONTEXT_TYPE
    /// </summary>
    public enum MPEGContextType
    {
        BCSDeMux = 0,
        WinSock = 1
    }
}
