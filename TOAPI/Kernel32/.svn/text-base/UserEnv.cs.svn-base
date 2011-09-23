using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.InteropServices;

namespace TOAPI.UserEnvironment
{
    public class UserEnv
    {
        [DllImport("userenv.dll", EntryPoint = "CreateEnvironmentBlock")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CreateEnvironmentBlock(ref IntPtr lpEnvironment, IntPtr hToken, [MarshalAs(UnmanagedType.Bool)] bool bInherit);

    }
}
