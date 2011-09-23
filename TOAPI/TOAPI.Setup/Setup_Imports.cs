using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace TOAPI.Setup
{
    public partial class setup
    {

        // from dbt.h

        [DllImport("setupapi.dll")]
        public static extern Int32 SetupDiClassNameFromGuid(ref Guid ClassGuid,
            StringBuilder className, Int32 ClassNameSize, ref Int32 RequiredSize);

        [DllImport("setupapi.dll", SetLastError = true)]
        public static extern Int32 SetupDiCreateDeviceInfoList(ref Guid ClassGuid, Int32 hwndParent);

        [DllImport("setupapi.dll", SetLastError = true)]
        public static extern Int32 SetupDiDestroyDeviceInfoList(IntPtr DeviceInfoSet);

        [DllImport("setupapi.dll")]
        public static extern Int32 SetupDiEnumDeviceInfo(IntPtr DeviceInfoSet,
            Int32 MemberIndex, ref SP_DEVINFO_DATA DeviceInterfaceData);

        [DllImport("setupapi.dll", SetLastError = true)]
        public static extern Boolean SetupDiEnumDeviceInterfaces(
            IntPtr DeviceInfoSet, 
            IntPtr DeviceInfoData, 
            ref Guid InterfaceClassGuid, 
            Int32 MemberIndex, 
            ref SP_DEVICE_INTERFACE_DATA DeviceInterfaceData);

        [DllImport("setupapi.dll")]
        public static extern Int32 SetupDiGetClassDescription(ref Guid ClassGuid,
            StringBuilder classDescription, Int32 ClassDescriptionSize, ref Int32 RequiredSize);

        [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr SetupDiGetClassDevs(ref System.Guid ClassGuid, IntPtr Enumerator, IntPtr hwndParent, Int32 Flags);

        [DllImport("setupapi.dll")]
        public static extern IntPtr SetupDiGetClassDevsEx(IntPtr ClassGuid,
            [MarshalAs(UnmanagedType.LPStr)]String enumerator,
            IntPtr hwndParent, Int32 Flags, IntPtr DeviceInfoSet,
            [MarshalAs(UnmanagedType.LPStr)]String MachineName, IntPtr Reserved);

        [DllImport("setupapi.dll")]
        public static extern Int32 SetupDiGetDeviceInstanceId(IntPtr DeviceInfoSet,
            ref SP_DEVINFO_DATA DeviceInfoData,
            StringBuilder DeviceInstanceId, Int32 DeviceInstanceIdSize, ref Int32 RequiredSize);

        [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern Boolean SetupDiGetDeviceInterfaceDetail(IntPtr DeviceInfoSet, ref SP_DEVICE_INTERFACE_DATA DeviceInterfaceData, IntPtr DeviceInterfaceDetailData, Int32 DeviceInterfaceDetailDataSize, ref Int32 RequiredSize, IntPtr DeviceInfoData);

        [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern Boolean SetupDiGetDeviceInterfaceDetail(IntPtr DeviceInfoSet, 
            ref SP_DEVICE_INTERFACE_DATA DeviceInterfaceData,
            ref SP_DEVICE_INTERFACE_DETAIL_DATA DeviceInterfaceDetailData, 
            Int32 DeviceInterfaceDetailDataSize, 
            ref Int32 RequiredSize, 
            IntPtr DeviceInfoData);

    }
}
