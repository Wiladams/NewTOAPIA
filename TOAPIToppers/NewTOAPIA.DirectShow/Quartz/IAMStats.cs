using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NewTOAPIA.DirectShow.Quartz
{
    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("bc9bcf80-dcd2-11d2-abf6-00a0c905f375"),
    InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IAMStats
    {
        [PreserveSig]
        int Reset();

        [PreserveSig]
        int get_Count([Out] out int plCount);

        [PreserveSig]
        int GetValueByIndex(
            [In] int lIndex,
            [Out, MarshalAs(UnmanagedType.BStr)] out string szName,
            [Out] out int lCount,
            [Out] out double dLast,
            [Out] out double dAverage,
            [Out] out double dStdDev,
            [Out] out double dMin,
            [Out] out double dMax
            );

        [PreserveSig]
        int GetValueByName(
            [In, MarshalAs(UnmanagedType.BStr)] string szName,
            [Out] out int lIndex,
            [Out] out int lCount,
            [Out] out double dLast,
            [Out] out double dAverage,
            [Out] out double dStdDev,
            [Out] out double dMin,
            [Out] out double dMax
            );

        [PreserveSig]
        int GetIndex(
            [In, MarshalAs(UnmanagedType.BStr)] string szName,
            [In, MarshalAs(UnmanagedType.Bool)] bool lCreate,
            [Out] out int plIndex
            );

        [PreserveSig]
        int AddValue(
            [In] int lIndex,
            [In] double dValue
            );
    }
}
