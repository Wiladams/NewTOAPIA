using System;
using System.Drawing;


using NewTOAPIA.Drawing;
using NewTOAPIA.Graphics;

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
            redPen = new GPen(Colorrefs.Red);
            linePen = new GPen(Colorrefs.Cyan);

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
            Point2I[] points = new Point2I[fNumSegments];

            //aPort.SaveState();

            aPort.DrawLine(redPen, new Point2I(0, cyClient/2), new Point2I(cxClient, cyClient / 2));

            for (i = 0; i < fNumSegments; i++)
            {
                points[i].x = i * cxClient / fNumSegments;
                points[i].y = (int)(((double)cyClient/2.0f)*(1.0f-Math.Sin(Math.PI*2.0f*(double)i/(double)fNumSegments)));
            }

            aPort.DrawLines(linePen, points);


            //aPort.ResetState();
        }
    }
}
