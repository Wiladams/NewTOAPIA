using System;
using TOAPI.Types;
using TOAPI.GDI32;

// This pen represents a special DC_PEN that is tied to a DeviceContext
// It is a solid cosmetic pen (1 pixel), and only the color can be changed.
// It is good for those cases where you want to use the same object over and
// over again in a device context without having to create and destroy pens all
// the time.
public class GDI32DCPen : GDIPen
{
	public GDI32DCPen(IntPtr device, uint color)
        : base()
	{
		Handle = GDI32.GetStockObject((int)StockSolid.DC);
		//Color = color;
	}

	public override void OnSetHandle(IntPtr aHandle)
	{
		// The handle has been set
	}

}
