using System;
using System.Runtime.InteropServices;

namespace TOAPI.WinMM
{
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
    public struct MIXERLINECONTROLS
    {
        public int structSize;
        public uint dwLineID;
        public Anonymous_b0ebf495_df26_4585_948e_9f1956718bad Union1;
        public uint cControls;
        public uint cbmxctrl;
        public IntPtr pamxctrl;     /// LPMIXERCONTROLW->tagMIXERCONTROLW*

        public void Init()
        {
            structSize = Marshal.SizeOf(this);
        }
    }

    [StructLayoutAttribute(LayoutKind.Explicit)]
    public struct Anonymous_b0ebf495_df26_4585_948e_9f1956718bad
    {
        /// DWORD->unsigned int
        [FieldOffset(0)]
        public uint dwControlID;

        /// DWORD->unsigned int
        [FieldOffset(0)]
        public uint dwControlType;
    }


}
