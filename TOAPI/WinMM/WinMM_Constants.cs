using System;
using System.Collections.Generic;
using System.Text;

using WORD = System.Int16;

namespace TOAPI.WinMM
{

    public partial class winmm
    {
        // device ID for wave device mapper
        //public const uint WAVE_MAPPER = unchecked((uint)(-1));
        public const int WAVE_MAPPER = -1;

        // The following would be API accurate, but won't work for VB because VB
        // does not support unsigned.
        //public const uint WAVE_MAPPER = unchecked((uint)(-1));



        public const uint CALLBACK_NULL = 0x00000000;    /* no callback */
        public const uint CALLBACK_WINDOW = 0x00010000;    /* dwCallback is a HWND */
        public const uint CALLBACK_TASK = 0x00020000;    /* dwCallback is a HTASK */
        public const uint CALLBACK_FUNCTION = 0x00030000;    /* dwCallback is a FARPROC */
        public const uint CALLBACK_THREAD = (CALLBACK_TASK);/* thread ID replaces 16 bit task */
        public const uint CALLBACK_EVENT = 0x00050000;    /* dwCallback is an EVENT Handle */
        public const uint CALLBACK_TYPEMASK = 0x00070000;    /* callback type mask */

        // Multimedia Extensions Window Messages
        // from MMSystem.h
        public const int MM_JOY1MOVE = 0x3A0;           /* joystick */
        public const int MM_JOY2MOVE = 0x3A1;
        public const int MM_JOY1ZMOVE = 0x3A2;
        public const int MM_JOY2ZMOVE = 0x3A3;
        public const int MM_JOY1BUTTONDOWN = 0x3B5;
        public const int MM_JOY2BUTTONDOWN = 0x3B6;
        public const int MM_JOY1BUTTONUP = 0x3B7;
        public const int MM_JOY2BUTTONUP = 0x3B8;

        public const int MM_MCINOTIFY = 0x3B9;           /* MCI */

        public const int MM_WOM_OPEN = 0x3BB;           /* waveform output */
        public const int MM_WOM_CLOSE = 0x3BC;
        public const int MM_WOM_DONE = 0x3BD;

        public const int MM_WIM_OPEN = 0x3BE;           /* waveform input */
        public const int MM_WIM_CLOSE = 0x3BF;
        public const int MM_WIM_DATA = 0x3C0;

        public const int MM_MIM_OPEN = 0x3C1;           /* MIDI input */
        public const int MM_MIM_CLOSE = 0x3C2;
        public const int MM_MIM_DATA = 0x3C3;
        public const int MM_MIM_LONGDATA = 0x3C4;
        public const int MM_MIM_ERROR = 0x3C5;
        public const int MM_MIM_LONGERROR = 0x3C6;

        public const int MM_MOM_OPEN = 0x3C7;           /* MIDI output */
        public const int MM_MOM_CLOSE = 0x3C8;
        public const int MM_MOM_DONE = 0x3C9;



