using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using TOAPI.Kernel32;

namespace Machiner
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            // Change the console window
            Terminal term = new Terminal();
            term.Title = "Terminal Window Title";
            term.DisplayMode = (int)ConsoleDisplayModeForSet.CONSOLE_FULLSCREEN_MODE;

            //Console.WriteLine("Terminal Title: {0}", term.Title);
            term.WriteLine("Terminal Window Title - has been set");
            COORD fontSize = term.FontSize;
            Console.WriteLine("Terminal Font Size: X = {0}, Y = {1}", fontSize.X, fontSize.Y);
            Console.WriteLine("mouse buttons: {0}", term.MouseButtons);

            // Instantiate a machine
            Machine aMachine = new Machine();

            // Print some properties
            Console.WriteLine("Name: {0}", aMachine.ShortName);
            Console.WriteLine("Domain: {0}", aMachine.DomainName);

            Environment environ = new Environment();
            Console.WriteLine("Command Line: {0}", environ.CommandLine);

            // Get the name of the process image that this process is running
            //Process aProc = new Process();
            //PrintProcess(aProc);
            //PrintAllProcesses();

            PrintAllDrives();
            //PrintVolumes();

            Console.ReadLine();
        }

        public static void PrintAllDrives()
        {
            string driveStrings = null;
            char[] chars = null;

            // First make the call with '0' as the buffer length
            // that will tell us how many bytes to allocate
            uint retValue = Kernel32.GetLogicalDriveStrings(0, chars);

            // Now allocate the actual number of bytes we need
            chars = new char[retValue+1];

            // Make the call one more time
            uint retValue2 = Kernel32.GetLogicalDriveStrings(retValue, chars);

            // Now, turn the null terminated array into a array of strings
            ArrayList stringArray = new ArrayList(32);
            int offset = 0;
            int position = 0;

            do
            {
                while (chars[position] != 0)
                    position++;
                string newString = new string(chars, offset, position - offset);
                stringArray.Add(newString);
                position++;
                offset = position;
            } while (position < retValue2);

            // Finally, print out the strings
            string[] stringsArray = new string[stringArray.Count];
            
            for (int ctr = 0; ctr <stringArray.Count; ctr++)
            {
                stringsArray[ctr] = (string)stringArray[ctr];
            }

            foreach (string str in stringsArray)
                PrintVolumeInformation(str);
        }

        public static void PrintVolumeInformation(string aVolume)
        {
            StringBuilder volumeName = new StringBuilder(256);
            StringBuilder fileSystemName = new StringBuilder(256);
            uint volumeSerialNumber;
            uint maxComponentLength;
            uint fileSystemFlags;

            Kernel32.GetVolumeInformation(aVolume,
                volumeName, 256,
                out volumeSerialNumber,
                out maxComponentLength,
                out fileSystemFlags,
                fileSystemName, 256);

            Console.WriteLine("Volume: {0}", aVolume);
            Console.WriteLine("  Volume Name: {0}", volumeName.ToString());
            Console.WriteLine("  Serial Number: {0}", volumeSerialNumber);
            Console.WriteLine("  Max Comp Length: {0}", maxComponentLength);
            Console.WriteLine("  File Sys Flags: {0}", fileSystemFlags);
            Console.WriteLine("  File Sys Name: {0}", fileSystemName);

            ulong freeBytes=0;
            ulong totalBytes=0;
            ulong totalFreeBytes=0;

            Kernel32.GetDiskFreeSpaceEx(aVolume, ref freeBytes, ref totalBytes, ref totalFreeBytes);

            Console.WriteLine("  Total Bytes: {0}Mb", (totalBytes/1024)/1024);
            Console.WriteLine("  Total Free: {0}Mb", (totalFreeBytes/1024)/1024);
            Console.WriteLine("  Total Avail: {0}Mb", (freeBytes/1024)/1024);
        }

        public static void PrintVolumes()
        {
            const uint N = 1024;
            StringBuilder volume = new StringBuilder((int)N, (int)N);
        //    StringCollection ret = new StringCollection();
            IntPtr volume_handle = Kernel32.FindFirstVolume(volume, N);
            do
            {
                //Program.PrintVolumeInformation(volume.ToString());
                Console.WriteLine("Volume: {0}", volume.ToString());
        //        ret.Add(volume.ToString());

            } while (Kernel32.FindNextVolume(volume_handle, volume, N));

            Kernel32.FindVolumeClose(volume_handle);
        //    return ret;
        }


        public static void PrintVolumeMountPoints(string rootPath)
        {
            const uint N = 1024;
            StringBuilder MountPoint = new StringBuilder((int)N, (int)N);
            //    StringCollection ret = new StringCollection();
            IntPtr volume_handle = Kernel32.FindFirstVolumeMountPoint(rootPath, MountPoint, N);
            do
            {
                Console.WriteLine("Volume Mount Point: {0}", MountPoint.ToString());
            } while (Kernel32.FindNextVolumeMountPoint(volume_handle, MountPoint, N));

            Kernel32.FindVolumeMountPointClose(volume_handle);
        }


        public static void PrintProcess(Process aProcess)
        {
            Console.WriteLine("Process Information");
            Console.WriteLine("===================");

            Console.WriteLine("Image File Name: {0}", aProcess.ImageFileName);
            Console.WriteLine("Handle Count: {0}", aProcess.HandleCount);
        }

        static void PrintProcEntry(PROCESSENTRY32 procEntry)
        {
            Console.WriteLine("Process Entry");
            Console.WriteLine("=============================");
            Console.WriteLine("File: {0}",procEntry.szExeFile);
            Console.WriteLine("Thread Count: {0}", procEntry.cntThreads);
            Console.WriteLine("");
        }

        public static void PrintAllProcesses()
        {
            List<PROCESSENTRY32> procList = Process.GetAllProcesses();

            foreach (PROCESSENTRY32 procEntry in procList)
            {
                PrintProcEntry(procEntry);
            }
        }
    }
}
