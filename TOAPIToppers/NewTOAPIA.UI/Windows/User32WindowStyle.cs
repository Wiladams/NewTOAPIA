using System;
using System.Drawing;

using TOAPI.User32;

namespace NewTOAPIA.UI
{
    [Flags]
    public enum WindowStyle
    {
        Overlapped = User32.WS_OVERLAPPED,
        OverlappedWindow = User32.WS_OVERLAPPEDWINDOW,
        Popup = User32.WS_POPUP,
        PopupWindow = User32.WS_POPUPWINDOW,
        Child = User32.WS_CHILD,

        Visible = User32.WS_VISIBLE,
        Disabled = User32.WS_DISABLED,

        ClipSiblings = User32.WS_CLIPSIBLINGS,
        ClipChildren = User32.WS_CLIPCHILDREN,

        Border = User32.WS_BORDER,
        DialogFrame = User32.WS_DLGFRAME,
        SystemMenu = User32.WS_SYSMENU,
        ThickFrame = User32.WS_THICKFRAME,
        Group = User32.WS_GROUP,
        TabStop = User32.WS_TABSTOP,

        VerticalScroll = User32.WS_VSCROLL,
        HorizontalScroll = User32.WS_HSCROLL,
        Maximize = User32.WS_MAXIMIZE,
        Minimize = User32.WS_MINIMIZE,
        Caption = User32.WS_CAPTION,
        MinimizeBox = User32.WS_MINIMIZEBOX,
        MaximizeBox = User32.WS_MAXIMIZEBOX,
        SizeBox = User32.WS_SIZEBOX,

        Titled = User32.WS_TILED,
        Iconic = User32.WS_ICONIC,
    }

    [Flags]
    public enum ExtendedWindowStyle
    {
            DialogModalFrame = User32.WS_EX_DLGMODALFRAME,
            NoParentNotify = User32.WS_EX_NOPARENTNOTIFY,
            TopMost = User32.WS_EX_TOPMOST,
            AcceptFiles = User32.WS_EX_ACCEPTFILES,
            Transparent = User32.WS_EX_TRANSPARENT,
            MdiChild = User32.WS_EX_MDICHILD,
            ToolWindow = User32.WS_EX_TOOLWINDOW,
            WindowEdge = User32.WS_EX_WINDOWEDGE,
            ClientEdge = User32.WS_EX_CLIENTEDGE,
            ContextHelp = User32.WS_EX_CONTEXTHELP,
            Right = User32.WS_EX_RIGHT,
            Left = User32.WS_EX_LEFT,
            RtlReading = User32.WS_EX_RTLREADING,
            LtrReading = User32.WS_EX_LTRREADING,
            LeftScrollBar = User32.WS_EX_LEFTSCROLLBAR,
            RightScrollBar = User32.WS_EX_RIGHTSCROLLBAR,
            ControlParent = User32.WS_EX_CONTROLPARENT,
            StaticEdge = User32.WS_EX_STATICEDGE,
            AppWindow = User32.WS_EX_APPWINDOW,
            OverLappedWindow = User32.WS_EX_OVERLAPPEDWINDOW,
            PaletteWindow = User32.WS_EX_PALETTEWINDOW,
            Layered = User32.WS_EX_LAYERED,
            NoInheritLayout = User32.WS_EX_NOINHERITLAYOUT, // Disable inheritence of mirroring by children
            LayoutRtl = User32.WS_EX_LAYOUTRTL,             // Right to left mirroring
            Composited = User32.WS_EX_COMPOSITED,
            NoActive = User32.WS_EX_NOACTIVATE,
    }

}