        // general error return values
        public const int MMSYSERR_BASE = 0;
        public const int MMSYSERR_NOERROR = 0;                    /* no error */
        public const int MMSYSERR_ERROR = (MMSYSERR_BASE + 1);  /* unspecified error */
        public const int MMSYSERR_BADDEVICEID = (MMSYSERR_BASE + 2);  /* device ID out of range */
        public const int MMSYSERR_NOTENABLED = (MMSYSERR_BASE + 3);  /* driver failed enable */
        public const int MMSYSERR_ALLOCATED = (MMSYSERR_BASE + 4);  /* device already allocated */
        public const int MMSYSERR_INVALHANDLE = (MMSYSERR_BASE + 5);  /* device handle is invalid */
        public const int MMSYSERR_NODRIVER = (MMSYSERR_BASE + 6);  /* no device driver present */
        public const int MMSYSERR_NOMEM = (MMSYSERR_BASE + 7);  /* memory allocation error */
        public const int MMSYSERR_NOTSUPPORTED = (MMSYSERR_BASE + 8);  /* function isn't supported */
        public const int MMSYSERR_BADERRNUM = (MMSYSERR_BASE + 9);  /* error value out of range */
        public const int MMSYSERR_INVALFLAG = (MMSYSERR_BASE + 10); /* invalid flag passed */
        public const int MMSYSERR_INVALPARAM = (MMSYSERR_BASE + 11); /* invalid parameter passed */
        public const int MMSYSERR_HANDLEBUSY = (MMSYSERR_BASE + 12); /* handle being used */
        /* simultaneously on another */
        /* thread (eg callback) */
        public const int MMSYSERR_INVALIDALIAS = (MMSYSERR_BASE + 13); /* specified alias not found */
        public const int MMSYSERR_BADDB = (MMSYSERR_BASE + 14); /* bad registry database */
        public const int MMSYSERR_KEYNOTFOUND = (MMSYSERR_BASE + 15); /* registry key not found */
        public const int MMSYSERR_READERROR = (MMSYSERR_BASE + 16); /* registry read error */
        public const int MMSYSERR_WRITEERROR = (MMSYSERR_BASE + 17); /* registry write error */
        public const int MMSYSERR_DELETEERROR = (MMSYSERR_BASE + 18); /* registry delete error */
        public const int MMSYSERR_VALNOTFOUND = (MMSYSERR_BASE + 19); /* registry value not found */
        public const int MMSYSERR_NODRIVERCB = (MMSYSERR_BASE + 20); /* driver does not call DriverCallback */
        public const int MMSYSERR_MOREDATA = (MMSYSERR_BASE + 21); /* more data to be returned */
        public const int MMSYSERR_LASTERROR = (MMSYSERR_BASE + 21); /* last error in range */

        /* waveform audio error return values */
        public const int WAVERR_BASE = 32;
        public const int WAVERR_BADFORMAT = (WAVERR_BASE + 0);    /* unsupported wave format */
        public const int WAVERR_STILLPLAYING = (WAVERR_BASE + 1);    /* still something playing */
        public const int WAVERR_UNPREPARED = (WAVERR_BASE + 2);    /* header not prepared */
        public const int WAVERR_SYNC = (WAVERR_BASE + 3);    /* device is synchronous */
        public const int WAVERR_LASTERROR = (WAVERR_BASE + 3);    /* last error in range */

        // flags for dwFlags parameter in waveOutOpen() and waveInOpen()
        public const int WAVE_FORMAT_QUERY = 0x0001;
        public const int WAVE_ALLOWSYNC = 0x0002;
        public const int WAVE_MAPPED = 0x0004;
        public const int WAVE_FORMAT_DIRECT = 0x0008;
        public const int WAVE_FORMAT_DIRECT_QUERY = (WAVE_FORMAT_QUERY | WAVE_FORMAT_DIRECT);

        // flags for dwFlags field of WAVEHDR
        public const int WHDR_DONE = 0x00000001;  /* done bit */
        public const int WHDR_PREPARED = 0x00000002;  /* set if this header has been prepared */
        public const int WHDR_BEGINLOOP = 0x00000004;  /* loop start block */
        public const int WHDR_ENDLOOP = 0x00000008;  /* loop end block */
        public const int WHDR_INQUEUE = 0x00000010;  /* reserved for driver */


        // flags for dwSupport field of WAVEOUTCAPS
        public const int WAVECAPS_PITCH = 0x0001;   /* supports pitch control */
        public const int WAVECAPS_PLAYBACKRATE = 0x0002;   /* supports playback rate control */
        public const int WAVECAPS_VOLUME = 0x0004;   /* supports volume control */
        public const int WAVECAPS_LRVOLUME = 0x0008;   /* separate left-right volume control */
        public const int WAVECAPS_SYNC = 0x0010;
        public const int WAVECAPS_SAMPLEACCURATE = 0x0020;


