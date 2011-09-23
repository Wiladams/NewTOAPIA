using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.DirectShow
{
    using System.Drawing;
    using System.Runtime.InteropServices;

    /// <summary>
    /// From ALLOCATOR_PROPERTIES
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class AllocatorProperties
    {
        public int cBuffers;
        public int cbBuffer;
        public int cbAlign;
        public int cbPrefix;
    }

    /// <summary>
    /// From COLORKEY
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class ColorKey
    {
        public int KeyType;
        public int PaletteIndex;
        public int LowColorValue;
        public int HighColorValue;
    }

    /// <summary>
    /// DirectShowLib.DsGuid is a wrapper class around a System.Guid value type.
    /// </summary>
    /// <remarks>
    /// This class is necessary to enable null paramters passing.
    /// </remarks>
    [StructLayout(LayoutKind.Explicit)]
    public class DsGuid
    {
        [FieldOffset(0)]
        private Guid guid;

        public static readonly DsGuid Empty = Guid.Empty;

        /// <summary>
        /// Empty constructor. 
        /// Initialize it with System.Guid.Empty
        /// </summary>
        public DsGuid()
        {
            this.guid = Guid.Empty;
        }

        /// <summary>
        /// Constructor.
        /// Initialize this instance with a given System.Guid string representation.
        /// </summary>
        /// <param name="g">A valid System.Guid as string</param>
        public DsGuid(string g)
        {
            this.guid = new Guid(g);
        }

        /// <summary>
        /// Constructor.
        /// Initialize this instance with a given System.Guid.
        /// </summary>
        /// <param name="g">A System.Guid value type</param>
        public DsGuid(Guid g)
        {
            this.guid = g;
        }

        /// <summary>
        /// Get a string representation of this DirectShowLib.DsGuid Instance.
        /// </summary>
        /// <returns>A string representing this instance</returns>
        public override string ToString()
        {
            return this.guid.ToString();
        }

        /// <summary>
        /// Get a string representation of this DirectShowLib.DsGuid Instance with a specific format.
        /// </summary>
        /// <param name="format"><see cref="System.Guid.ToString"/> for a description of the format parameter.</param>
        /// <returns>A string representing this instance according to the format parameter</returns>
        public string ToString(string format)
        {
            return this.guid.ToString(format);
        }

        public override int GetHashCode()
        {
            return this.guid.GetHashCode();
        }

        /// <summary>
        /// Define implicit cast between DirectShowLib.DsGuid and System.Guid for languages supporting this feature.
        /// VB.Net doesn't support implicit cast. <see cref="DirectShowLib.DsGuid.ToGuid"/> for similar functionality.
        /// <code>
        ///   // Define a new DsGuid instance
        ///   DsGuid dsG = new DsGuid("{33D57EBF-7C9D-435e-A15E-D300B52FBD91}");
        ///   // Do implicit cast between DsGuid and Guid
        ///   Guid g = dsG;
        ///
        ///   Console.WriteLine(g.ToString());
        /// </code>
        /// </summary>
        /// <param name="g">DirectShowLib.DsGuid to be cast</param>
        /// <returns>A casted System.Guid</returns>
        public static implicit operator Guid(DsGuid g)
        {
            return g.guid;
        }

        /// <summary>
        /// Define implicit cast between System.Guid and DirectShowLib.DsGuid for languages supporting this feature.
        /// VB.Net doesn't support implicit cast. <see cref="DirectShowLib.DsGuid.FromGuid"/> for similar functionality.
        /// <code>
        ///   // Define a new Guid instance
        ///   Guid g = new Guid("{B9364217-366E-45f8-AA2D-B0ED9E7D932D}");
        ///   // Do implicit cast between Guid and DsGuid
        ///   DsGuid dsG = g;
        ///
        ///   Console.WriteLine(dsG.ToString());
        /// </code>
        /// </summary>
        /// <param name="g">System.Guid to be cast</param>
        /// <returns>A casted DirectShowLib.DsGuid</returns>
        public static implicit operator DsGuid(Guid g)
        {
            return new DsGuid(g);
        }

        /// <summary>
        /// Get the System.Guid equivalent to this DirectShowLib.DsGuid instance.
        /// </summary>
        /// <returns>A System.Guid</returns>
        public Guid ToGuid()
        {
            return this.guid;
        }

        /// <summary>
        /// Get a new DirectShowLib.DsGuid instance for a given System.Guid
        /// </summary>
        /// <param name="g">The System.Guid to wrap into a DirectShowLib.DsGuid</param>
        /// <returns>A new instance of DirectShowLib.DsGuid</returns>
        public static DsGuid FromGuid(Guid g)
        {
            return new DsGuid(g);
        }
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
    /// DirectShowLib.DsInt is a wrapper class around a <see cref="System.Int32"/> value type.
    /// </summary>
    /// <remarks>
    /// This class is necessary to enable null paramters passing.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public class DsInt
    {
        private int Value;

        /// <summary>
        /// Constructor
        /// Initialize a new instance of DirectShowLib.DsInt with the Value parameter
        /// </summary>
        /// <param name="Value">Value to assign to this new instance</param>
        public DsInt(int Value)
        {
            this.Value = Value;
        }

        /// <summary>
        /// Get a string representation of this DirectShowLib.DsInt Instance.
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
        /// Define implicit cast between DirectShowLib.DsInt and System.Int64 for languages supporting this feature.
        /// VB.Net doesn't support implicit cast. <see cref="DirectShowLib.DsInt.ToInt64"/> for similar functionality.
        /// <code>
        ///   // Define a new DsInt instance
        ///   DsInt dsI = new DsInt(0x12345678);
        ///   // Do implicit cast between DsInt and Int32
        ///   int i = dsI;
        ///
        ///   Console.WriteLine(i.ToString());
        /// </code>
        /// </summary>
        /// <param name="g">DirectShowLib.DsInt to be cast</param>
        /// <returns>A casted System.Int32</returns>
        public static implicit operator int(DsInt l)
        {
            return l.Value;
        }

        /// <summary>
        /// Define implicit cast between System.Int32 and DirectShowLib.DsInt for languages supporting this feature.
        /// VB.Net doesn't support implicit cast. <see cref="DirectShowLib.DsGuid.FromInt32"/> for similar functionality.
        /// <code>
        ///   // Define a new Int32 instance
        ///   int i = 0x12345678;
        ///   // Do implicit cast between Int64 and DsInt
        ///   DsInt dsI = i;
        ///
        ///   Console.WriteLine(dsI.ToString());
        /// </code>
        /// </summary>
        /// <param name="g">System.Int32 to be cast</param>
        /// <returns>A casted DirectShowLib.DsInt</returns>
        public static implicit operator DsInt(int l)
        {
            return new DsInt(l);
        }

        /// <summary>
        /// Get the System.Int32 equivalent to this DirectShowLib.DsInt instance.
        /// </summary>
        /// <returns>A System.Int32</returns>
        public int ToInt32()
        {
            return this.Value;
        }

        /// <summary>
        /// Get a new DirectShowLib.DsInt instance for a given System.Int32
        /// </summary>
        /// <param name="g">The System.Int32 to wrap into a DirectShowLib.DsInt</param>
        /// <returns>A new instance of DirectShowLib.DsInt</returns>
        public static DsInt FromInt32(int l)
        {
            return new DsInt(l);
        }
    }

    /// <summary>
    /// DirectShowLib.DsShort is a wrapper class around a <see cref="System.Int16"/> value type.
    /// </summary>
    /// <remarks>
    /// This class is necessary to enable null paramters passing.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public class DsShort
    {
        private short Value;

        /// <summary>
        /// Constructor
        /// Initialize a new instance of DirectShowLib.DsShort with the Value parameter
        /// </summary>
        /// <param name="Value">Value to assign to this new instance</param>
        public DsShort(short Value)
        {
            this.Value = Value;
        }

        /// <summary>
        /// Get a string representation of this DirectShowLib.DsShort Instance.
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
        /// Define implicit cast between DirectShowLib.DsShort and System.Int16 for languages supporting this feature.
        /// VB.Net doesn't support implicit cast. <see cref="DirectShowLib.DsShort.ToInt64"/> for similar functionality.
        /// <code>
        ///   // Define a new DsShort instance
        ///   DsShort dsS = new DsShort(0x1234);
        ///   // Do implicit cast between DsShort and Int16
        ///   short s = dsS;
        ///
        ///   Console.WriteLine(s.ToString());
        /// </code>
        /// </summary>
        /// <param name="g">DirectShowLib.DsShort to be cast</param>
        /// <returns>A casted System.Int16</returns>
        public static implicit operator short(DsShort l)
        {
            return l.Value;
        }

        /// <summary>
        /// Define implicit cast between System.Int16 and DirectShowLib.DsShort for languages supporting this feature.
        /// VB.Net doesn't support implicit cast. <see cref="DirectShowLib.DsGuid.FromInt16"/> for similar functionality.
        /// <code>
        ///   // Define a new Int16 instance
        ///   short s = 0x1234;
        ///   // Do implicit cast between Int64 and DsShort
        ///   DsShort dsS = s;
        ///
        ///   Console.WriteLine(dsS.ToString());
        /// </code>
        /// </summary>
        /// <param name="g">System.Int16 to be cast</param>
        /// <returns>A casted DirectShowLib.DsShort</returns>
        public static implicit operator DsShort(short l)
        {
            return new DsShort(l);
        }

        /// <summary>
        /// Get the System.Int16 equivalent to this DirectShowLib.DsShort instance.
        /// </summary>
        /// <returns>A System.Int16</returns>
        public short ToInt16()
        {
            return this.Value;
        }

        /// <summary>
        /// Get a new DirectShowLib.DsShort instance for a given System.Int64
        /// </summary>
        /// <param name="g">The System.Int16 to wrap into a DirectShowLib.DsShort</param>
        /// <returns>A new instance of DirectShowLib.DsShort</returns>
        public static DsShort FromInt16(short l)
        {
            return new DsShort(l);
        }
    }

    public class DsRect
    {
        public int left;
        public int top;
        public int right;
        public int bottom;

        /// <summary>
        /// Empty contructor. Initialize all fields to 0
        /// </summary>
        public DsRect()
        {
            this.left = 0;
            this.top = 0;
            this.right = 0;
            this.bottom = 0;
        }

        /// <summary>
        /// A parametred constructor. Initialize fields with given values.
        /// </summary>
        /// <param name="left">the left value</param>
        /// <param name="top">the top value</param>
        /// <param name="right">the right value</param>
        /// <param name="bottom">the bottom value</param>
        public DsRect(int left, int top, int right, int bottom)
        {
            this.left = left;
            this.top = top;
            this.right = right;
            this.bottom = bottom;
        }

        /// <summary>
        /// A parametred constructor. Initialize fields with a given <see cref="System.Drawing.Rectangle"/>.
        /// </summary>
        /// <param name="rectangle">A <see cref="System.Drawing.Rectangle"/></param>
        /// <remarks>
        /// Warning, DsRect define a rectangle by defining two of his corners and <see cref="System.Drawing.Rectangle"/> define a rectangle with his upper/left corner, his width and his height.
        /// </remarks>
        public DsRect(Rectangle rectangle)
        {
            this.left = rectangle.Left;
            this.top = rectangle.Top;
            this.right = rectangle.Right;
            this.bottom = rectangle.Bottom;
        }

        /// <summary>
        /// Provide de string representation of this DsRect instance
        /// </summary>
        /// <returns>A string formated like this : [left, top - right, bottom]</returns>
        public override string ToString()
        {
            return string.Format("[{0}, {1} - {2}, {3}]", this.left, this.top, this.right, this.bottom);
        }

        public override int GetHashCode()
        {
            return this.left.GetHashCode() |
                this.top.GetHashCode() |
                this.right.GetHashCode() |
                this.bottom.GetHashCode();
        }

        /// <summary>
        /// Define implicit cast between DirectShowLib.DsRect and System.Drawing.Rectangle for languages supporting this feature.
        /// VB.Net doesn't support implicit cast. <see cref="DirectShowLib.DsRect.ToRectangle"/> for similar functionality.
        /// <code>
        ///   // Define a new Rectangle instance
        ///   Rectangle r = new Rectangle(0, 0, 100, 100);
        ///   // Do implicit cast between Rectangle and DsRect
        ///   DsRect dsR = r;
        ///
        ///   Console.WriteLine(dsR.ToString());
        /// </code>
        /// </summary>
        /// <param name="r">a DsRect to be cast</param>
        /// <returns>A casted System.Drawing.Rectangle</returns>
        public static implicit operator Rectangle(DsRect r)
        {
            return r.ToRectangle();
        }

        /// <summary>
        /// Define implicit cast between System.Drawing.Rectangle and DirectShowLib.DsRect for languages supporting this feature.
        /// VB.Net doesn't support implicit cast. <see cref="DirectShowLib.DsRect.FromRectangle"/> for similar functionality.
        /// <code>
        ///   // Define a new DsRect instance
        ///   DsRect dsR = new DsRect(0, 0, 100, 100);
        ///   // Do implicit cast between DsRect and Rectangle
        ///   Rectangle r = dsR;
        ///
        ///   Console.WriteLine(r.ToString());
        /// </code>
        /// </summary>
        /// <param name="r">A System.Drawing.Rectangle to be cast</param>
        /// <returns>A casted DsRect</returns>
        public static implicit operator DsRect(Rectangle r)
        {
            return new DsRect(r);
        }

        /// <summary>
        /// Get the System.Drawing.Rectangle equivalent to this DirectShowLib.DsRect instance.
        /// </summary>
        /// <returns>A System.Drawing.Rectangle</returns>
        public Rectangle ToRectangle()
        {
            return new Rectangle(this.left, this.top, (this.right - this.left), (this.bottom - this.top));
        }

        /// <summary>
        /// Get a new DirectShowLib.DsRect instance for a given <see cref="System.Drawing.Rectangle"/>
        /// </summary>
        /// <param name="r">The <see cref="System.Drawing.Rectangle"/> used to initialize this new DirectShowLib.DsGuid</param>
        /// <returns>A new instance of DirectShowLib.DsGuid</returns>
        public static DsRect FromRectangle(Rectangle r)
        {
            return new DsRect(r);
        }
    }

}
