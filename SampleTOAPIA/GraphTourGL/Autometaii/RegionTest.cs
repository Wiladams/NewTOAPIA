using System;
using System.Collections.Generic;
using System.Drawing;

//using TOAPI.Types;
using NewTOAPIA.Drawing;

namespace Autometaii
{
    [Serializable]
    public class RegionTest : Autometus
    {
        Size fSize;

        public RegionTest(Size aSize)
        {
            fSize = aSize;
        }

        public void ReceiveCommand(Command_Render command)
        {
            RunOnce(command.GraphPort);
        }

        public void RunOnce(GDIRenderer aPort)
        {
            // Do some region stuff
            GDIRegion aRegion = GDIRegion.CreateFromRectangle(10, 10, 50, 250, Guid.NewGuid());
            GDIRegion bRegion = GDIRegion.CreateFromRectangle(10, 10, 50, 250, Guid.NewGuid());
            GDIRegion cRegion = GDIRegion.CreateFromRectangle(0, 0, 0, 0, Guid.NewGuid());
            GDIRegion dRegion = GDIRegion.CreateFromRectangle(30, 40, 200, 100, Guid.NewGuid());

            bool isEqual = aRegion.Equals(bRegion);

            Rectangle[] rects = aRegion.GetRectangles();

            aRegion.Add(dRegion);
            //aRegion.Subtract(dRegion);
            //aRegion.Intersect(dRegion);

            //RegionCombineType combType = cRegion.Combine(aRegion, dRegion, RegionCombineStyles.OR);
            //RegionCombineType combType = cRegion.Combine(aRegion, dRegion, RegionCombineStyles.XOR);
            //RegionCombineType combType = cRegion.Combine(aRegion, dRegion, RegionCombineStyles.Diff);
            //RegionCombineType combType = cRegion.Combine(aRegion, dRegion, RegionCombineStyles.AND);
            //rects = aRegion.GetRectangles();


            Rectangle frame = aRegion.GetFrame();

            uint colorref = RGBColor.RGB(255, 127, 127);
            GDIBrush newBrush = new GDIBrush(BrushStyle.Solid, HatchStyle.DiagCross, colorref, Guid.NewGuid());
            aPort.SetBrush(newBrush);

            //aPort.FillRegion(aRegion, newBrush);
            //aPort.DrawRegion(aRegion);
            //aPort.FrameRegion(aRegion, newBrush, new Size(1, 1));

            //aPort.SetDefaultBrushColor(RGBColor.Yellow);
            //aPort.FillRegion(cRegion, GDISolidBrush.DeviceContextBrush);

            rects = aRegion.GetRectangles();
            //Random rnd = new Random();

            GDIPen regionPen = new CosmeticPen(PenStyle.Solid, RGBColor.Red, Guid.NewGuid());
            GDISolidBrush regionBrush = new GDISolidBrush(RGBColor.Red);

            // I GraphPort does not have FillRegion yet, so we fake it
            foreach (Rectangle r in rects)
            {
                // Create some random color
                //uint randomcolor = RGBColor.RGB((byte)rnd.Next(0, 255), (byte)rnd.Next(0, 255), (byte)rnd.Next(0, 255));
                //aPort.DrawRectangle(regionPen, r.Left, r.Top, r.Width, r.Height);
                aPort.FillRectangle(regionBrush, r.Left, r.Top, r.Width, r.Height);
            }
        }
    }
}