        // defines for dwFormat field of WAVEINCAPS and WAVEOUTCAPS
        public const int WAVE_INVALIDFORMAT = 0x00000000;       /* invalid format */
        public const int WAVE_FORMAT_1M08 = 0x00000001;       /* 11.025 kHz, Mono,   8-bit  */
        public const int WAVE_FORMAT_1S08 = 0x00000002;       /* 11.025 kHz, Stereo, 8-bit  */
        public const int WAVE_FORMAT_1M16 = 0x00000004;       /* 11.025 kHz, Mono,   16-bit */
        public const int WAVE_FORMAT_1S16 = 0x00000008;       /* 11.025 kHz, Stereo, 16-bit */
        public const int WAVE_FORMAT_2M08 = 0x00000010;       /* 22.05  kHz, Mono,   8-bit  */
        public const int WAVE_FORMAT_2S08 = 0x00000020;       /* 22.05  kHz, Stereo, 8-bit  */
        public const int WAVE_FORMAT_2M16 = 0x00000040;       /* 22.05  kHz, Mono,   16-bit */
        public const int WAVE_FORMAT_2S16 = 0x00000080;       /* 22.05  kHz, Stereo, 16-bit */
        public const int WAVE_FORMAT_4M08 = 0x00000100;       /* 44.1   kHz, Mono,   8-bit  */
        public const int WAVE_FORMAT_4S08 = 0x00000200;       /* 44.1   kHz, Stereo, 8-bit  */
        public const int WAVE_FORMAT_4M16 = 0x00000400;       /* 44.1   kHz, Mono,   16-bit */
        public const int WAVE_FORMAT_4S16 = 0x00000800;       /* 44.1   kHz, Stereo, 16-bit */

        public const int WAVE_FORMAT_44M08 = 0x00000100;       /* 44.1   kHz, Mono,   8-bit  */
        public const int WAVE_FORMAT_44S08 = 0x00000200;       /* 44.1   kHz, Stereo, 8-bit  */
        public const int WAVE_FORMAT_44M16 = 0x00000400;       /* 44.1   kHz, Mono,   16-bit */
        public const int WAVE_FORMAT_44S16 = 0x00000800;       /* 44.1   kHz, Stereo, 16-bit */
        public const int WAVE_FORMAT_48M08 = 0x00001000;       /* 48     kHz, Mono,   8-bit  */
        public const int WAVE_FORMAT_48S08 = 0x00002000;       /* 48     kHz, Stereo, 8-bit  */
        public const int WAVE_FORMAT_48M16 = 0x00004000;       /* 48     kHz, Mono,   16-bit */
        public const int WAVE_FORMAT_48S16 = 0x00008000;       /* 48     kHz, Stereo, 16-bit */
        public const int WAVE_FORMAT_96M08 = 0x00010000;       /* 96     kHz, Mono,   8-bit  */
        public const int WAVE_FORMAT_96S08 = 0x00020000;       /* 96     kHz, Stereo, 8-bit  */
        public const int WAVE_FORMAT_96M16 = 0x00040000;       /* 96     kHz, Mono,   16-bit */
        public const int WAVE_FORMAT_96S16 = 0x00080000;       /* 96     kHz, Stereo, 16-bit */

        // wave callback messages
        // Output callbacks
        public const int WOM_OPEN = MM_WOM_OPEN;
        public const int WOM_CLOSE = MM_WOM_CLOSE;
        public const int WOM_DONE = MM_WOM_DONE;

        // Input callbacks
        public const int WIM_OPEN = MM_WIM_OPEN;
        public const int WIM_CLOSE = MM_WIM_CLOSE;
        public const int WIM_DATA = MM_WIM_DATA;


        // Mixer Constants
        public const int MIXER_SHORT_NAME_CHARS = 16;
        public const int MIXER_LONG_NAME_CHARS = 64;


        //  MMRESULT error return values specific to the mixer API
        public const int MIXERR_BASE = 1024;
        public const int MIXERR_INVALLINE = (MIXERR_BASE + 0);
        public const int MIXERR_INVALCONTROL = (MIXERR_BASE + 1);
        public const int MIXERR_INVALVALUE = (MIXERR_BASE + 2);
        public const int MIXERR_LASTERROR = (MIXERR_BASE + 2);


        public const uint MIXER_OBJECTF_MIXER = 0x00000000;
        public const uint MIXER_OBJECTF_WAVEOUT = 0x10000000;
        public const uint MIXER_OBJECTF_WAVEIN = 0x20000000;
        public const uint MIXER_OBJECTF_MIDIOUT = 0x30000000;
        public const uint MIXER_OBJECTF_MIDIIN = 0x40000000;
        public const uint MIXER_OBJECTF_AUX = 0x50000000;

