using System;

using TOAPI.Types;

namespace Autometaii
{
    /// <summary>
    /// SineWave is an autometus that generates a sinewave for display.
    /// </summary>
    [Serializable]
    class SineWave : Autometus
    {
        GCDelegate fGraphPort;
        int fNumSegments;
        Size fSize;

        public SineWave(Size aSize, int nSegments)
        {
            fNumSegments = nSegments;
            fSize = aSize;    
        }

        GCDelegate GraphPort
        {
            get { return fGraphPort; }
            set { fGraphPort = value; }
        }

        public override void ReceiveCommand(ICommand command)
        {
            RunOnce(((Command_Render)command).GraphPort);
        }

        public void RunOnce(IRenderGDI aPort)
        {
            int i;
            int cxClient = fSize.cx;
            int cyClient = fSize.cy;
            Point[] points = new Point[fNumSegments];

            aPort.SaveState();

            aPort.SetDefaultPenColor(RGBColor.Red);
            aPort.MoveTo(0, cyClient / 2);
            aPort.LineTo(cxClient, cyClient / 2);

            aPort.SetDefaultPenColor(RGBColor.Black);
            for (i = 0; i < fNumSegments; i++)
            {
                points[i].x = i * cxClient / fNumSegments;
                points[i].y = (int)(((double)cyClient/2.0f)*(1.0f-Math.Sin(Math.PI*2.0f*(double)i/(double)fNumSegments)));
            }

            aPort.PolyLine(points);


            aPort.RestoreState();
        }
    }
}
