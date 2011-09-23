using System;
using System.Text;
using System.Runtime.InteropServices;

using TOAPI.Types;


namespace TOAPI.WinMM
{
    public partial class winmm
    {
        // timeGetSystemTime
        [DllImport("winmm.dll", EntryPoint = "timeGetSystemTime")]
        public static extern int timeGetSystemTime(ref MMTIME pmmt, int cbmmt);
        // timeGetTime
        [DllImport("winmm.dll", EntryPoint = "timeGetTime")]
        public static extern int timeGetTime();

        // timeGetDevCaps
        [DllImport("winmm.dll", EntryPoint = "timeGetDevCaps")]
        public static extern int timeGetDevCaps(ref TIMECAPS ptc, int cbtc);

        // timeKillEvent
        [DllImport("winmm.dll", EntryPoint = "timeKillEvent")]
        public static extern int timeKillEvent(int uTimerID);

        // timeSetEvent
        [DllImport("winmm.dll", EntryPoint = "timeSetEvent")]
        public static extern int timeSetEvent(int uDelay, int uResolution, TIMECALLBACK fptc, IntPtr dwUser, int fuEvent);

        // timeBeginPeriod
        [DllImport("winmm.dll", EntryPoint = "timeBeginPeriod")]
        public static extern int timeBeginPeriod(int uPeriod);

        // timeEndPeriod
        [DllImport("winmm.dll", EntryPoint = "timeEndPeriod")]
        public static extern int timeEndPeriod(int uPeriod);
    }
}