        public const uint MIXER_OBJECTF_HANDLE = 0x80000000;
        public const uint MIXER_OBJECTF_HMIXER = (MIXER_OBJECTF_HANDLE | MIXER_OBJECTF_MIXER);
        public const uint MIXER_OBJECTF_HWAVEOUT = (MIXER_OBJECTF_HANDLE | MIXER_OBJECTF_WAVEOUT);
        public const uint MIXER_OBJECTF_HWAVEIN = (MIXER_OBJECTF_HANDLE | MIXER_OBJECTF_WAVEIN);
        public const uint MIXER_OBJECTF_HMIDIOUT = (MIXER_OBJECTF_HANDLE | MIXER_OBJECTF_MIDIOUT);
        public const uint MIXER_OBJECTF_HMIDIIN = (MIXER_OBJECTF_HANDLE | MIXER_OBJECTF_MIDIIN);

        public const int MIXER_GETLINEINFOF_DESTINATION = 0x00000000;
        public const int MIXER_GETLINEINFOF_SOURCE = 0x00000001;
        public const int MIXER_GETLINEINFOF_LINEID = 0x00000002;
        public const int MIXER_GETLINEINFOF_COMPONENTTYPE = 0x00000003;
        public const int MIXER_GETLINEINFOF_TARGETTYPE = 0x00000004;

        public const int MIXER_GETLINEINFOF_QUERYMASK = 0x0000000F;


        //  MIXERLINE.fdwLine
        public const uint MIXERLINE_LINEF_ACTIVE = 0x00000001;
        public const uint MIXERLINE_LINEF_DISCONNECTED = 0x00008000;
        public const uint MIXERLINE_LINEF_SOURCE = unchecked(0x80000000);


        //  MIXERLINE.dwComponentType
        //  component types for destinations and sources
        public const int MIXERLINE_COMPONENTTYPE_DST_FIRST = 0x00000000;
        public const int MIXERLINE_COMPONENTTYPE_DST_UNDEFINED = (MIXERLINE_COMPONENTTYPE_DST_FIRST + 0);
        public const int MIXERLINE_COMPONENTTYPE_DST_DIGITAL = (MIXERLINE_COMPONENTTYPE_DST_FIRST + 1);
        public const int MIXERLINE_COMPONENTTYPE_DST_LINE = (MIXERLINE_COMPONENTTYPE_DST_FIRST + 2);
        public const int MIXERLINE_COMPONENTTYPE_DST_MONITOR = (MIXERLINE_COMPONENTTYPE_DST_FIRST + 3);
        public const int MIXERLINE_COMPONENTTYPE_DST_SPEAKERS = (MIXERLINE_COMPONENTTYPE_DST_FIRST + 4);
        public const int MIXERLINE_COMPONENTTYPE_DST_HEADPHONES = (MIXERLINE_COMPONENTTYPE_DST_FIRST + 5);
        public const int MIXERLINE_COMPONENTTYPE_DST_TELEPHONE = (MIXERLINE_COMPONENTTYPE_DST_FIRST + 6);
        public const int MIXERLINE_COMPONENTTYPE_DST_WAVEIN = (MIXERLINE_COMPONENTTYPE_DST_FIRST + 7);
        public const int MIXERLINE_COMPONENTTYPE_DST_VOICEIN = (MIXERLINE_COMPONENTTYPE_DST_FIRST + 8);
        public const int MIXERLINE_COMPONENTTYPE_DST_LAST = (MIXERLINE_COMPONENTTYPE_DST_FIRST + 8);

        public const int MIXERLINE_COMPONENTTYPE_SRC_FIRST = 0x00001000;
        public const int MIXERLINE_COMPONENTTYPE_SRC_UNDEFINED = (MIXERLINE_COMPONENTTYPE_SRC_FIRST + 0);
        public const int MIXERLINE_COMPONENTTYPE_SRC_DIGITAL = (MIXERLINE_COMPONENTTYPE_SRC_FIRST + 1);
        public const int MIXERLINE_COMPONENTTYPE_SRC_LINE = (MIXERLINE_COMPONENTTYPE_SRC_FIRST + 2);
        public const int MIXERLINE_COMPONENTTYPE_SRC_MICROPHONE = (MIXERLINE_COMPONENTTYPE_SRC_FIRST + 3);
        public const int MIXERLINE_COMPONENTTYPE_SRC_SYNTHESIZER = (MIXERLINE_COMPONENTTYPE_SRC_FIRST + 4);
        public const int MIXERLINE_COMPONENTTYPE_SRC_COMPACTDISC = (MIXERLINE_COMPONENTTYPE_SRC_FIRST + 5);
        public const int MIXERLINE_COMPONENTTYPE_SRC_TELEPHONE = (MIXERLINE_COMPONENTTYPE_SRC_FIRST + 6);
        public const int MIXERLINE_COMPONENTTYPE_SRC_PCSPEAKER = (MIXERLINE_COMPONENTTYPE_SRC_FIRST + 7);
        public const int MIXERLINE_COMPONENTTYPE_SRC_WAVEOUT = (MIXERLINE_COMPONENTTYPE_SRC_FIRST + 8);
        public const int MIXERLINE_COMPONENTTYPE_SRC_AUXILIARY = (MIXERLINE_COMPONENTTYPE_SRC_FIRST + 9);
        public const int MIXERLINE_COMPONENTTYPE_SRC_ANALOG = (MIXERLINE_COMPONENTTYPE_SRC_FIRST + 10);
        public const int MIXERLINE_COMPONENTTYPE_SRC_LAST = (MIXERLINE_COMPONENTTYPE_SRC_FIRST + 10);


