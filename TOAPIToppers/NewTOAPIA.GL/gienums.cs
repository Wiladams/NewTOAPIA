using System;

using TOAPI.OpenGL;

namespace NewTOAPIA.GL
{
    public enum Version
    {
        Version11 = 1,
    }




    public enum GLBoolean : byte
    {
        True = 1,
        False = 0,
    }

    public enum AccumOp
    {
        Accum = 0x0100,
        Load = 0x0101,
        Return = 0x0102,
        Mult = 0x0103,
        Add = 0x0104,
    }

    public enum AlphaFunction
    {
        Never = 0x0200,
        Less = 0x0201,
        Equal = 0x0202,
        Lequal = 0x0203,
        Greater = 0x0204,
        Notequal = 0x0205,
        Gequal = 0x0206,
        Always = 0x0207,
    }

    [Flags]
    public enum AttribMask
    {
        CurrentBit = 0x00000001,
        PointBit = 0x00000002,
        LineBit = 0x00000004,
        PolygonBit = 0x00000008,
        PolygonStippleBit = 0x00000010,
        PixelModeBit = 0x00000020,
        LightingBit = 0x00000040,
        FogBit = 0x00000080,
        DepthBufferBit = 0x00000100,
        AccumBufferBit = 0x00000200,
        StencilBufferBit = 0x00000400,
        ViewportBit = 0x00000800,
        TransformBit = 0x00001000,
        EnableBit = 0x00002000,
        ColorBufferBit = 0x00004000,
        HintBit = 0x00008000,
        EvalBit = 0x00010000,
        ListBit = 0x00020000,
        TextureBit = 0x00040000,
        ScissorBit = 0x00080000,
        AllAttribBits = 0x000fffff,
    }

    public enum BeginMode
    {
        Points = 0x0000,
        Lines = 0x0001,
        LineLoop = 0x0002,
        LineStrip = 0x0003,
        Triangles = 0x0004,
        TriangleStrip = 0x0005,
        TriangleFan = 0x0006,
        Quads = 0x0007,
        QuadStrip = 0x0008,
        Polygon = 0x0009,
    }

    public enum BlendEquation
    {
        Add = gl.GL_FUNC_ADD,
        Subtract = gl.GL_FUNC_SUBTRACT,
        ReverseSubtract = gl.GL_FUNC_REVERSE_SUBTRACT,
        Min = gl.GL_MIN,
        Max = gl.GL_MAX
    }

    public enum BlendingFactorDest
    {
        Zero = 0,
        One = 1,
        SrcColor = 0x0300,
        OneMinusSrcColor = 0x0301,
        SrcAlpha = 0x0302,
        OneMinusSrcAlpha = 0x0303,
        DstAlpha = 0x0304,
        OneMinusDstAlpha = 0x0305,
    }

    public enum BlendingFactorSrc
    {
        Zero = 0,
        One = 1,
        SrcColor = gl.GL_SRC_COLOR, // 0x0300
        SrcAlpha = 0x0302,
        OneMinusSrcAlpha = 0x0303,
        DstAlpha = 0x0304,
        OneMinusDstAlpha = 0x0305,
        DstColor = 0x0306,
        OneMinusDstColor = 0x0307,
        SrcAlphaSaturate = 0x0308,
    }

    public enum BufferAccess
    {
        ReadOnly = gl.GL_READ_ONLY,
        WriteOnly = gl.GL_WRITE_ONLY,
        ReadWrite = gl.GL_READ_WRITE
    }

    public enum BufferParameter
    {
        BufferSize = gl.GL_BUFFER_SIZE,
        BufferUsage = gl.GL_BUFFER_USAGE,
        BufferAccess = gl.GL_BUFFER_ACCESS,
        BufferMapped = gl.GL_BUFFER_MAPPED,
        BufferMapPointer = gl.GL_BUFFER_MAP_POINTER
    }

    public enum BufferTarget
    {
        ArrayBuffer = gl.GL_ARRAY_BUFFER,
        ElementArrayBuffer = gl.GL_ELEMENT_ARRAY_BUFFER,
        PixelPackBuffer = gl.GL_PIXEL_PACK_BUFFER_ARB,
        PixelUnpackBuffer = gl.GL_PIXEL_UNPACK_BUFFER_ARB
    }

    public enum BufferUsage
    {
        StreamDraw = gl.GL_STREAM_DRAW, 
        StreamRead = gl.GL_STREAM_READ,
        StreamCopy = gl.GL_STREAM_COPY, 
        StaticDraw = gl.GL_STATIC_DRAW,    // Default
        StaticRead = gl.GL_STATIC_READ, 
        StaticCopy = gl.GL_STATIC_COPY,
        DynamicDraw = gl.GL_DYNAMIC_DRAW, 
        DynamicRead = gl.GL_DYNAMIC_READ,
        DynamicCopy = gl.GL_DYNAMIC_COPY
    }

    [Flags]
    public enum ClearBufferMask
    {
        ColorBufferBit = 0x00004000,
        AccumBufferBit = 0x00000200,
        StencilBufferBit = 0x00000400,
        DepthBufferBit = 0x00000100,
    }

    public enum ClientArrayType
    {
        VertexArray = gl.GL_VERTEX_ARRAY,
        NormalArray = gl.GL_NORMAL_ARRAY,
        ColorArray = gl.GL_COLOR_ARRAY,
        IndexArray = gl.GL_INDEX_ARRAY,
        TextureCoordArray = gl.GL_TEXTURE_COORD_ARRAY,
        EdgeFlagArray = gl.GL_EDGE_FLAG_ARRAY,
    }

    [Flags]
    public enum ClientAttribMask
    {
        ClientPixelStoreBit = 0x00000001,
        ClientVertexArrayBit = 0x00000002,
        ClientAllAttribBits = unchecked((int)0xffffffff),
    }

    public enum ClipPlaneName
    {
        ClipPlane0 = 0x3000,
        ClipPlane1 = 0x3001,
        ClipPlane2 = 0x3002,
        ClipPlane3 = 0x3003,
        ClipPlane4 = 0x3004,
        ClipPlane5 = 0x3005,
    }

    public enum ColorMaterialParameter
    {
        Ambient = 0x1200,
        Diffuse = 0x1201,
        Specular = 0x1202,
        Emission = 0x1600,
        AmbientAndDiffuse = 0x1602,
    }

    public enum ColorPointerType
    {
        Byte = 0x1400,
        UnsignedByte = 0x1401,
        Short = 0x1402,
        UnsignedShort = 0x1403,
        Int = 0x1404,
        UnsignedInt = 0x1405,
        Float = 0x1406,
        Double = 0x140A,
    }

    public enum DataType
    {
        Byte = 0x1400,
        UnsignedByte = 0x1401,
        Short = 0x1402,
        UnsignedShort = 0x1403,
        Int = 0x1404,
        UnsignedInt = 0x1405,
        Float = 0x1406,
        _2Bytes = 0x1407,
        _3Bytes = 0x1408,
        _4Bytes = 0x1409,
        Double = 0x140A,
    }

    public enum DepthFunction
    {
        Never = 0x0200,
        Less = 0x0201,
        Equal = 0x0202,
        Lequal = 0x0203,
        Greater = 0x0204,
        Notequal = 0x0205,
        Gequal = 0x0206,
        Always = 0x0207,
    }

    public enum DrawBufferMode
    {
        None = gl.GL_NONE,
        FrontLeft = gl.GL_FRONT_LEFT,
        FrontRight = gl.GL_FRONT_RIGHT,
        BackLeft = gl.GL_BACK_LEFT,
        BackRight = gl.GL_BACK_RIGHT,
        Front = gl.GL_FRONT,
        Back = gl.GL_BACK,
        Left = gl.GL_LEFT,
        Right = gl.GL_RIGHT,
        FrontAndBack = gl.GL_FRONT_AND_BACK,

