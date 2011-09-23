using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.DirectShow.SBE
{
    /// <summary>
    /// From unnamed structure
    /// </summary>
    public enum RecordingType
    {
        Content = 0, //  no post-recording or overlapped
        Reference //  allows post-recording & overlapped
    }

    /// <summary>
    /// From STREAMBUFFER_ATTR_DATATYPE
    /// </summary>
    public enum StreamBufferAttrDataType
    {
        DWord = 0,
        String = 1,
        Binary = 2,
        Bool = 3,
        QWord = 4,
        Word = 5,
        Guid = 6
    }

    /// <summary>
    /// From unnamed structure
    /// </summary>
    public enum StreamBufferEventCode
    {
        TimeHole = 0x0326, // STREAMBUFFER_EC_TIMEHOLE
        StaleDataRead, // STREAMBUFFER_EC_STALE_DATA_READ
        StaleFileDeleted, // STREAMBUFFER_EC_STALE_FILE_DELETED
        ContentBecomingStale, // STREAMBUFFER_EC_CONTENT_BECOMING_STALE
        WriteFailure, // STREAMBUFFER_EC_WRITE_FAILURE
        ReadFailure, // STREAMBUFFER_EC_READ_FAILURE
        RateChanged, // STREAMBUFFER_EC_RATE_CHANGED
        PrimaryAudio // STREAMBUFFER_EC_PRIMARY_AUDIO
    }

}
