using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

using TOAPI.GDI32;
using TOAPI.User32;

namespace NewTOAPIA.Drawing.GDI
{
    using NewTOAPIA.Graphics;

    /// <summary>
    /// Provides an interface to GDI Drawing.
    /// </summary>
    /// <remarks>
    /// The general style of these methods is to utilize types and constructs from
    /// System.Drawing wherever possible.  This is primarily the Point, Rectangle, 
    /// and Size structures.  The interfaces also use enumerations for the various
    /// parameter types instead of the raw integer numbers that are present in 
    /// WinGdi.h  This makes programming the methods more discoverable in Visual Studio.
    /// 
    /// GDIContext implements the IDeviceContext interface from System.Drawing.  This only 
    /// provides for GetHdc(), and ReleaseHdc().  It's simply a matter of convenience, but 
    /// does allow the GDIContext to be integrated into the System.Drawing interfaces as well.

    /// The GDIContext object encapsulates the GDI Device Context.  This class
    /// represents the primary interface to the GDI drawing system.  The first responsibility
    /// of the class is to handle GDI Context handles.  As such, it provides static methods
    /// for the creation of context handles.
    /// 
    /// The class is implemented as a SafeHandle because this allows the system to help
    /// provide lifetime management.  
    /// 
    /// Over time, all the TOAPI interop calls will be 
    /// converterd to use a SafeHandle instead of IntPtr.  This will make the interop
    /// calls safer in the cases where thread aborts occur.
    /// </remarks>
    public partial class GDIInfoContext : SafeHandle
    {
        #region Constructors

        internal GDIInfoContext(GDIContext aDC)
            : this(GDI32.CreateCompatibleDC(aDC), true)
        {
        }

        internal GDIInfoContext(IntPtr hDC, bool ownHandle)
            : base(IntPtr.Zero, ownHandle)
        {
            SetHandle(hDC);

            SetupDefaultState();
        }

        public void SetGraphicsMode(int mode)
        {
            GDI32.SetGraphicsMode(this, mode);
        }

        /// <summary>
        /// Setup the device to be in a known good starting state.
        /// We know we want ADVANCED drawing, so that is set.
        /// </summary>
        protected virtual void SetupDefaultState()
        {
            SetGraphicsMode(GDI32.GM_ADVANCED);
            GDI32.SetMapMode(this, (int)MappingModes.Anisotropic);
        }

        #endregion

        #region SafeHandle
        public override bool IsInvalid
        {
            get
            {
                return IsClosed || (IntPtr.Zero == handle);
            }
        }

        protected override bool ReleaseHandle()
        {
            return GDI32.DeleteDC(base.handle);
        }
        #endregion

        #region Capabililities
        public int GetDeviceCaps(int nIndex)
        {
            int retValue = GDI32.GetDeviceCaps(this, nIndex);
            return retValue;
        }

        /// <summary>
        /// Returns the number of bits that are used to represent each pixel on
        /// the device represented by the device context.
        /// </summary>
        public int BitsPerPixel
        {
            get
            {
                int retValue = GetDeviceCaps(GDI32.BITSPIXEL);

                return retValue;
            }
        }

        /// <summary>
        /// Indicates what type of technology the device represents.  The 
        /// most common would be DeviceTechnology.RasterDisplay, and
        /// DeviceTechnology.RasterPrinter.
        /// </summary>
        public DeviceTechnology TypeOfDevice
        {
            get
            {
                int retValue = GetDeviceCaps(GDI32.TECHNOLOGY);

                return (DeviceTechnology)retValue;
            }
        }

        /// <summary>
        /// A number indicating the version of the driver.  It is typically '1'.
        /// </summary>
        public int DriverVersion
        {
            get
            {
                int retValue = GetDeviceCaps(GDI32.DRIVERVERSION);

                return retValue;
            }
        }

        /// <summary>
        /// Get the Layout of the device context.  
        /// This can be RTL, 
        /// Bottom To Top (BTT), and a couple
        /// of other flags.  By default it is Left To Right (LTR).
        /// </summary>
        public int Layout
        {
            get
            {
                int retValue = GDI32.GetLayout(this);
                return retValue;
            }

            set
            {
                int retValue = GDI32.SetLayout(this, value);
            }
        }

