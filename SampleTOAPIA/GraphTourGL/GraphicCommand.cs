using System;
using System.Collections.Generic;

    class GraphicCommand
    {
        public int fCommand;   // One of the EMR_ constants
        public byte[] fData;    // The actual data

        public GraphicCommand(int aCommand, byte[] data)
        {
            fCommand = aCommand;
            fData = data;
        }
    }

