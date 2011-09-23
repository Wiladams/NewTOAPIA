namespace TOAPI.WinMM
{
    using System;

    /// <summary>
    /// Specifies capabilities of a waveOut device.
    /// </summary>
    [Flags]
    public enum WAVECAPS
    {
        /// <summary>
        /// The device can change playback pitch.
        /// </summary>
        WAVECAPS_PITCH = 1,

        /// <summary>
        /// The device can change the playback rate.
        /// </summary>
        WAVECAPS_PLAYBACKRATE = 2,

        /// <summary>
        /// The device can change the volume.
        /// </summary>
        WAVECAPS_VOLUME = 4,

        /// <summary>
        /// The device can change the stereo volume.
        /// </summary>
        WAVECAPS_LRVOLUME = 8,

        /// <summary>
        /// The device is synchronus.
        /// </summary>
        WAVECAPS_SYNC = 16,

        /// <summary>
        /// The device supports sample accurate.
        /// </summary>
        WAVECAPS_SAMPLEACCURATE = 32,

        /// <summary>
        /// The device supports direct sound writing.
        /// </summary>
        WAVECAPS_DIRECTSOUND = 64,
    }

    /// <summary>
    /// Specifies the frequency, bitdepth, and channel count of the formats a device can support.
    /// </summary>
    [Flags]
    public enum WAVEFORMATS
    {
        /// <summary>
        /// 11.025 kHz, Mono,   8-bit
        /// </summary>
        WAVE_FORMAT_1M08 = 0x00000001,

        /// <summary>
        /// 11.025 kHz, Stereo, 8-bit
        /// </summary>
        WAVE_FORMAT_1S08 = 0x00000002,

        /// <summary>
        /// 11.025 kHz, Mono,   16-bit
        /// </summary>
        WAVE_FORMAT_1M16 = 0x00000004,

        /// <summary>
        /// 11.025 kHz, Stereo, 16-bit
        /// </summary>
        WAVE_FORMAT_1S16 = 0x00000008,

        /// <summary>
        /// 22.05  kHz, Mono,   8-bit
        /// </summary>
        WAVE_FORMAT_2M08 = 0x00000010,

        /// <summary>
        /// 22.05  kHz, Stereo, 8-bit
        /// </summary>
        WAVE_FORMAT_2S08 = 0x00000020,

        /// <summary>
        /// 22.05  kHz, Mono,   16-bit
        /// </summary>
        WAVE_FORMAT_2M16 = 0x00000040,

        /// <summary>
        /// 22.05  kHz, Stereo, 16-bit
        /// </summary>
        WAVE_FORMAT_2S16 = 0x00000080,

        /// <summary>
        /// 44.1   kHz, Mono,   8-bit
        /// </summary>
        WAVE_FORMAT_4M08 = 0x00000100,

        /// <summary>
        /// 44.1   kHz, Stereo, 8-bit
        /// </summary>
        WAVE_FORMAT_4S08 = 0x00000200,

        /// <summary>
        /// 44.1   kHz, Mono,   16-bit
        /// </summary>
        WAVE_FORMAT_4M16 = 0x00000400,

        /// <summary>
        /// 44.1   kHz, Stereo, 16-bit
        /// </summary>
        WAVE_FORMAT_4S16 = 0x00000800,

        /// <summary>
        /// 48     kHz, Mono,   8-bit
        /// </summary>
        WAVE_FORMAT_48M08 = 0x00001000,

        /// <summary>
        /// 48     kHz, Stereo, 8-bit
        /// </summary>
        WAVE_FORMAT_48S08 = 0x00002000,

        /// <summary>
        /// 48     kHz, Mono,   16-bit
        /// </summary>
        WAVE_FORMAT_48M16 = 0x00004000,

        /// <summary>
        /// 48     kHz, Stereo, 16-bit
        /// </summary>
        WAVE_FORMAT_48S16 = 0x00008000,

        /// <summary>
        /// 96     kHz, Mono,   8-bit
        /// </summary>
        WAVE_FORMAT_96M08 = 0x00010000,

        /// <summary>
        /// 96     kHz, Stereo, 8-bit
        /// </summary>
        WAVE_FORMAT_96S08 = 0x00020000,

        /// <summary>
        /// 96     kHz, Mono,   16-bit
        /// </summary>
        WAVE_FORMAT_96M16 = 0x00040000,

        /// <summary>
        /// 96     kHz, Stereo, 16-bit
        /// </summary>
        WAVE_FORMAT_96S16 = 0x00080000,
    }

    /// <summary>
    /// Describes a wave format scheme.
    /// </summary>
    public enum WAVEFORMATTAG
    {
        /// <summary>
        /// Pulse Code Modulation
        /// </summary>
        WAVE_FORMAT_PCM = 0x01,

        /// <summary>
        /// Adaptive Differential Pulse Code Modulation
        /// </summary>
        WAVE_FORMAT_ADPCM = 0x02
    }

    /// <summary>
    /// Indicates a WaveIn message.
    /// </summary>
    public enum WaveInMessage
    {
        /// <summary>
        /// Not Used.  Indicates that there is no message.
        /// </summary>
        None = 0x000,

        /// <summary>
        /// Indicates that the device has been opened.
        /// </summary>
        DeviceOpened = winmm.MM_WIM_OPEN,

        /// <summary>
        /// Indicates that the device has been closed.
        /// </summary>
        DeviceClosed = winmm.MM_WIM_CLOSE,

        /// <summary>
        /// Indicates that playback of a write operation has been completed.
        /// </summary>
        DataReady = winmm.MM_WIM_DATA
    }

    /// <summary>
    /// Indicates a WaveOut message.
    /// </summary>
    public enum WaveOutMessage
    {
        /// <summary>
        /// Not Used.  Indicates that there is no message.
        /// </summary>
        None = 0x000,

        /// <summary>
        /// Indicates that the device has been opened.
        /// </summary>
        DeviceOpened = winmm.MM_WOM_OPEN,

        /// <summary>
        /// Indicates that the device has been closed.
        /// </summary>
        DeviceClosed = winmm.MM_WOM_CLOSE,

        /// <summary>
        /// Indicates that playback of a write operation has been completed.
        /// </summary>
        WriteDone = winmm.MM_WOM_DONE
    }

    public enum WAVHDRFlags : int
    {
        None = 0x0,
        Done = 0x00000001,      /* done bit */
        Prepared = 0x00000002,  /* set if this header has been prepared */
        BeginLoop = 0x00000004, /* loop start block */
        EndLoop = 0x00000008,   /* loop end block */
        InQueue = 0x00000010    /* reserved for driver */
    }

    /// <summary>
    /// Used as a return result from many of the WinMM calls.
    /// </summary>
    public enum MMSYSERROR
    {
        /// <summary>
        /// No Error. (Success)
        /// </summary>
        MMSYSERR_NOERROR = winmm.MMSYSERR_BASE + 0,

        /// <summary>
        /// Unspecified Error.
        /// </summary>
        MMSYSERR_ERROR = 1,

        /// <summary>
        /// Device ID out of range.
        /// </summary>
        MMSYSERR_BADDEVICEID = 2,

        /// <summary>
        /// Driver failed enable.
        /// </summary>
        MMSYSERR_NOTENABLED = 3,

        /// <summary>
        /// Device is already allocated.
        /// </summary>
        MMSYSERR_ALLOCATED = 4,

        /// <summary>
        /// Device handle is invalid.
        /// </summary>
        MMSYSERR_INVALHANDLE = 5,

        /// <summary>
        /// No device driver is present.
        /// </summary>
        MMSYSERR_NODRIVER = 6,

        /// <summary>
        /// In sufficient memory, or memory allocation error.
        /// </summary>
        MMSYSERR_NOMEM = 7,

        /// <summary>
        /// Unsupported function.
        /// </summary>
        MMSYSERR_NOTSUPPORTED = 8,

        /// <summary>
        /// Error value out of range.
        /// </summary>
        MMSYSERR_BADERRNUM = 9,

        /// <summary>
        /// Invalid flag passed.
        /// </summary>
        MMSYSERR_INVALFLAG = 10,

        /// <summary>
        /// Invalid parameter passed.
        /// </summary>
        MMSYSERR_INVALPARAM = 11,

        /// <summary>
        /// Handle being used simultaneously on another thread.
        /// </summary>
        MMSYSERR_HANDLEBUSY = 12,

        /// <summary>
        /// Specified alias not found.
        /// </summary>
        MMSYSERR_INVALIDALIAS = 13,

        /// <summary>
        /// Bad registry database.
        /// </summary>
        MMSYSERR_BADDB = 14,

        /// <summary>
        /// Registry key not found.
        /// </summary>
        MMSYSERR_KEYNOTFOUND = 15,

        /// <summary>
        /// Registry read error.
        /// </summary>
        MMSYSERR_READERROR = 16,

        /// <summary>
        /// Registry write error.
        /// </summary>
        MMSYSERR_WRITEERROR = 17,

        /// <summary>
        /// Registry delete error.
        /// </summary>
        MMSYSERR_DELETEERROR = 18,

        /// <summary>
        /// Registry value not found.
        /// </summary>
        MMSYSERR_VALNOTFOUND = 19,

        /// <summary>
        /// Driver does not call DriverCallback.
        /// </summary>
        MMSYSERR_NODRIVERCB = 20,

        /// <summary>
        /// More data to be returned.
        /// </summary>
        MMSYSERR_MOREDATA = 21,

        /// <summary>
        /// Unsupported wave format.
        /// </summary>
        WAVERR_BADFORMAT = 32,

        /// <summary>
        /// Still something playing.
        /// </summary>
        WAVERR_STILLPLAYING = 33,

        /// <summary>
        /// Header not prepared.
        /// </summary>
        WAVERR_UNPREPARED = 34,

        /// <summary>
        /// Device is syncronus.
        /// </summary>
        WAVERR_SYNC = 35,

        /// <summary>
        /// Header not prepared.
        /// </summary>
        MIDIERR_UNPREPARED = 64,

        /// <summary>
        /// Still something playing.
        /// </summary>
        MIDIERR_STILLPLAYING = 65,

        /// <summary>
        /// No configured instruments.
        /// </summary>
        MIDIERR_NOMAP = 66,

        /// <summary>
        /// Hardware is still busy.
        /// </summary>
        MIDIERR_NOTREADY = 67,

        /// <summary>
        /// Port no longer connected
        /// </summary>
        MIDIERR_NODEVICE = 68,

        /// <summary>
        /// Invalid MIF
        /// </summary>
        MIDIERR_INVALIDSETUP = 69,

        /// <summary>
        /// Operation unsupported with open mode.
        /// </summary>
        MIDIERR_BADOPENMODE = 70,

        /// <summary>
        /// Thru device 'eating' a message
        /// </summary>
        MIDIERR_DONT_CONTINUE = 71,

        /// <summary>
        /// The resolution specified in uPeriod is out of range.
        /// </summary>
        TIMERR_NOCANDO = 96 + 1,

        /// <summary>
        /// Time struct size
        /// </summary>
        TIMERR_STRUCT = 96 + 33,

        /// <summary>
        /// Bad parameters
        /// </summary>
        JOYERR_PARMS = 160 + 5,

        /// <summary>
        /// Request not completed
        /// </summary>
        JOYERR_NOCANDO = 160 + 6,

        /// <summary>
        /// Joystick is unplugged
        /// </summary>
        JOYERR_UNPLUGGED = 160 + 7,

        /// <summary>
        /// Invalid device ID
        /// </summary>
        MCIERR_INVALID_DEVICE_ID = 256 + 1,

        /// <summary>
        /// Unrecognized keyword.
        /// </summary>
        MCIERR_UNRECOGNIZED_KEYWORD = 256 + 3,

        /// <summary>
        /// Unrecognized command
        /// </summary>
        MCIERR_UNRECOGNIZED_COMMAND = 256 + 5,

        /// <summary>
        /// Hardware error
        /// </summary>
        MCIERR_HARDWARE = 256 + 6,

        /// <summary>
        /// Invalid device name
        /// </summary>
        MCIERR_INVALID_DEVICE_NAME = 256 + 7,

        /// <summary>
        /// Out of memory
        /// </summary>
        MCIERR_OUT_OF_MEMORY = 256 + 8,

        MCIERR_DEVICE_OPEN = 256 + 9,

        MCIERR_CANNOT_LOAD_DRIVER = 256 + 10,

        MCIERR_MISSING_COMMAND_STRING = 256 + 11,

        MCIERR_PARAM_OVERFLOW = 256 + 12,

        MCIERR_MISSING_STRING_ARGUMENT = 256 + 13,

        MCIERR_BAD_INTEGER = 256 + 14,

        MCIERR_PARSER_INTERNAL = 256 + 15,

        MCIERR_DRIVER_INTERNAL = 256 + 16,

        MCIERR_MISSING_PARAMETER = 256 + 17,

        MCIERR_UNSUPPORTED_FUNCTION = 256 + 18,

        MCIERR_FILE_NOT_FOUND = 256 + 19,

        MCIERR_DEVICE_NOT_READY = 256 + 20,

        MCIERR_INTERNAL = 256 + 21,

        MCIERR_DRIVER = 256 + 22,

        MCIERR_CANNOT_USE_ALL = 256 + 23,

        MCIERR_MULTIPLE = 256 + 24,

        MCIERR_EXTENSION_NOT_FOUND = 256 + 25,

        MCIERR_OUTOFRANGE = 256 + 26,

        MCIERR_FLAGS_NOT_COMPATIBLE = 256 + 28,

        MCIERR_FILE_NOT_SAVED = 256 + 30,

        MCIERR_DEVICE_TYPE_REQUIRED = 256 + 31,

        MCIERR_DEVICE_LOCKED = 256 + 32,

        MCIERR_DUPLICATE_ALIAS = 256 + 33,

        MCIERR_BAD_CONSTANT = 256 + 34,

        MCIERR_MUST_USE_SHAREABLE = 256 + 35,

        MCIERR_MISSING_DEVICE_NAME = 256 + 36,

        MCIERR_BAD_TIME_FORMAT = 256 + 37,

        MCIERR_NO_CLOSING_QUOTE = 256 + 38,

        MCIERR_DUPLICATE_FLAGS = 256 + 39,

        MCIERR_INVALID_FILE = 256 + 40,

        MCIERR_NULL_PARAMETER_BLOCK = 256 + 41,

        MCIERR_UNNAMED_RESOURCE = 256 + 42,

        MCIERR_NEW_REQUIRES_ALIAS = 256 + 43,

        MCIERR_NOTIFY_ON_AUTO_OPEN = 256 + 44,

        MCIERR_NO_ELEMENT_ALLOWED = 256 + 45,

        MCIERR_NONAPPLICABLE_FUNCTION = 256 + 46,

        MCIERR_ILLEGAL_FOR_AUTO_OPEN = 256 + 47,

        MCIERR_FILENAME_REQUIRED = 256 + 48,

        MCIERR_EXTRA_CHARACTERS = 256 + 49,

        MCIERR_DEVICE_NOT_INSTALLED = 256 + 50,

        MCIERR_GET_CD = 256 + 51,

        MCIERR_SET_CD = 256 + 52,

        MCIERR_SET_DRIVE = 256 + 53,

        MCIERR_DEVICE_LENGTH = 256 + 54,

        MCIERR_DEVICE_ORD_LENGTH = 256 + 55,

        MCIERR_NO_INTEGER = 256 + 56,

        MCIERR_WAVE_OUTPUTSINUSE = 256 + 64,

        MCIERR_WAVE_SETOUTPUTINUSE = 256 + 65,

        MCIERR_WAVE_INPUTSINUSE = 256 + 66,

        MCIERR_WAVE_SETINPUTINUSE = 256 + 67,

        MCIERR_WAVE_OUTPUTUNSPECIFIED = 256 + 68,

        MCIERR_WAVE_INPUTUNSPECIFIED = 256 + 69,

        MCIERR_WAVE_OUTPUTSUNSUITABLE = 256 + 70,

        MCIERR_WAVE_SETOUTPUTUNSUITABLE = 256 + 71,

        MCIERR_WAVE_INPUTSUNSUITABLE = 256 + 72,

        MCIERR_WAVE_SETINPUTUNSUITABLE = 256 + 73,

        MCIERR_SEQ_DIV_INCOMPATIBLE = 256 + 80,

        MCIERR_SEQ_PORT_INUSE = 256 + 81,

        MCIERR_SEQ_PORT_NONEXISTENT = 256 + 82,

        MCIERR_SEQ_PORT_MAPNODEVICE = 256 + 83,

        MCIERR_SEQ_PORT_MISCERROR = 256 + 84,

        MCIERR_SEQ_TIMER = 256 + 85,

        MCIERR_SEQ_PORTUNSPECIFIED = 256 + 86,

        MCIERR_SEQ_NOMIDIPRESENT = 256 + 87,

        MCIERR_NO_WINDOW = 256 + 90,

        MCIERR_CREATEWINDOW = 256 + 91,

        MCIERR_FILE_READ = 256 + 92,

        MCIERR_FILE_WRITE = 256 + 93,

        MCIERR_NO_IDENTITY = 256 + 94,

        MIXERR_INVALLINE = 1024 + 0,

        MIXERR_INVALCONTROL = 1024 + 1,

        MIXERR_INVALVALUE = 1024 + 2,

        MIXERR_LASTERROR = 1024 + 2,
    }
}