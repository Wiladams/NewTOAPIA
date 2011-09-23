using System;
using System.Runtime.InteropServices;

using TOAPI.Types;

namespace TOAPI.Kernel32
{
    public partial class Kernel32
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern ushort GlobalAddAtom([In] string atomName);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern ushort GlobalDeleteAtom(ushort atom);
        
        // Basic memory management functions

        //CopyMemory Copies a block of memory from one location to another. 
        //CreateMemoryResourceNotification Creates a memory resource notification object. 
        //FillMemory Fills a block of memory with a specified value. 
        //GetLargePageMinimum Retrieves the minimum size of a large page. 
        //GetSystemFileCacheSize Retrieves the current size limits for the working set of the system cache. 
        //GetWriteWatch Retrieves the addresses of the pages that have been written to in a region of virtual memory. 
        //GlobalMemoryStatus Obtains information about the system's current usage of both physical and virtual memory. 
        //GlobalMemoryStatusEx Obtains information about the system's current usage of both physical and virtual memory. 
        //MoveMemory Moves a block of memory from one location to another. 
        [DllImport("Kernel32.dll", EntryPoint = "RtlMoveMemory", SetLastError = false)]
        static extern void MoveMemory(IntPtr dest, IntPtr src, int size);
        
        //QueryMemoryResourceNotification Retrieves the state of the specified memory resource object. 
        //ResetWriteWatch Resets the write-tracking state for a region of virtual memory. 
        //SecureZeroMemory Fills a block of memory with zeros. 
        //SetSystemFileCacheSize Limits the size of the working set for the file system cache. 
        
        //ZeroMemory Fills a block of memory with zeros. 
        [DllImport("Kernel32.dll", EntryPoint = "RtlZeroMemory", SetLastError = false)]
        static extern void ZeroMemory(IntPtr dest, int size);

        // File mapping is the association of a file's contents with a portion 
        // of the virtual address space of a process. The system creates a file 
        // mapping object (also known as a section object) to maintain this 
        // association. A file view is the portion of virtual address space 
        // that a process uses to access the file's contents. File mapping allows 
        // the process to use both random input and output (I/O) and sequential I/O. 
        // It also allows the process to work efficiently with a large data file, 
        // such as a database, without having to map the whole file into memory. 
        // Multiple processes can also use memory-mapped files to share data.

        //CreateFileMapping Creates or opens a named or unnamed file mapping object for a specified file. 
        [DllImport("kernel32.dll", EntryPoint = "CreateFileMappingW")]
        public static extern IntPtr CreateFileMappingW([In] IntPtr hFile,
            [In] ref SECURITY_ATTRIBUTES lpFileMappingAttributes, uint flProtect, 
            uint dwMaximumSizeHigh, uint dwMaximumSizeLow, 
            [In][MarshalAs(UnmanagedType.LPWStr)] string lpName);

        [DllImport("kernel32.dll", EntryPoint = "CreateFileMappingW")]
        public static extern IntPtr CreateFileMappingW([In] IntPtr hFile,
            [In] IntPtr lpFileMappingAttributes, uint flProtect,
            uint dwMaximumSizeHigh, uint dwMaximumSizeLow,
            [In][MarshalAs(UnmanagedType.LPWStr)] string lpName);


        //CreateFileMappingNuma Creates or opens a named or unnamed file mapping object for a specified file, and specifies the NUMA node for the physical memory. 
        
        //FlushViewOfFile Writes to the disk a byte range within a mapped view of a file. 
        [DllImport("kernel32.dll", EntryPoint = "FlushViewOfFile")]
        [return: MarshalAsAttribute(UnmanagedType.Bool)]
        public static extern bool FlushViewOfFile(IntPtr lpBaseAddress, uint dwNumberOfBytesToFlush);

        //GetMappedFileName Checks whether the specified address is within a memory-mapped file in the address space of the specified process. If so, the function returns the name of the memory-mapped file. 
        
        //MapViewOfFile Maps a view of a file mapping into the address space of a calling process. 
        [DllImport("kernel32.dll", EntryPoint = "MapViewOfFile")]
        public static extern IntPtr MapViewOfFile(IntPtr hFileMappingObject, 
            uint dwDesiredAccess, 
            uint dwFileOffsetHigh, uint dwFileOffsetLow, 
            uint dwNumberOfBytesToMap);

