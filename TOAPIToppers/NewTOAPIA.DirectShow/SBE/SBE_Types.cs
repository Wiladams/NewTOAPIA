using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.DirectShow.SBE
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// From STREAMBUFFER_ATTRIBUTE
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct StreamBufferAttribute
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        public string pszName;
        public StreamBufferAttrDataType StreamBufferAttributeType;
        public IntPtr pbAttribute; // BYTE *
        public short cbLength;
    }

    /// <summary>
    /// From SBE_PIN_DATA
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SBEPinData
    {
        public long cDataBytes; //  total sample payload bytes
        public long cSamplesProcessed; //  samples processed
        public long cDiscontinuities; //  number of discontinuities
        public long cSyncPoints; //  number of syncpoints
        public long cTimestamps; //  number of timestamps
    }
}
