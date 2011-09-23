using System;
using System.Text;
using System.Runtime.InteropServices;

using TOAPI.Types;

namespace TOAPI.WinMM
{
    public partial class winmm
    {
        // Low level multimedia io routines
        // These are for reading through RIFF files
        [DllImport("winmm.dll", EntryPoint = "mmioInstallIOProcW", SetLastError = true)]
        public static extern IntPtr mmioInstallIOProcW(uint fccIOProc, ref MMIOPROC pIOProc, uint dwFlags);

        [DllImport("winmm.dll", EntryPoint = "mmioOpenA")]
        public static extern System.IntPtr mmioOpenA([In][MarshalAs(UnmanagedType.LPStr)] string pszFileName, ref MMIOINFO pmmioinfo, uint fdwOpen);

        [DllImport("winmm.dll", EntryPoint = "mmioOpenW", SetLastError = true)]
        public static extern IntPtr mmioOpenW([In][MarshalAs(UnmanagedType.LPWStr)] string pszFileName, ref MMIOINFO pmmioinfo, uint fdwOpen);

        [DllImport("winmm.dll", EntryPoint = "mmioRenameW")]
        public static extern uint mmioRenameW([In] [MarshalAs(UnmanagedType.LPWStr)] string pszFileName, [In] [MarshalAs(UnmanagedType.LPWStr)] string pszNewFileName, ref MMIOINFO pmmioinfo, uint fdwRename);

        [DllImport("winmm.dll", EntryPoint = "mmioClose")]
        public static extern uint mmioClose(IntPtr hmmio, uint fuClose);

        // mmioRead
        [DllImport("winmm.dll", EntryPoint = "mmioRead")]
        public static extern int mmioRead(IntPtr hmmio, IntPtr pch, int cch);

        [DllImport("winmm.dll", EntryPoint = "mmioRead")]
        public static extern int mmioRead(IntPtr hmmio, ref WAVEFORMAT pch, int cch);

        [DllImport("winmm.dll", EntryPoint = "mmioWrite")]
        public static extern int mmioWrite(IntPtr hmmio, ref int pch, int cch);

        [DllImport("winmm.dll", EntryPoint = "mmioSeek")]
        public static extern int mmioSeek(IntPtr hmmio, int lOffset, int iOrigin);

        [DllImport("winmm.dll", EntryPoint = "mmioGetInfo")]
        public static extern uint mmioGetInfo(IntPtr hmmio, ref MMIOINFO pmmioinfo, uint fuInfo);

        [DllImport("winmm.dll", EntryPoint = "mmioSetInfo")]
        public static extern uint mmioSetInfo(IntPtr hmmio, ref MMIOINFO pmmioinfo, uint fuInfo);

        [DllImport("winmm.dll", EntryPoint = "mmioSetBuffer")]
        public static extern uint mmioSetBuffer(IntPtr hmmio, [MarshalAs(UnmanagedType.LPStr)] StringBuilder pchBuffer, int cchBuffer, uint fuBuffer);

        [DllImport("winmm.dll", EntryPoint = "mmioFlush")]
        public static extern uint mmioFlush(IntPtr hmmio, uint fuFlush);

        [DllImport("winmm.dll", EntryPoint = "mmioAdvance")]
        public static extern uint mmioAdvance(IntPtr hmmio, ref MMIOINFO pmmioinfo, uint fuAdvance);

        [DllImport("winmm.dll", EntryPoint = "mmioSendMessage")]
        [return: MarshalAs(UnmanagedType.SysInt)]
        public static extern int mmioSendMessage(IntPtr hmmio, uint uMsg, [MarshalAs(UnmanagedType.SysInt)] int lParam1, [MarshalAs(UnmanagedType.SysInt)] int lParam2);

        [DllImport("winmm.dll", EntryPoint = "mmioSendMessage")]
        public static extern IntPtr mmioSendMessage(IntPtr hmmio, uint uMsg, IntPtr lParam1, IntPtr lParam2);

        // mmioDescend
        [DllImport("winmm.dll", EntryPoint = "mmioDescend")]
        public static extern uint mmioDescend(IntPtr hmmio, ref MMCKINFO pmmcki, ref MMCKINFO pmmckiParent, uint fuDescend);

        [DllImport("winmm.dll", EntryPoint = "mmioDescend")]
        public static extern uint mmioDescend(IntPtr hmmio, ref MMCKINFO pmmcki, IntPtr pmmckiParent, uint fuDescend);

        [DllImport("winmm.dll", EntryPoint = "mmioAscend")]
        public static extern uint mmioAscend(IntPtr hmmio, ref MMCKINFO pmmcki, uint fuAscend);

        [DllImport("winmm.dll", EntryPoint = "mmioCreateChunk")]
        public static extern uint mmioCreateChunk(IntPtr hmmio, ref MMCKINFO pmmcki, uint fuCreate);

        [DllImport("winmm.dll", EntryPoint = "mmioStringToFOURCCW")]
        public static extern uint mmioStringToFOURCCW([In] [MarshalAs(UnmanagedType.LPWStr)] string sz, uint uFlags);
    }
}
