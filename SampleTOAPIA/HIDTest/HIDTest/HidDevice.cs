namespace NewTOAPIA
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Security.AccessControl;

    using Microsoft.Win32.SafeHandles;

    using TOAPI.HID;
    using TOAPI.Kernel32;
    using TOAPI.Setup;
    using TOAPI.Types;

    public class HidDevice : IDisposable
    {
        // Basic data coming from the interface
        public short ProductID;
        public short VendorID;
        public string DevicePath;

        // Data coming from device query
        internal int inputReportLength;
        internal int outputReportLength;

        // Internally used fields
        /// <summary>
        /// Event handler called when device has been removed
        /// </summary>
        public event EventHandler OnDeviceRemoved;

        public SafeFileHandle DeviceFileHandle;     // Handle to the device
        private FileStream m_oFile;

        #region Constructors
        public HidDevice(string devicePathName)
        {
            Initialize(devicePathName);
        }

        void Initialize(string devicePathName)
        {
            DevicePath = devicePathName;

            // Get the vendor and product ids out of the path
            int startIndex = 0;

            startIndex = devicePathName.IndexOf("vid_", 0);
            string vendorIDstring = "0x" + devicePathName.Substring(startIndex + 4, 4);
            VendorID = Convert.ToInt16(vendorIDstring, 16);

            startIndex = devicePathName.IndexOf("pid_", 0);
            string productID = "0x" + devicePathName.Substring(startIndex + 4, 4);
            ProductID = Convert.ToInt16(productID, 16);

            // Try to open up the device
            DeviceFileHandle = Kernel32.CreateFile(DevicePath,
                (uint)(Kernel32.GENERIC_READ | Kernel32.GENERIC_WRITE),
                (uint)0,
                (IntPtr)IntPtr.Zero,
                (uint)Kernel32.OPEN_EXISTING,
                (uint)Kernel32.FILE_FLAG_OVERLAPPED,
                (IntPtr)IntPtr.Zero);
            //m_oFile = new FileStream(DevicePath,
            //    FileMode.Open, FileAccess.ReadWrite);

            // If we had a problem trying to open the device file handle
            // throw an exception
            if (DeviceFileHandle.IsInvalid)
            {
                DeviceFileHandle = null;
                return;
                //throw HIDDeviceException.GenerateWithWinError("Failed to create device file");
            }

            // Try to get the device capabilities
            IntPtr lpData = new IntPtr(0) ;
            if (Hid.HidD_GetPreparsedData(DeviceFileHandle, ref lpData))	// get windows to read the device data into an internal buffer
            {
                try
                {
                    HIDP_CAPS oCaps = new HIDP_CAPS();
                    
                    Hid.HidP_GetCaps(lpData, ref oCaps);	// extract the device capabilities from the internal buffer
                    inputReportLength = oCaps.InputReportByteLength;	// get the input...
                    outputReportLength = oCaps.OutputReportByteLength;	// ... and output report lengths

                    //m_oFile = new FileStream(m_hHandle, FileAccess.Read | FileAccess.Write, true, m_nInputReportLength, true);
                    m_oFile = new FileStream(DeviceFileHandle, FileAccess.Read | FileAccess.Write, inputReportLength, true);

                    BeginAsyncRead();	// kick off the first asynchronous read                              
                }
                catch (Exception ex)
                {
                    throw HIDDeviceException.GenerateWithWinError("Failed to get the detailed data from the hid.");
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
            try
            {
                if (bDisposing)	// if we are disposing, need to close the managed resources
                {
                    if (m_oFile != null)
                    {
                        m_oFile.Close();
                        m_oFile = null;
                    }
                }
                if (DeviceFileHandle != null)
                    DeviceFileHandle.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        #endregion

        #region IO
        /// <summary>
        /// Kicks off an asynchronous read which completes when data is read or when the device
        /// is disconnected. Uses a callback.
        /// </summary>
        private void BeginAsyncRead()
        {
            byte[] arrInputReport = new byte[inputReportLength];
            // put the buff we used to receive the stuff as the async state then we can get at it when the read completes

            m_oFile.BeginRead(arrInputReport, 0, inputReportLength, new AsyncCallback(ReadCompleted), arrInputReport);
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
            catch (IOException ex)	// if we got an IO exception, the device was removed
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
        /// virtual handler for any action to be taken when data is received. Override to use.
        /// </summary>
        /// <param name="oInRep">The input report that was received</param>
        protected virtual void HandleDataReceived(InputReport oInRep)
        {
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
            catch (IOException ex)
            {
                //Console.WriteLine(ex.ToString());
                // The device was removed!
                throw new HIDDeviceException("Probbaly the device was removed");
            }
            catch (Exception exx)
            {
                Console.WriteLine(exx.ToString());
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

        #region Device Management
        /// <summary>
        /// Virtual handler for any action to be taken when a device is removed. Override to use.
        /// </summary>
        protected virtual void HandleDeviceRemoved()
        {
        }

        #endregion

        public override string ToString()
        {
            return string.Format("<HID vid='{0}', pid='{1}'>\n\t<Vendor>{2}</Vendor>\n</HID>",
                Convert.ToString(VendorID, 16),
                Convert.ToString(ProductID,16),
                GetVendorName(VendorID));
        }

        #region Static Helpers
        public static Guid HIDGuid
        {
            get
            {
                Guid hidGuid = new Guid();
                Hid.HidD_GetHidGuid(ref hidGuid);

                return hidGuid;
            }
        }

        public static string GetVendorName(short vendorId)
        {
            if (Enum.IsDefined(typeof(HidVendors), vendorId))
            {
                return Enum.GetName(typeof(HidVendors), vendorId);
            }

            return Convert.ToString(vendorId, 16);
        }

        public static IEnumerable<HidDevice> GetDevices()
        {
            Guid hidGuid = HidDevice.HIDGuid;

            // Get the list of HID devices
            IntPtr deviceInfoSet = new IntPtr();
            deviceInfoSet = setup.SetupDiGetClassDevs(ref hidGuid, IntPtr.Zero, IntPtr.Zero, setup.DIGCF_PRESENT | setup.DIGCF_DEVICEINTERFACE);

            int memberIndex = 0;
            SP_DEVICE_INTERFACE_DATA MyDeviceInterfaceData = new SP_DEVICE_INTERFACE_DATA();
            MyDeviceInterfaceData.cbSize = Marshal.SizeOf(MyDeviceInterfaceData);

            bool success;
            bool lastDevice = false;
            int bufferSize = 0;
            UnmanagedMemory detailDataBuffer;
            List<HidDevice> devicePathNames = new List<HidDevice>();
            bool deviceFound = false;

            do
            {
                success = setup.SetupDiEnumDeviceInterfaces(deviceInfoSet,
                    IntPtr.Zero,
                    ref hidGuid,
                    memberIndex,
                    ref MyDeviceInterfaceData);

                if (!success)
                {
                    lastDevice = true;
                }
                else
                {
                    // First call to get size of data buffer
                    success = setup.SetupDiGetDeviceInterfaceDetail(deviceInfoSet,
                        ref MyDeviceInterfaceData,
                        IntPtr.Zero,
                        0,
                        ref bufferSize,
                        IntPtr.Zero);

                    // Allocate memory for the SP_DEVICE_INTERFACE_DETAIL_DATA structure using the returned buffer size
                    detailDataBuffer = new UnmanagedMemory(bufferSize);

                    // Store cbSize in the first bytes of the array. The number of bytes varies with 32- and 64-bit systems.

                    Marshal.WriteInt32(detailDataBuffer.MemoryPointer, (IntPtr.Size == 4) ? (4 + Marshal.SystemDefaultCharSize) : 8);

                    // Call SetupDiGetDeviceInterfaceDetail again.
                    // This time, pass a pointer to DetailDataBuffer
                    // and the returned required buffer size.
                    success = setup.SetupDiGetDeviceInterfaceDetail
                        (deviceInfoSet,
                        ref MyDeviceInterfaceData,
                        detailDataBuffer.MemoryPointer,
                        bufferSize,
                        ref bufferSize,
                        IntPtr.Zero);

                    //diDetail.cbSize = (IntPtr.Size == 4) ? (4 + Marshal.SystemDefaultCharSize) : 8;
                    //SP_DEVICE_INTERFACE_DETAIL_DATA diDetail = new SP_DEVICE_INTERFACE_DETAIL_DATA();
                    //diDetail.cbSize = Marshal.SizeOf(diDetail);
                    //success = setup.SetupDiGetDeviceInterfaceDetail
                    //    (deviceInfoSet,
                    //    ref MyDeviceInterfaceData,
                    //    ref diDetail,
                    //    diDetail.cbSize,
                    //    ref bufferSize,
                    //    IntPtr.Zero);

                    IntPtr pDevicePathName = detailDataBuffer.MemoryPointer + 4;
                    string devicePathName = Marshal.PtrToStringAuto(pDevicePathName);

                    HidDevice newDevice = new HidDevice(devicePathName);
                    devicePathNames.Add(newDevice);
                    deviceFound = true;
                }
                memberIndex++;
            } while (!lastDevice);

            // Destroy the device Info Set to cleanup memory
            setup.SetupDiDestroyDeviceInfoList(deviceInfoSet);

            return devicePathNames;

        }
        #endregion
    }
}