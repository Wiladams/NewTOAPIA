using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;

using Microsoft.Win32.SafeHandles;

using TOAPI.HID;
using TOAPI.Kernel32;
using TOAPI.Setup;
using TOAPI.User32;

namespace USBHIDTest
{
    [SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
    [SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
    public class HIDDevice : IDisposable
    {
        #region Privates variables
        /// <summary>Filestream we can use to read/write from</summary>
        private FileStream m_oFile;
        /// <summary>Length of input report : device gives us this</summary>
        private int m_nInputReportLength;
        /// <summary>Length if output report : device gives us this</summary>
        private int m_nOutputReportLength;
        /// <summary>Handle to the device</summary>
        private SafeHandle m_hHandle;

        private Guid _hidGuid = Guid.Empty;


        #endregion

        [SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
        internal HIDDevice(uint pid, uint vid) 
        { 
            //findDevice(pid, vid); 
        }
        

        public uint ProductID { get; private set; }
        public uint VendorID { get; private set; }
        public uint VersionNumber { get; private set; }
        public string Name { get; private set; }
        public string SerialNumber { get; private set; }

        internal ushort FeatureReportLength { get; private set; }
        internal ushort[] Registers { get; set; }


        #region Publics
        /// <summary>
        /// Event handler called when device has been removed
        /// </summary>
        public event EventHandler OnDeviceRemoved;

        /// <summary>
        /// Accessor for output report length
        /// </summary>
        public int OutputReportLength
        {
            get
            {
                return m_nOutputReportLength;
            }
        }

        /// <summary>
        /// Accessor for input report length
        /// </summary>
        public int InputReportLength
        {
            get
            {
                return m_nInputReportLength;
            }
        }

        /// <summary>
        /// Virtual method to create an input report for this device. Override to use.
        /// </summary>
        /// <returns>A shiny new input report</returns>
        public virtual InputReport CreateInputReport()
        {
            return null;
        }
        #endregion


        #region Public static
        /// <summary>
        /// Finds a device given its PID and VID
        /// </summary>
        /// <param name="nVid">Vendor id for device (VID)</param>
        /// <param name="nPid">Product id for device (PID)</param>
        /// <param name="oType">Type of device class to create</param>
        /// <returns>A new device class of the given type or null</returns>
        public static HIDDevice FindDevice(int nVid, int nPid, Type oType)
        {
            string strPath = string.Empty;
            string strSearch = string.Format("vid_{0:x4}&pid_{1:x4}", nVid, nPid); // first, build the path search string
            Guid gHid;
            Hid.HidD_GetHidGuid(out gHid);	// next, get the GUID from Windows that it uses to represent the HID USB interface
            IntPtr hInfoSet = Setup.SetupDiGetClassDevs(ref gHid, null, IntPtr.Zero, Setup.DIGCF_DEVICEINTERFACE | Setup.DIGCF_PRESENT);	// this gets a list of all HID devices currently connected to the computer (InfoSet)
            
            try
            {
                SP_DEVICE_INTERFACE_DATA oInterface = new SP_DEVICE_INTERFACE_DATA();	// build up a device interface data block
                oInterface.cbSize = Marshal.SizeOf(oInterface);
                
                // Now iterate through the InfoSet memory block assigned within Windows in the call to SetupDiGetClassDevs
                // to get device details for each device connected
                int nIndex = 0;
                while (Setup.SetupDiEnumDeviceInterfaces(hInfoSet, IntPtr.Zero, ref gHid, nIndex, ref oInterface))	// this gets the device interface information for a device at index 'nIndex' in the memory block
                {
                    string strDevicePath = GetDevicePath(hInfoSet, ref oInterface);	// get the device path (see helper method 'GetDevicePath')
                    if (strDevicePath.IndexOf(strSearch) >= 0)	// do a string search, if we find the VID/PID string then we found our device!
                    {
                        HIDDevice oNewDevice = (HIDDevice)Activator.CreateInstance(oType);	// create an instance of the class for this device
                        oNewDevice.Initialise(strDevicePath);	// initialise it with the device path
                        return oNewDevice;	// and return it
                    }
                    nIndex++;	// if we get here, we didn't find our device. So move on to the next one.
                }
            }
            finally
            {
                // Before we go, we have to free up the InfoSet memory reserved by SetupDiGetClassDevs
                Setup.SetupDiDestroyDeviceInfoList(hInfoSet);
            }
            return null;	// oops, didn't find our device
        }
        #endregion

        #region Privates/protected
        /// <summary>
        /// Initialises the device
        /// </summary>
        /// <param name="strPath">Path to the device</param>
        private void Initialise(string strPath)
        {
            // Create the file from the device path
            m_hHandle = Kernel32.CreateFile(strPath, Kernel32.GENERIC_READ | Kernel32.GENERIC_WRITE, 0, IntPtr.Zero, Kernel32.OPEN_EXISTING, Kernel32.FILE_FLAG_OVERLAPPED, 0);
            
            if (!m_hHandle.IsInvalid)	// if the open worked...
            {
                IntPtr lpData;
                if (Hid.HidD_GetPreparsedData(m_hHandle, out lpData))	// get windows to read the device data into an internal buffer
                {
                    try
                    {
                        HIDP_CAPS oCaps;
                        Hid.HidP_GetCaps(lpData, out oCaps);	// extract the device capabilities from the internal buffer
                        m_nInputReportLength = oCaps.InputReportByteLength;	// get the input...
                        m_nOutputReportLength = oCaps.OutputReportByteLength;	// ... and output report lengths
                        //m_oFile = new FileStream(m_hHandle, FileAccess.Read | FileAccess.Write, true, m_nInputReportLength, true);	// wrap the file handle in a .Net file stream
                        m_oFile = new FileStream(new SafeFileHandle(m_hHandle.DangerousGetHandle(), false), FileAccess.Read | FileAccess.Write, m_nInputReportLength, true);	// wrap the file handle in a .Net file stream
                        BeginAsyncRead();	// kick off the first asynchronous read
                    }
                    finally
                    {
                        Hid.HidD_FreePreparsedData(lpData);	// before we quit the funtion, we must free the internal buffer reserved in GetPreparsedData
                    }
                }
                else	// GetPreparsedData failed? Chuck an exception
                {
                    throw HIDDeviceException.GenerateWithWinError("GetPreparsedData failed");
                }
            }
            else	// File open failed? Chuck an exception
            {
                m_hHandle.SetHandleAsInvalid();
                throw HIDDeviceException.GenerateWithWinError("Failed to create device file");
            }
        }
        /// <summary>
        /// Kicks off an asynchronous read which completes when data is read or when the device
        /// is disconnected. Uses a callback.
        /// </summary>
        private void BeginAsyncRead()
        {
            byte[] arrInputReport = new byte[m_nInputReportLength];
            // put the buff we used to receive the stuff as the async state then we can get at it when the read completes
            m_oFile.BeginRead(arrInputReport, 0, m_nInputReportLength, new AsyncCallback(ReadCompleted), arrInputReport);
        }
        /// <summary>
        /// Callback for above. Care with this as it will be called on the background thread from the async read
        /// </summary>
        /// <param name="iResult">Async result parameter</param>
        protected void ReadCompleted(IAsyncResult iResult)
        {
            byte[] arrBuff = (byte[])iResult.AsyncState;	// retrieve the read buffer
            try
            {
                m_oFile.EndRead(iResult);	// call end read : this throws any exceptions that happened during the read
                try
                {
                    InputReport oInRep = CreateInputReport();	// Create the input report for the device
                    oInRep.SetData(arrBuff);	// and set the data portion - this processes the data received into a more easily understood format depending upon the report type
                    HandleDataReceived(oInRep);	// pass the new input report on to the higher level handler
                }
                finally
                {
                    BeginAsyncRead();	// when all that is done, kick off another read for the next report
                }
            }
            catch (IOException)	// if we got an IO exception, the device was removed
            {
                HandleDeviceRemoved();
                if (OnDeviceRemoved != null)
                {
                    OnDeviceRemoved(this, new EventArgs());
                }
                Dispose();
            }
        }
        /// <summary>
        /// Write an output report to the device.
        /// </summary>
        /// <param name="oOutRep">Output report to write</param>
        protected void Write(OutputReport oOutRep)
        {
            try
            {
                m_oFile.Write(oOutRep.Buffer, 0, oOutRep.BufferLength);
            }
            catch (IOException)
            {
                // The device was removed!
                throw new HIDDeviceException("Device was removed");
            }
        }
        /// <summary>
        /// virtual handler for any action to be taken when data is received. Override to use.
        /// </summary>
        /// <param name="oInRep">The input report that was received</param>
        protected virtual void HandleDataReceived(InputReport oInRep)
        {
        }
        /// <summary>
        /// Virtual handler for any action to be taken when a device is removed. Override to use.
        /// </summary>
        protected virtual void HandleDeviceRemoved()
        {
        }
        /// <summary>
        /// Helper method to return the device path given a DeviceInterfaceData structure and an InfoSet handle.
        /// Used in 'FindDevice' so check that method out to see how to get an InfoSet handle and a DeviceInterfaceData.
        /// </summary>
        /// <param name="hInfoSet">Handle to the InfoSet</param>
        /// <param name="oInterface">DeviceInterfaceData structure</param>
        /// <returns>The device path or null if there was some problem</returns>
        private static string GetDevicePath(IntPtr hInfoSet, ref SP_DEVICE_INTERFACE_DATA oInterface)
        {
            int nRequiredSize = 0;

            // Get the device interface details
            if (!Setup.SetupDiGetDeviceInterfaceDetail(hInfoSet, ref oInterface, IntPtr.Zero, 0, ref nRequiredSize, IntPtr.Zero))
            {
                SP_DEVICE_INTERFACE_DETAIL_DATA oDetail = new SP_DEVICE_INTERFACE_DETAIL_DATA();
                oDetail.cbSize = 5;	// hardcoded to 5! Sorry, but this works and trying more future proof versions by setting the size to the struct sizeof failed miserably. If you manage to sort it, mail me! Thx
                if (Setup.SetupDiGetDeviceInterfaceDetail(hInfoSet, ref oInterface, ref oDetail, nRequiredSize, ref nRequiredSize, IntPtr.Zero))
                {
                    return oDetail.DevicePath;
                }
            }
            return null;
        }
        #endregion

        #region private
        //[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
        //private void findDevice(uint pid, uint vid)
        //{
        //    Hid.HidD_GetHidGuid(out _hidGuid);
        //    hHidDeviceInfo = Setup.SetupDiGetClassDevs(ref _hidGuid, IntPtr.Zero, IntPtr.Zero, Setup.DIGCF_PRESENT | Setup.DIGCF_DEVICEINTERFACE);
        //    if (hHidDeviceInfo.ToInt32() > -1)
        //    {
        //        uint i = 0;
        //        while (!isValid && i < MAX_USB_DEVICES)
        //        {
        //            var hidDeviceInterfaceData = new SP_DEVICE_INTERFACE_DATA();
        //            hidDeviceInterfaceData.cbSize = Marshal.SizeOf(hidDeviceInterfaceData);
        //            if (Setup.SetupDiEnumDeviceInterfaces(hHidDeviceInfo, IntPtr.Zero, ref _hidGuid, (int)i, ref hidDeviceInterfaceData))
        //            {
        //                bool detailResult;
        //                int length=0, required=0;
        //                Setup.SetupDiGetDeviceInterfaceDetail(hHidDeviceInfo, ref hidDeviceInterfaceData, IntPtr.Zero, 0, ref length, IntPtr.Zero);
        //                var hidDeviceInterfaceDetailData = new PSP_DEVICE_INTERFACE_DETAIL_DATA();
        //                hidDeviceInterfaceDetailData.cbSize = 5; //DWORD cbSize (size 4) + Char[0] (size 1) for 32bit only!
        //                var hDeviceInterfaceDetailData = Marshal.AllocHGlobal(Marshal.SizeOf(hidDeviceInterfaceDetailData));
        //                Marshal.StructureToPtr(hidDeviceInterfaceDetailData, hDeviceInterfaceDetailData, true);
        //                detailResult = Setup.SetupDiGetDeviceInterfaceDetail(hHidDeviceInfo, ref hidDeviceInterfaceData, hDeviceInterfaceDetailData, (int)length, ref required, IntPtr.Zero);
        //                Marshal.PtrToStructure(hDeviceInterfaceDetailData, hidDeviceInterfaceDetailData);
        //                if (detailResult)
        //                {
        //                    SafeFileHandle aHandle = Kernel32.CreateFile(hidDeviceInterfaceDetailData.DevicePath,
        //                       Kernel32.GENERIC_READ |
        //                       Kernel32.GENERIC_WRITE,
        //                       Kernel32.FILE_SHARE_READ |
        //                       Kernel32.FILE_SHARE_WRITE,
        //                       IntPtr.Zero,
        //                       Kernel32.OPEN_EXISTING,
        //                       Kernel32.FILE_FLAG_OVERLAPPED,
        //                       0);
        //                    base.handle = aHandle.DangerousGetHandle();

        //                    if (base.handle.ToInt32() > -1)
        //                    {
        //                        HIDD_ATTRIBUTES hidDeviceAttributes = new HIDD_ATTRIBUTES();
        //                        if (Hid.HidD_GetAttributes(this, ref hidDeviceAttributes))
        //                        {
        //                            if ((hidDeviceAttributes.VendorID == vid) && (hidDeviceAttributes.ProductID == pid))
        //                            {
        //                                isValid = true;
        //                                ProductID = pid;
        //                                VendorID = vid;
        //                                VersionNumber = (uint)hidDeviceAttributes.VersionNumber;
        //                                IntPtr buffer = Marshal.AllocHGlobal(126);//max alloc for string;
        //                                if (HidD_GetProductString(this.handle, buffer, 126)) Name = Marshal.PtrToStringAuto(buffer);
        //                                if (HidD_GetSerialNumberString(this.handle, buffer, 126)) SerialNumber = Marshal.PtrToStringAuto(buffer);
        //                                Marshal.FreeHGlobal(buffer);
        //                                var capabilities = new HIDP_CAPS();
        //                                IntPtr hPreparsedData = IntPtr.Zero;
        //                                if (Hid.HidD_GetPreparsedData(this, out hPreparsedData))
        //                                {
        //                                    if (Hid.HidP_GetCaps(hPreparsedData, out capabilities)>0) 
        //                                        FeatureReportLength = (ushort)capabilities.FeatureReportByteLength;
        //                                    Hid.HidD_FreePreparsedData(hPreparsedData);
        //                                }
        //                                break;
        //                            }
        //                        }
        //                        else
        //                        {
        //                            Kernel32.CloseHandle(base.handle);
        //                        }
        //                    }
        //                }
        //                Marshal.FreeHGlobal(hDeviceInterfaceDetailData);
        //            }
        //            i++;

        //        }
        //    }
        //}


        public static IntPtr RegisterForUsbEvents(IntPtr hWnd, Guid gClass)
        {
            DEV_BROADCAST_DEVICEINTERFACE oInterfaceIn = new DEV_BROADCAST_DEVICEINTERFACE();
            oInterfaceIn.dbcc_classguid = gClass;
            oInterfaceIn.dbcc_devicetype = TOAPI.Setup.Setup.DBT_DEVTYP_DEVICEINTERFACE;
            oInterfaceIn.dbcc_reserved = 0;
            
            return User32.RegisterDeviceNotification(hWnd, oInterfaceIn, Setup.DEVICE_NOTIFY_WINDOW_HANDLE);
        }

        /// <summary>
        /// Unregisters notifications. Can be used in form dispose
        /// </summary>
        /// <param name="hHandle">Handle returned from RegisterForUSBEvents</param>
        /// <returns>True if successful</returns>
        public static bool UnregisterForUsbEvents(IntPtr hHandle)
        {
            return User32.UnregisterDeviceNotification(hHandle);
        }

        /// <summary>
        /// Helper to get the HID guid.
        /// </summary>
        public static Guid HIDGuid
        {
            get
            {
                Guid gHid;
                Hid.HidD_GetHidGuid(out gHid);
                return gHid;
            }
        }

        //#region p/invoke
        //[SuppressUnmanagedCodeSecurity()]
        //internal static class Native
        //{
        //    #region methods
        //    [DllImport("hid.dll", SetLastError = true)]
        //    internal static extern void HidD_GetHidGuid(
        //       ref Guid lpHidGuid);

        //    [DllImport("hid.dll", SetLastError = true)]
        //    internal static extern bool HidD_GetAttributes(
        //       IntPtr hDevice,
        //       out HIDD_ATTRIBUTES Attributes);

        //    [DllImport("hid.dll", SetLastError = true)]
        //    internal static extern bool HidD_GetPreparsedData(
        //       IntPtr hDevice,
        //       out IntPtr hData);

        //    [DllImport("hid.dll", SetLastError = true)]
        //    internal static extern bool HidD_FreePreparsedData(
        //       IntPtr hData);

        //    [DllImport("hid.dll", SetLastError = true)]
        //    internal static extern bool HidP_GetCaps(
        //       IntPtr hData,
        //       out HIDP_CAPS capabilities);

        //    [DllImport("hid.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        //    internal static extern bool HidD_GetFeature(
        //       IntPtr hDevice,
        //       IntPtr hReportBuffer,
        //       uint ReportBufferLength);

        //    [DllImport("hid.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        //    internal static extern bool HidD_SetFeature(
        //       IntPtr hDevice,
        //       IntPtr ReportBuffer,
        //       uint ReportBufferLength);

        [DllImport("hid.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        internal static extern bool HidD_GetProductString(
           IntPtr hDevice,
           IntPtr Buffer,
           uint BufferLength);

        [DllImport("hid.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        internal static extern bool HidD_GetSerialNumberString(
           IntPtr hDevice,
           IntPtr Buffer,
           uint BufferLength);

        //    [DllImport("setupapi.dll", SetLastError = true)]
        //    internal static extern IntPtr SetupDiGetClassDevs(
        //       ref Guid ClassGuid,
        //       [MarshalAs(UnmanagedType.LPTStr)] string Enumerator,
        //       IntPtr hwndParent,
        //       UInt32 Flags);

        //    [DllImport("setupapi.dll", SetLastError = true)]
        //    internal static extern bool SetupDiEnumDeviceInterfaces(
        //       IntPtr DeviceInfoSet,
        //       int DeviceInfoData,
        //       ref  Guid lpHidGuid,
        //       uint MemberIndex,
        //       ref  SP_DEVICE_INTERFACE_DATA lpDeviceInterfaceData);

        //    [DllImport("setupapi.dll", SetLastError = true)]
        //    internal static extern bool SetupDiGetDeviceInterfaceDetail(
        //       IntPtr DeviceInfoSet,
        //       ref SP_DEVICE_INTERFACE_DATA lpDeviceInterfaceData,
        //       IntPtr hDeviceInterfaceDetailData,
        //       uint detailSize,
        //       out uint requiredSize,
        //       IntPtr hDeviceInfoData);

        //    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        //    [DllImport("kernel32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        //    internal static extern IntPtr CreateFile(
        //          string lpFileName,
        //          uint dwDesiredAccess,
        //          uint dwShareMode,
        //          IntPtr SecurityAttributes,
        //          uint dwCreationDisposition,
        //          uint dwFlagsAndAttributes,
        //          IntPtr hTemplateFile);

        //    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        //    [DllImport("kernel32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        //    internal static extern bool CloseHandle(IntPtr hHandle);
        //    #endregion
        //    #region structs
        //    [StructLayout(LayoutKind.Sequential)]
        //    internal struct SP_DEVICE_INTERFACE_DATA
        //    {
        //        public int cbSize;
        //        public Guid InterfaceClassGuid;
        //        public int Flags;
        //        public int Reserved;
        //    }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        internal class PSP_DEVICE_INTERFACE_DETAIL_DATA
        {
            public int cbSize;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string DevicePath;
        }

        //    [StructLayout(LayoutKind.Sequential)]
        //    internal struct HIDD_ATTRIBUTES
        //    {
        //        public int Size; // = sizeof (struct _HIDD_ATTRIBUTES) = 10
        //        public UInt16 VendorID;
        //        public UInt16 ProductID;
        //        public UInt16 VersionNumber;
        //    }
        //    [StructLayout(LayoutKind.Sequential)]
        //    internal struct HIDP_CAPS
        //    {
        //        public UInt16 Usage;
        //        public UInt16 UsagePage;
        //        public UInt16 InputReportByteLength;
        //        public UInt16 OutputReportByteLength;
        //        public UInt16 FeatureReportByteLength;
        //        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        //        public UInt16[] Reserved;
        //        public UInt16 NumberLinkCollectionNodes;
        //        public UInt16 NumberInputButtonCaps;
        //        public UInt16 NumberInputValueCaps;
        //        public UInt16 NumberInputDataIndices;
        //        public UInt16 NumberOutputButtonCaps;
        //        public UInt16 NumberOutputValueCaps;
        //        public UInt16 NumberOutputDataIndices;
        //        public UInt16 NumberFeatureButtonCaps;
        //        public UInt16 NumberFeatureValueCaps;
        //        public UInt16 NumberFeatureDataIndices;
        //    }
        //    #endregion
        //    #region constants
        //    internal const uint DIGCF_PRESENT = 0x00000002;
        //    internal const uint DIGCF_DEVICEINTERFACE = 0x00000010;
        //    internal const uint GENERIC_READ = 0x80000000;
        //    internal const uint GENERIC_WRITE = 0x40000000;
        //    internal const uint FILE_SHARE_READ = 0x00000001;
        //    internal const uint FILE_SHARE_WRITE = 0x00000002;
        //    internal const int OPEN_EXISTING = 3;
        //    internal const int FILE_FLAG_OVERLAPPED = 0x40000000;
        //    #endregion
        //}
        //#endregion
        #endregion

        #region IDisposable Members
        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// Disposer called by both dispose and finalise
        /// </summary>
        /// <param name="bDisposing">True if disposing</param>
        protected virtual void Dispose(bool bDisposing)
        {
            if (bDisposing)	// if we are disposing, need to close the managed resources
            {
                if (m_oFile != null)
                {
                    m_oFile.Close();
                    m_oFile = null;
                }
            }
            if (!m_hHandle.IsClosed)	// Dispose and finalize, get rid of unmanaged resources
            {
                m_hHandle.Close();
            }
        }
        #endregion


        /// <summary>Converts all significant properties of <see cref="USBDevice"/> to human readble string</summary>
        /// <returns>Human readble string</returns>
        public override string ToString()
        {
            return string.Format("{0} (Product:{1:x}, Vendor:{2:x}, Version:{3:x}, S/N:{4})", Name, ProductID, VendorID, VersionNumber, SerialNumber);
        }

    }



}
