

namespace NewTOAPIA.GL
{
    using System;
    using System.Runtime.InteropServices;
    
    using TOAPI.OpenGL;
    
    using NewTOAPIA.Drawing;
    using NewTOAPIA.Graphics;

    public partial class GraphicsInterface
    {
        public static bool gCheckErrors;

        #region Fields
        private GLContext fGI;
        private GLExtensions fExtensions;
        private GLState fState;
        private Glu fGlu;

        private GLAspectList fAspectList;
        private GLFrameBuffer fBufferList;
        private GLClientFeatureList fClientFeatureList;
        private GLPrimitiveDrawing fDrawingList;
        private GLFeatureList fServerFeatureList;

        private GLNameStack fNameStack;
        #endregion

        #region Constructors
        static GraphicsInterface()
        {
            gCheckErrors = false;
        }

        public GraphicsInterface(GLContext GI)
        {
            fGlu = new Glu(this);
            fGI = GI;
            fExtensions = new GLExtensions(this);

            LoadExtensions();

            fState = new GLState(this);

            fServerFeatureList = new GLFeatureList(this);
            fClientFeatureList = new GLClientFeatureList(this);
            fBufferList = new GLFrameBuffer(this);
            fDrawingList = new GLPrimitiveDrawing(this);
        }
        #endregion

        #region Properties

        public GLFrameBuffer Buffers
        {
            get { return fBufferList; }
        }

        public GLClientFeatureList ClientFeatures
        {
            get { return fClientFeatureList; }
        }

        public GLPrimitiveDrawing Drawing
        {
            get { return fDrawingList; }
        }

        public GLExtensions Extensions
        {
            get { return fExtensions; }
        }

        public GLFeatureList Features
        {
            get { return fServerFeatureList; }
        }

        public Glu Glu
        {
            get { return fGlu; }
        }

        public GLContext GI
        {
            get { return fGI;}
        }

        public GLNameStack NameStack
        {
            get
            {
                if (null == fNameStack)
                {
                    fNameStack = new GLNameStack(this);
                }

                return fNameStack;
            }
        }

        public GLState State
        {
            get { return fState; }
        }
        #endregion

        #region Public Methods
        protected void CheckException()
        {
            if (gCheckErrors)
            {
                GLErrorCode errorCode = GetError();
                if (GLErrorCode.NoError != errorCode)
                    throw new GLException(this, errorCode);

            }
        }

        #region Graphics Calls
        public void Accum(AccumOp op, float value)
        {
            gl.glAccum((int)op, value);
            CheckException();
        }

        public void ArrayElement(int i)
        {
            gl.glArrayElement(i);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }


        public void AlphaFunc(AlphaFunction func, float reference)
        {
            gl.glAlphaFunc((int)func, reference);
            CheckException();
        }

        public void BlendFunc(BlendingFactorSrc sfactor, BlendingFactorDest dfactor)
        {
            gl.glBlendFunc((int)sfactor, (int)dfactor);
            CheckException();
        }


