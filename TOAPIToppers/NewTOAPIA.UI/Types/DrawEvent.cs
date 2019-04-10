using System;
using System.Drawing;

//using TOAPI.Types;
using NewTOAPIA.Drawing;
using NewTOAPIA.Graphics;

namespace NewTOAPIA.UI
{
    /// <summary>
    /// This class represents the information needed by graphics to paint when necessary.
    /// </summary>
    public class DrawEvent
    {
        IGraphPort fDevice;
        RectangleI fClipRect;

        public DrawEvent(IGraphPort device, RectangleI clipRect)
        {
            fClipRect = clipRect;
            fDevice = device;
        }

        public IGraphPort GraphPort
        {
            get { return fDevice; }
        }

        public RectangleI ClipRect
        {
            get { return fClipRect; }
        }
    }

}