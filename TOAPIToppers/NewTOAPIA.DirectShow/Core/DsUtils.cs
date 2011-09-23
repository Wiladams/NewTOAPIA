using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace NewTOAPIA.DirectShow.Core
{
    using NewTOAPIA.DirectShow.DES;

    sealed public class DsUtils
    {
        private DsUtils()
        {
            // Prevent people from trying to instantiate this class
        }

        /// <summary>
        /// Returns the PinCategory of the specified pin.  Usually a member of PinCategory.  Not all pins have a category.
        /// </summary>
        /// <param name="pPin"></param>
        /// <returns>Guid indicating pin category or Guid.Empty on no category.  Usually a member of PinCategory</returns>
        public static Guid GetPinCategory(IPin pPin)
        {
            Guid guidRet = Guid.Empty;

            // Memory to hold the returned guid
            int iSize = Marshal.SizeOf(typeof(Guid));
            IntPtr ipOut = Marshal.AllocCoTaskMem(iSize);

            try
            {
                int hr;
                int cbBytes;
                Guid g = PropSetID.Pin;

                // Get an IKsPropertySet from the pin
                IKsPropertySet pKs = pPin as IKsPropertySet;

                if (pKs != null)
                {
                    // Query for the Category
                    hr = pKs.Get(g, (int)AMPropertyPin.Category, IntPtr.Zero, 0, ipOut, iSize, out cbBytes);
                    DsError.ThrowExceptionForHR(hr);

                    // Marshal it to the return variable
                    guidRet = (Guid)Marshal.PtrToStructure(ipOut, typeof(Guid));
                }
            }
            finally
            {
                Marshal.FreeCoTaskMem(ipOut);
                ipOut = IntPtr.Zero;
            }

            return guidRet;
        }


        /// <summary>
        ///  Free the nested interfaces within a PinInfo struct.
        /// </summary>
        public static void FreePinInfo(PinInfo pinInfo)
        {
            if (pinInfo.filter != null)
            {
                Marshal.ReleaseComObject(pinInfo.filter);
                pinInfo.filter = null;
            }
        }

        // Get filter name represented by the moniker string
        public static IMoniker GetMonikerFromMonikerString(string monikerString)
        {
            IBindCtx bindCtx = null;
            IMoniker moniker = null;
            int n = 0;

            // create bind context
            if (TOAPI.Ole.Ole32.CreateBindCtx(0, out bindCtx) == 0)
            {
                // convert moniker`s string to a moniker
                if (TOAPI.Ole.Ole32.MkParseDisplayName(bindCtx, monikerString, ref n, out moniker) == 0)
                {
                    return moniker;
                    //Marshal.ReleaseComObject(moniker);
                    //moniker = null;
                }
                Marshal.ReleaseComObject(bindCtx);
                bindCtx = null;
            }

            return moniker;
        }

    }



}
