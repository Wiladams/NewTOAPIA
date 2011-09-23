using System;
using System.Collections.Generic;
using System.Text;

using TOAPI.WinMM;

namespace NewTOAPIA.Media.WinMM
{
    // wave callback messages
    public enum WaveCallbackMsg : int
    {
        OutputOpened = 0x03BB,
        OutputClosed = 0x03BC,
        OutputDone = 0x03BD,
        InputOpened = 0x03BE,
        InputClosed = 0x03BF,
        InputData = 0x03C0
    }


    public enum MMIO : uint
    {
        // bit field masks
        RWMODE = 0x00000003,      // open file for reading/writing/both
        SHAREMODE = 0x00000070,      // file sharing mode number

        // constants for dwFlags field of MMIOINFO
        CREATE = 0x00001000,      // create new file (or truncate file)
        PARSE = 0x00000100,      // parse new file returning path
        DELETE = 0x00000200,      // create new file (or truncate file)
        EXIST = 0x00004000,      // checks for existence of file
        ALLOCBUF = 0x00010000,      // mmioOpen() should allocate a buffer
        GETTEMP = 0x00020000,      // mmioOpen() should retrieve temp name

        DIRTY = 0x10000000,      /* I/O buffer is dirty */

        // read/write mode numbers (bit field MMIO_RWMODE)
        READ = 0x00000000,     /* open file for reading only */
        WRITE = 0x00000001,      /* open file for writing only */
        READWRITE = 0x00000002,      /* open file for reading and writing */

        // share mode numbers (bit field MMIO_SHAREMODE)
        COMPAT = 0x00000000,      /* compatibility mode */
        EXCLUSIVE = 0x00000010,      /* exclusive-access mode */
        DENYWRITE = 0x00000020,      /* deny writing to other processes */
        DENYREAD = 0x00000030,      /* deny reading to other processes */
        DENYNONE = 0x00000040,      /* deny nothing to other processes */

        /* various MMIO flags */
        FHOPEN = 0x0010,  /* mmioClose: keep file handle open */
        EMPTYBUF = 0x0010,  /* mmioFlush: empty the I/O buffer */
        TOUPPER = 0x0010,  /* mmioStringToFOURCC: to u-case */
        INSTALLPROC = 0x00010000,  /* mmioInstallIOProc: install MMIOProc */
        GLOBALPROC = 0x10000000,  /* mmioInstallIOProc: install globally */
        REMOVEPROC = 0x00020000,  /* mmioInstallIOProc: remove MMIOProc */
        UNICODEPROC = 0x01000000,  /* mmioInstallIOProc: Unicode MMIOProc */
        FINDPROC = 0x00040000,  /* mmioInstallIOProc: find an MMIOProc */
        FINDCHUNK = 0x0010,  /* mmioDescend: find a chunk by ID */
        FINDRIFF = 0x0020,  /* mmioDescend: find a LIST chunk */
        FINDLIST = 0x0040,  /* mmioDescend: find a RIFF chunk */
        CREATERIFF = 0x0020,  /* mmioCreateChunk: make a LIST chunk */
        CREATELIST = 0x0040,  /* mmioCreateChunk: make a RIFF chunk */

        DEFAULTBUFFER = 8192,       // default buffer size

    }

    // flags for mmioSeek()
    public enum SEEK
    {
        SET = 0,               // seek to an absolute position
        CUR = 1,               // seek relative to current position
        END = 2,               // seek relative to end of file
    }

    // message numbers for MMIOPROC I/O procedure functions
    public enum MMIOM : uint
    {
        READ = MMIO.READ,       // read
        WRITE = MMIO.WRITE,      // write
        SEEK = 2,           /* seek to a new position in file */
        OPEN = 3,           /* open file */
        CLOSE = 4,          /* close file */
        WRITEFLUSH = 5,     /* write and flush */
        RENAME = 6,         /* rename specified file */
        USER = 0x8000       /* beginning of user-defined messages */
    }

    // flags used with waveOutOpen(), waveInOpen(), midiInOpen(), and
    // midiOutOpen() to specify the type of the dwCallback parameter.

    public enum MM_CALLBACK
    {
        Null = 0x00000000,          /* no callback */
        TypeMask = 0x00070000,      /* callback type mask */
        Window = 0x00010000,        /* dwCallback is a HWND */
        Thread = 0x00020000,        /* thread ID replaces 16 bit task */
        Function = 0x00030000,      /* dwCallback is a FARPROC */
        Event = 0x00050000,         /* dwCallback is an EVENT Handle */
    }



    /* flags for dwFlags parameter in waveOutOpen() and waveInOpen() */
    public enum WAVE
    {
        FORMAT_QUERY         =0x0001,
        ALLOWSYNC            =0x0002,
        MAPPED               =0x0004,
        FORMAT_DIRECT        =0x0008,
        FORMAT_DIRECT_QUERY  =(FORMAT_QUERY | FORMAT_DIRECT)
    }

