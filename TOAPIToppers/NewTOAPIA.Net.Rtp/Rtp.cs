using System;

namespace NewTOAPIA.Net.Rtp
{
    /// <summary>
    /// This class contains statics, constants and enums used throughout the Rtp code
    /// </summary>
    public class Rtp
    {
        /// <summary>
        ///  Current version of RTP / RTCP - 2
        /// </summary>
        public const byte VERSION = 2;

        /// <summary>
        /// Size, in bytes, of an SSRC
        /// </summary>
        public const int SSRC_SIZE = 4;
    }
}
