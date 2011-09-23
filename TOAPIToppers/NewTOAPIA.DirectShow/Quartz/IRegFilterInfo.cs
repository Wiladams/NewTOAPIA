
namespace NewTOAPIA.DirectShow.Quartz
{
    using System.Runtime.InteropServices;

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("56a868bb-0ad4-11ce-b03a-0020af0ba770"),
    InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IRegFilterInfo
    {
        [PreserveSig]
        int get_Name([Out, MarshalAs(UnmanagedType.BStr)] out string strName);

        [PreserveSig]
        int Filter([Out, MarshalAs(UnmanagedType.IDispatch)] out object ppUnk);
    }

}
