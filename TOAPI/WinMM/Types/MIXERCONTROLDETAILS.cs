using System;
using System.Runtime.InteropServices;

namespace TOAPI.WinMM
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MIXERCONTROLDETAILS
    {
        public uint cbStruct;       /// DWORD->unsigned int
        public uint dwControlID;    /// DWORD->unsigned int
        public uint cChannels;      /// DWORD->unsigned int
        /// Anonymous_39007b03_fc92_4c35_95a7_e876aec21a67
        public Anonymous_39007b03_fc92_4c35_95a7_e876aec21a67 Union1;
        public uint cbDetails;      /// DWORD->unsigned int
        public System.IntPtr paDetails; /// LPVOID->void*
    }
    
    [StructLayoutAttribute(LayoutKind.Explicit)]
    public struct Anonymous_39007b03_fc92_4c35_95a7_e876aec21a67
    {
        /// HWND->HWND__*
        [FieldOffsetAttribute(0)]
        public IntPtr hwndOwner;

        /// DWORD->unsigned int
        [FieldOffsetAttribute(0)]
        public uint cMultipleItems;
    }
}
