using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.DirectShow
{
    using System.Runtime.InteropServices;

    // This abstract class contains definitions for use in implementing a custom marshaler.
    //
    // MarshalManagedToNative() gets called before the COM method, and MarshalNativeToManaged() gets
    // called after.  This allows for allocating a correctly sized memory block for the COM call,
    // then to break up the memory block and build an object that c# can digest.

    abstract internal class DsMarshaler : ICustomMarshaler
    {
        #region Data Members
        // The cookie isn't currently being used.
        protected string m_cookie;

        // The managed object passed in to MarshalManagedToNative, and modified in MarshalNativeToManaged
        protected object m_obj;
        #endregion

        // The constructor.  This is called from GetInstance (below)
        public DsMarshaler(string cookie)
        {
            // If we get a cookie, save it.
            m_cookie = cookie;
        }

        // Called just before invoking the COM method.  The returned IntPtr is what goes on the stack
        // for the COM call.  The input arg is the parameter that was passed to the method.
        virtual public IntPtr MarshalManagedToNative(object managedObj)
        {
            // Save off the passed-in value.  Safe since we just checked the type.
            m_obj = managedObj;

            // Create an appropriately sized buffer, blank it, and send it to the marshaler to
            // make the COM call with.
            int iSize = GetNativeDataSize() + 3;
            IntPtr p = Marshal.AllocCoTaskMem(iSize);

            for (int x = 0; x < iSize / 4; x++)
            {
                Marshal.WriteInt32(p, x * 4, 0);
            }

            return p;
        }

        // Called just after invoking the COM method.  The IntPtr is the same one that just got returned
        // from MarshalManagedToNative.  The return value is unused.
        virtual public object MarshalNativeToManaged(IntPtr pNativeData)
        {
            return m_obj;
        }

        // Release the (now unused) buffer
        virtual public void CleanUpNativeData(IntPtr pNativeData)
        {
            if (pNativeData != IntPtr.Zero)
            {
                Marshal.FreeCoTaskMem(pNativeData);
            }
        }

        // Release the (now unused) managed object
        virtual public void CleanUpManagedData(object managedObj)
        {
            m_obj = null;
        }

        // This routine is (apparently) never called by the marshaler.  However it can be useful.
        abstract public int GetNativeDataSize();

        // GetInstance is called by the marshaler in preparation to doing custom marshaling.  The (optional)
        // cookie is the value specified in MarshalCookie="asdf", or "" is none is specified.

        // It is commented out in this abstract class, but MUST be implemented in derived classes
        //public static ICustomMarshaler GetInstance(string cookie)
    }


}