        // There could be more auxilary buffers
        Aux0 = 0x0409,
        Aux1 = 0x040A,
        Aux2 = 0x040B,
        Aux3 = 0x040C,
    }

    public enum DrawElementType
    {
        UnsignedByte = 0x1401,
        UnsignedShort = 0x1403,
        UnsignedInt = 0x1405,
    }

    public enum GLErrorCode
    {
        NoError = gl.GL_NO_ERROR,
        InvalidEnum = gl.GL_INVALID_ENUM,
        InvalidValue = gl.GL_INVALID_VALUE,
        InvalidOperation = gl.GL_INVALID_OPERATION,
        StackOverflow = gl.GL_STACK_OVERFLOW,
        StackUnderflow = gl.GL_STACK_UNDERFLOW,
        OutOfMemory = gl.GL_OUT_OF_MEMORY,
        TableTooLarge = gl.GL_TABLE_TOO_LARGE,
    }

    public enum Extensions
    {
        ExtVertexArray = 1,
        ExtBgra = 1,
        ExtPalettedTexture = 1,
        WinSwapHint = 1,
        WinDrawRangeElements = 1,
    }

    public enum FeedBackMode
    {
        _2d = 0x0600,
        _3d = 0x0601,
        _3dColor = 0x0602,
        _3dColorTexture = 0x0603,
        _4dColorTexture = 0x0604,
    }

    public enum FeedBackToken
    {
        PassThroughToken = 0x0700,
        PointToken = 0x0701,
        LineToken = 0x0702,
        PolygonToken = 0x0703,
        BitmapToken = 0x0704,
        DrawPixelToken = 0x0705,
        CopyPixelToken = 0x0706,
        LineResetToken = 0x0707,
    }

    public enum FogMode
    {
        Linear = 0x2601,
        Exp = 0x0800,
        Exp2 = 0x0801,
    }

    public enum FogParameter
    {
        FogColor = 0x0B66,
        FogDensity = 0x0B62,
        FogEnd = 0x0B64,
        FogIndex = 0x0B61,
        FogMode = 0x0B65,
        FogStart = 0x0B63,
    }

    public enum GetMapTarget
    {
        Coeff = 0x0A00,
        Order = 0x0A01,
        Domain = 0x0A02,
    }

    public enum GetPixelMap
    {
        PixelMapIToI = 0x0C70,
        PixelMapSToS = 0x0C71,
        PixelMapIToR = 0x0C72,
        PixelMapIToG = 0x0C73,
        PixelMapIToB = 0x0C74,
        PixelMapIToA = 0x0C75,
        PixelMapRToR = 0x0C76,
        PixelMapGToG = 0x0C77,
        PixelMapBToB = 0x0C78,
        PixelMapAToA = 0x0C79,
    }

    public enum GetPointerTarget
    {
        VertexArrayPointer = 0x808E,
        NormalArrayPointer = 0x808F,
        ColorArrayPointer = 0x8090,
        IndexArrayPointer = 0x8091,
        TextureCoordArrayPointer = 0x8092,
        EdgeFlagArrayPointer = 0x8093,
        FeedbackBufferPointer = 0x0DF0,
        SelectionBufferPointer = 0x0DF3,
    }

