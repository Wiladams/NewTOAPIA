using System;
using System.Drawing;

using TOAPI.GDI32;
using TOAPI.Types;

namespace NewTOAPIA.Drawing
{
    public class GDIView
    {
        GDIContext fDeviceContext;

        #region Constructor
        public GDIView(GDIContext dc)
        {
            fDeviceContext = dc;
        }
        #endregion

        public Size ViewportExtent
        {
            get
            {
                SIZE aSize = new SIZE();
                bool result = GDI32.GetViewportExtEx(fDeviceContext, out aSize);
                
                return new Size(aSize.cx, aSize.cy);
            }

            set
            {
                fDeviceContext.SetViewportExtent(value.Width, value.Height);
            }
        }

        public System.Drawing.Point ViewportOrigin
        {
            get
            {
                POINT aPoint = new POINT();
                bool result = GDI32.GetViewportOrgEx(fDeviceContext, out aPoint);
                
                return new Point(aPoint.X, aPoint.Y);
            }

            set
            {
                fDeviceContext.SetViewportOrigin(value.X, value.Y);
            }
        }

        public Size WindowExtent
        {
            get
            {
                SIZE aSize = new SIZE();
                bool result = GDI32.GetWindowExtEx(fDeviceContext, out aSize);

                return new Size(aSize.cx, aSize.cy);
            }

            set
            {
                fDeviceContext.SetWindowExtent(value.Width, value.Height);
            }
        }

        public System.Drawing.Point WindowOrigin
        {
            get
            {
                POINT aPoint = new POINT();
                bool result = GDI32.GetWindowOrgEx(fDeviceContext, out aPoint);

                return new System.Drawing.Point(aPoint.X, aPoint.Y);
            }

            set
            {
                fDeviceContext.SetWindowOrigin(value.X, value.Y);
            }
        }
    }
}
