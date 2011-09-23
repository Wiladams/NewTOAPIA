using System;


namespace NewTOAPIA.UI
{
    using System.Drawing;

    using NewTOAPIA.Drawing;
    using NewTOAPIA.Drawing.GDI;

    public interface IPlatformWindowBase : IWindowBase
    {
        // Some Properties
        void SetWindowRegion(GDIRegion aRegion);
    }

    public interface IPlatformWindow : IPlatformWindowBase
    {
        void CaptureMouse();
        void ReleaseMouse();

        // Some drawing related stuff
        GDIContext DeviceContextClientArea { get; }
        GDIContext DeviceContextWholeWindow { get; }

    }

}