    public enum GetTarget
    {
        AccumClearValue = 0x0B80,
        AccumRedBits = 0x0D58,
        AccumGreenBits = 0x0D59,
        AccumBlueBits = 0x0D5A,
        AccumAlphaBits = 0x0D5B,
        AlphaBias = 0x0D1D,
        AlphaBits = 0x0D55,
        AlphaScale = 0x0D1C,
        AlphaTest = 0x0BC0,
        AlphaTestFunc = 0x0BC1,
        AlphaTestRef = 0x0BC2,
        AutoNormal = 0x0D80,
        AuxBuffers = 0x0C00,
        BlendDst = 0x0BE0,
        BlendSrc = 0x0BE1,
        Blend = 0x0BE2,
        ClientAttribStackDepth = 0x0BB1,
        ColorClearValue = 0x0C22,
        ColorWritemask = 0x0C23,
        ColorArray = 0x8076,
        ColorArraySize = 0x8081,
        ColorArrayType = 0x8082,
        ColorArrayStride = 0x8083,
        ColorLogicOp = 0x0BF2,
        ColorMaterialFace = 0x0B55,
        ColorMaterialParameter = 0x0B56,
        ColorMaterial = 0x0B57,
        CullFace = 0x0B44,
        CullFaceMode = 0x0B45,
        CurrentColor = 0x0B00,
        CurrentIndex = 0x0B01,
        CurrentNormal = 0x0B02,
        CurrentProgram = gl.GL_CURRENT_PROGRAM,
        CurrentTextureCoords = 0x0B03,
        CurrentRasterColor = 0x0B04,
        CurrentRasterIndex = 0x0B05,
        CurrentRasterTextureCoords = 0x0B06,
        CurrentRasterPosition = 0x0B07,
        CurrentRasterPositionValid = 0x0B08,
        CurrentRasterDistance = 0x0B09,
        DepthRange = 0x0B70,
        DepthTest = 0x0B71,
        DepthWritemask = 0x0B72,
        DepthClearValue = 0x0B73,
        DepthFunc = 0x0B74,
        EdgeFlag = 0x0B43,
        EdgeFlagArray = 0x8079,
        EdgeFlagArrayBufferBinding = 0x889B,
        EdgeFlagArrayStride = 0x808C,
        ElementArrayBufferBinding = 0x8895,
        Fog = gl.GL_FOG,
        FogIndex = 0x0B61,
        FogDensity = 0x0B62,
        FogStart = 0x0B63,
        FogEnd = gl.GL_FOG_END,
        FogMode = gl.GL_FOG_MODE,
        FogColor = gl.GL_FOG_COLOR,
        FrontFace = gl.GL_FRONT_FACE,
        Lighting = gl.GL_LIGHTING,
        LightModelLocalViewer = 0x0B51,
        LightModelTwoSide = 0x0B52,
        LightModelAmbient = 0x0B53,
        LineSmooth = 0x0B20,
        LineWidth = 0x0B21,
        LineWidthRange = 0x0B22,
        LineWidthGranularity = 0x0B23,
        LineStipple = 0x0B24,
        LineStipplePattern = 0x0B25,
        LineStippleRepeat = 0x0B26,
        ListMode = 0x0B30,
        ListBase = 0x0B32,
        ListIndex = 0x0B33,
        Map1Color4 = 0x0D90,
        Map1Index = 0x0D91,
        Map1Normal = 0x0D92,
        Map1TextureCoord1 = 0x0D93,
        Map1TextureCoord2 = 0x0D94,
        Map1TextureCoord3 = 0x0D95,
        Map1TextureCoord4 = 0x0D96,
        Map1Vertex3 = 0x0D97,
        Map1Vertex4 = 0x0D98,
        Map2Color4 = 0x0DB0,
        Map2Index = 0x0DB1,
        Map2Normal = 0x0DB2,
        Map2TextureCoord1 = 0x0DB3,
        Map2TextureCoord2 = 0x0DB4,
        Map2TextureCoord3 = 0x0DB5,
        Map2TextureCoord4 = 0x0DB6,
        Map2Vertex3 = 0x0DB7,
        Map2Vertex4 = 0x0DB8,
        Map1GridDomain = 0x0DD0,
        Map1GridSegments = 0x0DD1,
        Map2GridDomain = 0x0DD2,
        Map2GridSegments = 0x0DD3,
        MaxAttribStackDepth = 0x0D35,
        MaxClientAttribStackDepth = 0x0D3B,
        MaxClipPlanes = 0x0D32,
        MaxCombinedTextureImageUnits = gl.GL_MAX_COMBINED_TEXTURE_IMAGE_UNITS,
        MaxDrawBuffers = gl.GL_MAX_DRAW_BUFFERS,
        MaxEvalOrder = 0x0D30,
        MaxFragmentUniformComponents = gl.GL_MAX_FRAGMENT_UNIFORM_COMPONENTS,
        MaxLights = 0x0D31,
        MaxListNesting = 0x0B31,
        MaxModelviewStackDepth = 0x0D36,
        MaxNameStackDepth = 0x0D37,
        MaxPixelMapTable = 0x0D34,
        MaxProjectionStackDepth = 0x0D38,
        MaxRectangleTextureSize = gl.GL_MAX_RECTANGLE_TEXTURE_SIZE_ARB,
        MaxRenderBufferSize = gl.GL_MAX_RENDERBUFFER_SIZE_EXT,
        MaxTextureCoords  = gl.GL_MAX_TEXTURE_COORDS,
        MaxTextureImageUnits = gl.GL_MAX_TEXTURE_IMAGE_UNITS,
        MaxTextureSize = 0x0D33,
        MaxTextureStackDepth = 0x0D39,
        MaxTextureUnits = gl.GL_MAX_TEXTURE_UNITS,
        MaxVaryingFloats = gl.GL_MAX_VARYING_FLOATS,
        MaxVertexAttribs = gl.GL_MAX_VERTEX_ATTRIBS,
        MaxVertexTextureImageUnits = gl.GL_MAX_VERTEX_TEXTURE_IMAGE_UNITS,
        MaxVertexUniforComponents = gl.GL_MAX_VERTEX_UNIFORM_COMPONENTS,
        MaxViewportDims = 0x0D3A,
        Normalize = 0x0BA1,
        PackSwapBytes = 0x0D00,
        PackLsbFirst = 0x0D01,
        PackRowLength = 0x0D02,
        PackSkipRows = 0x0D03,
        PackSkipPixels = 0x0D04,
        PackAlignment = 0x0D05,
        PixelMapIToI = 0x0C70,
        PixelMapSToS = 0x0C71,
        PixelMapIToR = 0x0C72,
        PixelMapIToG = 0x0C73,
        PixelMapIToB = 0x0C74,
        PixelMapIToA = 0x0C75,
        PixelMapRToR = 0x0C76,
        PixelMapGToG = 0x0C77,
        PixelMapBToB = 0x0C78,
        PixelMapAToA = 0x0C79,
        PixelMapIToISize = 0x0CB0,
        PixelMapSToSSize = 0x0CB1,
        PixelMapIToRSize = 0x0CB2,
        PixelMapIToGSize = 0x0CB3,
        PixelMapIToBSize = 0x0CB4,
        PixelMapIToASize = 0x0CB5,
        PixelMapRToRSize = 0x0CB6,
        PixelMapGToGSize = 0x0CB7,
        PixelMapBToBSize = 0x0CB8,
        PixelMapAToASize = 0x0CB9,
        PointSmooth = 0x0B10,
        PointSize = 0x0B11,
        PointSizeRange = 0x0B12,
        PointSizeGranularity = 0x0B13,
        PolygonMode = 0x0B40,
        PolygonSmooth = 0x0B41,
        PolygonStipple = 0x0B42,
        ShadeModel = 0x0B54,
        StencilTest = 0x0B90,
        StencilClearValue = 0x0B91,
        StencilFunc = 0x0B92,
        StencilValueMask = 0x0B93,
        StencilFail = 0x0B94,
        StencilPassDepthFail = 0x0B95,
        StencilPassDepthPass = 0x0B96,
        StencilRef = 0x0B97,
        StencilWritemask = 0x0B98,
        Texture1d = 0x0DE0,
        Texture2d = 0x0DE1,
        TextureBinding1d = 0x8068,
        TextureBinding2d = 0x8069,
        TextureCoordArray = 0x8078,
        TextureCoordArrayBufferBinding = 0x889A,
        TextureCoordArraySize = 0x8088,
        TextureCoordArrayType = 0x8089,
        TextureCoordArrayStride = 0x808A,
        TextureEnvColor = 0x2201,
        TextureEnvMode = 0x2200,
        TextureGenS = 0x0C60,
        TextureGenT = 0x0C61,
        TextureGenR = 0x0C62,
        TextureGenQ = 0x0C63,

        MatrixMode = 0x0BA0,
        ModelviewStackDepth = 0x0BA3,
        ModelviewMatrix = 0x0BA6,

        ProjectionStackDepth = 0x0BA4,
        ProjectionMatrix = 0x0BA7,
        TextureStackDepth = 0x0BA5,
        TextureMatrix = 0x0BA8,
        AttribStackDepth = 0x0BB0,
        Dither = 0x0BD0,
        LogicOpMode = 0x0BF0,
        IndexLogicOp = 0x0BF1,
        LogicOp = IndexLogicOp,
        DrawBuffer = 0x0C01,
        ReadBuffer = 0x0C02,
        ScissorBox = 0x0C10,
        ScissorTest = 0x0C11,
        IndexClearValue = 0x0C20,
        IndexWritemask = 0x0C21,
        IndexMode = 0x0C30,
        RgbaMode = 0x0C31,
        Doublebuffer = 0x0C32,
        Stereo = 0x0C33,
        RenderMode = 0x0C40,
        PerspectiveCorrectionHint = 0x0C50,
        PointSmoothHint = 0x0C51,
        LineSmoothHint = 0x0C52,
        PolygonSmoothHint = 0x0C53,
        FogHint = 0x0C54,
        MapColor = 0x0D10,
        MapStencil = 0x0D11,
        IndexShift = 0x0D12,
        IndexOffset = 0x0D13,
        RedScale = 0x0D14,
        RedBias = 0x0D15,
        GreenScale = 0x0D18,
        GreenBias = 0x0D19,
        BlueScale = 0x0D1A,
        BlueBias = 0x0D1B,
        DepthScale = 0x0D1E,
        DepthBias = 0x0D1F,
        SubpixelBits = 0x0D50,
        IndexBits = 0x0D51,
        RedBits = 0x0D52,
        GreenBits = 0x0D53,
        BlueBits = 0x0D54,
        DepthBits = 0x0D56,
        StencilBits = 0x0D57,
        NameStackDepth = 0x0D70,
        FeedbackBufferPointer = 0x0DF0,
        FeedbackBufferSize = 0x0DF1,
        FeedbackBufferType = 0x0DF2,
        SelectionBufferSize = 0x0DF4,
        NormalArray = 0x8075,
        IndexArray = 0x8077,
        NormalArrayType = 0x807E,
        NormalArrayStride = 0x807F,
        IndexArrayType = 0x8085,
        IndexArrayStride = 0x8086,
        PolygonOffsetFactor = 0x8038,
        PolygonOffsetUnits = 0x2A00,
        ArrayBufferBinding = 0x8894,
        NormalArrayBufferBinding = 0x8897,
        ColorArrayBufferBinding = 0x8898,
        IndexArrayBufferBinding = 0x8899,
        SecondaryColorArrayBufferBinding = 0x889C,
        FogCoordinateArrayBufferBinding = 0x889D,

        UnpackSwapBytes = 0x0CF0,
        UnpackLsbFirst = 0x0CF1,
        UnpackRowLength = 0x0CF2,
        UnpackSkipRows = 0x0CF3,
        UnpackSkipPixels = 0x0CF4,
        UnpackAlignment = 0x0CF5,

