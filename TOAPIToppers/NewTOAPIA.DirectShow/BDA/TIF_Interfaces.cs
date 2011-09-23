


namespace NewTOAPIA.DirectShow.BDA
{
    using System;
    using System.Runtime.InteropServices;
    using System.Runtime.InteropServices.ComTypes;

    using NewTOAPIA.DirectShow.DES;

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("DFEF4A68-EE61-415f-9CCB-CD95F2F98A3A"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IBDA_TIF_REGISTRATION
    {
        [PreserveSig]
        int RegisterTIFEx(
          [In] IPin pTIFInputPin,
          [Out] out int ppvRegistrationContext,
          [Out, MarshalAs(UnmanagedType.Interface)] out object ppMpeg2DataControl
          );

        [PreserveSig]
        int UnregisterTIF([In] int pvRegistrationContext);
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("F9BAC2F9-4149-4916-B2EF-FAA202326862"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMPEG2_TIF_CONTROL
    {
        [PreserveSig]
        int RegisterTIF(
            [In, MarshalAs(UnmanagedType.Interface)] object pUnkTIF,
            [In, Out] ref int ppvRegistrationContext
            );

        [PreserveSig]
        int UnregisterTIF([In] int pvRegistrationContext);

        [PreserveSig]
        int AddPIDs(
            [In] int ulcPIDs,
            [In] ref int pulPIDs
            );

        [PreserveSig]
        int DeletePIDs(
            [In] int ulcPIDs,
            [In] ref int pulPIDs
            );

        [PreserveSig]
        int GetPIDCount([Out] out int pulcPIDs);

        [PreserveSig]
        int GetPIDs(
            [Out] out int pulcPIDs,
            [Out] out int pulPIDs
            );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("A3B152DF-7A90-4218-AC54-9830BEE8C0B6"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ITuneRequestInfo
    {
        [PreserveSig]
        int GetLocatorData([In] ITuneRequest Request);

        [PreserveSig]
        int GetComponentData([In] ITuneRequest CurrentRequest);

        [PreserveSig]
        int CreateComponentList([In] ITuneRequest CurrentRequest);

        [PreserveSig]
        int GetNextProgram(
            [In] ITuneRequest CurrentRequest,
            [Out] out ITuneRequest TuneRequest
            );

        [PreserveSig]
        int GetPreviousProgram(
            [In] ITuneRequest CurrentRequest,
            [Out] out ITuneRequest TuneRequest
            );

        [PreserveSig]
        int GetNextLocator(
            [In] ITuneRequest CurrentRequest,
            [Out] out ITuneRequest TuneRequest
            );

        [PreserveSig]
        int GetPreviousLocator(
            [In] ITuneRequest CurrentRequest,
            [Out] out ITuneRequest TuneRequest
            );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("EFDA0C80-F395-42c3-9B3C-56B37DEC7BB7"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IGuideDataEvent
    {
        [PreserveSig]
        int GuideDataAcquired();

        [PreserveSig]
        int ProgramChanged([In] object varProgramDescriptionID);

        [PreserveSig]
        int ServiceChanged([In] object varProgramDescriptionID);

        [PreserveSig]
        int ScheduleEntryChanged([In] object varProgramDescriptionID);

        [PreserveSig]
        int ProgramDeleted([In] object varProgramDescriptionID);

        [PreserveSig]
        int ServiceDeleted([In] object varProgramDescriptionID);

        [PreserveSig]
        int ScheduleDeleted([In] object varProgramDescriptionID);
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("88EC5E58-BB73-41d6-99CE-66C524B8B591"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IGuideDataProperty
    {
        [PreserveSig]
        int get_Name([Out, MarshalAs(UnmanagedType.BStr)] out string pbstrName);

        [PreserveSig]
        int get_Language([Out] out int idLang);

        [PreserveSig]
        int get_Value([Out] out object pvar);
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("AE44423B-4571-475c-AD2C-F40A771D80EF"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IEnumGuideDataProperties
    {
        [PreserveSig]
        int Next(
            [In] int celt,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex=0)] IGuideDataProperty [] ppprop,
            [In] IntPtr pcelt
            );

        [PreserveSig]
        int Skip([In] int celt);

        [PreserveSig]
        int Reset();

        [PreserveSig]
        int Clone([Out] out IEnumGuideDataProperties ppenum);
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("1993299C-CED6-4788-87A3-420067DCE0C7"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IEnumTuneRequests
    {
        [PreserveSig]
        int Next(
            [In] int celt,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex=0)] ITuneRequest[] ppprop,
            [In] IntPtr pcelt
            );

        [PreserveSig]
        int Skip([In] int celt);

        [PreserveSig]
        int Reset();

        [PreserveSig]
        int Clone([Out] out IEnumTuneRequests ppenum);
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("61571138-5B01-43cd-AEAF-60B784A0BF93"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IGuideData
    {
        [PreserveSig]
        int GetServices([Out] out IEnumTuneRequests ppEnumTuneRequests);

        [PreserveSig]
        int GetServiceProperties(
            [In] ITuneRequest pTuneRequest,
            [Out] out IEnumGuideDataProperties ppEnumProperties
            );

        [PreserveSig]
        int GetGuideProgramIDs([Out] out IEnumVARIANT pEnumPrograms);

        [PreserveSig]
        int GetProgramProperties(
            [In] object varProgramDescriptionID,
            [Out] out IEnumGuideDataProperties ppEnumProperties
            );

        [PreserveSig]
        int GetScheduleEntryIDs([Out] out IEnumVARIANT pEnumScheduleEntries);

        [PreserveSig]
        int GetScheduleEntryProperties(
            [In] object varScheduleEntryDescriptionID,
            [Out] out IEnumGuideDataProperties ppEnumProperties
            );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("4764ff7c-fa95-4525-af4d-d32236db9e38"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IGuideDataLoader
    {
        [PreserveSig]
        int Init([In] IGuideData pGuideStore);

        [PreserveSig]
        int Terminate();
    }


}
