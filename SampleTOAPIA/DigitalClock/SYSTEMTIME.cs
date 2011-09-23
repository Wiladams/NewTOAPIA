using System;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public struct SYSTEMTIME
{
	[MarshalAs(UnmanagedType.U2)]
	public short Year;
	[MarshalAs(UnmanagedType.U2)]
	public short Month;
	[MarshalAs(UnmanagedType.U2)]
	public short DayOfWeek;
	[MarshalAs(UnmanagedType.U2)]
	public short Day;
	[MarshalAs(UnmanagedType.U2)]
	public short Hour;
	[MarshalAs(UnmanagedType.U2)]
	public short Minute;
	[MarshalAs(UnmanagedType.U2)]
	public short Second;
	[MarshalAs(UnmanagedType.U2)]
	public short Milliseconds;
}



