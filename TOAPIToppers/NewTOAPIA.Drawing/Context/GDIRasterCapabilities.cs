using System;
using System.Collections.Generic;
using System.Text;

using TOAPI.GDI32;

namespace NewTOAPIA.Drawing
{
    public class GDIRasterCapabilities
    {
        GDIInfoContext fDeviceContext;

        public GDIRasterCapabilities(GDIInfoContext dc)
        {
            fDeviceContext = dc;
        }

        GDIInfoContext DC 
        {
            get {return fDeviceContext;}
        }

        public bool RequiresBandingSupport
        {
            get
            {
                int retValue = this.DC.GetDeviceCaps(GDI32.RASTERCAPS);
                bool required = (retValue & GDI32.RC_BANDING) > 0;
                return required;
            }
        }

        public bool CanPerformFloodFills
        {
            get
            {
                int retValue = this.DC.GetDeviceCaps(GDI32.RASTERCAPS);
                bool cando = (retValue & GDI32.RC_FLOODFILL) > 0;
                return cando;
            }
        }

        public bool CanTransferBitmaps
        {
            get
            {
                int retValue = this.DC.GetDeviceCaps(GDI32.RASTERCAPS);
                bool cando = (retValue & GDI32.RC_BITBLT) > 0;
                return cando;
            }
        }

        public bool IsPaletteBased
        {
            get
            {
                int retValue = this.DC.GetDeviceCaps(GDI32.RASTERCAPS);
                bool cando = (retValue & GDI32.RC_PALETTE) > 0;
                return cando;
            }
        }

        public bool SupportsDIBits
        {
            get
            {
                int retValue = this.DC.GetDeviceCaps(GDI32.RASTERCAPS);
                bool cando = (retValue & GDI32.RC_DI_BITMAP) > 0;
                return cando;
            }
        }

        public bool SupportsLargeBitmaps
        {
            get
            {
                int retValue = this.DC.GetDeviceCaps(GDI32.RASTERCAPS);
                bool cando = (retValue & GDI32.RC_BITMAP64) > 0;
                return cando;
            }
        }

        public bool SupportsScaling
        {
            get
            {
                int retValue = this.DC.GetDeviceCaps(GDI32.RASTERCAPS);
                bool cando = (retValue & GDI32.RC_SCALING) > 0;
                return cando;
            }
        }

        public bool SupportsSetDIBitsToDevice
        {
            get
            {
                int retValue = this.DC.GetDeviceCaps(GDI32.RASTERCAPS);
                bool cando = (retValue & GDI32.RC_DIBTODEV) > 0;
                return cando;
            }
        }

        public bool SupportsStretchBlt
        {
            get
            {
                int retValue = this.DC.GetDeviceCaps(GDI32.RASTERCAPS);
                bool cando = (retValue & GDI32.RC_STRETCHBLT) > 0;
                return cando;
            }
        }

        public bool SupportsStretchDIBits
        {
            get
            {
                int retValue = this.DC.GetDeviceCaps(GDI32.RASTERCAPS);
                bool cando = (retValue & GDI32.RC_STRETCHDIB) > 0;
                return cando;
            }
        }
    }
}