        VertexArray = 0x8074,
        VertexArrayBufferBinding = 0x8896,
        VertexArraySize = 0x807A,
        VertexArrayStride = 0x807C,
        VertexArrayType = 0x807B,
        VertexAttribArrayBufferBinding = 0x889F,

        Viewport = 0x0BA2,

        WeightArrayBufferBinding = 0x889E,

        ZoomX = 0x0D16,
        ZoomY = 0x0D17,
        
    }

    public enum GetTextureImageFormat
    {
        BgrExt = 0x80E0,
        BgraExt = 0x80E1,
        Red = 0x1903,
        Green = 0x1904,
        Blue = 0x1905,
        Alpha = 0x1906,
        Rgb = 0x1907,
        Rgba = 0x1908,
        Luminance = 0x1909,
        LuminanceAlpha = 0x190A,
    }

    public enum GetTextureImageType
    {
        Byte = 0x1400,
        UnsignedByte = 0x1401,
        Short = 0x1402,
        UnsignedShort = 0x1403,
        Int = 0x1404,
        UnsignedInt = 0x1405,
        Float = 0x1406,
    }

    public enum GetTextureLevelParamter
    {
        TextureWidth = 0x1000,
        TextureHeight = 0x1001,
        TextureInternalFormat = 0x1003,
        TextureComponents = TextureInternalFormat,
        TextureBorder = 0x1005,
        TextureRedSize = 0x805C,
        TextureGreenSize = 0x805D,
        TextureBlueSize = 0x805E,
        TextureAlphaSize = 0x805F,
        TextureLuminanceSize = 0x8060,
        TextureIntensitySize = 0x8061,
    }

    public enum GetTextureParameter
    {
        TextureMagFilter = 0x2800,
        TextureMinFilter = 0x2801,
        TextureWrapS = 0x2802,
        TextureWrapT = 0x2803,
        TextureBorderColor = 0x1004,
        TexturePriority = 0x8066,
        TextureResident = 0x8067,
    }

    public enum GetTextureParameterTarget
    {
        Texture1d = gl.GL_TEXTURE_1D,
        Texture2d = gl.GL_TEXTURE_2D,
        Texture3d = gl.GL_TEXTURE_3D,
        CubeMap = gl.GL_TEXTURE_CUBE_MAP,
    }

    public enum GetTextureImageTarget
    {
        Texture1d = gl.GL_TEXTURE_1D,
        Texture2d = gl.GL_TEXTURE_2D,
        Texture3d = gl.GL_TEXTURE_3D,
        TextureCubeMapPositiveX = gl.GL_TEXTURE_CUBE_MAP_POSITIVE_X,
        TextureCubeMapNegativeX = gl.GL_TEXTURE_CUBE_MAP_NEGATIVE_X,
        TextureCubeMapPositiveY = gl.GL_TEXTURE_CUBE_MAP_POSITIVE_Y,
        TextureCubeMapNegativeY = gl.GL_TEXTURE_CUBE_MAP_NEGATIVE_Y,
        TextureCubeMapPositiveZ = gl.GL_TEXTURE_CUBE_MAP_POSITIVE_Z,
        TextureCubeMapNegativeZ = gl.GL_TEXTURE_CUBE_MAP_NEGATIVE_Z,
    }

    public enum GetTextureLevelTarget
    {
        Texture1d = gl.GL_TEXTURE_1D,
        Texture2d = gl.GL_TEXTURE_2D,
        Texture3d = gl.GL_TEXTURE_3D,
        ProxyTexture1d = gl.GL_PROXY_TEXTURE_1D,
        ProxyTexture2d = gl.GL_PROXY_TEXTURE_2D,
        ProxyTexture3d = gl.GL_PROXY_TEXTURE_3D,
        ProxyTextureCubeMap = gl.GL_PROXY_TEXTURE_CUBE_MAP,
        TextureCubeMapPositiveX = gl.GL_TEXTURE_CUBE_MAP_POSITIVE_X,
        TextureCubeMapNegativeX = gl.GL_TEXTURE_CUBE_MAP_NEGATIVE_X,
        TextureCubeMapPositiveY = gl.GL_TEXTURE_CUBE_MAP_POSITIVE_Y,
        TextureCubeMapNegativeY = gl.GL_TEXTURE_CUBE_MAP_NEGATIVE_Y,
        TextureCubeMapPositiveZ = gl.GL_TEXTURE_CUBE_MAP_POSITIVE_Z,
        TextureCubeMapNegativeZ = gl.GL_TEXTURE_CUBE_MAP_NEGATIVE_Z,
    }

    public enum GLOption
    {
        Fog = 0x0B60,
        PointSmooth = 0x0B10,
        LineSmooth = 0x0B20,
        LineStipple = 0x0B24,
        PolygonSmooth = 0x0B41,
        PolygonStipple = 0x0B42,

        ColorSum = gl.GL_COLOR_SUM,
        CullFace = 0x0B44,
        
        DepthTest = 0x0B71,
        StencilTest = 0x0B90,
        AlphaTest = 0x0BC0,
        
        Blend = 0x0BE2,
        IndexLogicOp = 0x0BF1,
        LogicOp = IndexLogicOp,
        ColorLogicOp = 0x0BF2,
        Dither = 0x0BD0,
        ClipPlane0 = 0x3000,
        ClipPlane1 = 0x3001,
        ClipPlane2 = 0x3002,
        ClipPlane3 = 0x3003,
        ClipPlane4 = 0x3004,
        ClipPlane5 = 0x3005,

        // Lighting
        Lighting = 0x0B50,
        Light0 = 0x4000,
        Light1 = 0x4001,
        Light2 = 0x4002,
        Light3 = 0x4003,
        Light4 = 0x4004,
        Light5 = 0x4005,
        Light6 = 0x4006,
        Light7 = 0x4007,

        // Texturing
        TextureGenS = 0x0C60,
        TextureGenT = 0x0C61,
        TextureGenR = 0x0C62,
        TextureGenQ = 0x0C63,
        Texture1d = 0x0DE0,
        Texture2d = 0x0DE1,
        TextureRectangle = gl.GL_TEXTURE_RECTANGLE_ARB,
        Map1Vertex3 = 0x0D97,
        Map1Vertex4 = 0x0D98,
        Map1Color4 = 0x0D90,
        Map1Index = 0x0D91,
        Map1Normal = 0x0D92,
        Map1TextureCoord1 = 0x0D93,
        Map1TextureCoord2 = 0x0D94,
        Map1TextureCoord3 = 0x0D95,
        Map1TextureCoord4 = 0x0D96,
        Map2Vertex3 = 0x0DB7,
        Map2Vertex4 = 0x0DB8,
        Map2Color4 = 0x0DB0,
        Map2Index = 0x0DB1,
        Map2Normal = 0x0DB2,
        Map2TextureCoord1 = 0x0DB3,
        Map2TextureCoord2 = 0x0DB4,
        Map2TextureCoord3 = 0x0DB5,
        Map2TextureCoord4 = 0x0DB6,
        
        ScissorTest = 0x0C11,
        ColorMaterial = 0x0B57,
        Normalize = 0x0BA1,
        AutoNormal = 0x0D80,
        
        PolygonOffsetPoint = 0x2A01,
        PolygonOffsetLine = 0x2A02,
        PolygonOffsetFill = 0x8037,

        PointSprite = 0x8861,
    
        // Version 1.2 
        // Imaging
        Histogram = gl.GL_HISTOGRAM,
        ColorTable = gl.GL_COLOR_TABLE,
        Convolution2D = gl.GL_CONVOLUTION_2D,
    }

