namespace NewTOAPIA.DirectShow.Core
{
	using System;
	using System.Runtime.InteropServices;
    using NewTOAPIA.DirectShow.DES;

	// IEnumPins interface
	//
	// Enumerates pins on a filter
	//
	[ComImport,
	Guid("56A86892-0AD4-11CE-B03A-0020AF0BA770"),
	InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IEnumPins
	{
		// Retrieves a specified number of pins
		[PreserveSig]
		int Next(
			[In] int cPins,
			[Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex=0)] IPin[] ppPins,
			[Out] out int pcFetched);

		// Skips over a specified number of pins
		[PreserveSig]
		int Skip(
			[In] int cPins);

		// Resets the enumeration sequence to the beginning
		[PreserveSig]
		int Reset();

		// Makes a copy of the enumerator with the same enumeration state
		[PreserveSig]
		void Clone(
			[Out] out IEnumPins ppEnum);
	}

}