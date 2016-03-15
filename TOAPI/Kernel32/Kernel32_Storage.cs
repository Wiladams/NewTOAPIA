using System;
using System.Runtime.InteropServices;
using System.Text;

namespace TOAPI.Kernel32
{
    public partial class Kernel32
    {
        //GetDiskFreeSpace Retrieves information about the specified disk, including the amount of free space on the disk. 
        [DllImport("kernel32.dll", CharSet=CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetDiskFreeSpace([In] string lpRootPathName, 
            ref int lpSectorsPerCluster, 
            ref int lpBytesPerSector, 
            ref int lpNumberOfFreeClusters, 
            ref int lpTotalNumberOfClusters);
        
        //GetDiskFreeSpaceEx Retrieves information about the specified disk, including the amount of free space on the disk. 
        [DllImport("kernel32.dll", CharSet=CharSet.Ansi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetDiskFreeSpaceEx([In] string lpDirectoryName, 
            ref ulong lpFreeBytesAvailableToCaller, 
            ref ulong lpTotalNumberOfBytes, 
            ref ulong lpTotalNumberOfFreeBytes);



        //The following functions are used in volume management
        //DefineDosDevice Defines, redefines, or deletes MS-DOS device names. 
        [DllImport("kernel32.dll")]
        static extern bool DefineDosDevice(uint dwFlags, string lpDeviceName,
           string lpTargetPath);

        //QueryDosDevice Retrieves information about MS-DOS device names. 


        //GetDriveType Determines whether a disk drive is a removable, fixed, CD-ROM, RAM disk, or network drive. 
        [DllImport("kernel32.dll")]
        public static extern DriveType GetDriveType([MarshalAs(UnmanagedType.LPStr)] string lpRootPathName);

        //GetLogicalDrives Retrieves a bitmask representing the currently available disk drives. 
        [DllImport("kernel32.dll")]
        static extern uint GetLogicalDrives();

        //GetLogicalDriveStrings Fills a buffer with strings that specify valid drives in the system. 
        // static extern uint GetLogicalDriveStrings(uint nBufferLength, 
        //    [Out] StringBuilder lpBuffer); --- Don't do this!

        // if we were to use the StringBuilder, only the first string would be returned
        // so, since arrays are reference types, we can pass an array of chars
        // just initialize it prior to call the function as
        // char[] lpBuffer = new char[nBufferLength];
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern uint GetLogicalDriveStrings(uint nBufferLength, [Out] char[] lpBuffer);


        //GetVolumeInformation Retrieves information about the file system and volume associated with the specified root directory. 
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetVolumeInformation(
          string RootPathName,
          StringBuilder VolumeNameBuffer,
          int VolumeNameSize,
          out uint VolumeSerialNumber,
          out uint MaximumComponentLength,
          out uint FileSystemFlags,
          StringBuilder FileSystemNameBuffer,
          int nFileSystemNameSize);


        //GetVolumeInformationByHandleW Retrieves information about the file system and volume associated with the specified file.  
        
        //SetVolumeLabel Sets the label of a file system volume. 
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern bool SetVolumeLabel(string lpRootPathName, string lpVolumeName);




        //The following functions are used with volume mount points.

        //FindFirstVolume Retrieves the name of a volume on a computer. 
        [DllImport("kernel32.dll")]
        public static extern IntPtr FindFirstVolume([Out] StringBuilder lpszVolumeName,
           uint cchBufferLength);


        //FindNextVolume Continues a volume search started by a call to FindFirstVolume. 
        [DllImport("kernel32.dll")]
        public static extern bool FindNextVolume(IntPtr hFindVolume, 
            [Out] StringBuilder lpszVolumeName, uint cchBufferLength);
        
        //FindVolumeClose Closes the specified volume search handle. 
        [DllImport("kernel32.dll")]
        public static extern bool FindVolumeClose(IntPtr hFindVolume);


        //FindFirstVolumeMountPoint Retrieves the name of a volume mount point on the specified volume. 
        [DllImport("kernel32.dll", CharSet=CharSet.Auto)]
        public static extern IntPtr FindFirstVolumeMountPoint([In] string lpszRootPathName, 
            StringBuilder lpszVolumeMountPoint, uint cchBufferLength);


        //FindNextVolumeMountPoint Continues a volume mount point search started by a call to FindFirstVolumeMountPoint. 
        [DllImport("kernel32.dll", CharSet=CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FindNextVolumeMountPoint(IntPtr hFindVolumeMountPoint, StringBuilder lpszVolumeMountPoint, uint cchBufferLength);
        
        //FindVolumeMountPointClose Closes the specified mount-point search handle. 
        [DllImport("kernel32.dll", EntryPoint = "FindVolumeMountPointClose")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FindVolumeMountPointClose(IntPtr hFindVolumeMountPoint);

        //GetVolumeNameForVolumeMountPoint Retrieves the unique volume name for the specified volume mount point or root directory. 
        //GetVolumePathName Retrieves the volume mount point at which the specified path is mounted. 
        //GetVolumePathNamesForVolumeName Retrieves a list of path names for the specified volume name. 
        
        //DeleteVolumeMountPoint Unmounts the volume from the specified volume mount point. 
        //SetVolumeMountPoint Mounts the specified volume at the specified volume mount point. 
    }
}
