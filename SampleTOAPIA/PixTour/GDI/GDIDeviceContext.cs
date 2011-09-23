using System;
using System.Collections.Generic;
using System.Text;

using TOAPI.GDI32;
using TOAPI.Types;
using TOAPI.User32;

/// <summary>
/// The GDIDeviceContext is meant to provide easy access to GDI devices.  It has 
/// static methods for creating various devices.
/// </summary>
public class GDIDeviceContext : IHandle
{
    IntPtr fDCHandle;       // The actual device context handle

    public GDIDeviceContext(GDIDeviceContext aDC)
    {
        fDCHandle = GDI32.CreateCompatibleDC(aDC.Handle);
    }

    GDIDeviceContext(IntPtr hDC)
    {
        fDCHandle = hDC;
    }

    public int GetDeviceCaps(IntPtr hDC, int nIndex)
    {
        int retValue = GDI32.GetDeviceCaps(Handle, nIndex);
        return retValue;
    }

    public IntPtr Handle
    {
        get { return fDCHandle; }
    }

    public static GDIDeviceContext CreateForDefaultDisplay()
    {
        IntPtr hdc = IntPtr.Zero;

        hdc = GDI32.CreateDC("DISPLAY", null, null, IntPtr.Zero);
        GDIDeviceContext newContext = new GDIDeviceContext(hdc);

        // public static IntPtr CreateDC(string lpszDriver, string lpszDevice, IntPtr lpInitData);

        return newContext;
    }

    public static GDIDeviceContext CreateForAllAttachedMonitors()
    {
        IntPtr hdc = IntPtr.Zero;

        hdc = GDI32.CreateDC("DISPLAY", null, null, IntPtr.Zero);
        GDIDeviceContext newContext = new GDIDeviceContext(hdc);

        // public static IntPtr CreateDC(string lpszDriver, string lpszDevice, IntPtr lpInitData);
        
        return newContext;
    }

    public static GDIDeviceContext CreateForWindow(IntPtr hWnd)
    {
        IntPtr hdc = IntPtr.Zero;

        hdc = User32.GetDC(hWnd);
        GDIDeviceContext newContext = new GDIDeviceContext(hdc);

        return newContext;
    }

    //[DllImport("gdi32.dll", CharSet = CharSet.Auto)]
    //private static extern IntPtr CreateIC(string lpszDriverName, string lpszDeviceName, int /*DEVMODE*/ lpInitData);

    /// <summary>
    /// Get the Layout of the device context.  This can be RTL, Bottom To Top (BTT), and a couple
    /// of other flags.  By default it is Left To Right (LTR).
    /// </summary>
    public int Layout
    {
        get
        {
            int retValue = GDI32.GetLayout(Handle);
            return retValue;
        }

        set
        {
            int retValue = GDI32.SetLayout(Handle, value);
        }
    }

    public int SaveState()
    {
        int savedState = GDI32.SaveDC(Handle);
        return savedState;
    }

    public bool RestoreState()
    {
        return RestoreState(-1);
    }

    public bool RestoreState(int toState)
    {
        bool retValue = GDI32.RestoreDC(Handle, toState);
        return retValue;
    }

    // Returning various capabilities for the device
    public Size Resolution
    {
        get
        {
            Size aSize = new Size();

            aSize.cx = GDI32.GetDeviceCaps(Handle, GDI32.HORZRES);
            aSize.cy = GDI32.GetDeviceCaps(Handle, GDI32.VERTRES);

            return aSize;
        }
    }

//CancelDC Cancels any pending operation on the specified device context. 
//ChangeDisplaySettings Changes the settings of the default display device to the specified graphics mode. 
//ChangeDisplaySettingsEx Changes the settings of the specified display device to the specified graphics mode. 
//CreateDC Creates a device context for a device using the specified name. 
//CreateIC Creates an information context for the specified device. 
//DeleteDC Deletes the specified device context. 

    //DeleteObject Deletes a logical pen, brush, font, bitmap, region, or palette, freeing all system resources associated with the object. 
    //EnumObjects Enumerates the pens or brushes available for the specified device context. 
    //EnumObjectsProc An application-defined callback function used with the EnumObjects function. 
    //GetCurrentObject Retrieves a handle to an object of the specified type that has been selected into the specified device context. 
    //GetObject Retrieves information for the specified graphics object. 
    //GetObjectType Retrieves the type of the specified object. 
    //GetStockObject Retrieves a handle to one of the stock pens, brushes, fonts, or palettes. 
    //SelectObject Selects an object into the specified device context. 

    //GetDeviceCaps Retrieves device-specific information for the specified device. 
    //DrawEscape Provides drawing capabilities of the specified video display that are not directly available through the graphics device interface. 
//EnumDisplayDevices Retrieves information about the display devices in a system. 
//EnumDisplaySettings Retrieves information about one of the graphics modes for a display device. 
//EnumDisplaySettingsEx Retrieves information about one of the graphics modes for a display device. 
//GetDC Retrieves a handle to a display device context for the client area of a specified window or for the entire screen. 
//GetDCBrushColor Retrieves the current brush color for the specified device context. 
//GetDCEx Retrieves a handle to a display device context for the client area of a specified window or for the entire screen. 
//GetDCOrgEx Retrieves the final translation origin for a specified device context. 
//GetDCPenColor Retrieves the current pen color for the specified device context. 
//ReleaseDC Releases a device context, freeing it for use by other applications. 
//ResetDC Updates the specified printer or plotter device context using the specified information. 
//SetDCBrushColor Sets the current device context brush color to the specified color value. 
//SetDCPenColor Sets the current device context pen color to the specified color value. 
    //GetLayout Retrieves the layout of a device context. 
    //SetLayout 

    // For printers specifically
    //DeviceCapabilities Retrieves the capabilities of a printer device driver. 

}

