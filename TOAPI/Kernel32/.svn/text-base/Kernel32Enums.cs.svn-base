using System;


namespace TOAPI.Kernel32
{
    // For use with GetConsoleMode()
    // Use this one if the handle is an InputHandle
    public enum InputConsoleMode
    {
        ENABLE_PROCESSED_INPUT = 0x0001,
        ENABLE_LINE_INPUT = 0x0002,
        ENABLE_ECHO_INPUT = 0x0004,
        ENABLE_MOUSE_INPUT = 0x0010,
        ENABLE_INSERT_MODE = 0x0020,
        ENABLE_QUICK_EDIT_MODE = 0x0040,
        ENABLE_WINDOW_INPUT = 0x0008,
    }

    // For use with GetConsoleMode()
    // Use this one if the handle is an ScreenBuffer
    public enum ScreenBufferMode
    {
        ENABLE_PROCESSED_OUTPUT = 0x0001,
        ENABLE_WRAP_AT_EOL_OUTPUT = 0x0002,
    }

    [Flags]
    public enum CharacterAttributes : ushort
    {
        FOREGROUND_BLUE             = 0x0001,
        FOREGROUND_GREEN            = 0x0002,
        FOREGROUND_RED              = 0x0004,
        FOREGROUND_INTENSITY        = 0x0008,
        BACKGROUND_BLUE             = 0x0010,
        BACKGROUND_GREEN            = 0x0020,
        BACKGROUND_RED              = 0x0040,
        BACKGROUND_INTENSITY        = 0x0080,
        COMMON_LVB_LEADING_BYTE     = 0X0100,
        COMMON_LVB_TRAILING_BYTE    = 0X0200,
        COMMON_LVB_GRID_HORIZONTAL  = 0X0400,
        COMMON_LVB_GRID_LVERTICAL   = 0X0800,
        COMMON_LVB_GRID_RVERTICAL   = 0X1000,
        COMMON_LVB_REVERSE_VIDEO    = 0X4000,
        COMMON_LVB_UNDERSCORE       = 0X8000,
    }

    public enum ConsoleTextColor : ushort
    {
        Black = 0x0000,
        DarkGray = CharacterAttributes.FOREGROUND_BLUE | CharacterAttributes.FOREGROUND_GREEN | CharacterAttributes.FOREGROUND_RED,
        DarkBlue = CharacterAttributes.FOREGROUND_BLUE,
        DarkGreen  = CharacterAttributes.FOREGROUND_GREEN,
        DarkCyan = CharacterAttributes.FOREGROUND_BLUE|CharacterAttributes.FOREGROUND_GREEN,
        DarkRed = CharacterAttributes.FOREGROUND_RED,
        DarkMagenta = CharacterAttributes.FOREGROUND_BLUE|CharacterAttributes.FOREGROUND_RED, 
        DarkYellow = CharacterAttributes.FOREGROUND_RED|CharacterAttributes.FOREGROUND_GREEN,
        Blue = CharacterAttributes.FOREGROUND_BLUE|CharacterAttributes.FOREGROUND_INTENSITY,
        Green = CharacterAttributes.FOREGROUND_GREEN|CharacterAttributes.FOREGROUND_INTENSITY,
        Cyan = CharacterAttributes.FOREGROUND_BLUE|CharacterAttributes.FOREGROUND_GREEN|CharacterAttributes.FOREGROUND_INTENSITY,
        Red = CharacterAttributes.FOREGROUND_RED|CharacterAttributes.FOREGROUND_INTENSITY,
        Magenta = CharacterAttributes.FOREGROUND_BLUE|CharacterAttributes.FOREGROUND_RED|CharacterAttributes.FOREGROUND_INTENSITY,
        Yellow = CharacterAttributes.FOREGROUND_RED|CharacterAttributes.FOREGROUND_GREEN|CharacterAttributes.FOREGROUND_INTENSITY,
        Gray = CharacterAttributes.FOREGROUND_BLUE | CharacterAttributes.FOREGROUND_GREEN | CharacterAttributes.FOREGROUND_RED | CharacterAttributes.FOREGROUND_INTENSITY,
        White = 0x000f,
    }

