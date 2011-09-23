using System;

using TOAPI.Types;
using HumLog;
using NewTOAPIA.Drawing;

namespace HamSketch
{
    public class InstructorSpace : MetaSpace
    {
        Guid tile2ID;
        Guid tile3ID;
        SketchWindow fScetchSurface;

        public InstructorSpace(RECT aFrame)
            : base("Auditorium", aFrame)
        {
            // And one larger one
            fScetchSurface = new SketchWindow(200, 0, 600, 600);
            fScetchSurface.Show();

            ClockWindow aClock = new ClockWindow(0, 0, 198, 198);
            //aClock.Show();

            tile2ID = MetaSpace.CreateSurface("tile2", new RECT(0, 200, 198, 198),null);

            tile3ID = MetaSpace.CreateSurface("tile3", new RECT(0, 400, 198, 198), null);

        }

        //public override void OnSurfaceCreated(Guid uniqueID)
        //{
        //    UISurface aSurface = UISurface.gSurfaces[uniqueID];

        //    aSurface.ClearToColor(RGBColor.Magenta);
        //}
    }
}