        //  MIXERLINE.Target.dwType
        public const int MIXERLINE_TARGETTYPE_UNDEFINED = 0;
        public const int MIXERLINE_TARGETTYPE_WAVEOUT = 1;
        public const int MIXERLINE_TARGETTYPE_WAVEIN = 2;
        public const int MIXERLINE_TARGETTYPE_MIDIOUT = 3;
        public const int MIXERLINE_TARGETTYPE_MIDIIN = 4;
        public const int MIXERLINE_TARGETTYPE_AUX = 5;




        /****************************************************************************

                            Timer support

****************************************************************************/

        // timer error return values
        public const int TIMERR_BASE = 96;
        public const int TIMERR_NOERROR = (0);                  // no error
        public const int TIMERR_NOCANDO = (TIMERR_BASE + 1);      // request not completed
        public const int TIMERR_STRUCT = (TIMERR_BASE + 33);   // time struct size

        /* timer data types */
        //typedef void (CALLBACK TIMECALLBACK)(UINT uTimerID, UINT uMsg, DWORD_PTR dwUser, DWORD_PTR dw1, DWORD_PTR dw2);


        /* flags for fuEvent parameter of timeSetEvent() function */
        public const int TIME_ONESHOT = 0x0000;   /* program timer for single event */
        public const int TIME_PERIODIC = 0x0001;   /* program for continuous periodic event */

        public const int TIME_CALLBACK_FUNCTION = 0x0000;  /* callback is function */
        public const int TIME_CALLBACK_EVENT_SET = 0x0010;  /* callback is event - use SetEvent */
        public const int TIME_CALLBACK_EVENT_PULSE = 0x0020;  /* callback is event - use PulseEvent */

        public const int TIME_KILL_SYNCHRONOUS = 0x0100;  /* This flag prevents the event from occurring */
        /* after the user calls timeKillEvent() to */
        /* destroy it. */


        /****************************************************************************

                            Joystick support

****************************************************************************/
        public const int MAX_JOYSTICKOEMVXDNAME = 260; /* max oem vxd name length (including NULL) */

        /* joystick error return values */
        public const int JOYERR_BASE = 160;

        public const int JOYERR_NOERROR = (0);                  /* no error */
        public const int JOYERR_PARMS = (JOYERR_BASE + 5);      /* bad parameters */
        public const int JOYERR_NOCANDO = (JOYERR_BASE + 6);      /* request not completed */
        public const int JOYERR_UNPLUGGED = (JOYERR_BASE + 7);      /* joystick is unplugged */

        /* constants used with JOYINFO and JOYINFOEX structures and MM_JOY* messages */
        public const int JOY_BUTTON1 = 0x0001;
        public const int JOY_BUTTON2 = 0x0002;
        public const int JOY_BUTTON3 = 0x0004;
        public const int JOY_BUTTON4 = 0x0008;
        public const int JOY_BUTTON1CHG = 0x0100;
        public const int JOY_BUTTON2CHG = 0x0200;
        public const int JOY_BUTTON3CHG = 0x0400;
        public const int JOY_BUTTON4CHG = 0x0800;

