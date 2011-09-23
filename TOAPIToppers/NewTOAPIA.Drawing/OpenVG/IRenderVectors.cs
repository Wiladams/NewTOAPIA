namespace NewTOAPIA.Drawing
{

    public enum VGErrorCode
    {
        VG_NO_ERROR = 0,
        VG_BAD_HANDLE_ERROR = 0x1000,
        VG_ILLEGAL_ARGUMENT_ERROR = 0x1001,
        VG_OUT_OF_MEMORY_ERROR = 0x1002,
        VG_PATH_CAPABILITY_ERROR = 0x1003,
        VG_UNSUPPORTED_IMAGE_FORMAT_ERROR = 0x1004,
        VG_UNSUPPORTED_PATH_FORMAT_ERROR = 0x1005,
        VG_IMAGE_IN_USE_ERROR = 0x1006,
        VG_NO_CONTEXT_ERROR = 0x1007,

        VG_ERROR_CODE_FORCE_SIZE = OpenVG.VG_MAX_ENUM
    }

    public enum VGParamType
    {
        /* Mode settings */
        VG_MATRIX_MODE = 0x1100,
        VG_FILL_RULE = 0x1101,
        VG_IMAGE_QUALITY = 0x1102,
        VG_RENDERING_QUALITY = 0x1103,
        VG_BLEND_MODE = 0x1104,
        VG_IMAGE_MODE = 0x1105,

        /* Scissoring rectangles */
        VG_SCISSOR_RECTS = 0x1106,

        /* Color Transformation */
        VG_COLOR_TRANSFORM = 0x1170,
        VG_COLOR_TRANSFORM_VALUES = 0x1171,

        /* Stroke parameters */
        VG_STROKE_LINE_WIDTH = 0x1110,
        VG_STROKE_CAP_STYLE = 0x1111,
        VG_STROKE_JOIN_STYLE = 0x1112,
        VG_STROKE_MITER_LIMIT = 0x1113,
        VG_STROKE_DASH_PATTERN = 0x1114,
        VG_STROKE_DASH_PHASE = 0x1115,
        VG_STROKE_DASH_PHASE_RESET = 0x1116,

        /* Edge fill color for VG_TILE_FILL tiling mode */
        VG_TILE_FILL_COLOR = 0x1120,

        /* Color for vgClear */
        VG_CLEAR_COLOR = 0x1121,

        /* Glyph origin */
        VG_GLYPH_ORIGIN = 0x1122,

        /* Enable/disable alpha masking and scissoring */
        VG_MASKING = 0x1130,
        VG_SCISSORING = 0x1131,

        /* Pixel layout information */
        VG_PIXEL_LAYOUT = 0x1140,
        VG_SCREEN_LAYOUT = 0x1141,

        /* Source format selection for image filters */
        VG_FILTER_FORMAT_LINEAR = 0x1150,
        VG_FILTER_FORMAT_PREMULTIPLIED = 0x1151,

        /* Destination write enable mask for image filters */
        VG_FILTER_CHANNEL_MASK = 0x1152,

        /* Implementation limits (read-only) */
        VG_MAX_SCISSOR_RECTS = 0x1160,
        VG_MAX_DASH_COUNT = 0x1161,
        VG_MAX_KERNEL_SIZE = 0x1162,
        VG_MAX_SEPARABLE_KERNEL_SIZE = 0x1163,
        VG_MAX_COLOR_RAMP_STOPS = 0x1164,
        VG_MAX_IMAGE_WIDTH = 0x1165,
        VG_MAX_IMAGE_HEIGHT = 0x1166,
        VG_MAX_IMAGE_PIXELS = 0x1167,
        VG_MAX_IMAGE_BYTES = 0x1168,
        VG_MAX_FLOAT = 0x1169,
        VG_MAX_GAUSSIAN_STD_DEVIATION = 0x116A,

        VG_PARAM_TYPE_FORCE_SIZE = OpenVG.VG_MAX_ENUM
    }

    public enum VGRenderingQuality
    {
        VG_RENDERING_QUALITY_NONANTIALIASED = 0x1200,
        VG_RENDERING_QUALITY_FASTER = 0x1201,
        VG_RENDERING_QUALITY_BETTER = 0x1202, /* Default */

        VG_RENDERING_QUALITY_FORCE_SIZE = OpenVG.VG_MAX_ENUM
    }

    public enum VGPixelLayout
    {
        VG_PIXEL_LAYOUT_UNKNOWN = 0x1300,
        VG_PIXEL_LAYOUT_RGB_VERTICAL = 0x1301,
        VG_PIXEL_LAYOUT_BGR_VERTICAL = 0x1302,
        VG_PIXEL_LAYOUT_RGB_HORIZONTAL = 0x1303,
        VG_PIXEL_LAYOUT_BGR_HORIZONTAL = 0x1304,

        VG_PIXEL_LAYOUT_FORCE_SIZE = OpenVG.VG_MAX_ENUM
    }

    public enum VGMatrixMode
    {
        VG_MATRIX_PATH_USER_TO_SURFACE = 0x1400,
        VG_MATRIX_IMAGE_USER_TO_SURFACE = 0x1401,
        VG_MATRIX_FILL_PAINT_TO_USER = 0x1402,
        VG_MATRIX_STROKE_PAINT_TO_USER = 0x1403,
        VG_MATRIX_GLYPH_USER_TO_SURFACE = 0x1404,

        VG_MATRIX_MODE_FORCE_SIZE = OpenVG.VG_MAX_ENUM
    }

    public enum VGMaskOperation
    {
        VG_CLEAR_MASK = 0x1500,
        VG_FILL_MASK = 0x1501,
        VG_SET_MASK = 0x1502,
        VG_UNION_MASK = 0x1503,
        VG_INTERSECT_MASK = 0x1504,
        VG_SUBTRACT_MASK = 0x1505,

        VG_MASK_OPERATION_FORCE_SIZE = OpenVG.VG_MAX_ENUM
    }


    public enum VGPathDatatype
    {
        VG_PATH_DATATYPE_S_8 = 0,
        VG_PATH_DATATYPE_S_16 = 1,
        VG_PATH_DATATYPE_S_32 = 2,
        VG_PATH_DATATYPE_F = 3,

        VG_PATH_DATATYPE_FORCE_SIZE = OpenVG.VG_MAX_ENUM
    }

    public enum VGPathAbsRel
    {
        VG_ABSOLUTE = 0,
        VG_RELATIVE = 1,

        VG_PATH_ABS_REL_FORCE_SIZE = OpenVG.VG_MAX_ENUM
    }

    public enum VGPathSegment
    {
        VG_CLOSE_PATH = (0 << 1),
        VG_MOVE_TO = (1 << 1),
        VG_LINE_TO = (2 << 1),
        VG_HLINE_TO = (3 << 1),
        VG_VLINE_TO = (4 << 1),
        VG_QUAD_TO = (5 << 1),
        VG_CUBIC_TO = (6 << 1),
        VG_SQUAD_TO = (7 << 1),
        VG_SCUBIC_TO = (8 << 1),
        VG_SCCWARC_TO = (9 << 1),
        VG_SCWARC_TO = (10 << 1),
        VG_LCCWARC_TO = (11 << 1),
        VG_LCWARC_TO = (12 << 1),

        VG_PATH_SEGMENT_FORCE_SIZE = OpenVG.VG_MAX_ENUM
    }

    public enum VGPathCommand
    {
        VG_MOVE_TO_ABS = VGPathSegment.VG_MOVE_TO | VGPathAbsRel.VG_ABSOLUTE,
        VG_MOVE_TO_REL = VGPathSegment.VG_MOVE_TO | VGPathAbsRel.VG_RELATIVE,
        VG_LINE_TO_ABS = VGPathSegment.VG_LINE_TO | VGPathAbsRel.VG_ABSOLUTE,
        VG_LINE_TO_REL = VGPathSegment.VG_LINE_TO | VGPathAbsRel.VG_RELATIVE,
        VG_HLINE_TO_ABS = VGPathSegment.VG_HLINE_TO | VGPathAbsRel.VG_ABSOLUTE,
        VG_HLINE_TO_REL = VGPathSegment.VG_HLINE_TO | VGPathAbsRel.VG_RELATIVE,
        VG_VLINE_TO_ABS = VGPathSegment.VG_VLINE_TO | VGPathAbsRel.VG_ABSOLUTE,
        VG_VLINE_TO_REL = VGPathSegment.VG_VLINE_TO | VGPathAbsRel.VG_RELATIVE,
        VG_QUAD_TO_ABS = VGPathSegment.VG_QUAD_TO | VGPathAbsRel.VG_ABSOLUTE,
        VG_QUAD_TO_REL = VGPathSegment.VG_QUAD_TO | VGPathAbsRel.VG_RELATIVE,
        VG_CUBIC_TO_ABS = VGPathSegment.VG_CUBIC_TO | VGPathAbsRel.VG_ABSOLUTE,
        VG_CUBIC_TO_REL = VGPathSegment.VG_CUBIC_TO | VGPathAbsRel.VG_RELATIVE,
        VG_SQUAD_TO_ABS = VGPathSegment.VG_SQUAD_TO | VGPathAbsRel.VG_ABSOLUTE,
        VG_SQUAD_TO_REL = VGPathSegment.VG_SQUAD_TO | VGPathAbsRel.VG_RELATIVE,
        VG_SCUBIC_TO_ABS = VGPathSegment.VG_SCUBIC_TO | VGPathAbsRel.VG_ABSOLUTE,
        VG_SCUBIC_TO_REL = VGPathSegment.VG_SCUBIC_TO | VGPathAbsRel.VG_RELATIVE,
        VG_SCCWARC_TO_ABS = VGPathSegment.VG_SCCWARC_TO | VGPathAbsRel.VG_ABSOLUTE,
        VG_SCCWARC_TO_REL = VGPathSegment.VG_SCCWARC_TO | VGPathAbsRel.VG_RELATIVE,
        VG_SCWARC_TO_ABS = VGPathSegment.VG_SCWARC_TO | VGPathAbsRel.VG_ABSOLUTE,
        VG_SCWARC_TO_REL = VGPathSegment.VG_SCWARC_TO | VGPathAbsRel.VG_RELATIVE,
        VG_LCCWARC_TO_ABS = VGPathSegment.VG_LCCWARC_TO | VGPathAbsRel.VG_ABSOLUTE,
        VG_LCCWARC_TO_REL = VGPathSegment.VG_LCCWARC_TO | VGPathAbsRel.VG_RELATIVE,
        VG_LCWARC_TO_ABS = VGPathSegment.VG_LCWARC_TO | VGPathAbsRel.VG_ABSOLUTE,
        VG_LCWARC_TO_REL = VGPathSegment.VG_LCWARC_TO | VGPathAbsRel.VG_RELATIVE,

        VG_PATH_COMMAND_FORCE_SIZE = OpenVG.VG_MAX_ENUM
    }

    public enum VGPathCapabilities
    {
        VG_PATH_CAPABILITY_APPEND_FROM = (1 << 0),
        VG_PATH_CAPABILITY_APPEND_TO = (1 << 1),
        VG_PATH_CAPABILITY_MODIFY = (1 << 2),
        VG_PATH_CAPABILITY_TRANSFORM_FROM = (1 << 3),
        VG_PATH_CAPABILITY_TRANSFORM_TO = (1 << 4),
        VG_PATH_CAPABILITY_INTERPOLATE_FROM = (1 << 5),
        VG_PATH_CAPABILITY_INTERPOLATE_TO = (1 << 6),
        VG_PATH_CAPABILITY_PATH_LENGTH = (1 << 7),
        VG_PATH_CAPABILITY_POINT_ALONG_PATH = (1 << 8),
        VG_PATH_CAPABILITY_TANGENT_ALONG_PATH = (1 << 9),
        VG_PATH_CAPABILITY_PATH_BOUNDS = (1 << 10),
        VG_PATH_CAPABILITY_PATH_TRANSFORMED_BOUNDS = (1 << 11),
        VG_PATH_CAPABILITY_ALL = (1 << 12) - 1,

        VG_PATH_CAPABILITIES_FORCE_SIZE = OpenVG.VG_MAX_ENUM
    }

    public enum VGPathParamType
    {
        VG_PATH_FORMAT = 0x1600,
        VG_PATH_DATATYPE = 0x1601,
        VG_PATH_SCALE = 0x1602,
        VG_PATH_BIAS = 0x1603,
        VG_PATH_NUM_SEGMENTS = 0x1604,
        VG_PATH_NUM_COORDS = 0x1605,

        VG_PATH_PARAM_TYPE_FORCE_SIZE = OpenVG.VG_MAX_ENUM
    }

    public enum VGCapStyle
    {
        VG_CAP_BUTT = 0x1700,
        VG_CAP_ROUND = 0x1701,
        VG_CAP_SQUARE = 0x1702,

        VG_CAP_STYLE_FORCE_SIZE = OpenVG.VG_MAX_ENUM
    }

    public enum VGJoinStyle
    {
        VG_JOIN_MITER = 0x1800,
        VG_JOIN_ROUND = 0x1801,
        VG_JOIN_BEVEL = 0x1802,

        VG_JOIN_STYLE_FORCE_SIZE = OpenVG.VG_MAX_ENUM
    }

    public enum VGFillRule
    {
        VG_EVEN_ODD = 0x1900,
        VG_NON_ZERO = 0x1901,

        VG_FILL_RULE_FORCE_SIZE = OpenVG.VG_MAX_ENUM
    }

    public enum VGPaintMode
    {
        VG_STROKE_PATH = (1 << 0),
        VG_FILL_PATH = (1 << 1),

        VG_PAINT_MODE_FORCE_SIZE = OpenVG.VG_MAX_ENUM
    }

    public enum VGPaintParamType
    {
        /* Color paint parameters */
        VG_PAINT_TYPE = 0x1A00,
        VG_PAINT_COLOR = 0x1A01,
        VG_PAINT_COLOR_RAMP_SPREAD_MODE = 0x1A02,
        VG_PAINT_COLOR_RAMP_PREMULTIPLIED = 0x1A07,
        VG_PAINT_COLOR_RAMP_STOPS = 0x1A03,

        /* Linear gradient paint parameters */
        VG_PAINT_LINEAR_GRADIENT = 0x1A04,

        /* Radial gradient paint parameters */
        VG_PAINT_RADIAL_GRADIENT = 0x1A05,

        /* Pattern paint parameters */
        VG_PAINT_PATTERN_TILING_MODE = 0x1A06,

        VG_PAINT_PARAM_TYPE_FORCE_SIZE = OpenVG.VG_MAX_ENUM
    }

    public enum VGPaintType
    {
        VG_PAINT_TYPE_COLOR = 0x1B00,
        VG_PAINT_TYPE_LINEAR_GRADIENT = 0x1B01,
        VG_PAINT_TYPE_RADIAL_GRADIENT = 0x1B02,
        VG_PAINT_TYPE_PATTERN = 0x1B03,

        VG_PAINT_TYPE_FORCE_SIZE = OpenVG.VG_MAX_ENUM
    }

    public enum VGColorRampSpreadMode
    {
        VG_COLOR_RAMP_SPREAD_PAD = 0x1C00,
        VG_COLOR_RAMP_SPREAD_REPEAT = 0x1C01,
        VG_COLOR_RAMP_SPREAD_REFLECT = 0x1C02,

        VG_COLOR_RAMP_SPREAD_MODE_FORCE_SIZE = OpenVG.VG_MAX_ENUM
    }

    public enum VGTilingMode
    {
        VG_TILE_FILL = 0x1D00,
        VG_TILE_PAD = 0x1D01,
        VG_TILE_REPEAT = 0x1D02,
        VG_TILE_REFLECT = 0x1D03,

        VG_TILING_MODE_FORCE_SIZE = OpenVG.VG_MAX_ENUM
    }

    public enum VGImageFormat
    {
        /* RGB{A,X} channel ordering */
        VG_sRGBX_8888 = 0,
        VG_sRGBA_8888 = 1,
        VG_sRGBA_8888_PRE = 2,
        VG_sRGB_565 = 3,
        VG_sRGBA_5551 = 4,
        VG_sRGBA_4444 = 5,
        VG_sL_8 = 6,
        VG_lRGBX_8888 = 7,
        VG_lRGBA_8888 = 8,
        VG_lRGBA_8888_PRE = 9,
        VG_lL_8 = 10,
        VG_A_8 = 11,
        VG_BW_1 = 12,
        VG_A_1 = 13,
        VG_A_4 = 14,

        /* {A,X}RGB channel ordering */
        VG_sXRGB_8888 = 0 | (1 << 6),
        VG_sARGB_8888 = 1 | (1 << 6),
        VG_sARGB_8888_PRE = 2 | (1 << 6),
        VG_sARGB_1555 = 4 | (1 << 6),
        VG_sARGB_4444 = 5 | (1 << 6),
        VG_lXRGB_8888 = 7 | (1 << 6),
        VG_lARGB_8888 = 8 | (1 << 6),
        VG_lARGB_8888_PRE = 9 | (1 << 6),

        /* BGR{A,X} channel ordering */
        VG_sBGRX_8888 = 0 | (1 << 7),
        VG_sBGRA_8888 = 1 | (1 << 7),
        VG_sBGRA_8888_PRE = 2 | (1 << 7),
        VG_sBGR_565 = 3 | (1 << 7),
        VG_sBGRA_5551 = 4 | (1 << 7),
        VG_sBGRA_4444 = 5 | (1 << 7),
        VG_lBGRX_8888 = 7 | (1 << 7),
        VG_lBGRA_8888 = 8 | (1 << 7),
        VG_lBGRA_8888_PRE = 9 | (1 << 7),

        /* {A,X}BGR channel ordering */
        VG_sXBGR_8888 = 0 | (1 << 6) | (1 << 7),
        VG_sABGR_8888 = 1 | (1 << 6) | (1 << 7),
        VG_sABGR_8888_PRE = 2 | (1 << 6) | (1 << 7),
        VG_sABGR_1555 = 4 | (1 << 6) | (1 << 7),
        VG_sABGR_4444 = 5 | (1 << 6) | (1 << 7),
        VG_lXBGR_8888 = 7 | (1 << 6) | (1 << 7),
        VG_lABGR_8888 = 8 | (1 << 6) | (1 << 7),
        VG_lABGR_8888_PRE = 9 | (1 << 6) | (1 << 7),

        VG_IMAGE_FORMAT_FORCE_SIZE = OpenVG.VG_MAX_ENUM
    }

    public enum VGImageQuality
    {
        VG_IMAGE_QUALITY_NONANTIALIASED = (1 << 0),
        VG_IMAGE_QUALITY_FASTER = (1 << 1),
        VG_IMAGE_QUALITY_BETTER = (1 << 2),

        VG_IMAGE_QUALITY_FORCE_SIZE = OpenVG.VG_MAX_ENUM
    }

    public enum VGImageParamType
    {
        VG_IMAGE_FORMAT = 0x1E00,
        VG_IMAGE_WIDTH = 0x1E01,
        VG_IMAGE_HEIGHT = 0x1E02,

        VG_IMAGE_PARAM_TYPE_FORCE_SIZE = OpenVG.VG_MAX_ENUM
    }

    public enum VGImageMode
    {
        VG_DRAW_IMAGE_NORMAL = 0x1F00,
        VG_DRAW_IMAGE_MULTIPLY = 0x1F01,
        VG_DRAW_IMAGE_STENCIL = 0x1F02,

        VG_IMAGE_MODE_FORCE_SIZE = OpenVG.VG_MAX_ENUM
    }

    public enum VGImageChannel
    {
        VG_RED = (1 << 3),
        VG_GREEN = (1 << 2),
        VG_BLUE = (1 << 1),
        VG_ALPHA = (1 << 0),

        VG_IMAGE_CHANNEL_FORCE_SIZE = OpenVG.VG_MAX_ENUM
    }

    public enum VGBlendMode
    {
        VG_BLEND_SRC = 0x2000,
        VG_BLEND_SRC_OVER = 0x2001,
        VG_BLEND_DST_OVER = 0x2002,
        VG_BLEND_SRC_IN = 0x2003,
        VG_BLEND_DST_IN = 0x2004,
        VG_BLEND_MULTIPLY = 0x2005,
        VG_BLEND_SCREEN = 0x2006,
        VG_BLEND_DARKEN = 0x2007,
        VG_BLEND_LIGHTEN = 0x2008,
        VG_BLEND_ADDITIVE = 0x2009,

        VG_BLEND_MODE_FORCE_SIZE = OpenVG.VG_MAX_ENUM
    }

    public enum VGFontParamType
    {
        VG_FONT_NUM_GLYPHS = 0x2F00,

        VG_FONT_PARAM_TYPE_FORCE_SIZE = OpenVG.VG_MAX_ENUM
    }

    public enum VGHardwareQueryType
    {
        VG_IMAGE_FORMAT_QUERY = 0x2100,
        VG_PATH_DATATYPE_QUERY = 0x2101,

        VG_HARDWARE_QUERY_TYPE_FORCE_SIZE = OpenVG.VG_MAX_ENUM
    }

    public enum VGHardwareQueryResult
    {
        VG_HARDWARE_ACCELERATED = 0x2200,
        VG_HARDWARE_UNACCELERATED = 0x2201,

        VG_HARDWARE_QUERY_RESULT_FORCE_SIZE = OpenVG.VG_MAX_ENUM
    }

    public enum VGStringID
    {
        VG_VENDOR = 0x2300,
        VG_RENDERER = 0x2301,
        VG_VERSION = 0x2302,
        VG_EXTENSIONS = 0x2303,

        VG_STRING_ID_FORCE_SIZE = OpenVG.VG_MAX_ENUM
    }
}
