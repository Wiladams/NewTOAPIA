using System;
using System.Collections.Generic;

using TOAPI.GDI32;
using TOAPI.Types;
using TOAPI.User32;

using NewTOAPIA;    // BufferChunk
using NewTOAPIA.Net.Rtp;    // Classes used - RtpSession, RtpSender, RtpParticipant, RtpStream
using NewTOAPIA.Drawing;
//using NewTOAPIA.Drawing.Filters;
using NewTOAPIA.UI;

using HamSketch.Tools;
using HumLog;

namespace HamSketch
{
    public class SketchWindow : UIWindow
    {
        BaseTool fCurrentTool;

        public SketchWindow(int x, int y, int width, int height)
            : base("Sketcher", x, y, width, height)
        {
        }

        protected override void OnSurfaceCreated(Guid uniqueID)
        {
            base.OnSurfaceCreated(uniqueID);

            //ClearToColor(RGBColor.Cream);

            //SelectTool(new LineTool(RGBColor.Tomato));
            SelectTool(new Pencil(RGBColor.Tomato));
        }

        void SelectTool(BaseTool aTool)
        {
            fCurrentTool = aTool;
            fCurrentTool.AttachToWindow(this);
            fCurrentTool.GraphPort = GraphPort;
        }


        public void ClearSurface()
        {
            //ClearToColor(RGBColor.White);
        }
    }
}
