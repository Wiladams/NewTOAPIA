﻿using System;


using NewTOAPIA.Drawing.GDI;
using NewTOAPIA.Graphics;

namespace Autometaii
{
    [Serializable]
    public class RegionTest : Autometus
    {
        Size2I fSize;

        public RegionTest(Size2I aSize)
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

            RectangleI[] rects = aRegion.GetRectangles();

            aRegion.Add(dRegion);
            //aRegion.Subtract(dRegion);
            //aRegion.Intersect(dRegion);

            //RegionCombineType combType = cRegion.Combine(aRegion, dRegion, RegionCombineStyles.OR);
            //RegionCombineType combType = cRegion.Combine(aRegion, dRegion, RegionCombineStyles.XOR);
            //RegionCombineType combType = cRegion.Combine(aRegion, dRegion, RegionCombineStyles.Diff);
            //RegionCombineType combType = cRegion.Combine(aRegion, dRegion, RegionCombineStyles.AND);
            //rects = aRegion.GetRectangles();


            RectangleI frame = aRegion.GetFrame();

            Colorref colorref = Colorref.FromRGB(255, 127, 127);
            GDIBrush newBrush = new GDIBrush(BrushStyle.Solid, HatchStyle.DiagCross, colorref, Guid.NewGuid());
            aPort.SetBrush(newBrush);

            //aPort.FillRegion(aRegion, newBrush);
            //aPort.DrawRegion(aRegion);
            //aPort.FrameRegion(aRegion, newBrush, new Size(1, 1));

            //aPort.SetDefaultBrushColor(RGBColor.Yellow);
            //aPort.FillRegion(cRegion, GDISolidBrush.DeviceContextBrush);

            rects = aRegion.GetRectangles();
            //Random rnd = new Random();

            GDIPen regionPen = new GDICosmeticPen(PenStyle.Solid, Colorrefs.Red, Guid.NewGuid());
            GDISolidBrush regionBrush = new GDISolidBrush(Colorrefs.Red);

            // I GraphPort does not have FillRegion yet, so we fake it
            foreach (RectangleI r in rects)
            {
                // Create some random color
                //uint randomcolor = RGBColor.RGB((byte)rnd.Next(0, 255), (byte)rnd.Next(0, 255), (byte)rnd.Next(0, 255));
                //aPort.DrawRectangle(regionPen, r.Left, r.Top, r.Width, r.Height);
                aPort.FillRectangle(regionBrush, r.Left, r.Top, r.Width, r.Height);
            }
        }
    }
}