    public enum GLFace
    {
        Front = 0x0404,
        Back = 0x0405,
        FrontAndBack = 0x0408,
    }

    public enum FrontFaceDirection
    {
        Cw = 0x0900,
        Ccw = 0x0901,
    }

    public enum HintMode
    {
        DontCare = 0x1100,
        Fastest = 0x1101,
        Nicest = 0x1102,
    }

    public enum HintTarget
    {
        PerspectiveCorrectionHint = 0x0C50,
        PointSmoothHint = 0x0C51,
        LineSmoothHint = 0x0C52,
        PolygonSmoothHint = 0x0C53,
        FogHint = 0x0C54,
    }

    public enum LightModelParameter
    {
        LightModelAmbient = 0x0B53,
        LightModelLocalViewer = 0x0B51,
        LightModelTwoSide = 0x0B52,
        LightModelColorControl = 0x81F8,
    }

    public enum GLLightName
    {
        Light0 = 0x4000,
        Light1 = 0x4001,
        Light2 = 0x4002,
        Light3 = 0x4003,
        Light4 = 0x4004,
        Light5 = 0x4005,
        Light6 = 0x4006,
        Light7 = 0x4007,
    }

    public enum LightModel
    {
        SeparateSpecularColor = 0x81FA,
        SingleColor = 0x81F9,
    }

    public enum LightParameter
    {
        Ambient = 0x1200,
        Diffuse = 0x1201,
        Specular = 0x1202,
        Position = 0x1203,
        SpotDirection = 0x1204,
        SpotExponent = 0x1205,
        SpotCutoff = 0x1206,
        ConstantAttenuation = 0x1207,
        LinearAttenuation = 0x1208,
        QuadraticAttenuation = 0x1209,
    }

    public enum ListMode
    {
        Compile = 0x1300,
        CompileAndExecute = 0x1301,
    }

    public enum ListNameType
    {
        Byte = 0x1400,
        UnsignedByte = 0x1401,
        Short = 0x1402,
        UnsignedShort = 0x1403,
        Int = 0x1404,
        UnsignedInt = 0x1405,
        Float = 0x1406,
        _2Bytes = 0x1407,
        _3Bytes = 0x1408,
        _4Bytes = 0x1409,
    }

    public enum LogicOp
    {
        Clear = 0x1500,
        And = 0x1501,
        AndReverse = 0x1502,
        Copy = 0x1503,
        AndInverted = 0x1504,
        Noop = 0x1505,
        Xor = 0x1506,
        Or = 0x1507,
        Nor = 0x1508,
        Equiv = 0x1509,
        Invert = 0x150A,
        OrReverse = 0x150B,
        CopyInverted = 0x150C,
        OrInverted = 0x150D,
        Nand = 0x150E,
        Set = 0x150F,
    }

    public enum MapTarget
    {
        Map1Color4 = 0x0D90,
        Map1Index = 0x0D91,
        Map1Normal = 0x0D92,
        Map1TextureCoord1 = 0x0D93,
        Map1TextureCoord2 = 0x0D94,
        Map1TextureCoord3 = 0x0D95,
        Map1TextureCoord4 = 0x0D96,
        Map1Vertex3 = 0x0D97,
        Map1Vertex4 = 0x0D98,
        Map2Color4 = 0x0DB0,
        Map2Index = 0x0DB1,
        Map2Normal = 0x0DB2,
        Map2TextureCoord1 = 0x0DB3,
        Map2TextureCoord2 = 0x0DB4,
        Map2TextureCoord3 = 0x0DB5,
        Map2TextureCoord4 = 0x0DB6,
        Map2Vertex3 = 0x0DB7,
        Map2Vertex4 = 0x0DB8,
    }


    public enum GetMaterialParameter
    {
        Emission = 0x1600,
        Shininess = 0x1601,
        ColorIndexes = 0x1603,
        Ambient = 0x1200,
        Diffuse = 0x1201,
        Specular = 0x1202,
    }

    public enum IndexPointerType
    {
        Short = 0x1402,
        Int = 0x1404,
        Float = 0x1406,
        Double = 0x140A,
    }

    public enum InterleavedArrays
    {
        V2f = 0x2A20,
        V3f = 0x2A21,
        C4ubV2f = 0x2A22,
        C4ubV3f = 0x2A23,
        C3fV3f = 0x2A24,
        N3fV3f = 0x2A25,
        C4fN3fV3f = 0x2A26,
        T2fV3f = 0x2A27,
        T4fV4f = 0x2A28,
        T2fC4ubV3f = 0x2A29,
        T2fC3fV3f = 0x2A2A,
        T2fN3fV3f = 0x2A2B,
        T2fC4fN3fV3f = 0x2A2C,
        T4fC4fN3fV4f = 0x2A2D,
    }

    public enum MaterialParameter
    {
        Emission = 0x1600,
        Shininess = 0x1601,
        AmbientAndDiffuse = 0x1602,
        ColorIndexes = 0x1603,
        Ambient = 0x1200,
        Diffuse = 0x1201,
        Specular = 0x1202,
    }

    public enum MatrixMode
    {
        Modelview = 0x1700,
        Projection = 0x1701,
        Texture = 0x1702,
        Color = gl.GL_COLOR,
    }

    public enum MeshMode1
    {
        Point = 0x1B00,
        Line = 0x1B01,
    }

    public enum MeshMode2
    {
        Point = 0x1B00,
        Line = 0x1B01,
        Fill = 0x1B02,
    }

    public enum NormalPointerType
    {
        Byte = 0x1400,
        Short = 0x1402,
        Int = 0x1404,
        Float = 0x1406,
        Double = 0x140A,
    }

    public enum PolygonMode
    {
        Point = 0x1B00,
        Line = 0x1B01,
        Fill = 0x1B02,
    }

    public enum PolygonOffset
    {
        PolygonOffsetFactor = 0x8038,
        PolygonOffsetUnits = 0x2A00,
        PolygonOffsetPoint = 0x2A01,
        PolygonOffsetLine = 0x2A02,
        PolygonOffsetFill = 0x8037,
    }

 


    public enum ExtPalettedTexture
    {
        ColorTableFormatExt = 0x80D8,
        ColorTableWidthExt = 0x80D9,
        ColorTableRedSizeExt = 0x80DA,
        ColorTableGreenSizeExt = 0x80DB,
        ColorTableBlueSizeExt = 0x80DC,
        ColorTableAlphaSizeExt = 0x80DD,
        ColorTableLuminanceSizeExt = 0x80DE,
        ColorTableIntensitySizeExt = 0x80DF,
    }

    public enum PixelCopyType
    {
        Color = 0x1800,
        Depth = 0x1801,
        Stencil = 0x1802,
    }


    /// <summary>
    /// GLPixelFormat
    /// Used with glDrawPixels.  This looks very similar to the TextureInternalFormat, but there are
    /// a couple here (StencilIndex, DepthComponent),that aren't appropriate for textures.
    /// This format is used for PixelData to represent pixel data taken from files, or other sources.
    /// </summary>

    public enum PixelMap
    {
        PixelMapIToI = 0x0C70,
        PixelMapSToS = 0x0C71,
        PixelMapIToR = 0x0C72,
        PixelMapIToG = 0x0C73,
        PixelMapIToB = 0x0C74,
        PixelMapIToA = 0x0C75,
        PixelMapRToR = 0x0C76,
        PixelMapGToG = 0x0C77,
        PixelMapBToB = 0x0C78,
        PixelMapAToA = 0x0C79,
    }

