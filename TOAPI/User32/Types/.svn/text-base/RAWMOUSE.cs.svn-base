using System.Runtime.InteropServices;

namespace TOAPI.User32
{
    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct RAWMOUSE
    {
        public ushort usFlags;          // MOUSE_ATTRUBUTES_CHANGED, MOUSE_MOVE_RELATIVE, MOUSE_MOVE_ABSOLUTE, MOUSE_VIRTUAL_DESKTOP
        public Anonymous_ef15544e_45f4_489f_ada6_235445661354 Union1;
        public uint ulRawButtons;       // Raw state of the buttons
        public int lLastX;              // Motion in the X direction. This is signed relative motion or absolute motion, depending on the value of usFlags.
        public int lLastY;              // Motion in the Y direction. This is signed relative motion or absolute motion, depending on the value of usFlags
        public uint ulExtraInformation; // Device-specific additional information for the event.
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct Anonymous_ef15544e_45f4_489f_ada6_235445661354
    {
        [FieldOffset(0)]
        public uint ulButtons;  // Reserved
        [FieldOffset(0)]
        public Anonymous_c00c5d17_7ed5_4d0a_a9ad_b8430c018c9b Struct1;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Anonymous_c00c5d17_7ed5_4d0a_a9ad_b8430c018c9b
    {
        public ushort usButtonFlags;    // RI_MOUSE_XXX_BUTTON_DOWN/UP, RI_MOUSE_WHEEL
        public ushort usButtonData;     // The wheel delta is stored in usButtonData
    }
}