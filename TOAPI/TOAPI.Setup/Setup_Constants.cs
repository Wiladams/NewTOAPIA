namespace TOAPI.Setup
{
    using System;

    public partial class setup
    {
        // from setupapi.h

        public const Int32 DIGCF_PRESENT            = 0x02;
        public const Int32 DIGCF_DEVICEINTERFACE    = 0x10;

        // from dbt.h

        public const Int32 DBT_DEVICEARRIVAL = 0X8000;
        public const Int32 DBT_DEVICEREMOVECOMPLETE = 0X8004;
        public const Int32 DBT_DEVTYP_DEVICEINTERFACE = 5;
        public const Int32 DBT_DEVTYP_HANDLE = 6;


        // Used with RegisterDeviceNotification
        public const Int32 DEVICE_NOTIFY_WINDOW_HANDLE = 0;
        public const Int32 DEVICE_NOTIFY_SERVICE_HANDLE = 1;
        public const Int32 DEVICE_NOTIFY_ALL_INTERFACE_CLASSES = 4;
    }
}
