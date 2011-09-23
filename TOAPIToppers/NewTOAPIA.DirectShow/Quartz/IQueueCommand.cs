
namespace NewTOAPIA.DirectShow.Quartz
{
    using System;
    using System.Runtime.InteropServices;
    using NewTOAPIA.DirectShow.Core;

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("56a868b7-0ad4-11ce-b03a-0020af0ba770"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IQueueCommand
    {
        [PreserveSig]
        int InvokeAtStreamTime(
            [Out] out IDeferredCommand pCmd,
            [In] double time,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid iid,
            [In] int dispidMethod,
            [In] DispatchFlags wFlags,
            [In] int cArgs,
            [In] object[] pDispParams,
            [In] IntPtr pvarResult,
            [Out] out short puArgErr
            );

        int InvokeAtPresentationTime(
            [Out] out IDeferredCommand pCmd,
            [In] double time,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid iid,
            [In] int dispidMethod,
            [In] DispatchFlags wFlags,
            [In] int cArgs,
            [In] object[] pDispParams,
            [In] IntPtr pvarResult,
            [Out] out short puArgErr
            );
    }

}
