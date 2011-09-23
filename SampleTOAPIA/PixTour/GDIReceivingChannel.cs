using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using TOAPI.GDI32;
using TOAPI.Types;
using TOAPI.User32;

public class GDIReceivingChannel
{
    GDIListener fListener;
    GDICommandRecipient fCommandRecipient;
    GDIGeometryRenderer fRenderer;
    string fMulticastAddressName;
    int fPort;

    public GDIReceivingChannel(string multicastAddress, int aPort, GDIDeviceContext aDevice)
    {
        fMulticastAddressName = multicastAddress;
        fPort = aPort;

        fListener = new GDIListener(fMulticastAddressName, fPort);
        fRenderer = new GDIGeometryRenderer(aDevice);

        fCommandRecipient = new GDICommandRecipient(fRenderer);
        fListener.DataReceived += new ReceiveByteArray(this.ReceiveData);
    }

    void ReceiveData(byte[] data)
    {
        // Create a memory stream on the byte array
        MemoryStream recordStream = new MemoryStream(data, false);

        // Deserialize the object from the memory stream
        BinaryFormatter formatter = new BinaryFormatter();
        //SoapFormatter formatter = new SoapFormatter();

        EMR aRecord = (EMR)formatter.Deserialize(recordStream);

        fCommandRecipient.ReceiveCommand(aRecord);
    }
}

