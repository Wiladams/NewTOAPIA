using System;
using System.Runtime.InteropServices;

namespace TOAPI.HID
{

    [StructLayout(LayoutKind.Sequential)]
    public struct HIDD_ATTRIBUTES
    {
        public Int32 Size;
        public Int16 VendorID;
        public Int16 ProductID;
        public Int16 VersionNumber;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct HIDP_CAPS
    {
        public Int16 Usage;
        public Int16 UsagePage;
        public Int16 InputReportByteLength;
        public Int16 OutputReportByteLength;
        public Int16 FeatureReportByteLength;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public Int16[] Reserved;
        public Int16 NumberLinkCollectionNodes;
        public Int16 NumberInputButtonCaps;
        public Int16 NumberInputValueCaps;
        public Int16 NumberInputDataIndices;
        public Int16 NumberOutputButtonCaps;
        public Int16 NumberOutputValueCaps;
        public Int16 NumberOutputDataIndices;
        public Int16 NumberFeatureButtonCaps;
        public Int16 NumberFeatureValueCaps;
        public Int16 NumberFeatureDataIndices;
    }

    //  If IsRange is false, UsageMin is the Usage and UsageMax is unused.
    //  If IsStringRange is false, StringMin is the String index and StringMax is unused.
    //  If IsDesignatorRange is false, DesignatorMin is the designator index and DesignatorMax is unused.

    [StructLayout(LayoutKind.Sequential)]
    public struct HidP_Value_Caps
    {
        public Int16 UsagePage;
        public Byte ReportID;
        public Int32 IsAlias;
        public Int16 BitField;
        public Int16 LinkCollection;
        public Int16 LinkUsage;
        public Int16 LinkUsagePage;
        public Int32 IsRange;
        public Int32 IsStringRange;
        public Int32 IsDesignatorRange;
        public Int32 IsAbsolute;
        public Int32 HasNull;
        public Byte Reserved;
        public Int16 BitSize;
        public Int16 ReportCount;
        public Int16 Reserved2;
        public Int16 Reserved3;
        public Int16 Reserved4;
        public Int16 Reserved5;
        public Int16 Reserved6;
        public Int32 LogicalMin;
        public Int32 LogicalMax;
        public Int32 PhysicalMin;
        public Int32 PhysicalMax;

        // This is actually a union between
        // 'Range' and 'NotRange' data
        // When 'NotRange', just use the 'Min' values
        public Int16 UsageMin;
        public Int16 UsageMax;
        public Int16 StringMin;
        public Int16 StringMax;
        public Int16 DesignatorMin;
        public Int16 DesignatorMax;
        public Int16 DataIndexMin;
        public Int16 DataIndexMax;
    }

}