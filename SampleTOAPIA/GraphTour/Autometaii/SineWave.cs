using System;
using System.Drawing;

using NewTOAPIA;
using NewTOAPIA.Drawing;
using NewTOAPIA.Drawing.GDI;

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
        Size fSize;

        public SineWave(Size aSize, int nSegments)
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
            Point[] points = new Point[fNumSegments];

            aPort.SaveState();

            GDIPen redPen = new GDIPen(RGBColor.Red);
            aPort.DrawLine(redPen, new Point(0, cyClient/2), new Point(cxClient, cyClient / 2));

            GDIPen blackPen = new GDIPen(RGBColor.Black);
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
