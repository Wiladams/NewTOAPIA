using System;
using System.Collections.Generic;
using System.Text;

namespace TOAPI.Types
{
    public class ACC
    {
        // Access_mask bits can be found in Winnt.h
        // These access masks are used for things like creating and
        // opening window stations, desktops, and the like
        public const int
            DELETE = 0x00010000,
            READ_CONTROL = 0x00020000,
            WRITE_DAC = 0x00040000,
            WRITE_OWNER = 0x00080000,
            SYNCHRONIZE = 0x00100000;

        public const int
            STANDARD_RIGHTS_REQUIRED = 0x000f0000,
            STANDARD_RIGHTS_READ = READ_CONTROL,
            STANDARD_RIGHTS_WRITE = READ_CONTROL,
            STANDARD_RIGHTS_EXECUTE = READ_CONTROL,
            STANDARD_RIGHTS_ALL = 0x001f0000,

            SPECIFIC_RIGHTS_ALL = 0x0000ffff,

            ACCESS_SYSTEM_SECURITY = 0x01000000,

            MAXIMUM_ALLOWED = 0x02000000;


        public const int TOKEN_ASSIGN_PRIMARY      = 0x0001;
        public const int TOKEN_DUPLICATE = 0x0002;
        public const int TOKEN_IMPERSONATE = 0x0004;
        public const int TOKEN_QUERY = 0x0008;
        public const int TOKEN_QUERY_SOURCE = 0x0010;
        public const int TOKEN_ADJUST_PRIVILEGES = 0x0020;
        public const int TOKEN_ADJUST_GROUPS = 0x0040;
        public const int TOKEN_ADJUST_DEFAULT = 0x0080;
        public const int TOKEN_ADJUST_SESSIONID = 0x0100;

        public const int SE_GROUP_INTEGRITY = 0x00000020;

        public const int TOKEN_ALL_ACCESS_P = (STANDARD_RIGHTS_REQUIRED |
                                      TOKEN_ASSIGN_PRIMARY |
                                      TOKEN_DUPLICATE |
                                      TOKEN_IMPERSONATE |
                                      TOKEN_QUERY |
                                      TOKEN_QUERY_SOURCE |
                                      TOKEN_ADJUST_PRIVILEGES |
                                      TOKEN_ADJUST_GROUPS |
                                      TOKEN_ADJUST_DEFAULT);

        public const int TOKEN_ALL_ACCESS = TOKEN_ALL_ACCESS_P | TOKEN_ADJUST_SESSIONID;

        public const int TOKEN_READ = STANDARD_RIGHTS_READ | TOKEN_QUERY;


        public const int TOKEN_WRITE = STANDARD_RIGHTS_WRITE |
                                      TOKEN_ADJUST_PRIVILEGES |
                                      TOKEN_ADJUST_GROUPS |
                                      TOKEN_ADJUST_DEFAULT;

        public const int TOKEN_EXECUTE = STANDARD_RIGHTS_EXECUTE;

    }
}
