
namespace NewTOAPIA.DirectShow.DES
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    using NewTOAPIA.DirectShow;

    // c# does not correctly marshal arrays of pointers.
    //

    internal class EMTMarshaler : DsMarshaler
    {
        public EMTMarshaler(string cookie)
            : base(cookie)
        {
        }

        // Called just after invoking the COM method.  The IntPtr is the same one that just got returned
        // from MarshalManagedToNative.  The return value is unused.
        override public object MarshalNativeToManaged(IntPtr pNativeData)
        {
            AMMediaType[] emt = m_obj as AMMediaType[];

            for (int x = 0; x < emt.Length; x++)
            {
                // Copy in the value, and advance the pointer
                IntPtr p = Marshal.ReadIntPtr(pNativeData, x * IntPtr.Size);
                if (p != IntPtr.Zero)
                {
                    emt[x] = (AMMediaType)Marshal.PtrToStructure(p, typeof(AMMediaType));
                }
                else
                {
                    emt[x] = null;
                }
            }

            return null;
        }

        // The number of bytes to marshal out
        override public int GetNativeDataSize()
        {
            // Get the array size
            int i = ((Array)m_obj).Length;

            // Multiply that times the size of a pointer
            int j = i * IntPtr.Size;

            return j;
        }

        // This method is called by interop to create the custom marshaler.  The (optional)
        // cookie is the value specified in MarshalCookie="asdf", or "" is none is specified.
        public static ICustomMarshaler GetInstance(string cookie)
        {
            return new EMTMarshaler(cookie);
        }
    }
}
