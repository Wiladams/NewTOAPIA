using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.UI.Input
{
    // http://www.usb.org/developers/devclass_docs/Hut1_12.pdf
    public enum USBUsagePage
    {
        Undefined = 0x00,
        GenericDesktopControls = 0x01,
        SimulationControls = 0x02,
        VRControls = 0x03,
        SportControls = 0x04,
        GameControls = 0x05,
        GenericDeviceControls = 0x06,
        KeyboardKeypad = 0x07,
        LEDs = 0x08,
        Button = 0x09,
        Ordinal = 0x0A,
        Telephony = 0x0B,
        Consumer = 0x0C,
        Digitizer = 0x0D,
        PhysicalInterfaceDevice = 0x0F,
        Unicode = 0x10,
        AlphanumericDisplay = 0x14,
        MedicalInstruments = 0x40,
        //80-83 Monitor pages USB Device Class Definition for
        //Monitor Devices
        //84-87 Power pages USB Device Class Definition for
        //Power Devices
        BarCodeScanner = 0x8C,
        Scale = 0x8D,
        MagneticStripReader = 0x8E,
        CameraControl = 0x90,
        ArcadePage = 0x91,
    }


}
