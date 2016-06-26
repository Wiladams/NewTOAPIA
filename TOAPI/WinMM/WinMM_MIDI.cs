using System;
using System.Runtime.InteropServices;

namespace TOAPI.WinMM
{
    public partial class winmm
    {
        [DllImport("winmm.dll", EntryPoint = "midiConnect")]
        public static extern int midiConnect(IntPtr hmi, IntPtr hmo, IntPtr pReserved);

        [DllImport("winmm.dll", EntryPoint = "midiDisconnect")]
        public static extern int midiDisconnect(IntPtr hmi, IntPtr hmo, IntPtr pReserved);




        [DllImport("winmm.dll", EntryPoint = "midiInOpen")]
        public static extern int midiInOpen(ref IntPtr phmi, int uDeviceID, int dwCallback, int dwInstance, int fdwOpen);

        [DllImport("winmm.dll", EntryPoint = "midiInStop")]
        public static extern int midiInStop(IntPtr hmi);

        [DllImport("winmm.dll", EntryPoint = "midiInClose")]
        public static extern int midiInClose(IntPtr hmi);

        [DllImport("winmm.dll", EntryPoint = "midiInGetID")]
        public static extern int midiInGetID(IntPtr hmi, ref int puDeviceID);

        [DllImport("winmm.dll", EntryPoint = "midiInReset")]
        public static extern int midiInReset(IntPtr hmi);

        [DllImport("winmm.dll", EntryPoint = "midiInStart")]
        public static extern int midiInStart(IntPtr hmi);

        [DllImport("winmm.dll", EntryPoint = "midiInMessage")]
        public static extern int midiInMessage(IntPtr hmi, int uMsg, int dw1, int dw2);

        [DllImport("winmm.dll", EntryPoint = "midiInAddBuffer")]
        public static extern int midiInAddBuffer(IntPtr hmi, ref midihdr pmh, int cbmh);

        [DllImport("winmm.dll", EntryPoint = "midiInGetNumDevs")]
        public static extern int midiInGetNumDevs();

        [DllImport("winmm.dll", EntryPoint = "midiInGetDevCapsW")]
        public static extern int midiInGetDevCapsW([MarshalAsAttribute(UnmanagedType.SysUInt)] int uDeviceID, ref MIDIINCAPSW pmic, int cbmic);

        [DllImport("winmm.dll", EntryPoint = "midiInGetErrorTextW")]
        public static extern int midiInGetErrorTextW(int mmrError, [MarshalAsAttribute(UnmanagedType.LPWStr)] System.Text.StringBuilder pszText, int cchText);

        [DllImport("winmm.dll", EntryPoint = "midiInPrepareHeader")]
        public static extern int midiInPrepareHeader(IntPtr hmi, ref midihdr pmh, int cbmh);

        [DllImport("winmm.dll", EntryPoint = "midiInUnprepareHeader")]
        public static extern int midiInUnprepareHeader(IntPtr hmi, ref midihdr pmh, int cbmh);






        [DllImport("winmm.dll", EntryPoint = "midiOutGetNumDevs")]
        public static extern int midiOutGetNumDevs();

        [DllImport("winmm.dll", EntryPoint = "midiOutOpen")]
        public static extern int midiOutOpen(ref IntPtr phmo, int uDeviceID, int dwCallback, int dwInstance, int fdwOpen);

        [DllImport("winmm.dll", EntryPoint = "midiOutClose")]
        public static extern int midiOutClose(IntPtr hmo);

        [DllImport("winmm.dll", EntryPoint = "midiOutGetID")]
        public static extern int midiOutGetID(IntPtr hmo, ref int puDeviceID);

        [DllImport("winmm.dll", EntryPoint = "midiOutReset")]
        public static extern int midiOutReset(IntPtr hmo);

        [DllImport("winmm.dll", EntryPoint = "midiOutLongMsg")]
        public static extern int midiOutLongMsg(IntPtr hmo, ref midihdr pmh, int cbmh);

        [DllImport("winmm.dll", EntryPoint = "midiOutMessage")]
        public static extern int midiOutMessage(IntPtr hmo, int uMsg, int dw1, int dw2);

        [DllImport("winmm.dll", EntryPoint = "midiOutShortMsg")]
        public static extern int midiOutShortMsg(IntPtr hmo, int dwMsg);

        [DllImport("winmm.dll", EntryPoint = "midiOutGetVolume")]
        public static extern int midiOutGetVolume(IntPtr hmo, ref int pdwVolume);

        [DllImport("winmm.dll", EntryPoint = "midiOutSetVolume")]
        public static extern int midiOutSetVolume(IntPtr hmo, int dwVolume);


        [DllImport("winmm.dll", EntryPoint = "midiOutGetDevCapsW")]
        public static extern int midiOutGetDevCapsW([MarshalAsAttribute(UnmanagedType.SysUInt)] int uDeviceID, ref MIDIOUTCAPSW pmoc, int cbmoc);

        [DllImport("winmm.dll", EntryPoint = "midiOutCachePatches")]
        public static extern int midiOutCachePatches(IntPtr hmo, int uBank, ref ushort pwpa, int fuCache);

        [DllImport("winmm.dll", EntryPoint = "midiOutGetErrorTextW")]
        public static extern int midiOutGetErrorTextW(int mmrError, [MarshalAsAttribute(UnmanagedType.LPWStr)] System.Text.StringBuilder pszText, int cchText);

        [DllImport("winmm.dll", EntryPoint = "midiOutPrepareHeader")]
        public static extern int midiOutPrepareHeader(IntPtr hmo, ref midihdr pmh, int cbmh);

        [DllImport("winmm.dll", EntryPoint = "midiOutUnprepareHeader")]
        public static extern int midiOutUnprepareHeader(IntPtr hmo, ref midihdr pmh, int cbmh);

        [DllImport("winmm.dll", EntryPoint = "midiOutCacheDrumPatches")]
        public static extern int midiOutCacheDrumPatches(IntPtr hmo, int uPatch, ref ushort pwkya, int fuCache);





        [DllImport("winmm.dll", EntryPoint = "midiStreamOut")]
        public static extern int midiStreamOut(IntPtr hms, ref midihdr pmh, int cbmh);

        [DllImport("winmm.dll", EntryPoint = "midiStreamOpen")]
        public static extern int midiStreamOpen(ref IntPtr phms, ref int puDeviceID, int cMidi, int dwCallback, int dwInstance, int fdwOpen);

        [DllImport("winmm.dll", EntryPoint = "midiStreamClose")]
        public static extern int midiStreamClose(IntPtr hms);

        [DllImport("winmm.dll", EntryPoint = "midiStreamRestart")]
        public static extern int midiStreamRestart(IntPtr hms);

        [DllImport("winmm.dll", EntryPoint = "midiStreamStop")]
        public static extern int midiStreamStop(IntPtr hms);

        [DllImport("winmm.dll", EntryPoint = "midiStreamPause")]
        public static extern int midiStreamPause(IntPtr hms);

        [DllImport("winmm.dll", EntryPoint = "midiStreamPosition")]
        public static extern int midiStreamPosition(IntPtr hms, ref mmtime_tag lpmmt, int cbmmt);

        [DllImport("winmm.dll", EntryPoint = "midiStreamProperty")]
        public static extern int midiStreamProperty(IntPtr hms, ref byte lppropdata, int dwProperty);

    }
}
