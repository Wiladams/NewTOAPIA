namespace NewTOAPIA.DirectShow
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// From AM_SEEKING_SeekingCapabilities
    /// </summary>
    [ComVisible(false), Flags]
    public enum AMSeekingSeekingCapabilities
    {
        None = 0,
        CanSeekAbsolute = 0x001,
        CanSeekForwards = 0x002,
        CanSeekBackwards = 0x004,
        CanGetCurrentPos = 0x008,
        CanGetStopPos = 0x010,
        CanGetDuration = 0x020,
        CanPlayBackwards = 0x040,
        CanDoSegments = 0x080,
        Source = 0x100
    }

    /// <summary>
    /// From AM_SEEKING_SeekingFlags
    /// </summary>
    [ComVisible(false), Flags]
    public enum AMSeekingSeekingFlags
    {
        NoPositioning = 0x00,
        AbsolutePositioning = 0x01,
        RelativePositioning = 0x02,
        IncrementalPositioning = 0x03,
        PositioningBitsMask = 0x03,
        SeekToKeyFrame = 0x04,
        ReturnTime = 0x08,
        Segment = 0x10,
        NoFlush = 0x20
    }


    /// <summary>
    /// From DISPATCH_* defines
    /// </summary>
    [Flags]
    public enum DispatchFlags : short
    {
        None = 0x0,
        Method = 0x1,
        PropertyGet = 0x2,
        PropertyPut = 0x4,
        PropertyPutRef = 0x8
    }

    /// <summary>
    /// From #define OATRUE/OAFALSE
    /// </summary>
    public enum OABool
    {
        False = 0,
        True = -1 // bools in .NET use 1, not -1
    }

    /// <summary>
    /// From TVAudioMode
    /// </summary>
    [Flags]
    public enum TVAudioMode
    {
        None = 0,
        Mono = 0x0001,
        Stereo = 0x0002,
        LangA = 0x0010,
        LangB = 0x0020,
        LangC = 0x0040,
    }

    /// <summary>
    /// From WS_* defines
    /// </summary>
    [Flags]
    public enum WindowStyle
    {
        Overlapped = 0x00000000,
        Popup = unchecked((int)0x80000000), // enum can't be uint for VB
        Child = 0x40000000,
        Minimize = 0x20000000,
        Visible = 0x10000000,
        Disabled = 0x08000000,
        ClipSiblings = 0x04000000,
        ClipChildren = 0x02000000,
        Maximize = 0x01000000,
        Caption = 0x00C00000,
        Border = 0x00800000,
        DlgFrame = 0x00400000,
        VScroll = 0x00200000,
        HScroll = 0x00100000,
        SysMenu = 0x00080000,
        ThickFrame = 0x00040000,
        Group = 0x00020000,
        TabStop = 0x00010000,
        MinimizeBox = 0x00020000,
        MaximizeBox = 0x00010000
    }

    /// <summary>
    /// From WS_EX_* defines
    /// </summary>
    [Flags]
    public enum WindowStyleEx
    {
        DlgModalFrame = 0x00000001,
        NoParentNotify = 0x00000004,
        Topmost = 0x00000008,
        AcceptFiles = 0x00000010,
        Transparent = 0x00000020,
        MDIChild = 0x00000040,
        ToolWindow = 0x00000080,
        WindowEdge = 0x00000100,
        ClientEdge = 0x00000200,
        ContextHelp = 0x00000400,
        Right = 0x00001000,
        Left = 0x00000000,
        RTLReading = 0x00002000,
        LTRReading = 0x00000000,
        LeftScrollBar = 0x00004000,
        RightScrollBar = 0x00000000,
        ControlParent = 0x00010000,
        StaticEdge = 0x00020000,
        APPWindow = 0x00040000,
        Layered = 0x00080000,
        NoInheritLayout = 0x00100000,
        LayoutRTL = 0x00400000,
        Composited = 0x02000000,
        NoActivate = 0x08000000
    }

    /// <summary>
    /// From SW_* defines
    /// </summary>
    public enum WindowState
    {
        Hide = 0,
        Normal,
        ShowMinimized,
        ShowMaximized,
        ShowNoActivate,
        Show,
        Minimize,
        ShowMinNoActive,
        ShowNA,
        Restore,
        ShowDefault,
        ForceMinimize
    }
    
    /// <summary>
    /// From AMPROPERTY_PIN
    /// </summary>
    public enum AMPropertyPin
    {
        Category,
        Medium
    }

    /// <summary>
    /// From _AM_RENSDEREXFLAGS
    /// </summary>
    [Flags]
    public enum AMRenderExFlags
    {
        None = 0,
        RenderToExistingRenderers = 1
    }

    /// <summary>
    /// From AnalogVideoStandard
    /// </summary>
    [Flags]
    public enum AnalogVideoStandard
    {
        None = 0x00000000,
        NTSC_M = 0x00000001,
        NTSC_M_J = 0x00000002,
        NTSC_433 = 0x00000004,
        PAL_B = 0x00000010,
        PAL_D = 0x00000020,
        PAL_G = 0x00000040,
        PAL_H = 0x00000080,
        PAL_I = 0x00000100,
        PAL_M = 0x00000200,
        PAL_N = 0x00000400,
        PAL_60 = 0x00000800,
        SECAM_B = 0x00001000,
        SECAM_D = 0x00002000,
        SECAM_G = 0x00004000,
        SECAM_H = 0x00008000,
        SECAM_K = 0x00010000,
        SECAM_K1 = 0x00020000,
        SECAM_L = 0x00040000,
        SECAM_L1 = 0x00080000,
        PAL_N_COMBO = 0x00100000,

        NTSCMask = 0x00000007,
        PALMask = 0x00100FF0,
        SECAMMask = 0x000FF000
    }

    /// <summary>
    /// From FILTER_STATE
    /// </summary>
    public enum FilterState
    {
        Stopped,
        Paused,
        Running
    }


    /// <summary>
    /// From KSPROPERTY_SUPPORT_* defines
    /// </summary>
    public enum KSPropertySupport
    {
        Get = 1,
        Set = 2
    }

    // event notification codes
    //
    [ComVisible(false)]
    public enum EventCode
    {
        // EvCod.h
        Complete = 0x01,        // EC_COMPLETE
        UserAbort = 0x02,       // EC_USERABORT
        ErrorAbort = 0x03,      // EC_ERRORABORT
        Time = 0x04,            // EC_TIME
        Repaint = 0x05,         // EC_REPAINT
        StErrStopped = 0x06,    // EC_STREAM_ERROR_STOPPED
        StErrStPlaying = 0x07,  // EC_STREAM_ERROR_STILLPLAYING
        ErrorStPlaying = 0x08,  // EC_ERROR_STILLPLAYING
        PaletteChanged = 0x09,  // EC_PALETTE_CHANGED
        VideoSizeChanged = 0x0a, // EC_VIDEO_SIZE_CHANGED
        QualityChange = 0x0b,   // EC_QUALITY_CHANGE
        ShuttingDown = 0x0c,    // EC_SHUTTING_DOWN
        ClockChanged = 0x0d,    // EC_CLOCK_CHANGED
        Paused = 0x0e,          // EC_PAUSED
        OpeningFile = 0x10,     // EC_OPENING_FILE
        BufferingData = 0x11,   // EC_BUFFERING_DATA
        FullScreenLost = 0x12,  // EC_FULLSCREEN_LOST
        Activate = 0x13,        // EC_ACTIVATE
        NeedRestart = 0x14,     // EC_NEED_RESTART
        WindowDestroyed = 0x15, // EC_WINDOW_DESTROYED
        DisplayChanged = 0x16,  // EC_DISPLAY_CHANGED
        Starvation = 0x17,      // EC_STARVATION
        OleEvent = 0x18,        // EC_OLE_EVENT
        NotifyWindow = 0x19,    // EC_NOTIFY_WINDOW
        StreamControlStopped = 0x1A, // EC_STREAM_CONTROL_STOPPED
        StreamControlStarted = 0x1B, // EC_STREAM_CONTROL_STARTED
        EndOfSegment = 0x1C,    // EC_END_OF_SEGMENT
        SegmentStarted = 0x1D,  // EC_SEGMENT_STARTED
        LengthChanged = 0x1E,   // EC_LENGTH_CHANGED
        DeviceLost = 0x1f,      // EC_DEVICE_LOST
        StepComplete = 0x24,    // EC_STEP_COMPLETE
        TimeCodeAvailable = 0x30, // EC_TIMECODE_AVAILABLE
        ExtDeviceModeChange = 0x31, // EC_EXTDEVICE_MODE_CHANGE
        StateChange = 0x32,     // EC_STATE_CHANGE
        GraphChanged = 0x50,    // EC_GRAPH_CHANGED
        ClockUnset = 0x51,      // EC_CLOCK_UNSET
        VMRRenderDeviceSet = 0x53, // EC_VMR_RENDERDEVICE_SET
        VMRSurfaceFlipped = 0x54, // EC_VMR_SURFACE_FLIPPED
        VMRReconnectionFailed = 0x55, // EC_VMR_RECONNECTION_FAILED
        PreprocessComplete = 0x56, // EC_PREPROCESS_COMPLETE
        CodecApiEvent = 0x57,   // EC_CODECAPI_EVENT

        // DVDevCod.h
        DvdDomainChange = 0x101, // EC_DVD_DOMAIN_CHANGE
        DvdTitleChange = 0x102, // EC_DVD_TITLE_CHANGE
        DvdChapterStart = 0x103, // EC_DVD_CHAPTER_START
        DvdAudioStreamChange = 0x104, // EC_DVD_AUDIO_STREAM_CHANGE
        DvdSubPicictureStreamChange = 0x105, // EC_DVD_SUBPICTURE_STREAM_CHANGE
        DvdAngleChange = 0x106, // EC_DVD_ANGLE_CHANGE
        DvdButtonChange = 0x107, // EC_DVD_BUTTON_CHANGE
        DvdValidUopsChange = 0x108, // EC_DVD_VALID_UOPS_CHANGE
        DvdStillOn = 0x109, // EC_DVD_STILL_ON
        DvdStillOff = 0x10a, // EC_DVD_STILL_OFF
        DvdCurrentTime = 0x10b, // EC_DVD_CURRENT_TIME
        DvdError = 0x10c, // EC_DVD_ERROR
        DvdWarning = 0x10d, // EC_DVD_WARNING
        DvdChapterAutoStop = 0x10e, // EC_DVD_CHAPTER_AUTOSTOP
        DvdNoFpPgc = 0x10f, // EC_DVD_NO_FP_PGC
        DvdPlaybackRateChange = 0x110, // EC_DVD_PLAYBACK_RATE_CHANGE
        DvdParentalLevelChange = 0x111, // EC_DVD_PARENTAL_LEVEL_CHANGE
        DvdPlaybackStopped = 0x112, // EC_DVD_PLAYBACK_STOPPED
        DvdAnglesAvailable = 0x113, // EC_DVD_ANGLES_AVAILABLE
        DvdPlayPeriodAutoStop = 0x114, // EC_DVD_PLAYPERIOD_AUTOSTOP
        DvdButtonAutoActivated = 0x115, // EC_DVD_BUTTON_AUTO_ACTIVATED
        DvdCmdStart = 0x116, // EC_DVD_CMD_START
        DvdCmdEnd = 0x117, // EC_DVD_CMD_END
        DvdDiscEjected = 0x118, // EC_DVD_DISC_EJECTED
        DvdDiscInserted = 0x119, // EC_DVD_DISC_INSERTED
        DvdCurrentHmsfTime = 0x11a, // EC_DVD_CURRENT_HMSF_TIME
        DvdKaraokeMode = 0x11b, // EC_DVD_KARAOKE_MODE

        // AudEvCod.h
        SNDDEVInError = 0x200, // EC_SNDDEV_IN_ERROR
        SNDDEVOutError = 0x201, // EC_SNDDEV_OUT_ERROR

        WMTIndexEvent = 0x0251, // EC_WMT_INDEX_EVENT
        WMTEvent = 0x0252, // EC_WMT_EVENT

        Built = 0x300, // EC_BUILT
        Unbuilt = 0x301, // EC_UNBUILT

        // Sbe.h
        StreamBufferTimeHole = 0x0326, // STREAMBUFFER_EC_TIMEHOLE
        StreamBufferStaleDataRead = 0x0327, // STREAMBUFFER_EC_STALE_DATA_READ
        StreamBufferStaleFileDeleted = 0x0328, // STREAMBUFFER_EC_STALE_FILE_DELETED
        StreamBufferContentBecomingStale = 0x0329, // STREAMBUFFER_EC_CONTENT_BECOMING_STALE
        StreamBufferWriteFailure = 0x032a, // STREAMBUFFER_EC_WRITE_FAILURE
        StreamBufferReadFailure = 0x032b, // STREAMBUFFER_EC_READ_FAILURE
        StreamBufferRateChanged = 0x032c, // STREAMBUFFER_EC_RATE_CHANGED
    }

    // Specifies the seeking capabilities of a media stream
    //
    //[ComVisible(false), Flags]
    //public enum SeekingCapabilities		// AM_SEEKING_SEEKING_CAPABILITIES
    //{
    //    CanSeekAbsolute = 0x001,
    //    CanSeekForwards = 0x002,
    //    CanSeekBackwards = 0x004,
    //    CanGetCurrentPos = 0x008,
    //    CanGetStopPos = 0x010,
    //    CanGetDuration = 0x020,
    //    CanPlayBackwards = 0x040,
    //    CanDoSegments = 0x080,
    //    Source = 0x100
    //}

    // Positioning and Modifier Flags
    //
    //[ComVisible(false), Flags]
    //public enum SeekingFlags
    //{
    //    NoPositioning = 0x00,	// No change in position
    //    AbsolutePositioning = 0x01,	// The specified position is absolute
    //    RelativePositioning = 0x02,	// The specified position is relative to the previous value
    //    IncrementalPositioning = 0x03,	// The stop position is relative to the current position
    //    SeekToKeyFrame = 0x04,	// Seek to the nearest key frame. This might be faster, but less accurate.
    //    ReturnTime = 0x08,	// Return the equivalent reference times
    //    Segment = 0x10,	// Use segment seeking
    //    NoFlush = 0x20	// Do not flush
    //}

}