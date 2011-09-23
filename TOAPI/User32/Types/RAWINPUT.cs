using System;
using System.Runtime.InteropServices;

namespace TOAPI.User32
{
    [StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct RAWINPUT
    {
        public RAWINPUTHEADER header;

        /// Anonymous_a2adf1ff_d43f_42d1_a9b6_e49430df0265
        public Anonymous_a2adf1ff_d43f_42d1_a9b6_e49430df0265 data;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Explicit)]
    public struct Anonymous_a2adf1ff_d43f_42d1_a9b6_e49430df0265
    {
        [System.Runtime.InteropServices.FieldOffsetAttribute(0)]
        public RAWMOUSE mouse;

        [System.Runtime.InteropServices.FieldOffsetAttribute(0)]
        public RAWKEYBOARD keyboard;

        //[System.Runtime.InteropServices.FieldOffsetAttribute(0)]
        //public RAWHID hid;
    }







}
