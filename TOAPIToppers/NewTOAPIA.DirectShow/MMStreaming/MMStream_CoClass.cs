using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.DirectShow.MMStreaming
{
    using System.Runtime.InteropServices;


    /// <summary>
    /// From CLSID_AMMultiMediaStream
    /// </summary>
    [ComImport, Guid("49c47ce5-9ba4-11d0-8212-00c04fc32c45")]
    public class AMMultiMediaStream
    {
    }

    /// <summary>
    /// From CLSID_AMMediaTypeStream
    /// </summary>
    [ComImport, Guid("CF0F2F7C-F7BF-11d0-900D-00C04FD9189D")]
    public class AMMediaTypeStream
    {
    }

    /// <summary>
    /// From CLSID_AMDirectDrawStream
    /// </summary>
    [ComImport, Guid("49c47ce4-9ba4-11d0-8212-00c04fc32c45")]
    public class AMDirectDrawStream
    {
    }

    /// <summary>
    /// From CLSID_AMAudioStream
    /// </summary>
    [ComImport, Guid("8496e040-af4c-11d0-8212-00c04fc32c45")]
    public class AMAudioStream
    {
    }

    /// <summary>
    /// From CLSID_AMAudioData
    /// </summary>
    [ComImport, Guid("f2468580-af8a-11d0-8212-00c04fc32c45")]
    public class AMAudioData
    {
    }

}
