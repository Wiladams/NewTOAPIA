using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.DirectShow.MMStreaming
{
    #region GUIDS

    sealed public class MSPID
    {
        private MSPID()
        {
            // Prevent people from trying to instantiate this class
        }

        /// <summary> MSPID_PrimaryVideo </summary>
        public static readonly Guid PrimaryVideo = new Guid(0xa35ff56a, 0x9fda, 0x11d0, 0x8f, 0xdf, 0x0, 0xc0, 0x4f, 0xd9, 0x18, 0x9d);

        /// <summary> MSPID_PrimaryAudio </summary>
        public static readonly Guid PrimaryAudio = new Guid(0xa35ff56b, 0x9fda, 0x11d0, 0x8f, 0xdf, 0x0, 0xc0, 0x4f, 0xd9, 0x18, 0x9d);
    }

    #endregion
}
