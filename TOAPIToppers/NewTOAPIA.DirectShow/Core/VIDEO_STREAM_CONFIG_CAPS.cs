using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.DirectShow.Core
{
    using System.Runtime.InteropServices;

    using TOAPI.Types;

    /// <summary>
    /// From VIDEO_STREAM_CONFIG_CAPS
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class VIDEO_STREAM_CONFIG_CAPS
    {
        public Guid guid;
        public AnalogVideoStandard VideoStandard;
        public SIZE InputSize;
        public SIZE MinCroppingSize;
        public SIZE MaxCroppingSize;
        public int CropGranularityX;
        public int CropGranularityY;
        public int CropAlignX;
        public int CropAlignY;
        public SIZE MinOutputSize;
        public SIZE MaxOutputSize;
        public int OutputGranularityX;
        public int OutputGranularityY;
        public int StretchTapsX;
        public int StretchTapsY;
        public int ShrinkTapsX;
        public int ShrinkTapsY;
        public long MinFrameInterval;
        public long MaxFrameInterval;
        public int MinBitsPerSecond;
        public int MaxBitsPerSecond;
    }
}
