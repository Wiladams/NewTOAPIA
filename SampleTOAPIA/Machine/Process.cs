using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

using TOAPI.Types;
using TOAPI.Kernel32;

public enum ProcessRights
{
    PROCESS_TERMINATE = (0x0001),       // Required to terminate a process using TerminateProcess. 
    PROCESS_CREATE_THREAD = (0x0002),   // Required to create a thread. 
    PROCESS_VM_OPERATION = (0x0008),    // Required to perform an operation on the address space of a process (see VirtualProtectEx and WriteProcessMemory). 
    PROCESS_VM_READ = (0x0010),         // Required to read memory in a process using ReadProcessMemory. 
    PROCESS_VM_WRITE = (0x0020),        // Required to write to memory in a process using WriteProcessMemory. 
    PROCESS_DUP_HANDLE = (0x0040),      // Required to duplicate a handle using DuplicateHandle. 
    PROCESS_CREATE_PROCESS = (0x0080),  // Required to create a process. 
    PROCESS_SET_QUOTA = (0x0100),       // Required to set memory limits using SetProcessWorkingSetSize. 
    PROCESS_SET_INFORMATION = (0x0200), // Required to set certain information about a process, such as its priority class (see SetPriorityClass). 
    PROCESS_QUERY_INFORMATION = (0x0400), // Required to retrieve certain information about a process, such as its token, exit code, and priority class (see OpenProcessToken, GetExitCodeProcess, GetPriorityClass, and IsProcessInJob). 
    PROCESS_SUSPEND_RESUME = (0x0800),  // Required to suspend or resume a process. 
    PROCESS_QUERY_LIMITED_INFORMATION = (0x1000), // Required to retrieve certain information about a process (see QueryFullProcessImageName). If you are granted PROCESS_QUERY_INFORMATION, you are also granted PROCESS_QUERY_LIMITED_INFORMATION. 
    SYNCHRONIZE = (0x00100000),        // Required to wait for the process to terminate using the wait functions. 
    PROCESS_ALL_ACCESS = (0x1F0FFF),    // All possible access rights for a process object. 
}

public class Process
{
    IntPtr fProcessHandle;
    uint fProcessID;

    public Process()
        :this(Kernel32.GetCurrentProcessId())
    {
    }

    public Process(uint procID, IntPtr procHandle)
    {
        fProcessHandle = procHandle;
        fProcessID = procID;
    }

    public Process(uint procID)
    {
        fProcessID = procID;
        fProcessHandle = Kernel32.OpenProcess((int)(ProcessRights.PROCESS_QUERY_INFORMATION | ProcessRights.PROCESS_VM_READ), 
            false, fProcessID);
    }

    public IntPtr ProcessHandle
    {
        get { return fProcessHandle; }
    }

    public int HandleCount
    {
        get
        {
            int handleCount = 0;
            bool succeeded = Kernel32.GetProcessHandleCount(ProcessHandle, out handleCount);

            return handleCount;
        }
    }

    public uint ID
    {
        get { return fProcessID; }
    }

    protected void Exit(int aCode)
    {
        Kernel32.ExitProcess(aCode);
    }

    public string ImageFileName
    {
        get
        {
            StringBuilder imageBuilder = new StringBuilder(256);
            int retValue = PSAPI.GetProcessImageFileName(ProcessHandle, imageBuilder, 256);

            return imageBuilder.ToString();
        }
    }

    public void Terminate(uint exitCode)
    {
        bool terminated = Kernel32.TerminateProcess(fProcessHandle, exitCode);
    }


    /// <summary>
    /// Get a list of all the processes running on the system at a given time.
    /// </summary>
    /// <returns>The list of all the processes on the system at the time of the call.</returns>
    public static List<PROCESSENTRY32> GetAllProcesses()
    {
        IntPtr snapshot = Kernel32.CreateToolhelp32Snapshot((uint)SnapshotType.TH32CS_SNAPPROCESS, 0);
        List<PROCESSENTRY32> procList = new List<PROCESSENTRY32>();

        PROCESSENTRY32 procEntry = new PROCESSENTRY32();
        procEntry.dwSize = (uint)Marshal.SizeOf(procEntry);  // don't forget to set the structure size

        bool success = Kernel32.Process32First(snapshot, ref procEntry);
        uint error = Kernel32.GetLastError();

        if (success)
        {
            procList.Add(procEntry);

            procEntry = new PROCESSENTRY32();
            procEntry.dwSize = (uint)Marshal.SizeOf(procEntry);

            while (Kernel32.Process32Next(snapshot, ref procEntry))
            {
                procList.Add(procEntry);
            }
        }

        Kernel32.CloseHandle(snapshot);

        return procList;
    }

    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();

        builder.Append("<process id='");
        builder.Append(fProcessID);
        builder.Append("'>");
        builder.Append("</process>");

        return builder.ToString();
    }
}
