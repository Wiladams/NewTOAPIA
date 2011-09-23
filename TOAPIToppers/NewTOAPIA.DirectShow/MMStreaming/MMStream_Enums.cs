using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.DirectShow.MMStreaming
{
    /// <summary>
    /// From unnamed enum
    /// </summary>
    [Flags]
    public enum AMMStream
    {
        None = 0x0,
        AddDefaultRenderer = 0x1,
        CreatePeer = 0x2,
        StopIfNoSamples = 0x4,
        NoStall = 0x8
    }

    /// <summary>
    /// From unnamed enum
    /// </summary>
    [Flags]
    public enum AMMMultiStream
    {
        None = 0x0,
        NoGraphThread = 0x1
    }

    /// <summary>
    /// From unnamed enum
    /// </summary>
    [Flags]
    public enum AMOpenModes
    {
        RenderTypeMask = 0x3,
        RenderToExisting = 0,
        RenderAllStreams = 0x1,
        NoRender = 0x2,
        NoClock = 0x4,
        Run = 0x8
    }

    /// <summary>
    /// From COMPLETION_STATUS_FLAGS
    /// </summary>
    [Flags]
    public enum CompletionStatusFlags
    {
        None = 0x0,
        NoUpdateOk = 0x1,
        Wait = 0x2,
        Abort = 0x4
    }

    /// <summary>
    /// From unnamed enum
    /// </summary>
    [Flags]
    public enum SSUpdate
    {
        None = 0x0,
        ASync = 0x1,
        Continuous = 0x2
    }

    /// <summary>
    /// From STREAM_STATE
    /// </summary>
    public enum StreamState
    {
        // Fields
        Run = 1,
        Stop = 0
    }

    /// <summary>
    /// From STREAM_TYPE
    /// </summary>
    public enum StreamType
    {
        // Fields
        Read = 0,
        Transform = 2,
        Write = 1
    }

    /// <summary>
    /// From unnamed enum
    /// </summary>
    public enum MMSSF
    {
        HasClock = 0x1,
        SupportSeek = 0x2,
        Asynchronous = 0x4
    }
}
