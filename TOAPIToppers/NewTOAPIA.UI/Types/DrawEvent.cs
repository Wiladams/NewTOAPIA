using System;
using System.Drawing;

//using TOAPI.Types;
using NewTOAPIA.Drawing;

namespace NewTOAPIA.UI
{
    /// <summary>
    /// This class represents the information needed by graphics to paint when necessary.
    /// </summary>
    public class DrawEvent
    {
        IGraphPort fDevice;
        Rectangle fClipRect;

        public DrawEvent(IGraphPort device, Rectangle clipRect)
        {
            fClipRect = clipRect;
            fDevice = device;
        }

        public IGraphPort GraphPort
        {
            get { return fDevice; }
        }

        public Rectangle ClipRect
        {
            get { return fClipRect; }
        }
    }

}