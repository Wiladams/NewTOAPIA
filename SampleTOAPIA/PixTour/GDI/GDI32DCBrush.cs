//------------------------------------------------------------------------------
// <copyright file="GDI32DCBrush.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

// <summary> GDI32DCBrush represents a base brush type for GDI interaction. 
// 
//</summary>
//------------------------------------------------------------------------------

using System;
using TOAPI.Types;
using TOAPI.GDI32;

// This pen represents a special DC_BRUSH that is tied to a DeviceContext.
// It is good for those cases where you want to use the same object over and
// over again in a device context without having to create and destroy brushes all
// the time.

public class GDI32DCBrush : GDIBrush
{
	IntPtr fDevice;

	public GDI32DCBrush(IntPtr deviceContext, uint color)
	{
        fDevice = deviceContext;
		Handle = GDI32.GetStockObject((int)StockSolidBrush.DC);
		Color = color;
	}

	public override void OnSetColor(uint color)
	{
		// The color has been set
		// We need to change it in the device context
		GDI32.SetDCBrushColor(fDevice, color);
	}

	public override void OnSetHandle(IntPtr aHandle)
	{
		// The handle has been set
	}

	public void FocusHere(IntPtr device)
	{
		GDI32.SelectObject(device, Handle);
	}
}

