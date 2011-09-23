namespace NewTOAPIA.DirectShow.Quartz
{
    using System;
    using System.Runtime.InteropServices;

    using NewTOAPIA.DirectShow.Core;

    // IMediaEventEx interface
    //
    // IMediaEventEx adds methods that enable an application window
    // to receive messages when events occur
    //
    [ComImport,
    Guid("56A868C0-0AD4-11CE-B03A-0020AF0BA770"),
    InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IMediaEventEx : IMediaEvent
    {
        // Retrieves a handle to a manual-reset event that remains
        // signaled while the queue contains event notifications
        [PreserveSig]
        new int GetEventHandle(out IntPtr hEvent);

        // Retrieves the next event notification from the event queue
        [PreserveSig]
        new int GetEvent(
            out EventCode lEventCode,
            out IntPtr lParam1,
            out IntPtr lParam2,
            int msTimeout);

        // Waits for the filter graph to render all available data
        [PreserveSig]
        int WaitForCompletion(
            int msTimeout,
            out int pEvCode);

        // Cancels the filter graph manager's default handling of
        // a specified event
        [PreserveSig]
        new int CancelDefaultHandling(
            int lEvCode);

        // Restores the filter graph manager's default handling of
        // a specified event
        [PreserveSig]
        new int RestoreDefaultHandling(
            int lEvCode);

        // Frees resources associated with the parameters of an event
        [PreserveSig]
        new int FreeEventParams(
            EventCode lEventCode,
            IntPtr lParam1,
            IntPtr lParam2);

        // Registers a window to process event notifications
        [PreserveSig]
        int SetNotifyWindow(
            IntPtr hwnd,
            int lMsg,
            IntPtr lInstanceData);

        // Enables or disables event notifications
        // 0 - ON, 1 - OFF
        [PreserveSig]
        int SetNotifyWindow(
            int lNoNotifyFlags);

        // Determines whether event notifications are enabled
        [PreserveSig]
        int GetNotifyFlags(
            out int lNoNotifyFlags);
    }
}