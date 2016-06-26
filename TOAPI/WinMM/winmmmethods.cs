using System;
using System.Runtime.InteropServices;

namespace TOAPI.WinMM
{
    /// <summary>
    /// Old time Window Media routines, Transcoded from MMSystem.h
    /// A lot of these routines are superseded by Window Media Audio routines, but those are a lot more heavy weight,
    /// and require the use of the Windows Media SDK, or DirectShow, filters, and the like, which is far too much to
    /// deal with when you just want to open up a typical .wav file and play it.
    /// </summary>
    public partial class winmm
    {
        // Functions related to dealing with Waveform Audio
        // auxGetDevCaps
        [DllImport("winmm.dll", EntryPoint = "auxGetDevCapsW")]
        public static extern int auxGetDevCapsW(IntPtr uDeviceID, ref AUXCAPSW pac, int cbac);

        // auxGetNumDevs
        [DllImport("winmm.dll", EntryPoint = "auxGetNumDevs")]
        public static extern int auxGetNumDevs();
        
        // auxGetVolume
        [DllImport("winmm.dll", EntryPoint = "auxGetVolume")]
        public static extern int auxGetVolume(int uDeviceID, ref int pdwVolume);
        
        // auxOutMessage
        [DllImport("winmm.dll", EntryPoint = "auxOutMessage")]
        public static extern int auxOutMessage(int uDeviceID, int uMsg, int dw1, int dw2);
        
        // auxSetVolume
        [DllImport("winmm.dll", EntryPoint = "auxSetVolume")]
        public static extern int auxSetVolume(int uDeviceID, int dwVolume);











        // Higher level playback routines
        // PlaySound
        [DllImport("winmm.dll", CharSet=CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PlaySound([In] string pszSound, IntPtr hmod, int fdwSound);

        // sndPlaySound
        [DllImport("winmm.dll", EntryPoint = "sndPlaySoundW", CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool sndPlaySoundW([In] string pszSound, int fuSound);

    }
}