    public enum PixelStore
    {
        UnpackSwapBytes = 0x0CF0,
        UnpackLsbFirst = 0x0CF1,
        UnpackRowLength = 0x0CF2,
        UnpackSkipRows = 0x0CF3,
        UnpackSkipPixels = 0x0CF4,
        UnpackAlignment = 0x0CF5,
        PackSwapBytes = 0x0D00,
        PackLsbFirst = 0x0D01,
        PackRowLength = 0x0D02,
        PackSkipRows = 0x0D03,
        PackSkipPixels = 0x0D04,
        PackAlignment = 0x0D05,
    }

    public enum PixelTransfer
    {
        MapColor = 0x0D10,
        MapStencil = 0x0D11,

        IndexShift = 0x0D12,
        IndexOffset = 0x0D13,
        
        RedScale = 0x0D14,
        GreenScale = 0x0D18,
        BlueScale = 0x0D1A,
        AlphaScale = 0x0D1C,
        DepthScale = 0x0D1E,

        RedBias = 0x0D15,
        GreenBias = 0x0D19,
        BlueBias = 0x0D1B,
        AlphaBias = 0x0D1D,
        DepthBias = 0x0D1F,

        PostConvolutionRedScale = gl.GL_POST_CONVOLUTION_RED_SCALE,
        PostConvolutionGreenScale = gl.GL_POST_CONVOLUTION_GREEN_SCALE,
        PostConvolutionBlueScale = gl.GL_POST_CONVOLUTION_BLUE_SCALE,
        PostConvolutionAlphaScale = gl.GL_POST_CONVOLUTION_ALPHA_SCALE,

        PostConvolutionRedBias = gl.GL_POST_CONVOLUTION_RED_BIAS,
        PostConvolutionGreenBias = gl.GL_POST_CONVOLUTION_GREEN_BIAS,
        PostConvolutionBlueBias = gl.GL_POST_CONVOLUTION_BLUE_BIAS,
        PostConvolutionAlphaBias = gl.GL_POST_CONVOLUTION_ALPHA_BIAS,

        PostColorMatrixRedScale = gl.GL_POST_COLOR_MATRIX_RED_SCALE,
        PostColorMatrixGreenScale = gl.GL_POST_COLOR_MATRIX_GREEN_SCALE,
        PostColorMatrixBlueScale = gl.GL_POST_COLOR_MATRIX_BLUE_SCALE,
        PostColorMatrixAlphaScale = gl.GL_POST_COLOR_MATRIX_ALPHA_SCALE,

        PostColorMatrixRedBias = gl.GL_POST_COLOR_MATRIX_RED_BIAS,
        PostColorMatrixGreenBias = gl.GL_POST_COLOR_MATRIX_GREEN_BIAS,
        PostColorMatrixBlueBias = gl.GL_POST_COLOR_MATRIX_BLUE_BIAS,
        PostColorMatrixAlphaBias = gl.GL_POST_COLOR_MATRIX_ALPHA_BIAS,
    }


    public enum ReadBufferMode
    {
        FrontLeft = 0x0400,
        FrontRight = 0x0401,
        BackLeft = 0x0402,
        BackRight = 0x0403,
        Front = 0x0404,
        Back = 0x0405,
        Left = 0x0406,
        Right = 0x0407,
        Aux0 = 0x0409,
        Aux1 = 0x040A,
        Aux2 = 0x040B,
        Aux3 = 0x040C,
    }

    public enum RenderingMode
    {
        Render = 0x1C00,
        Feedback = 0x1C01,
        Select = 0x1C02,
    }

    public enum ShadingModel
    {
        Flat = 0x1D00,
        Smooth = 0x1D01,
    }

    public enum StencilFunction
    {
        Never = 0x0200,
        Less = 0x0201,
        Equal = 0x0202,
        Lequal = 0x0203,
        Greater = 0x0204,
        Notequal = 0x0205,
        Gequal = 0x0206,
        Always = 0x0207,
    }

    public enum StencilOp
    {
        Zero = 0,
        Keep = 0x1E00,
        Replace = 0x1E01,
        Incr = 0x1E02,
        Decr = 0x1E03,
        Invert = 0x150A,
    }

    public enum StringName
    {
        Vendor = 0x1F00,
        Renderer = 0x1F01,
        Version = 0x1F02,
        Extensions = 0x1F03,
        ShadingLanguageVersion = 0x00008b8c,
    }

    /// <summary>
    /// TextureInternalFormat
    /// Used with glTexImage2D calls
    /// </summary>
    public enum TextureInternalFormat
    {
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,

        Alpha = gl.GL_ALPHA,
        Alpha4 = 0x803B,
        Alpha8 = 0x803C,
        Alpha12 = 0x803D,
        Alpha16 = 0x803E,
        CompressedAlpha = gl.GL_COMPRESSED_ALPHA,
        DepthComponent = 0x1902,
        DepthComponent16 = 0x81A5,
        DepthComponent24 = 0x81A6,
        DepthComponent32 = 0x81A7,
        Luminance = gl.GL_LUMINANCE,
        Luminance4 = 0x803F,
        Luminance8 = 0x8040,
        Luminance12 = 0x8041,
        Luminance16 = 0x8042,
        CompressedLuminance = gl.GL_COMPRESSED_LUMINANCE,
        LuminanceAlpha = gl.GL_LUMINANCE_ALPHA,
        Luminance4Alpha4 = 0x8043,
        Luminance6Alpha2 = 0x8044,
        Luminance8Alpha8 = 0x8045,
        Luminance12Alpha4 = 0x8046,
        Luminance12Alpha12 = 0x8047,
        Luminance16Alpha16 = 0x8048,
        CompressedLuminanceAlpha = gl.GL_COMPRESSED_LUMINANCE_ALPHA,
        Intensity = 0x8049,
        Intensity4 = 0x804A,
        Intensity8 = 0x804B,
        Intensity12 = 0x804C,
        Intensity16 = 0x804D,
        CompressedIntensity = gl.GL_COMPRESSED_INTENSITY,
        R3G3B2 = 0x2A10,
        Rgb = gl.GL_RGB,
        Rgb4 = gl.GL_RGB4,
        Rgb5 = 0x8050,
        Rgb8 = gl.GL_RGB8 ,
        Rgb10 = 0x8052,
        Rgb12 = 0x8053,
        Rgb16 = 0x8054,
        CompressedRGB = gl.GL_COMPRESSED_RGB,
        Rgba = gl.GL_RGBA,
        Rgba2 = 0x8055,
        Rgba4 = 0x8056,
        Rgb5A1 = 0x8057,
        Rgba8 = gl.GL_RGBA8 ,
        Rgb10A2 = 0x8059,
        Rgba12 = 0x805A,
        Rgba16 = 0x805B,
        CompressedRGBA = gl.GL_COMPRESSED_RGBA,
        //SLuminance = gl.gl_sluminance,
        //SLuminance8 = gl.gl_sluminance8,
        //SLuminanceAlpha = gl.gl_sluminanceAlpha,
        //SLuminance8Alpha8 = gl.gl_sluminance8_Alpha8,
        //SRgb = gl.gl_srgb,
        //SRgb8 = gl.gl_srgb8,
        //SRgbAlpha = gl.gl_SRGB_ALPHA,
        //SRgb8Alpha8 = gl.gl_SRGB8_ALPHA8,

        //TextureRedSize = 0x805C,
        //TextureGreenSize = 0x805D,
        //TextureBlueSize = 0x805E,
        //TextureAlphaSize = 0x805F,
        //TextureLuminanceSize = 0x8060,
        //TextureIntensitySize = 0x8061,
        //ProxyTexture1d = 0x8063,
        //ProxyTexture2d = 0x8064,
    }

