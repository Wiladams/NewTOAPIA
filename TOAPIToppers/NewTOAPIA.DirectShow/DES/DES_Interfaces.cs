using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;

namespace NewTOAPIA.DirectShow.DES
{
    using NewTOAPIA.DirectShow.Core;

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    Guid("E43E73A2-0EFA-11D3-9601-00A0C9441E20")]
    public interface IAMErrorLog
    {
        [PreserveSig]
        int LogError(
            int Severity,
            [MarshalAs(UnmanagedType.BStr)] string pErrorString,
            int ErrorCode,
            int hresult,
            [In] IntPtr pExtraInfo
            );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
Guid("963566DA-BE21-4EAF-88E9-35704F8F52A1")]
    public interface IAMSetErrorLog
    {
        [PreserveSig]
        int get_ErrorLog(
            out IAMErrorLog pVal
            );

        [PreserveSig]
        int put_ErrorLog(
            IAMErrorLog newVal
            );
    }


    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
Guid("78530B74-61F9-11D2-8CAD-00A024580902")]
    public interface IAMTimeline
    {
        [PreserveSig]
        int CreateEmptyNode(
            out IAMTimelineObj ppObj,
            TimelineMajorType Type
            );

        [PreserveSig]
        int AddGroup(
            IAMTimelineObj pGroup
            );

        [PreserveSig]
        int RemGroupFromList(
            IAMTimelineObj pGroup
            );

        [PreserveSig]
        int GetGroup(
            out IAMTimelineObj ppGroup,
            int WhichGroup
            );

        [PreserveSig]
        int GetGroupCount(
            out int pCount
            );

        [PreserveSig]
        int ClearAllGroups();

        [PreserveSig]
        int GetInsertMode(
            out TimelineInsertMode pMode
            );

        [PreserveSig]
        int SetInsertMode(
            TimelineInsertMode Mode
            );

        [PreserveSig]
        int EnableTransitions(
            [MarshalAs(UnmanagedType.Bool)] bool fEnabled
            );

        [PreserveSig]
        int TransitionsEnabled(
            [MarshalAs(UnmanagedType.Bool)] out bool pfEnabled
            );

        [PreserveSig]
        int EnableEffects(
            [MarshalAs(UnmanagedType.Bool)] bool fEnabled
            );

        [PreserveSig]
        int EffectsEnabled(
            [MarshalAs(UnmanagedType.Bool)] out bool pfEnabled
            );

        [PreserveSig]
        int SetInterestRange(
            long Start,
            long Stop
            );

        [PreserveSig]
        int GetDuration(
            out long pDuration
            );

        [PreserveSig]
        int GetDuration2(
            out double pDuration
            );

        [PreserveSig]
        int SetDefaultFPS(
            double FPS
            );

        [PreserveSig]
        int GetDefaultFPS(
            out double pFPS
            );

        [PreserveSig]
        int IsDirty(
            [MarshalAs(UnmanagedType.Bool)] out bool pDirty
            );

        [PreserveSig]
        int GetDirtyRange(
            out long pStart,
            out long pStop
            );

        [PreserveSig]
        int GetCountOfType(
            int Group,
            out int pVal,
            out int pValWithComps,
            TimelineMajorType majortype
            );

        [PreserveSig]
        int ValidateSourceNames(
            SFNValidateFlags ValidateFlags,
            IMediaLocator pOverride,
            IntPtr NotifyEventHandle
            );

        [PreserveSig]
        int SetDefaultTransition(
            [MarshalAs(UnmanagedType.LPStruct)] Guid pGuid
            );

        [PreserveSig]
        int GetDefaultTransition(
            out Guid pGuid
            );

        [PreserveSig]
        int SetDefaultEffect(
            [MarshalAs(UnmanagedType.LPStruct)] Guid pGuid
            );

        [PreserveSig]
        int GetDefaultEffect(
            out Guid pGuid
            );

        [PreserveSig]
        int SetDefaultTransitionB(
            [MarshalAs(UnmanagedType.BStr)] string pGuid
            );

        [PreserveSig]
        int GetDefaultTransitionB(
            [Out, MarshalAs(UnmanagedType.BStr)] out string sGuid
            );

        [PreserveSig]
        int SetDefaultEffectB(
            [MarshalAs(UnmanagedType.BStr)] string pGuid
            );

        [PreserveSig]
        int GetDefaultEffectB(
            [Out, MarshalAs(UnmanagedType.BStr)] out string sGuid
            );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
Guid("EAE58536-622E-11D2-8CAD-00A024580902")]
    public interface IAMTimelineComp
    {
        [PreserveSig]
        int VTrackInsBefore(
            IAMTimelineObj pVirtualTrack,
            int priority
            );

        [PreserveSig]
        int VTrackSwapPriorities(
            int VirtualTrackA,
            int VirtualTrackB
            );

        [PreserveSig]
        int VTrackGetCount(
            out int pVal
            );

        [PreserveSig]
        int GetVTrack(
            out IAMTimelineObj ppVirtualTrack,
            int Which
            );

        [PreserveSig]
        int GetCountOfType(
            out int pVal,
            out int pValWithComps,
            TimelineMajorType majortype
            );

        [PreserveSig]
        int GetRecursiveLayerOfType(
            out IAMTimelineObj ppVirtualTrack,
            int WhichLayer,
            TimelineMajorType Type
            );

        [PreserveSig]
        int GetRecursiveLayerOfTypeI(
            out IAMTimelineObj ppVirtualTrack,
            [In, Out] ref int pWhichLayer,
            TimelineMajorType Type
            );

        [PreserveSig]
        int GetNextVTrack(
            IAMTimelineObj pVirtualTrack,
            out IAMTimelineObj ppNextVirtualTrack
            );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
Guid("BCE0C264-622D-11D2-8CAD-00A024580902")]
    public interface IAMTimelineEffect
    {
        [PreserveSig]
        int EffectGetPriority(out int pVal);
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
Guid("EAE58537-622E-11D2-8CAD-00A024580902"),
InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMTimelineEffectable
    {
        [PreserveSig]
        int EffectInsBefore(
            IAMTimelineObj pFX,
            int priority
            );

        [PreserveSig]
        int EffectSwapPriorities(
            int PriorityA,
            int PriorityB
            );

        [PreserveSig]
        int EffectGetCount(
            out int pCount
            );

        [PreserveSig]
        int GetEffect(
            out IAMTimelineObj ppFx,
            int Which
            );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
Guid("9EED4F00-B8A6-11D2-8023-00C0DF10D434"),
InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMTimelineGroup
    {
        [PreserveSig]
        int SetTimeline(
            IAMTimeline pTimeline
            );

        [PreserveSig]
        int GetTimeline(
            out IAMTimeline ppTimeline
            );

        [PreserveSig]
        int GetPriority(
            out int pPriority
            );

        [PreserveSig]
        int GetMediaType(
            [Out, MarshalAs(UnmanagedType.LPStruct)] AMMediaType pmt
            );

        [PreserveSig]
        int SetMediaType(
            [In, MarshalAs(UnmanagedType.LPStruct)] AMMediaType pmt
            );

        [PreserveSig]
        int SetOutputFPS(
            double FPS
            );

        [PreserveSig]
        int GetOutputFPS(
            out double pFPS
            );

        [PreserveSig]
        int SetGroupName(
            [MarshalAs(UnmanagedType.BStr)] string pGroupName
            );

        [PreserveSig]
        int GetGroupName(
            [MarshalAs(UnmanagedType.BStr)] out string pGroupName
            );

        [PreserveSig]
        int SetPreviewMode(
            [MarshalAs(UnmanagedType.Bool)] bool fPreview
            );

        [PreserveSig]
        int GetPreviewMode(
            [MarshalAs(UnmanagedType.Bool)] out bool pfPreview
            );

        [PreserveSig]
        int SetMediaTypeForVB(
            [In] int Val
            );

        [PreserveSig]
        int GetOutputBuffering(
            out int pnBuffer
            );

        [PreserveSig]
        int SetOutputBuffering(
            [In] int nBuffer
            );

        [PreserveSig]
        int SetSmartRecompressFormat(
            SCompFmt0 pFormat
            );

        [PreserveSig]
        int GetSmartRecompressFormat(
            out SCompFmt0 ppFormat
            );

        [PreserveSig]
        int IsSmartRecompressFormatSet(
            [MarshalAs(UnmanagedType.Bool)] out bool pVal
            );

        [PreserveSig]
        int IsRecompressFormatDirty(
            [MarshalAs(UnmanagedType.Bool)] out bool pVal
            );

        [PreserveSig]
        int ClearRecompressFormatDirty();

        [PreserveSig]
        int SetRecompFormatFromSource(
            IAMTimelineSrc pSource
            );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
Guid("78530B77-61F9-11D2-8CAD-00A024580902"),
InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMTimelineObj
    {
        [PreserveSig]
        int GetStartStop(
            out long pStart,
            out long pStop
            );

        [PreserveSig]
        int GetStartStop2(
            out double pStart,
            out double pStop
            );

        [PreserveSig]
        int FixTimes(
            ref long pStart,
            ref long pStop
            );

        [PreserveSig]
        int FixTimes2(
            ref double pStart,
            ref double pStop
            );

        [PreserveSig]
        int SetStartStop(
            long Start,
            long Stop
            );

        [PreserveSig]
        int SetStartStop2(
            double Start,
            double Stop
            );

        [PreserveSig]
        int GetPropertySetter(
            out IPropertySetter pVal
            );

        [PreserveSig]
        int SetPropertySetter(
            IPropertySetter newVal
            );

        [PreserveSig]
        int GetSubObject(
            [MarshalAs(UnmanagedType.IUnknown)] out object pVal
            );

        [PreserveSig]
        int SetSubObject(
            [In, MarshalAs(UnmanagedType.IUnknown)] object newVal
            );

        [PreserveSig]
        int SetSubObjectGUID(
            Guid newVal
            );

        [PreserveSig]
        int SetSubObjectGUIDB(
            [MarshalAs(UnmanagedType.BStr)] string newVal
            );

        [PreserveSig]
        int GetSubObjectGUID(
            out Guid pVal
            );

        [PreserveSig]
        int GetSubObjectGUIDB(
            [MarshalAs(UnmanagedType.BStr)] out string pVal
            );

        [PreserveSig]
        int GetSubObjectLoaded(
            [MarshalAs(UnmanagedType.Bool)] out bool pVal
            );

        [PreserveSig]
        int GetTimelineType(
            out TimelineMajorType pVal
            );

        [PreserveSig]
        int SetTimelineType(
            TimelineMajorType newVal
            );

        [PreserveSig]
        int GetUserID(
            out int pVal
            );

        [PreserveSig]
        int SetUserID(
            int newVal
            );

        [PreserveSig]
        int GetGenID(
            out int pVal
            );

        [PreserveSig]
        int GetUserName(
            [MarshalAs(UnmanagedType.BStr)] out string pVal
            );

        [PreserveSig]
        int SetUserName(
            [MarshalAs(UnmanagedType.BStr)] string newVal
            );

        [PreserveSig]
        int GetUserData(
            IntPtr pData,
            out int pSize
            );

        [PreserveSig]
        int SetUserData(
            IntPtr pData,
            int Size
            );

        [PreserveSig]
        int GetMuted(
            [MarshalAs(UnmanagedType.Bool)] out bool pVal
            );

        [PreserveSig]
        int SetMuted(
            [MarshalAs(UnmanagedType.Bool)] bool newVal
            );

        [PreserveSig]
        int GetLocked(
            [MarshalAs(UnmanagedType.Bool)] out bool pVal
            );

        [PreserveSig]
        int SetLocked(
            [MarshalAs(UnmanagedType.Bool)] bool newVal
            );

        [PreserveSig]
        int GetDirtyRange(
            out long pStart,
            out long pStop
            );

        [PreserveSig]
        int GetDirtyRange2(
            out double pStart,
            out double pStop
            );

        [PreserveSig]
        int SetDirtyRange(
            long Start,
            long Stop
            );

        [PreserveSig]
        int SetDirtyRange2(
            double Start,
            double Stop
            );

        [PreserveSig]
        int ClearDirty();

        [PreserveSig]
        int Remove();

        [PreserveSig]
        int RemoveAll();

        [PreserveSig]
        int GetTimelineNoRef(
            out IAMTimeline ppResult
            );

        [PreserveSig]
        int GetGroupIBelongTo(
            out IAMTimelineGroup ppGroup
            );

        [PreserveSig]
        int GetEmbedDepth(
            out int pVal
            );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
Guid("A0F840A0-D590-11D2-8D55-00A0C9441E20")]
    public interface IAMTimelineSplittable
    {
        [PreserveSig]
        int SplitAt(
            long Time
            );

        [PreserveSig]
        int SplitAt2(
            double Time
            );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
Guid("78530B79-61F9-11D2-8CAD-00A024580902"),
InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMTimelineSrc
    {
        [PreserveSig]
        int GetMediaTimes(
            out long pStart,
            out long pStop
            );

        [PreserveSig]
        int GetMediaTimes2(
            out double pStart,
            out double pStop
            );

        [PreserveSig]
        int ModifyStopTime(
            long Stop
            );

        [PreserveSig]
        int ModifyStopTime2(
            double Stop
            );

        [PreserveSig]
        int FixMediaTimes(
            ref long pStart,
            ref long pStop
            );

        [PreserveSig]
        int FixMediaTimes2(
            ref double pStart,
            ref double pStop
            );

        [PreserveSig]
        int SetMediaTimes(
            long Start,
            long Stop
            );

        [PreserveSig]
        int SetMediaTimes2(
            double Start,
            double Stop
            );

        [PreserveSig]
        int SetMediaLength(
            long Length
            );

        [PreserveSig]
        int SetMediaLength2(
            double Length
            );

        [PreserveSig]
        int GetMediaLength(
            out long pLength
            );

        [PreserveSig]
        int GetMediaLength2(
            out double pLength
            );

        [PreserveSig]
        int GetMediaName(
            [MarshalAs(UnmanagedType.BStr)] out string pVal
            );

        [PreserveSig]
        int SetMediaName(
            [MarshalAs(UnmanagedType.BStr)] string newVal
            );

        [PreserveSig]
        int SpliceWithNext(
            IAMTimelineObj pNext
            );

        [PreserveSig]
        int GetStreamNumber(
            out int pVal
            );

        [PreserveSig]
        int SetStreamNumber(
            int Val
            );

        [PreserveSig]
        int IsNormalRate(
            [MarshalAs(UnmanagedType.Bool)] out bool pVal
            );

        [PreserveSig]
        int GetDefaultFPS(
            out double pFPS
            );

        [PreserveSig]
        int SetDefaultFPS(
            double FPS
            );

        [PreserveSig]
        int GetStretchMode(
            out ResizeFlags pnStretchMode
            );

        [PreserveSig]
        int SetStretchMode(
            ResizeFlags nStretchMode
            );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("EAE58538-622E-11D2-8CAD-00A024580902"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMTimelineTrack
    {
        [PreserveSig]
        int SrcAdd(
            IAMTimelineObj pSource
            );

        [PreserveSig]
        int GetNextSrc(
            out IAMTimelineObj ppSrc,
            ref long pInOut
            );

        [PreserveSig]
        int GetNextSrc2(
            out IAMTimelineObj ppSrc,
            ref double pInOut
            );

        [PreserveSig]
        int MoveEverythingBy(
            long Start,
            long MoveBy
            );

        [PreserveSig]
        int MoveEverythingBy2(
            double Start,
            double MoveBy
            );

        [PreserveSig]
        int GetSourcesCount(
            out int pVal
            );

        [PreserveSig]
        int AreYouBlank(
            [MarshalAs(UnmanagedType.Bool)] out bool pVal
            );

        [PreserveSig]
        int GetSrcAtTime(
            out IAMTimelineObj ppSrc,
            long Time,
            DexterFTrackSearchFlags SearchDirection
            );

        [PreserveSig]
        int GetSrcAtTime2(
            out IAMTimelineObj ppSrc,
            double Time,
            DexterFTrackSearchFlags SearchDirection
            );

        [PreserveSig]
        int InsertSpace(
            long rtStart,
            long rtEnd
            );

        [PreserveSig]
        int InsertSpace2(
            double rtStart,
            double rtEnd
            );

        [PreserveSig]
        int ZeroBetween(
            long rtStart,
            long rtEnd
            );

        [PreserveSig]
        int ZeroBetween2(
            double rtStart,
            double rtEnd
            );

        [PreserveSig]
        int GetNextSrcEx(
            IAMTimelineObj pLast,
            out IAMTimelineObj ppNext
            );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    Guid("BCE0C265-622D-11D2-8CAD-00A024580902")]
    public interface IAMTimelineTrans
    {
        [PreserveSig]
        int GetCutPoint(
            out long pTLTime
            );

        [PreserveSig]
        int GetCutPoint2(
            out double pTLTime
            );

        [PreserveSig]
        int SetCutPoint(
            long TLTime
            );

        [PreserveSig]
        int SetCutPoint2(
            double TLTime
            );

        [PreserveSig]
        int GetSwapInputs(
            [MarshalAs(UnmanagedType.Bool)] out bool pVal
            );

        [PreserveSig]
        int SetSwapInputs(
            [MarshalAs(UnmanagedType.Bool)] bool pVal
            );

        [PreserveSig]
        int GetCutsOnly(
            [MarshalAs(UnmanagedType.Bool)] out bool pVal
            );

        [PreserveSig]
        int SetCutsOnly(
            [MarshalAs(UnmanagedType.Bool)] bool pVal
            );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("378FA386-622E-11D2-8CAD-00A024580902"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMTimelineTransable
    {
        [PreserveSig]
        int TransAdd(
            IAMTimelineObj pTrans
            );

        [PreserveSig]
        int TransGetCount(
            out int pCount
            );

        [PreserveSig]
        int GetNextTrans(
            out IAMTimelineObj ppTrans,
            ref long pInOut
            );

        [PreserveSig]
        int GetNextTrans2(
            out IAMTimelineObj ppTrans,
            ref double pInOut
            );

        [PreserveSig]
        int GetTransAtTime(
            out IAMTimelineObj ppObj,
            long Time,
            DexterFTrackSearchFlags SearchDirection
            );

        [PreserveSig]
        int GetTransAtTime2(
            out IAMTimelineObj ppObj,
            double Time,
            DexterFTrackSearchFlags SearchDirection
            );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("A8ED5F80-C2C7-11D2-8D39-00A0C9441E20"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMTimelineVirtualTrack
    {
        [PreserveSig]
        int TrackGetPriority(
            out int pPriority
            );

        [PreserveSig]
        int SetTrackDirty();
    }

    // IBaseFilter interface
    //
    // The IBaseFilter interface provides methods for controlling a filter
    //
    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("56a86895-0ad4-11ce-b03a-0020af0ba770"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IBaseFilter : IMediaFilter
    {
        #region IPersist Methods

        [PreserveSig]
        new int GetClassID([Out] out Guid pClassID);

        #endregion

        #region IMediaFilter Methods

        [PreserveSig]
        new int Stop();

        [PreserveSig]
        new int Pause();

        [PreserveSig]
        new int Run(long tStart);

        [PreserveSig]
        new int GetState([In] int dwMilliSecsTimeout, [Out] out FilterState filtState);

        [PreserveSig]
        new int SetSyncSource([In] IReferenceClock pClock);

        [PreserveSig]
        new int GetSyncSource([Out] out IReferenceClock pClock);

        #endregion

        [PreserveSig]
        int EnumPins([Out] out IEnumPins ppEnum);

        [PreserveSig]
        int FindPin([In, MarshalAs(UnmanagedType.LPWStr)] string Id, [Out] out IPin ppPin);

        [PreserveSig]
        int QueryFilterInfo([Out] out FilterInfo pInfo);

        [PreserveSig]
        int JoinFilterGraph([In] IFilterGraph pGraph, [In, MarshalAs(UnmanagedType.LPWStr)] string pName);

        [PreserveSig]
        int QueryVendorInfo([Out, MarshalAs(UnmanagedType.LPWStr)] out string pVendorInfo);
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
Guid("E31FB81B-1335-11D1-8189-0000F87557DB")]
    public interface IDXEffect
    {
        [PreserveSig]
        int get_Capabilities(
            out int pVal
            );

        [PreserveSig]
        int get_Progress(
            out float pVal
            );

        [PreserveSig]
        int put_Progress(
            float newVal
            );

        [PreserveSig]
        int get_StepResolution(
            out float pVal
            );

        [PreserveSig]
        int get_Duration(
            out float pVal
            );

        [PreserveSig]
        int put_Duration(
            float newVal
            );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
Guid("4EE9EAD9-DA4D-43D0-9383-06B90C08B12B"),
InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDxtAlphaSetter : IDXEffect
    {
        #region IDXEffect Methods

        [PreserveSig]
        new int get_Capabilities(
            out int pVal
            );

        [PreserveSig]
        new int get_Progress(
            out float pVal
            );

        [PreserveSig]
        new int put_Progress(
            float newVal
            );

        [PreserveSig]
        new int get_StepResolution(
            out float pVal
            );

        [PreserveSig]
        new int get_Duration(
            out float pVal
            );

        [PreserveSig]
        new int put_Duration(
            float newVal
            );

        #endregion

        [PreserveSig]
        int get_Alpha(
            out int pVal
            );

        [PreserveSig]
        int put_Alpha(
            int newVal
            );

        [PreserveSig]
        int get_AlphaRamp(
            out double pVal
            );

        [PreserveSig]
        int put_AlphaRamp(
            double newVal
            );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
Guid("BB44391E-6ABD-422F-9E2E-385C9DFF51FC"),
InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDxtCompositor : IDXEffect
    {
        #region IDXEffect

        [PreserveSig]
        new int get_Capabilities(
            out int pVal
            );

        [PreserveSig]
        new int get_Progress(
            out float pVal
            );

        [PreserveSig]
        new int put_Progress(
            float newVal
            );

        [PreserveSig]
        new int get_StepResolution(
            out float pVal
            );

        [PreserveSig]
        new int get_Duration(
            out float pVal
            );

        [PreserveSig]
        new int put_Duration(
            float newVal
            );

        #endregion

        [PreserveSig]
        int get_OffsetX(
            out int pVal
            );

        [PreserveSig]
        int put_OffsetX(
            int newVal
            );

        [PreserveSig]
        int get_OffsetY(
            out int pVal
            );

        [PreserveSig]
        int put_OffsetY(
            int newVal
            );

        [PreserveSig]
        int get_Width(
            out int pVal
            );

        [PreserveSig]
        int put_Width(
            int newVal
            );

        [PreserveSig]
        int get_Height(
            out int pVal
            );

        [PreserveSig]
        int put_Height(
            int newVal
            );

        [PreserveSig]
        int get_SrcOffsetX(
            out int pVal
            );

        [PreserveSig]
        int put_SrcOffsetX(
            int newVal
            );

        [PreserveSig]
        int get_SrcOffsetY(
            out int pVal
            );

        [PreserveSig]
        int put_SrcOffsetY(
            int newVal
            );

        [PreserveSig]
        int get_SrcWidth(
            out int pVal
            );

        [PreserveSig]
        int put_SrcWidth(
            int newVal
            );

        [PreserveSig]
        int get_SrcHeight(
            out int pVal
            );

        [PreserveSig]
        int put_SrcHeight(
            int newVal
            );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("DE75D011-7A65-11D2-8CEA-00A0C9441E20"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDxtJpeg : IDXEffect
    {
        #region IDXEffect

        [PreserveSig]
        new int get_Capabilities(
            out int pVal
            );

        [PreserveSig]
        new int get_Progress(
            out float pVal
            );

        [PreserveSig]
        new int put_Progress(
            float newVal
            );

        [PreserveSig]
        new int get_StepResolution(
            out float pVal
            );

        [PreserveSig]
        new int get_Duration(
            out float pVal
            );

        [PreserveSig]
        new int put_Duration(
            float newVal
            );

        #endregion

        [PreserveSig]
        int get_MaskNum(
            out int MIDL_0018
            );

        [PreserveSig]
        int put_MaskNum(
            int MIDL_0019
            );

        [PreserveSig]
        int get_MaskName(
            [MarshalAs(UnmanagedType.BStr)] out string pVal
            );

        [PreserveSig]
        int put_MaskName(
            [MarshalAs(UnmanagedType.BStr)] string newVal
            );

        [PreserveSig]
        int get_ScaleX(
            out double MIDL_0020
            );

        [PreserveSig]
        int put_ScaleX(
            double MIDL_0021
            );

        [PreserveSig]
        int get_ScaleY(
            out double MIDL_0022
            );

        [PreserveSig]
        int put_ScaleY(
            double MIDL_0023
            );

        [PreserveSig]
        int get_OffsetX(
            out int MIDL_0024
            );

        [PreserveSig]
        int put_OffsetX(
            int MIDL_0025
            );

        [PreserveSig]
        int get_OffsetY(
            out int MIDL_0026
            );

        [PreserveSig]
        int put_OffsetY(
            int MIDL_0027
            );

        [PreserveSig]
        int get_ReplicateX(
            out int pVal
            );

        [PreserveSig]
        int put_ReplicateX(
            int newVal
            );

        [PreserveSig]
        int get_ReplicateY(
            out int pVal
            );

        [PreserveSig]
        int put_ReplicateY(
            int newVal
            );

        [PreserveSig]
        int get_BorderColor(
            out int pVal
            );

        [PreserveSig]
        int put_BorderColor(
            int newVal
            );

        [PreserveSig]
        int get_BorderWidth(
            out int pVal
            );

        [PreserveSig]
        int put_BorderWidth(
            int newVal
            );

        [PreserveSig]
        int get_BorderSoftness(
            out int pVal
            );

        [PreserveSig]
        int put_BorderSoftness(
            int newVal
            );

        [PreserveSig]
        int ApplyChanges();

        [PreserveSig]
        int LoadDefSettings();
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
Guid("3255DE56-38FB-4901-B980-94B438010D7B"),
InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDxtKey : IDXEffect
    {
        #region IDXEffect

        [PreserveSig]
        new int get_Capabilities(
            out int pVal
            );

        [PreserveSig]
        new int get_Progress(
            out float pVal
            );

        [PreserveSig]
        new int put_Progress(
            float newVal
            );

        [PreserveSig]
        new int get_StepResolution(
            out float pVal
            );

        [PreserveSig]
        new int get_Duration(
            out float pVal
            );

        [PreserveSig]
        new int put_Duration(
            float newVal
            );

        #endregion

        [PreserveSig]
        int get_KeyType(
            out int MIDL_0028
            );

        [PreserveSig]
        int put_KeyType(
            int MIDL_0029
            );

        [PreserveSig]
        int get_Hue(
            out int MIDL_0030
            );

        [PreserveSig]
        int put_Hue(
            int MIDL_0031
            );

        [PreserveSig]
        int get_Luminance(
            out int MIDL_0032
            );

        [PreserveSig]
        int put_Luminance(
            int MIDL_0033
            );

        [PreserveSig]
        int get_RGB(
            out int MIDL_0034
            );

        [PreserveSig]
        int put_RGB(
            int MIDL_0035
            );

        [PreserveSig]
        int get_Similarity(
            out int MIDL_0036
            );

        [PreserveSig]
        int put_Similarity(
            int MIDL_0037
            );

        [PreserveSig]
        int get_Invert(
            [MarshalAs(UnmanagedType.Bool)] out bool MIDL_0038
            );

        [PreserveSig]
        int put_Invert(
            [MarshalAs(UnmanagedType.Bool)] bool MIDL_0039
            );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
Guid("56a86893-0ad4-11ce-b03a-0020af0ba770"),
InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IEnumFilters
    {
        [PreserveSig]
        int Next(
            [In] int cFilters,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] IBaseFilter[] ppFilter,
            [In] IntPtr pcFetched
            );

        [PreserveSig]
        int Skip([In] int cFilters);

        [PreserveSig]
        int Reset();

        [PreserveSig]
        int Clone([Out] out IEnumFilters ppEnum);
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
Guid("89c31040-846b-11ce-97d3-00aa0055595a"),
InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IEnumMediaTypes
    {
        [PreserveSig]
        int Next(
            [In] int cMediaTypes,
            [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(EMTMarshaler), SizeParamIndex = 0)] AMMediaType[] ppMediaTypes,
            [In] IntPtr pcFetched
            );

        [PreserveSig]
        int Skip([In] int cMediaTypes);

        [PreserveSig]
        int Reset();

        [PreserveSig]
        int Clone([Out] out IEnumMediaTypes ppEnum);
    }

    // IEnumPins interface
    //
    // Enumerates pins on a filter
    //
    [ComImport,
    Guid("56A86892-0AD4-11CE-B03A-0020AF0BA770"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IEnumPins
    {
        // Retrieves a specified number of pins
        [PreserveSig]
        int Next(
            [In] int cPins,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] IPin[] ppPins,
            [Out] out int pcFetched);

        // Skips over a specified number of pins
        [PreserveSig]
        int Skip(
            [In] int cPins);

        // Resets the enumeration sequence to the beginning
        [PreserveSig]
        int Reset();

        // Makes a copy of the enumerator with the same enumeration state
        [PreserveSig]
        void Clone(
            [Out] out IEnumPins ppEnum);
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
Guid("3127CA40-446E-11CE-8135-00AA004BB851"),
InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IErrorLog
    {
        [PreserveSig]
        int AddError(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pszPropName,
            [In] System.Runtime.InteropServices.ComTypes.EXCEPINFO pExcepInfo);
    }

    // IFilterGraph interface
    //
    // The IFilterGraph interface is an abstraction representing
    // a graph of filters
    //
    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("56a8689f-0ad4-11ce-b03a-0020af0ba770"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IFilterGraph
    {
        [PreserveSig]
        int AddFilter(
            [In] IBaseFilter pFilter,
            [In, MarshalAs(UnmanagedType.LPWStr)] string pName
            );

        [PreserveSig]
        int RemoveFilter([In] IBaseFilter pFilter);

        [PreserveSig]
        int EnumFilters([Out] out IEnumFilters ppEnum);

        [PreserveSig]
        int FindFilterByName(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pName,
            [Out] out IBaseFilter ppFilter
            );

        [PreserveSig]
        int ConnectDirect(
            [In] IPin ppinOut,
            [In] IPin ppinIn,
            [In, MarshalAs(UnmanagedType.LPStruct)] AMMediaType pmt
            );

        [PreserveSig]
        [Obsolete("This method is obsolete; use the IFilterGraph2.ReconnectEx method instead.")]
        int Reconnect([In] IPin ppin);

        [PreserveSig]
        int Disconnect([In] IPin ppin);

        [PreserveSig]
        int SetDefaultSyncSource();
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
Guid("F03FA8DE-879A-4D59-9B2C-26BB1CF83461")]
    public interface IFindCompressorCB
    {
        [PreserveSig]
        int GetCompressor(
            [MarshalAs(UnmanagedType.LPStruct)] AMMediaType pType,
            [MarshalAs(UnmanagedType.LPStruct)] AMMediaType pCompType,
            out IBaseFilter ppFilter
            );
    }

    // IGraphBuilder interface
    //
    // The IGraphBuilder interface allows applications to call upon
    // the filter graph manager to attempt to build a complete filter
    // graph, or parts of a filter graph given only partial information,
    // such as the name of a file or the interfaces of two separate pins
    //
    [ComImport,
    Guid("56A868A9-0AD4-11CE-B03A-0020AF0BA770"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IGraphBuilder
    {
        // --- IFilterGraph Methods

        // Adds a filter to the graph and names it
        // by using the pName parameter
        [PreserveSig]
        int AddFilter(
            [In] IBaseFilter pFilter,
            [In, MarshalAs(UnmanagedType.LPWStr)] string pName);

        // Removes a filter from the graph
        [PreserveSig]
        int RemoveFilter([In] IBaseFilter pFilter);

        // Provides an enumerator for all filters in the graph
        [PreserveSig]
        //		int EnumFilters(
        //			[Out] out IEnumFilters ppEnum);
        int EnumFilters([Out] out IntPtr ppEnum);

        // Finds a filter that was added
        // to the filter graph with a specific name
        [PreserveSig]
        int FindFilterByName([In, MarshalAs(UnmanagedType.LPWStr)] string pName, [Out] out IBaseFilter ppFilter);

        // Connects the two pins directly
        [PreserveSig]
        int ConnectDirect(
            [In] IPin ppinOut,
            [In] IPin ppinIn,
            [In, MarshalAs(UnmanagedType.LPStruct)] AMMediaType pmt);

        // Disconnects this and the pin to which it connects and
        // then reconnects it to the same pin
        [PreserveSig]
        int Reconnect([In] IPin ppin);

        // Disconnects this pin
        [PreserveSig]
        int Disconnect([In] IPin ppin);

        // Sets the default source of synchronization
        [PreserveSig]
        int SetDefaultSyncSource();

        // --- IGraphBuilder methods

        // Connects the two pins, using intermediates if necessary
        [PreserveSig]
        int Connect([In] IPin ppinOut, [In] IPin ppinIn);

        // Builds a filter graph that renders the data from this output pin
        [PreserveSig]
        int Render([In] IPin ppinOut);

        // Builds a filter graph that renders the specified file
        [PreserveSig]
        int RenderFile(
            [In, MarshalAs(UnmanagedType.LPWStr)] string lpcwstrFile,
            [In, MarshalAs(UnmanagedType.LPWStr)] string lpcwstrPlayList);

        // Adds a source filter to the filter graph for a specific file
        [PreserveSig]
        int AddSourceFilter(
            [In, MarshalAs(UnmanagedType.LPWStr)] string lpcwstrFileName,
            [In, MarshalAs(UnmanagedType.LPWStr)] string lpcwstrFilterName,
            [Out] out IBaseFilter ppFilter);

        // Sets the file into which actions taken in attempting
        // to perform an operation are logged
        [PreserveSig]
        int SetLogFile(IntPtr hFile);

        //
        [PreserveSig]
        int Abort();

        //
        [PreserveSig]
        int ShouldOperationContinue();
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
InterfaceType(ComInterfaceType.InterfaceIsDual),
Guid("AE9472BE-B0C3-11D2-8D24-00A0C9441E20")]
    public interface IGrfCache
    {
        [PreserveSig]
        int AddFilter(IGrfCache ChainedCache, long Id, IBaseFilter pFilter, [MarshalAs(UnmanagedType.LPWStr)] string pName);

        [PreserveSig]
        int ConnectPins(IGrfCache ChainedCache, long PinID1, IPin pPin1, long PinID2, IPin pPin2);

        [PreserveSig]
        int SetGraph(IGraphBuilder pGraph);

        [PreserveSig]
        int DoConnectionsNow();
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    Guid("65BD0710-24D2-4ff7-9324-ED2E5D3ABAFA")]
    public interface IMediaDet
    {
        [PreserveSig]
        int get_Filter(
            [MarshalAs(UnmanagedType.IUnknown)] out object pVal
            );

        [PreserveSig]
        int put_Filter(
            [MarshalAs(UnmanagedType.IUnknown)] object newVal
            );

        [PreserveSig]
        int get_OutputStreams(
            out int pVal
            );

        [PreserveSig]
        int get_CurrentStream(
            out int pVal
            );

        [PreserveSig]
        int put_CurrentStream(
            int newVal
            );

        [PreserveSig]
        int get_StreamType(
            out Guid pVal
            );

        [PreserveSig]
        int get_StreamTypeB(
            [MarshalAs(UnmanagedType.BStr)] out string pVal
            );

        [PreserveSig]
        int get_StreamLength(
            out double pVal
            );

        [PreserveSig]
        int get_Filename(
            [MarshalAs(UnmanagedType.BStr)] out string pVal
            );

        [PreserveSig]
        int put_Filename(
            [MarshalAs(UnmanagedType.BStr)] string newVal
            );

        [PreserveSig]
        int GetBitmapBits(
            double StreamTime,
            out int pBufferSize,
            [In] IntPtr pBuffer,
            int Width,
            int Height
            );

        [PreserveSig]
        int WriteBitmapBits(
            double StreamTime,
            int Width,
            int Height,
            [In, MarshalAs(UnmanagedType.BStr)] string Filename);

        [PreserveSig]
        int get_StreamMediaType(
            [Out, MarshalAs(UnmanagedType.LPStruct)] AMMediaType pVal);

        [PreserveSig]
        int GetSampleGrabber(
            out ISampleGrabber ppVal);

        [PreserveSig]
        int get_FrameRate(
            out double pVal);

        [PreserveSig]
        int EnterBitmapGrabMode(
            double SeekTime);
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("56a86899-0ad4-11ce-b03a-0020af0ba770"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMediaFilter : IPersist
    {
        #region IPersist Methods

        [PreserveSig]
        new int GetClassID(
            [Out] out Guid pClassID);

        #endregion

        [PreserveSig]
        int Stop();

        [PreserveSig]
        int Pause();

        [PreserveSig]
        int Run([In] long tStart);

        [PreserveSig]
        int GetState(
            [In] int dwMilliSecsTimeout,
            [Out] out FilterState filtState
            );

        [PreserveSig]
        int SetSyncSource([In] IReferenceClock pClock);

        [PreserveSig]
        int GetSyncSource([Out] out IReferenceClock pClock);
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    Guid("288581E0-66CE-11D2-918F-00C0DF10D434")]
    public interface IMediaLocator
    {
        [PreserveSig]
        int FindMediaFile(
            [MarshalAs(UnmanagedType.BStr)] string Input,
            [MarshalAs(UnmanagedType.BStr)] string FilterString,
            [MarshalAs(UnmanagedType.BStr)] out string pOutput,
            SFNValidateFlags Flags
            );

        [PreserveSig]
        int AddFoundLocation(
            [MarshalAs(UnmanagedType.BStr)] string DirectoryName
            );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
Guid("56a8689a-0ad4-11ce-b03a-0020af0ba770"),
InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMediaSample
    {
        [PreserveSig]
        int GetPointer([Out] out IntPtr ppBuffer); // BYTE **

        [PreserveSig]
        int GetSize();

        [PreserveSig]
        int GetTime(
            [Out] out long pTimeStart,
            [Out] out long pTimeEnd
            );

        [PreserveSig]
        int SetTime(
            [In, MarshalAs(UnmanagedType.LPStruct)] DsLong pTimeStart,
            [In, MarshalAs(UnmanagedType.LPStruct)] DsLong pTimeEnd
            );

        [PreserveSig]
        int IsSyncPoint();

        [PreserveSig]
        int SetSyncPoint([In, MarshalAs(UnmanagedType.Bool)] bool bIsSyncPoint);

        [PreserveSig]
        int IsPreroll();

        [PreserveSig]
        int SetPreroll([In, MarshalAs(UnmanagedType.Bool)] bool bIsPreroll);

        [PreserveSig]
        int GetActualDataLength();

        [PreserveSig]
        int SetActualDataLength([In] int len);

        /// <summary>
        /// Returned object must be released with DsUtils.FreeAMMediaType()
        /// </summary>
        [PreserveSig]
        int GetMediaType([Out, MarshalAs(UnmanagedType.LPStruct)] out AMMediaType ppMediaType);

        [PreserveSig]
        int SetMediaType([In, MarshalAs(UnmanagedType.LPStruct)] AMMediaType pMediaType);

        [PreserveSig]
        int IsDiscontinuity();

        [PreserveSig]
        int SetDiscontinuity([In, MarshalAs(UnmanagedType.Bool)] bool bDiscontinuity);

        [PreserveSig]
        int GetMediaTime(
            [Out] out long pTimeStart,
            [Out] out long pTimeEnd
            );

        [PreserveSig]
        int SetMediaTime(
            [In, MarshalAs(UnmanagedType.LPStruct)] DsLong pTimeStart,
            [In, MarshalAs(UnmanagedType.LPStruct)] DsLong pTimeEnd
            );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("0000010c-0000-0000-C000-000000000046"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPersist
    {
        [PreserveSig]
        int GetClassID([Out] out Guid pClassID);
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
Guid("00000109-0000-0000-C000-000000000046"),
InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPersistStream : IPersist
    {
        #region IPersist Methods

        [PreserveSig]
        new int GetClassID([Out] out Guid pClassID);

        #endregion

        [PreserveSig]
        int IsDirty();

        [PreserveSig]
        int Load([In] IStream pStm);

        [PreserveSig]
        int Save(
        [In] IStream pStm,
 [In, MarshalAs(UnmanagedType.Bool)] bool fClearDirty);

        [PreserveSig]
        int GetSizeMax([Out] out long pcbSize);
    }

    // IPin interface
    //
    // The IPin interface represents a single, unidirectional
    // connection point on a filter
    //
    [ComImport,
    Guid("56A86891-0AD4-11CE-B03A-0020AF0BA770"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPin
    {
        // Connects the pin to another pin
        [PreserveSig]
        int Connect([In] IPin pReceivePin, [In, MarshalAs(UnmanagedType.LPStruct)] AMMediaType pmt);

        // Accepts a connection from another pin
        [PreserveSig]
        int ReceiveConnection(
            [In] IPin pReceivePin,
            [In, MarshalAs(UnmanagedType.LPStruct)] AMMediaType pmt);

        // Breaks the current pin connection
        [PreserveSig]
        int Disconnect();

        // Retrieves the pin connected to this pin
        [PreserveSig]
        int ConnectedTo(
            [Out] out IPin ppPin);

        // Retrieves the media type for the current pin connection
        [PreserveSig]
        int ConnectionMediaType(
            [Out, MarshalAs(UnmanagedType.LPStruct)] AMMediaType pmt);

        // Retrieves information about the pin, such as the name,
        // the owning filter, and the direction
        [PreserveSig]
        int QueryPinInfo(
            [Out, MarshalAs(UnmanagedType.LPStruct)] PinInfo pInfo);

        // Retrieves the direction of the pin (input or output)
        [PreserveSig]
        int QueryDirection(
            out PinDirection pPinDir);

        // Retrieves the pin identifier
        [PreserveSig]
        int QueryId(
            [Out, MarshalAs(UnmanagedType.LPWStr)] out string Id);

        // Determines whether the pin accepts a specified media type
        [PreserveSig]
        int QueryAccept(
            [In, MarshalAs(UnmanagedType.LPStruct)] AMMediaType pmt);

        // Enumerates the pin's preferred media types
        [PreserveSig]
        int EnumMediaTypes(
            IntPtr ppEnum);

        // Retrieves the pins that are connected
        // internally to this pin (within the filter)
        [PreserveSig]
        int QueryInternalConnections(
            IntPtr apPin,
            [In, Out] ref int nPin);

        // Notifies the pin that no additional data is expected
        [PreserveSig]
        int EndOfStream();

        // Begins a flush operation
        [PreserveSig]
        int BeginFlush();

        // Ends a flush operation
        [PreserveSig]
        int EndFlush();

        // Notifies the pin that media samples received after
        // this call are grouped as a segment
        [PreserveSig]
        int NewSegment(
            long tStart,
            long tStop,
            double dRate);
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
Guid("55272A00-42CB-11CE-8135-00AA004BB851"),
InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPropertyBag
    {
        [PreserveSig]
        int Read(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pszPropName,
            [Out, MarshalAs(UnmanagedType.Struct)] out object pVar,
            [In] IErrorLog pErrorLog
            );

        [PreserveSig]
        int Write(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pszPropName,
            [In, MarshalAs(UnmanagedType.Struct)] ref object pVar
            );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    Guid("AE9472BD-B0C3-11D2-8D24-00A0C9441E20")]
    public interface IPropertySetter
    {
        [PreserveSig]
        int LoadXML(
            [In, MarshalAs(UnmanagedType.IUnknown)] object pxml
            );

        [PreserveSig]
        int PrintXML(
            [Out, MarshalAs(UnmanagedType.LPStr)] StringBuilder pszXML,
            [In] int cbXML,
            out int pcbPrinted,
            [In] int indent
            );

        [PreserveSig]
        int CloneProps(
            out IPropertySetter ppSetter,
            [In] long rtStart,
            [In] long rtStop
            );

        [PreserveSig]
        int AddProp(
            [In] DexterParam Param,
            [In, MarshalAs(UnmanagedType.LPArray)] DexterValue[] paValue
            );

        [PreserveSig]
        int GetProps(
            out int pcParams,
            out IntPtr paParam,
            out IntPtr paValue
            );

        [PreserveSig]
        int FreeProps(
            [In] int cParams,
            [In] IntPtr paParam,
            [In] IntPtr paValue
            );

        [PreserveSig]
        int ClearProps();

        [PreserveSig]
        int SaveToBlob(
            out int pcSize,
            out IntPtr ppb
            );

        [PreserveSig]
        int LoadFromBlob(
            [In] int cSize,
            [In] IntPtr pb
            );

        [PreserveSig]
        int SetProps(
            [In, MarshalAs(UnmanagedType.IUnknown)] object pTarget,
            [In] long rtNow
            );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
Guid("56a86897-0ad4-11ce-b03a-0020af0ba770"),
InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IReferenceClock
    {
        [PreserveSig]
        int GetTime([Out] out long pTime);

        [PreserveSig]
        int AdviseTime(
            [In] long baseTime,
            [In] long streamTime,
            [In] IntPtr hEvent, // System.Threading.WaitHandle?
            [Out] out int pdwAdviseCookie
            );

        [PreserveSig]
        int AdvisePeriodic(
            [In] long startTime,
            [In] long periodTime,
            [In] IntPtr hSemaphore, // System.Threading.WaitHandle?
            [Out] out int pdwAdviseCookie
            );

        [PreserveSig]
        int Unadvise([In] int dwAdviseCookie);
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    Guid("6BEE3A81-66C9-11D2-918F-00C0DF10D434")]
    public interface IRenderEngine
    {
        [PreserveSig]
        int SetTimelineObject(
            IAMTimeline pTimeline
            );

        [PreserveSig]
        int GetTimelineObject(
            out IAMTimeline ppTimeline
            );

        [PreserveSig]
        int GetFilterGraph(
            out IGraphBuilder ppFG
            );

        [PreserveSig]
        int SetFilterGraph(
            IGraphBuilder pFG
            );

        [PreserveSig]
        int SetInterestRange(
            long Start,
            long Stop
            );

        [PreserveSig]
        int SetInterestRange2(
            double Start,
            double Stop
            );

        [PreserveSig]
        int SetRenderRange(
            long Start,
            long Stop
            );

        [PreserveSig]
        int SetRenderRange2(
            double Start,
            double Stop
            );

        [PreserveSig]
        int GetGroupOutputPin(
            int Group,
            out IPin ppRenderPin
            );

        [PreserveSig]
        int ScrapIt();

        [PreserveSig]
        int RenderOutputPins();

        [PreserveSig]
        int GetVendorString(
            [MarshalAs(UnmanagedType.BStr)] out string sVendor
            );

        [PreserveSig]
        int ConnectFrontEnd();

        [PreserveSig]
        int SetSourceConnectCallback(
#if ALLOW_UNTESTED_INTERFACES
            IGrfCache pCallback
#else
object pCallback
#endif
);

        [PreserveSig]
        int SetDynamicReconnectLevel(
            ConnectFDynamic Level
            );

        [PreserveSig]
        int DoSmartRecompression();

        [PreserveSig]
        int UseInSmartRecompressionGraph();

        [PreserveSig]
        int SetSourceNameValidation(
            [MarshalAs(UnmanagedType.BStr)] string FilterString,
            IMediaLocator pOverride,
            SFNValidateFlags Flags
            );

        [PreserveSig]
        int Commit();

        [PreserveSig]
        int Decommit();

        [PreserveSig]
        int GetCaps(
            int Index,
            out int pReturn
            );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    Guid("6BEE3A82-66C9-11d2-918F-00C0DF10D434")]
    public interface IRenderEngine2
    {
        [PreserveSig]
        int SetResizerGUID(
            [In] Guid ResizerGuid
            );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
Guid("4ada63a0-72d5-11d2-952a-0060081840bc")]
    public interface IResize
    {
        [PreserveSig]
        int get_Size(
            out int piHeight,
            out int piWidth,
            ResizeFlags pFlag
            );

        [PreserveSig]
        int get_InputSize(
            out int piHeight,
            out int piWidth
            );

        [PreserveSig]
        int put_Size(
            int Height,
            int Width,
            ResizeFlags Flag
            );

        [PreserveSig]
        int get_MediaType(
            [MarshalAs(UnmanagedType.LPStruct)] out AMMediaType pmt
            );

        [PreserveSig]
        int put_MediaType(
            [MarshalAs(UnmanagedType.LPStruct)] AMMediaType pmt
            );
    }

    // ISampleGrabber interface
    //
    // This interface provides methods for retrieving individual
    // media samples as they move through the filter graph
    //
    [ComImport,
    Guid("6B652FFF-11FE-4FCE-92AD-0266B5D7C78F"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ISampleGrabber
    {
        // Specifies whether the filter should stop the graph
        // after receiving one sample
        [PreserveSig]
        int SetOneShot([In, MarshalAs(UnmanagedType.Bool)] bool OneShot);

        // Specifies the media type for the connection on
        // the Sample Grabber's input pin
        [PreserveSig]
        int SetMediaType([In, MarshalAs(UnmanagedType.LPStruct)] AMMediaType pmt);

        // Retrieves the media type for the connection on
        // the Sample Grabber's input pin
        [PreserveSig]
        int GetConnectedMediaType([Out, MarshalAs(UnmanagedType.LPStruct)] AMMediaType pmt);

        // Specifies whether to copy sample data into a buffer
        // as it goes through the filter
        [PreserveSig]
        int SetBufferSamples([In, MarshalAs(UnmanagedType.Bool)] bool BufferThem);

        // Retrieves a copy of the sample that
        // the filter received most recently
        [PreserveSig]
        int GetCurrentBuffer(ref int pBufferSize, IntPtr pBuffer);

        //
        [PreserveSig]
        int GetCurrentSample(IntPtr ppSample);

        // Specifies a callback method to call on incoming samples
        [PreserveSig]
        int SetCallback(ISampleGrabberCB pCallback, int WhichMethodToCallback);
    }

    // ISampleGrabberCB interface
    //
    // This interface provides callback methods for the
    // ISampleGrabber::SetCallback method
    //
    [ComImport,
    Guid("0579154A-2B53-4994-B0D0-E773148EFF85"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ISampleGrabberCB
    {
        // Callback method that receives a pointer to the media sample
        [PreserveSig]
        //		int SampleCB(
        //			double SampleTime,
        //			IMediaSample pSample);
        int SampleCB(double SampleTime, IntPtr pSample);

        // Callback method that receives a pointer to the sample buffer
        [PreserveSig]
        int BufferCB(double SampleTime, IntPtr pBuffer, int BufferLen);
    }


    /// <summary>
    /// MISSING - ISequentialStream
    /// </summary>
    

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("F03FA8CE-879A-4D59-9B2C-26BB1CF83461"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ISmartRenderEngine
    {
        [PreserveSig]
        int SetGroupCompressor(
            int Group,
            IBaseFilter pCompressor
            );

        [PreserveSig]
        int GetGroupCompressor(
            int Group,
            out IBaseFilter pCompressor
            );

        [PreserveSig]
        int SetFindCompressorCB(
#if ALLOW_UNTESTED_INTERFACES
            IFindCompressorCB pCallback
#else
object pCallback
#endif
);
    }

    // Summary:
    //     Provides the managed definition of the IStream interface, with ISequentialStream
    //     functionality.
    [Guid("0000000c-0000-0000-C000-000000000046")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IStream
    {
        // Summary:
        //     Creates a new stream object with its own seek pointer that references the
        //     same bytes as the original stream.
        //
        // Parameters:
        //   ppstm:
        //     When this method returns, contains the new stream object. This parameter
        //     is passed uninitialized.
        void Clone(out IStream ppstm);
        //
        // Summary:
        //     Ensures that any changes made to a stream object that is open in transacted
        //     mode are reflected in the parent storage.
        //
        // Parameters:
        //   grfCommitFlags:
        //     A value that controls how the changes for the stream object are committed.
        void Commit(int grfCommitFlags);
        //
        // Summary:
        //     Copies a specified number of bytes from the current seek pointer in the stream
        //     to the current seek pointer in another stream.
        //
        // Parameters:
        //   pstm:
        //     A reference to the destination stream.
        //
        //   cb:
        //     The number of bytes to copy from the source stream.
        //
        //   pcbRead:
        //     On successful return, contains the actual number of bytes read from the source.
        //
        //   pcbWritten:
        //     On successful return, contains the actual number of bytes written to the
        //     destination.
        void CopyTo(IStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten);
        //
        // Summary:
        //     Restricts access to a specified range of bytes in the stream.
        //
        // Parameters:
        //   libOffset:
        //     The byte offset for the beginning of the range.
        //
        //   cb:
        //     The length of the range, in bytes, to restrict.
        //
        //   dwLockType:
        //     The requested restrictions on accessing the range.
        void LockRegion(long libOffset, long cb, int dwLockType);
        //
        // Summary:
        //     Reads a specified number of bytes from the stream object into memory starting
        //     at the current seek pointer.
        //
        // Parameters:
        //   pv:
        //     When this method returns, contains the data read from the stream. This parameter
        //     is passed uninitialized.
        //
        //   cb:
        //     The number of bytes to read from the stream object.
        //
        //   pcbRead:
        //     A pointer to a ULONG variable that receives the actual number of bytes read
        //     from the stream object.
        void Read(byte[] pv, int cb, IntPtr pcbRead);
        //
        // Summary:
        //     Discards all changes that have been made to a transacted stream since the
        //     last System.Runtime.InteropServices.ComTypes.IStream.Commit(System.Int32)
        //     call.
        void Revert();
        //
        // Summary:
        //     Changes the seek pointer to a new location relative to the beginning of the
        //     stream, to the end of the stream, or to the current seek pointer.
        //
        // Parameters:
        //   dlibMove:
        //     The displacement to add to dwOrigin.
        //
        //   dwOrigin:
        //     The origin of the seek. The origin can be the beginning of the file, the
        //     current seek pointer, or the end of the file.
        //
        //   plibNewPosition:
        //     On successful return, contains the offset of the seek pointer from the beginning
        //     of the stream.
        void Seek(long dlibMove, int dwOrigin, IntPtr plibNewPosition);
        //
        // Summary:
        //     Changes the size of the stream object.
        //
        // Parameters:
        //   libNewSize:
        //     The new size of the stream as a number of bytes.
        void SetSize(long libNewSize);
        //
        // Summary:
        //     Retrieves the System.Runtime.InteropServices.STATSTG structure for this stream.
        //
        // Parameters:
        //   pstatstg:
        //     When this method returns, contains a STATSTG structure that describes this
        //     stream object. This parameter is passed uninitialized.
        //
        //   grfStatFlag:
        //     Members in the STATSTG structure that this method does not return, thus saving
        //     some memory allocation operations.
        void Stat(out System.Runtime.InteropServices.ComTypes.STATSTG pstatstg, int grfStatFlag);
        //
        // Summary:
        //     Removes the access restriction on a range of bytes previously restricted
        //     with the System.Runtime.InteropServices.ComTypes.IStream.LockRegion(System.Int64,System.Int64,System.Int32)
        //     method.
        //
        // Parameters:
        //   libOffset:
        //     The byte offset for the beginning of the range.
        //
        //   cb:
        //     The length, in bytes, of the range to restrict.
        //
        //   dwLockType:
        //     The access restrictions previously placed on the range.
        void UnlockRegion(long libOffset, long cb, int dwLockType);
        //
        // Summary:
        //     Writes a specified number of bytes into the stream object starting at the
        //     current seek pointer.
        //
        // Parameters:
        //   pv:
        //     The buffer to write this stream to.
        //
        //   cb:
        //     The number of bytes to write to the stream.
        //
        //   pcbWritten:
        //     On successful return, contains the actual number of bytes written to the
        //     stream object. If the caller sets this pointer to System.IntPtr.Zero, this
        //     method does not provide the actual number of bytes written.
        void Write(byte[] pv, int cb, IntPtr pcbWritten);
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    InterfaceType(ComInterfaceType.InterfaceIsDual),
    Guid("18C628ED-962A-11D2-8D08-00A0C9441E20")]
    public interface IXml2Dex
    {
        [PreserveSig]
        int CreateGraphFromFile(
            [MarshalAs(UnmanagedType.IUnknown)] out object ppGraph,
            [MarshalAs(UnmanagedType.IUnknown)] object pTimeline,
            [MarshalAs(UnmanagedType.BStr)] string Filename
            );

        [PreserveSig]
        int WriteGrfFile(
            [MarshalAs(UnmanagedType.IUnknown)] object pGraph,
            [MarshalAs(UnmanagedType.BStr)] string Filename
            );

        [PreserveSig]
        int WriteXMLFile(
            [MarshalAs(UnmanagedType.IUnknown)] object pTimeline,
            [MarshalAs(UnmanagedType.BStr)] string Filename
            );

        [PreserveSig]
        int ReadXMLFile(
            [MarshalAs(UnmanagedType.IUnknown)] object pTimeline,
            [MarshalAs(UnmanagedType.BStr)] string XMLName
            );

        [PreserveSig]
        int Delete(
            [MarshalAs(UnmanagedType.IUnknown)] object pTimeline,
            double dStart,
            double dEnd
            );

        [PreserveSig]
        int WriteXMLPart(
            [MarshalAs(UnmanagedType.IUnknown)] object pTimeline,
            double dStart,
            double dEnd,
            [MarshalAs(UnmanagedType.BStr)] string Filename
            );

        [PreserveSig]
        int PasteXMLFile(
            [MarshalAs(UnmanagedType.IUnknown)] object pTimeline,
            double dStart,
            [MarshalAs(UnmanagedType.BStr)] string Filename
            );

        [PreserveSig]
        int CopyXML(
            [MarshalAs(UnmanagedType.IUnknown)] object pTimeline,
            double dStart,
            double dEnd
            );

        [PreserveSig]
        int PasteXML(
            [MarshalAs(UnmanagedType.IUnknown)] object pTimeline,
            double dStart
            );

        [PreserveSig]
        int Reset();

        [PreserveSig]
        int ReadXML(
            [MarshalAs(UnmanagedType.IUnknown)] object pTimeline,
            [MarshalAs(UnmanagedType.IUnknown)] object pxml
            );

        [PreserveSig]
        int WriteXML(
            [MarshalAs(UnmanagedType.IUnknown)] object pTimeline,
            [MarshalAs(UnmanagedType.BStr)] out string pbstrXML
            );
    }

}
