using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.DirectShow.BDA
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// From PID_MAP
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PIDMap
    {
        public int ulPID;
        public MediaSampleContent MediaSampleContent;
    }

    /// <summary>
    /// From EALocationCodeType
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct EALocationCodeType
    {
        public LocationCodeSchemeType LocationCodeScheme;
        public byte StateCode;
        public byte CountySubdivision;
        public short CountyCode;
    }


    /// <summary>
    /// From SmartCardApplication
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SmartCardApplication
    {
        public ApplicationTypeType ApplicationType;
        public short ApplicationVersion;
        [MarshalAs(UnmanagedType.BStr)]
        public string pbstrApplicationName;
        [MarshalAs(UnmanagedType.BStr)]
        public string pbstrApplicationURL;
    }

    /// <summary>
    /// From MPEG2_TRANSPORT_STRIDE
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MPEG2TransportStride
    {
        public int dwOffset;
        public int dwPacketLength;
        public int dwStride;
    }

    /// <summary>
    /// From BDA_TEMPLATE_PIN_JOINT
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct BDATemplatePinJoint
    {
        public int uliTemplateConnection;
        public int ulcInstancesMax;
    }

    /// <summary>
    /// From KS_BDA_FRAME_INFO
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct KSBDAFrameInfo
    {
        public int ExtendedHeaderSize; // Size of this extended header
        public int dwFrameFlags; //
        public int ulEvent; //
        public int ulChannelNumber; //
        public int ulSubchannelNumber; //
        public int ulReason; //
    }

    /// <summary>
    /// From BDA_TEMPLATE_CONNECTION
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct BDATemplateConnection
    {
        public int FromNodeType;
        public int FromNodePinType;
        public int ToNodeType;
        public int ToNodePinType;
    }

}
