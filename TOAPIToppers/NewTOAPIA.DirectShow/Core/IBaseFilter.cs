namespace NewTOAPIA.DirectShow.Core
{
	using System;
	using System.Runtime.InteropServices;

	// IBaseFilter interface
	//
	// The IBaseFilter interface provides methods for controlling a filter
	//
    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("56a86895-0ad4-11ce-b03a-0020af0ba770"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IBaseFilter : IMediaFilter
    {
        #region IPersist Methods

        [PreserveSig]
        new int GetClassID([Out] out Guid pClassID);

        #endregion

        #region IMediaFilter Methods

        [PreserveSig]
        new int Stop();

        [PreserveSig]
        new int Pause();

        [PreserveSig]
        new int Run(long tStart);

        [PreserveSig]
        new int GetState([In] int dwMilliSecsTimeout, [Out] out FilterState filtState);

        [PreserveSig]
        new int SetSyncSource([In] IReferenceClock pClock);

        [PreserveSig]
        new int GetSyncSource([Out] out IReferenceClock pClock);

        #endregion

        [PreserveSig]
        int EnumPins([Out] out IEnumPins ppEnum);

        [PreserveSig]
        int FindPin([In, MarshalAs(UnmanagedType.LPWStr)] string Id, [Out] out IPin ppPin);

        [PreserveSig]
        int QueryFilterInfo([Out] out FilterInfo pInfo);

        [PreserveSig]
        int JoinFilterGraph([In] IFilterGraph pGraph, [In, MarshalAs(UnmanagedType.LPWStr)] string pName);

        [PreserveSig]
        int QueryVendorInfo([Out, MarshalAs(UnmanagedType.LPWStr)] out string pVendorInfo);
    }
}
