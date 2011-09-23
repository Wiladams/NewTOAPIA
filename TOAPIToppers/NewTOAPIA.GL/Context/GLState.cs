

namespace NewTOAPIA.GL
{
    using TOAPI.OpenGL;
    using TOAPI.Types;

    using NewTOAPIA.Graphics;

    public class GLState
    {
        public GraphicsInterface GI { get; private set; }

        public GLState(GraphicsInterface graphicInterface)
        {
            GI = graphicInterface;
        }


        //GL_ACCUM_ALPHA_BITS The params parameter returns one value: the number of alpha bitplanes in the accumulation buffer.  
        public int AccumAlphaBits
        {
            get {
                int retValue = GI.GetInteger(GetTarget.AccumAlphaBits);
                return retValue;
            }

            set {
            }
        }

        //GL_ACCUM_BLUE_BITS The params parameter returns one value: the number of blue bitplanes in the accumulation buffer.  
        public int AccumBlueBits
        {
            get {
                int retValue = GI.GetInteger(GetTarget.AccumBlueBits);
                return retValue;
            }

            set {
            }
        }

        //GL_ACCUM_GREEN_BITS The params parameter returns one value: the number of green bitplanes in the accumulation buffer.  
        public int AccumGreenBits
        {
            get {
                int retValue = GI.GetInteger(GetTarget.AccumGreenBits);
                return retValue;

            }

            set {
            }
        }


        //GL_ACCUM_RED_BITS The params parameter returns one value: the number of red bitplanes in the accumulation buffer.  
        public int AccumRedBits
        {
            get {
                int retValue = GI.GetInteger(GetTarget.AccumRedBits);
                return retValue;
            }

            set {
            }
        }

