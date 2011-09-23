using System;

using TOAPI.Types;
using TOAPI.GDI32;

/// <summary>
/// Represent the default solid brush colors.
/// </summary>
public enum StockSolidBrush
{
	White = 0,
	LightGray = 1,
	Gray = 2,
	DarkGray = 3,
	Black = 4,
	Hollow = 5,
	Null = 5,
	DC = 18
}

/// <summary>
///    Summary description for GDI32Brush.
/// </summary>
public class GDIBrush : Brush
{
	private IntPtr fBrushHandle;

	public GDIBrush()
	{
		fBrushHandle = IntPtr.Zero;
	}

    public GDIBrush(int aStyle,  int hatch, uint colorref, Guid uniqueID)
        :base(aStyle,hatch,colorref,uniqueID)
    {
        fBrushHandle = GDI32.CreateBrushIndirect(ref fLogBrush);
    }


    GDIBrush(uint color)
    {
		fBrushHandle = GDI32.CreateSolidBrush((int)color);
    }

	public override IntPtr Handle 
	{
		get 
		{	
			return fBrushHandle;
		}
		set 
		{
			fBrushHandle = value;
		}
	}

	public uint Color
	{
		get { return fLogBrush.lbColor; }
        //set
        //{
        //    fColor = value;
        //    OnSetColor(value);
        //}
	}

	public virtual void OnSetColor(uint color)
	{
	}

	public virtual void OnSetHandle(IntPtr aHandle)
	{
	}

	~GDIBrush()
	{
		Dispose(false);
	}

	// Implement IDisposable.
	// Do not make this method virtual.
	// A derived class should not be able to override this method.
	public void Dispose()
	{
		Dispose(true);
		// Take yourself off the Finalization queue 
		// to prevent finalization code for this object
		// from executing a second time.
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposing)
	{
		// If disposing equals true, dispose all managed 
		// and unmanaged resources.
		if (disposing)
		{
			// Dispose managed resources.
		}
		
		// Release unmanaged resources. If disposing is false, 
		// only the following code is executed.
		if (fBrushHandle != IntPtr.Zero)
		{
			GDI32.DeleteObject(fBrushHandle);
			fBrushHandle = IntPtr.Zero;
		}
	}
}


