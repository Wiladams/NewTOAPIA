using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

using TOAPI.GDI32;
using TOAPI.User32;

namespace NewTOAPIA.Drawing
{
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
    public partial class GDIContext : GDIInfoContext 
    {
        #region Constructors

        internal GDIContext(GDIContext aDC)
            : this(GDI32.CreateCompatibleDC(aDC), true)
        {
        }

        internal GDIContext(IntPtr hDC, bool ownHandle)
            : base(hDC, ownHandle)
        {
        }

        /// <summary>
        /// Setup the device to be in a known good starting state.
        /// We know we want ADVANCED drawing, so that is set.
        /// </summary>
        protected override void SetupDefaultState()
        {
            SetGraphicsMode(GDI32.GM_ADVANCED);
            GDI32.SetMapMode(this, (int)MappingModes.Anisotropic);
        }


        #endregion

        #region Static Methods to create a context
        public static GDIContext CreateForDevice(string device, string name)
        {
            IntPtr hdc = IntPtr.Zero;

            hdc = GDI32.CreateDC(device, name, null, IntPtr.Zero);
            GDIContext newContext = new GDIContext(hdc, true);

            return newContext;
        }

        public static GDIContext CreateForDisplay(string displayName)
        {
            IntPtr hdc = IntPtr.Zero;

            hdc = GDI32.CreateDC("DISPLAY", displayName, null, IntPtr.Zero);
            GDIContext newContext = new GDIContext(hdc, true);

            return newContext;
        }

        public static GDIContext CreateForDefaultDisplay()
        {
            IntPtr hdc = IntPtr.Zero;

            hdc = User32.GetWindowDC(User32.GetDesktopWindow());
            GDIContext newContext = new GDIContext(hdc, false);

            return newContext;
        }

        /// <summary>
        /// Create a GDIContext so that drawing to the desktop can occur.
        /// On Vista, the drawing will be behind all windows, but will draw on top
        /// of any icons on the actual desktop.
        /// For Windows XP, the drawing will be behind those icons as well.
        /// </summary>
        /// <returns>The Device Context for drawing to the desktop background.</returns>
        public static GDIContext CreateForDesktopBackground()
        {
            IntPtr hDC = IntPtr.Zero;

            // First get a handle on the Program Manager window using FindWindow
            IntPtr hProgMan = User32.FindWindow("ProgMan", null);

            if (IntPtr.Zero != hProgMan)
            {
                // The only child of this window is the Shell Window
                // So get that handle.
                IntPtr hShellView = User32.GetWindow(hProgMan, User32.GW_CHILD);

                // Now that we have the shell window, get the ListView, which is the 
                // child displaying everything.
                IntPtr hListView = User32.GetWindow(hShellView, User32.GW_CHILD);

                // Finally, get the DeviceContext handle for this list view
                hDC = User32.GetDC(hListView);
            }
            else
            {
                // If null was returned, then we're probably on Vista, and need to ask for
                // The "Program Manager" window.
                if (IntPtr.Zero == hProgMan)
                {
                    hProgMan = User32.FindWindow("Program Manager", null);
                    hDC = User32.GetDC(hProgMan);
                }
            }


            // And create a GDIContext for it
            GDIContext aContext = new GDIContext(hDC, false);

            return aContext;

        }

        public static GDIContext CreateForAllAttachedMonitors()
        {
            IntPtr hdc = IntPtr.Zero;

            hdc = GDI32.CreateDC("DISPLAY", null, null, IntPtr.Zero);
            GDIContext newContext = new GDIContext(hdc, true);

            // public static IntPtr CreateDC(string lpszDriver, string lpszDevice, IntPtr lpInitData);

            return newContext;
        }

        public static GDIContext CreateForMemory()
        {
            IntPtr hdc;

            hdc = GDI32.CreateCompatibleDC(GDIContext.CreateForDefaultDisplay());
            GDIContext newConText = new GDIContext(hdc, true);

            return newConText;
        }

        public static GDIContext CreateForWholeWindow(IntPtr hWnd)
        {
            IntPtr hdc = IntPtr.Zero;

            hdc = User32.GetWindowDC(hWnd);
            GDIContext newContext = new GDIContext(hdc, false);

            return newContext;
        }

        public static GDIContext CreateForWindowClientArea(IntPtr hWnd)
        {
            IntPtr hdc = IntPtr.Zero;

            hdc = User32.GetDC(hWnd);
            //hdc = User32.GetDCEx(hWnd, IntPtr.Zero, DeviceContextValues.Window);
            GDIContext newContext = new GDIContext(hdc, false);

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