        //MapViewOfFileEx Maps a view of a file mapping into the address space of a calling process. A caller can optionally specify a suggested memory address for the view. 
        [DllImport("kernel32.dll", EntryPoint = "MapViewOfFileEx")]
        public static extern IntPtr MapViewOfFileEx(IntPtr hFileMappingObject, uint dwDesiredAccess, uint dwFileOffsetHigh, uint dwFileOffsetLow, uint dwNumberOfBytesToMap, IntPtr lpBaseAddress);

        //MapViewOfFileExNuma Maps a view of a file mapping into the address space of a calling process, and specifies the NUMA node for the physical memory. 
        
        //OpenFileMapping Opens a named file mapping object. 
        [DllImport("kernel32.dll", EntryPoint = "OpenFileMappingW")]
        public static extern IntPtr OpenFileMappingW(uint dwDesiredAccess, 
            [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, 
            [In][MarshalAsAttribute(UnmanagedType.LPWStr)] string lpName);

        //UnmapViewOfFile Unmaps a mapped view of a file from the calling process's address space. 
        [DllImport("kernel32.dll", EntryPoint = "UnmapViewOfFile")]
        [return: MarshalAsAttribute(UnmanagedType.Bool)]
        public static extern bool UnmapViewOfFile([In]IntPtr lpBaseAddress);




        // Address Windowing Extensions (AWE) is a set of extensions that allows an 
        // application to quickly manipulate physical memory greater than 4GB. Certain 
        // data-intensive applications, such as database management systems and scientific 
        // and engineering software, need access to very large caches of data. In the case 
        // of very large data sets, restricting the cache to fit within an application's 
        // 2GB of user address space is a severe restriction. In these situations, the 
        // cache is too small to properly support the application.

        //AllocateUserPhysicalPages Allocates physical memory pages to be mapped and unmapped within any AWE region of the process. 
        //FreeUserPhysicalPages Frees physical memory pages previously allocated with AllocateUserPhysicalPages. 
        //MapUserPhysicalPages Maps previously allocated physical memory pages at the specified address within an AWE region. 
        //MapUserPhysicalPagesScatter Maps previously allocated physical memory pages at the specified address within an AWE region. 


        // The heap functions enable a process to create a private heap, a block 
        // of one or more pages in the address space of the calling process. The 
        // process can then use a separate set of functions to manage the memory 
        // in that heap. There is no difference between memory allocated from a 
        // private heap and that allocated by using the other memory allocation functions. 

        //GetProcessHeap Obtains a handle to the heap of the calling process. 
        //GetProcessHeaps Obtains handles to all of the heaps that are valid for the calling process. 
        //HeapAlloc Allocates a block of memory from a heap. 
        //HeapCompact Attempts to compact a specified heap. 
        //HeapCreate Creates a heap object. 
        //HeapDestroy Destroys the specified heap object. 
        //HeapFree Frees a memory block allocated from a heap. 
        //HeapLock Attempts to acquire the lock associated with a specified heap. 
        //HeapQueryInformation Retrieves information about the specified heap. 
        //HeapReAlloc Reallocates a block of memory from a heap. 
        //HeapSetInformation Sets heap information for the specified heap. 
        //HeapSize Retrieves the size of a memory block allocated from a heap. 
        //HeapUnlock Releases ownership of the lock associated with a specified heap. 
        //HeapValidate Attempts to validate a specified heap. 
        //HeapWalk Enumerates the memory blocks in a specified heap. 

        // The virtual memory functions enable a process to manipulate or 
        // determine the status of pages in its virtual address space. They can perform the following operations:

        //VirtualAlloc Reserves or commits a region of pages in the virtual address space of the calling process. 
        //VirtualAllocEx Reserves or commits a region of pages in the virtual address space of the specified process. 
        //VirtualAllocExNuma Reserves or commits a region of memory within the virtual address space of the specified process, and specifies the NUMA node for the physical memory. 
        //VirtualFree Releases or decommits a region of pages within the virtual address space of the calling process. 
        //VirtualFreeEx Releases or decommits a region of memory within the virtual address space of a specified process. 
        //VirtualLock Locks the specified region of the process's virtual address space into physical memory. 
        //VirtualProtect Changes the access protection on a region of committed pages in the virtual address space of the calling process. 
        //VirtualProtectEx Changes the access protection on a region of committed pages in the virtual address space of the calling process. 
        //VirtualQuery Provides information about a range of pages in the virtual address space of the calling process. 
        //VirtualQueryEx Provides information about a range of pages in the virtual address space of the calling process. 
        //VirtualUnlock Unlocks a specified range of pages in the virtual address space of a process. 

        
        // The following are the global and local functions. These functions are slower 
        // than other memory management functions and do not provide as many features. 
        // Therefore, new applications should use the heap functions. However, the global 
        // functions are still used with DDE and the clipboard functions.

        //Function Description 
        //GlobalAlloc Allocates the specified number of bytes from the heap. 
        [DllImport("kernel32.dll", EntryPoint = "GlobalAlloc")]
        public static extern IntPtr GlobalAlloc(uint uFlags, uint dwBytes);

        //GlobalDiscard Discards the specified global memory block.

        //GlobalFlags Returns information about the specified global memory object. 
        [DllImport("kernel32.dll", EntryPoint = "GlobalFlags")]
        public static extern uint GlobalFlags(IntPtr hMem);
   
        //GlobalFree Frees the specified global memory object. 
        [DllImport("kernel32.dll")]
        public static extern int GlobalFree(IntPtr hMem);

        //GlobalHandle Retrieves the handle associated with the specified pointer to a global memory block. 
        [DllImport("kernel32.dll", EntryPoint = "GlobalHandle")]
        public static extern IntPtr GlobalHandle(IntPtr pMem);

        //GlobalLock Locks a global memory object and returns a pointer to the first byte of the object's memory block. 
        [DllImport("kernel32.dll", EntryPoint = "GlobalLock")]
        public static extern IntPtr GlobalLock(IntPtr hMem);

        //GlobalReAlloc Changes the size or attributes of a specified global memory object. 
        [DllImport("kernel32.dll", EntryPoint = "GlobalReAlloc")]
        public static extern IntPtr GlobalReAlloc(IntPtr hMem, uint dwBytes, uint uFlags);

        //GlobalSize Retrieves the current size of the specified global memory object. 
        [DllImport("kernel32.dll", EntryPoint = "GlobalSize")]
        public static extern uint GlobalSize(IntPtr hMem);
        
        //GlobalUnlock Decrements the lock count associated with a memory object. 
        [DllImport("kernel32.dll", EntryPoint = "GlobalUnlock")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GlobalUnlock(IntPtr hMem);

        //LocalAlloc Allocates the specified number of bytes from the heap. 
        [DllImport("kernel32.dll", EntryPoint = "LocalAlloc")]
        public static extern IntPtr LocalAlloc(uint uFlags, uint uBytes);

        //LocalDiscard Discards the specified local memory object. 
        // This one is a macro for Realloc

        //LocalFlags Returns information about the specified local memory object. 
        [DllImport("kernel32.dll", EntryPoint = "LocalFlags")]
        public static extern uint LocalFlags(IntPtr hMem);

        //LocalFree Frees the specified local memory object. 
        [DllImport("kernel32.dll", EntryPoint = "LocalFree")]
        public static extern IntPtr LocalFree(IntPtr hMem);

        //LocalHandle Retrieves the handle associated with the specified pointer to a local memory object. 
        [DllImport("kernel32.dll", EntryPoint = "LocalHandle")]
        public static extern IntPtr LocalHandle(IntPtr pMem);

        //LocalLock Locks a local memory object and returns a pointer to the first byte of the object's memory block. 
        [DllImport("kernel32.dll", EntryPoint = "LocalLock")]
        public static extern IntPtr LocalLock(IntPtr hMem);

        //LocalReAlloc Changes the size or the attributes of a specified local memory object. 
        [DllImport("kernel32.dll", EntryPoint = "LocalReAlloc")]
        public static extern IntPtr LocalReAlloc(IntPtr hMem, uint uBytes, uint uFlags);

        //LocalSize Returns the current size of the specified local memory object. 
        [DllImport("kernel32.dll", EntryPoint = "LocalSize")]
        public static extern uint LocalSize(IntPtr hMem);

        //LocalUnlock Decrements the lock count associated with a memory object.
        [DllImport("kernel32.dll", EntryPoint = "LocalUnlock")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool LocalUnlock(IntPtr hMem);

    }
}
