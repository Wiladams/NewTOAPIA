using System;
using System.Collections.Generic;
using System.Text;

using NewTOAPIA;
using NewTOAPIA.Drawing;

namespace DistributedDesktop
{
    public delegate void PixBltPixelBuffer24(GDIDIBSection pixMap, int x, int y);
    public delegate void PixBltPixelArray(IPixelArray pixMap, int x, int y);
    public delegate void PixBltLumb(PixelArray<Lumb> pixMap, int x, int y);
    public delegate void PixBltBGRb(PixelAccessor<BGRb> pixMap, int x, int y);
    public delegate void PixBltBGRAb(PixelArray<BGRAb> pixMap, int x, int y);



    public enum UICommands
    {
        PixBltRaw = 10000,      // Frame Buffer Management
        PixBltRLE,
        PixBltJPG,
        PixBltLuminance,
        Flush = 10500,
    }

    public interface IUIPort
    {
        void PixBltPixelBuffer24(GDIDIBSection pixMap, int x, int y);
        void PixBltPixelArray(IPixelArray pixMap, int x, int y);
        void PixBltBGRb(PixelAccessor<BGRb> pixMap, int x, int y);
        void PixBltBGRAb(PixelArray<BGRAb> pixMap, int x, int y);
        void PixBltLumb(PixelArray<Lumb> pixMap, int x, int y);

    }
}