        /// <summary>
        /// How many pixels are there in a logical inch.  For print devices,
        /// this information is actually accurate.  For display devices, it
        /// is typically wrong as it depends on the pixel density of the display.
        /// On laptops, it might be more accurate as the manufacturer knows the 
        /// exact value.  Typical values might be 96 and 120 for a typical LCD monitor.
        /// </summary>
        public Size PixelsPerLogicalInch
        {
            get
            {
                System.Drawing.Size aSize = new System.Drawing.Size();

                aSize.Width = GetDeviceCaps(GDI32.LOGPIXELSX);
                aSize.Height = GetDeviceCaps(GDI32.LOGPIXELSY);

                return aSize;
            }
        }

        public GDIRasterCapabilities RasterCapabilities
        {
            get
            {
                GDIRasterCapabilities rasterCaps = new GDIRasterCapabilities(this);

                return rasterCaps;
            }
        }

        /// <summary>
        /// The pixel dimension of the device.  This is not logical units, but the actual
        /// number of addressable pixels.
        /// </summary>
        public Size2I SizeInPixels
        {
            get
            {
                Size2I aSize = new Size2I(GetDeviceCaps(GDI32.HORZRES), GetDeviceCaps(GDI32.VERTRES));

                return aSize;
            }
        }

        /// <summary>
        /// The size of the device in millimeters.  This will be accurate for 
        /// printer devices, but may not be so for a raster Display device.
        /// </summary>
        public Size SizeInMillimeters
        {
            get
            {
                Size aSize = new Size();

                aSize.Width = GetDeviceCaps(GDI32.HORZSIZE);
                aSize.Height = GetDeviceCaps(GDI32.VERTSIZE);

                return aSize;
            }
        }

        /// <summary>
        /// Gives information on the text rendering capabilities of the device.
        /// </summary>
        public GDITextCapabilities TextCapabilities
        {
            get
            {
                int retValue = GetDeviceCaps(GDI32.TEXTCAPS);
                GDITextCapabilities textCaps = new GDITextCapabilities(retValue);

                return textCaps;
            }
        }

        /// <summary>
        /// Returns a value in Hertz representing the refresh rate of the device.
        /// This is only useful for a Raster Display device.
        /// </summary>
        public int VerticalRefreshRate
        {
            get
            {
                int retValue = GetDeviceCaps(GDI32.VREFRESH);

                return retValue;
            }
        }
        #endregion

        #region Static Methods to create a context
        public static GDIInfoContext CreateInfoForDevice(string device, string name)
        {
            IntPtr hdc = IntPtr.Zero;

            hdc = GDI32.CreateDC(device, name, null, IntPtr.Zero);
            GDIInfoContext newContext = new GDIContext(hdc, true);

            return newContext;
        }
        #endregion

        #region Informational
        //CancelDC Cancels any pending operation on the specified device context. 
        //ChangeDisplaySettings Changes the settings of the default display device to the specified graphics mode. 
        //ChangeDisplaySettingsEx Changes the settings of the specified display device to the specified graphics mode. 
        //CreateIC Creates an information context for the specified device. 

        //DeleteObject Deletes a logical pen, brush, font, bitmap, region, or palette, freeing all system resources associated with the object. 
        //EnumObjects Enumerates the pens or brushes available for the specified device context. 
        //EnumObjectsProc An application-defined callback function used with the EnumObjects function. 
        //GetCurrentObject Retrieves a handle to an object of the specified type that has been selected into the specified device context. 
        //GetObject Retrieves information for the specified graphics object. 
        //GetObjectType Retrieves the type of the specified object. 
        //GetStockObject Retrieves a handle to one of the stock pens, brushes, fonts, or palettes. 

        //DrawEscape Provides drawing capabilities of the specified video display that are not directly available through the graphics device interface. 
        //GetDCBrushColor Retrieves the current brush color for the specified device context. 
        //GetDCEx Retrieves a handle to a display device context for the client area of a specified window or for the entire screen. 
        //GetDCOrgEx Retrieves the final translation origin for a specified device context. 
        //GetDCPenColor Retrieves the current pen color for the specified device context. 
        //ReleaseDC Releases a device context, freeing it for use by other applications. 
        //GetLayout Retrieves the layout of a device context. 
        //SetLayout 

        // For printers specifically
        //DeviceCapabilities Retrieves the capabilities of a printer device driver. 
        #endregion
    }
}
