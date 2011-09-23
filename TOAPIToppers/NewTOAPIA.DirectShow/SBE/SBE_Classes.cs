using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.DirectShow.SBE
{
    /// <summary>
    /// From g_wszStreamBufferRecording* static const WCHAR
    /// </summary>
    sealed public class StreamBufferRecording
    {
        private StreamBufferRecording()
        {
        }

        ////////////////////////////////////////////////////////////////
        //
        // List of pre-defined attributes
        //
        public readonly string Duration = "Duration";

        public readonly string Bitrate = "Bitrate";
        public readonly string Seekable = "Seekable";
        public readonly string Stridable = "Stridable";
        public readonly string Broadcast = "Broadcast";
        public readonly string Protected = "Is_Protected";
        public readonly string Trusted = "Is_Trusted";
        public readonly string Signature_Name = "Signature_Name";
        public readonly string HasAudio = "HasAudio";
        public readonly string HasImage = "HasImage";
        public readonly string HasScript = "HasScript";
        public readonly string HasVideo = "HasVideo";
        public readonly string CurrentBitrate = "CurrentBitrate";
        public readonly string OptimalBitrate = "OptimalBitrate";
        public readonly string HasAttachedImages = "HasAttachedImages";
        public readonly string SkipBackward = "Can_Skip_Backward";
        public readonly string SkipForward = "Can_Skip_Forward";
        public readonly string NumberOfFrames = "NumberOfFrames";
        public readonly string FileSize = "FileSize";
        public readonly string HasArbitraryDataStream = "HasArbitraryDataStream";
        public readonly string HasFileTransferStream = "HasFileTransferStream";

        ////////////////////////////////////////////////////////////////
        //
        // The content description object supports 5 basic attributes.
        //
        public readonly string Title = "Title";

        public readonly string Author = "Author";
        public readonly string Description = "Description";
        public readonly string Rating = "Rating";
        public readonly string Copyright = "Copyright";

        ////////////////////////////////////////////////////////////////
        //
        // These attributes are used to configure DRM using IWMDRMWriter::SetDRMAttribute.
        //
        public readonly string Use_DRM = "Use_DRM";

        public readonly string DRM_Flags = "DRM_Flags";
        public readonly string DRM_Level = "DRM_Level";

        ////////////////////////////////////////////////////////////////
        //
        // These are the additional attributes defined in the WM attribute
        // namespace that give information about the content.
        //
        public readonly string AlbumTitle = "WM/AlbumTitle";

        public readonly string Track = "WM/Track";
        public readonly string PromotionURL = "WM/PromotionURL";
        public readonly string AlbumCoverURL = "WM/AlbumCoverURL";
        public readonly string Genre = "WM/Genre";
        public readonly string Year = "WM/Year";
        public readonly string GenreID = "WM/GenreID";
        public readonly string MCDI = "WM/MCDI";
        public readonly string Composer = "WM/Composer";
        public readonly string Lyrics = "WM/Lyrics";
        public readonly string TrackNumber = "WM/TrackNumber";
        public readonly string ToolName = "WM/ToolName";
        public readonly string ToolVersion = "WM/ToolVersion";
        public readonly string IsVBR = "IsVBR";
        public readonly string AlbumArtist = "WM/AlbumArtist";

        ////////////////////////////////////////////////////////////////
        //
        // These optional attributes may be used to give information
        // about the branding of the content.
        //
        public readonly string BannerImageType = "BannerImageType";

        public readonly string BannerImageData = "BannerImageData";
        public readonly string BannerImageURL = "BannerImageURL";
        public readonly string CopyrightURL = "CopyrightURL";

        ////////////////////////////////////////////////////////////////
        //
        // Optional attributes, used to give information
        // about video stream properties.
        //
        public readonly string AspectRatioX = "AspectRatioX";

        public readonly string AspectRatioY = "AspectRatioY";

        ////////////////////////////////////////////////////////////////
        //
        // The NSC file supports the following attributes.
        //
        public readonly string NSCName = "NSC_Name";

        public readonly string NSCAddress = "NSC_Address";
        public readonly string NSCPhone = "NSC_Phone";
        public readonly string NSCEmail = "NSC_Email";
        public readonly string NSCDescription = "NSC_Description";
    }
}
