
using System.Diagnostics;

using System;
using System.Drawing;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

using TOAPI.Types;


namespace NewTOAPIA.DirectShow
{
    using NewTOAPIA.DirectShow.Core;
    using NewTOAPIA.DirectShow.DES;

    /// <summary>
    ///  Capabilities of the video device such as 
    ///  min/max frame size and frame rate.
    /// </summary>
    public class VideoCapability
    {
        #region Fields
        VIDEO_STREAM_CONFIG_CAPS m_StreamConfigCaps;
        AMMediaType m_MediaType;

        VIDEOINFOHEADER m_VideoInfo;    // Derived from the AMMediaType object
        #endregion

        #region Constructor
        internal VideoCapability(AMMediaType mediaType, VIDEO_STREAM_CONFIG_CAPS caps)
        {
            if (null == mediaType)
                throw new ArgumentNullException("mediaType");

            m_MediaType = mediaType;
            m_StreamConfigCaps = caps;

            m_VideoInfo = (VIDEOINFOHEADER)Marshal.PtrToStructure(mediaType.formatPtr, typeof(VIDEOINFOHEADER));
        }
        #endregion

        #region Properties
        #region AMMediaType
        public Guid MajorType
        {
            get { return m_MediaType.majorType; }
        }

        public string MajorTypeString
        {
            get { return MediaType.GetMediaTypeString(MajorType); }
        }

        public Guid SubType
        {
            get { return m_MediaType.subType; }
        }

        public string SubTypeString
        {
            get { return MediaSubType.GetTypeString(SubType); }
        }
        #endregion

        #region VideoInfoHeader
        public long AvgTimePerFrame
        {
            get {return m_VideoInfo.AvgTimePerFrame;}
        }

        public int BitErrorRate
        {
            get {return m_VideoInfo.BitErrorRate;}
        }

        public int BitRate
        {
            get {return m_VideoInfo.BitRate;}
        }

        public BITMAPINFOHEADER BmiHeader
        {
            get { return m_VideoInfo.BmiHeader; }
        }

        public Rectangle SrcRect
        {
            get {return new Rectangle(m_VideoInfo.SrcRect.X, m_VideoInfo.SrcRect.Y, m_VideoInfo.SrcRect.Width, m_VideoInfo.SrcRect.Height);}
        }

        public Rectangle TargetRect
        {
            get {return new Rectangle(m_VideoInfo.TargetRect.X, m_VideoInfo.TargetRect.Y, m_VideoInfo.TargetRect.Width, m_VideoInfo.TargetRect.Height);}
        }

        #endregion

        //public uint CompressionCode
        //{
        //    get { return m_VideoInfo.BmiHeader.biCompression; }
        //}

        //public string CompressionString
        //{
        //    get
        //    {
        //        //string dStr = FOURCC.FourCCToString(CompressionCode);
        //        string dStr = FourCCToString(CompressionCode);
        //        return dStr;
        //    }
        //}

        #region Video_Stream_Config_Caps
        /// <summary> Native size of the incoming video signal. This is the largest signal the filter can digitize with every pixel remaining unique. Read-only. </summary>
        public Size InputSize
        {
            get { return new Size(m_StreamConfigCaps.InputSize.Width, m_StreamConfigCaps.InputSize.Height); }
        }

        /// <summary> Minimum supported frame size. Read-only. </summary>
        public Size MinFrameSize
        {
            get { return new Size(m_StreamConfigCaps.MinOutputSize.Width, m_StreamConfigCaps.MinOutputSize.Height); }
        }

        /// <summary> Maximum supported frame size. Read-only. </summary>
        public Size MaxFrameSize
        {
            get { return new Size(m_StreamConfigCaps.MaxOutputSize.Width, m_StreamConfigCaps.MaxOutputSize.Height); }
        }

        /// <summary> Granularity of the output width. This value specifies the increments that are valid between MinFrameSize and MaxFrameSize. Read-only. </summary>
        public int FrameSizeGranularityX
        {
            get {return m_StreamConfigCaps.OutputGranularityX;}
        }