        #region Setting Vertex Color
        #region Color...
        public void Color(ColorRGBA aColor)
        {
            gl.glColor4f(aColor.R,aColor.G, aColor.B, aColor.A);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        //public void Color(System.Drawing.Color aColor)
        //{
        //    gl.glColor4ub(aColor.R, aColor.G, aColor.B, aColor.A);
        //}

        public void Color(float r, float g, float b, float a)
        {
            gl.glColor4f(r, g, b, a);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }
        #endregion

        #region Color3...
        //public void Color3(System.Drawing.Color aColor)
        //{
        //    gl.glColor3ub(aColor.R, aColor.G, aColor.B);
        //    // Don't check for error, because this is called between
        //    // glBegin()/glEnd()
        //    //CheckException();
        //}

        public void Color(Vector3f vec)
        {
            gl.glColor3f(vec.x, vec.y, vec.z);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void Color(float red, float green, float blue)
        {
            gl.glColor3f(red, green, blue);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }


        public void Color3(float[] v)
        {
            gl.glColor3fv(v);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void Color(byte red, byte green, byte blue)
        {
            gl.glColor3ub(red, green, blue);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void Color3(byte[] v)
        {
            gl.glColor3ubv(v);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }
        #endregion

        #region Color4...
        public void Color4(ColorRGBA aColor)
        {
            gl.glColor4f(aColor.R, aColor.G, aColor.B, aColor.A);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }


        public void Color4(float4 vec)
        {
            gl.glColor4f(vec.x, vec.y, vec.z, vec.w);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void Color4(float[] v)
        {
            gl.glColor4fv(v);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void Color4(byte red, byte green, byte blue, byte alpha)
        {
            gl.glColor4ub(red, green, blue, alpha);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void Color4(byte[] v)
        {
            gl.glColor4ubv(v);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }
        #endregion
        #endregion Set Vertex Color

        public void ColorMask(byte red, byte green, byte blue, byte alpha)
        {
            gl.glColorMask(red, green, blue, alpha);
            CheckException();
        }

        public void ColorMaterial(GLFace face, ColorMaterialParameter mode)
        {
            gl.glColorMaterial((int)face, (int)mode);
            CheckException();
        }

        #region Color Pointer
        public void ColorPointer(int size, ColorPointerType type, int stride, IntPtr pointer)
        {
            gl.glColorPointer(size, (int)type, stride, pointer);
            CheckException();
        }

        public void ColorPointer(ColorRGBA[] pointer)
        {
            gl.glColorPointer(4, (int)ColorPointerType.Float, 0, pointer);
            CheckException();
        }

        public void ColorPointer(int size, ColorPointerType type, int stride, byte[] pointer)
        {
            gl.glColorPointer(size, (int)type, stride, pointer);
            CheckException();
        }

        public void ColorPointer(int size, ColorPointerType type, int stride, float[,] pointer)
        {
            gl.glColorPointer(size, (int)type, stride, pointer);
            CheckException();
        }
        #endregion


        public void CullFace(GLFace mode)
        {
            gl.glCullFace((int)mode);
            CheckException();
        }


        public void DeleteTextures(int n, uint[] textures)
        {
            gl.glDeleteTextures(n, textures);
            CheckException();
        }

        public void DepthFunc(DepthFunction func)
        {
            gl.glDepthFunc((int)func);
            CheckException();
        }

        public void DepthMask(Boolean flag)
        {
            gl.glDepthMask(flag);
            CheckException();
        }

        public void DepthRange(double zNear, double zFar)
        {
            gl.glDepthRange(zNear, zFar);
            CheckException();
        }



        #region Drawing
        public void DrawArrays(BeginMode mode, int first, int count)
        {
            gl.glDrawArrays((int)mode, first, count);
            CheckException();
        }

        public void DrawBuffer(DrawBufferMode mode)
        {
            gl.glDrawBuffer((int)mode);
            CheckException();
        }

        #region Draw Elements
        public void DrawElements(BeginMode mode, int count, DrawElementType type, IntPtr indices)
        {
            gl.glDrawElements((int)mode, count, (int)type, indices);
            CheckException();
        }

        public void DrawElements(BeginMode mode, int count, DrawElementType type, byte[] indices)
        {
            gl.glDrawElements((int)mode, count, (int)type, indices);
            CheckException();
        }

        public void DrawElements(BeginMode mode, int count, DrawElementType type, ushort[] indices)
        {
            gl.glDrawElements((int)mode, count, (int)type, indices);
            CheckException();
        }

        public void DrawElements(BeginMode mode, int count, DrawElementType type, uint[] indices)
        {
            gl.glDrawElements((int)mode, count, (int)type, indices);
            CheckException();
        }

        public void DrawElements(BeginMode mode, int count, DrawElementType type, int[] indices)
        {
            gl.glDrawElements((int)mode, count, (int)type, indices);
            CheckException();
        }
        #endregion


        #endregion Drawing

        #region Edge Flag
        public void EdgeFlag(bool flag)
        {
            gl.glEdgeFlag(flag);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void EdgeFlagPointer(int stride, Boolean[] pointer)
        {
            gl.glEdgeFlagPointer(stride, pointer);
        }

        public void EdgeFlagPointer(int stride, byte[] pointer)
        {
            gl.glEdgeFlagPointer(stride, pointer);
        }

        public void EdgeFlagPointer(int stride, IntPtr pointer)
        {
            gl.glEdgeFlagPointer(stride, pointer);
        }

        public void EdgeFlagv(ref Boolean flag)
        {
            gl.glEdgeFlagv(ref flag);
        }
        #endregion

        #region Evaluate Coordinates
        public void EvalCoord1(double u)
        {
            gl.glEvalCoord1d(u);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void EvalCoord1(double[] u)
        {
            gl.glEvalCoord1dv(u);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void EvalCoord1(float u)
        {
            gl.glEvalCoord1f(u);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void EvalCoord1(float[] u)
        {
            gl.glEvalCoord1fv(u);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void EvalCoord2(double u, double v)
        {
            gl.glEvalCoord2d(u, v);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void EvalCoord2(double[] u)
        {
            gl.glEvalCoord2dv(u);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void EvalCoord2(float u, float v)
        {
            gl.glEvalCoord2f(u, v);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void EvalCoord2(float[] u)
        {
            gl.glEvalCoord2fv(u);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }
        #endregion

        #region EvalMesh
        public void EvalMesh1(MeshMode1 mode, int i1, int i2)
        {
            gl.glEvalMesh1((int)mode, i1, i2);
            CheckException();
        }

        public void EvalMesh2(MeshMode2 mode, int i1, int i2, int j1, int j2)
        {
            gl.glEvalMesh2((int)mode, i1, i2, j1, j2);
            CheckException();
        }
        #endregion

        #region EvalPoint
        public void EvalPoint1(int i)
        {
            gl.glEvalPoint1(i);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void EvalPoint2(int i, int j)
        {
            gl.glEvalPoint2(i, j);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }
        #endregion

        public void FeedbackBuffer(int size, FeedBackMode type, float[] buffer)
        {
            gl.glFeedbackBuffer(size, (int)type, buffer);
            CheckException();
        }

        public void Finish()
        {
            gl.glFinish();
            CheckException();
        }

        public void Flush()
        {
            gl.glFlush();
            CheckException();
        }

        #region Fog
        public void Fogf(FogParameter pname, float param)
        {
            gl.glFogf((int)pname, (int)param);
            CheckException();
        }

        public void Fogfv(FogParameter pname, float[] parameters)
        {
            gl.glFogfv((int)pname, parameters);
            CheckException();
        }

        #endregion

        public void FrontFace(FrontFaceDirection mode)
        {
            gl.glFrontFace((int)mode);
            CheckException();
        }

        public void GetClipPlane(ClipPlaneName plane, double[] equation)
        {
            gl.glGetClipPlane((int)plane, equation);
            CheckException();
        }

        public GLErrorCode GetError()
        {
            return (GLErrorCode)gl.glGetError();
        }

        #region Get State information
        public void GetBoolean(GetTarget pname, byte[] parameters)
        {
            gl.glGetBooleanv((int)pname, parameters);
        }

        public void GetBoolean(GetTarget pname, int[] parameters)
        {
            gl.glGetBooleanv((int)pname, parameters);
        }

        public bool GetBoolean(GetTarget pname)
        {
            int[] parameters = new int[1];
            GetBoolean(pname, parameters);

            return (parameters[0]==1);
        }


        public void GetDouble(GetTarget pname, double[] parameters)
        {
            gl.glGetDoublev((int)pname, parameters);
        }

        public double GetDouble(GetTarget pname)
        {
            double[] parameters = new double[1];
            GetDouble(pname, parameters);

            return parameters[0];
        }


        public void GetFloat(GetTarget pname, float[] parameters)
        {
            gl.glGetFloatv((int)pname, parameters);
        }

        public float GetFloat(GetTarget pname)
        {
            float[] parameters = new float[1];
            GetFloat(pname, parameters);

            return parameters[0];
        }

        public void GetInteger(GetTarget pname, int[] parameters)
        {
            gl.glGetIntegerv((int)pname, parameters);
        }

        public int GetInteger(GetTarget pname)
        {
            int[] parameters = new int[1];
            GetInteger(pname, parameters);
        
            return parameters[0];
        }
        #endregion


        public void GetMapdv(MapTarget target, GetMapTarget query, double[] v)
        {
            gl.glGetMapdv((int)target, (int)query, v);
        }

        public void GetMapfv(MapTarget target, GetMapTarget query, float[] v)
        {
            gl.glGetMapfv((int)target, (int)query, v);
        }

        public void GetMapiv(MapTarget target, GetMapTarget query, int[] v)
        {
            gl.glGetMapiv((int)target, (int)query, v);
        }

        #region GetMaterial
        public void GetMaterialfv(GLFace face, GetMaterialParameter pname, float[] parameters)
        {
            gl.glGetMaterialfv((int)face, (int)pname, parameters);
        }

        public void GetMaterialiv(GLFace face, GetMaterialParameter pname, int[] parameters)
        {
            gl.glGetMaterialiv((int)face, (int)pname, parameters);
        }
        #endregion

        #region GetPixelMap
        public void GetPixelMapfv(GetPixelMap map, float[] values)
        {
            gl.glGetPixelMapfv((int)map, values);
        }

        public void GetPixelMapuiv(GetPixelMap map, uint[] values)
        {
            gl.glGetPixelMapuiv((int)map, values);
        }

        public void GetPixelMapusv(GetPixelMap map, ushort[] values)
        {
            gl.glGetPixelMapusv((int)map, values);
        }
        #endregion

        public void GetPointerv(GetPointerTarget pname, ref IntPtr parameters)
        {
            gl.glGetPointerv((int)pname, ref parameters);
        }

        public void GetPolygonStipple(byte[] mask)
        {
            gl.glGetPolygonStipple(mask);
        }


        #region GetTexEnv
        public void GetTexEnvfv(TextureEnvironmentTarget target, TextureEnvironmentParameter pname, float[] parameters)
        {
            gl.glGetTexEnvfv((int)target, (int)pname, parameters);
        }

        public void GetTexEnviv(TextureEnvironmentTarget target, TextureEnvironmentParameter pname, int[] parameters)
        {
            gl.glGetTexEnviv((int)target, (int)pname, parameters);
        }
#endregion

        #region GetTexGen
        public void GetTexGendv(TextureCoordName coord, TextureGenParameter pname, double[] parameters)
        {
            gl.glGetTexGendv((int)coord, (int)pname, parameters);
        }

        public void GetTexGenfv(TextureCoordName coord, TextureGenParameter pname, float[] parameters)
        {
            gl.glGetTexGenfv((int)coord, (int)pname, parameters);
        }

        public void GetTexGeniv(TextureCoordName coord, TextureGenParameter pname, int[] parameters)
        {
            gl.glGetTexGeniv((int)coord, (int)pname, parameters);
        }
        #endregion

        #region GetTexImage
        public void GetTexImage(GetTextureImageTarget target, int level, GetTextureImageFormat format, GetTextureImageType type, byte[] pixels)
        {
            gl.glGetTexImage((int)target, level, (int)format, (int)type, pixels);
        }
        #endregion

        #region GetTexLevelParameter
        public void GetTexLevelParameterfv(GetTextureLevelTarget target, int level, GetTextureLevelParamter pname, float[] parameters)
        {
            gl.glGetTexLevelParameterfv((int)target, level, (int)pname, parameters);
        }

        public void GetTexLevelParameteriv(GetTextureLevelTarget target, int level, GetTextureLevelParamter pname, int[] parameters)
        {
            gl.glGetTexLevelParameteriv((int)target, level, (int)pname, parameters);
        }
        #endregion

        #region GetTexParameter
        public void GetTexParameter(GetTextureParameterTarget target, GetTextureParameter pname, float[] parameters)
        {
            gl.glGetTexParameterfv((int)target, (int)pname, parameters);
            CheckException();
        }

        public void GetTexParameter(GetTextureParameterTarget target, GetTextureParameter pname, int[] parameters)
        {
            gl.glGetTexParameteriv((int)target, (int)pname, parameters);
            CheckException();
        }
        #endregion

        public void Hint(HintTarget target, HintMode mode)
        {
            gl.glHint((int)target, (int)mode);
            CheckException();
        }

        public void IndexMask(uint mask)
        {
            gl.glIndexMask(mask);
            CheckException();
        }

        #region IndexPointer
        //public void IndexPointer(IndexPointerType type, int stride, short[] pointer)
        //{
        //    gl.glIndexPointer((int)type, stride, pointer);
        //    CheckException();
        //}

        //public void IndexPointer(IndexPointerType type, int stride, int[] pointer)
        //{
        //    gl.glIndexPointer((int)type, stride, pointer);
        //    CheckException();
        //}

        //public void IndexPointer(IndexPointerType type, int stride, float[] pointer)
        //{
        //    gl.glIndexPointer((int)type, stride, pointer);
        //    CheckException();
        //}

        //public void IndexPointer(IndexPointerType type, int stride, double[] pointer)
        //{
        //    gl.glIndexPointer((int)type, stride, pointer);
        //    CheckException();
        //}

        public void IndexPointer(IndexPointerType type, int stride, IntPtr pointer)
        {
            gl.glIndexPointer((int)type, stride, pointer);
            CheckException();
        }
        #endregion

        #region Index
        public void Index(float c)
        {
            gl.glIndexf(c);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void Index(float[] c)
        {
            gl.glIndexfv(c);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void Index(int c)
        {
            gl.glIndexi(c);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void Index(int[] c)
        {
            gl.glIndexiv(c);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void Index(short c)
        {
            gl.glIndexs(c);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void Index(short[] c)
        {
            gl.glIndexsv(c);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void Index(byte c)
        {
            gl.glIndexub(c);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void Index(byte[] c)
        {
            gl.glIndexubv(c);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }
        #endregion

        public void InterleavedArrays(InterleavedArrays format, int stride, byte[] pointer)
        {
            gl.glInterleavedArrays((int)format, stride, pointer);
            CheckException();
        }

        public void InterleavedArrays(InterleavedArrays format, int stride, float[] pointer)
        {
            gl.glInterleavedArrays((int)format, stride, pointer);
            CheckException();
        }

        public Boolean IsEnabled(GLOption cap)
        {
            return gl.glIsEnabled((int)cap);
        }

        public Boolean IsTexture(uint texture)
        {
            return gl.glIsTexture(texture);
        }

        public void LineStipple(int factor, ushort pattern)
        {
            gl.glLineStipple(factor, pattern);
            CheckException();
        }

        public void LineWidth(float width)
        {
            gl.glLineWidth(width);
            CheckException();
        }

        public void PixelZoom(float xfactor, float yfactor)
        {
            gl.glPixelZoom(xfactor, yfactor);
            CheckException();
        }

        public void PointSize(float size)
        {
            gl.glPointSize(size);
            CheckException();
        }

        public void LogicOp(LogicOp opcode)
        {
            gl.glLogicOp((int)opcode);
            CheckException();
        }

        #region Map
        #region Map1
        public void Map1(MapTarget target, double u1, double u2, int stride, int order, double[] points)
        {
            gl.glMap1d((int)target, u1, u2, stride, order, points);
        }

        public void Map1(MapTarget target, float u1, float u2, int stride, int order, float[] points)
        {
            gl.glMap1f((int)target, u1, u2, stride, order, points);
        }
        #endregion Map1

        #region Map2
        public void Map2(MapTarget target, double u1, double u2, int ustride, int uorder, double v1, double v2, int vstride, int vorder, double[] points)
        {
            gl.glMap2d((int)target, u1, u2, ustride, uorder, v1, v2, vstride, vorder, points);
        }

        public void Map2(MapTarget target, float u1, float u2, int ustride, int uorder, float v1, float v2, int vstride, int vorder, float[] points)
        {
            gl.glMap2f((int)target, u1, u2, ustride, uorder, v1, v2, vstride, vorder, points);
        }
        #endregion Map2

        #region MapGrid1
        public void MapGrid1(int un, double u1, double u2)
        {
            gl.glMapGrid1d(un, u1, u2);
        }

        public void MapGrid1(int un, float u1, float u2)
        {
            gl.glMapGrid1f(un, u1, u2);
        }
        #endregion MapGrid1

        #region MapGrid2
        public void MapGrid2(int un, double u1, double u2, int vn, double v1, double v2)
        {
            gl.glMapGrid2d(un, u1, u2, vn, v1, v2);
        }

        public void MapGrid2(int un, float u1, float u2, int vn, float v1, float v2)
        {
            gl.glMapGrid2f(un, u1, u2, vn, v1, v2);
        }
#endregion MapGrid2
        #endregion Map

        #region Material
        public void Material(GLFace face, MaterialParameter pname, float param)
        {
            gl.glMaterialf((int)face, (int)pname, param);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void Material(GLFace face, MaterialParameter pname, float3 parameters)
        {
            gl.glMaterialfv((int)face, (int)pname, (float[])parameters);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void Material(GLFace face, MaterialParameter pname, float4 parameters)
        {
            gl.glMaterialfv((int)face, (int)pname, (float[])parameters);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void Material(GLFace face, MaterialParameter pname, float[] parameters)
        {
            gl.glMaterialfv((int)face, (int)pname, parameters);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void Material(GLFace face, MaterialParameter pname, int param)
        {
            gl.glMateriali((int)face, (int)pname, param);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void Material(GLFace face, MaterialParameter pname, int[] parameters)
        {
            gl.glMaterialiv((int)face, (int)pname, parameters);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }
        #endregion

        #region Normal
        public void Normal(float3 vec)
        {
            gl.glNormal3f(vec.x, vec.y, vec.z);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void Normal(float nx, float ny, float nz)
        {
            gl.glNormal3f(nx, ny, nz);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void Normal(float[] v)
        {
            gl.glNormal3fv(v);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }
        #endregion

        #region NormalPointer
        public void NormalPointer(Vector3f[] pointer)
        {
            gl.glNormalPointer((int)NormalPointerType.Float, 0, pointer);
            CheckException();
        }
        
        public void NormalPointer(NormalPointerType type, int stride, IntPtr pointer)
        {
            gl.glNormalPointer((int)type, stride, pointer);
            CheckException();
        }

        //public void NormalPointer(NormalPointerType type, int stride, byte[] pointer)
        //{
        //    gl.glNormalPointer((int)type, stride, pointer);
        //    CheckException();
        //}

        //public void NormalPointer(NormalPointerType type, int stride, short[] pointer)
        //{
        //    gl.glNormalPointer((int)type, stride, pointer);
        //    CheckException();
        //}

        //public void NormalPointer(NormalPointerType type, int stride, int[] pointer)
        //{
        //    gl.glNormalPointer((int)type, stride, pointer);
        //    CheckException();
        //}

        public void NormalPointer(NormalPointerType type, int stride, float[] pointer)
        {
            gl.glNormalPointer((int)type, stride, pointer);
            CheckException();
        }

        //public void NormalPointer(NormalPointerType type, int stride, double[] pointer)
        //{
        //    gl.glNormalPointer((int)type, stride, pointer);
        //    CheckException();
        //}

        #endregion

        public void PassThrough(float token)
        {
            gl.glPassThrough(token);
            CheckException();
        }

        #region PixelMap
        public void PixelMapfv(uint map, int mapsize, float[] values)
        {
            gl.glPixelMapfv(map, mapsize, values);
        }

        public void PixelMapuiv(PixelMap map, int mapsize, uint[] values)
        {
            gl.glPixelMapuiv((int)map, mapsize, values);
        }

        public void PixelMapusv(PixelMap map, int mapsize, ushort[] values)
        {
            gl.glPixelMapusv((int)map, mapsize, values);
        }
        #endregion

        #region PixelStore
        public void PixelStore(PixelStoreCommand aCmd)
        {
            gl.glPixelStorei((int)aCmd.Parameter, aCmd.CommandValue);
            CheckException();
        }

        public void PixelStore(PixelStore pname, float param)
        {
            gl.glPixelStoref((int)pname, param);
            CheckException();
        }

        public void PixelStore(PixelStore pname, int param)
        {
            gl.glPixelStorei((int)pname, param);
            CheckException();
        }
        #endregion

        #region PixelTransfer
        public void PixelTransfer(PixelTransfer pname, float param)
        {
            gl.glPixelTransferf((int)pname, param);
            CheckException();
        }

        public void PixelTransfer(PixelTransfer pname, int param)
        {
            gl.glPixelTransferi((int)pname, param);
            CheckException();
        }
        #endregion PixelTransfer

        #region Polygon
        public void PolygonMode(GLFace face, PolygonMode mode)
        {
            gl.glPolygonMode((int)face, (int)mode);
            CheckException();
        }

        public void PolygonOffset(float factor, float units)
        {
            gl.glPolygonOffset(factor, units);
            CheckException();
        }

        public void PolygonStipple(byte[] mask)
        {
            gl.glPolygonStipple(mask);
            CheckException();
        }
        #endregion

        public void PrioritizeTextures(int n, uint[] textures, float[] priorities)
        {
            gl.glPrioritizeTextures(n, textures, priorities);
            CheckException();
        }

        #region string GetString(StringName name)
        public string GetString(StringName name)
        {
            IntPtr strPtr = gl.glGetString((int)name);
            if (IntPtr.Zero == strPtr)
                return string.Empty;

            string aString = Marshal.PtrToStringAnsi(strPtr);

            return aString;
        }

        public string GetGluString(GluStringName name)
        {
            IntPtr strPtr = gl.glGetString((int)name);
            if (IntPtr.Zero == strPtr)
                return string.Empty;

            string aString = Marshal.PtrToStringAnsi(strPtr);

            return aString;
        }

        #endregion

        #region RasterPos
        #region void RasterPos2...
        public void RasterPos(float2 vec)
        {
            gl.glRasterPos2f(vec.x, vec.y);
        }

        //public void RasterPos(double x, double y)
        //{
        //    gl.glRasterPos2d(x, y);
        //}

        //public void RasterPos2dv(double[] v)
        //{
        //    gl.glRasterPos2dv(v);
        //}

        public void RasterPos2f(float x, float y)
        {
            gl.glRasterPos2f(x, y);
        }

        public void RasterPos2fv(float[] v)
        {
            gl.glRasterPos2fv(v);
        }

        public void RasterPos2i(int x, int y)
        {
            gl.glRasterPos2i(x, y);
        }

        public void RasterPos2iv(int[] v)
        {
            gl.glRasterPos2iv(v);
        }

        //public void RasterPos2s(short x, short y)
        //{
        //    gl.glRasterPos2s(x, y);
        //}

        //public void RasterPos2sv(short[] v)
        //{
        //    gl.glRasterPos2sv(v);
        //}
        #endregion

        #region void RasterPos3...
        public void RasterPos(Vector3f vec)
        {
            gl.glRasterPos3f(vec.x, vec.y, vec.z);
        }

        public void RasterPos3d(double x, double y, double z)
        {
            gl.glRasterPos3d(x, y, z);
        }

        public void RasterPos3dv(double[] v)
        {
            gl.glRasterPos3dv(v);
        }

        public void RasterPos3f(float x, float y, float z)
        {
            gl.glRasterPos3f(x, y, z);
        }

        public void RasterPos3fv(float[] v)
        {
            gl.glRasterPos3fv(v);
        }

        public void RasterPos3i(int x, int y, int z)
        {
            gl.glRasterPos3i(x, y, z);
        }

        public void RasterPos3iv(int[] v)
        {
            gl.glRasterPos3iv(v);
        }

        public void RasterPos3s(short x, short y, short z)
        {
            gl.glRasterPos3s(x, y, z);
        }

        public void RasterPos3sv(short[] v)
        {
            gl.glRasterPos3sv(v);
        }
        #endregion

        #region void RasterPos4...
        public void RasterPos(float4 vec)
        {
            gl.glRasterPos4f(vec.x, vec.y, vec.z, vec.w);
        }

        //public void RasterPos4d(double x, double y, double z, double w)
        //{
        //    gl.glRasterPos4d(x, y, z, w);
        //}

        //public void RasterPos4dv(double[] v)
        //{
        //    gl.glRasterPos4dv(v);
        //}

        public void RasterPos(float x, float y, float z, float w)
        {
            gl.glRasterPos4f(x, y, z, w);
        }

        public void RasterPos4(float[] v)
        {
            gl.glRasterPos4fv(v);
        }

        public void RasterPos(int x, int y, int z, int w)
        {
            gl.glRasterPos4i(x, y, z, w);
        }

        public void RasterPos4(int[] v)
        {
            gl.glRasterPos4iv(v);
        }

        //public void RasterPos4s(short x, short y, short z, short w)
        //{
        //    gl.glRasterPos4s(x, y, z, w);
        //}

        //public void RasterPos4sv(short[] v)
        //{
        //    gl.glRasterPos4sv(v);
        //}
        #endregion
        #endregion

        public void ReadBuffer(ReadBufferMode mode)
        {
            gl.glReadBuffer((int)mode);
        }

        public void SelectBuffer(int size, uint[] buffer)
        {
            gl.glSelectBuffer(size, buffer);
        }

        #region ReadPixels
        public void ReadPixels(int x, int y, GLPixelData pixMap)
        {
            gl.glReadPixels(x, y, pixMap.Width, pixMap.Height, (int)pixMap.PixelFormat, (int)pixMap.PixelType, pixMap.Pixels);
            CheckException();
        }

        #endregion

        #region View Management
        public void ClipPlane(ClipPlaneName plane, double[] equation)
        {
            gl.glClipPlane((int)plane, equation);
        }
        
        public void Frustum(double left, double right, double bottom, double top, double zNear, double zFar)
        {
            gl.glFrustum(left, right, bottom, top, zNear, zFar);
        }

        public void Ortho(double left, double right, double bottom, double top, double zNear, double zFar)
        {
            gl.glOrtho(left, right, bottom, top, zNear, zFar);
        }

        public void Scissor(int x, int y, int width, int height)
        {
            gl.glScissor(x, y, width, height);
        }

        public void SetViewport(Viewport aviewport)
        {
            Viewport(aviewport.x, aviewport.y, aviewport.width, aviewport.height);    
        }

        public void Viewport(int x, int y, int width, int height)
        {
            gl.glViewport(x, y, width, height);
        }
        #endregion View Management

        #region Transform
        /// <summary>
        /// Rotate by the angle amounts specified in the vector.  Each component of the
        /// vector gives an amount to rotate about the respective axis.
        /// </summary>
        /// <param name="angles"></param>
        public void Rotate(Vector3f angles)
        {
            gl.glRotatef(angles.x, 1.0f, 0, 0);
            CheckException();

            gl.glRotatef(angles.y, 0, 1.0f, 0);
            CheckException();
            
            gl.glRotatef(angles.z, 0, 0, 1.0f);
            CheckException();
        }

        public void Rotate(float angle, float x, float y, float z)
        {
            gl.glRotatef(angle, x, y, z);
            CheckException();
        }

        public void Scale(float x, float y, float z)
        {
            gl.glScalef(x, y, z);
            CheckException();
        }

        public void Translate(Vector3D vec)
        {
            Translate(vec.x, vec.y, vec.z);
        }

        public void Translate(Vector3f vec)
        {
            Translate(vec.x, vec.y, vec.z);
        }

        public void Translate(double x, double y, double z)
        {
            gl.glTranslated(x, y, z);
            CheckException();
        }
        #endregion

        #region Matrix Management
        public void LoadIdentity()
        {
            gl.glLoadIdentity();
            CheckException();
        }

        public void LoadMatrixd(double[] m)
        {
            gl.glLoadMatrixd(m);
            CheckException();
        }

        public void LoadMatrixf(float[] m)
        {
            gl.glLoadMatrixf(m);
            CheckException();
        }

        public void MatrixMode(MatrixMode mode)
        {
            gl.glMatrixMode((int)mode);
            CheckException();
        }

        public void MultMatrix(double[] m)
        {
            gl.glMultMatrixd(m);
            CheckException();
        }

        public void MultMatrix(float[] m)
        {
            gl.glMultMatrixf(m);
            CheckException();
        }

        public void PushMatrix()
        {
            gl.glPushMatrix();
            CheckException();
        }

        public void PopMatrix()
        {
            gl.glPopMatrix();
            CheckException();
        }
        #endregion


        public RenderingMode RenderMode(RenderingMode mode)
        {
            int retValue = gl.glRenderMode((int)mode);
            CheckException();

            return (RenderingMode)retValue;
        }

        public void ShadeModel(ShadingModel mode)
        {
            gl.glShadeModel((int)mode);
            CheckException();
        }

        #region Stencil
        public void StencilFunc(StencilFunction func, int reference, uint mask)
        {
            gl.glStencilFunc((int)func, reference, mask);
        }

        public void StencilMask(uint mask)
        {
            gl.glStencilMask(mask);
        }

        public void StencilOp(StencilOp fail, StencilOp zfail, StencilOp zpass)
        {
            gl.glStencilOp((int)fail, (int)zfail, (int)zpass);
        }
        #endregion

        public void BindTexture(TextureBindTarget target, uint texture)
        {
            gl.glBindTexture((int)target, texture);
            CheckException();
        }


        #region TexCoord1
        public void TexCoord(float s)
        {
            gl.glTexCoord1f(s);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void TexCoord1(float[] v)
        {
            gl.glTexCoord1fv(v);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }


        #endregion TexCoord1

        #region TexCoord2
        public void TexCoord(TextureCoordinates coords)
        {
            gl.glTexCoord2f(coords.s, coords.t);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void TexCoord(float s, float t)
        {
            gl.glTexCoord2f(s, t);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void TexCoord2(float[] v)
        {
            gl.glTexCoord2fv(v);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }
        #endregion

        #region TexCoord3
        public void TexCoord3(double s, double t, double r)
        {
            gl.glTexCoord3d(s, t, r);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void TexCoord3(double[] v)
        {
            gl.glTexCoord3dv(v);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void TexCoord3(float s, float t, float r)
        {
            gl.glTexCoord3f(s, t, r);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void TexCoord3(float[] v)
        {
            gl.glTexCoord3fv(v);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void TexCoord3(int s, int t, int r)
        {
            gl.glTexCoord3i(s, t, r);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void TexCoord3(int[] v)
        {
            gl.glTexCoord3iv(v);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void TexCoord3(short s, short t, short r)
        {
            gl.glTexCoord3s(s, t, r);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void TexCoord3(short[] v)
        {
            gl.glTexCoord3sv(v);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }
        #endregion TexCoord3

        #region TexCoord4
        public void TexCoord4(double s, double t, double r, double q)
        {
            gl.glTexCoord4d(s, t, r, q);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void TexCoord4(double[] v)
        {
            gl.glTexCoord4dv(v);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void TexCoord4(float s, float t, float r, float q)
        {
            gl.glTexCoord4f(s, t, r, q);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void TexCoord4(float[] v)
        {
            gl.glTexCoord4fv(v);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void TexCoord4(int s, int t, int r, int q)
        {
            gl.glTexCoord4i(s, t, r, q);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void TexCoord4(int[] v)
        {
            gl.glTexCoord4iv(v);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void TexCoord4(short s, short t, short r, short q)
        {
            gl.glTexCoord4s(s, t, r, q);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void TexCoord4(short[] v)
        {
            gl.glTexCoord4sv(v);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }
        #endregion

        #region TexCoordPointer
        public void TexCoordPointer(TextureCoordinates[] pointer)
        {
            gl.glTexCoordPointer(2, (int)TexCoordPointerType.Float, 0, pointer);
        }

        public void TexCoordPointer(int size, TexCoordPointerType type, int stride, IntPtr pointer)
        {
            gl.glTexCoordPointer(size, (int)type, stride, pointer);
        }

        //public void TexCoordPointer(int size, TexCoordPointerType type, int stride, short[] pointer)
        //{
        //    gl.glTexCoordPointer(size, type, stride, pointer);
        //}

        //public void TexCoordPointer(int size, TexCoordPointerType type, int stride, int[] pointer)
        //{
        //    gl.glTexCoordPointer(size, type, stride, pointer);
        //}

        //public void TexCoordPointer(int size, TexCoordPointerType type, int stride, float[] pointer)
        //{
        //    gl.glTexCoordPointer(size, type, stride, pointer);
        //}

        //public void TexCoordPointer(int size, TexCoordPointerType type, int stride, double[] pointer)
        //{
        //    gl.glTexCoordPointer(size, type, stride, pointer);
        //}

        #endregion TexCoordPointer

        #region TexEnv
        public void TexEnv(TextureEnvironmentTarget target, TextureEnvironmentParameter pname, float param)
        {
            gl.glTexEnvf((int)target, (int)pname, param);
        }

        public void TexEnv(TextureEnvironmentTarget target, TextureEnvironmentParameter pname, int param)
        {
            gl.glTexEnvi((int)target, (int)pname, param);
        }

        public void TexEnv(TextureEnvironmentTarget target, TextureEnvironmentParameter pname, float[] parameters)
        {
            gl.glTexEnvfv((int)target, (int)pname, parameters);
        }

        public void TexEnv(TextureEnvironmentTarget target, TextureEnvironmentParameter pname, int[] parameters)
        {
            gl.glTexEnviv((int)target, (int)pname, parameters);
        }

        // Specific to TextureEnvironmentTarget.TextureEnv
        public void TexEnv(TextureEnvTargetParameter pname, int param)
        {
            gl.glTexEnvi((int)TextureEnvironmentTarget.TextureEnv, (int)pname, param);
        }

        // Specific to TextureEnvironmentTarget.TextureEnv, TextureEnvTargetParameter.TextureEnvMode
        public void TexEnv(TextureEnvModeParam param)
        {
            gl.glTexEnvi((int)TextureEnvironmentTarget.TextureEnv, (int)TextureEnvTargetParameter.TextureEnvMode, (int)param);
        }

        // Specific to TextureEnvironmentTarget.TextureFilter, 
        public void TexEnv(TextureFilterTargetParameter pname, int param)
        {
            gl.glTexEnvi((int)TextureEnvironmentTarget.TextureFilter, (int)pname, param);
        }

        #endregion TexEnv

        #region TexGen
        public void TexGen(TextureCoordName coord, TextureGenParameter pname, TextureGenMode param)
        {
            gl.glTexGeni((int)coord, (uint)pname, (int)param);
            CheckException();
        }

        public void TexGen(TextureCoordName coord, TextureGenParameter pname, int param)
        {
            gl.glTexGeni((int)coord, (uint)pname, param);
            CheckException();
        }

        public void TexGen(TextureCoordName coord, uint pname, int[] parameters)
        {
            gl.glTexGeniv((int)coord, pname, parameters);
            CheckException();
        }

        public void TexGen(TextureCoordName coord, TextureGenParameter pname, float param)
        {
            gl.glTexGenf((int)coord, (int)pname, param);
            CheckException();
        }

        public void TexGen(TextureCoordName coord, TextureGenParameter pname, float[] parameters)
        {
            gl.glTexGenfv((int)coord, (uint)pname, parameters);
            CheckException();
        }


        #endregion TexGen

        #region TexImage1D
        public void TexImage1D(Texture1DTarget target, int level, TextureInternalFormat internalformat, int width, int border, TexturePixelFormat format, PixelComponentType type, IntPtr pixels)
        {
            gl.glTexImage1D((int)target, level, (int)internalformat, width, border, (int)format, (int)type, pixels);
        }
        public void TexImage1D(Texture1DTarget target, int level, TextureInternalFormat internalformat, int width, int border, TexturePixelFormat format, PixelComponentType type, object pixels)
        {
            gl.glTexImage1D((int)target, level, (int)internalformat, width, border, (int)format, (int)type, pixels);
        }
        #endregion

        #region TexImage2D
        public void TexImage2D(int level, TextureInternalFormat internalformat, int width, int height, int border, TexturePixelFormat format, PixelComponentType type, IntPtr pixels)
        {
            gl.glTexImage2D((int)Texture2DTarget.Texture2d, level, (int)internalformat, width, height, border, (int)format, (int)type, pixels);
            CheckException();
        }

        public void TexImage2D(Texture2DTarget target, int level, TextureInternalFormat internalformat, int width, int height, int border, TexturePixelFormat format, PixelComponentType type, IntPtr pixels)
        {
            gl.glTexImage2D((int)target, level, (int)internalformat, width, height, border, (int)format, (int)type, pixels);
            CheckException();
        }

        public void TexImage2D(int level, TextureInternalFormat internalformat, int width, int height, int border, TexturePixelFormat format, PixelComponentType type, object pixels)
        {
            gl.glTexImage2D((int)Texture2DTarget.Texture2d, level, (int)internalformat, width, height, border, (int)format, (int)type, pixels);
            CheckException();
        }

        public void TexImage2D(Texture2DTarget target, int level, TextureInternalFormat internalformat, int width, int height, int border, TexturePixelFormat format, PixelComponentType type, object pixels)
        {
            gl.glTexImage2D((int)target, level, (int)internalformat, width, height, border, (int)format, (int)type, pixels);
            CheckException();
        }

        #endregion TexImage2D

        #region TexParameter
        public void SetTexture2DParameters(GLTextureParameters parameters)
        {
            // Set Wrap Mode
            TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureWrapS, parameters.WrapModeS);
            TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureWrapT, parameters.WrapModeT);

            // Set Filters
            TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureMagFilter, parameters.MagFilter);
            TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureMagFilter, parameters.MinFilter);
        }

        public void TexParameter(TextureParameterTarget target, TextureParameterName pname, float param)
        {
            gl.glTexParameterf((int)target, (int)pname, param);
            CheckException();
        }

        public void TexParameterfv(TextureArrayParameterTarget target, TextureParameterName pname, float[] parameters)
        {
            gl.glTexParameterfv((int)target, (int)pname, parameters);
            CheckException();
        }

        public void TexParameter(TextureParameterTarget target, TextureParameterName pname, int param)
        {
            gl.glTexParameteri((int)target, (int)pname, param);
            CheckException();
        }

        public void TexParameteriv(TextureArrayParameterTarget target, TextureParameterName pname, int[] parameters)
        {
            gl.glTexParameteriv((int)target, (int)pname, parameters);
            CheckException();
        }

        public void TexParameter(TextureParameterTarget target, TextureParameterName pname, TextureWrapMode param)
        {
            gl.glTexParameteri((int)target, (int)pname, (int)param);
            CheckException();
        }

        public void TexParameter(TextureParameterTarget target, TextureParameterName pname, TextureMagFilter param)
        {
            gl.glTexParameteri((int)target, (int)pname, (int)param);
            CheckException();
        }

        public void TexParameter(TextureParameterTarget target, TextureParameterName pname, TextureMinFilter param)
        {
            gl.glTexParameteri((int)target, (int)pname, (int)param);
            CheckException();
        }

        public void TexParameter(TextureParameterTarget target, TextureParameterName pname, TextureShadow shadowParam)
        {
            gl.glTexParameteri((int)target, (int)pname, (int)shadowParam);
            CheckException();
        }

        #endregion TexParameter

        #region TexSubImage
        public void TexSubImage1D(int level, int xoffset, int width, TexturePixelFormat format, PixelComponentType type, object[] pixels)
        {
            gl.glTexSubImage1D((int)Texture1DTarget.Texture1d, level, xoffset, width, (int)format, (int)type, pixels);
        }

        public void TexSubImage2D(Texture2DTarget target, int level, int xoffset, int yoffset, int width, int height, TexturePixelFormat format, PixelComponentType type, IntPtr pixels)
        {
            if (null == pixels || pixels == IntPtr.Zero)
                return;

            gl.glTexSubImage2D((int)target, level, xoffset, yoffset, width, height, (int)format, (int)type, pixels);
        }

        public void TexSubImage2D(Texture2DTarget target, int level, int xoffset, int yoffset, int width, int height, TexturePixelFormat format, PixelComponentType type, byte[] pixels)
        {
            gl.glTexSubImage2D((int)target, level, xoffset, yoffset, width, height, (int)format, (int)type, pixels);
        }
        #endregion

        #region Vertex2
        public void Vertex(float2 aVector)
        {
            gl.glVertex2f(aVector.x, aVector.y);
           
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }


        public void Vertex(float x, float y)
        {
            gl.glVertex2f(x, y);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void Vertex2(float[] v)
        {
            gl.glVertex2fv(v);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        #endregion

        #region Vertex3
        public void Vertex(float3 vec)
        {
            gl.glVertex3f(vec.x, vec.y, vec.z);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void Vertex(float x, float y, float z)
        {
            gl.glVertex3f(x, y, z);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void Vertex3(float[] v)
        {
            gl.glVertex3fv(v);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }
        #endregion Vertex3

        #region Vertex4
        public void Vertex(float4 vec)
        {
            gl.glVertex4f(vec.x, vec.y, vec.z, vec.w);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void Vertex(float x, float y, float z, float w)
        {
            gl.glVertex4f(x, y, z, w);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        public void Vertex4(float[] v)
        {
            gl.glVertex4fv(v);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }
        #endregion

        #region VertexPointer
        public void VertexPointer(Point3D[] pointer)
        {
            gl.glVertexPointer(3, (int)VertexPointerType.Float, 1, pointer);
            CheckException();
        }

        public void VertexPointer(Vector3f[] pointer)
        {
            gl.glVertexPointer(3, (int)VertexPointerType.Float, 0, pointer);
            CheckException();
        }

        public void VertexPointer(float3[] pointer)
        {
            gl.glVertexPointer(3, (int)VertexPointerType.Float, 0, pointer);
            CheckException();
        }

        public void VertexPointer(int size, VertexPointerType type, int stride, IntPtr pointer)
        {
            gl.glVertexPointer(size, (int)type, stride, pointer);
            CheckException();
        }

        public void VertexPointer(int size, VertexPointerType type, int stride, object pointer)
        {
            gl.glVertexPointer(size, (int)type, stride, pointer);
            CheckException();
        }
        

        public void VertexPointer(int size, int stride, short[] pointer)
        {
            gl.glVertexPointer(size, (int)VertexPointerType.Short, stride, pointer);
            CheckException();
        }

        public void VertexPointer(int size, int stride, int[] pointer)
        {
            gl.glVertexPointer(size, (int)VertexPointerType.Int, stride, pointer);
            CheckException();
        }

        public void VertexPointer(int size, int stride, float[] pointer)
        {
            gl.glVertexPointer(size, (int)VertexPointerType.Float, stride, pointer);
            CheckException();
        }

        public void VertexPointer(int size, int stride, double[,] pointer)
        {
            gl.glVertexPointer(size, (int)VertexPointerType.Double, stride, pointer);
            CheckException();
        }

        #endregion
        #endregion Graphics Calls
        #endregion Public Methods

    }
}
