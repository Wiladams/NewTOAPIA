using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace TOAPI.OleaAut32
{
    /// <summary>
    /// Provides interop routines for OleAut32
    /// </summary>
    public class OleAut32
    {
        [DllImport("oleaut32.dll", PreserveSig = false)]
        static extern void GetActiveObject(ref Guid rclsid, IntPtr pvReserved,
           [MarshalAs(UnmanagedType.IUnknown)] out Object ppunk);
        
        [DllImport("oleaut32.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
        public static extern ITypeLib LoadTypeLibEx(string szFile, REGKIND regkind);

        [DllImport("oleaut32.dll", PreserveSig = false)]
        static extern void RegisterTypeLib(ITypeLib ptlib,
           [MarshalAs(UnmanagedType.BStr)] string szFullPath,
           [MarshalAs(UnmanagedType.BStr)] string szHelpDir);


    }
}