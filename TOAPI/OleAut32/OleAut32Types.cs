using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace TOAPI.OleaAut32
{
    // The following useful structures are not defined
    // in the System.Runtime.InteropServices.ComTypes namespace
    public struct ARRAYDESC
    {
        public System.Runtime.InteropServices.ComTypes.TYPEDESC tdescElem;
        public ushort cDims;
        public IntPtr rgbounds;
    }

    public struct SAFEARRAYBOUND
    {
        public ulong cElements;
        public long lLbound;
    }

    public struct PARAMDESCEX
    {
        public IntPtr cByte;
        [MarshalAs(UnmanagedType.Struct)]
        public object varDefaultValue;
    }

}
