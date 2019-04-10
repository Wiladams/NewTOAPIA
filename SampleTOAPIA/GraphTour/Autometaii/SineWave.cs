using System;

using NewTOAPIA.Drawing;
using NewTOAPIA.Drawing.GDI;
using NewTOAPIA.Graphics;

namespace Autometaii
{
    /// <summary>
    /// SineWave is an autometus that generates a sinewave for display.
    /// </summary>
    [Serializable]
    class SineWave : Autometus
    {
        IGraphPort fGraphPort;
        int fNumSegments;
        Size2I fSize;

        public SineWave(Size2I aSize, int nSegments)
        {
            fNumSegments = nSegments;
            fSize = aSize;    
        }

        IGraphPort GraphPort
        {
            get { return fGraphPort; }
            set { fGraphPort = value; }
        }

        public override void ReceiveCommand(ICommand command)
        {
            RunOnce(((Command_Render)command).GraphPort);
        }

        public void RunOnce(GDIRenderer aPort)
        {
            int i;
            int cxClient = fSize.Width;
            int cyClient = fSize.Height;
            Point2I[] points = new Point2I[fNumSegments];

            aPort.SaveState();

            GDIPen redPen = new GDIPen(Colorrefs.Red);
            aPort.DrawLine(redPen, new Point2I(0, cyClient/2), new Point2I(cxClient, cyClient / 2));

            GDIPen blackPen = new GDIPen(Colorrefs.Black);
            for (i = 0; i < fNumSegments; i++)
            {
                points[i].X = i * cxClient / fNumSegments;
                points[i].Y = (int)(((double)cyClient/2.0f)*(1.0f-Math.Sin(Math.PI*2.0f*(double)i/(double)fNumSegments)));
            }

            aPort.DrawLines(blackPen, points);


            aPort.ResetState();
        }
    }
}
