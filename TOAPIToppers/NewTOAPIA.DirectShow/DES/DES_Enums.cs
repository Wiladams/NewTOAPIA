using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace NewTOAPIA.DirectShow.DES
{
    /// <summary>
    /// From unnamed enum
    /// </summary>
    public enum ConnectFDynamic
    {
        None = 0x00000000,
        Sources = 0x00000001,
        Effects = 0x00000002
    }

    /// <summary>
    /// From DEXTERF
    /// </summary>
    public enum Dexterf
    {
        Jump,
        Interpolate
    }

    /// <summary>
    /// From DEXTERF_TRACK_SEARCH_FLAGS
    /// </summary>
    public enum DexterFTrackSearchFlags
    {
        Bounding = -1,
        ExactlyAt = 0,
        Forwards = 1
    }

    /// <summary>
    /// From FILTER_STATE
    /// </summary>
    public enum FilterState
    {
        Stopped,
        Paused,
        Running
    }

    /// <summary>
    /// From PIN_DIRECTION
    /// </summary>
    public enum PinDirection
    {
        Input,
        Output
    }

    /// <summary>
    /// From unnamed enum
    /// </summary>
    public enum ResizeFlags
    {
        Stretch,
        Crop,
        PreserveAspectRatio,
        PreserveAspectRatioNoLetterBox
    }

    /// <summary>
    /// From SCompFmt0
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class SCompFmt0
    {
        public int nFormatId;
        public AMMediaType MediaType;
    }

    /// <summary>
    /// From unnamed enum
    /// </summary>
    [Flags]
    public enum SFNValidateFlags
    {
        None = 0x00000000,
        Check = 0x00000001,
        Popup = 0x00000002,
        TellMe = 0x00000004,
        Replace = 0x00000008,
        UseLocal = 0x000000010,
        NoFind = 0x000000020,
        IgnoreMuted = 0x000000040,
        End
    }

    /// <summary>
    /// From unnamed enum
    /// </summary>
    public enum TimelineInsertMode
    {
        Insert = 1,
        Overlay = 2
    }

    /// <summary>
    /// From TIMELINE_MAJOR_TYPE
    /// </summary>
    [Flags]
    public enum TimelineMajorType
    {
        None = 0,
        Composite = 1,
        Effect = 0x10,
        Group = 0x80,
        Source = 4,
        Track = 2,
        Transition = 8
    }
}