        /* constants used with JOYINFOEX */
        public const int JOY_BUTTON5 = 0x00000010;
        public const int JOY_BUTTON6 = 0x00000020;
        public const int JOY_BUTTON7 = 0x00000040;
        public const int JOY_BUTTON8 = 0x00000080;
        public const int JOY_BUTTON9 = 0x00000100;
        public const int JOY_BUTTON10 = 0x00000200;
        public const int JOY_BUTTON11 = 0x00000400;
        public const int JOY_BUTTON12 = 0x00000800;
        public const int JOY_BUTTON13 = 0x00001000;
        public const int JOY_BUTTON14 = 0x00002000;
        public const int JOY_BUTTON15 = 0x00004000;
        public const int JOY_BUTTON16 = 0x00008000;
        public const int JOY_BUTTON17 = 0x00010000;
        public const int JOY_BUTTON18 = 0x00020000;
        public const int JOY_BUTTON19 = 0x00040000;
        public const int JOY_BUTTON20 = 0x00080000;
        public const int JOY_BUTTON21 = 0x00100000;
        public const int JOY_BUTTON22 = 0x00200000;
        public const int JOY_BUTTON23 = 0x00400000;
        public const int JOY_BUTTON24 = 0x00800000;
        public const int JOY_BUTTON25 = 0x01000000;
        public const int JOY_BUTTON26 = 0x02000000;
        public const int JOY_BUTTON27 = 0x04000000;
        public const int JOY_BUTTON28 = 0x08000000;
        public const int JOY_BUTTON29 = 0x10000000;
        public const int JOY_BUTTON30 = 0x20000000;
        public const int JOY_BUTTON31 = 0x40000000;
        public const uint JOY_BUTTON32 = 0x80000000;

        /* constants used with JOYINFOEX structure */
        public const int JOY_POVCENTERED = (WORD)( - 1);
        public const int JOY_POVFORWARD = 0;
        public const int JOY_POVRIGHT = 9000;
        public const int JOY_POVBACKWARD = 18000;
        public const int JOY_POVLEFT = 27000;

        public const int JOY_RETURNX = 0x00000001;
        public const int JOY_RETURNY = 0x00000002;
        public const int JOY_RETURNZ = 0x00000004;
        public const int JOY_RETURNR = 0x00000008;
        public const int JOY_RETURNU = 0x00000010;    /* axis 5 */
        public const int JOY_RETURNV = 0x00000020;     /* axis 6 */
        public const int JOY_RETURNPOV = 0x00000040;
        public const int JOY_RETURNBUTTONS = 0x00000080;
        public const int JOY_RETURNRAWDATA = 0x00000100;
        public const int JOY_RETURNPOVCTS = 0x00000200;
        public const int JOY_RETURNCENTERED = 0x00000400;
        public const int JOY_USEDEADZONE = 0x00000800;
        public const int JOY_RETURNALL = (JOY_RETURNX | JOY_RETURNY | JOY_RETURNZ |
                                         JOY_RETURNR | JOY_RETURNU | JOY_RETURNV |
                                         JOY_RETURNPOV | JOY_RETURNBUTTONS);
        public const int JOY_CAL_READALWAYS = 0x00010000;
        public const int JOY_CAL_READXYONLY = 0x00020000;
        public const int JOY_CAL_READ3 = 0x00040000;
        public const int JOY_CAL_READ4 = 0x00080000;
        public const int JOY_CAL_READXONLY = 0x00100000;
        public const int JOY_CAL_READYONLY = 0x00200000;
        public const int JOY_CAL_READ5 = 0x00400000;
        public const int JOY_CAL_READ6 = 0x00800000;
        public const int JOY_CAL_READZONLY = 0x01000000;
        public const int JOY_CAL_READRONLY = 0x02000000;
        public const int JOY_CAL_READUONLY = 0x04000000;
        public const int JOY_CAL_READVONLY = 0x08000000;

        /* joystick ID constants */
        public const int JOYSTICKID1 = 0;
        public const int JOYSTICKID2 = 1;

        /* joystick driver capabilites */
        public const int JOYCAPS_HASZ = 0x0001;
        public const int JOYCAPS_HASR = 0x0002;
        public const int JOYCAPS_HASU = 0x0004;
        public const int JOYCAPS_HASV = 0x0008;
        public const int JOYCAPS_HASPOV = 0x0010;
        public const int JOYCAPS_POV4DIR = 0x0020;
        public const int JOYCAPS_POVCTS = 0x0040;

    }
}
