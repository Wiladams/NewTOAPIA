
using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Security;

namespace TOAPI.WinMM
{
    using Types;

    public partial class winmm
    {
        // WaveOutBreakLoop
        [DllImport("winmm.dll", EntryPoint = "waveOutBreakLoop")]
        public static extern MMSYSERROR waveOutBreakLoop(WaveOutSafeHandle hwo);

        // WaveOutClose
        [DllImport("winmm.dll", EntryPoint = "waveOutClose")]
        public static extern MMSYSERROR waveOutClose(WaveOutSafeHandle hwo);

        // WaveOutGetDevCaps
        // Use IntPtr because the parameter can be either a device index, or a handle 
        // to an already opened device.
        [DllImport("winmm.dll", CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        public static extern MMSYSERROR waveOutGetDevCaps(IntPtr uDeviceID, ref WAVEOUTCAPS pwoc,  int cbwoc);

        //[DllImport("winmm.dll", EntryPoint = "waveOutGetDevCapsW")]
        //public static extern MMSYSERROR waveOutGetDevCapsW(IntPtr uDeviceID, ref WAVEOUTCAPS pwoc, int cbwoc);

        //[DllImport("winmm.dll", EntryPoint = "waveOutGetDevCapsA")]
        //public static extern MMSYSERROR waveOutGetDevCapsA(IntPtr uDeviceID, ref WAVEOUTCAPSA pwoc, uint cbwoc);


        // WaveOutGetErrorText
        [DllImport("winmm.dll", CharSet = CharSet.Auto)]
        public static extern MMSYSERROR waveOutGetErrorText(int mmrError, StringBuilder pszText, int cchText);

        // WaveOutGetID
        [DllImport("winmm.dll", EntryPoint = "waveOutGetID")]
        public static extern MMSYSERROR waveOutGetID(WaveOutSafeHandle hwo, out int puDeviceID);

        // WaveOutGetNumDevs
        [DllImport("winmm.dll", EntryPoint = "waveOutGetNumDevs")]
        public static extern int waveOutGetNumDevs();

        // WaveOutGetPitch
        [DllImport("winmm.dll", EntryPoint = "waveOutGetPitch")]
        public static extern MMSYSERROR waveOutGetPitch(WaveOutSafeHandle hwo, out int pdwPitch);

        // WaveOutGetPlaybackRate
        [DllImport("winmm.dll", EntryPoint = "waveOutGetPlaybackRate")]
        public static extern MMSYSERROR waveOutGetPlaybackRate(WaveOutSafeHandle hwo, out int pdwRate);

        // WaveOutGetPosition
        [DllImport("winmm.dll", EntryPoint = "waveOutGetPosition")]
        public static extern MMSYSERROR waveOutGetPosition(WaveOutSafeHandle hwo, ref MMTIME pmmt, int cbmmt);

        // WaveOutGetVolume
        [DllImport("winmm.dll", EntryPoint = "waveOutGetVolume")]
        public static extern MMSYSERROR waveOutGetVolume(WaveOutSafeHandle hwo, out int pdwVolume);

        // WaveOutMessage
        [DllImport("winmm.dll", EntryPoint = "waveOutMessage")]
        public static extern MMSYSERROR waveOutMessage(WaveOutSafeHandle hwo, uint uMsg, uint dw1, uint dw2);

        // WaveOutOpen
        [DllImport("winmm.dll", EntryPoint = "waveOutOpen")]
        public static extern MMSYSERROR waveOutOpen(ref IntPtr phwo, 
            int uDeviceID,
            [In, MarshalAs(UnmanagedType.LPStruct)]WAVEFORMATEX pwfx, 
            WaveCallback dwCallback,    // When using a callback function
            IntPtr dwInstance, 
            int openFlags);

        // WaveOutPause
        [DllImport("winmm.dll", EntryPoint = "waveOutPause")]
        public static extern MMSYSERROR waveOutPause(WaveOutSafeHandle hwo);

        // WaveOutPrepareHeader
        [DllImport("winmm.dll", EntryPoint = "waveOutPrepareHeader")]
        public static extern MMSYSERROR waveOutPrepareHeader(WaveOutSafeHandle hwo, 
            [In, Out, MarshalAs(UnmanagedType.LPStruct)] WAVEHDR pwh, 
            int cbwh);

        //[DllImport("winmm.dll", EntryPoint = "waveOutPrepareHeader")]
        //public static extern int waveOutPrepareHeader(IntPtr hwo, IntPtr pwh, int cbwh);

        // -- WaveOutProc --
        // WaveOutReset
        [DllImport("winmm.dll", EntryPoint = "waveOutReset")]
        public static extern MMSYSERROR waveOutReset(WaveOutSafeHandle hwo);

        // WaveOutRestart
        [DllImport("winmm.dll", EntryPoint = "waveOutRestart")]
        public static extern MMSYSERROR waveOutRestart(WaveOutSafeHandle hwo);

        // WaveOutSetPitch
        [DllImport("winmm.dll", EntryPoint = "waveOutSetPitch")]
        public static extern MMSYSERROR waveOutSetPitch(WaveOutSafeHandle hwo, int dwPitch);

        // WaveOutSetPlaybackRate
        [DllImport("winmm.dll", EntryPoint = "waveOutSetPlaybackRate")]
        public static extern MMSYSERROR waveOutSetPlaybackRate(WaveOutSafeHandle hwo, int dwRate);

        // WaveOutSetVolume
        [DllImport("winmm.dll", EntryPoint = "waveOutSetVolume")]
        public static extern MMSYSERROR waveOutSetVolume(WaveOutSafeHandle hwo, int dwVolume);

        // WaveOutUnprepareHeader
        [DllImport("winmm.dll", EntryPoint = "waveOutUnprepareHeader")]
        public static extern MMSYSERROR waveOutUnprepareHeader(WaveOutSafeHandle hwo, 
            [In, Out, MarshalAs(UnmanagedType.LPStruct)]WAVEHDR pwh, 
            int cbwh);

        //[DllImport("winmm.dll", EntryPoint = "waveOutUnprepareHeader")]
        //public static extern int waveOutUnprepareHeader(IntPtr hwo, IntPtr pwh, int cbwh);

        // WaveOutWrite
        [DllImport("winmm.dll", EntryPoint = "waveOutWrite", SetLastError = true), SuppressUnmanagedCodeSecurity]
        public static extern MMSYSERROR waveOutWrite(WaveOutSafeHandle hwo, 
            [In, Out, MarshalAs(UnmanagedType.LPStruct)] WAVEHDR pwh, 
            int cbwh);

        [DllImport("winmm.dll", EntryPoint = "waveOutWrite", SetLastError = true)]
        public static extern MMSYSERROR waveOutWrite(WaveOutSafeHandle hwo, IntPtr pwh, int cbwh);
    }
}
