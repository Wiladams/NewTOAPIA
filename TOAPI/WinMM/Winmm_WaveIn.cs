using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Security;

using TOAPI.Types;

namespace TOAPI.WinMM
{
    /// <summary>
    /// Used with the <see cref="NativeMethods.waveOutOpen"/> command.
    /// </summary>
    [Flags]
    public enum WAVEOPENFLAGS
    {
        /// <summary>
        /// No callback mechanism. This is the default setting.
        /// </summary>
        CALLBACK_NULL = 0x00000,

        /// <summary>
        /// Indicates the dwCallback parameter is a window handle.
        /// </summary>
        CALLBACK_WINDOW = 0x10000,

        /// <summary>
        /// The dwCallback parameter is a thread identifier.
        /// </summary>
        CALLBACK_THREAD = 0x20000,

        /// <summary>
        /// The dwCallback parameter is a thread identifier.
        /// </summary>
        [Obsolete]
        CALLBACK_TASK = 0x20000,

        /// <summary>
        /// The dwCallback parameter is a callback procedure address.
        /// </summary>
        CALLBACK_FUNCTION = 0x30000,

        /// <summary>
        /// If this flag is specified, <see cref="NativeMethods.waveOutOpen"/> queries the device to determine if it supports the given format, but the device is not actually opened.
        /// </summary>
        WAVE_FORMAT_QUERY = 0x00001,

        /// <summary>
        /// If this flag is specified, a synchronous waveform-audio device can be opened. If this flag is not specified while opening a synchronous driver, the device will fail to open.
        /// </summary>
        WAVE_ALLOWSYNC = 0x00002,

        /// <summary>
        /// If this flag is specified, the uDeviceID parameter specifies a waveform-audio device to be mapped to by the wave mapper.
        /// </summary>
        WAVE_MAPPED = 0x00004,

        /// <summary>
        /// If this flag is specified, the ACM driver does not perform conversions on the audio data.
        /// </summary>
        WAVE_FORMAT_DIRECT = 0x00008
    }

    // Callback for WaveOutOpen() and WaveInOpen() functions
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate void WaveCallback(IntPtr hWaveOut, int message, IntPtr userData, IntPtr wavhdr, IntPtr reserved);

    //public delegate void WaveAudioProc(IntPtr hwo, uint uMsg, IntPtr dwInstance, IntPtr dwParam1, int dwReserved);


    public partial class winmm
    {
        // WaveInAddBuffer
        //[DllImport("winmm.dll", EntryPoint = "waveInAddBuffer")]
        //public static extern int waveInAddBuffer(IntPtr hwi, WAVEHDR pwh, int cbwh);

        [DllImport("winmm.dll", EntryPoint = "waveInAddBuffer")]
        public static extern int waveInAddBuffer(WaveInSafeHandle hwi, IntPtr pwh, int cbwh);

        
        // WaveInClose
        //[DllImport("winmm.dll", EntryPoint = "waveInClose")]
        //public static extern int waveInClose(IntPtr hwi);

        [DllImport("winmm.dll", EntryPoint = "waveInClose")]
        public static extern MMSYSERROR waveInClose(WaveInSafeHandle hwi);

        // WaveInGetDevCaps
        [DllImport("winmm.dll", ExactSpelling = true, CharSet = CharSet.Unicode, EntryPoint = "waveInGetDevCapsW"),
        SuppressUnmanagedCodeSecurity]
        public static extern int waveInGetDevCapsW(int uDeviceID, 
            [Out, MarshalAs(UnmanagedType.LPStruct)] WAVEINCAPSW pwic, 
            int cbwic);

        // WaveInGetErrorText
        [DllImport("winmm.dll", ExactSpelling = true, CharSet = CharSet.Unicode, EntryPoint = "waveInGetErrorTextW"),
        SuppressUnmanagedCodeSecurity]
        public static extern int waveInGetErrorTextW(int mmrError, [Out] StringBuilder pszText, int cchText);

        // WaveInGetID
        [DllImport("winmm.dll", EntryPoint = "waveInGetID")]
        public static extern int waveInGetID(WaveInSafeHandle hwi, out int puDeviceID);

        // WaveInGetNumDevs
        [DllImport("winmm.dll", EntryPoint = "waveInGetNumDevs")]
        public static extern int waveInGetNumDevs();

        // WaveInGetPosition
        [DllImport("winmm.dll", EntryPoint = "waveInGetPosition")]
        public static extern int waveInGetPosition(WaveInSafeHandle hwi, ref MMTIME pmmt, int cbmmt);

        // WaveInMessage
        [DllImport("winmm.dll", EntryPoint = "waveInMessage")]
        public static extern int waveInMessage(WaveInSafeHandle hwi, int uMsg, IntPtr dw1, IntPtr dw2);

        // WaveInOpen
        //WINMMAPI MMRESULT WINAPI waveInOpen( OUT LPHWAVEIN phwi, IN UINT uDeviceID,
        //  IN LPCWAVEFORMATEX pwfx, IN DWORD_PTR dwCallback, IN DWORD_PTR dwInstance, IN DWORD fdwOpen);
        [DllImport("winmm.dll", EntryPoint = "waveInOpen")]
        public static extern MMSYSERROR waveInOpen(ref IntPtr phwi, 
            int uDeviceID,
            [In, MarshalAs(UnmanagedType.LPStruct)] WAVEFORMATEX pwfx, 
            WaveCallback dwCallback, 
            IntPtr dwInstance,
            WAVEOPENFLAGS openFlags);

        // WaveInPrepareHeader
        [DllImport("winmm.dll", EntryPoint = "waveInPrepareHeader"), SuppressUnmanagedCodeSecurity]
        public static extern int waveInPrepareHeader(WaveInSafeHandle hwi, 
            [In, Out, MarshalAs(UnmanagedType.LPStruct)] WAVEHDR pwh, 
            int cbwh);

        //[DllImport("winmm.dll", EntryPoint = "waveInPrepareHeader")]
        //public static extern int waveInPrepareHeader(IntPtr hwi, IntPtr pwh, int cbwh);

        // WaveInReset
        [DllImport("winmm.dll", EntryPoint = "waveInReset")]
        public static extern int waveInReset(WaveInSafeHandle hwi);

        // WaveInStart
        [DllImport("winmm.dll", EntryPoint = "waveInStart")]
        public static extern int waveInStart(WaveInSafeHandle hwi);

        // WaveInStop
        [DllImport("winmm.dll", EntryPoint = "waveInStop")]
        public static extern int waveInStop(WaveInSafeHandle hwi);

        // WaveInUnprepareHeader
        [DllImport("winmm.dll", EntryPoint = "waveInUnprepareHeader")]
        public static extern int waveInUnprepareHeader(WaveInSafeHandle hwi, 
            [In, Out, MarshalAs(UnmanagedType.LPStruct)] WAVEHDR pwh, 
            int cbwh);

        //[DllImport("winmm.dll", EntryPoint = "waveInUnprepareHeader")]
        //public static extern int waveInUnprepareHeader(IntPtr hwi, IntPtr pwh, int cbwh);
    }
}
