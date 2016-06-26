namespace TOAPI.Kernel32
{
    public partial class Kernel32
    {
        public const int ERROR_NOT_ALL_ASSIGNED = 1300;

        // From Winnt.h
        public const uint
            SE_PRIVILEGE_ENABLED_BY_DEFAULT = (0x00000001),
            SE_PRIVILEGE_ENABLED            = (0x00000002),
            SE_PRIVILEGE_REMOVED            = (0X00000004),
            SE_PRIVILEGE_USED_FOR_ACCESS    = (0x80000000);

        public const uint
            SE_PRIVILEGE_VALID_ATTRIBUTES   = (SE_PRIVILEGE_ENABLED_BY_DEFAULT |
                                         SE_PRIVILEGE_ENABLED            |
                                         SE_PRIVILEGE_REMOVED            |
                                         SE_PRIVILEGE_USED_FOR_ACCESS);

        // From Winnt.h
        public const string SE_DEBUG_NAME = "SeDebugPrivilege";
        public const string SE_RESTORE_NAME = "SeRestorePrivilege";
        public const string SE_BACKUP_NAME = "SeBackupPrivilege";

        //
// dwCreationFlag values
//

//#define DEBUG_PROCESS                     0x00000001
//#define DEBUG_ONLY_THIS_PROCESS           0x00000002

//#define CREATE_SUSPENDED                  0x00000004

//#define DETACHED_PROCESS                  0x00000008

//#define CREATE_NEW_CONSOLE                0x00000010

//#define NORMAL_PRIORITY_CLASS             0x00000020
//#define IDLE_PRIORITY_CLASS               0x00000040
//#define HIGH_PRIORITY_CLASS               0x00000080
//#define REALTIME_PRIORITY_CLASS           0x00000100

//#define CREATE_NEW_PROCESS_GROUP          0x00000200
//#define CREATE_UNICODE_ENVIRONMENT        0x00000400

//#define CREATE_SEPARATE_WOW_VDM           0x00000800
//#define CREATE_SHARED_WOW_VDM             0x00001000
//#define CREATE_FORCEDOS                   0x00002000

//#define BELOW_NORMAL_PRIORITY_CLASS       0x00004000
//#define ABOVE_NORMAL_PRIORITY_CLASS       0x00008000

//#define STACK_SIZE_PARAM_IS_A_RESERVATION 0x00010000
//#define INHERIT_CALLER_PRIORITY           0x00020000

//#define CREATE_PROTECTED_PROCESS          0x00040000

//#define EXTENDED_STARTUPINFO_PRESENT      0x00080000

//#define PROCESS_MODE_BACKGROUND_BEGIN     0x00100000
//#define PROCESS_MODE_BACKGROUND_END       0x00200000

//#define CREATE_BREAKAWAY_FROM_JOB         0x01000000
//#define CREATE_PRESERVE_CODE_AUTHZ_LEVEL  0x02000000

//#define CREATE_DEFAULT_ERROR_MODE         0x04000000

//#define CREATE_NO_WINDOW                  0x08000000

        //
//#define PROFILE_USER                      0x10000000
//#define PROFILE_KERNEL                    0x20000000
//#define PROFILE_SERVER                    0x40000000

//#define CREATE_IGNORE_SYSTEM_DEFAULT      0x80000000

        // Thread Priority
//#define THREAD_PRIORITY_LOWEST          THREAD_BASE_PRIORITY_MIN
//#define THREAD_PRIORITY_BELOW_NORMAL    (THREAD_PRIORITY_LOWEST+1)
//#define THREAD_PRIORITY_NORMAL          0
//#define THREAD_PRIORITY_HIGHEST         THREAD_BASE_PRIORITY_MAX
//#define THREAD_PRIORITY_ABOVE_NORMAL    (THREAD_PRIORITY_HIGHEST-1)
//#define THREAD_PRIORITY_ERROR_RETURN    (MAXLONG)

//#define THREAD_PRIORITY_TIME_CRITICAL   THREAD_BASE_PRIORITY_LOWRT
//#define THREAD_PRIORITY_IDLE            THREAD_BASE_PRIORITY_IDLE

//#define THREAD_MODE_BACKGROUND_BEGIN    0x00010000
//#define THREAD_MODE_BACKGROUND_END      0x00020000
    }
}
