using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

using TOAPI.GDI32;

namespace NewTOAPIA.Drawing
{
    public partial class GDIContext
    {
        public virtual IntPtr SelectObject(SafeHandle objectHandle)
        {
            return GDI32.SelectObject(this, objectHandle.DangerousGetHandle());
        }

        public virtual IntPtr SelectObject(IntPtr objectHandle)
        {
            return GDI32.SelectObject(this, objectHandle);
        }

        public virtual void SelectStockObject(int objectIndex)
        {
            // First get a handle on the object
            IntPtr objHandle = GDI32.GetStockObject(objectIndex);

            // Then select it into the device context
            GDI32.SelectObject(this, objHandle);
        }

        public virtual GDIPen CreatePen(PenType aType, PenStyle aStyle, PenJoinStyle aJoinStyle, PenEndCap aEndCap, uint colorref, int width, Guid uniqueID)
        {
            GDIPen aPen = new GDIPen(aType, aStyle, aJoinStyle, aEndCap, colorref, width, uniqueID);
            return aPen;
        }

        public virtual GDIBrush CreateBrush(BrushStyle aStyle, HatchStyle hatch, uint colorref, Guid uniqueID)
        {
            GDIBrush aBrush = new GDIBrush(aStyle, hatch, colorref, uniqueID);
            return aBrush;
        }

    }
}