    // For SetConsoleDisplayMode()
    public enum ConsoleDisplayModeForSet : int
    {
        CONSOLE_FULLSCREEN_MODE = 1,     // Text is displayed in full-screen mode. 
        CONSOLE_WINDOWED_MODE = 2         // Text is displayed in a window
    }

    public enum ConsoleDisplayModeForGet : int
    {
        CONSOLE_WINDOWED = 0,
        CONSOLE_FULLSCREEN = 1,             // Text is displayed in full-screen mode. 
        CONSOLE_FULLSCREEN_HARDWARE = 2,    // Console has direct access to hardware 
    }

    // SetConsoleMode()
    public enum ConsoleMode : int
    {
        ENABLE_PROCESSED_INPUT = 0x0001,
        ENABLE_LINE_INPUT = 0x0002,
        ENABLE_ECHO_INPUT = 0x0004,
        ENABLE_WINDOW_INPUT = 0x0008,
        ENABLE_MOUSE_INPUT = 0x0010,
        ENABLE_INSERT_MODE = 0x0020,
        ENABLE_QUICK_EDIT_MODE = 0x0040,
    }

    public enum ConsoleScreenMode : int
    {
        ENABLE_PROCESSED_OUTPUT = 0x0001,
        ENABLE_WRAP_AT_EOL_OUTPUT = 0x0002
    }


    // Used with INPUT_RECORD Structure
    public enum EVENT_TYPE : ushort
    {
        KEY_EVENT                   = 0x0001, // KEY_EVENT_RECORD structure with information about a keyboard event. 
        MOUSE_EVENT                 = 0x0002, // MOUSE_EVENT_RECORD structure with information about a mouse movement or button press event. 
        WINDOW_BUFFER_SIZE_EVENT    = 0x0004, // WINDOW_BUFFER_SIZE_RECORD structure with information about the new size of the console screen buffer. 
        MENU_EVENT                  = 0x0008, // MENU_EVENT_RECORD structure. These events are used internally and should be ignored. 
        FOCUS_EVENT                 = 0x0010, // FOCUS_EVENT_RECORD structure. These events are used internally and should be ignored. 
    }

    // Used with CreateToolhelp32Snapshot()
    public enum SnapshotType : uint
    {
        TH32CS_SNAPALL =  TH32CS_SNAPHEAPLIST | TH32CS_SNAPMODULE | TH32CS_SNAPPROCESS | TH32CS_SNAPTHREAD, 
        TH32CS_SNAPHEAPLIST = 0x00000001, // Includes all heaps of the process specified in th32ProcessID in the snapshot. To enumerate the heaps, see Heap32ListFirst. 
        TH32CS_SNAPPROCESS = 0x00000002, // Includes all processes in the system in the snapshot. To enumerate the processes, see Process32First. 
        TH32CS_SNAPTHREAD = 0x00000004, 
        TH32CS_SNAPMODULE = 0x00000008, // Includes all modules of the process specified in th32ProcessID in the snapshot. To enumerate the modules, see Module32First.
        TH32CS_SNAPMODULE32 = 0x00000010, // Includes all 32-bit modules of the process specified in th32ProcessID in the snapshot when run on 64-bit Windows. This flag can be combined with TH32CS_SNAPMODULE or TH32CS_SNAPALL. 
        TH32CS_INHERIT = 0x80000000, // Indicates that the snapshot handle is to be inheritable. 
    }