    public enum TextureCoordName
    {
        S = 0x2000,
        T = 0x2001,
        R = 0x2002,
        Q = 0x2003,
    }

    public enum TexCoordPointerType
    {
        Short = 0x1402,
        Int = 0x1404,
        Float = 0x1406,
        Double = 0x140A,
    }

    #region Texture Environment
    public enum TextureEnvironmentParameter
    {
        TextureEnvMode = gl.GL_TEXTURE_ENV_MODE,
        TextureLodBias = gl.GL_TEXTURE_LOD_BIAS,

        CombineRGB = gl.GL_COMBINE_RGB,
        CombineAlpha = gl.GL_COMBINE_ALPHA,
        
        SRC0RGB = gl.GL_SRC0_RGB,
        SRC1RGB = gl.GL_SRC1_RGB,
        SRC2RGB = gl.GL_SRC2_RGB,

        SRC0Alpha = gl.GL_SRC1_ALPHA,
        SRC1Alpha = gl.GL_SRC1_ALPHA,
        SRC2Alpha = gl.GL_SRC2_ALPHA,

        Operand0RGB = gl.GL_OPERAND0_RGB,
        Operand1RGB = gl.GL_OPERAND1_RGB,
        Operand2RGB = gl.GL_OPERAND2_RGB,

        Operand0Alpha = gl.GL_OPERAND0_ALPHA,
        Operand1Alpha = gl.GL_OPERAND1_ALPHA,
        Operand2Alpha = gl.GL_OPERAND2_ALPHA,

        RGBScale = gl.GL_RGB_SCALE,
        AlphaScale = gl.GL_ALPHA_SCALE,
        CoordReplace = gl.GL_COORD_REPLACE,
    }


    public enum TextureEnvironmentTarget
    {
        TextureEnv = 0x2300,
        TextureFilter = gl.GL_TEXTURE_FILTER_CONTROL,
        PointSprite = 0x8861,
    }

    public enum TextureEnvTargetParameter
    {
        TextureEnvMode = gl.GL_TEXTURE_ENV_MODE,
        TextureEnvColor = gl.GL_TEXTURE_ENV_COLOR,
        CombineRGB = gl.GL_COMBINE_RGB,
        CombineAlpha = gl.GL_COMBINE_ALPHA,
        RGBScale = gl.GL_RGB_SCALE,
        AlphaScale = gl.GL_ALPHA_SCALE,
        SRC0RGB = gl.GL_SRC0_RGB,
        SRC1RGB = gl.GL_SRC1_RGB,
        SRC2RGB = gl.GL_SRC2_RGB,
        SRC0Alpha = gl.GL_SRC1_ALPHA,
        SRC1Alpha = gl.GL_SRC1_ALPHA,
        SRC2Alpha = gl.GL_SRC2_ALPHA,
    }

    public enum TextureEnvModeParam
    {
        Add = gl.GL_ADD,
        Modulate = 0x2100,
        Decal = 0x2101,
        Blend = 0x0BE2,
        Replace = 0x1E01,
        Combine = gl.GL_COMBINE,
    }

    public enum TextureFilterTargetParameter
    {
        TextureLodBias = gl.GL_TEXTURE_LOD_BIAS,
    }
    #endregion

    public enum TextureGenMode
    {
        EyeLinear = 0x2400,
        ObjectLinear = 0x2401,
        SphereMap = 0x2402,
    }

    public enum TextureGenParameter
    {
        TextureGenMode = 0x2500,
        ObjectPlane = 0x2501,
        EyePlane = 0x2502,
    }

    /// <summary>
    /// TexturePixelFormat
    /// Similar to GLPixelFormat, except this one does not have ColorIndex,
    /// and DepthIndex.
    /// </summary>
    public enum TexturePixelFormat
    {
        ColorIndex = 0x1900,
        Red = 0x1903,
        Green = 0x1904,
        Blue = 0x1905,
        Alpha = 0x1906,
        Rgb = 0x1907,
        Rgba = 0x1908,
        Luminance = 0x1909,
        LuminanceAlpha = 0x190A,
        Bgr = 0x80E0,
        Bgra = 0x80E1,
    }

    public enum TextureMagFilter
    {
        Nearest = 0x2600,
        Linear = 0x2601,
    }

    public enum TextureMinFilter
    {
        Nearest = 0x2600,
        Linear = 0x2601,
        NearestMipmapNearest = 0x2700,
        LinearMipmapNearest = 0x2701,
        NearestMipmapLinear = 0x2702,
        LinearMipmapLinear = 0x2703,
    }

    public enum TextureObject
    {
        TexturePriority = 0x8066,
        TextureResident = 0x8067,
        TextureBinding1d = 0x8068,
        TextureBinding2d = 0x8069,
    }

    public enum TextureParameterName
    {
        TextureMagFilter = 0x2800,
        TextureMinFilter = 0x2801,
        TextureWrapS = 0x2802,
        TextureWrapT = 0x2803,
        TextureWrapR = 0x8072,
        TextureBorderColor = 0x1004,
        TexturePriority = 0x8066,
        GenerateMipmap = 0x8191,
        TextureMinLod = 0x813A,
        TextureMaxLod = 0x813B,
        TextureBaseLevel = 0x813C,
        TextureMaxLevel = 0x813D,
        TextureMaxAnisotropy = 0x84FE,
        DepthTextureMode = 0x884B,
        TextureCompareMode = 0x884C,
        TextureCompareFunc = 0x884D,
        TextureCompareFailValue = 0x80BF,
    }


    public enum TextureShadow
    {
        TextureCompareModeARB = 0x884C,
        TextureCompareFuncARB = 0x884D,
        CompareRToTextureARB = 0x884E,
        LEqual = 0x0203,
        GEqual = 0x0206,
    }

    public enum Texture1DTarget
    {
        Texture1d = 0x0DE0,
        ProxyTexture1d = 0x8063,
    }

    public enum Texture2DTarget
    {
        Texture2d = gl.GL_TEXTURE_2D,
        ProxyTexture2d = gl.GL_PROXY_TEXTURE_2D,
        ProxyTextureCubeMap = gl.GL_PROXY_TEXTURE_CUBE_MAP,
        TextureCubeMapPositiveX = gl.GL_TEXTURE_CUBE_MAP_POSITIVE_X,
        TextureCubeMapNegativeX = gl.GL_TEXTURE_CUBE_MAP_NEGATIVE_X,
        TextureCubeMapPositiveY = gl.GL_TEXTURE_CUBE_MAP_POSITIVE_Y,
        TextureCubeMapNegativeY = gl.GL_TEXTURE_CUBE_MAP_NEGATIVE_Y,
        TextureCubeMapPositiveZ = gl.GL_TEXTURE_CUBE_MAP_POSITIVE_Z,
        TextureCubeMapNegativeZ = gl.GL_TEXTURE_CUBE_MAP_NEGATIVE_Z,
    }

    public enum TextureBindTarget
    {
        Rectangle = gl.GL_TEXTURE_RECTANGLE_ARB,
        Texture1d = gl.GL_TEXTURE_1D,
        Texture2d = gl.GL_TEXTURE_2D,
        Texture3d = gl.GL_TEXTURE_3D,
        TextureCubeMap = gl.GL_TEXTURE_CUBE_MAP,
    }

    public enum TextureParameterTarget
    {
        Texture1d = gl.GL_TEXTURE_1D,
        Texture2d = gl.GL_TEXTURE_2D,
        Texture3d = gl.GL_TEXTURE_3D,
        TextureCubeMap = gl.GL_TEXTURE_CUBE_MAP,
    }

