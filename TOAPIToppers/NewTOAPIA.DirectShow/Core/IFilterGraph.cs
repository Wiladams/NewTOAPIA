namespace NewTOAPIA.DirectShow.Core
{
	using System;
	using System.Runtime.InteropServices;
    using System.Runtime.InteropServices.ComTypes;
    using NewTOAPIA.DirectShow.DES;

	// IFilterGraph interface
	//
	// The IFilterGraph interface is an abstraction representing
	// a graph of filters
	//
    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("56a8689f-0ad4-11ce-b03a-0020af0ba770"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IFilterGraph
    {
        [PreserveSig]
        int AddFilter(
            [In] IBaseFilter pFilter,
            [In, MarshalAs(UnmanagedType.LPWStr)] string pName
            );

        [PreserveSig]
        int RemoveFilter([In] IBaseFilter pFilter);

        [PreserveSig]
        int EnumFilters([Out] out IEnumFilters ppEnum);

        [PreserveSig]
        int FindFilterByName(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pName,
            [Out] out IBaseFilter ppFilter
            );

        [PreserveSig]
        int ConnectDirect(
            [In] IPin ppinOut,
            [In] IPin ppinIn,
            [In, MarshalAs(UnmanagedType.LPStruct)] AMMediaType pmt
            );

        [PreserveSig]
        [Obsolete("This method is obsolete; use the IFilterGraph2.ReconnectEx method instead.")]
        int Reconnect([In] IPin ppin);

        [PreserveSig]
        int Disconnect([In] IPin ppin);

        [PreserveSig]
        int SetDefaultSyncSource();
    }

    //[ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    //Guid("36b73882-c2c8-11cf-8b46-00805f6cef60"),
    //InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    //public interface IFilterGraph2 : IGraphBuilder
    //{
    //    #region IFilterGraph Methods

    //    [PreserveSig]
    //    new int AddFilter(
    //        [In] IBaseFilter pFilter,
    //        [In, MarshalAs(UnmanagedType.LPWStr)] string pName
    //        );

    //    [PreserveSig]
    //    new int RemoveFilter([In] IBaseFilter pFilter);

    //    [PreserveSig]
    //    new int EnumFilters([Out] out IEnumFilters ppEnum);

    //    [PreserveSig]
    //    new int FindFilterByName(
    //        [In, MarshalAs(UnmanagedType.LPWStr)] string pName,
    //        [Out] out IBaseFilter ppFilter
    //        );

    //    [PreserveSig]
    //    new int ConnectDirect(
    //        [In] IPin ppinOut,
    //        [In] IPin ppinIn,
    //        [In, MarshalAs(UnmanagedType.LPStruct)]
    //        AMMediaType pmt
    //        );

    //    [PreserveSig]
    //    new int Reconnect([In] IPin ppin);

    //    [PreserveSig]
    //    new int Disconnect([In] IPin ppin);

    //    [PreserveSig]
    //    new int SetDefaultSyncSource();

    //    #endregion

    //    #region IGraphBuilder Method

    //    [PreserveSig]
    //    new int Connect(
    //        [In] IPin ppinOut,
    //        [In] IPin ppinIn
    //        );

    //    [PreserveSig]
    //    new int Render([In] IPin ppinOut);

    //    [PreserveSig]
    //    new int RenderFile(
    //        [In, MarshalAs(UnmanagedType.LPWStr)] string lpcwstrFile,
    //        [In, MarshalAs(UnmanagedType.LPWStr)] string lpcwstrPlayList
    //        );

    //    [PreserveSig]
    //    new int AddSourceFilter(
    //        [In, MarshalAs(UnmanagedType.LPWStr)] string lpcwstrFileName,
    //        [In, MarshalAs(UnmanagedType.LPWStr)] string lpcwstrFilterName,
    //        [Out] out IBaseFilter ppFilter
    //        );

    //    [PreserveSig]
    //    new int SetLogFile(IntPtr hFile); // DWORD_PTR

    //    [PreserveSig]
    //    new int Abort();

    //    [PreserveSig]
    //    new int ShouldOperationContinue();

    //    #endregion

    //    [PreserveSig]
    //    int AddSourceFilterForMoniker(
    //        [In] IMoniker pMoniker,
    //        [In] IBindCtx pCtx,
    //        [In, MarshalAs(UnmanagedType.LPWStr)] string lpcwstrFilterName,
    //        [Out] out IBaseFilter ppFilter
    //        );

    //    [PreserveSig]
    //    int ReconnectEx(
    //        [In] IPin ppin,
    //        [In] AMMediaType pmt
    //        );

    //    [PreserveSig]
    //    int RenderEx(
    //        [In] IPin pPinOut,
    //        [In] AMRenderExFlags dwFlags,
    //        [In] IntPtr pvContext // DWORD *
    //        );
    //}

}
