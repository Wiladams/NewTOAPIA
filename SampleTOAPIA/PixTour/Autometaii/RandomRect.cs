using System;

using TOAPI.Types;

namespace Autometaii
{
    [Serializable]
    public class RandomRect : Autometus
    {
        Size fSize;

        public RandomRect(Size aSize)
        {
            fSize = aSize;
        }

        public void ReceiveCommand(Command_Render command)
        {
            RunOnce(command.GraphPort);
        }

        public void RunOnce(IRenderGDI aPort)
        {
            int maxWidth = 320; //  fSize.cx - 10 - 10;
            int maxHeight = 240; // fSize.cy - 10 - 10;


            Random rnd = new Random();

            aPort.SaveState();

            aPort.UseDefaultBrush();
            aPort.UseDefaultPen();
            aPort.SetDefaultPenColor(RGBColor.Black);
            aPort.Flush();


            for (int i = 0; i < 1000; i++)
            {
                int width = rnd.Next(10, maxWidth);
                int height = rnd.Next(10, maxHeight);
                int orgX = rnd.Next(10, fSize.cx-width);
                int orgY = rnd.Next(10, fSize.cy-height);

                aPort.SetDefaultBrushColor(RGBColor.RGB((byte)rnd.Next(0,255),(byte)rnd.Next(0,255),(byte)rnd.Next(0,255)));
                
                aPort.Rectangle(orgX, orgY, orgX+width, orgY+height);
                //aPort.Flush();
            }
            aPort.Flush();


            aPort.RestoreState();
        }
    }
}
