

using System;
using System.Runtime.InteropServices;

namespace NewTOAPIA.DirectShow.BDA
{
    using NewTOAPIA.DirectShow.Core;
    using NewTOAPIA.DirectShow.DES;

    #region Declarations

#if ALLOW_UNTESTED_INTERFACES




#endif



    /// <summary>
    /// From BDANODE_DESCRIPTOR
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct BDANodeDescriptor
    {
        public int ulBdaNodeType;
        public Guid guidFunction;
        public Guid guidName;
    }


    #endregion

    #region Interfaces

#if ALLOW_UNTESTED_INTERFACES

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("fd501041-8ebe-11ce-8183-00aa00577da2"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IBDA_NetworkProvider
    {
        [PreserveSig]
        int PutSignalSource([In] int ulSignalSource);

        [PreserveSig]
        int GetSignalSource([Out] out int pulSignalSource);

        [PreserveSig]
        int GetNetworkType([Out] out Guid pguidNetworkType);

        [PreserveSig]
        int PutTuningSpace([In, MarshalAs(UnmanagedType.LPStruct)] Guid guidTuningSpace);

        [PreserveSig]
        int GetTuningSpace([Out] out Guid pguidTuingSpace);

        [PreserveSig]
        int RegisterDeviceFilter(
            [In, MarshalAs(UnmanagedType.Interface)] object pUnkFilterControl,
            [Out] out int ppvRegisitrationContext
            );

        [PreserveSig]
        int UnRegisterDeviceFilter([In] int pvRegistrationContext);
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("71985F46-1CA1-11d3-9CC8-00C04F7971E0"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IBDA_VoidTransform
    {
        [PreserveSig]
        int Start();

        [PreserveSig]
        int Stop();
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("DDF15B0D-BD25-11d2-9CA0-00C04F7971E0"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IBDA_NullTransform
    {
        [PreserveSig]
        int Start();

        [PreserveSig]
        int Stop();
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("afb6c2a2-2c41-11d3-8a60-0000f81e0e4a"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IEnumPIDMap
    {
        [PreserveSig]
        int Next(
            [In] int cRequest,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0, ArraySubType = UnmanagedType.Struct)] PIDMap[] pPIDMap,
            [In] IntPtr pcReceived
            );

        [PreserveSig]
        int Skip([In] int cRecords);

        [PreserveSig]
        int Reset();

        [PreserveSig]
        int Clone([Out] out IEnumPIDMap ppIEnumPIDMap);
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("0DED49D5-A8B7-4d5d-97A1-12B0C195874D"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IBDA_PinControl
    {
        [PreserveSig]
        int GetPinID([Out] out int pulPinID);

        [PreserveSig]
        int GetPinType([Out] out int pulPinType);

        [PreserveSig]
        int RegistrationContext([Out] out int pulRegistrationCtx);
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("34518D13-1182-48e6-B28F-B24987787326"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IBDA_AutoDemodulateEx : IBDA_AutoDemodulate
    {
    #region IBDA_AutoDemodulate Methods

        [PreserveSig]
        new int put_AutoDemodulate();

        #endregion

        [PreserveSig]
        int get_SupportedDeviceNodeTypes(
            [In] int ulcDeviceNodeTypesMax,
            [Out] out int pulcDeviceNodeTypes,
            [In, Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Struct)] Guid[] pguidDeviceNodeTypes
            );

        [PreserveSig]
        int get_SupportedVideoFormats(
          [Out] out AMTunerModeType pulAMTunerModeType,
          [Out] out AnalogVideoStandard pulAnalogVideoStandard
          );

        [PreserveSig]
        int get_AuxInputCount(
          [Out] out int pulCompositeCount,
          [Out] out int pulSvideoCount
          );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("D806973D-3EBE-46de-8FBB-6358FE784208"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IBDA_EasMessage
    {
        [PreserveSig]
        int get_EasMessage(
          [In] int ulEventID,
          [Out, MarshalAs(UnmanagedType.IUnknown)] out object ppEASObject
          );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("8E882535-5F86-47AB-86CF-C281A72A0549"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IBDA_TransportStreamInfo
    {
        [PreserveSig]
        int get_PatTableTickCount([Out] out int pPatTickCount);
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("CD51F1E0-7BE9-4123-8482-A2A796C0A6B0"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IBDA_ConditionalAccess
    {
        [PreserveSig]
        int get_SmartCardStatus(
            [Out] out SmartCardStatusType pCardStatus,
            [Out] out SmartCardAssociationType pCardAssociation,
            [Out, MarshalAs(UnmanagedType.BStr)] out string pbstrCardError,
            [Out, MarshalAs(UnmanagedType.VariantBool)] out bool pfOOBLocked
            );

        [PreserveSig]
        int get_SmartCardInfo(
            [Out, MarshalAs(UnmanagedType.BStr)] out string pbstrCardName,
            [Out, MarshalAs(UnmanagedType.BStr)] out string pbstrCardManufacturer,
            [Out, MarshalAs(UnmanagedType.VariantBool)] out bool pfDaylightSavings,
            [Out] out byte pbyRatingRegion,
            [Out] out int plTimeZoneOffsetMinutes,
            [Out, MarshalAs(UnmanagedType.BStr)] out string pbstrLanguage,
            [Out] out EALocationCodeType pEALocationCode
            );

        [PreserveSig]
        int get_SmartCardApplications(
            [In, Out] ref int pulcApplications,
            [In] int ulcApplicationsMax,
            [In, Out] SmartCardApplication[] rgApplications
            );

        [PreserveSig]
        int get_Entitlement(
            [In] short usVirtualChannel,
            [Out] out EntitlementType pEntitlement
            );

        [PreserveSig]
        int TuneByChannel([In] short usVirtualChannel);

        [PreserveSig]
        int SetProgram([In] short usProgramNumber);

        [PreserveSig]
        int AddProgram([In] short usProgramNumber);

        [PreserveSig]
        int RemoveProgram([In] short usProgramNumber);

        [PreserveSig]
        int GetModuleUI(
            [In] byte byDialogNumber,
            [Out, MarshalAs(UnmanagedType.BStr)] out string pbstrURL
            );

        [PreserveSig]
        int InformUIClosed(
            [In] byte byDialogNumber,
            [In] UICloseReasonType CloseReason
            );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("20e80cb5-c543-4c1b-8eb3-49e719eee7d4"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IBDA_DiagnosticProperties : IPropertyBag
    {
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("F98D88B0-1992-4cd6-A6D9-B9AFAB99330D"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IBDA_DRM
    {
        [PreserveSig]
        int GetDRMPairingStatus(
          [Out] out BDA_DrmPairingError pdwStatus,
          [Out] out int phError
          );

        [PreserveSig]
        int PerformDRMPairing([In, MarshalAs(UnmanagedType.Bool)] bool fSync);
    }

#endif

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("1347D106-CF3A-428a-A5CB-AC0D9A2A4338"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IBDA_SignalStatistics
    {
        [PreserveSig]
        int put_SignalStrength([In] int lDbStrength);

        [PreserveSig]
        int get_SignalStrength([Out] out int plDbStrength);

        [PreserveSig]
        int put_SignalQuality([In] int lPercentQuality);

        [PreserveSig]
        int get_SignalQuality([Out] out int plPercentQuality);

        [PreserveSig]
        int put_SignalPresent([In, MarshalAs(UnmanagedType.U1)] bool fPresent);

        [PreserveSig]
        int get_SignalPresent([Out, MarshalAs(UnmanagedType.U1)] out bool pfPresent);

        [PreserveSig]
        int put_SignalLocked([In, MarshalAs(UnmanagedType.U1)] bool fLocked);

        [PreserveSig]
        int get_SignalLocked([Out, MarshalAs(UnmanagedType.U1)] out bool pfLocked);

        [PreserveSig]
        int put_SampleTime([In] int lmsSampleTime);

        [PreserveSig]
        int get_SampleTime([Out] out int plmsSampleTime);
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("79B56888-7FEA-4690-B45D-38FD3C7849BE"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IBDA_Topology
    {
        [PreserveSig]
        int GetNodeTypes(
            [Out] out int pulcNodeTypes,
            [In] int ulcNodeTypesMax,
            [Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4, SizeParamIndex = 1)] int[] rgulNodeTypes
            );

        [PreserveSig]
        int GetNodeDescriptors(
            [Out] out int ulcNodeDescriptors,
            [In] int ulcNodeDescriptorsMax,
            [Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStruct, SizeParamIndex = 1)] BDANodeDescriptor[] rgNodeDescriptors
            );

        [PreserveSig]
        int GetNodeInterfaces(
            [In] int ulNodeType,
            [Out] out int pulcInterfaces,
            [In] int ulcInterfacesMax,
            [Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStruct, SizeParamIndex = 2)] Guid[] rgguidInterfaces
            );

        [PreserveSig]
        int GetPinTypes(
            [Out] out int pulcPinTypes,
            [In] int ulcPinTypesMax,
            [Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4, SizeParamIndex = 1)] int[] rgulPinTypes
            );

        [PreserveSig]
        int GetTemplateConnections(
            [Out] out int pulcConnections,
            [In] int ulcConnectionsMax,
            [Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStruct, SizeParamIndex = 1)] BDATemplateConnection[] rgConnections
            );

        [PreserveSig]
        int CreatePin(
            [In] int ulPinType,
            [Out] out int pulPinId
            );

        [PreserveSig]
        int DeletePin([In] int ulPinId);

        [PreserveSig]
        int SetMediaType(
            [In] int ulPinId,
            [In, MarshalAs(UnmanagedType.LPStruct)] AMMediaType pMediaType
            );

        [PreserveSig]
        int SetMedium(
            [In] int ulPinId,
            [In] RegPinMedium pMedium
            );

        [PreserveSig]
        int CreateTopology(
            [In] int ulInputPinId,
            [In] int ulOutputPinId
            );

        [PreserveSig]
        int GetControlNode(
            [In] int ulInputPinId,
            [In] int ulOutputPinId,
            [In] int ulNodeType,
            [Out, MarshalAs(UnmanagedType.IUnknown)] out object ppControlNode // IUnknown
            );

    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("06FB45C1-693C-4ea7-B79F-7A6A54D8DEF2"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IFrequencyMap
    {
        [PreserveSig]
        int get_FrequencyMapping(
            [Out] out int ulCount,
            [Out] out IntPtr ppulList
            );

        [PreserveSig]
        int put_FrequencyMapping(
            [In] int ulCount,
            [In, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4)] int[] pList
            );

        [PreserveSig]
        int get_CountryCode([Out] out int pulCountryCode);

        [PreserveSig]
        int put_CountryCode([In] int ulCountryCode);

        [PreserveSig]
        int get_DefaultFrequencyMapping(
            [In] int ulCountryCode,
            [Out] out int pulCount,
            [Out] out IntPtr ppulList
            );

        [PreserveSig]
        int get_CountryCodeList(
            [Out] out int pulCount,
            [Out] out IntPtr ppulList
            );

    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("DDF15B12-BD25-11d2-9CA0-00C04F7971E0"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IBDA_AutoDemodulate
    {
        [PreserveSig]
        int put_AutoDemodulate();
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("FD0A5AF3-B41D-11d2-9C95-00C04F7971E0"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IBDA_DeviceControl
    {
        [PreserveSig]
        int StartChanges();

        [PreserveSig]
        int CheckChanges();

        [PreserveSig]
        int CommitChanges();

        [PreserveSig]
        int GetChangeState([Out] out BDAChangeState pState);
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("EF30F379-985B-4d10-B640-A79D5E04E1E0"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IBDA_DigitalDemodulator
    {
        [PreserveSig]
        int put_ModulationType([In] ref ModulationType pModulationType);

        [PreserveSig]
        int get_ModulationType([Out] out ModulationType pModulationType);

        [PreserveSig]
        int put_InnerFECMethod([In] ref FECMethod pFECMethod);

        [PreserveSig]
        int get_InnerFECMethod([Out] out FECMethod pFECMethod);

        [PreserveSig]
        int put_InnerFECRate([In] ref BinaryConvolutionCodeRate pFECRate);

        [PreserveSig]
        int get_InnerFECRate([Out] out BinaryConvolutionCodeRate pFECRate);

        [PreserveSig]
        int put_OuterFECMethod([In] ref FECMethod pFECMethod);

        [PreserveSig]
        int get_OuterFECMethod([Out] out FECMethod pFECMethod);

        [PreserveSig]
        int put_OuterFECRate([In] ref BinaryConvolutionCodeRate pFECRate);

        [PreserveSig]
        int get_OuterFECRate([Out] out BinaryConvolutionCodeRate pFECRate);

        [PreserveSig]
        int put_SymbolRate([In] ref int pSymbolRate);

        [PreserveSig]
        int get_SymbolRate([Out] out int pSymbolRate);

        [PreserveSig]
        int put_SpectralInversion([In] ref SpectralInversion pSpectralInversion);

        [PreserveSig]
        int get_SpectralInversion([Out] out SpectralInversion pSpectralInversion);
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("71985F43-1CA1-11d3-9CC8-00C04F7971E0"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IBDA_EthernetFilter
    {
        [PreserveSig]
        int GetMulticastListSize(
            out int pulcbAddresses);

        [PreserveSig]
        int PutMulticastList(
            int ulcbAddresses,
            IntPtr pAddressList);

        [PreserveSig]
        int GetMulticastList(
            ref int pulcbAddresses,
            IntPtr pAddressList);

        [PreserveSig]
        int PutMulticastMode(
            MulticastMode ulModeMask);

        [PreserveSig]
        int GetMulticastMode(
            out MulticastMode pulModeMask);

    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("71985F47-1CA1-11d3-9CC8-00C04F7971E0"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IBDA_FrequencyFilter
    {
        [PreserveSig]
        int put_Autotune([In] int ulTransponder);

        [PreserveSig]
        int get_Autotune([Out] out int pulTransponder);

        [PreserveSig]
        int put_Frequency([In] int ulFrequency);

        [PreserveSig]
        int get_Frequency([Out] out int pulFrequency);

        [PreserveSig]
        int put_Polarity([In] Polarisation Polarity);

        [PreserveSig]
        int get_Polarity([Out] out Polarisation pPolarity);

        [PreserveSig]
        int put_Range([In] int ulRange);

        [PreserveSig]
        int get_Range([Out] out int pulRange);

        [PreserveSig]
        int put_Bandwidth([In] int ulBandwidth);

        [PreserveSig]
        int get_Bandwidth([Out] out int pulBandwidth);

        [PreserveSig]
        int put_FrequencyMultiplier([In] int ulMultiplier);

        [PreserveSig]
        int get_FrequencyMultiplier([Out] out int pulMultiplier);
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("3F4DC8E2-4050-11d3-8F4B-00C04F7971E2"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    Obsolete("IBDA_IPSinkControl is no longer being supported for Ring 3 clients. Use the BDA_IPSinkInfo interface instead.")]
    public interface IBDA_IPSinkControl
    {
        [PreserveSig]
        int GetMulticastList(
            out int pulcbSize,
            out IntPtr pbBuffer); // BYTE **

        [PreserveSig]
        int GetAdapterIPAddress(
            out int pulcbSize,
            out IntPtr pbBuffer); // BYTE **
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("A750108F-492E-4d51-95F7-649B23FF7AD7"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IBDA_IPSinkInfo
    {
        [PreserveSig]
        int get_MulticastList(
            out int pulcbAddresses,
            out IntPtr ppbAddressList); // BYTE **

        [PreserveSig]
        int get_AdapterIPAddress(
            [MarshalAs(UnmanagedType.BStr)] out string pbstrBuffer);

        [PreserveSig]
        int get_AdapterDescription(
            [MarshalAs(UnmanagedType.BStr)]  out string pbstrBuffer);
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("71985F44-1CA1-11d3-9CC8-00C04F7971E0"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IBDA_IPV4Filter
    {
        [PreserveSig]
        int GetMulticastListSize(
            out int pulcbAddresses);

        [PreserveSig]
        int PutMulticastList(
            int ulcbAddresses,
            IntPtr pAddressList);

        [PreserveSig]
        int GetMulticastList(
            ref int pulcbAddresses,
            IntPtr pAddressList);

        [PreserveSig]
        int PutMulticastMode(
            MulticastMode ulModeMask);

        [PreserveSig]
        int GetMulticastMode(
            out MulticastMode pulModeMask);

    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("E1785A74-2A23-4fb3-9245-A8F88017EF33"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IBDA_IPV6Filter
    {
        [PreserveSig]
        int GetMulticastListSize(
            out int pulcbAddresses);

        [PreserveSig]
        int PutMulticastList(
            int ulcbAddresses,
            IntPtr pAddressList);  // BYTE []

        [PreserveSig]
        int GetMulticastList(
            ref int pulcbAddresses,
            IntPtr pAddressList);  // BYTE []

        [PreserveSig]
        int PutMulticastMode(
            MulticastMode ulModeMask);

        [PreserveSig]
        int GetMulticastMode(
            out MulticastMode pulModeMask);
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("992CF102-49F9-4719-A664-C4F23E2408F4"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IBDA_LNBInfo
    {
        [PreserveSig]
        int put_LocalOscilatorFrequencyLowBand([In] int ulLOFLow);

        [PreserveSig]
        int get_LocalOscilatorFrequencyLowBand([Out] out int pulLOFLow);

        [PreserveSig]
        int put_LocalOscilatorFrequencyHighBand([In] int ulLOFHigh);

        [PreserveSig]
        int get_LocalOscilatorFrequencyHighBand([Out] out int pulLOFHigh);

        [PreserveSig]
        int put_HighLowSwitchFrequency([In] int ulSwitchFrequency);

        [PreserveSig]
        int get_HighLowSwitchFrequency([Out] out int pulSwitchFrequency);
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("afb6c2a1-2c41-11d3-8a60-0000f81e0e4a"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMPEG2PIDMap
    {
        [PreserveSig]
        int MapPID(
            [In] int culPID,
            [In, MarshalAs(UnmanagedType.LPArray)] int[] pulPID,
            [In] MediaSampleContent MediaSampleContent
            );

        [PreserveSig]
        int UnmapPID(
            [In] int culPID,
            [In, MarshalAs(UnmanagedType.LPArray)] int[] pulPID
            );

        [PreserveSig,
        Obsolete("Because of bug in DS 9.0c, you can't get the PID map from .NET", false)]
#if ALLOW_UNTESTED_INTERFACES
        int EnumPIDMap([Out] out IEnumPIDMap pIEnumPIDMap);
#else
        int EnumPIDMap([Out] out object pIEnumPIDMap);
#endif

    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("D2F1644B-B409-11d2-BC69-00A0C9EE9E16"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IBDA_SignalProperties
    {
        [PreserveSig]
        int PutNetworkType([In, MarshalAs(UnmanagedType.LPStruct)] Guid guidNetworkType);

        [PreserveSig]
        int GetNetworkType([Out] out Guid pguidNetworkType);

        [PreserveSig]
        int PutSignalSource([In] int ulSignalSource);

        [PreserveSig]
        int GetSignalSource([Out] out int pulSignalSource);

        [PreserveSig]
        int PutTuningSpace([In, MarshalAs(UnmanagedType.LPStruct)] Guid guidTuningSpace);

        [PreserveSig]
        int GetTuningSpace([Out] out Guid pguidTuingSpace);
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("4B2BD7EA-8347-467b-8DBF-62F784929CC3"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ICCSubStreamFiltering
    {
        [PreserveSig]
        int get_SubstreamTypes([Out] out CCSubstreamService Types);

        [PreserveSig]
        int put_SubstreamTypes([In] CCSubstreamService Types);
    }

    #endregion
}
