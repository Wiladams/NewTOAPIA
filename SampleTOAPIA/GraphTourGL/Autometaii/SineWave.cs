using System;
using System.Drawing;

using NewTOAPIA;
using NewTOAPIA.Drawing;

namespace Autometaii
{
    /// <summary>
    /// SineWave is an autometus that generates a sinewave for display.
    /// </summary>
    [Serializable]
    class SineWave : Autometus
    {
        IDraw2D fGraphPort;
        int fNumSegments;
        Size fSize;
        GPen redPen;
        GPen linePen;

        public SineWave(Size aSize, int nSegments)
        {
            fNumSegments = nSegments;
            fSize = aSize;
            redPen = new GPen(RGBColor.Red);
            linePen = new GPen(RGBColor.Cyan);

        }

        IDraw2D GraphPort
        {
            get { return fGraphPort; }
            set { fGraphPort = value; }
        }

        public override void ReceiveCommand(ICommand command)
        {
            RunOnce(((Command_Render)command).GraphPort);
        }

        public void RunOnce(IDraw2D aPort)
        {
            int i;
            int cxClient = fSize.Width;
            int cyClient = fSize.Height;
            Point[] points = new Point[fNumSegments];

            //aPort.SaveState();

            aPort.DrawLine(redPen, new Point(0, cyClient/2), new Point(cxClient, cyClient / 2));

            for (i = 0; i < fNumSegments; i++)
            {
                points[i].X = i * cxClient / fNumSegments;
                points[i].Y = (int)(((double)cyClient/2.0f)*(1.0f-Math.Sin(Math.PI*2.0f*(double)i/(double)fNumSegments)));
            }

            aPort.DrawLines(linePen, points);


            //aPort.ResetState();
        }
    }
}
