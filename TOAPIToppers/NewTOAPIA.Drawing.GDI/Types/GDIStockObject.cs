using System;
using System.Collections.Generic;
using System.Text;

using TOAPI.GDI32;

namespace NewTOAPIA.Drawing.GDI
{
    public enum StockObjects
    {
        BlackBrush = GDI32.BLACK_BRUSH,
        DarkGrayBrush = GDI32.DKGRAY_BRUSH,
        DCBrush = GDI32.DC_BRUSH,
        GrayBrush = GDI32.GRAY_BRUSH,
        LightGrayBrush = GDI32.LTGRAY_BRUSH,
        NullBrush = GDI32.NULL_BRUSH,
        WhiteBrush = GDI32.WHITE_BRUSH,
        BlackPen = GDI32.BLACK_PEN,
        DCPen = GDI32.DC_PEN,
        NullPen = GDI32.NULL_PEN,
        WhitePen = GDI32.WHITE_PEN,
        AnsiFixedFont = GDI32.ANSI_FIXED_FONT,
        AnsiVarFont = GDI32.ANSI_VAR_FONT,
        DeviceDefaultFont = GDI32.DEVICE_DEFAULT_FONT,
        DefaultGUIFont = GDI32.DEFAULT_GUI_FONT,
        OEMFixedFont = GDI32.OEM_FIXED_FONT,
        SystemFont = GDI32.SYSTEM_FONT,
        SystemFixedFont = GDI32.SYSTEM_FIXED_FONT,
        DefaultPalette = GDI32.DEFAULT_PALETTE,
    }

    public class GDIStockObject : GDIObject
    {
        static Dictionary<StockObjects, GDIStockObject> gStockObjects;
        StockObjects fObjectIndex;

        static GDIStockObject()
        {
            gStockObjects = new Dictionary<StockObjects, GDIStockObject>();
        }

        public GDIStockObject(StockObjects index)
            : base(false, Guid.NewGuid())
        {
            fObjectIndex = index;
            IntPtr objectHandle = GDI32.GetStockObject((int)index);
            SetHandle(objectHandle);
        }

        public StockObjects ObjectIndex
        {
            get { return fObjectIndex; }
        }

        public static GDIStockObject GetStockObject(StockObjects index)
        {
            GDIStockObject anObject;

            // 1. Lookup the object in the existing stock object table
            if (gStockObjects.ContainsKey(index))
            {
                anObject = gStockObjects[index];
                return anObject;
            }
            
            // 2. create the object if it doesn't exist already
            anObject = new GDIStockObject(index);
            gStockObjects.Add(index, anObject);

            return anObject;
        }
    }
}
