 
 // OpenVG 1.1 Reference Implementation

 namespace NewTOAPIA.Drawing
{
    using System;

    using VGboolean = System.Boolean;
    using VGubyte = System.Byte;
    using VGshort = System.UInt16;
    using VGint = System.Int32;
    using VGuint = System.UInt32;
    using VGbitfield = System.UInt32;
    using VGfloat = System.Single;

    using VGHandle = System.IntPtr;
    using VGMaskLayer = System.Object;


    abstract public partial class OpenVG
    {
        public const int OPENVG_VERSION_1_0 = 1;
        public const int OPENVG_VERSION_1_0_1 = 1;
        public const int OPENVG_VERSION_1_1 = 2;

        public const int VG_MAXSHORT = 0x7FFF;
        public const int VG_MAXINT = 0x7FFFFFFF;
        public const int VG_MAX_ENUM = 0x7FFFFFFF;

        public const int VG_PATH_FORMAT_STANDARD = 0;


        public object VG_INVALID_HANDLE = null;

        /* Function Prototypes */
        abstract public VGErrorCode GetError();

        abstract public void Flush();
        abstract public void Finish();


        /* Masking and Clearing */
        abstract public void Mask(VGHandle mask, VGMaskOperation operation,
                                             VGint x, VGint y,
                                             VGint width, VGint height);
        abstract public void RenderToMask(VGPath path,
                                                    VGbitfield paintModes,
                                                    VGMaskOperation operation);
        abstract public VGMaskLayer CreateMaskLayer(VGint width, VGint height);
        abstract public void DestroyMaskLayer(VGMaskLayer maskLayer);
        abstract public void FillMaskLayer(VGMaskLayer maskLayer,
                                                     VGint x, VGint y,
                                                     VGint width, VGint height,
                                                     VGfloat value);
        abstract public void CopyMask(VGMaskLayer maskLayer,
                                                VGint dx, VGint dy,
                                                VGint sx, VGint sy,
                                                VGint width, VGint height);
        abstract public void Clear(VGint x, VGint y, VGint width, VGint height);


        /* Paint */
        abstract public void SetPaint(VGPaint paint, VGbitfield paintModes);
        abstract public VGPaint GetPaint(VGPaintMode paintMode);
        abstract public void SetColor(VGPaint paint, VGuint rgba);
        abstract public VGuint GetColor(VGPaint paint);
        abstract public void PaintPattern(VGPaint paint, VGImage pattern);



        /* Paths */
        abstract public void DrawPath(VGPath path, VGbitfield paintModes);


        /* Image Filters */
        abstract public void ColorMatrix(VGImage dst, VGImage src, VGfloat[] matrix);
        abstract public void Convolve(VGImage dst, VGImage src,
                                    VGint kernelWidth, VGint kernelHeight,
                                    VGint shiftX, VGint shiftY,
                                     VGshort[] kernel,
                                    VGfloat scale,
                                    VGfloat bias,
                                    VGTilingMode tilingMode);
        abstract public void SeparableConvolve(VGImage dst, VGImage src,
                                             VGint kernelWidth,
                                             VGint kernelHeight,
                                             VGint shiftX, VGint shiftY,
                                              VGshort[] kernelX,
                                              VGshort[] kernelY,
                                             VGfloat scale,
                                             VGfloat bias,
                                             VGTilingMode tilingMode);
        abstract public void GaussianBlur(VGImage dst, VGImage src,
                                        VGfloat stdDeviationX,
                                        VGfloat stdDeviationY,
                                        VGTilingMode tilingMode);
        abstract public void Lookup(VGImage dst, VGImage src,
                                   VGubyte[] redLUT,
                                   VGubyte[] greenLUT,
                                   VGubyte[] blueLUT,
                                   VGubyte[] alphaLUT,
                                  VGboolean outputLinear,
                                  VGboolean outputPremultiplied);
        abstract public void LookupSingle(VGImage dst, VGImage src,
                                         VGuint[] lookupTable,
                                        VGImageChannel sourceChannel,
                                        VGboolean outputLinear,
                                        VGboolean outputPremultiplied);

        /* Text */
        abstract public void SetGlyphToPath(VGFont font, VGuint glyphIndex, VGPath path, VGboolean isHinted, VGfloat glyphOrigin, // [2],
                                                       VGfloat escapement); // [2]) ;
        abstract public void SetGlyphToImage(VGFont font, VGuint glyphIndex, VGImage image,
                                                        VGfloat[] glyphOrigin, // [2],
                                                        VGfloat[] escapement); // [2]) ;
        abstract public void ClearGlyph(VGFont font, VGuint glyphIndex);
        abstract public void DrawGlyph(VGFont font, VGuint glyphIndex, VGbitfield paintModes, VGboolean allowAutoHinting);
        abstract public void DrawGlyphs(VGFont font, VGint glyphCount, VGuint[] glyphIndices,
                                                   VGfloat[] adjustments_x, VGfloat[] adjustments_y,
                                                  VGbitfield paintModes, VGboolean allowAutoHinting);

        /* Hardware Queries */
        abstract public VGHardwareQueryResult HardwareQuery(VGHardwareQueryType key, VGint setting);

        /* Renderer and Extension Information */
        abstract public VGubyte[] GetString(VGStringID name);

    }
}