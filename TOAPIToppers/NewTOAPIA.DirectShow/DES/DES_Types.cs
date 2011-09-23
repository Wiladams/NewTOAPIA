using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace NewTOAPIA.DirectShow.DES
{
    /// <summary>
    /// From AM_MEDIA_TYPE 
    /// When you are done with an instance of this class,
    /// you can call Dispose(), to avoid leaking
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class AMMediaType : IDisposable
    {
        public Guid majorType;
        public Guid subType;

        [MarshalAs(UnmanagedType.Bool)]
        public bool fixedSizeSamples;
        
        [MarshalAs(UnmanagedType.Bool)]
        public bool temporalCompression;
        
        public int sampleSize=1;
        public Guid formatType;
        public IntPtr unkPtr; // IUnknown Pointer
        public int formatSize;
        public IntPtr formatPtr; // Pointer to a buff determined by formatType

        // dispose
        public void Dispose()
        {
            if (formatSize != 0)
                Marshal.FreeCoTaskMem(formatPtr);
            if (unkPtr != IntPtr.Zero)
                Marshal.Release(unkPtr);
        }
    }

    /// <summary>
    /// From DEXTER_PARAM
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct DexterParam
    {
        [MarshalAs(UnmanagedType.BStr)]
        public string Name;
        public int dispID;
        public int nValues;
    }

    /// <summary>
    /// From DEXTER_VALUE
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct DexterValue
    {
        [MarshalAs(UnmanagedType.Struct)]
        public object v;
        public long rt;
        public Dexterf dwInterp;
    }

    /// <summary>
    /// DirectShowLib.DsLong is a wrapper class around a <see cref="System.Int64"/> value type.
    /// </summary>
    /// <remarks>
    /// This class is necessary to enable null paramters passing.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public class DsLong
    {
        private long Value;

        /// <summary>
        /// Constructor
        /// Initialize a new instance of DirectShowLib.DsLong with the Value parameter
        /// </summary>
        /// <param name="Value">Value to assign to this new instance</param>
        public DsLong(long Value)
        {
            this.Value = Value;
        }

        /// <summary>
        /// Get a string representation of this DirectShowLib.DsLong Instance.
        /// </summary>
        /// <returns>A string representing this instance</returns>
        public override string ToString()
        {
            return this.Value.ToString();
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        /// <summary>
        /// Define implicit cast between DirectShowLib.DsLong and System.Int64 for languages supporting this feature.
        /// VB.Net doesn't support implicit cast. <see cref="DirectShowLib.DsLong.ToInt64"/> for similar functionality.
        /// <code>
        ///   // Define a new DsLong instance
        ///   DsLong dsL = new DsLong(9876543210);
        ///   // Do implicit cast between DsLong and Int64
        ///   long l = dsL;
        ///
        ///   Console.WriteLine(l.ToString());
        /// </code>
        /// </summary>
        /// <param name="g">DirectShowLib.DsLong to be cast</param>
        /// <returns>A casted System.Int64</returns>
        public static implicit operator long(DsLong l)
        {
            return l.Value;
        }

        /// <summary>
        /// Define implicit cast between System.Int64 and DirectShowLib.DsLong for languages supporting this feature.
        /// VB.Net doesn't support implicit cast. <see cref="DirectShowLib.DsGuid.FromInt64"/> for similar functionality.
        /// <code>
        ///   // Define a new Int64 instance
        ///   long l = 9876543210;
        ///   // Do implicit cast between Int64 and DsLong
        ///   DsLong dsl = l;
        ///
        ///   Console.WriteLine(dsl.ToString());
        /// </code>
        /// </summary>
        /// <param name="g">System.Int64 to be cast</param>
        /// <returns>A casted DirectShowLib.DsLong</returns>
        public static implicit operator DsLong(long l)
        {
            return new DsLong(l);
        }

        /// <summary>
        /// Get the System.Int64 equivalent to this DirectShowLib.DsLong instance.
        /// </summary>
        /// <returns>A System.Int64</returns>
        public long ToInt64()
        {
            return this.Value;
        }

        /// <summary>
        /// Get a new DirectShowLib.DsLong instance for a given System.Int64
        /// </summary>
        /// <param name="g">The System.Int64 to wrap into a DirectShowLib.DsLong</param>
        /// <returns>A new instance of DirectShowLib.DsLong</returns>
        public static DsLong FromInt64(long l)
        {
            return new DsLong(l);
        }
    }

    /// <summary>
    /// From FILTER_INFO
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct FilterInfo
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string achName;
        [MarshalAs(UnmanagedType.Interface)]
        public IFilterGraph pGraph;
    }

    /// <summary>
    /// From PIN_INFO
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct PinInfo
    {
        [MarshalAs(UnmanagedType.Interface)]
        public IBaseFilter filter;
        public PinDirection dir;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string name;
    }

}
