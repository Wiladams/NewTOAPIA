

using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace NewTOAPIA.DirectShow.DVD
{

    using NewTOAPIA.DirectShow.Core;
    using NewTOAPIA.DirectShow.DES;

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("A70EFE61-E2A3-11d0-A9BE-00AA0061BE93"),
    Obsolete("The IDvdControl interface is deprecated. Use IDvdControl2 instead.", false),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDvdControl
    {
        [PreserveSig]
        int TitlePlay([In] int ulTitle);

        [PreserveSig]
        int ChapterPlay(
            [In] int ulTitle,
            [In] int ulChapter
            );

        [PreserveSig]
        int TimePlay(
            [In] int ulTitle,
            [In] int bcdTime
            );

        [PreserveSig]
        int StopForResume();

        [PreserveSig]
        int GoUp();

        [PreserveSig]
        int TimeSearch([In] int bcdTime);

        [PreserveSig]
        int ChapterSearch([In] int ulChapter);

        [PreserveSig]
        int PrevPGSearch();

        [PreserveSig]
        int TopPGSearch();

        [PreserveSig]
        int NextPGSearch();

        [PreserveSig]
        int ForwardScan([In] double dwSpeed);

        [PreserveSig]
        int BackwardScan([In] double dwSpeed);

        [PreserveSig]
        int MenuCall([In] DvdMenuId MenuID);

        [PreserveSig]
        int Resume();

        [PreserveSig]
        int UpperButtonSelect();

        [PreserveSig]
        int LowerButtonSelect();

        [PreserveSig]
        int LeftButtonSelect();

        [PreserveSig]
        int RightButtonSelect();

        [PreserveSig]
        int ButtonActivate();

        [PreserveSig]
        int ButtonSelectAndActivate([In] int ulButton);

        [PreserveSig]
        int StillOff();

        [PreserveSig]
        int PauseOn();

        [PreserveSig]
        int PauseOff();

        [PreserveSig]
        int MenuLanguageSelect([In] int Language);

        [PreserveSig]
        int AudioStreamChange([In] int ulAudio);

        [PreserveSig]
        int SubpictureStreamChange(
            [In] int ulSubPicture,
            [In, MarshalAs(UnmanagedType.Bool)] bool bDisplay
            );

        [PreserveSig]
        int AngleChange([In] int ulAngle);

        [PreserveSig]
        int ParentalLevelSelect([In] int ulParentalLevel);

        [PreserveSig]
        int ParentalCountrySelect([In] short wCountry);

        [PreserveSig]
        int KaraokeAudioPresentationModeChange([In] int ulMode);

        [PreserveSig]
        int VideoModePreferrence([In] int ulPreferredDisplayMode);

        [PreserveSig]
        int SetRoot([In, MarshalAs(UnmanagedType.LPWStr)] string pszPath);

        [PreserveSig]
        int MouseActivate([In] Point point);

        [PreserveSig]
        int MouseSelect([In] Point point);

        [PreserveSig]
        int ChapterPlayAutoStop(
            [In] int ulTitle,
            [In] int ulChapter,
            [In] int ulChaptersToPlay
            );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("A70EFE60-E2A3-11d0-A9BE-00AA0061BE93"),
    Obsolete("The IDvdInfo interface is deprecated. Use IDvdInfo2 instead.", false),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDvdInfo
    {
        [PreserveSig]
        int GetCurrentDomain([Out] out DvdDomain pDomain);

        [PreserveSig]
        int GetCurrentLocation([Out] out DvdPlaybackLocation pLocation);

        [PreserveSig]
        int GetTotalTitleTime([Out] out int pulTotalTime);

        [PreserveSig]
        int GetCurrentButton(
            [Out] out int pulButtonsAvailable,
            [Out] out int pulCurrentButton
            );

        [PreserveSig]
        int GetCurrentAngle(
            [Out] out int pulAnglesAvailable,
            [Out] out int pulCurrentAngle
            );

        [PreserveSig]
        int GetCurrentAudio(
            [Out] out int pulStreamsAvailable,
            [Out] out int pulCurrentStream
            );

        [PreserveSig]
        int GetCurrentSubpicture(
            [Out] out int pulStreamsAvailable,
            [Out] out int pulCurrentStream,
            [Out, MarshalAs(UnmanagedType.Bool)] out bool pIsDisabled
            );

        [PreserveSig]
        int GetCurrentUOPS([Out] out int pUOP);

        [PreserveSig]
        int GetAllSPRMs([Out] out SPRMArray pRegisterArray);

        [PreserveSig]
        int GetAllGPRMs([Out] out GPRMArray pRegisterArray);

        [PreserveSig]
        int GetAudioLanguage(
            [In] int ulStream,
            [Out] out int pLanguage
            );

        [PreserveSig]
        int GetSubpictureLanguage(
            [In] int ulStream,
            [Out] out int pLanguage
            );

        [PreserveSig]
        int GetTitleAttributes(
            [In] int ulTitle,
            [Out] out DvdAtr pATR
            );

        [PreserveSig]
        int GetVMGAttributes([Out] out DvdAtr pATR);

        [PreserveSig]
        int GetCurrentVideoAttributes([Out] out DvdVideoATR pATR);

        [PreserveSig]
        int GetCurrentAudioAttributes([Out] out DvdAudioATR pATR);

        [PreserveSig]
        int GetCurrentSubpictureAttributes([Out] out DvdSubpictureATR pATR);


        [PreserveSig]
        int GetCurrentVolumeInfo(
            [Out] out int pulNumOfVol,
            [Out] out int pulThisVolNum,
            [Out] DvdDiscSide pSide,
            [Out] out int pulNumOfTitles
            );


        [PreserveSig]
        int GetDVDTextInfo(
            [Out] out IntPtr pTextManager, // BYTE *
            [In] int ulBufSize,
            [Out] out int pulActualSize
            );

        [PreserveSig]
        int GetPlayerParentalLevel(
            [Out] out int pulParentalLevel,
            [Out] out int pulCountryCode
            );

        [PreserveSig]
        int GetNumberOfChapters(
            [In] int ulTitle,
            [Out] out int pulNumberOfChapters
            );

        [PreserveSig]
        int GetTitleParentalLevels(
            [In] int ulTitle,
            [Out] out int pulParentalLevels
            );

        [PreserveSig]
        int GetRoot(
            [Out] out IntPtr pRoot, // LPSTR
            [In] int ulBufSize,
            [Out] out int pulActualSize
            );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("153ACC21-D83B-11d1-82BF-00A0C9696C8F"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDDrawExclModeVideo
    {
        [PreserveSig]
        int SetDDrawObject([In, MarshalAs(UnmanagedType.IUnknown)] object pDDrawObject);

        [PreserveSig]
        int GetDDrawObject(
            [Out, MarshalAs(UnmanagedType.IUnknown)] out object ppDDrawObject,
            [Out, MarshalAs(UnmanagedType.Bool)] out bool pbUsingExternal
            );

        [PreserveSig]
        int SetDDrawSurface([In, MarshalAs(UnmanagedType.IUnknown)] object pDDrawSurface);

        [PreserveSig]
        int GetDDrawSurface(
            [Out, MarshalAs(UnmanagedType.IUnknown)] out object ppDDrawSurface,
            [Out, MarshalAs(UnmanagedType.Bool)] out bool pbUsingExternal
            );

        [PreserveSig]
        int SetDrawParameters(
            [In] Rectangle prcSource,
            [In] Rectangle prcTarget
            );

        [PreserveSig]
        int GetNativeVideoProps(
            [Out] out int pdwVideoWidth,
            [Out] out int pdwVideoHeight,
            [Out] out int pdwPictAspectRatioX,
            [Out] out int pdwPictAspectRatioY
            );

        [PreserveSig]
        int SetCallbackInterface(
            [In, MarshalAs(UnmanagedType.IUnknown)] object pCallback,
            [In] int dwFlags
            );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("913c24a0-20ab-11d2-9038-00a0c9697298"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDDrawExclModeVideoCallback
    {
        [PreserveSig]
        int OnUpdateOverlay(
            [In, MarshalAs(UnmanagedType.Bool)] bool bBefore,
            [In] AMOverlayNotifyFlags dwFlags,
            [In, MarshalAs(UnmanagedType.Bool)] bool bOldVisible,
            [In] Rectangle prcOldSrc,
            [In] Rectangle prcOldDest,
            [In, MarshalAs(UnmanagedType.Bool)] bool bNewVisible,
            [In] Rectangle prcNewSrc,
            [In] Rectangle prcNewDest
            );

        [PreserveSig]
        int OnUpdateColorKey(
            [In] ColorKey pKey,
            [In] int dwColor
            );

        [PreserveSig]
        int OnUpdateSize(
            [In] int dwWidth,
            [In] int dwHeight,
            [In] int dwARWidth,
            [In] int dwARHeight
            );
    }


    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("FCC152B6-F372-11d0-8E00-00C04FD7C08B"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDvdGraphBuilder
    {
        [PreserveSig]
        int GetFiltergraph([Out] out IGraphBuilder ppGB);

        [PreserveSig]
        int GetDvdInterface(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid,
            [Out, MarshalAs(UnmanagedType.Interface)] out object ppvIF
            );

        [PreserveSig]
        int RenderDvdVideoVolume(
            [In, MarshalAs(UnmanagedType.LPWStr)] string lpcwszPathName,
            [In] AMDvdGraphFlags dwFlags,
            [Out] out AMDvdRenderStatus pStatus
            );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("33BC7430-EEC0-11D2-8201-00A0C9D74842"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDvdControl2
    {
        [PreserveSig]
        int PlayTitle(
            [In] int ulTitle,
            [In] DvdCmdFlags dwFlags,
            [Out] out IDvdCmd ppCmd
            );

        [PreserveSig]
        int PlayChapterInTitle(
            [In] int ulTitle,
            [In] int ulChapter,
            [In] DvdCmdFlags dwFlags,
            [Out] out IDvdCmd ppCmd
            );

        [PreserveSig]
        int PlayAtTimeInTitle(
            [In] int ulTitle,
            [In] DvdHMSFTimeCode pStartTime,
            [In] DvdCmdFlags dwFlags,
            [Out] out IDvdCmd ppCmd
            );

        [PreserveSig]
        int Stop();

        [PreserveSig]
        int ReturnFromSubmenu(
            [In] DvdCmdFlags dwFlags,
            [Out] out IDvdCmd ppCmd
            );

        [PreserveSig]
        int PlayAtTime(
            [In] DvdHMSFTimeCode pTime,
            [In] DvdCmdFlags dwFlags,
            [Out] out IDvdCmd ppCmd
            );

        [PreserveSig]
        int PlayChapter(
            [In] int ulChapter,
            [In] DvdCmdFlags dwFlags,
            [Out] out IDvdCmd ppCmd
            );

        [PreserveSig]
        int PlayPrevChapter(
            [In] DvdCmdFlags dwFlags,
            [Out] out IDvdCmd ppCmd
            );

        [PreserveSig]
        int ReplayChapter(
            [In] DvdCmdFlags dwFlags,
            [Out] out IDvdCmd ppCmd
            );

        [PreserveSig]
        int PlayNextChapter(
            [In] DvdCmdFlags dwFlags,
            [Out] out IDvdCmd ppCmd
            );

        [PreserveSig]
        int PlayForwards(
            [In] double dSpeed,
            [In] DvdCmdFlags dwFlags,
            [Out] out IDvdCmd ppCmd
            );

        [PreserveSig]
        int PlayBackwards(
            [In] double dSpeed,
            [In] DvdCmdFlags dwFlags,
            [Out] out IDvdCmd ppCmd
            );

        [PreserveSig]
        int ShowMenu(
            [In] DvdMenuId MenuID,
            [In] DvdCmdFlags dwFlags,
            [Out] out IDvdCmd ppCmd
            );

        [PreserveSig]
        int Resume(
            [In] DvdCmdFlags dwFlags,
            [Out] out IDvdCmd ppCmd
            );

        [PreserveSig]
        int SelectRelativeButton(DvdRelativeButton buttonDir);

        [PreserveSig]
        int ActivateButton();

        [PreserveSig]
        int SelectButton([In] int ulButton);

        [PreserveSig]
        int SelectAndActivateButton([In] int ulButton);

        [PreserveSig]
        int StillOff();

        [PreserveSig]
        int Pause([In, MarshalAs(UnmanagedType.Bool)] bool bState);

        [PreserveSig]
        int SelectAudioStream(
            [In] int ulAudio,
            [In] DvdCmdFlags dwFlags,
            [Out] out IDvdCmd ppCmd
            );

        [PreserveSig]
        int SelectSubpictureStream(
            [In] int ulSubPicture,
            [In] DvdCmdFlags dwFlags,
            [Out] out IDvdCmd ppCmd
            );

        [PreserveSig]
        int SetSubpictureState(
            [In, MarshalAs(UnmanagedType.Bool)] bool bState,
            [In] DvdCmdFlags dwFlags,
            [Out] out IDvdCmd ppCmd
            );

        [PreserveSig]
        int SelectAngle(
            [In] int ulAngle,
            [In] DvdCmdFlags dwFlags,
            [Out] out IDvdCmd ppCmd
            );

        [PreserveSig]
        int SelectParentalLevel([In] int ulParentalLevel);

        [PreserveSig]
        int SelectParentalCountry([In, MarshalAs(UnmanagedType.LPArray)] byte[] bCountry);

        [PreserveSig]
        int SelectKaraokeAudioPresentationMode([In] DvdKaraokeDownMix ulMode);

        [PreserveSig]
        int SelectVideoModePreference([In] DvdPreferredDisplayMode ulPreferredDisplayMode);

        [PreserveSig]
        int SetDVDDirectory([In, MarshalAs(UnmanagedType.LPWStr)] string pszwPath);

        [PreserveSig]
        int ActivateAtPosition([In] Point point);

        [PreserveSig]
        int SelectAtPosition([In] Point point);

        [PreserveSig]
        int PlayChaptersAutoStop(
            [In] int ulTitle,
            [In] int ulChapter,
            [In] int ulChaptersToPlay,
            [In] DvdCmdFlags dwFlags,
            [Out] out IDvdCmd ppCmd
            );

        [PreserveSig]
        int AcceptParentalLevelChange([In, MarshalAs(UnmanagedType.Bool)] bool bAccept);

        [PreserveSig]
        int SetOption(
            [In] DvdOptionFlag flag,
            [In, MarshalAs(UnmanagedType.Bool)] bool fState
            );

        [PreserveSig]
        int SetState(
            [In] IDvdState pState,
            [In] DvdCmdFlags dwFlags,
            [Out] out IDvdCmd ppCmd
            );

        [PreserveSig]
        int PlayPeriodInTitleAutoStop(
            [In] int ulTitle,
            [In, MarshalAs(UnmanagedType.LPStruct)] DvdHMSFTimeCode pStartTime,
            [In, MarshalAs(UnmanagedType.LPStruct)] DvdHMSFTimeCode pEndTime,
            [In] DvdCmdFlags dwFlags,
            [Out] out IDvdCmd ppCmd
            );

        [PreserveSig]
        int SetGPRM(
            [In] int ulIndex,
            [In] short wValue,
            [In] DvdCmdFlags dwFlags,
            [Out] out IDvdCmd ppCmd
            );

        [PreserveSig]
        int SelectDefaultMenuLanguage([In] int Language);

        [PreserveSig]
        int SelectDefaultAudioLanguage(
            [In] int Language,
            [In] DvdAudioLangExt audioExtension
            );

        [PreserveSig]
        int SelectDefaultSubpictureLanguage(
            [In] int Language,
            [In] DvdSubPictureLangExt subpictureExtension
            );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("34151510-EEC0-11D2-8201-00A0C9D74842"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDvdInfo2
    {
        [PreserveSig]
        int GetCurrentDomain([Out] out DvdDomain pDomain);

        [PreserveSig]
        int GetCurrentLocation([Out] out DvdPlaybackLocation2 pLocation);

        [PreserveSig]
        int GetTotalTitleTime(
            [Out] DvdHMSFTimeCode pTotalTime,
            [Out] out DvdTimeCodeFlags ulTimeCodeFlags
            );

        [PreserveSig]
        int GetCurrentButton(
            [Out] out int pulButtonsAvailable,
            [Out] out int pulCurrentButton
            );

        [PreserveSig]
        int GetCurrentAngle(
            [Out] out int pulAnglesAvailable,
            [Out] out int pulCurrentAngle
            );

        [PreserveSig]
        int GetCurrentAudio(
            [Out] out int pulStreamsAvailable,
            [Out] out int pulCurrentStream
            );

        [PreserveSig]
        int GetCurrentSubpicture(
            [Out] out int pulStreamsAvailable,
            [Out] out int pulCurrentStream,
            [Out, MarshalAs(UnmanagedType.Bool)] out bool pbIsDisabled
            );

        [PreserveSig]
        int GetCurrentUOPS([Out] out ValidUOPFlag pulUOPs);

        [PreserveSig]
        int GetAllSPRMs([Out] out SPRMArray pRegisterArray);

        [PreserveSig]
        int GetAllGPRMs([Out] out GPRMArray pRegisterArray);

        [PreserveSig]
        int GetAudioLanguage(
            [In] int ulStream,
            [Out] out int pLanguage
            );

        [PreserveSig]
        int GetSubpictureLanguage(
            [In] int ulStream,
            [Out] out int pLanguage
            );

        [PreserveSig]
        int GetTitleAttributes(
            [In] int ulTitle,
            [Out] out DvdMenuAttributes pMenu,
            [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DTAMarshaler))] DvdTitleAttributes pTitle
            );

        [PreserveSig]
        int GetVMGAttributes([Out] out DvdMenuAttributes pATR);

        [PreserveSig]
        int GetCurrentVideoAttributes([Out] out DvdVideoAttributes pATR);

        [PreserveSig]
        int GetAudioAttributes(
            [In] int ulStream,
            [Out] out DvdAudioAttributes pATR
            );

        [PreserveSig]
        int GetKaraokeAttributes(
            [In] int ulStream,
            [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DKAMarshaler))] DvdKaraokeAttributes pAttributes
            );

        [PreserveSig]
        int GetSubpictureAttributes(
            [In] int ulStream,
            [Out] out DvdSubpictureAttributes pATR
            );

        [PreserveSig]
        int GetDVDVolumeInfo(
            [Out] out int pulNumOfVolumes,
            [Out] out int pulVolume,
            [Out] out DvdDiscSide pSide,
            [Out] out int pulNumOfTitles
            );

        [PreserveSig]
        int GetDVDTextNumberOfLanguages([Out] out int pulNumOfLangs);

        [PreserveSig]
        int GetDVDTextLanguageInfo(
            [In] int ulLangIndex,
            [Out] out int pulNumOfStrings,
            [Out] out int pLangCode,
            [Out] out DvdTextCharSet pbCharacterSet
            );

        [PreserveSig]
        int GetDVDTextStringAsNative(
            [In] int ulLangIndex,
            [In] int ulStringIndex,
            [MarshalAs(UnmanagedType.LPStr)]System.Text.StringBuilder pbBuffer,
            [In] int ulMaxBufferSize,
            [Out] out int pulActualSize,
            [Out] out DvdTextStringType pType
            );

        [PreserveSig]
        int GetDVDTextStringAsUnicode(
            [In] int ulLangIndex,
            [In] int ulStringIndex,
            System.Text.StringBuilder pchwBuffer,
            [In] int ulMaxBufferSize,
            [Out] out int pulActualSize,
            [Out] out DvdTextStringType pType
            );

        [PreserveSig]
        int GetPlayerParentalLevel(
            [Out] out int pulParentalLevel,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 2)] byte[] pbCountryCode
            );

        [PreserveSig]
        int GetNumberOfChapters(
            [In] int ulTitle,
            [Out] out int pulNumOfChapters
            );

        [PreserveSig]
        int GetTitleParentalLevels(
            [In] int ulTitle,
            [Out] out DvdParentalLevel pulParentalLevels
            );

        [PreserveSig]
        int GetDVDDirectory(
            System.Text.StringBuilder pszwPath,
            [In] int ulMaxSize,
            [Out] out int pulActualSize
            );

        [PreserveSig]
        int IsAudioStreamEnabled(
            [In] int ulStreamNum,
            [Out, MarshalAs(UnmanagedType.Bool)] out bool pbEnabled
            );

        [PreserveSig]
        int GetDiscID(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pszwPath,
            [Out] out long pullDiscID
            );

        [PreserveSig]
        int GetState([Out] out IDvdState pStateData);

        [PreserveSig]
        int GetMenuLanguages(
            [MarshalAs(UnmanagedType.LPArray)] int[] pLanguages,
            [In] int ulMaxLanguages,
            [Out] out int pulActualLanguages
            );

        [PreserveSig]
        int GetButtonAtPosition(
            [In] Point point,
            [Out] out int pulButtonIndex
            );

        [PreserveSig]
        int GetCmdFromEvent(
            [In] IntPtr lParam1,
            [Out] out IDvdCmd pCmdObj
            );

        [PreserveSig]
        int GetDefaultMenuLanguage([Out] out int pLanguage);

        [PreserveSig]
        int GetDefaultAudioLanguage(
            [Out] out int pLanguage,
            [Out] out DvdAudioLangExt pAudioExtension
            );

        [PreserveSig]
        int GetDefaultSubpictureLanguage(
            [Out] out int pLanguage,
            [Out] out DvdSubPictureLangExt pSubpictureExtension
            );

        [PreserveSig]
        int GetDecoderCaps(ref DvdDecoderCaps pCaps);

        [PreserveSig]
        int GetButtonRect(
            [In] int ulButton,
            [Out] DsRect pRect
            );

        [PreserveSig]
        int IsSubpictureStreamEnabled(
            [In] int ulStreamNum,
            [Out, MarshalAs(UnmanagedType.Bool)] out bool pbEnabled
            );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("5a4a97e4-94ee-4a55-9751-74b5643aa27d"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDvdCmd
    {
        [PreserveSig]
        int WaitForStart();

        [PreserveSig]
        int WaitForEnd();
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("86303d6d-1c4a-4087-ab42-f711167048ef"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDvdState
    {
        [PreserveSig]
        int GetDiscID([Out] out long pullUniqueID);

        [PreserveSig]
        int GetParentalLevel([Out] out int pulParentalLevel);
    }

}
