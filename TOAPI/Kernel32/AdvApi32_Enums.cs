using System;
using System.Collections.Generic;
using System.Text;

namespace TOAPI.Kernel32
{
    public enum SECURITY_IMPERSONATION_LEVEL : int
    {
        SecurityAnonymous = 0,
        SecurityIdentification = 1,
        SecurityImpersonation = 2,
        SecurityDelegation = 3,
    }

    public enum TOKEN_TYPE : int
    {
        TokenPrimary = 1,
        TokenImpersonation = 2
    }

    [Flags]
    public enum SCM_ACCESS : uint
    {
        SC_MANAGER_CONNECT = 0x00001,
    }

    [Flags]
    public enum SERVICE_ACCESS : uint
    {
        STANDARD_RIGHTS_REQUIRED = 0xF0000,
        SERVICE_QUERY_CONFIG = 0x00001,
        SERVICE_CHANGE_CONFIG = 0x00002,
        SERVICE_QUERY_STATUS = 0x00004,
        SERVICE_ENUMERATE_DEPENDENTS = 0x00008,
        SERVICE_START = 0x00010,
        SERVICE_STOP = 0x00020,
        SERVICE_PAUSE_CONTINUE = 0x00040,
        SERVICE_INTERROGATE = 0x00080,
        SERVICE_USER_DEFINED_CONTROL = 0x00100,
        SERVICE_ALL_ACCESS = (STANDARD_RIGHTS_REQUIRED |
                          SERVICE_QUERY_CONFIG |
                          SERVICE_CHANGE_CONFIG |
                          SERVICE_QUERY_STATUS |
                          SERVICE_ENUMERATE_DEPENDENTS |
                          SERVICE_START |
                          SERVICE_STOP |
                          SERVICE_PAUSE_CONTINUE |
                          SERVICE_INTERROGATE |
                          SERVICE_USER_DEFINED_CONTROL)
    }

    public enum SC_STATUS_TYPE
    {
        SC_STATUS_PROCESS_INFO = 0
    }


}