    [Flags]
    public enum ProcessCreationFlags : int
    {
        CREATE_BREAKAWAY_FROM_JOB   = 0x01000000,
        CREATE_DEFAULT_ERROR_MODE   = 0x04000000,
        CREATE_NEW_CONSOLE          = 0x00000010,
        CREATE_NEW_PROCESS_GROUP    = 0x00000200,
        CREATE_NO_WINDOW            = 0x08000000,
        CREATE_PROTECTED_PROCESS    = 0x00040000,
        CREATE_PRESERVE_CODE_AUTHZ_LEVEL = 0x02000000,
        CREATE_SEPARATE_WOW_VDM     = 0x00000800,
        CREATE_SHARED_WOW_VDM       = 0x00001000,
        CREATE_SUSPENDED            = 0x00000004,
        CREATE_UNICODE_ENVIRONMENT  = 0x00000400,
        DEBUG_ONLY_THIS_PROCESS     = 0x00000002,
        DEBUG_PROCESS               = 0x00000001,
        DETACHED_PROCESS            = 0x00000008,
        EXTENDED_STARTUPINFO_PRESENT = 0x00080000,

        // Priority Class as well
        NORMAL_PRIORITY_CLASS       = 0x00000020,
        IDLE_PRIORITY_CLASS         = 0x00000040,
        HIGH_PRIORITY_CLASS         = 0x00000080,
        REALTIME_PRIORITY_CLASS     = 0x00000100,
        BELOW_NORMAL_PRIORITY_CLASS = 0x00004000,
        ABOVE_NORMAL_PRIORITY_CLASS = 0x00008000,

    }



    public enum PROCESSOR_CACHE_TYPE
    {
        CacheUnified,
        CacheInstruction,
        CacheData,
        CacheTrace,
    }

    public enum LOGICAL_PROCESSOR_RELATIONSHIP
    {
        RelationProcessorCore,
        RelationNumaNode,
        RelationCache,
    }

    public enum FileSeekPosition : uint
    {
        FILE_BEGIN,      // The starting point is 0 (zero) or the beginning of the file. 
        FILE_CURRENT,    // The starting point is the current value of the file pointer. 
        FILE_END,        // The starting point is the current end-of-file position. 
    }

    public enum STREAM_INFO_LEVELS
    {
        FindStreamInfoStandard,
        FindStreamInfoMaxInfoLevel,
    }


    // Used as the dwFlags parameter of DefineDosDevice(...)
    public enum DefineDOSDeviceAction : uint
    {
        DDD_RAW_TARGET_PATH = 0x00000001,
        DDD_REMOVE_DEFINITION = 0x00000002,
        DDD_EXACT_MATCH_ON_REMOVE = 0x00000004,
        DDD_NO_BROADCAST_SYSTEM = 0x00000008,
    }


    public enum DriveType : uint
    {
        /// <summary>The drive type cannot be determined.</summary>
        Unknown = 0,    //DRIVE_UNKNOWN
        /// <summary>The root path is invalid, for example, no volume is mounted at the path.</summary>
        Error = 1,        //DRIVE_NO_ROOT_DIR
        /// <summary>The drive is a type that has removable media, for example, a floppy drive or removable hard disk.</summary>
        Removable = 2,    //DRIVE_REMOVABLE
        /// <summary>The drive is a type that cannot be removed, for example, a fixed hard drive.</summary>
        Fixed = 3,        //DRIVE_FIXED
        /// <summary>The drive is a remote (network) drive.</summary>
        Remote = 4,        //DRIVE_REMOTE
        /// <summary>The drive is a CD-ROM drive.</summary>
        CDROM = 5,        //DRIVE_CDROM
        /// <summary>The drive is a RAM disk.</summary>
        RAMDisk = 6        //DRIVE_RAMDISK
    }

