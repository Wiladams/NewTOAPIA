using System;

using TOAPI.Types;

/// <summary>
/// This class represents the information needed by graphics to paint when necessary.
/// </summary>
public class DrawEventArgs : EventArgs
{
	IGraphPort fDevice;
    PAINTSTRUCT fPaintStruct;

    public DrawEventArgs(IGraphPort device, PAINTSTRUCT pStruct)
	{
        fPaintStruct = pStruct;
		fDevice = device;
	}

    public IGraphPort GraphDevice
	{
		get {return fDevice;}
	}

	public Rectangle ClipRect
	{
		get {return fPaintStruct.rcPaint;}
	}
}

