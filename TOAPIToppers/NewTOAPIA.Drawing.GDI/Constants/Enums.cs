namespace NewTOAPIA.Drawing.GDI
{
    using TOAPI.GDI32;

    /// <summary>
    /// The GDIContext class returns this enumeration for it's TypeOfTechnology property.
    /// This tells you what kind of device is represented by the context.
    /// </summary>
    public enum DeviceTechnology : int
    {
        /// <summary>
        /// A Vector Plotter.  Typically a pen based device.
        /// </summary>
        Plotter = GDI32.DT_PLOTTER,

        /// <summary>
        /// A Raster Display.  This is what a device context for a
        /// display device will return.
        /// </summary>
        RasterDisplay = GDI32.DT_RASDISPLAY,

        /// <summary>
        /// A dot matrix printer, including inkjets, laserjets, and the like
        /// will typically return this value.
        /// </summary>
        RasterPrinter = GDI32.DT_RASPRINTER,    // Raster printer

        RasterCamera = GDI32.DT_RASCAMERA,      // Raster camera 
        CharacterStream = GDI32.DT_CHARSTREAM,  // Character stream 
        Metafile = GDI32.DT_METAFILE,           // Metafile 
        DisplayFile = GDI32.DT_DISPFILE,        // Display file 
    }
}