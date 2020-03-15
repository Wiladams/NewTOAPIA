using System;
using System.Collections.Generic;
using System.Drawing;

using NewTOAPIA;
using NewTOAPIA.Drawing;
using NewTOAPIA.Drawing.GDI;
using NewTOAPIA.Graphics;


namespace Autometaii
{
    [Serializable]
    public class TextTest : Autometus
    {
        Size2I fSize;
        uint fTextColor;
        GDIFont fFont;
        string fStringMessage;
        Point fOrigin;

        public TextTest(Size2I aSize)
        {
            fSize = aSize;
            fOrigin = new Point(0, 0);
            fTextColor = Colorrefs.Red;
            fFont = new GDIFont("Algerian", 96, Guid.NewGuid());
            fStringMessage = "Hello World!";

            Dimension = aSize;
        }

        public Size2I Dimension
        {
            get { return fSize; }
            set
            {
                fSize = value;
                OnSetSize(value);
            }
        }

        protected void OnSetSize(Size2I newSize)
        {
            // Calculate the size of the string, and 
            // center it in the window
            int centerX = newSize.Width / 2;
            int centerY = newSize.Height / 2;

            Size2I stringSize = fFont.MeasureString(fStringMessage);
            fOrigin.X = centerX - stringSize.Width/ 2;
            fOrigin.Y = centerY - stringSize.Height/2;

        }

        public void ReceiveCommand(Command_Render command)
        {
            RunOnce(command.GraphPort);
        }

        public void RunOnce(GDIRenderer aPort)
        {
            aPort.SetBkMode((int)BackgroundMixMode.Transparent);
            aPort.SetTextColor(fTextColor);

            //Guid fontID = Guid.NewGuid();
            //aPort.CreateFont("Times New Roman", 96, fontID);
            //aPort.SelectUniqueObject(fontID);
            aPort.SetFont(fFont);

            aPort.DrawString(fOrigin.X, fOrigin.Y, fStringMessage);

            aPort.Flush();
        }
    }
}
