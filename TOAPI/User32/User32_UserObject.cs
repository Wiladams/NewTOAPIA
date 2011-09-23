using System;
using System.Runtime.InteropServices;

namespace TOAPI.User32
{
    public partial class User32
    {
        // Desktops and Window Sessions
        // GetUserObjectInformation
        //[DllImport("user32.dll", SetLastError = true)]
        //[return: MarshalAsAttribute(UnmanagedType.Bool)]
        //public static extern bool GetUserObjectInformation(IntPtr hObj, int nIndex, [Out] byte[] pvInfo, int nLength, out uint lpnLengthNeeded);

        [DllImport("user32.dll", SetLastError = true, EntryPoint = "GetUserObjectInformationW")]
        public static extern int GetUserObjectInformation(IntPtr hObj, int nIndex, IntPtr pvInfo, int nLength, [Out] out int lpnLengthNeeded);
        
        //[DllImport("user32.dll", SetLastError = true)]
        //[return: MarshalAsAttribute(UnmanagedType.Bool)]
        //public static extern bool GetUserObjectInformation([In] IntPtr hObj, int nIndex, IntPtr pvInfo, int nLength, IntPtr lpnLengthNeeded);


        public static int GetUserObjectInformationW([In] IntPtr hObj, int nIndex, object pvInfo, int nLength, out int LengthNeeded)
        {
            int flagsLength;
            int retValue;

            // First figure out how much length is needed
            retValue = GetUserObjectInformationW(hObj, UOI_FLAGS, IntPtr.Zero, 0, out LengthNeeded);

            // Set the length to the length needed
            flagsLength = LengthNeeded;

            // Now make the call again with the right size and structure
            GCHandle h0 = GCHandle.Alloc(pvInfo, GCHandleType.Pinned);
            try
            {
                retValue = GetUserObjectInformationW(hObj, UOI_FLAGS, h0.AddrOfPinnedObject(), flagsLength, out LengthNeeded);
            }
            finally
            {
                h0.Free();
            }

            return retValue;
        }

        // GetUserObjectSecurity
        [DllImport("user32.dll", EntryPoint = "GetUserObjectSecurity")]
        public static extern int GetUserObjectSecurity([System.Runtime.InteropServices.InAttribute()] IntPtr hObj, [In] ref uint pSIRequested, System.IntPtr pSID, uint nLength, [System.Runtime.InteropServices.OutAttribute()] out uint lpnLengthNeeded);

        // SetUserObjectInformation
        [DllImport("user32.dll", EntryPoint = "SetUserObjectInformationW")]
        public static extern int SetUserObjectInformationW(IntPtr hObj, int nIndex, [In] IntPtr pvInfo, uint nLength);

        // SetUserObjectSecurity
        [DllImport("user32.dll", EntryPoint = "SetUserObjectSecurity")]
        public static extern int SetUserObjectSecurity(IntPtr hObj, [In] ref uint pSIRequested, [In] IntPtr pSID);

    }
}
