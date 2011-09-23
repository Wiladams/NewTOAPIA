namespace NewTOAPIA.DirectShow.Core
{
    using System;
    using System.Runtime.InteropServices;

    using NewTOAPIA.DirectShow.DES;

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("56a86893-0ad4-11ce-b03a-0020af0ba770"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IEnumFilters
    {
        [PreserveSig]
        int Next(
            [In] int cFilters,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] IBaseFilter[] ppFilter,
            [In] IntPtr pcFetched
            );

        [PreserveSig]
        int Skip([In] int cFilters);

        [PreserveSig]
        int Reset();

        [PreserveSig]
        int Clone([Out] out IEnumFilters ppEnum);
    }
}