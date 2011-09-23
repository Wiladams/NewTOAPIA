using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace USBHIDTest
{
    public static class Extensions
    {
        //[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
        //internal static bool InitRegisters(this USBDevice device)
        //{
        //    var result = false;
        //    var registers = device.ReadRegisters(ReportType.Entire);
        //    result = registers != default(ushort[]);
        //    if (result) device.Registers = registers;
        //    return result;
        //}

        //[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
        //internal static ushort[] ReadRegisters(this USBDevice device, ReportType report)
        //{
        //    var result = default(ushort[]);
        //    if (!device.IsInvalid)
        //    {
        //        var buffer = new byte[device.FeatureReportLength];
        //        buffer[0] = (byte)report;
        //        var reportLength = 0;
        //        switch (report)
        //        {
        //            case ReportType.StatusRssi:
        //            case ReportType.ReadChan:
        //                reportLength = 1;
        //                break;
        //            case ReportType.RDS: reportLength = Native.RDS_REGISTER_NUM; break;
        //            case ReportType.Entire: reportLength = Native.FMRADIO_REGISTER_NUM; break;
        //            case ReportType.Scratch: reportLength = Native.SCRATCH_PAGE_SIZE; break;
        //        }
        //        result = new ushort[reportLength];
        //        if (reportLength <= (device.FeatureReportLength - 1) / Native.FMRADIO_REGISTER_SIZE)
        //        {
        //            if (report >= ReportType.DeviceID && report <= ReportType.Entire)
        //            {
        //                var hEPBuffer = Marshal.AllocHGlobal(buffer.Length);
        //                Marshal.Copy(buffer, 0, hEPBuffer, buffer.Length);
        //                if (USBDevice.Native.HidD_GetFeature(device.DangerousGetHandle(), hEPBuffer, (uint)buffer.Length))
        //                {
        //                    Marshal.Copy(hEPBuffer, buffer, 0, buffer.Length);
        //                    for (byte i = 0; i < result.Length; i++)
        //                    {
        //                        result[i] = (ushort)((buffer[(i * 2) + 1] << 8) | buffer[(i * 2) + 2]);
        //                    }
        //                }
        //                Marshal.FreeHGlobal(hEPBuffer);
        //            }
        //            else if (report == ReportType.RDS)
        //            {
        //                buffer = new byte[Native.RDS_REPORT_SIZE];
        //                buffer[0] = (byte)ReportType.RDS;
        //                uint bytesRead;
        //                Native.OVERLAPPED o = new Native.OVERLAPPED();
        //                o.hEvent = Native.CreateEventW(IntPtr.Zero, false, false, string.Empty);
        //                IntPtr hOverlapped = Marshal.AllocHGlobal(Marshal.SizeOf(o));
        //                Marshal.StructureToPtr(o, hOverlapped, true);
        //                if (!Native.ReadFile(device.DangerousGetHandle(), buffer, (uint)buffer.Length, out bytesRead, hOverlapped))
        //                {
        //                    var error = Marshal.GetLastWin32Error();
        //                    if (error == Native.ERROR_IO_PENDING)
        //                    {
        //                        if (Native.WaitForSingleObject(o.hEvent, 3000))
        //                        {
        //                            Native.GetOverlappedResult(device.DangerousGetHandle(), ref o, out bytesRead, false);
        //                        }
        //                    }
        //                }
        //                Marshal.FreeHGlobal(hOverlapped);
        //                for (byte i = 0; i < result.Length; i++)
        //                {
        //                    result[i] = (ushort)((buffer[(i * 2) + 1] << 8) | buffer[(i * 2) + 2]);
        //                }
        //            }
        //        }
        //        else if (reportLength <= (device.FeatureReportLength - 1))
        //        {
        //            var hEPBuffer = Marshal.AllocHGlobal(device.FeatureReportLength);
        //            Marshal.Copy(buffer, 0, hEPBuffer, buffer.Length);
        //            if (USBDevice.Native.HidD_GetFeature(device.DangerousGetHandle(), hEPBuffer, device.FeatureReportLength))
        //            {
        //                Marshal.Copy(hEPBuffer, buffer, 0, buffer.Length);
        //                for (byte i = 0; i < result.Length; i++)
        //                {
        //                    result[i] = (ushort)buffer[i + 1];
        //                }
        //            }
        //            Marshal.FreeHGlobal(hEPBuffer);
        //        }
        //    }
        //    return result;
        //}

    }
}
