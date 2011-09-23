using System;
using System.Collections.Generic;
using System.Text;

using NewTOAPIA;
using NewTOAPIA.Drawing;

namespace PixelShare.Core
{
    public delegate void PixBltPixelBuffer24(GDIDIBSection pixMap, int x, int y);
    public delegate void PixBltPixelArray(IPixelArray pixMap, int x, int y);
    public delegate void PixBltLumb(PixelArray<Lumb> pixMap, int x, int y);
    public delegate void PixBltBGRb(PixelArray<BGRb> pixMap, int x, int y);
    public delegate void PixBltBGRAb(PixelArray<BGRAb> pixMap, int x, int y);

    public delegate void ShowCursorHandler();
    public delegate void HideCursorHandler();
    public delegate void MoveCursorHandler(int x, int y);


    public enum UICommands
    {
        PixBltRaw = 10000,      // Frame Buffer Management
        PixBltRLE,
        PixBltJPG,
        PixBltLuminance,
        Flush = 10500,
        Showcursor = 11000, // UI Management
        HideCursor,
        MoveCursor,
        MouseActivity,
        KeyboardActivity,
    }

    public interface IUIPort
    {
        void PixBltPixelBuffer24(GDIDIBSection pixMap, int x, int y);
        void PixBltPixelArray(IPixelArray pixMap, int x, int y);
        void PixBltBGRb(PixelArray<BGRb> pixMap, int x, int y);
        void PixBltBGRAb(PixelArray<BGRAb> pixMap, int x, int y);
        void PixBltLumb(PixelArray<Lumb> pixMap, int x, int y);

        void ShowCursor();
        void HideCursor();
        void MoveCursor(int x, int y);
    }
}
