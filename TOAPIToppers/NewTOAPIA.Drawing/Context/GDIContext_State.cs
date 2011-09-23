using System;
using System.Collections.Generic;
using System.Text;

using TOAPI.GDI32;

namespace NewTOAPIA.Drawing
{
    public partial class GDIContext
    {
        public virtual void SaveState()
        {
            int savedState = GDI32.SaveDC(this);
        }

        public virtual void ResetState()
        {
            RestoreState(-1);
        }

        public virtual void RestoreState(int toState)
        {
            bool retValue = GDI32.RestoreDC(this, toState);
        }

        public virtual void Flush()
        {
            GDI32.GdiFlush();
        }

        #region Setting Drawing Attributes
        public virtual void SetDefaultPenColor(UInt32 colorref)
        {
            GDI32.SetDCPenColor(this, colorref);
        }

        public virtual void SetMappingMode(MappingModes aMode)
        {
            int result = GDI32.SetMapMode(this, (int)aMode);
        }

        public virtual void SetBkColor(uint colorref)
        {
            GDI32.SetBkColor(this, colorref);
        }

        public virtual void SetBkMode(int bkMode)
        {
            GDI32.SetBkMode(this, bkMode);
        }

        public virtual void SetROP2(BinaryRasterOps rasOp)
        {
            GDI32.SetROP2(this, (int)rasOp);
        }

        public virtual void SetPolyFillMode(PolygonFillMode fillMode)
        {
            GDI32.SetPolyFillMode(this, (int)fillMode);
        }
        #endregion

        #region Retrieving Drawing Attributes
        public virtual uint GetBkColor()
        {
            uint retValue = GDI32.GetBkColor(this);

            return retValue;
        }

        public virtual int GetBkMode()
        {
            int retValue = GDI32.GetBkMode(this);

            return retValue;
        }

        public virtual uint GetDCBrushColor()
        {
            uint retValue = GDI32.GetDCBrushColor(this);

            return retValue;
        }

        public virtual uint GetDCPenColor()
        {
            uint retValue = GDI32.GetDCPenColor(this);

            return retValue;
        }

        public virtual BinaryRasterOps GetROP2()
        {
            BinaryRasterOps retValue = (BinaryRasterOps)GDI32.GetROP2(this);
            return retValue;
        }
        #endregion
    }
}
