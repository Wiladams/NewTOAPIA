using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.Net.Rtp
{
    public class Rtcp
    {
        #region Constants
        /// <summary>
        /// SDES private extension prefix (PEP) - Source
        /// 
        /// This was added to distinguish between streams from participants vs senders
        /// </summary>
        public const string PEP_SOURCE = "Src";

        /// <summary>
        /// SDES private extension data (PED) - Participant
        /// </summary>
        public const string PED_PARTICIPANT = "P";

        /// <summary>
        /// SDES private extension data (PED) - Stream
        /// </summary>
        public const string PED_STREAM = "S";

        /// <summary>
        /// SDES private extension prefix (PEP) - PayloadType
        /// 
        /// This was added in order to be able to create an RtpStream from Rtcp data
        /// </summary>
        public const string PEP_PAYLOADTYPE = "PT";

        /// <summary>
        /// SDES private extension prefix (PEP) - FEC
        /// 
        /// This was added in order to be able to know the FEC characteristics of a stream
        /// </summary>
        public const string PEP_FEC = "FEC";

        /// <summary>
        /// SDES private extension prefix (PEP) - REC
        /// 
        /// This was added in order to indicate the stream is reliable (Reliable Error Correction)
        /// </summary>
        public const string PEP_REC = "REC";

        /// <summary>
        /// SDES private extension prefix (PEP) - DBP
        /// 
        /// This was added in order to be able to know the bandwidth throttle at the receiving side, for playback purposes.
        /// </summary>
        public const string PEP_DBP = "DBP";
        #endregion
    }
}
