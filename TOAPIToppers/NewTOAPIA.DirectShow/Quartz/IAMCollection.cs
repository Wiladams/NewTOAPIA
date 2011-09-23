using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace NewTOAPIA.DirectShow.Quartz
{
    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("56a868b9-0ad4-11ce-b03a-0020af0ba770"),
    InterfaceType(ComInterfaceType.InterfaceIsDual)]
        public interface IAMCollection
    {
        [PreserveSig]
        int get_Count([Out] out int plCount);

        [PreserveSig]
        int Item(
            [In] int lItem,
            [Out, MarshalAs(UnmanagedType.IUnknown)] out object ppUnk
            );

        [PreserveSig]
        int get__NewEnum([Out, MarshalAs(UnmanagedType.IUnknown)] out object ppUnk);
    }
}
