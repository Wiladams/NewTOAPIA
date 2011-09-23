
namespace NewTOAPIA.Drawing.GDI
{
    using TOAPI.GDI32;
    using TOAPI.Types;

    using NewTOAPIA.Graphics;

    public class GDIView
    {
        GDIContext fDeviceContext;

        #region Constructor
        public GDIView(GDIContext dc)
        {
            fDeviceContext = dc;
        }
        #endregion

        public Size2I ViewportExtent
        {
            get
            {
                SIZE aSize = new SIZE();
                bool result = GDI32.GetViewportExtEx(fDeviceContext, out aSize);

                return new Size2I(aSize.cx, aSize.cy);
            }

            set
            {
                fDeviceContext.SetViewportExtent(value.width, value.height);
            }
        }

        public Point2I ViewportOrigin
        {
            get
            {
                POINT aPoint = new POINT();
                bool result = GDI32.GetViewportOrgEx(fDeviceContext, out aPoint);

                return new Point2I(aPoint.X, aPoint.Y);
            }

            set
            {
                fDeviceContext.SetViewportOrigin(value.x, value.y);
            }
        }

        public Size2I WindowExtent
        {
            get
            {
                SIZE aSize = new SIZE();
                bool result = GDI32.GetWindowExtEx(fDeviceContext, out aSize);

                return new Size2I(aSize.cx, aSize.cy);
            }

            set
            {
                fDeviceContext.SetWindowExtent(value.width, value.height);
            }
        }

        public Point2I WindowOrigin
        {
            get
            {
                POINT aPoint = new POINT();
                bool result = GDI32.GetWindowOrgEx(fDeviceContext, out aPoint);

                return new Point2I(aPoint.X, aPoint.Y);
            }

            set
            {
                fDeviceContext.SetWindowOrigin(value.x, value.y);
            }
        }
    }
}
