using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.DirectShow
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// From CameraControlProperty
    /// </summary>
    public enum CameraControlProperty
    {
        Pan = 0,
        Tilt,
        Roll,
        Zoom,
        Exposure,
        Iris,
        Focus
    }

    /// <summary>
    /// From CameraControlFlags
    /// </summary>
    [Flags]
    public enum CameraControlFlags
    {
        None = 0x0,
        Auto = 0x0001,
        Manual = 0x0002
    }


    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("C6E13370-30AC-11d0-A18C-00A0C9118956"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMCameraControl
    {
        [PreserveSig]
        int GetRange(
            [In] CameraControlProperty Property,
            [Out] out int pMin,
            [Out] out int pMax,
            [Out] out int pSteppingDelta,
            [Out] out int pDefault,
            [Out] out CameraControlFlags pCapsFlags
            );

        [PreserveSig]
        int Set(
            [In] CameraControlProperty Property,
            [In] int lValue,
            [In] CameraControlFlags Flags
            );

        [PreserveSig]
        int Get(
            [In] CameraControlProperty Property,
            [Out] out int lValue,
            [Out] out CameraControlFlags Flags
            );
    }
}
