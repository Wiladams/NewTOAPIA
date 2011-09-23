using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class GCCodec
{
    static string Encoding
    {
        get { return "GCCodec"; }
    }
}


class GCDecoder
{
    static string StreamEncoding
    {
        get { return "GCBinary"; }
    }

    public static GraphicCommand Decode(int command, byte[] data)
    {
        return null;
    }
}
