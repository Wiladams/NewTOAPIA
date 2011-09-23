using System;

using TOAPI.Types;
using HumLog;
using NewTOAPIA.Drawing;

namespace HamSketch
{
    public class InstructorSpace : MetaSpace
    {
        SketchWindow fScetchSurface;

        public InstructorSpace(RECT aFrame)
            : base("Auditorium", aFrame)
        {
            // And one larger one
            fScetchSurface = new SketchWindow(200, 0, 600, 600);
            fScetchSurface.Show();
        }

        //public override void OnSurfaceCreated(Guid uniqueID)
        //{
        //    UISurface aSurface = UISurface.gSurfaces[uniqueID];

        //    aSurface.ClearToColor(RGBColor.Magenta);
        //}
    }
}
