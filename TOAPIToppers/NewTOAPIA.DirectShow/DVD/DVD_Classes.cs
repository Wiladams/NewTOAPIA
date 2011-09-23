using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.DirectShow.DVD
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// CLSID_DvdGraphBuilder
    /// </summary>
    [ComImport, Guid("FCC152B7-F372-11d0-8E00-00C04FD7C08B")]
    public class DvdGraphBuilder
    {
    }

    /// <summary>
    /// From DVD_KaraokeAttributes
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 32)]
    public class DvdKaraokeAttributes
    {
        public byte bVersion;
        public bool fMasterOfCeremoniesInGuideVocal1;
        public bool fDuet;
        public DvdKaraokeAssignment ChannelAssignment;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.I2, SizeConst = 8)]
        public DvdKaraokeContents[] wChannelContents;
    }

        /// <summary>
    /// From DVD_TitleAttributes
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class DvdTitleAttributes
    {
        public DvdTitleAppMode AppMode;
        public DvdVideoAttributes VideoAttributes;
        public int ulNumberOfAudioStreams;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 8)]
        public DvdAudioAttributes[] AudioAttributes;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 8)]
        public DvdMultichannelAudioAttributes[] MultichannelAudioAttributes;
        public int ulNumberOfSubpictureStreams;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 32)]
        public DvdSubpictureAttributes[] SubpictureAttributes;
    }

        /// <summary>
    /// From DVD_HMSF_TIMECODE
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class DvdHMSFTimeCode
    {
        public byte bHours;
        public byte bMinutes;
        public byte bSeconds;
        public byte bFrames;
    }

}
