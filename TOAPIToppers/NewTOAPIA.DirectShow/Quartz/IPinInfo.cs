

namespace NewTOAPIA.DirectShow.Quartz
{
    using System.Runtime.InteropServices;

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("56a868bd-0ad4-11ce-b03a-0020af0ba770"),
    InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IPinInfo
    {
        [PreserveSig]
        int get_Pin([Out, MarshalAs(UnmanagedType.IUnknown)] out object ppUnk);

        [PreserveSig]
        int get_ConnectedTo([Out, MarshalAs(UnmanagedType.IDispatch)] out object ppUnk);

        [PreserveSig]
        int get_ConnectionMediaType([Out, MarshalAs(UnmanagedType.IUnknown)] out object ppUnk);

        [PreserveSig]
        int get_FilterInfo([Out, MarshalAs(UnmanagedType.IUnknown)] out object ppUnk);

        [PreserveSig]
        int get_Name([Out, MarshalAs(UnmanagedType.BStr)] out string ppUnk);

        [PreserveSig]
        int get_Direction([Out] int ppDirection);

        [PreserveSig]
        int get_PinID([Out, MarshalAs(UnmanagedType.BStr)] out string strPinID);

        [PreserveSig]
        int get_MediaTypes([Out, MarshalAs(UnmanagedType.IUnknown)] out object ppUnk);

        [PreserveSig]
        int Connect([In, MarshalAs(UnmanagedType.IUnknown)] object pPin);

        [PreserveSig]
        int ConnectDirect([In, MarshalAs(UnmanagedType.IUnknown)] object pPin);

        [PreserveSig]
        int ConnectWithType(
            [In, MarshalAs(UnmanagedType.IUnknown)] object pPin,
            [In, MarshalAs(UnmanagedType.IUnknown)] object pMediaType
            );

        [PreserveSig]
        int Disconnect();

        [PreserveSig]
        int Render();
    }
}
