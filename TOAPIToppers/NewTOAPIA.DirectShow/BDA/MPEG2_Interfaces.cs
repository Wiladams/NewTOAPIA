﻿

using System;
using System.Runtime.InteropServices;

namespace NewTOAPIA.DirectShow.BDA
{

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("D19BDB43-405B-4a7c-A791-C89110C33165"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ITSDT
    {
        [PreserveSig]
        int Initialize(
          [In] ISectionList pSectionList,
          [In] IMpeg2Data pMPEGData
          );

        [PreserveSig]
        int GetVersionNumber([Out] out byte pbVal);

        [PreserveSig]
        int GetCountOfTableDescriptors([Out] out int pdwVal);

        [PreserveSig]
        int GetTableDescriptorByIndex(
          [In] int dwIndex,
          [Out] out IGenericDescriptor ppDescriptor
          );

        [PreserveSig]
        int GetTableDescriptorByTag(
          [In] byte bTag,
          [In, Out] DsInt pdwCookie,
          [Out] out IGenericDescriptor ppDescriptor
          );

        [PreserveSig]
        int RegisterForNextTable([In] IntPtr hNextTableAvailable);

        [PreserveSig]
        int GetNextTable([Out] out ITSDT ppTSDT);

        [PreserveSig]
        int RegisterForWhenCurrent([In] IntPtr hNextTableIsCurrent);

        [PreserveSig]
        int ConvertNextToCurrent();
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("919F24C5-7B14-42ac-A4B0-2AE08DAF00AC"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPSITables
    {
        [PreserveSig]
        int GetTable(
          [In] int dwTSID,
          [In] int dwTID_PID,
          [In] int dwHashedVer,
          [In] int dwPara4,
          [Out] out object ppIUnknown
          );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("6A5918F8-A77A-4f61-AED0-5702BDCDA3E6"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IGenericDescriptor
    {
        [PreserveSig]
        int Initialize(
          [In] IntPtr pbDesc,
          [In] int bCount
          );

        [PreserveSig]
        int GetTag([Out] out byte pbVal);

        [PreserveSig]
        int GetLength([Out] out byte pbVal);

        [PreserveSig]
        int GetBody([Out] out IntPtr ppbVal);
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("7C6995FB-2A31-4bd7-953E-B1AD7FB7D31C"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ICAT
    {
        [PreserveSig]
        int Initialize(
          [In] ISectionList pSectionList,
          [In] IMpeg2Data pMPEGData
          );

        [PreserveSig]
        int GetVersionNumber([Out] out byte pbVal);

        [PreserveSig]
        int GetCountOfTableDescriptors([Out] out int pdwVal);

        [PreserveSig]
        int GetTableDescriptorByIndex(
          [In] int dwIndex,
          [Out] out IGenericDescriptor ppDescriptor
          );

        [PreserveSig]
        int GetTableDescriptorByTag(
          [In] byte bTag,
          [In, Out] DsInt pdwCookie,
          [Out] out IGenericDescriptor ppDescriptor
          );

        [PreserveSig]
        int RegisterForNextTable([In] IntPtr hNextTableAvailable);

        [PreserveSig]
        int GetNextTable(
          [In] int dwTimeout,
          [Out] out ICAT ppCAT);

        [PreserveSig]
        int RegisterForWhenCurrent([In] IntPtr hNextTableIsCurrent);

        [PreserveSig]
        int ConvertNextToCurrent();
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("01F3B398-9527-4736-94DB-5195878E97A8"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPMT
    {
        [PreserveSig]
        int Initialize(
          [In] ISectionList pSectionList,
          [In] IMpeg2Data pMPEGData
          );

        [PreserveSig]
        int GetProgramNumber([Out] out short pwVal);

        [PreserveSig]
        int GetVersionNumber([Out] out byte pbVal);

        [PreserveSig]
        int GetPcrPid([Out] out short pPidVal);

        [PreserveSig]
        int GetCountOfTableDescriptors([Out] out int pdwVal);

        [PreserveSig]
        int GetTableDescriptorByIndex(
          [In] int dwIndex,
          [Out] out IGenericDescriptor ppDescriptor
          );

        [PreserveSig]
        int GetTableDescriptorByTag(
          [In] Byte bTag,
          [In, Out] DsInt pdwCookie,
          [Out] out IGenericDescriptor ppDescriptor
          );

        [PreserveSig]
        int GetCountOfRecords([Out] out short pwVal);

        [PreserveSig]
        int GetRecordStreamType(
          [In] int dwRecordIndex,
          [Out] out byte pbVal
          );

        [PreserveSig]
        int GetRecordElementaryPid(
          [In] int dwRecordIndex,
          [Out] out short pPidVal
          );

        [PreserveSig]
        int GetRecordCountOfDescriptors(
          [In] int dwRecordIndex,
          [Out] out int pdwVal
          );

        [PreserveSig]
        int GetRecordDescriptorByIndex(
          [In] int dwRecordIndex,
          [In] int dwDescIndex,
          [Out] out IGenericDescriptor ppDescriptor
          );

        [PreserveSig]
        int GetRecordDescriptorByTag(
          [In] int dwRecordIndex,
          [In] Byte bTag,
          [In, Out] DsInt pdwCookie,
          [Out] out IGenericDescriptor ppDescriptor
          );

        [PreserveSig]
        int QueryServiceGatewayInfo(
          [Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Struct)] out DsmccElement[] ppDSMCCList,
          [Out] out int puiCount
          );

        [PreserveSig]
        int QueryMPEInfo(
          [Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Struct)] out MpeElement[] ppMPEList,
          [Out] out int puiCount
          );

        [PreserveSig]
        int RegisterForNextTable([In] IntPtr hNextTableAvailable);

        [PreserveSig]
        int GetNextTable([Out] out IPMT ppPMT);

        [PreserveSig]
        int RegisterForWhenCurrent([In] IntPtr hNextTableIsCurrent);

        [PreserveSig]
        int ConvertNextToCurrent();
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("6623B511-4B5F-43c3-9A01-E8FF84188060"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPAT
    {
        [PreserveSig]
        int Initialize(
          [In] ISectionList pSectionList,
          [In] IMpeg2Data pMPEGData
          );

        [PreserveSig]
        int GetTransportStreamId([Out] out short pwVal);

        [PreserveSig]
        int GetVersionNumber([Out] out byte pbVal);

        [PreserveSig]
        int GetCountOfRecords([Out] out int pwVal);

        [PreserveSig]
        int GetRecordProgramNumber(
          [In] int dwIndex,
          [Out] out short pwVal
          );

        [PreserveSig]
        int GetRecordProgramMapPid(
          [In] int dwIndex,
          [Out] out short pwVal
          );

        [PreserveSig]
        int FindRecordProgramMapPid(
          [In] short wProgramNumber,
          [Out] out short pwVal
          );

        [PreserveSig]
        int RegisterForNextTable([In] IntPtr hNextTableAvailable);

        [PreserveSig]
        int GetNextTable([Out] out IPAT ppPAT);

        [PreserveSig]
        int RegisterForWhenCurrent([In] IntPtr hNextTableIsCurrent);

        [PreserveSig]
        int ConvertNextToCurrent();
    }

}
