
namespace NewTOAPIA.COM
{
    using System;
    using System.Runtime.InteropServices;

    abstract public class Unknown
    {
        private readonly IntPtr m_comPointer;

        private delegate int AddRefDelegate(IntPtr comPointer);
        private delegate int ReleaseDelegate(IntPtr comPointer);
        private delegate int QueryInterfaceDelegate(IntPtr comPointer, ref Guid riid, IntPtr ppvObject);

        private AddRefDelegate AddRefMethod;
        private ReleaseDelegate ReleaseMethod;
        private QueryInterfaceDelegate QueryInterfaceMethod;

        protected Unknown(IntPtr comPointer)
        {
            m_comPointer = comPointer;
            SetupIUnknownMethods();
        }

        private void SetupIUnknownMethods()
        {
            /* Read the COM v-table pointer */
            IntPtr pVtable = Marshal.ReadIntPtr(m_comPointer);

            const int ADDREF_VTABLE_INDEX = 0;
            const int RELEASE_VTABLE_INDEX = 1;
            const int QUERYINTERFACE_VTABLE_INDEX = 2;

            /* Get the function pointer */
            IntPtr pFunc = Marshal.ReadIntPtr(pVtable, ADDREF_VTABLE_INDEX * IntPtr.Size);

            /* Cast the function pointer to a .NET delegate */
            AddRefMethod = (AddRefDelegate)Marshal.GetDelegateForFunctionPointer(pFunc, typeof(AddRefDelegate));

            /* Rinse and repeat */
            pFunc = Marshal.ReadIntPtr(pVtable, RELEASE_VTABLE_INDEX * IntPtr.Size);

            ReleaseMethod = (ReleaseDelegate)Marshal.GetDelegateForFunctionPointer(pFunc, typeof(ReleaseDelegate));

            pFunc = Marshal.ReadIntPtr(pVtable, QUERYINTERFACE_VTABLE_INDEX * IntPtr.Size);

            QueryInterfaceMethod = (QueryInterfaceDelegate)Marshal.GetDelegateForFunctionPointer(pFunc, typeof(QueryInterfaceDelegate));
        }

        public int AddRef()
        {
            return AddRefMethod(m_comPointer);
        }

        public int Release()
        {
            return ReleaseMethod(m_comPointer);
        }

        public int QueryInterface(IntPtr comPointer, ref Guid riid, IntPtr ppvObject)
        {
            return QueryInterfaceMethod(comPointer, ref riid, ppvObject);
        }

        public IntPtr ComPointer
        {
            get { return m_comPointer; }
        }
    }
}