    public enum TextureArrayParameterTarget
    {
        Texture1d = gl.GL_TEXTURE_1D,
        Texture2d = gl.GL_TEXTURE_2D,
        Texture3d = gl.GL_TEXTURE_3D,
    }

    public enum TextureUnitID
    {
        Unit0 = gl.GL_TEXTURE0,
        Unit1 = gl.GL_TEXTURE1,
        Unit2 = gl.GL_TEXTURE2,
        Unit3 = gl.GL_TEXTURE3,
        Unit4 = gl.GL_TEXTURE4,
        Unit5 = gl.GL_TEXTURE5,
        Unit6 = gl.GL_TEXTURE6,
        Unit7 = gl.GL_TEXTURE7,
        Unit8 = gl.GL_TEXTURE8,
        Unit9 = gl.GL_TEXTURE9,
        Unit10 = gl.GL_TEXTURE10,
        Unit11 = gl.GL_TEXTURE11,
        Unit12 = gl.GL_TEXTURE12,
        Unit13 = gl.GL_TEXTURE13,
        Unit14 = gl.GL_TEXTURE14,
        Unit15 = gl.GL_TEXTURE15,
        Unit16 = gl.GL_TEXTURE16,
        Unit17 = gl.GL_TEXTURE17,
        Unit18 = gl.GL_TEXTURE18,
        Unit19 = gl.GL_TEXTURE19,
        Unit20 = gl.GL_TEXTURE20,
        Unit21 = gl.GL_TEXTURE21,
        Unit22 = gl.GL_TEXTURE22,
        Unit23 = gl.GL_TEXTURE23,
        Unit24 = gl.GL_TEXTURE24,
        Unit25 = gl.GL_TEXTURE25,
        Unit26 = gl.GL_TEXTURE26,
        Unit27 = gl.GL_TEXTURE27,
        Unit28 = gl.GL_TEXTURE28,
        Unit29 = gl.GL_TEXTURE29,
        Unit30 = gl.GL_TEXTURE30,
        Unit31 = gl.GL_TEXTURE31,
    }

    public enum TextureWrapMode
    {
        Clamp = gl.GL_CLAMP,
        ClampToBorder = gl.GL_CLAMP_TO_BORDER,
        Repeat = gl.GL_REPEAT,
        MirroredRepeat = gl.GL_MIRRORED_REPEAT,
    }

    public enum UniformType
    {
        Float = gl.GL_FLOAT,
        FloatVec2 = gl.GL_FLOAT_VEC2,
        FloatVec3 = gl.GL_FLOAT_VEC3,
        FloatVec4 = gl.GL_FLOAT_VEC4,
        Int = gl.GL_INT,
        IntVec2 = gl.GL_INT_VEC2,
        IntVec3 = gl.GL_INT_VEC3,
        IntVec4 = gl.GL_INT_VEC4,
        Bool = gl.GL_BOOL,
        BoolVec2 = gl.GL_BOOL_VEC2,
        BoolVec3 = gl.GL_BOOL_VEC3,
        BoolVec4 = gl.GL_BOOL_VEC4,
        FloatMat2 = gl.GL_FLOAT_MAT2,
        FloatMat3 = gl.GL_FLOAT_MAT3,
        FloatMat4 = gl.GL_FLOAT_MAT4,
        Sampler1D = gl.GL_SAMPLER_1D,
        Sampler2D = gl.GL_SAMPLER_2D,
        Sampler3D = gl.GL_SAMPLER_3D,
        SamplerCube = gl.GL_SAMPLER_CUBE,
        Sampler1DShadow = gl.GL_SAMPLER_1D_SHADOW,
        Sampler2DShadow = gl.GL_SAMPLER_2D_SHADOW,
    }

    public enum VertexArray
    {
        VertexArray = 0x8074,
        NormalArray = 0x8075,
        ColorArray = 0x8076,
        IndexArray = 0x8077,
        TextureCoordArray = 0x8078,
        EdgeFlagArray = 0x8079,
        VertexArraySize = 0x807A,
        VertexArrayType = 0x807B,
        VertexArrayStride = 0x807C,
        NormalArrayType = 0x807E,
        NormalArrayStride = 0x807F,
        ColorArraySize = 0x8081,
        ColorArrayType = 0x8082,
        ColorArrayStride = 0x8083,
        IndexArrayType = 0x8085,
        IndexArrayStride = 0x8086,
        TextureCoordArraySize = 0x8088,
        TextureCoordArrayType = 0x8089,
        TextureCoordArrayStride = 0x808A,
        EdgeFlagArrayStride = 0x808C,
        VertexArrayPointer = 0x808E,
        NormalArrayPointer = 0x808F,
        ColorArrayPointer = 0x8090,
        IndexArrayPointer = 0x8091,
        TextureCoordArrayPointer = 0x8092,
        EdgeFlagArrayPointer = 0x8093,
        V2f = 0x2A20,
        V3f = 0x2A21,
        C4ubV2f = 0x2A22,
        C4ubV3f = 0x2A23,
        C3fV3f = 0x2A24,
        N3fV3f = 0x2A25,
        C4fN3fV3f = 0x2A26,
        T2fV3f = 0x2A27,
        T4fV4f = 0x2A28,
        T2fC4ubV3f = 0x2A29,
        T2fC3fV3f = 0x2A2A,
        T2fN3fV3f = 0x2A2B,
        T2fC4fN3fV3f = 0x2A2C,
        T4fC4fN3fV4f = 0x2A2D,
    }

    public enum ExtVertexArray
    {
        VertexArrayExt = 0x8074,
        NormalArrayExt = 0x8075,
        ColorArrayExt = 0x8076,
        IndexArrayExt = 0x8077,
        TextureCoordArrayExt = 0x8078,
        EdgeFlagArrayExt = 0x8079,
        VertexArraySizeExt = 0x807A,
        VertexArrayTypeExt = 0x807B,
        VertexArrayStrideExt = 0x807C,
        VertexArrayCountExt = 0x807D,
        NormalArrayTypeExt = 0x807E,
        NormalArrayStrideExt = 0x807F,
        NormalArrayCountExt = 0x8080,
        ColorArraySizeExt = 0x8081,
        ColorArrayTypeExt = 0x8082,
        ColorArrayStrideExt = 0x8083,
        ColorArrayCountExt = 0x8084,
        IndexArrayTypeExt = 0x8085,
        IndexArrayStrideExt = 0x8086,
        IndexArrayCountExt = 0x8087,
        TextureCoordArraySizeExt = 0x8088,
        TextureCoordArrayTypeExt = 0x8089,
        TextureCoordArrayStrideExt = 0x808A,
        TextureCoordArrayCountExt = 0x808B,
        EdgeFlagArrayStrideExt = 0x808C,
        EdgeFlagArrayCountExt = 0x808D,
        VertexArrayPointerExt = 0x808E,
        NormalArrayPointerExt = 0x808F,
        ColorArrayPointerExt = 0x8090,
        IndexArrayPointerExt = 0x8091,
        TextureCoordArrayPointerExt = 0x8092,
        EdgeFlagArrayPointerExt = 0x8093,
        DoubleExt = 0x140A,
    }


    public enum VertexPointerType
    {
        Short = 0x1402,
        Int = 0x1404,
        Float = 0x1406,
        Double = 0x140A,
    }



    public enum WinDrawRangeElements
    {
        MaxElementsVerticesWin = 0x80E8,
        MaxElementsIndicesWin = 0x80E9,
    }

    public enum WinPhongShading
    {
        PhongWin = 0x80EA,
        PhongHintWin = 0x80EB,
    }

    public enum WinSpecularFog
    {
        FogSpecularTextureWin = 0x80EC,
    }


}
