using System;
using TOAPI.Types;

namespace NewTOAPIA.Drawing
{

    /// <summary>
    ///    Summary description for Win32Brush.
    /// </summary>
    public class GDISolidBrush : GDIBrush
    {
        public static GDISolidBrush NullBrush = new GDISolidBrush(StockSolidBrush.Null);
        public static GDISolidBrush BlackBrush = new GDISolidBrush(StockSolidBrush.Black);
        public static GDISolidBrush GrayBrush = new GDISolidBrush(StockSolidBrush.Gray);
        public static GDISolidBrush LightGrayBrush = new GDISolidBrush(StockSolidBrush.LightGray);
        public static GDISolidBrush WhiteBrush = new GDISolidBrush(StockSolidBrush.White);
        public static GDISolidBrush DarkGrayBrush = new GDISolidBrush(StockSolidBrush.DarkGray);
        public static GDISolidBrush DeviceContextBrush = new GDISolidBrush(StockSolidBrush.DC);

        private GDISolidBrush(StockSolidBrush index)
            : base((uint)index)
        {
        }

        public GDISolidBrush(uint color)
            : base((uint)color)
        {
        }
    }
}
