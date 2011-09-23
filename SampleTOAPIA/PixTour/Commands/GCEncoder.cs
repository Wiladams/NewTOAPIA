using System;
using System.IO;

class GCEncoder
{
    BinaryWriter fWriter;
    Stream fOutStream;

    public GCEncoder(Stream outStream)
    {
        fOutStream = outStream;
        fWriter = new BinaryWriter(outStream);
    }

    public void Write(GraphicCommand aCommand)
    {
    }
}
