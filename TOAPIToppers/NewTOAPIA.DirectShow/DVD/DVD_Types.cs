using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.DirectShow.DVD
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// From DVD_ATR
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct DvdAtr
    {
        public int ulCAT;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.I1, SizeConst = 768)]
        public byte[] registers;
    }

    /// <summary>
    /// From typedef BYTE
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct DvdVideoATR
    {
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.I1, SizeConst = 2)]
        public byte[] attributes;
    }

    /// <summary>
    /// From typedef BYTE
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct DvdAudioATR
    {
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.I1, SizeConst = 8)]
        public byte[] attributes;
    }

    /// <summary>
    /// From typedef BYTE
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct DvdSubpictureATR
    {
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.I1, SizeConst = 6)]
        public byte[] attributes;
    }

    /// <summary>
    /// From DVD_PLAYBACK_LOCATION
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct DvdPlaybackLocation
    {
        public int TitleNum;
        public int ChapterNum;
        public int TimeCode;
    }

    /// <summary>
    /// From GPRMARRAY
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct GPRMArray
    {
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.I2, SizeConst = 16)]
        public short[] registers;
    }

    /// <summary>
    /// From SPRMARRAY
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SPRMArray
    {
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.I2, SizeConst = 24)]
        public short[] registers;
    }

    /// <summary>
    /// From DVD_PLAYBACK_LOCATION2
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct DvdPlaybackLocation2
    {
        public int TitleNum;
        public int ChapterNum;
        public DvdHMSFTimeCode TimeCode;
        public int TimeCodeFlags;
    }

    /// <summary>
    /// From DVD_AudioAttributes
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct DvdAudioAttributes
    {
        public DvdAudioAppMode AppMode;
        public byte AppModeData;
        public DvdAudioFormat AudioFormat;
        public int Language;
        public DvdAudioLangExt LanguageExtension;
        [MarshalAs(UnmanagedType.Bool)]
        public bool fHasMultichannelInfo;
        public int dwFrequency;
        public byte bQuantization;
        public byte bNumberOfChannels;
        public int dwReserved1;
        public int dwReserved2;
    }

    /// <summary>
    /// From DVD_MUA_MixingInfo
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct DvdMUAMixingInfo
    {
        [MarshalAs(UnmanagedType.Bool)]
        public bool fMixTo0;
        [MarshalAs(UnmanagedType.Bool)]
        public bool fMixTo1;
        [MarshalAs(UnmanagedType.Bool)]
        public bool fMix0InPhase;
        [MarshalAs(UnmanagedType.Bool)]
        public bool fMix1InPhase;
        public int dwSpeakerPosition;
    }

    /// <summary>
    /// From DVD_MUA_Coeff
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct DvdMUACoeff
    {
        public double log2_alpha;
        public double log2_beta;
    }

    /// <summary>
    /// From DVD_MultichannelAudioAttributes
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct DvdMultichannelAudioAttributes
    {
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 8)]
        public DvdMUAMixingInfo[] Info;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 8)]
        public DvdMUACoeff[] Coeff;
    }

    /// <summary>
    /// From DVD_VideoAttributes
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct DvdVideoAttributes
    {
        [MarshalAs(UnmanagedType.Bool)]
        public bool panscanPermitted;
        [MarshalAs(UnmanagedType.Bool)]
        public bool letterboxPermitted;
        public int aspectX;
        public int aspectY;
        public int frameRate;
        public int frameHeight;
        public DvdVideoCompression compression;
        [MarshalAs(UnmanagedType.Bool)]
        public bool line21Field1InGOP;
        [MarshalAs(UnmanagedType.Bool)]
        public bool line21Field2InGOP;
        public int sourceResolutionX;
        public int sourceResolutionY;
        [MarshalAs(UnmanagedType.Bool)]
        public bool isSourceLetterboxed;
        [MarshalAs(UnmanagedType.Bool)]
        public bool isFilmMode;
    }

    /// <summary>
    /// From DVD_SubpictureAttributes
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct DvdSubpictureAttributes
    {
        public DvdSubPictureType Type;
        public DvdSubPictureCoding CodingMode;
        public int Language;
        public DvdSubPictureLangExt LanguageExtension;
    }

    /// <summary>
    /// From DVD_MenuAttributes
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct DvdMenuAttributes
    {
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Bool, SizeConst = 8)]
        public bool[] fCompatibleRegion;
        public DvdVideoAttributes VideoAttributes;
        [MarshalAs(UnmanagedType.Bool)]
        public bool fAudioPresent;
        public DvdAudioAttributes AudioAttributes;
        [MarshalAs(UnmanagedType.Bool)]
        public bool fSubpicturePresent;
        public DvdSubpictureAttributes SubpictureAttributes;
    }

    /// <summary>
    /// From DVD_DECODER_CAPS
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct DvdDecoderCaps
    {
        public int dwSize;
        public DvdAudioCaps dwAudioCaps;
        public double dFwdMaxRateVideo;
        public double dFwdMaxRateAudio;
        public double dFwdMaxRateSP;
        public double dBwdMaxRateVideo;
        public double dBwdMaxRateAudio;
        public double dBwdMaxRateSP;
        public int dwRes1;
        public int dwRes2;
        public int dwRes3;
        public int dwRes4;
    }

    /// <summary>
    /// From AM_DVD_RENDERSTATUS
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct AMDvdRenderStatus
    {
        public int hrVPEStatus;
        [MarshalAs(UnmanagedType.Bool)]
        public bool bDvdVolInvalid;
        [MarshalAs(UnmanagedType.Bool)]
        public bool bDvdVolUnknown;
        [MarshalAs(UnmanagedType.Bool)]
        public bool bNoLine21In;
        [MarshalAs(UnmanagedType.Bool)]
        public bool bNoLine21Out;
        public int iNumStreams;
        public int iNumStreamsFailed;
        public AMDvdStreamFlags dwFailedStreamsFlag;
    }

}
