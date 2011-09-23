using System;
using System.Runtime.InteropServices;

namespace TOAPI.Types
{
    // -- MMTIME --
    [StructLayout(LayoutKind.Explicit)]
    public struct MMTIME
    {
        [FieldOffset(0)]
        public UInt32 wType;
            [FieldOffset(4)]
            public UInt32 ms;
            [FieldOffset(4)]
            public UInt32 sample;
            [FieldOffset(4)]
            public UInt32 cb;
            [FieldOffset(4)]
            public UInt32 ticks;
            [FieldOffset(4)]
            public Byte smpteHour;
                [FieldOffset(5)]
                public Byte smpteMin;
                [FieldOffset(6)]
                public Byte smpteSec;
                [FieldOffset(7)]
                public Byte smpteFrame;
                [FieldOffset(8)]
                public Byte smpteFps;
                [FieldOffset(9)]
                public Byte smpteDummy;
                [FieldOffset(10)]
                public Byte smptePad0;
                [FieldOffset(11)]
                public Byte smptePad1;
        [FieldOffset(4)]
        public UInt32 midiSongPtrPos;
    }
}