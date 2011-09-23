
namespace NewTOAPIA.DirectShow.Quartz
{
    using System.Runtime.InteropServices;

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("56a868bc-0ad4-11ce-b03a-0020af0ba770"),
    InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IMediaTypeInfo
    {
        [PreserveSig]
        int get_Type([Out, MarshalAs(UnmanagedType.BStr)] out string strType);

        [PreserveSig]
        int get_Subtype([Out, MarshalAs(UnmanagedType.BStr)] out string strType);
    }
}
