using System;
using System.Collections.Generic;

using TOAPI.Types;

namespace Autometaii
{
    [Serializable]
    public class TextTest : Autometus
    {
        Size fSize;

        public TextTest(Size aSize)
        {
            fSize = aSize;
        }

        public void ReceiveCommand(Command_Render command)
        {
            RunOnce(command.GraphPort);
        }

        public void RunOnce(IRenderGDI aPort)
        {
            aPort.SetBkMode((int)BackgroundMixMode.TRANSPARENT);
            aPort.SetTextColor(RGBColor.Red);
            aPort.DrawString(200, 200, "Hello World");
            aPort.Flush();
        }
    }
}