        //GL_ALPHA_BITS The params parameter returns one value: the number of alpha bitplanes in each color buffer.  
        public int AlphaBits
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.AlphaBits);
                return retValue;
            }

            set
            {
            }
        }

        //GL_ACCUM_CLEAR_VALUE The params parameter returns four values: the red, green, blue, and alpha values used to clear the accumulation buffer. Integer values, if requested, are linearly mapped from the internal floating-point representation such that 1.0 returns the most positive representable integer value, and 1.0 returns the most negative representable integer value. See glClearAccum.  
        public double[] AccumClearValue
        {
            get
            {
                double[] parameters = new double[4];
                GI.GetDouble(GetTarget.AccumClearValue, parameters);

                return parameters;
            }

            set
            {
            }
        }

        //GL_ALPHA_BIAS The params parameter returns one value: the alpha bias factor used during pixel transfers. See glPixelTransfer.  
        public double AlphaBias
        {
            get {
                double retValue = GI.GetDouble(GetTarget.AlphaBias);
                return retValue;
            }

            set {
            }
        }


        //GL_ALPHA_SCALE The params parameter returns one value: the alpha scale factor used during pixel transfers. See glPixelTransfer.  
        public double AlphaScale
        {
            get
            {
                double retValue = GI.GetDouble(GetTarget.AlphaScale);
                return retValue;
            }

            set
            {
            }
        }

        //GL_ALPHA_TEST The params parameter returns a single Boolean value indicating whether alpha testing of fragments is enabled. See glAlphaFunc.  
        public bool AlphaTest    
        {
            get {
                bool retValue = GI.GetBoolean(GetTarget.AlphaTest);
                return retValue;
            }

            set {
            }
        }

        //GL_ALPHA_TEST_FUNC The params parameter returns one value: the symbolic name of the alpha test function. See glAlphaFunc.  
        public int AlphaTestFunction
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.AlphaTestFunc);
                return retValue;
            }

            set
            {
            }
        }

        //GL_ALPHA_TEST_REF The params parameter returns one value: the reference value for the alpha test. See glAlphaFunc. An integer value, if requested, is linearly mapped from the internal floating-point representation such that 1.0 returns the most positive representable integer value, and 1.0 returns the most negative representable integer value.  
        public double AlphaTestReference
        {
            get
            {
                double retValue = GI.GetDouble(GetTarget.AlphaTestRef);
                return retValue;
            }

            set
            {
            }
        }

        //GL_ATTRIB_STACK_DEPTH The params parameter returns one value: the depth of the attribute stack. If the stack is empty, zero is returned. See glPushAttrib.  
        public int AttributeStackDepth
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.AttribStackDepth);
                return retValue;
            }

            set
            {
            }
        }

        //GL_AUTO_NORMAL The params parameter returns a single Boolean value indicating whether 2-D map evaluation automatically generates surface normals. See glMap2.  
        public bool AutoNormal
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.AutoNormal);
                return retValue;
            }

            set
            {
            }
        }

        //GL_AUX_BUFFERS The params parameter returns one value: the number of auxiliary color buffers.  
        public int AuxBuffers
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.AuxBuffers);
                return retValue;
            }

            set
            {
            }
        }
        
        //GL_BLEND The params parameter returns a single Boolean value indicating whether blending is enabled. See glBlendFunc.  
        public bool Blend
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.Blend);
                return retValue;
            }

            set
            {
            }
        }

        //GL_BLEND_DST The params parameter returns one value: the symbolic constant identifying the destination blend function. See glBlendFunc.  
        public BlendingFactorDest BlendDst
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.BlendDst);
                return (BlendingFactorDest)retValue;
            }

            set
            {
            }
        }


        //GL_BLEND_SRC The params parameter returns one value: the symbolic constant identifying the source blend function. See glBlendFunc.  
        public BlendingFactorSrc BlendSrc
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.BlendSrc);
                return (BlendingFactorSrc)retValue;
            }

            set
            {
            }
        }

        //GL_BLUE_BIAS The params parameter returns one value: the blue bias factor used during pixel transfers. See glPixelTransfer.  
        public double BlueBias
        {
            get
            {
                double retValue = GI.GetDouble(GetTarget.BlueBias);
                return retValue;
            }

            set
            {
            }
        }

        //GL_BLUE_BITS The params parameter returns one value: the number of blue bitplanes in each color buffer.  
        public int BlueBits
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.BlueBits);
                return retValue;
            }

            set
            {
            }
        }

        //GL_BLUE_SCALE The params parameter returns one value: the blue scale factor used during pixel transfers. See glPixelTransfer.  
        public double BlueScale
        {
            get
            {
                double retValue = GI.GetDouble(GetTarget.BlueScale);
                return retValue;
            }

            set
            {
            }
        }

        //GL_CLIENT_ATTRIB_STACK_DEPTH The params parameter returns one value indicating the depth of the attribute stack. The initial value is zero. See glPushClientAttrib. 
        public int ClientAttribStackDepth
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.ClientAttribStackDepth);
                return retValue;
            }

            set
            {
            }
        }
        
      
        //GL_CLIP_PLANEi The params parameter returns a single Boolean value indicating whether the specified clipping plane is enabled. See glClipPlane.  
        //public bool ClipPlane
        
        //GL_COLOR_ARRAY The params parameter returns a single Boolean value indicating whether the specified clipping plane is enabled. See glClipPlane. 

        //GL_COLOR_ARRAY_SIZE The params parameter returns one value, the number of components per color in the color array. See glColorPointer. 
        public int ColorArraySize
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.ColorArraySize);
                return retValue;
            }

            set
            {
            }
        }

        //GL_COLOR_ARRAY_STRIDE The params parameter returns one value, the byte offset between consecutive colors in the color array. See glColorPointer. 
        public int ColorArrayStride
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.ColorArrayStride);
                return retValue;
            }

            set
            {
            }
        }

        //GL_COLOR_ARRAY_TYPE The params parameter returns one value, the data type of each component in the color array. See glColorPointer. 
        public ColorPointerType ColorArrayType
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.ColorArrayType);
                return (ColorPointerType)retValue;
            }

            set
            {
            }
        }

        //GL_COLOR_CLEAR_VALUE The params parameter returns four values: the red, green, blue, and alpha values used to clear the color buffers. Integer values, if requested, are linearly mapped from the internal floating-point representation such that 1.0 returns the most positive representable integer value, and 1.0 returns the most negative representable integer value. See glClearColor.  
        public ColorRGBA ColorClearValue
        {
            get
            {
                float[] parameters = new float[4];
                GI.GetFloat(GetTarget.ColorClearValue, parameters);
                ColorRGBA newColor = new ColorRGBA(parameters);
                return newColor;
            }

            set
            {
            }
        }

        //GL_COLOR_LOGIC_OP The params parameter returns a single Boolean value indicating whether a fragment's RGBA color values are merged into the framebuffer using a logical operation. See glLogicOp. 
        public bool ColorLogicOp
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.ColorLogicOp);
                return retValue;
            }

            set
            {
            }
        }

        //GL_COLOR_MATERIAL The params parameter returns a single Boolean value indicating whether one or more material parameters are tracking the current color. See glColorMaterial.  
        public bool ColorMaterial
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.ColorMaterial);
                return retValue;
            }

            set
            {
            }
        }

        
        //GL_COLOR_MATERIAL_FACE The params parameter returns one value: a symbolic constant indicating which materials have a parameter that is tracking the current color. See glColorMaterial.  
        public GLFace ColorMaterialFace
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.ColorMaterialFace);
                return (GLFace)retValue;
            }

            set
            {
            }
        }

        //GL_COLOR_MATERIAL_PARAMETER The params parameter returns one value: a symbolic constant indicating which material parameters are tracking the current color. See glColorMaterial.  
        public ColorMaterialParameter ColorMaterialParameter
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.ColorMaterialParameter);
                return (ColorMaterialParameter)retValue;
            }

            set
            {
            }
        }

        //GL_COLOR_WRITEMASK The params parameter returns four Boolean values: the red, green, blue, and alpha write enables for the color buffers. See glColorMask.  

        //GL_CULL_FACE The params parameter returns a single Boolean value indicating whether polygon culling is enabled. See glCullFace.  
        public bool CullFace
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.CullFace);
                return retValue;
            }

            set
            {
            }
        }

        //GL_CULL_FACE_MODE The params parameter returns one value: a symbolic constant indicating which polygon faces are to be culled. See glCullFace.  
        public GLFace CullFaceMode
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.CullFaceMode);
                return (GLFace)retValue;
            }

            set
            {
            }
        }

        //GL_CURRENT_COLOR The params parameter returns four values: the red, green, blue, and alpha values of the current color. Integer values, if requested, are linearly mapped from the internal floating-point representation such that 1.0 returns the most positive representable integer value, and 1.0 returns the most negative representable integer value. See glColor.  
        public ColorRGBA CurrentColor
        {
            get
            {
                float[] parameters = new float[4];
                GI.GetFloat(GetTarget.CurrentColor, parameters);
                ColorRGBA newColor = new ColorRGBA(parameters);
                return newColor;
            }

            set
            {
            }
        }

        //GL_CURRENT_INDEX The params parameter returns one value: the current color index. See glIndex.  
        public int CurrentIndex
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.CurrentIndex);
                return retValue;
            }

            set
            {
            }
        }

        //GL_CURRENT_NORMAL The params parameter returns three values: the x, y, and z values of the current normal. Integer values, if requested, are linearly mapped from the internal floating-point representation such that 1.0 returns the most positive representable integer value, and 1.0 returns the most negative representable integer value. See glNormal.  

        //GL_CURRENT_RASTER_COLOR The params parameter returns four values: the red, green, blue, and alpha values of the current raster position. Integer values, if requested, are linearly mapped from the internal floating-point representation such that 1.0 returns the most positive representable integer value, and 1.0 returns the most negative representable integer value. See glRasterPos.  
        public ColorRGBA CurrentRasterColor
        {
            get
            {
                float[] parameters = new float[4];
                GI.GetFloat(GetTarget.CurrentRasterColor, parameters);
                ColorRGBA newColor = new ColorRGBA(parameters);
                return newColor;
            }

            set
            {
            }
        }

        //GL_CURRENT_RASTER_DISTANCE The params parameter returns one value: the distance from the eye to the current raster position. See glRasterPos.  
        public float CurrentRasterDistance
        {
            get
            {
                float retValue = GI.GetFloat(GetTarget.CurrentRasterDistance);
                return retValue;
            }

            set
            {
            }
        }

        //GL_CURRENT_RASTER_INDEX The params parameter returns one value: the color index of the current raster position. See glRasterPos.  
        public int CurrentRasterIndex
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.CurrentRasterIndex);
                return retValue;
            }

            set
            {
            }
        }

        //GL_CURRENT_RASTER_POSITION The params parameter returns four values: the x, y, z, and w components of the current raster position. The x, y, and z components are in window coordinates, and w is in clip coordinates. See glRasterPos.  

        //GL_CURRENT_RASTER_POSITION_VALID The params parameter returns a single Boolean value indicating whether the current raster position is valid. See glRasterPos. 
        public bool CurrentRasterPositionValid
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.CurrentRasterPositionValid);
                return retValue;
            }

            set
            {
            }
        }

        //GL_CURRENT_RASTER_TEXTURE_COORDS The params parameter returns four values: the s, t, r, and q current raster texture coordinates. See glRasterPos and glTexCoord.  

        //GL_CURRENT_TEXTURE_COORDS The params parameter returns four values: the s, t, r, and q current texture coordinates. See glTexCoord.  

        //GL_DEPTH_BIAS The params parameter returns one value: the depth bias factor used during pixel transfers. See glPixelTransfer.  
        public float DepthBias
        {
            get
            {
                float retValue = GI.GetFloat(GetTarget.DepthBias);
                return retValue;
            }

            set
            {
            }
        }

        //GL_DEPTH_BITS The params parameter returns one value: the number of bitplanes in the depth buffer.  
        public int DepthBits
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.DepthBits);
                return retValue;
            }

            set
            {
            }
        }

        //GL_DEPTH_CLEAR_VALUE The params parameter returns one value: the value that is used to clear the depth buffer. Integer values, if requested, are linearly mapped from the internal floating-point representation such that 1.0 returns the most positive representable integer value, and 1.0 returns the most negative representable integer value. See glClearDepth.  
        public float DepthClearValue
        {
            get
            {
                float retValue = GI.GetFloat(GetTarget.DepthClearValue);
                return retValue;
            }

            set
            {
            }
        }

        //GL_DEPTH_FUNC The params parameter returns one value: the symbolic constant that indicates the depth comparison function. See glDepthFunc.  
        public DepthFunction DepthFunc
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.DepthFunc);
                return (DepthFunction)retValue;
            }

            set
            {
            }
        }

        //GL_DEPTH_RANGE The params parameter returns two values: the near and far mapping limits for the depth buffer. Integer values, if requested, are linearly mapped from the internal floating-point representation such that 1.0 returns the most positive representable integer value, and 1.0 returns the most negative representable integer value. See glDepthRange.  

        //GL_DEPTH_SCALE The params parameter returns one value: the depth scale factor used during pixel transfers. See glPixelTransfer.  
        public float DepthScale
        {
            get
            {
                float retValue = GI.GetFloat(GetTarget.DepthScale);
                return retValue;
            }

            set
            {
            }
        }

        //GL_DEPTH_TEST The params parameter returns a single Boolean value indicating whether depth testing of fragments is enabled. See glDepthFunc and glDepthRange.  
        public bool DepthTest
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.DepthTest);
                return retValue;
            }

            set
            {
            }
        }

        //GL_DEPTH_WRITEMASK The params parameter returns a single Boolean value indicating if the depth buffer is enabled for writing. See glDepthMask.  
        public bool DepthWriteMask
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.DepthWritemask);
                return retValue;
            }

            set
            {
            }
        }

        //GL_DITHER The params parameter returns a single Boolean value indicating whether dithering of fragment colors and indexes is enabled.  
        public bool Dither
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.Dither);
                return retValue;
            }

            set
            {
            }
        }

        //GL_DOUBLEBUFFER The params parameter returns a single Boolean value indicating whether double buffering is supported.  
        public bool DoubleBuffer
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.Doublebuffer);
                return retValue;
            }

            set
            {
            }
        }

        //GL_DRAW_BUFFER The params parameter returns one value: a symbolic constant indicating which buffers are being drawn to. See glDrawBuffer.  
        public DrawBufferMode DrawBuffer
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.DrawBuffer);
                return (DrawBufferMode)retValue;
            }

            set
            {
            }
        }

        //GL_EDGE_FLAG The params parameter returns a single Boolean value indicating whether the current edge flag is true or false. See glEdgeFlag.  
        public bool EdgeFlag
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.EdgeFlag);
                return retValue;
            }

            set
            {
            }
        }

        //GL_EDGE_FLAG_ARRAY The params parameter returns a single Boolean value indicating whether the edge flag array is enabled. See glEdgeFlagPointer. 
        public bool EdgeFlagArray
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.EdgeFlagArray);
                return retValue;
            }

            set
            {
            }
        }

        //GL_EDGE_FLAG_ARRAY_STRIDE The params parameter returns one value, the byte offset between consecutive edge flags in the edge flag array. See glEdgeFlagPointer. 
        public int EdgeFlagArrayStride
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.EdgeFlagArrayStride);
                return retValue;
            }

            set
            {
            }
        }

        //GL_FOG The params parameter returns a single Boolean value indicating whether fogging is enabled. See glFog.  
        public bool Fog
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.Fog);
                return retValue;
            }

            set
            {
            }
        }

        //GL_FOG_COLOR The params parameter returns four values: the red, green, blue, and alpha components of the fog color. Integer values, if requested, are linearly mapped from the internal floating-point representation such that 1.0 returns the most positive representable integer value, and 1.0 returns the most negative representable integer value. See glFog.  
        public ColorRGBA FogColor
        {
            get
            {
                float[] parameters = new float[4];
                GI.GetFloat(GetTarget.FogColor, parameters);
                ColorRGBA newColor = new ColorRGBA(parameters);
                return newColor;
            }

            set
            {
            }
        }

        //GL_FOG_DENSITY The params parameter returns one value: the fog density parameter. See glFog.  
        public float FogDensity
        {
            get
            {
                float aFloat = GI.GetFloat(GetTarget.FogDensity);
                return aFloat;
            }

            set
            {
            }
        }

        //GL_FOG_END The params parameter returns one value: the end factor for the linear fog equation. See glFog.  
        public float FogEnd
        {
            get
            {
                float aFloat = GI.GetFloat(GetTarget.FogEnd);
                return aFloat;
            }

            set
            {
            }
        }

        //GL_FOG_HINT The params parameter returns one value: a symbolic constant indicating the mode of the fog hint. See glHint.  
        public FogParameter FogHint
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.FogHint);
                return (FogParameter)retValue;
            }

            set
            {
            }
        }

        //GL_FOG_INDEX The params parameter returns one value: the fog color index. See glFog.  
        public int FogIndex
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.FogIndex);
                return retValue;
            }

            set
            {
            }
        }

        //GL_FOG_MODE The params parameter returns one value: a symbolic constant indicating which fog equation is selected. See glFog.  
        public FogMode FogMode
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.FogMode);
                return (FogMode)retValue;
            }

            set
            {
            }
        }

        //GL_FOG_START The params parameter returns one value: the start factor for the linear fog equation. See glFog.  
        public float FogStart
        {
            get
            {
                float aFloat = GI.GetFloat(GetTarget.FogStart);
                return aFloat;
            }

            set
            {
            }
        }

        //GL_FRONT_FACE The params parameter returns one value: a symbolic constant indicating whether clockwise or counterclockwise polygon winding is treated as front-facing. See glFrontFace.  
        public FrontFaceDirection FrontFace
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.FrontFace);
                return (FrontFaceDirection)retValue;
            }

            set
            {
            }
        }

        //GL_GREEN_BIAS The params parameter returns one value: the green bias factor used during pixel transfers.  

        //GL_GREEN_BITS The params parameter returns one value: the number of green bitplanes in each color buffer.  
        public int GreenBits
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.GreenBits);
                return retValue;
            }

            set
            {
            }
        }

        //GL_GREEN_SCALE The params parameter returns one value: the green scale factor used during pixel transfers. See glPixelTransfer.  

        //GL_INDEX_ARRAY The params parameter returns a single Boolean value indicating whether the color index array is enabled. See glIndexPointer. 
        public bool IndexArray
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.IndexArray);
                return retValue;
            }

            set
            {
            }
        }

        //GL_INDEX_ARRAY_STRIDE The params parameter returns one value, the byte offset between consecutive color indexes in the color index array. See glIndexPointer. 
        public int IndexArrayStride
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.IndexArrayStride);
                return retValue;
            }

            set
            {
            }
        }

        //GL_INDEX_ARRAY_TYPE The params parameter returns one value, the data type of indexes in the color index array. The initial value is GL_FLOAT. See glIndexPointer. 
        public IndexPointerType IndexArrayType
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.IndexArrayType);
                return (IndexPointerType)retValue;
            }

            set
            {
            }
        }

        //GL_INDEX_BITS The params parameter returns one value: the number of bitplanes in each color-index buffer.  
        public int IndexBits
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.IndexBits);
                return retValue;
            }

            set
            {
            }
        }

        //GL_INDEX_CLEAR_VALUE The params parameter returns one value: the color index used to clear the color-index buffers. See glClearIndex.  
        public int IndexClearValue
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.IndexClearValue);
                return retValue;
            }

            set
            {
            }
        }

        //GL_INDEX_LOGIC_OP The params parameter returns a single Boolean value indicating whether a fragment's index values are merged into the framebuffer using a logical operation. See glLogicOp. 
        public bool IndexLogicOp
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.IndexLogicOp);
                return retValue;
            }

            set
            {
            }
        }

        //GL_INDEX_MODE The params parameter returns a single Boolean value indicating whether OpenGL is in color-index mode (TRUE) or RGBA mode (FALSE).  
        public bool IndexMode
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.IndexMode);
                return retValue;
            }

            set
            {
            }
        }

        //GL_INDEX_OFFSET The params parameter returns one value: the offset added to color and stencil indexes during pixel transfers. See glPixelTransfer.  
        public int IndexOffset
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.IndexOffset);
                return retValue;
            }

            set
            {
            }
        }

        //GL_INDEX_SHIFT The params parameter returns one value: the amount that color and stencil indexes are shifted during pixel transfers. See glPixelTransfer.  
        public int IndexShift
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.IndexShift);
                return retValue;
            }

            set
            {
            }
        }

        //GL_INDEX_WRITEMASK The params parameter returns one value: a mask indicating which bitplanes of each color-index buffer can be written. See glIndexMask.  

        //GL_LIGHTi The params parameter returns a single Boolean value indicating whether the specified light is enabled. See glLight and glLightModel.  
        //public bool Light0
        //{
        //    get
        //    {
        //        bool retValue = GI.GetBoolean(GetTarget.Light0);
        //        return retValue;
        //    }

        //    set
        //    {
        //    }
        //}

        //GL_LIGHTING The params parameter returns a single Boolean value indicating whether lighting is enabled. See glLightModel.  
        public bool Lighting
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.Lighting);
                return retValue;
            }

            set
            {
            }
        }

        //GL_LIGHT_MODEL_AMBIENT The params parameter returns four values: the red, green, blue, and alpha components of the ambient intensity of the entire scene. Integer values, if requested, are linearly mapped from the internal floating-point representation such that 1.0 returns the most positive representable integer value, and 1.0 returns the most negative representable integer value. See glLightModel.  
        public ColorRGBA LightModelAmbient
        {
            get
            {
                float[] parameters = new float[4];
                GI.GetFloat(GetTarget.LightModelAmbient, parameters);
                ColorRGBA newColor = new ColorRGBA(parameters);
                return newColor;
            }

            set
            {
            }
        }

        //GL_LIGHT_MODEL_LOCAL_VIEWER The params parameter returns a single Boolean value indicating whether specular reflection calculations treat the viewer as being local to the scene. See glLightModel.  
        public bool LightModelLocalViewer
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.LightModelLocalViewer);
                return retValue;
            }

            set
            {
            }
        }

        //GL_LIGHT_MODEL_TWO_SIDE The params parameter returns a single Boolean value indicating whether separate materials are used to compute lighting for front- and back-facing polygons. See glLightModel.  
        public bool LightModelTwoSide
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.LightModelTwoSide);
                return retValue;
            }

            set
            {
            }
        }

        //GL_LINE_SMOOTH The params parameter returns a single Boolean value indicating whether antialiasing of lines is enabled. See glLineWidth.  
        public bool LineSmooth
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.LineSmooth);
                return retValue;
            }

            set
            {
            }
        }

        //GL_LINE_SMOOTH_HINT The params parameter returns one value: a symbolic constant indicating the mode of the line antialiasing hint. See glHint.  
        public HintMode LineSmoothHint
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.LineSmoothHint);
                return (HintMode)retValue;
            }

            set
            {
            }
        }

        //GL_LINE_STIPPLE The params parameter returns a single Boolean value indicating whether stippling of lines is enabled. See glLineStipple.  
        public bool LineStipple
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.LineStipple);
                return retValue;
            }

            set
            {
            }
        }

        //GL_LINE_STIPPLE_PATTERN The params parameter returns one value: the 16-bit line stipple pattern. See glLineStipple.  

        //GL_LINE_STIPPLE_REPEAT The params parameter returns one value: the line stipple repeat factor. See glLineStipple.  
        public int LineStippleRepeat
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.LineStippleRepeat);
                return retValue;
            }

            set
            {
            }
        }

        //GL_LINE_WIDTH The params parameter returns one value: the line width as specified with glLineWidth.  
        public float LineWidth
        {
            get
            {
                float retValue = GI.GetFloat(GetTarget.LineWidth);
                return retValue;
            }

            set
            {
                GI.LineWidth(value);
            }
        }

        //GL_LINE_WIDTH_GRANULARITY The params parameter returns one value: the width difference between adjacent supported widths for antialiased lines. See glLineWidth.  
        public float LineWidthGranularity
        {
            get
            {
                float retValue = GI.GetFloat(GetTarget.LineWidthGranularity);
                return retValue;
            }

            set
            {
            }
        }

        //GL_LINE_WIDTH_RANGE The params parameter returns two values: the smallest and largest supported widths for antialiased lines. See glLineWidth.  
        public vec2 LineWidthRange
        {
            get
            {
                float[] parameters = new float[2];
                GI.GetFloat(GetTarget.LineWidthRange, parameters);
                vec2 retSize = new vec2(parameters[0], parameters[1]);

                return retSize;
            }

            set
            {
            }
        }

        //GL_LIST_BASE The params parameter returns one value: the base offset added to all names in arrays presented to glCallLists. See glListBase.  
        public int ListBase
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.ListBase);
                return retValue;
            }

            set
            {
            }
        }

        //GL_LIST_INDEX The params parameter returns one value: the name of the display list currently under construction. Zero is returned if no display list is currently under construction. See glNewList.  
        public int ListIndex
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.ListIndex);
                return retValue;
            }

            set
            {
            }
        }

        //GL_LIST_MODE The params parameter returns one value: a symbolic constant indicating the construction mode of the display list currently being constructed. See glNewList.  
        public ListMode ListMode
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.ListMode);
                return (ListMode)retValue;
            }

            set
            {
            }
        }

        //GL_LOGIC_OP The params parameter returns a single Boolean value indicating whether fragment indexes are merged into the framebuffer using a logical operation. See glLogicOp.  
        public bool LogicOp
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.LogicOp);
                return retValue;
            }

            set
            {
            }
        }

        //GL_LOGIC_OP_MODE The params parameter returns one value: a symbolic constant indicating the selected logic operational mode. See glLogicOp.  
        public LogicOp LogicOpMode
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.LogicOpMode);
                return (LogicOp)retValue;
            }

            set
            {
            }
        }

        //GL_MAP1_COLOR_4 The params parameter returns a single Boolean value indicating whether 1-D evaluation generates colors. See glMap1.  
        public bool Map1Color4
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.AlphaTest);
                return retValue;
            }

            set
            {
            }
        }

        //GL_MAP1_GRID_DOMAIN The params parameter returns two values: the endpoints of the 1-D maps grid domain. See glMapGrid.  

        //GL_MAP1_GRID_SEGMENTS The params parameter returns one value: the number of partitions in the 1-D maps grid domain. See glMapGrid.  
        public int Map1GridSegments
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.Map1GridSegments);
                return retValue;
            }

            set
            {
            }
        }

        //GL_MAP1_INDEX The params parameter returns a single Boolean value indicating whether 1-D evaluation generates color indexes. See glMap1.  
        public bool Map1Index
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.Map1Index);
                return retValue;
            }

            set
            {
            }
        }

        //GL_MAP1_NORMAL The params parameter returns a single Boolean value indicating whether 1-D evaluation generates normals. See glMap1.  
        public bool Map1Normal
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.Map1Normal);
                return retValue;
            }

            set
            {
            }
        }

        //GL_MAP1_TEXTURE_COORD_1 The params parameter returns a single Boolean value indicating whether 1-D evaluation generates 1-D texture coordinates. See glMap1.  
        public bool Map1TextureCoord1
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.Map1TextureCoord1);
                return retValue;
            }

            set
            {
            }
        }

        //GL_MAP1_TEXTURE_COORD_2 The params parameter returns a single Boolean value indicating whether 1-D evaluation generates 2-D texture coordinates. See glMap1.  
        public bool Map1TextureCoord2
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.Map1TextureCoord2);
                return retValue;
            }

            set
            {
            }
        }

        //GL_MAP1_TEXTURE_COORD_3 The params parameter returns a single Boolean value indicating whether 1-D evaluation generates 3-D texture coordinates. See glMap1.  
        public bool Map1TextureCoord3
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.Map1TextureCoord3);
                return retValue;
            }

            set
            {
            }
        }

        //GL_MAP1_TEXTURE_COORD_4 The params parameter returns a single Boolean value indicating whether 1-D evaluation generates 4-D texture coordinates. See glMap1.  
        public bool Map1TextureCoord4
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.Map1TextureCoord4);
                return retValue;
            }

            set
            {
            }
        }

        //GL_MAP1_VERTEX_3 The params parameter returns a single Boolean value indicating whether 1-D evaluation generates 3-D vertex coordinates. See glMap1.  
        public bool Map1Vertex3
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.Map1Vertex3);
                return retValue;
            }

            set
            {
            }
        }

        //GL_MAP1_VERTEX_4 The params parameter returns a single Boolean value indicating whether 1-D evaluation generates 4-D vertex coordinates. See glMap1.  
        public bool Map1Vertex4
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.Map1Vertex4);
                return retValue;
            }

            set
            {
            }
        }

        //GL_MAP2_COLOR_4 The params parameter returns a single Boolean value indicating whether 2-D evaluation generates colors. See glMap2.  
        public bool Map2Color4
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.Map2Color4);
                return retValue;
            }

            set
            {
            }
        }

        //GL_MAP2_GRID_DOMAIN The params parameter returns four values: the endpoints of the 2-D maps i and j grid domains. See glMapGrid.  

        //GL_MAP2_GRID_SEGMENTS The params parameter returns two values: the number of partitions in the 2-D maps i and j grid domains. See glMapGrid.  

        //GL_MAP2_INDEX The params parameter returns a single Boolean value indicating whether 2-D evaluation generates color indexes. See glMap2.  
        public bool Map2Index
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.Map2Index);
                return retValue;
            }

            set
            {
            }
        }

        //GL_MAP2_NORMAL The params parameter returns a single Boolean value indicating whether 2-D evaluation generates normals. See glMap2.  
        public bool Map2Normal
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.Map2Normal);
                return retValue;
            }

            set
            {
            }
        }

        //GL_MAP2_TEXTURE_COORD_1 The params parameter returns a single Boolean value indicating whether 2-D evaluation generates 1-D texture coordinates. See glMap2.  
        public bool Map2TextureCoord1
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.Map2TextureCoord1);
                return retValue;
            }

            set
            {
            }
        }

        //GL_MAP2_TEXTURE_COORD_2 The params parameter returns a single Boolean value indicating whether 2-D evaluation generates 2-D texture coordinates. See glMap2.  
        public bool Map2TextureCoord2
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.Map2TextureCoord2);
                return retValue;
            }

            set
            {
            }
        }

        //GL_MAP2_TEXTURE_COORD_3 The params parameter returns a single Boolean value indicating whether 2-D evaluation generates 3-D texture coordinates. See glMap2.  
        public bool Map2TextureCoord3
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.Map2TextureCoord3);
                return retValue;
            }

            set
            {
            }
        }

        //GL_MAP2_TEXTURE_COORD_4 The params parameter returns a single Boolean value indicating whether 2-D evaluation generates 4-D texture coordinates. See glMap2.  
        public bool Map2TextureCoord4
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.Map2TextureCoord4);
                return retValue;
            }

            set
            {
            }
        }

        //GL_MAP2_VERTEX_3 The params parameter returns a single Boolean value indicating whether 2-D evaluation generates 3-D vertex coordinates. See glMap2.  
        public bool Map2Vertex3
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.Map2Vertex3);
                return retValue;
            }

            set
            {
            }
        }

        //GL_MAP2_VERTEX_4 The params parameter returns a single Boolean value indicating whether 2-D evaluation generates 4-D vertex coordinates. See glMap2.  
        public bool Map2Vertex4
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.Map2Vertex4);
                return retValue;
            }

            set
            {
            }
        }

        //GL_MAP_COLOR The params parameter returns a single Boolean value indicating whether colors and color indexes are to be replaced by table lookup during pixel transfers. See glPixelTransfer.  
        public bool MapColor
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.MapColor);
                return retValue;
            }

            set
            {
            }
        }

        //GL_MAP_STENCIL The params parameter returns a single Boolean value indicating whether stencil indexes are to be replaced by table lookup during pixel transfers. See glPixelTransfer.  
        public bool MapStencil
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.MapStencil);
                return retValue;
            }

            set
            {
            }
        }

        //GL_MATRIX_MODE The params parameter returns one value: a symbolic constant indicating which matrix stack is currently the target of all matrix operations. See glMatrixMode.  
        public MatrixMode MatrixMode
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.MatrixMode);
                return (MatrixMode)retValue;
            }

            set
            {
            }
        }

        //GL_MAX_CLIENT_ATTRIB_STACK_DEPTH The params parameter returns one value indicating the maximum supported depth of the client attribute stack. See glPushClientAttrib.  
        public int MaxClientAttribStackDepth
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.MaxClientAttribStackDepth);
                return retValue;
            }

            set
            {
            }
        }

        //GL_MAX_ATTRIB_STACK_DEPTH The params parameter returns one value: the maximum supported depth of the attribute stack. See glPushAttrib.  
        public int MaxAttribStackDepth
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.MaxAttribStackDepth);
                return retValue;
            }

            set
            {
            }
        }

        //GL_MAX_CLIP_PLANES The params parameter returns one value: the maximum number of application-defined clipping planes. See glClipPlane.  
        public int MaxClipPlanes
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.MaxClipPlanes);
                return retValue;
            }

            set
            {
            }
        }

        //GL_MAX_EVAL_ORDER The params parameter returns one value: the maximum equation order supported by 1-D and 2-D evaluators. See glMap1 and glMap2.  
        public int MaxEvalOrder
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.MaxEvalOrder);
                return retValue;
            }

            set
            {
            }
        }

        //GL_MAX_LIGHTS The params parameter returns one value: the maximum number of lights. See glLight.  
        public int MaxLights
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.MaxLights);
                return retValue;
            }

            set
            {
            }
        }

        //GL_MAX_LIST_NESTING The params parameter returns one value: the maximum recursion depth allowed during display-list traversal. See glCallList.  
        public int MaxListNesting
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.MaxListNesting);
                return retValue;
            }

            set
            {
            }
        }

        //GL_MAX_MODELVIEW_STACK_DEPTH The params parameter returns one value: the maximum supported depth of the modelview matrix stack. See glPushMatrix.  
        public int MaxModelViewStackDepth
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.MaxModelviewStackDepth);
                return retValue;
            }

            set
            {
            }
        }

        //GL_MAX_NAME_STACK_DEPTH The params parameter returns one value: the maximum supported depth of the selection name stack. See glPushName.  
        public int MaxNameStackDepth
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.MaxNameStackDepth);
                return retValue;
            }

            set
            {
            }
        }

        //GL_MAX_PIXEL_MAP_TABLE The params parameter returns one value: the maximum supported size of a glPixelMap lookup table. See glPixelMap.  
        public int MaxPixelMapTable
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.MaxPixelMapTable);
                return retValue;
            }

            set
            {
            }
        }

        //GL_MAX_PROJECTION_STACK_DEPTH The params parameter returns one value: the maximum supported depth of the projection matrix stack. See glPushMatrix.  
        public int MaxProjectionStackDepth
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.MaxProjectionStackDepth);
                return retValue;
            }

            set
            {
            }
        }

        // GL_MAX_RECTANGLE_TEXTURE_SIZE_ARB
        public int MaxRectangleTextureSize
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.MaxRectangleTextureSize);
                return retValue;
            }
        }

        //GL_MAX_TEXTURE_SIZE The params parameter returns one value: the maximum width or height of any texture image (without borders). See glTexImage1D and glTexImage2D.  
        public int MaxTextureSize
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.MaxTextureSize);
                return retValue;
            }

            set
            {
            }
        }

        //GL_MAX_TEXTURE_STACK_DEPTH The params parameter returns one value: the maximum supported depth of the texture matrix stack. See glPushMatrix.  
        public int MaxTextureStackDepth
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.MaxTextureStackDepth);
                return retValue;
            }

            set
            {
            }
        }

        public int MaxTextureUnits
        {
            get
            {
                int maxUnits = GI.GetInteger(GetTarget.MaxTextureUnits);
                return maxUnits;
            }
        }

        //GL_MAX_VIEWPORT_DIMS The params parameter returns two values: the maximum supported width and height of the viewport. See glViewport.  

        //GL_MODELVIEW_MATRIX The params parameter returns 16 values: the modelview matrix on the top of the modelview matrix stack. See glPushMatrix.  

        //GL_MODELVIEW_STACK_DEPTH  The params parameter returns one value: the number of matrices on the modelview matrix stack. See glPushMatrix.  
        public int ModelviewStackDepth
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.ModelviewStackDepth);
                return retValue;
            }

            set
            {
            }
        }

        //GL_NAME_STACK_DEPTH  The params parameter returns one value: the number of names on the selection name stack. See glPushName.  
        public int NameStackDepth
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.NameStackDepth);
                return retValue;
            }

            set
            {
            }
        }

        //GL_NORMAL_ARRAY The params parameter returns a single Boolean value, indicating whether the normal array is enabled. See glNormalPointer. 
        public bool NormalArray
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.NormalArray);
                return retValue;
            }

            set
            {
            }
        }

        //GL_NORMAL_ARRAY_STRIDE The params parameter returns one value, the byte offset between consecutive normals in the normal array. See glNormalPointer. 
        public int NormalArrayStride
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.NormalArrayStride);
                return retValue;
            }

            set
            {
            }
        }

        //GL_NORMAL_ARRAY_TYPE The params parameter returns one value, the data type of each coordinate in the normal array. See glNormalPointer. 
        public NormalPointerType NormalArrayType
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.NormalArrayType);
                return (NormalPointerType)retValue;
            }

            set
            {
            }
        }

        //GL_NORMALIZE  The params parameter returns a single Boolean value indicating whether normals are automatically scaled to unit length after they have been transformed to eye coordinates. See glNormal.  
        public bool Normalize
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.Normalize);
                return retValue;
            }

            set
            {
            }
        }

        //GL_PACK_ALIGNMENT  The params parameter returns one value: the byte alignment used for writing pixel data to memory. See glPixelStore.  

        //GL_PACK_LSB_FIRST  The params parameter returns a single Boolean value indicating whether single-bit pixels being written to memory are written first to the least significant bit of each unsigned byte. See glPixelStore.  
        public bool PackLSBFirst
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.PackLsbFirst);
                return retValue;
            }

            set
            {
            }
        }

        //GL_PACK_ROW_LENGTH  The params parameter returns one value: the row length used for writing pixel data to memory. See glPixelStore.  

        //GL_PACK_SKIP_PIXELS  The params parameter returns one value: the number of pixel locations skipped before the first pixel is written into memory. See glPixelStore.  
        public int PackSkipPixels
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.PackSkipPixels);
                return retValue;
            }

            set
            {
            }
        }

        //GL_PACK_SKIP_ROWS  The params parameter returns one value: the number of rows of pixel locations skipped before the first pixel is written into memory. See glPixelStore.  
        public int PackSkipRows
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.PackSkipRows);
                return retValue;
            }

            set
            {
            }
        }

        //GL_PACK_SWAP_BYTES  The params parameter returns a single Boolean value indicating whether the bytes of 2-byte and 4-byte pixel indexes and components are swapped before being written to memory. See glPixelStore.  
        public bool PackSwapBytes
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.PackSwapBytes);
                return retValue;
            }

            set
            {
            }
        }

        //GL_PERSPECTIVE_CORRECTION_HINT  The params parameter returns one value: a symbolic constant indicating the mode of the perspective correction hint. See glHint.  
        public HintMode PerspectiveCorrectionHint
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.PerspectiveCorrectionHint);
                return (HintMode)retValue;
            }

            set
            {
            }
        }

        //GL_PIXEL_MAP_A_TO_A_SIZE  The params parameter returns one value: the size of the alpha-to-alpha pixel-translation table. See glPixelMap.  
        public int PixelMapAToASize
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.PixelMapAToASize);
                return retValue;
            }

            set
            {
            }
        }

        //GL_PIXEL_MAP_B_TO_B_SIZE  The params parameter returns one value: the size of the blue-to-blue pixel-translation table. See glPixelMap.  
        public int PixelMapBToBSize
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.PixelMapBToBSize);
                return retValue;
            }

            set
            {
            }
        }

        //GL_PIXEL_MAP_G_TO_G_SIZE  The params parameter returns one value: the size of the green-to-green pixel-translation table. See glPixelMap.  
        public int PixelMapGToGSize
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.PixelMapGToGSize);
                return retValue;
            }

            set
            {
            }
        }

        //GL_PIXEL_MAP_I_TO_A_SIZE  The params parameter returns one value: the size of the index-to-alpha pixel translation table. See glPixelMap.  
        public int PixelMapIToASize
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.PixelMapIToASize);
                return retValue;
            }

            set
            {
            }
        }

        //GL_PIXEL_MAP_I_TO_B_SIZE  The params parameter returns one value: the size of the index-to-blue pixel translation table. See glPixelMap.  
        public int PixelMapIToBSize
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.PixelMapIToBSize);
                return retValue;
            }

            set
            {
            }
        }

        //GL_PIXEL_MAP_I_TO_G_SIZE  The params parameter returns one value: the size of the index-to-green pixel-translation table. See glPixelMap.  
        public int PixelMapIToGSize
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.PixelMapIToGSize);
                return retValue;
            }

            set
            {
            }
        }

        //GL_PIXEL_MAP_I_TO_I_SIZE  The params parameter returns one value: the size of the index-to-index pixel-translation table. See glPixelMap.  
        public int PixelMapIToISize
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.PixelMapIToISize);
                return retValue;
            }

            set
            {
            }
        }

        //GL_PIXEL_MAP_I_TO_R_SIZE  The params parameter returns one value: the size of the index-to-red pixel-translation table. See glPixelMap.  
        public int PixelMapIToRSize
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.PixelMapIToRSize);
                return retValue;
            }

            set
            {
            }
        }

        //GL_PIXEL_MAP_R_TO_R_SIZE  The params parameter returns one value: the size of the red-to-red pixel-translation table. See glPixelMap.  
        public int PixelMapRToRSize
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.PixelMapRToRSize);
                return retValue;
            }

            set
            {
            }
        }

        //GL_PIXEL_MAP_S_TO_S_SIZE  The params parameter returns one value: the size of the stencil-to-stencil pixel translation table. See glPixelMap.  
        public int PixelMapSToSSize
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.PixelMapSToSSize);
                return retValue;
            }

            set
            {
            }
        }

        //GL_POINT_SIZE  The params parameter returns one value: the point size as specified by glPointSize.  
        public float PointSize
        {
            get
            {
                float retValue = GI.GetFloat(GetTarget.PointSize);
                return retValue;
            }

            set
            {
                GI.PointSize(value);
            }
        }

        //GL_POINT_SIZE_GRANULARITY  The params parameter returns one value: the size difference between adjacent supported sizes for antialiased points. See glPointSize.  
        public float PointSizeGranularity
        {
            get
            {
                float retValue = GI.GetFloat(GetTarget.PointSizeGranularity);
                return retValue;
            }

            set
            {
            }
        }

        //GL_POINT_SIZE_RANGE  The params parameter returns two values: the smallest and largest supported sizes for antialiased points. See glPointSize.  
        public float PointSizeRange
        {
            get
            {
                float retValue = GI.GetFloat(GetTarget.PointSizeRange);
                return retValue;
            }

            set
            {
            }
        }

        //GL_POINT_SMOOTH The params parameter returns a single Boolean value indicating whether antialiasing of points is enabled. See glPointSize.  
        public bool PointSmooth
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.PointSmooth);
                return retValue;
            }

            set
            {
            }
        }

        //GL_POINT_SMOOTH_HINT The params parameter returns one value: a symbolic constant indicating the mode of the point antialiasing hint. See glHint.  
        public HintMode PointSmoothHint
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.PointSmoothHint);
                return (HintMode)retValue;
            }

            set
            {
            }
        }

        //GL_POLYGON_MODE The params parameter returns two values: symbolic constants indicating whether front-facing and back-facing polygons are rasterized as points, lines, or filled polygons. See glPolygonMode.  
        public PolygonMode PolygonMode
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.PolygonMode);
                return (PolygonMode)retValue;
            }

            set
            {
            }
        }

        //GL_POLYGON_OFFSET_FACTOR The params parameter returns one value, the scaling factor used to determine the variable offset that is added to the depth value of each fragment generated when a polygon is rasterized. See glPolygonOffset. 
        public float PolygonOffsetFactor
        {
            get
            {
                float retValue = GI.GetFloat(GetTarget.PolygonOffsetFactor);
                return retValue;
            }

            set
            {
            }
        }

        //GL_POLYGON_OFFSET_UNITS The params parameter returns one value. This value is multiplied by an implementation-specific value and then added to the depth value of each fragment generated when a polygon is rasterized. See glPolygonOffset. 
        public float PolygonOffsetUnits
        {
            get
            {
                float retValue = GI.GetFloat(GetTarget.PolygonOffsetUnits);
                return retValue;
            }

            set
            {
            }
        }

        //GL_POLYGON_OFFSET_FILL The params parameter returns a single Boolean value indicating whether polygon offset is enabled for polygons in fill mode. See glPolygonOffset. 
        public bool PolygonOffsetFill
        {
            get
            {
                int[] parameters = new int[1];
                gl.glGetBooleanv(gl.GL_POLYGON_OFFSET_FILL, parameters);

                return (parameters[0] == 1);
            }

            set
            {
            }
        }

        //GL_POLYGON_OFFSET_LINE The params parameter returns a single Boolean value indicating whether polygon offset is enabled for polygons in line mode. See glPolygonOffset. 
        public bool PolygonOffsetLine
        {
            get
            {
                int[] parameters = new int[1];
                gl.glGetBooleanv(gl.GL_POLYGON_OFFSET_LINE, parameters);

                return (parameters[0] == 1);
            }

            set
            {
            }
        }

        //GL_POLYGON_OFFSET_POINT The params parameter returns a single Boolean value indicating whether polygon offset is enabled for polygons in point mode. See glPolygonOffset. 
        public bool PolygonOffsetPoint
        {
            get
            {
                int[] parameters = new int[1];
                gl.glGetBooleanv(gl.GL_POLYGON_OFFSET_POINT, parameters);

                return (parameters[0] == 1);
            }

            set
            {
            }
        }

        //GL_POLYGON_SMOOTH The params parameter returns a single Boolean value indicating whether antialiasing of polygons is enabled. See glPolygonMode.  
        public bool PolygonSmooth
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.PolygonSmooth);
                return retValue;
            }

            set
            {
            }
        }

        //GL_POLYGON_SMOOTH_HINT The params parameter returns one value: a symbolic constant indicating the mode of the polygon antialiasing hint. See glHint.  
        public HintMode PolygonSmoothHint
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.PolygonSmoothHint);
                return (HintMode)retValue;
            }

            set
            {
            }
        }

        //GL_POLYGON_STIPPLE The params parameter returns a single Boolean value indicating whether stippling of polygons is enabled. See glPolygonStipple.  
        public bool PolygonStipple
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.PolygonStipple);
                return retValue;
            }

            set
            {
            }
        }

        //GL_PROJECTION_MATRIX The params parameter returns 16 values: the projection matrix on the top of the projection matrix stack. See glPushMatrix.  

        //GL_PROJECTION_STACK_DEPTH  The params parameter returns one value: the number of matrices on the projection matrix stack. See glPushMatrix.  
        public int ProjectionStackDepth
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.ProjectionStackDepth);
                return retValue;
            }

            set
            {
            }
        }

        //GL_READ_BUFFER  The params parameter returns one value: a symbolic constant indicating which color buffer is selected for reading. See glReadPixels and glAccum.  
        public ReadBufferMode ReadBuffer
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.ReadBuffer);
                return (ReadBufferMode)retValue;
            }

            set
            {
            }
        }

        //GL_RED_BIAS  The params parameter returns one value: the red bias factor used during pixel transfers. See glPixelTransfer.  
        public float RedBias
        {
            get
            {
                float retValue = GI.GetFloat(GetTarget.RedBias);
                return retValue;
            }

            set
            {
            }
        }

        //GL_RED_BITS  The params parameter returns one value: the number of red bitplanes in each color buffer.  
        public int RedBits
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.RedBits);
                return retValue;
            }

            set
            {
            }
        }

        //GL_RED_SCALE  The params parameter returns one value: the red scale factor used during pixel transfers. See glPixelTransfer.  
        public float RedScale
        {
            get
            {
                float retValue = GI.GetFloat(GetTarget.RedScale);
                return retValue;
            }

            set
            {
            }
        }

        //GL_RENDER_MODE  The params parameter returns one value: a symbolic constant indicating whether OpenGL is in render, select, or feedback mode. See glRenderMode.  
        public RenderingMode RenderMode
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.RenderMode);
                return (RenderingMode)retValue;
            }

            set
            {
            }
        }

        //GL_RGBA_MODE  The params parameter returns a single Boolean value indicating whether OpenGL is in RGBA mode (TRUE) or color-index mode (FALSE). See glColor.  
        public bool RGBAMode
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.RgbaMode);
                return retValue;
            }

            set
            {
            }
        }

        //GL_SCISSOR_BOX  The params parameter returns four values: the x and y window coordinates of the scissor box, followed by its width and height. See glScissor.  
        public RECT ScissorBox
        {
            get
            {
                int[] parameters = new int[4];
                GI.GetInteger(GetTarget.ScissorBox, parameters);
                RECT aRect = new RECT(parameters[0], parameters[1], parameters[2], parameters[3]);
                return aRect;
            }

            set
            {
            }
        }

        //GL_SCISSOR_TEST The params parameter returns a single Boolean value indicating whether scissoring is enabled. See glScissor.  
        public bool ScissorTest
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.ScissorTest);
                return retValue;
            }

            set
            {
            }
        }

        //GL_SHADE_MODEL The params parameter returns one value: a symbolic constant indicating whether the shading mode is flat or smooth. See glShadeModel.  
        public ShadingModel ShadeModel
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.ShadeModel);
                return (ShadingModel)retValue;
            }

            set
            {
            }
        }

        //GL_STENCIL_BITS The params parameter returns one value: the number of bitplanes in the stencil buffer.  
        public int StencilBits
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.StencilBits);
                return retValue;
            }

            set
            {
            }
        }

        //GL_STENCIL_CLEAR_VALUE The params parameter returns one value: the index to which the stencil bitplanes are cleared. See glClearStencil.  
        public int StencilClearValue
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.StencilClearValue);
                return retValue;
            }

            set
            {
            }
        }

        //GL_STENCIL_FAIL The params parameter returns one value: a symbolic constant indicating what action is taken when the stencil test fails. See glStencilOp.  
        public StencilOp StencilFail
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.StencilFail);
                return (StencilOp)retValue;
            }

            set
            {
            }
        }

        //GL_STENCIL_FUNC The params parameter returns one value: a symbolic constant indicating what function is used to compare the stencil reference value with the stencil buffer value. See glStencilFunc.  
        public StencilFunction StencilFunc
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.StencilFunc);
                return (StencilFunction)retValue;
            }

            set
            {
            }
        }

        //GL_STENCIL_PASS_DEPTH_FAIL The params parameter returns one value: a symbolic constant indicating what action is taken when the stencil test passes, but the depth test fails. See glStencilOp.  
        public StencilOp StencilPassDepthFail
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.StencilPassDepthFail);
                return (StencilOp)retValue;
            }

            set
            {
            }
        }

        //GL_STENCIL_PASS_DEPTH_PASS The params parameter returns one value: a symbolic constant indicating what action is taken when the stencil test passes and the depth test passes. See glStencilOp.  
        public StencilOp StencilPassDepthPass
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.StencilPassDepthPass);
                return (StencilOp)retValue;
            }

            set
            {
            }
        }

        //GL_STENCIL_REF The params parameter returns one value: the reference value that is compared with the contents of the stencil buffer. See glStencilFunc.  
        public int StencilRef
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.StencilRef);
                return retValue;
            }

            set
            {
            }
        }

        //GL_STENCIL_TEST The params parameter returns a single Boolean value indicating whether stencil testing of fragments is enabled. See glStencilFunc and glStencilOp.  
        public bool StencilTest
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.StencilTest);
                return retValue;
            }

            set
            {
            }
        }

        //GL_STENCIL_VALUE_MASK The params parameter returns one value: the mask that is used to mask both the stencil reference value and the stencil buffer value before they are compared. See glStencilFunc.  
        public uint StencilValueMask
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.StencilValueMask);
                return (uint)retValue;
            }

            set
            {
            }
        }

        //GL_STENCIL_WRITEMASK The params parameter returns one value: the mask that controls writing of the stencil bitplanes. See glStencilMask. 
        public uint StencilWritemask
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.StencilWritemask);
                return (uint)retValue;
            }

            set
            {
            }
        }

        //GL_STEREO  The params parameter returns a single Boolean value indicating whether stereo buffers (left and right) are supported.  
        public bool StereoIsSupported
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.Stereo);
                return retValue;
            }

            set
            {
            }
        }

        //GL_SUBPIXEL_BITS The params parameter returns one value: an estimate of the number of bits of subpixel resolution that are used to position rasterized geometry in window coordinates.  
        public int SubpixelBits
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.SubpixelBits);
                return retValue;
            }

            set
            {
            }
        }

        //GL_TEXTURE_1D The params parameter returns a single Boolean value indicating whether 1-D texture mapping is enabled. See glTexImage1D.  
        public bool Texture1D
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.Texture1d);
                return retValue;
            }

            set
            {
            }
        }

        //GL_TEXTURE_2D The params parameter returns a single Boolean value indicating whether 2-D texture mapping is enabled. See glTexImage2D.  
        public bool Texture2D
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.Texture2d);
                return retValue;
            }

            set
            {
            }
        }

        //GL_TEXTURE_COORD_ARRAY The params parameter returns a single Boolean value indicating whether the texture coordinate array is enabled. See glTexCoordPointer. 
        public bool TextureCoordArray
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.TextureCoordArray);
                return retValue;
            }

            set
            {
            }
        }

        //GL_TEXTURE_COORD_ARRAY_SIZE The params parameter returns one value, the number of coordinates per element in the texture coordinate array. See glTexCoordPointer. 
        public int TextureCoordArraySize
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.TextureCoordArraySize);
                return retValue;
            }

            set
            {
            }
        }

        //GL_TEXTURE_COORD_ARRAY_STRIDE The params parameter returns one value, the byte offset between consecutive elements in the texture coordinate array. See glTexCoordPointer. 
        public int TextureCoordArrayStride
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.TextureCoordArrayStride);
                return retValue;
            }

            set
            {
            }
        }

        //GL_TEXTURE_COORD_ARRAY_TYPE The params parameter params returns one value, the data type of the coordinates in the texture coordinate array. See glTexCoordPointer. 
        public TexCoordPointerType TextureCoordArrayType
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.TextureCoordArrayType);
                return (TexCoordPointerType)retValue;
            }

            set
            {
            }
        }

        //GL_TEXTURE_ENV_COLOR The params parameter returns four values: the red, green, blue, and alpha values of the texture environment color. Integer values, if requested, are linearly mapped from the internal floating-point representation such that 1.0 returns the most positive representable integer value, and 1.0 returns the most negative representable integer value. See glTexEnv.  
        public ColorRGBA TextureEnvColor
        {
            get
            {
                float[] parameters = new float[4];
                gl.glGetFloatv(gl.GL_TEXTURE_ENV_COLOR, parameters);
                ColorRGBA newColor = new ColorRGBA(parameters);
                return newColor;
            }

            set
            {
            }
        }

        //GL_TEXTURE_ENV_MODE The params parameter returns one value: a symbolic constant indicating which texture environment function is currently selected. See glTexEnv.  
        public TextureEnvModeParam TextureEnvMode
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.TextureEnvMode);
                return (TextureEnvModeParam)retValue;
            }

            set
            {
            }
        }

        //GL_TEXTURE_GEN_Q The params parameter returns a single Boolean value indicating whether automatic generation of the Q texture coordinate is enabled. See glTexGen.  
        public bool TextureGenQ
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.TextureGenQ);
                return retValue;
            }

            set
            {
            }
        }

        //GL_TEXTURE_GEN_R The params parameter returns a single Boolean value indicating whether automatic generation of the R texture coordinate is enabled. See glTexGen.  
        public bool TextureGenR
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.TextureGenR);
                return retValue;
            }

            set
            {
            }
        }

        //GL_TEXTURE_GEN_S The params parameter returns a single Boolean value indicating whether automatic generation of the S texture coordinate is enabled. See glTexGen.  
        public bool TextureGenS
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.TextureGenS);
                return retValue;
            }

            set
            {
            }
        }

        //GL_TEXTURE_GEN_T The params parameter returns a single Boolean value indicating whether automatic generation of the T texture coordinate is enabled. See glTexGen.  
        public bool TextureGenT
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.TextureGenT);
                return retValue;
            }

            set
            {
            }
        }

        //GL_TEXTURE_MATRIX The params parameter returns 16 values: the texture matrix on the top of the texture matrix stack. See glPushMatrix.  

        //GL_TEXTURE_STACK_DEPTH The params parameter returns one value: the number of matrices on the texture matrix stack. See glPushMatrix.  
        public int TextureStackDepth
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.TextureStackDepth);
                return retValue;
            }

            set
            {
            }
        }

        //GL_UNPACK_ALIGNMENT The params parameter returns one value: the byte alignment used for reading pixel data from memory. See glPixelStore.  
        public int UnpackAlignment
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.UnpackAlignment);
                return retValue;
            }

            set
            {
            }
        }

        //GL_UNPACK_LSB_FIRST The params parameter returns a single Boolean value indicating whether single-bit pixels being read from memory are read first from the least significant bit of each unsigned byte. See glPixelStore.  
        public bool UnpackLSBFirst
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.UnpackLsbFirst);
                return retValue;
            }

            set
            {
            }
        }

        //GL_UNPACK_ROW_LENGTH The params parameter returns one value: the row length used for reading pixel data from memory. See glPixelStore.  
        public int UnpackRowLength
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.UnpackRowLength);
                return retValue;
            }

            set
            {
            }
        }

        //GL_UNPACK_SKIP_PIXELS The params parameter returns one value: the number of pixel locations skipped before the first pixel is read from memory. See glPixelStore.  
        public int UnpackSkipPixels
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.UnpackSkipPixels);
                return retValue;
            }

            set
            {
            }
        }

        //GL_UNPACK_SKIP_ROWS The params parameter returns one value: the number of rows of pixel locations skipped before the first pixel is read from memory. See glPixelStore.  
        public int UnpackSkipRows
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.UnpackSkipRows);
                return retValue;
            }

            set
            {
            }
        }

        //GL_UNPACK_SWAP_BYTES The params parameter returns a single Boolean value indicating whether the bytes of 2-byte and 4-byte pixel indexes and components are swapped after being read from memory. See glPixelStore.  
        public bool UnpackSwapBytes
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.UnpackSwapBytes);
                return retValue;
            }

            set
            {
            }
        }

        //GL_VERTEX_ARRAY The params parameter returns a single Boolean value indicating whether the vertex array is enabled. See glVertexPointer. 
        public bool VertexArray
        {
            get
            {
                bool retValue = GI.GetBoolean(GetTarget.VertexArray);
                return retValue;
            }

            set
            {
            }
        }

        //GL_VERTEX_ARRAY_SIZE The params parameter returns one value, the number of coordinates per vertex in the vertex array. See glVertexPointer. 
        public int VertexArraySize
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.VertexArraySize);
                return retValue;
            }

            set
            {
            }
        }

        //GL_VERTEX_ARRAY_STRIDE The params parameter returns one value, the byte offset between consecutive vertexes in the vertex array. See glVertexPointer. 
        public int VertexArrayStride
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.VertexArrayStride);
                return retValue;
            }

            set
            {
            }
        }

        //GL_VERTEX_ARRAY_TYPE The params parameter returns one value, the data type of each coordinate in the vertex array. See glVertexPointer. 
        public VertexPointerType VertexArrayType
        {
            get
            {
                int retValue = GI.GetInteger(GetTarget.VertexArrayType);
                return (VertexPointerType)retValue;
            }

            set
            {
            }
        }

        //GL_VIEWPORT The params parameter returns four values: the x and y window coordinates of the viewport, followed by its width and height. See glViewport.  
        public RECT ViewPort
        {
            get
            {
                int[] parameters = new int[4];
                GI.GetInteger(GetTarget.Viewport, parameters);
                RECT aRect = new RECT(parameters[0], parameters[1], parameters[2], parameters[3]);
                return aRect;
            }

            set
            {
            }
        }

        //GL_ZOOM_X The params parameter returns one value: the x pixel zoom factor. See glPixelZoom.  
        public float ZoomX
        {
            get
            {
                float retValue = GI.GetFloat(GetTarget.ZoomX);
                return retValue;
            }

            set
            {
            }
        }

        //GL_ZOOM_Y The params parameter returns one value: the y pixel zoom factor. See glPixelZoom.      
        public float ZoomY
        {
            get
            {
                float retValue = GI.GetFloat(GetTarget.ZoomY);
                return retValue;
            }

            set
            {
            }
        }
    }
}
