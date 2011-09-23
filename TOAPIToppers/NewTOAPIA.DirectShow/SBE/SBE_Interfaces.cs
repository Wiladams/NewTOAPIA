

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace NewTOAPIA.DirectShow.SBE
{

    using NewTOAPIA.DirectShow.Core;

    #region Interfaces

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("7E2D2A1E-7192-4bd7-80C1-061FD1D10402"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IStreamBufferConfigure3 : IStreamBufferConfigure2
    {
        #region IStreamBufferConfigure

        [PreserveSig]
        new int SetDirectory([In, MarshalAs(UnmanagedType.LPWStr)] string pszDirectoryName);

        [PreserveSig]
        new int GetDirectory([Out, MarshalAs(UnmanagedType.LPWStr)] out string pszDirectoryName);

        [PreserveSig]
        new int SetBackingFileCount(
            [In] int dwMin,
            [In] int dwMax
            );

        [PreserveSig]
        new int GetBackingFileCount(
            [Out] out int dwMin,
            [Out] out int dwMax
            );

        [PreserveSig]
        new int SetBackingFileDuration([In] int dwSeconds);

        [PreserveSig]
        new int GetBackingFileDuration([Out] out int pdwSeconds);

        #endregion

        #region IStreamBufferConfigure2

        [PreserveSig]
        new int SetMultiplexedPacketSize([In] int cbBytesPerPacket);

        [PreserveSig]
        new int GetMultiplexedPacketSize([Out] out int pcbBytesPerPacket);

        [PreserveSig]
        new int SetFFTransitionRates(
            [In] int dwMaxFullFrameRate,
            [In] int dwMaxNonSkippingRate
            );

        [PreserveSig]
        new int GetFFTransitionRates(
            [Out] out int pdwMaxFullFrameRate,
            [Out] out int pdwMaxNonSkippingRate
            );

        #endregion

        [PreserveSig]
        int SetStartRecConfig([In, MarshalAs(UnmanagedType.Bool)] bool fStartStopsCur);

        [PreserveSig]
        int GetStartRecConfig([Out, MarshalAs(UnmanagedType.Bool)] out bool pfStartStopsCur);

        [PreserveSig]
        int SetNamespace([In, MarshalAs(UnmanagedType.LPWStr)] string pszNamespace);

        [PreserveSig]
        int GetNamespace([Out, MarshalAs(UnmanagedType.LPWStr)] out string ppszNamespace);
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("9ce50f2d-6ba7-40fb-a034-50b1a674ec78"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IStreamBufferInitialize
    {
        [PreserveSig]
        int SetHKEY([In] IntPtr hkeyRoot); // HKEY

        [PreserveSig]
        int SetSIDs(
            [In] int cSIDs,
            [In, MarshalAs(UnmanagedType.LPArray)] IntPtr[] ppSID // PSID *
            );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("afd1f242-7efd-45ee-ba4e-407a25c9a77a"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IStreamBufferSink
    {
        [PreserveSig]
        int LockProfile([In, MarshalAs(UnmanagedType.LPWStr)] string pszStreamBufferFilename);

        [PreserveSig]
        int CreateRecorder(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pszFilename,
            [In] RecordingType dwRecordType,
            [Out, MarshalAs(UnmanagedType.IUnknown)] out object pRecordingIUnknown
            );

        [PreserveSig]
        int IsProfileLocked();
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("DB94A660-F4FB-4bfa-BCC6-FE159A4EEA93"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IStreamBufferSink2 : IStreamBufferSink
    {
        #region IStreamBufferSink Methods

        [PreserveSig]
        new int LockProfile([In, MarshalAs(UnmanagedType.LPWStr)] string pszStreamBufferFilename);

        [PreserveSig]
        new int CreateRecorder(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pszFilename,
            [In] RecordingType dwRecordType,
            [Out, MarshalAs(UnmanagedType.IUnknown)] out object pRecordingIUnknown
            );

        [PreserveSig]
        new int IsProfileLocked();

        #endregion

        [PreserveSig]
        int UnlockProfile();
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("974723f2-887a-4452-9366-2cff3057bc8f"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IStreamBufferSink3 : IStreamBufferSink2
    {
        #region IStreamBufferSink Methods

        [PreserveSig]
        new int LockProfile([In, MarshalAs(UnmanagedType.LPWStr)] string pszStreamBufferFilename);

        [PreserveSig]
        new int CreateRecorder(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pszFilename,
            [In] RecordingType dwRecordType,
            [Out, MarshalAs(UnmanagedType.IUnknown)] out object pRecordingIUnknown
            );

        [PreserveSig]
        new int IsProfileLocked();

        #endregion

        #region IStreamBufferSink2

        [PreserveSig]
        new int UnlockProfile();

        #endregion

        [PreserveSig]
        int SetAvailableFilter([In, Out] ref long prtMin);
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("1c5bd776-6ced-4f44-8164-5eab0e98db12"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IStreamBufferSource
    {
        [PreserveSig]
        int SetStreamSink([In] IStreamBufferSink pIStreamBufferSink);
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("ba9b6c99-f3c7-4ff2-92db-cfdd4851bf31"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IStreamBufferRecordControl
    {
        [PreserveSig]
        int Start([In, Out] ref long prtStart);

        [PreserveSig]
        int Stop([In] long rtStop);

        [PreserveSig]
        int GetRecordingStatus(
            [Out] out int phResult,
            [Out, MarshalAs(UnmanagedType.Bool)] out bool pbStarted,
            [Out, MarshalAs(UnmanagedType.Bool)] out bool pbStopped
            );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("9E259A9B-8815-42ae-B09F-221970B154FD"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IStreamBufferRecComp
    {
        [PreserveSig]
        int Initialize(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pszTargetFilename,
            [In, MarshalAs(UnmanagedType.LPWStr)] string pszSBRecProfileRef
            );

        [PreserveSig]
        int Append([In, MarshalAs(UnmanagedType.LPWStr)] string pszSBRecording);

        [PreserveSig]
        int AppendEx(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pszSBRecording,
            [In] long rtStart,
            [In] long rtStop
            );

        [PreserveSig]
        int GetCurrentLength([Out] out int pcSeconds);

        [PreserveSig]
        int Close();

        [PreserveSig]
        int Cancel();
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("16CA4E03-FE69-4705-BD41-5B7DFC0C95F3"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IStreamBufferRecordingAttribute
    {
        [PreserveSig]
        int SetAttribute(
            [In] int ulReserved,
            [In, MarshalAs(UnmanagedType.LPWStr)] string pszAttributeName,
            [In] StreamBufferAttrDataType StreamBufferAttributeType,
            [In] IntPtr pbAttribute, // BYTE *
            [In] short cbAttributeLength
            );

        [PreserveSig]
        int GetAttributeCount(
            [In] int ulReserved,
            [Out] out short pcAttributes
            );

        [PreserveSig]
        int GetAttributeByName(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pszAttributeName,
            [In] int pulReserved,
            [Out] out StreamBufferAttrDataType pStreamBufferAttributeType,
            [In, Out] IntPtr pbAttribute, // BYTE *
            [In, Out] ref short pcbLength
            );

        [PreserveSig]
        int GetAttributeByIndex(
            [In] short wIndex,
            [In] int pulReserved,
            [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszAttributeName,
            [In, Out] ref short pcchNameLength,
            [Out] out StreamBufferAttrDataType pStreamBufferAttributeType,
            IntPtr pbAttribute, // BYTE *
            [In, Out] ref short pcbLength
            );

        int EnumAttributes([Out] out IEnumStreamBufferRecordingAttrib ppIEnumStreamBufferAttrib);
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("C18A9162-1E82-4142-8C73-5690FA62FE33"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IEnumStreamBufferRecordingAttrib
    {
        [PreserveSig]
        int Next(
            [In] int cRequest,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] StreamBufferAttribute[] pStreamBufferAttribute,
            [In] IntPtr pcReceived
            );

        [PreserveSig]
        int Skip([In] int cRecords);

        [PreserveSig]
        int Reset();

        [PreserveSig]
        int Clone([Out] out IEnumStreamBufferRecordingAttrib ppIEnumStreamBufferAttrib);
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("ce14dfae-4098-4af7-bbf7-d6511f835414"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IStreamBufferConfigure
    {
        [PreserveSig]
        int SetDirectory([In, MarshalAs(UnmanagedType.LPWStr)] string pszDirectoryName);

        [PreserveSig]
        int GetDirectory([Out, MarshalAs(UnmanagedType.LPWStr)] out string pszDirectoryName);

        [PreserveSig]
        int SetBackingFileCount(
            [In] int dwMin,
            [In] int dwMax
            );

        [PreserveSig]
        int GetBackingFileCount(
            [Out] out int dwMin,
            [Out] out int dwMax
            );

        [PreserveSig]
        int SetBackingFileDuration([In] int dwSeconds);

        [PreserveSig]
        int GetBackingFileDuration([Out] out int pdwSeconds);
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("53E037BF-3992-4282-AE34-2487B4DAE06B"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IStreamBufferConfigure2 : IStreamBufferConfigure
    {
        #region IStreamBufferConfigure

        [PreserveSig]
        new int SetDirectory([In, MarshalAs(UnmanagedType.LPWStr)] string pszDirectoryName);

        [PreserveSig]
        new int GetDirectory([Out, MarshalAs(UnmanagedType.LPWStr)] out string pszDirectoryName);

        [PreserveSig]
        new int SetBackingFileCount(
            [In] int dwMin,
            [In] int dwMax
            );

        [PreserveSig]
        new int GetBackingFileCount(
            [Out] out int dwMin,
            [Out] out int dwMax
            );

        [PreserveSig]
        new int SetBackingFileDuration([In] int dwSeconds);

        [PreserveSig]
        new int GetBackingFileDuration([Out] out int pdwSeconds);

        #endregion

        [PreserveSig]
        int SetMultiplexedPacketSize([In] int cbBytesPerPacket);

        [PreserveSig]
        int GetMultiplexedPacketSize([Out] out int pcbBytesPerPacket);

        [PreserveSig]
        int SetFFTransitionRates(
            [In] int dwMaxFullFrameRate,
            [In] int dwMaxNonSkippingRate
            );

        [PreserveSig]
        int GetFFTransitionRates(
            [Out] out int pdwMaxFullFrameRate,
            [Out] out int pdwMaxNonSkippingRate
            );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("f61f5c26-863d-4afa-b0ba-2f81dc978596"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IStreamBufferMediaSeeking : IMediaSeeking
    {
        #region IMediaSeeking Methods

        [PreserveSig]
        new int GetCapabilities([Out] out AMSeekingSeekingCapabilities pCapabilities);

        [PreserveSig]
        new int CheckCapabilities([In, Out] ref AMSeekingSeekingCapabilities pCapabilities);

        [PreserveSig]
        new int IsFormatSupported([In] ref Guid pFormat);

        [PreserveSig]
        new int QueryPreferredFormat([Out] out Guid pFormat);

        [PreserveSig]
        new int GetTimeFormat([Out] out Guid pFormat);

        [PreserveSig]
        new int IsUsingTimeFormat([In, MarshalAs(UnmanagedType.LPStruct)] Guid pFormat);

        [PreserveSig]
        new int SetTimeFormat([In, MarshalAs(UnmanagedType.LPStruct)] Guid pFormat);

        [PreserveSig]
        new int GetDuration([Out] out long pDuration);

        [PreserveSig]
        new int GetStopPosition([Out] out long pStop);

        [PreserveSig]
        new int GetCurrentPosition([Out] out long pCurrent);

        [PreserveSig]
        new int ConvertTimeFormat(
            [Out] out long pTarget,
            [In, MarshalAs(UnmanagedType.LPStruct)] DsGuid pTargetFormat,
            [In] long Source,
            [In, MarshalAs(UnmanagedType.LPStruct)] DsGuid pSourceFormat
            );

        [PreserveSig]
        new int SetPositions(
            [In, Out, MarshalAs(UnmanagedType.LPStruct)] DsLong pCurrent,
            [In] AMSeekingSeekingFlags dwCurrentFlags,
            [In, Out, MarshalAs(UnmanagedType.LPStruct)] DsLong pStop,
            [In] AMSeekingSeekingFlags dwStopFlags
            );

        [PreserveSig]
        new int GetPositions(
            [Out] out long pCurrent,
            [Out] out long pStop
            );

        [PreserveSig]
        new int GetAvailable(
            [Out] out long pEarliest,
            [Out] out long pLatest
            );

        [PreserveSig]
        new int SetRate([In] double dRate);

        [PreserveSig]
        new int GetRate([Out] out double pdRate);

        [PreserveSig]
        new int GetPreroll([Out] out long pllPreroll);

        #endregion
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("3a439ab0-155f-470a-86a6-9ea54afd6eaf"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IStreamBufferMediaSeeking2 : IStreamBufferMediaSeeking
    {
        #region IMediaSeeking

        [PreserveSig]
        new int GetCapabilities([Out] out AMSeekingSeekingCapabilities pCapabilities);

        [PreserveSig]
        new int CheckCapabilities([In, Out] ref AMSeekingSeekingCapabilities pCapabilities);

        [PreserveSig]
        new int IsFormatSupported([In, MarshalAs(UnmanagedType.LPStruct)] Guid pFormat);

        [PreserveSig]
        new int QueryPreferredFormat([Out] out Guid pFormat);

        [PreserveSig]
        new int GetTimeFormat([Out] out Guid pFormat);

        [PreserveSig]
        new int IsUsingTimeFormat([In, MarshalAs(UnmanagedType.LPStruct)] Guid pFormat);

        [PreserveSig]
        new int SetTimeFormat([In, MarshalAs(UnmanagedType.LPStruct)] Guid pFormat);

        [PreserveSig]
        new int GetDuration([Out] out long pDuration);

        [PreserveSig]
        new int GetStopPosition([Out] out long pStop);

        [PreserveSig]
        new int GetCurrentPosition([Out] out long pCurrent);

        [PreserveSig]
        new int ConvertTimeFormat(
            [Out] out long pTarget,
            [In, MarshalAs(UnmanagedType.LPStruct)] DsGuid pTargetFormat,
            [In] long Source,
            [In, MarshalAs(UnmanagedType.LPStruct)] DsGuid pSourceFormat
            );

        [PreserveSig]
        new int SetPositions(
            [In, Out, MarshalAs(UnmanagedType.LPStruct)] DsLong pCurrent,
            [In] AMSeekingSeekingFlags dwCurrentFlags,
            [In, Out, MarshalAs(UnmanagedType.LPStruct)] DsLong pStop,
            [In] AMSeekingSeekingFlags dwStopFlags
            );

        [PreserveSig]
        new int GetPositions(
            [Out] out long pCurrent,
            [Out] out long pStop
            );

        [PreserveSig]
        new int GetAvailable(
            [Out] out long pEarliest,
            [Out] out long pLatest
            );

        [PreserveSig]
        new int SetRate([In] double dRate);

        [PreserveSig]
        new int GetRate([Out] out double pdRate);

        [PreserveSig]
        new int GetPreroll([Out] out long pllPreroll);

        #endregion

        [PreserveSig]
        int SetRateEx(
            [In] double dRate,
            [In] int dwFramesPerSec
            );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("9D2A2563-31AB-402e-9A6B-ADB903489440"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IStreamBufferDataCounters
    {
        [PreserveSig]
        int GetData([Out] out SBEPinData pPinData);

        [PreserveSig]
        int ResetData();
    }

    #endregion
}
