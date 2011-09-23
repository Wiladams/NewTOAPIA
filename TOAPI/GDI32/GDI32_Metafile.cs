using System;
using System.Runtime.InteropServices;
using System.Text;

using TOAPI.Types;

namespace TOAPI.GDI32
{
    //EnhMetaFileProc An application-defined callback function used with the EnumEnhMetaFile function. 
    public delegate int EnhMetaFileProc(IntPtr hdc, IntPtr lpht, [In()] ref ENHMETARECORD lpmr, int hHandles, IntPtr data);
   
    public partial class GDI32
    {
        //CloseEnhMetaFile Closes an enhanced-metafile device context. 
        [DllImport("gdi32.dll", EntryPoint = "CloseEnhMetaFile")]
        public static extern IntPtr CloseEnhMetaFile([In] SafeHandle hdc);

        //CopyEnhMetaFile Copies the contents of an enhanced-format metafile to a specified file. 
        [DllImport("gdi32.dll", EntryPoint = "CopyEnhMetaFileW")]
        public static extern IntPtr CopyEnhMetaFileW(IntPtr hEnh, [In()] [MarshalAs(UnmanagedType.LPWStr)] string lpFileName);

        //CreateEnhMetaFile Creates a device context for an enhanced-format metafile. 
        [DllImport("gdi32.dll", EntryPoint = "CreateEnhMetaFileW")]
        public static extern IntPtr CreateEnhMetaFileW([In] SafeHandle hdc, [In()] [MarshalAs(UnmanagedType.LPWStr)] string lpFilename, IntPtr lprc, [In()] [MarshalAs(UnmanagedType.LPWStr)] string lpDesc);

        //DeleteEnhMetaFile Deletes an enhanced-format metafile or an enhanced-format metafile handle. 
        [DllImport("gdi32.dll", EntryPoint = "DeleteEnhMetaFile")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteEnhMetaFile(IntPtr hmf);
        
        
        //EnumEnhMetaFile Enumerates the records within an enhanced-format metafile. 
        //        270  10D 00019B85 EnumEnhMetaFile
        [DllImport("gdi32.dll", EntryPoint = "EnumEnhMetaFile")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumEnhMetaFile([In] SafeHandle hdc, IntPtr hmf, EnhMetaFileProc proc, IntPtr param, IntPtr lpRect);

        //GdiComment Copies a comment from a buffer into a specified enhanced-format metafile. 
        
        //GetEnhMetaFile Creates a handle that identifies the enhanced-format metafile stored in the specified file. 
        [DllImport("gdi32.dll", EntryPoint = "GetEnhMetaFileW")]
        public static extern IntPtr GetEnhMetaFileW([In] [MarshalAs(UnmanagedType.LPWStr)] string lpName);

        //GetEnhMetaFileBits Retrieves the contents of the specified enhanced-format metafile and copies them into a buffer. 
        [DllImport("gdi32.dll", EntryPoint = "GetEnhMetaFileBits")]
        public static extern uint GetEnhMetaFileBits(IntPtr hEMF, uint nSize, IntPtr lpData);

        //GetEnhMetaFileDescription Retrieves an optional text description from an enhanced-format metafile and copies the string to the specified buffer. 
        [DllImport("gdi32.dll")]
        public static extern uint GetEnhMetaFileDescriptionW(IntPtr hemf, uint cchBuffer, [MarshalAs(UnmanagedType.LPWStr)] System.Text.StringBuilder lpDescription);

        [System.Diagnostics.DebuggerStepThrough()]
        [System.CodeDom.Compiler.GeneratedCode("InteropSignatureToolkit", "0.9 Beta1")]
        public static uint GetEnhMetaFileDescriptionW(IntPtr hemf, out string lpDescription)
        {
            System.Text.StringBuilder varlpDescription = new System.Text.StringBuilder(1024);
            uint methodRetVar;
            methodRetVar = GetEnhMetaFileDescriptionW(hemf, 1024, varlpDescription);
            lpDescription = varlpDescription.ToString();
            return methodRetVar;
        }

        //GetEnhMetaFileHeader Retrieves the record containing the header for the specified enhanced-format metafile. 
        [DllImport("gdi32.dll", EntryPoint = "GetEnhMetaFileHeader")]
        public static extern uint GetEnhMetaFileHeader(IntPtr hemf, uint nSize, IntPtr lpEnhMetaHeader);

        //GetEnhMetaFilePaletteEntries Retrieves optional palette entries from the specified enhanced metafile. 
        [DllImport("gdi32.dll", EntryPoint = "GetEnhMetaFilePaletteEntries")]
        public static extern uint GetEnhMetaFilePaletteEntries(IntPtr hemf, uint nNumEntries, IntPtr lpPaletteEntries);

        //GetWinMetaFileBits Converts the enhanced-format records from a metafile into Windows-format records. 
        [DllImport("gdi32.dll", EntryPoint = "GetWinMetaFileBits")]
        public static extern uint GetWinMetaFileBits(IntPtr hemf, uint cbData16, IntPtr pData16, int iMapMode, IntPtr hdcRef);

        //PlayEnhMetaFile Displays the picture stored in the specified enhanced-format metafile. 
        [DllImport("gdi32.dll", EntryPoint = "PlayEnhMetaFile")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PlayEnhMetaFile([In] SafeHandle hdc, IntPtr hmf, [In()] ref RECT lprect);

        //PlayEnhMetaFileRecord Plays an enhanced-metafile record by executing the graphics device interface (GDI) functions identified by the record. 
        [DllImport("gdi32.dll", EntryPoint = "PlayEnhMetaFileRecord")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PlayEnhMetaFileRecord([In] SafeHandle hdc, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Struct, SizeParamIndex = 3)] HANDLETABLE[] pht, [In()] ref ENHMETARECORD pmr, uint cht);

        //SetEnhMetaFileBits Creates a memory-based enhanced-format metafile from the specified data. 
        [DllImport("gdi32.dll", EntryPoint = "SetEnhMetaFileBits")]
        public static extern IntPtr SetEnhMetaFileBits(uint nSize, IntPtr pb);

        //SetWinMetaFileBits Converts a metafile from the older Windows format to the new enhanced format. 
        [DllImport("gdi32.dll", EntryPoint = "SetWinMetaFileBits")]
        public static extern IntPtr SetWinMetaFileBits(uint nSize, IntPtr lpMeta16Data, IntPtr hdcRef, IntPtr lpMFP);

    
        // These are obsolete
        // The could be implemented, and marked with an 'Obsolete' attribute guiding the user
        // towards the new functions.
        // Or, they can just be left out, and the user can figure out that they need to use 
        // the newer functions.
        //CloseMetaFile
        //CopyMetaFile
        //CreateMetaFile
        //DeleteMetaFile
        //EnumMetaFile
        //EnumMetaFileProc
        //GetMetaFileBitsEx
        //PlayMetaFile
        //PlayMetaFileRecord
        //SetMetaFileBitsEx 


    }
}
