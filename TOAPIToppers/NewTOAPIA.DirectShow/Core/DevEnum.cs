namespace NewTOAPIA.DirectShow.Core
{
    using System;
    using System.Runtime.InteropServices;
    using System.Runtime.InteropServices.ComTypes;

    using NewTOAPIA.DirectShow.Core;

    #region Declarations

    /// <summary>
    /// From CDEF_CLASS_* defines
    /// </summary>
    [Flags]
    public enum CDef
    {
        None = 0,
        ClassDefault = 0x0001,
        BypassClassManager = 0x0002,
        ClassLegacy = 0x0004,
        MeritAboveDoNotUse = 0x0008,
        DevmonCMGRDevice = 0x0010,
        DevmonDMO = 0x0020,
        DevmonPNPDevice = 0x0040,
        DevmonFilter = 0x0080,
        DevmonSelectiveMask = 0x00f0
    }

    #endregion

    #region Interfaces

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("29840822-5B84-11D0-BD3B-00A0C911CE86"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ICreateDevEnum
    {
        [PreserveSig]
        int CreateClassEnumerator(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid pType,
            [Out] out IEnumMoniker ppEnumMoniker,
            [In] CDef dwFlags);
    }

    /// <summary>
    /// CLSID_SystemDeviceEnum
    /// </summary>
    [ComImport, Guid("62BE5D10-60EB-11d0-BD3B-00A0C911CE86")]
    public class CreateDevEnum
    {
    }

    #endregion
}