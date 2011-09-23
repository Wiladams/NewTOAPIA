using System;
using TOAPI.Types;
using TOAPI.GDI32;

public class GDIPen : Pen, IDisposable
{
    protected IntPtr fPenHandle;

    // Start out with a blank color (black) and no handle
    // to an actual system pen.  Create the pen handle on the fly
    // to conserve system resources.
    public GDIPen()
    {
    }

    public GDIPen(int penStyle, int width, uint colorref, Guid uniqueID)
        :base(penStyle, width, colorref, uniqueID)
    {        
        fPenHandle = GDI32.CreatePenIndirect(ref fLogPen);
    }

	public override IntPtr Handle
	{
		get
		{
			return fPenHandle;
		}
		set
		{
			fPenHandle = value;
			OnSetHandle(value);
		}
	}


	public uint Color
	{
		get { return fLogPen.lopnColor; }
        //set 
        //{
        //    fColor = value;
        //    OnSetColor(value); 
        //}
	}

	public virtual void OnSetHandle(IntPtr aHandle)
	{
	}

	public virtual void OnSetColor(uint color)
	{
	}

	~GDIPen()
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
		if (fPenHandle != IntPtr.Zero)
		{
			GDI32.DeleteObject(Handle);
			fPenHandle = IntPtr.Zero;
		}
	}
}
