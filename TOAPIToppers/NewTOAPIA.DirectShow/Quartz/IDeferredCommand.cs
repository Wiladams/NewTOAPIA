

namespace NewTOAPIA.DirectShow.Quartz
{
    using System.Runtime.InteropServices;

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("56a868b8-0ad4-11ce-b03a-0020af0ba770"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDeferredCommand
    {
        [PreserveSig]
        int Cancel();

        [PreserveSig]
        int Confidence([Out] out int pConfidence);

        [PreserveSig]
        int Postpone([In] double newtime);

        [PreserveSig]
        int GetHResult([Out] out int phrResult);
    }
}
