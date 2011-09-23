using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.DirectShow.DVD
{


    /// <summary>
    /// From DVD_DOMAIN
    /// </summary>
    public enum DvdDomain
    {
        FirstPlay = 1,
        VideoManagerMenu,
        VideoTitleSetMenu,
        Title,
        Stop
    }

    /// <summary>
    /// From DVD_MENU_ID
    /// </summary>
    public enum DvdMenuId
    {
        Title = 2,
        Root = 3,
        Subpicture = 4,
        Audio = 5,
        Angle = 6,
        Chapter = 7
    }

    /// <summary>
    /// From DVD_DISC_SIDE
    /// </summary>
    public enum DvdDiscSide
    {
        SideA = 1,
        SideB = 2
    }

    /// <summary>
    /// From DVD_PREFERRED_DISPLAY_MODE
    /// </summary>
    public enum DvdPreferredDisplayMode
    {
        DisplayContentDefault = 0,
        Display16x9 = 1,
        Display4x3PanScanPreferred = 2,
        Display4x3LetterBoxPreferred = 3
    }

    /// <summary>
    /// From DVD_FRAMERATE
    /// </summary>
    public enum DvdFrameRate
    {
        FPS25 = 1,
        FPS30NonDrop = 3
    }

    /// <summary>
    /// From DVD_TIMECODE_FLAGS
    /// </summary>
    [Flags]
    public enum DvdTimeCodeFlags
    {
        None = 0,
        FPS25 = 0x00000001,
        FPS30 = 0x00000002,
        DropFrame = 0x00000004,
        Interpolated = 0x00000008,
    }

    /// <summary>
    /// From VALID_UOP_FLAG
    /// </summary>
    [Flags]
    public enum ValidUOPFlag
    {
        None = 0,
        PlayTitleOrAtTime = 0x00000001,
        PlayChapter = 0x00000002,
        PlayTitle = 0x00000004,
        Stop = 0x00000008,
        ReturnFromSubMenu = 0x00000010,
        PlayChapterOrAtTime = 0x00000020,
        PlayPrevOrReplay_Chapter = 0x00000040,
        PlayNextChapter = 0x00000080,
        PlayForwards = 0x00000100,
        PlayBackwards = 0x00000200,
        ShowMenuTitle = 0x00000400,
        ShowMenuRoot = 0x00000800,
        ShowMenuSubPic = 0x00001000,
        ShowMenuAudio = 0x00002000,
        ShowMenuAngle = 0x00004000,
        ShowMenuChapter = 0x00008000,
        Resume = 0x00010000,
        SelectOrActivateButton = 0x00020000,
        StillOff = 0x00040000,
        PauseOn = 0x00080000,
        SelectAudioStream = 0x00100000,
        SelectSubPicStream = 0x00200000,
        SelectAngle = 0x00400000,
        SelectKaraokeAudioPresentationMode = 0x00800000,
        SelectVideoModePreference = 0x01000000
    }

    /// <summary>
    /// From DVD_CMD_FLAGS
    /// </summary>
    [Flags]
    public enum DvdCmdFlags
    {
        None = 0x00000000,
        Flush = 0x00000001,
        SendEvents = 0x00000002,
        Block = 0x00000004,
        StartWhenRendered = 0x00000008,
        EndAfterRendered = 0x00000010,
    }

    /// <summary>
    /// From DVD_OPTION_FLAG
    /// </summary>
    public enum DvdOptionFlag
    {
        ResetOnStop = 1,
        NotifyParentalLevelChange = 2,
        HMSFTimeCodeEvents = 3,
        AudioDuringFFwdRew = 4,
        EnableNonblockingAPIs = 5,
        CacheSizeInMB = 6,
        EnablePortableBookmarks = 7,
        EnableExtendedCopyProtectErrors = 8,
    }

    /// <summary>
    /// From DVD_RELATIVE_BUTTON
    /// </summary>
    public enum DvdRelativeButton
    {
        Upper = 1,
        Lower = 2,
        Left = 3,
        Right = 4
    }

    /// <summary>
    /// From DVD_PARENTAL_LEVEL
    /// </summary>
    [Flags]
    public enum DvdParentalLevel
    {
        None = 0,
        Level8 = 0x8000,
        Level7 = 0x4000,
        Level6 = 0x2000,
        Level5 = 0x1000,
        Level4 = 0x0800,
        Level3 = 0x0400,
        Level2 = 0x0200,
        Level1 = 0x0100
    }

    /// <summary>
    /// From DVD_AUDIO_LANG_EXT
    /// </summary>
    public enum DvdAudioLangExt
    {
        NotSpecified = 0,
        Captions = 1,
        VisuallyImpaired = 2,
        DirectorComments1 = 3,
        DirectorComments2 = 4,
    }

    /// <summary>
    /// From DVD_SUBPICTURE_LANG_EXT
    /// </summary>
    public enum DvdSubPictureLangExt
    {
        NotSpecified = 0,
        CaptionNormal = 1,
        CaptionBig = 2,
        CaptionChildren = 3,
        CCNormal = 5,
        CCBig = 6,
        CCChildren = 7,
        Forced = 9,
        DirectorCommentsNormal = 13,
        DirectorCommentsBig = 14,
        DirectorCommentsChildren = 15,
    }

    /// <summary>
    /// From DVD_AUDIO_APPMODE
    /// </summary>
    public enum DvdAudioAppMode
    {
        None = 0,
        Karaoke = 1,
        Surround = 2,
        Other = 3,
    }

    /// <summary>
    /// From DVD_AUDIO_FORMAT
    /// </summary>
    public enum DvdAudioFormat
    {
        AC3 = 0,
        MPEG1 = 1,
        MPEG1_DRC = 2,
        MPEG2 = 3,
        MPEG2_DRC = 4,
        LPCM = 5,
        DTS = 6,
        SDDS = 7,
        Other = 8
    }

    /// <summary>
    /// From DVD_KARAOKE_DOWNMIX
    /// </summary>
    [Flags]
    public enum DvdKaraokeDownMix
    {
        None = 0,
        Mix_0to0 = 0x0001,
        Mix_1to0 = 0x0002,
        Mix_2to0 = 0x0004,
        Mix_3to0 = 0x0008,
        Mix_4to0 = 0x0010,
        Mix_Lto0 = 0x0020,
        Mix_Rto0 = 0x0040,
        Mix_0to1 = 0x0100,
        Mix_1to1 = 0x0200,
        Mix_2to1 = 0x0400,
        Mix_3to1 = 0x0800,
        Mix_4to1 = 0x1000,
        Mix_Lto1 = 0x2000,
        Mix_Rto1 = 0x4000,
    }

    /// <summary>
    /// From DVD_KARAOKE_CONTENTS
    /// </summary>
    [Flags]
    public enum DvdKaraokeContents : short
    {
        None = 0,
        GuideVocal1 = 0x0001,
        GuideVocal2 = 0x0002,
        GuideMelody1 = 0x0004,
        GuideMelody2 = 0x0008,
        GuideMelodyA = 0x0010,
        GuideMelodyB = 0x0020,
        SoundEffectA = 0x0040,
        SoundEffectB = 0x0080
    }

    /// <summary>
    /// From DVD_KARAOKE_ASSIGNMENT
    /// </summary>
    public enum DvdKaraokeAssignment
    {
        reserved0 = 0,
        reserved1 = 1,
        LR = 2,
        LRM = 3,
        LR1 = 4,
        LRM1 = 5,
        LR12 = 6,
        LRM12 = 7
    }

    /// <summary>
    /// From DVD_VIDEO_COMPRESSION
    /// </summary>
    public enum DvdVideoCompression
    {
        Other = 0,
        Mpeg1 = 1,
        Mpeg2 = 2
    }

    /// <summary>
    /// From DVD_SUBPICTURE_TYPE
    /// </summary>
    public enum DvdSubPictureType
    {
        NotSpecified = 0,
        Language = 1,
        Other = 2,
    }

    /// <summary>
    /// From DVD_SUBPICTURE_CODING
    /// </summary>
    public enum DvdSubPictureCoding
    {
        RunLength = 0,
        Extended = 1,
        Other = 2,
    }

    /// <summary>
    /// From DVD_TITLE_APPMODE
    /// </summary>
    public enum DvdTitleAppMode
    {
        NotSpecified = 0,
        Karaoke = 1,
        Other = 3,
    }

    /// <summary>
    /// From DVD_TextStringType
    /// </summary>
    public enum DvdTextStringType
    {
        DVD_Struct_Volume = 0x01,
        DVD_Struct_Title = 0x02,
        DVD_Struct_ParentalID = 0x03,
        DVD_Struct_PartOfTitle = 0x04,
        DVD_Struct_Cell = 0x05,
        DVD_Stream_Audio = 0x10,
        DVD_Stream_Subpicture = 0x11,
        DVD_Stream_Angle = 0x12,
        DVD_Channel_Audio = 0x20,
        DVD_General_Name = 0x30,
        DVD_General_Comments = 0x31,
        DVD_Title_Series = 0x38,
        DVD_Title_Movie = 0x39,
        DVD_Title_Video = 0x3a,
        DVD_Title_Album = 0x3b,
        DVD_Title_Song = 0x3c,
        DVD_Title_Other = 0x3f,
        DVD_Title_Sub_Series = 0x40,
        DVD_Title_Sub_Movie = 0x41,
        DVD_Title_Sub_Video = 0x42,
        DVD_Title_Sub_Album = 0x43,
        DVD_Title_Sub_Song = 0x44,
        DVD_Title_Sub_Other = 0x47,
        DVD_Title_Orig_Series = 0x48,
        DVD_Title_Orig_Movie = 0x49,
        DVD_Title_Orig_Video = 0x4a,
        DVD_Title_Orig_Album = 0x4b,
        DVD_Title_Orig_Song = 0x4c,
        DVD_Title_Orig_Other = 0x4f,
        DVD_Other_Scene = 0x50,
        DVD_Other_Cut = 0x51,
        DVD_Other_Take = 0x52,
    }

    /// <summary>
    /// From DVD_TextCharSet
    /// </summary>
    public enum DvdTextCharSet
    {
        CharSet_Unicode = 0,
        CharSet_ISO646 = 1,
        CharSet_JIS_Roman_Kanji = 2,
        CharSet_ISO8859_1 = 3,
        CharSet_ShiftJIS_Kanji_Roman_Katakana = 4
    }

    /// <summary>
    /// From DVD_AUDIO_CAPS_* defines
    /// </summary>
    [Flags]
    public enum DvdAudioCaps
    {
        None = 0,
        AC3 = 0x00000001,
        MPEG2 = 0x00000002,
        LPCM = 0x00000004,
        DTS = 0x00000008,
        SDDS = 0x00000010,
    }

    /// <summary>
    /// From AM_DVD_GRAPH_FLAGS
    /// </summary>
    [Flags]
    public enum AMDvdGraphFlags
    {
        None = 0,
        HWDecPrefer = 0x01,
        HWDecOnly = 0x02,
        SWDecPrefer = 0x04,
        SWDecOnly = 0x08,
        NoVPE = 0x100,
        DoNotClear = 0x200,
        VMR9Only = 0x800,
        EvrOnly = 0x1000,
    }

    /// <summary>
    /// From AM_DVD_STREAM_FLAGS
    /// </summary>
    [Flags]
    public enum AMDvdStreamFlags
    {
        None = 0x00,
        Video = 0x01,
        Audio = 0x02,
        SubPic = 0x04
    }

    [Flags]
    public enum AMOverlayNotifyFlags
    {
        None = 0,
        VisibleChange = 0x00000001,
        SourceChange = 0x00000002,
        DestChange = 0x00000004
    }
}