        /// <summary> Granularity of the output height. This value specifies the increments that are valid between MinFrameSize and MaxFrameSize. Read-only. </summary>
        public int FrameSizeGranularityY
        {
            get {return m_StreamConfigCaps.OutputGranularityY;}
        }

        /// <summary> Minimum supported frame rate. Read-only. </summary>
        public double MinFrameRate
        {
            get { return (double)10000000 / m_StreamConfigCaps.MaxFrameInterval; }
        }

        /// <summary> Maximum supported frame rate. Read-only. </summary>
        public double MaxFrameRate
        {
            get { return (double)10000000 / m_StreamConfigCaps.MinFrameInterval; }
        }
        #endregion

        #endregion

        #region Static Methods
        public static string FourCCToString(uint fourcc)
        {
            char c1 = (char)(fourcc & 0x000000ff);
            char c2 = (char)((fourcc & 0x0000ff00) >> 8);
            char c3 = (char)((fourcc & 0x00ff0000) >> 16);
            char c4 = (char)((fourcc & 0xff000000) >> 24);

            if (0 == c4 && 0 == c1)
                return "RGB";

            StringBuilder builder = new StringBuilder(4);
            builder.AppendFormat("{0}{1}{2}{3}", c1, c2, c3, c4);

            string retStr = builder.ToString();
            return retStr;
        }

        public static int GetNumberOfCapabilities(IAMStreamConfig videoStreamConfig)
        {
            if (null == videoStreamConfig)
                return 0;

            int c, size;
            int hr = videoStreamConfig.GetNumberOfCapabilities(out c, out size);
            DsError.ThrowExceptionForHR(hr);

            return c;
        }

        public static VideoCapability GetVideoCapability(IAMStreamConfig videoStreamConfig, int index)
        {
            int numCaps = GetNumberOfCapabilities(videoStreamConfig);

            if (index < 0 || index >= numCaps)
                throw new ArgumentOutOfRangeException("index", index, "");

            List<VideoCapability> vList = GetAllVideoCapabilities(videoStreamConfig);

            return vList[index];
        }

        public static List<VideoCapability> GetAllVideoCapabilities(IAMStreamConfig videoStreamConfig)
        {
            if (videoStreamConfig == null)
                throw new ArgumentNullException("videoStreamConfig");

            IntPtr pCaps = IntPtr.Zero;

            List<VideoCapability> capsList = new List<VideoCapability>();

            try
            {
                // Ensure this device reports capabilities
                int c, size;
                int hr = videoStreamConfig.GetNumberOfCapabilities(out c, out size);
                DsError.ThrowExceptionForHR(hr);

                
                if (c <= 0)
                    throw new NotSupportedException("Video device does not report capabilities.");

                if (size > Marshal.SizeOf(typeof(VIDEO_STREAM_CONFIG_CAPS)))
                    throw new NotSupportedException("Unable to retrieve video device capabilities. This video device requires a larger VideoStreamConfigCaps structure.");


                // Alloc memory for structure
                pCaps = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(VIDEO_STREAM_CONFIG_CAPS)));
                VIDEO_STREAM_CONFIG_CAPS cap;


                // Retrieve all capability structures
                for (int i = 0; i < c; i++)
                {
                    AMMediaType mediaType = new AMMediaType();
                    hr = videoStreamConfig.GetStreamCaps(i, out mediaType, pCaps);
                    DsError.ThrowExceptionForHR(hr);

                    if (MediaType.Video.Equals(mediaType.majorType))
                    {
                        cap = (VIDEO_STREAM_CONFIG_CAPS)Marshal.PtrToStructure(pCaps, typeof(VIDEO_STREAM_CONFIG_CAPS));
                        VideoCapability newCap = new VideoCapability(mediaType, cap);
                        capsList.Add(newCap);
                    } else if (MediaType.Audio.Equals(mediaType.majorType))
                    {
                            Console.WriteLine("Audio Configuration");
                    }
                }
            }
            finally
            {
                if (pCaps != IntPtr.Zero)
                    Marshal.FreeCoTaskMem(pCaps); 
                pCaps = IntPtr.Zero;

                //if (mediaType != null)
                //    DsUtils.FreeAMMediaType(mediaType); 
                //mediaType = null;
            }

            return capsList;
        }
        #endregion
    }
}