    public enum FileSystemFlags
    {
//    CASE_PRESERVED_NAMES
// The file system preserves the case of file names when it places a name on disk.
 
//FILE_CASE_SENSITIVE_SEARCH
// The file system supports case-sensitive file names.
 
//FILE_FILE_COMPRESSION
// The file system supports file-based compression.
 
//FILE_NAMED_STREAMS
// The file system supports named streams.
 
//FILE_PERSISTENT_ACLS
// The file system preserves and enforces access control lists (ACL). For example, the NTFS file system preserves and enforces ACLs, and the FAT file system does not.
 
//FILE_READ_ONLY_VOLUME
// The specified volume is read-only.

//Windows 2000:  This value is not supported. 
//FILE_SEQUENTIAL_WRITE_ONCE
// The volume supports a single sequential write.
 
//FILE_SUPPORTS_ENCRYPTION
// The file system supports the Encrypted File System (EFS).
 
//FILE_SUPPORTS_OBJECT_IDS
// The file system supports object identifiers.
 
//FILE_SUPPORTS_REPARSE_POINTS
// The file system supports re-parse points.
 
//FILE_SUPPORTS_SPARSE_FILES
// The file system supports sparse files.
 
//FILE_SUPPORTS_TRANSACTIONS
// The volume supports transactions.
 
//FILE_UNICODE_ON_DISK
// The file system supports Unicode in file names as they appear on disk.
 
//FILE_VOLUME_IS_COMPRESSED
// The specified volume is a compressed volume, for example, a DoubleSpace volume.
 
//FILE_VOLUME_QUOTAS
// The file system supports disk quotas.
 
    }

    public enum ProcessorFeature
    {
        PF_FLOATING_POINT_PRECISION_ERRATA = 0, // On a Pentium, a floating-point precision error can occur in rare circumstances. 
        PF_FLOATING_POINT_EMULATED = 1, // Floating-point operations are emulated using a software emulator. 
        // This function returns a nonzero value if floating-point operations are emulated; otherwise, it returns zero.
        PF_COMPARE_EXCHANGE_DOUBLE = 2, // The atomic compare and exchange operation (cmpxchg) is available. 
        PF_MMX_INSTRUCTIONS_AVAILABLE = 3, // The MMX instruction set is available. 
        PF_XMMI_INSTRUCTIONS_AVAILABLE = 6, // The SSE instruction set is available. 
        PF_3DNOW_INSTRUCTIONS_AVAILABLE = 7,     // The 3D-Now instruction set is available. 
        PF_RDTSC_INSTRUCTION_AVAILABLE = 8, // The RDTSC instruction is available. 
        PF_PAE_ENABLED = 9, // The processor is PAE-enabled. For more information, see Physical Address Extension. 
        PF_XMMI64_INSTRUCTIONS_AVAILABLE = 10, // The SSE2 instruction set is available.
        PF_NX_ENABLED = 12, // Data execution prevention is enabled.
        PF_SSE3_INSTRUCTIONS_AVAILABLE = 13, // The SSE3 instruction set is available.
        PF_COMPARE_EXCHANGE128 = 14, // The atomic compare and exchange 128-bit operation (cmpxchg16b) is available.
        PF_COMPARE64_EXCHANGE128 = 15, // The atomic compare 64 and exchange 128-bit operation (cmp8xchg16) is available.
        PF_CHANNELS_ENABLED = 16,// TBD 
    }

    public enum TOKEN_INFORMATION_CLASS : int
    {
        TokenUser = 1,
        TokenGroups,
        TokenPrivileges,
        TokenOwner,
        TokenPrimaryGroup,
        TokenDefaultDacl,
        TokenSource,
        TokenType,
        TokenImpersonationLevel,
        TokenStatistics,
        TokenRestrictedSids,
        TokenSessionId,
        TokenGroupsAndPrivileges,
        TokenSessionReference,
        TokenSandBoxInert,
        TokenAuditPolicy,
        TokenOrigin,
        TokenElevationType,
        TokenLinkedToken,
        TokenElevation,
        TokenHasRestrictions,
        TokenAccessInformation,
        TokenVirtualizationAllowed,
        TokenVirtualizationEnabled,
        TokenIntegrityLevel,
        TokenUIAccess,
        TokenMandatoryPolicy,
        TokenLogonSid,
        MaxTokenInfoClass
    }
}
