namespace NewTOAPIA.DirectShow.Quartz
{
    using System;
    using System.Runtime.InteropServices;

    using NewTOAPIA.DirectShow.Core;

    // IMediaEvent interface
    //
    // The IMediaEvent interface contains methods for retrieving event
    // notifications and for overriding the filter graph's default
    // handling of events.
    //
    [ComImport,
    Guid("56A868B6-0AD4-11CE-B03A-0020AF0BA770"),
    InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IMediaEvent
    {
        // Retrieves a handle to a manual-reset event that remains
        // signaled while the queue contains event notifications
        [PreserveSig]
        int GetEventHandle(out IntPtr hEvent);

        // Retrieves the next event notification from the event queue
        [PreserveSig]
        int GetEvent(
            out EventCode lEventCode,
            out IntPtr lParam1,
            out IntPtr lParam2,
            int msTimeout);

        // Waits for the filter graph to render all available data
        [PreserveSig]
        int GetEvent(
            int msTimeout,
            out int pEvCode);

        // Cancels the filter graph manager's default handling of
        // a specified event
        [PreserveSig]
        int CancelDefaultHandling(
            int lEvCode);

        // Restores the filter graph manager's default handling of
        // a specified event
        [PreserveSig]
        int RestoreDefaultHandling(
            int lEvCode);

        // Frees resources associated with the parameters of an event
        [PreserveSig]
        int FreeEventParams(
            EventCode lEventCode,
            IntPtr lParam1,
            IntPtr lParam2);
    }
}