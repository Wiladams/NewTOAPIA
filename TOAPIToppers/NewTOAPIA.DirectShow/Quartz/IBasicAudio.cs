using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NewTOAPIA.DirectShow.Quartz
{
    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("56a868b3-0ad4-11ce-b03a-0020af0ba770"),
    InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IBasicAudio
    {
        [PreserveSig]
        int set_Volume([In] int lVolume);

        [PreserveSig]
        int get_Volume([Out] out int plVolume);

        [PreserveSig]
        int set_Balance([In] int lBalance);

        [PreserveSig]
        int get_Balance([Out] out int plBalance);
    }
}
