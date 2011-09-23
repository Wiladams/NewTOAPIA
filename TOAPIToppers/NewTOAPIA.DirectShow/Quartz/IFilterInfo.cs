

namespace NewTOAPIA.DirectShow.Quartz
{
    using System.Runtime.InteropServices;

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("56a868ba-0ad4-11ce-b03a-0020af0ba770"),
    InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IFilterInfo
    {
        [PreserveSig]
        int FindPin(
            [In, MarshalAs(UnmanagedType.BStr)] string strPinID,
            [Out, MarshalAs(UnmanagedType.IDispatch)] out object ppUnk
            );

        [PreserveSig]
        int get_Name([Out, MarshalAs(UnmanagedType.BStr)] out string strName);

        [PreserveSig]
        int get_VendorInfo([Out, MarshalAs(UnmanagedType.BStr)] string strVendorInfo);

        [PreserveSig]
        int get_Filter([Out, MarshalAs(UnmanagedType.IUnknown)] out object ppUnk);

        [PreserveSig]
        int get_Pins([Out, MarshalAs(UnmanagedType.IDispatch)] out object ppUnk);

        [PreserveSig]
        int get_IsFileSource([Out] out int pbIsSource);

        [PreserveSig]
        int get_Filename([Out, MarshalAs(UnmanagedType.BStr)] out string pstrFilename);

        [PreserveSig]
        int set_Filename([In, MarshalAs(UnmanagedType.BStr)] string strFilename);
    }
}
