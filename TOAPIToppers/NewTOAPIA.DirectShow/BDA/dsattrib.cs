

using System;
using System.Runtime.InteropServices;

namespace NewTOAPIA.DirectShow.BDA
{
    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("583ec3cc-4960-4857-982b-41a33ea0a006"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAttributeSet
    {
        [PreserveSig]
        int SetAttrib(
          [In] Guid guidAttribute,
          [In] IntPtr pbAttribute,
          [In] int dwAttributeLength
          );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("52dbd1ec-e48f-4528-9232-f442a68f0ae1"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAttributeGet
    {
        [PreserveSig]
        int GetCount([Out] out int plCount);

        [PreserveSig]
        int GetAttribIndexed(
          [In] int lIndex,
          [Out] out Guid guidAttribute,
          [In, Out] IntPtr pbAttribute,
          [In, Out] ref int dwAttributeLength
          );

        [PreserveSig]
        int GetAttrib(
          [In] Guid guidAttribute,
          [In, Out] IntPtr pbAttribute,
          [In, Out] ref int dwAttributeLength
          );
    }



}
