using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewTOAPIA.Graphics
{
    using System.IO;
    using System.Runtime.InteropServices;

    abstract public class CvArray<TDepth> : UnmanagedObject
        where TDepth : new()
    {
        /// <summary>
        /// The size of the elements in the CvArray
        /// </summary>
        protected static readonly int _sizeOfElement = Marshal.SizeOf(typeof(TDepth));

        /// <summary>
        /// The pinned GCHandle to _array;
        /// </summary>
        protected GCHandle _dataHandle;

        #region Properties
        ///<summary> The pointer to the internal structure </summary>
        public new IntPtr Ptr
        {
            get { return _ptr; }
            set { _ptr = value; }
        }

        public int Columns { get; set; }
        public int Rows { get; set; }

        public abstract Array ManagedArray { get; set; }


        /// <summary>
        /// Get or Set an Array of bytes that represent the data in this array
        /// </summary>
        /// <remarks> Should only be used for serialization &amp; deserialization</remarks>
        [System.Diagnostics.DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Byte[] Bytes
        {
            get
            {
                int size;
                IntPtr dataStart;

                if (_dataHandle.IsAllocated)
                {
                    size = _sizeOfElement * ManagedArray.Length;
                    dataStart = _dataHandle.AddrOfPinnedObject();
                }
                else if (this is Matrix<TDepth>)
                {
                    Matrix<TDepth> matrix = (Matrix<TDepth>)this;
                    MCvMat mat = matrix.MCvMat;
                    if (mat.step == 0)
                    {  //The matrix only have one row
                        size = mat.cols * NumberOfChannels * Marshal.SizeOf(typeof(TDepth));
                    }
                    else
                        size = mat.rows * mat.step;
                    dataStart = mat.data;
                }
                else if (this is MatND<TDepth>)
                {
                    throw new NotImplementedException("Getting Bytes from Pinned MatND is not implemented");
                }
                else
                {  //this is Image<TColor, TDepth>
                    MIplImage iplImage = (MIplImage)Marshal.PtrToStructure(Ptr, typeof(MIplImage));
                    size = iplImage.height * iplImage.widthStep;
                    dataStart = iplImage.imageData;
                }
                Byte[] data = new Byte[size];
                Marshal.Copy(dataStart, data, 0, size);

                if (SerializationCompressionRatio == 0)
                {
                    return data;
                }
                else
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        //using (GZipStream compressedStream = new GZipStream(ms, CompressionMode.Compress))
                        using (ZOutputStream compressedStream = new ZOutputStream(ms, SerializationCompressionRatio))
                        {
                            compressedStream.Write(data, 0, data.Length);
                            compressedStream.Flush();
                        }
                        return ms.ToArray();
                    }
                }

            }
            set
            {
                Byte[] bytes;
                int size = _sizeOfElement * ManagedArray.Length;

                if (SerializationCompressionRatio == 0)
                {
                    bytes = value;
                }
                else
                {
                    try
                    {  //try to use zlib to decompressed the data
                        using (MemoryStream ms = new MemoryStream())
                        {
                            using (ZOutputStream stream = new ZOutputStream(ms))
                            {
                                stream.Write(value, 0, value.Length);
                                stream.Flush();
                            }
                            bytes = ms.ToArray();
                        }
                    }
                    catch
                    {  //if using zlib decompression fails, try to use .NET GZipStream to decompress

                        using (MemoryStream ms = new MemoryStream(value))
                        {
                            //ms.Position = 0;
                            using (GZipStream stream = new GZipStream(ms, CompressionMode.Decompress))
                            {
                                bytes = new Byte[size];
                                stream.Read(bytes, 0, size);
                            }
                        }
                    }
                }

                Marshal.Copy(bytes, 0, _dataHandle.AddrOfPinnedObject(), size);
            }
        }

        /// <summary>
        /// Allocate data for the array
        /// </summary>
        /// <param name="rows">The number of rows</param>
        /// <param name="cols">The number of columns</param>
        /// <param name="numberOfChannels">The number of channels of this cvArray</param>
        protected abstract void AllocateData(int rows, int cols, int numberOfChannels);

        #region UnmanagedObject
        /// <summary>
        /// Free the _dataHandle if it is set
        /// </summary>
        protected override void DisposeObject()
        {
            if (_dataHandle.IsAllocated)
                _dataHandle.Free();
        }
        #endregion

        #endregion
    }
}