    public enum AudioSampleSize : int
    {
        Bits8   = 8,
        Bits16  = 16
    }

    public enum AudioChannels : int
    {
        Mono = 1,
        Stereo = 2
    }

    public enum AudioSampleRate : int
    {
        kHz11_025   = 11025,
        kHz22_05    = 22050,
        kHz44_10    = 44100,
        kHz48       = 48000,
        kHz96       = 96000
    }

    // defines for dwFormat field of WAVEINCAPS and WAVEOUTCAPS
    [Flags]
    public enum WaveFormat : int
    {
        Invalid             = winmm.WAVE_INVALIDFORMAT,     // invalid format

        Mono8Bit11_025kHz       = winmm.WAVE_FORMAT_1M08,       // 11.025 kHz, Mono,   8-bit
        Mono8Bit22_05kHz        = winmm.WAVE_FORMAT_2M08,       // 22.05  kHz, Mono,   8-bit
        Mono8Bit44_10kHz        = winmm.WAVE_FORMAT_44M08,      // 44.1   kHz, Mono,   8-bit
        Mono8Bit48kHz           = winmm.WAVE_FORMAT_48M08,      // 48     kHz, Mono,   8-bit
        Mono8Bit96kHz           = winmm.WAVE_FORMAT_96M08,      // 96     kHz, Mono,   8-bit
        
        
        Mono16Bit11_025kHz      = winmm.WAVE_FORMAT_1M16,       // 11.025 kHz, Mono,   16-bit
        Mono16Bit22_05kHz       = winmm.WAVE_FORMAT_2M16,       // 22.05  kHz, Mono,   16-bit
        Mono16Bit44_10kHz       = winmm.WAVE_FORMAT_44M16,      // 44.1   kHz, Mono,   16-bit
        Mono16Bit48kHz          = winmm.WAVE_FORMAT_48M16,      // 48     kHz, Mono,   16-bit
        Mono16Bit96kHz          = winmm.WAVE_FORMAT_96M16,      // 96     kHz, Mono,   16-bit

        Stereo8Bit11_025kHz     = winmm.WAVE_FORMAT_1S08,       // 11.025 kHz, Stereo, 8-bit
        Stereo8Bit22_05kHz      = winmm.WAVE_FORMAT_2S08,       // 22.05  kHz, Stereo, 8-bit
        Stereo8Bit44_10kHz      = winmm.WAVE_FORMAT_44S08,      // 44.1   kHz, Stereo, 8-bit
        Stereo8Bit48kHz         = winmm.WAVE_FORMAT_48S08,      // 48     kHz, Stereo, 8-bit
        Stereo8Bit96kHz         = winmm.WAVE_FORMAT_96S08,      // 96     kHz, Stereo, 8-bit
        
        Stereo16Bit11_025kHz    = winmm.WAVE_FORMAT_1S16,       // 11.025 kHz, Stereo, 16-bit
        Stereo16Bit22_05kHz     = winmm.WAVE_FORMAT_2S16,       // 22.05  kHz, Stereo, 16-bit
        Stereo16Bit44_10kHz     = winmm.WAVE_FORMAT_44S16,      // 44.1   kHz, Stereo, 16-bit
        Stereo16Bit48kHz        = winmm.WAVE_FORMAT_48S16,      // 48     kHz, Stereo, 16-bit
        Stereo16Bit96hHz        = winmm.WAVE_FORMAT_96S16,      // 96     kHz, Stereo, 16-bit
    }

    /* flags for dwFlags field of WAVEHDR */
    public enum WHDR
    {
        DONE       =0x00000001,  /* done bit */
        PREPARED   =0x00000002,  /* set if this header has been prepared */
        BEGINLOOP  =0x00000004,  /* loop start block */
        ENDLOOP    =0x00000008,  /* loop end block */
        INQUEUE    =0x00000010  /* reserved for driver */
    }

    // Speaker Positions for dwChannelMask in WAVEFORMATEXTENSIBLE:
    public enum SpeakerPosition
    {
        FRONT_LEFT              =0x1,
        FRONT_RIGHT             =0x2,
        FRONT_CENTER            =0x4,
        LOW_FREQUENCY           =0x8,
        BACK_LEFT               =0x10,
        BACK_RIGHT              =0x20,
        FRONT_LEFT_OF_CENTER    =0x40,
        FRONT_RIGHT_OF_CENTER   =0x80,
        BACK_CENTER             =0x100,
        SIDE_LEFT               =0x200,
        SIDE_RIGHT              =0x400,
        TOP_CENTER              =0x800,
        TOP_FRONT_LEFT          =0x1000,
        TOP_FRONT_CENTER        =0x2000,
        TOP_FRONT_RIGHT         =0x4000,
        TOP_BACK_LEFT           =0x8000,
        TOP_BACK_CENTER         =0x10000,
        TOP_BACK_RIGHT          =0x20000,
    }


}
