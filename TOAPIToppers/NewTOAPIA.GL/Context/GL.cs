using System;
using System.Runtime.InteropServices;

using TOAPI.OpenGL;

namespace NewTOAPIA.GL
{
    using NewTOAPIA.Drawing;
    using NewTOAPIA.Graphics;

    public partial class GL
    {
        public static void Accum(AccumOp op, float value)
        {
            gl.glAccum((int)op, value);
        }


        public static bool AreTexturesResident(int n, uint[] textures, byte[] residences)
        {
            return gl.glAreTexturesResident(n, textures, residences);
        }

        public static bool AreTexturesResident(int n, uint[] textures, bool[] residences)
        {
            return gl.glAreTexturesResident(n, textures, residences);
        }

        public static void ArrayElement(int i)
        {
            gl.glArrayElement(i);
        }

        #region Begin and End Modes
        public static void Begin(BeginMode aMode)
        {
            gl.glBegin((int)aMode);
        }

        public static void BeginPoints()
        {
            Begin(BeginMode.Points);
        }

        public static void BeginLines()
        {
            Begin(BeginMode.Lines);
        }
        public static void BeginLineLoop()
        {
            Begin(BeginMode.LineLoop);
        }
        public static void BeginLineStrip()
        {
            Begin(BeginMode.LineStrip);
        }
        public static void BeginTriangles()
        {
            Begin(BeginMode.Triangles);
        }
        public static void BeginTriangleStrip()
        {
            Begin(BeginMode.TriangleStrip);
        }
        public static void BeginTriangleFan()
        {
            Begin(BeginMode.TriangleFan);
        }
        public static void BeginQuads()
        {
            Begin(BeginMode.Quads);
        }
        public static void BeginQuadStrip()
        {
            Begin(BeginMode.QuadStrip);
        }
        public static void BeginPolygon()
        {
            Begin(BeginMode.Polygon);
        }


        public static void End()
        {
            gl.glEnd();
        }
        #endregion

        public static void BindTexture(TextureBindTarget target, uint texture)
        {
            gl.glBindTexture((int)target, texture);
        }

        public static void Bitmap(int width, int height, float xorig, float yorig, float xmove, float ymove, byte[] bitmap)
        {
            gl.glBitmap(width, height, xorig, yorig, xmove, ymove, bitmap);
        }

        public static void AlphaFunc(AlphaFunction func, float reference)
        {
            gl.glAlphaFunc((int)func, reference);
        }

        public static void BlendFunc(BlendingFactorSrc sfactor, BlendingFactorDest dfactor)
        {
            gl.glBlendFunc((int)sfactor, (int)dfactor);
        }

        #region DisplayList
        public static void CallList(uint list)
        {
            gl.glCallList(list);
        }

        public static void CallLists(int n, ListNameType type, byte[] lists)
        {
            gl.glCallLists(n, (int)type, lists);
        }

        public static void CallLists(int n, ListNameType type, sbyte[] lists)
        {
            gl.glCallLists(n, (int)type, lists);
        }

        public static void CallLists(int n, ListNameType type, short[] lists)
        {
            gl.glCallLists(n, (int)type, lists);
        }

        public static void CallLists(int n, ListNameType type, ushort[] lists)
        {
            gl.glCallLists(n, (int)type, lists);
        }

        public static void CallLists(int n, ListNameType type, int[] lists)
        {
            gl.glCallLists(n, (int)type, lists);
        }

        public static void CallLists(int n, ListNameType type, uint[] lists)
        {
            gl.glCallLists(n, (int)type, lists);
        }

        public static void CallLists(int n, ListNameType type, float[] lists)
        {
            gl.glCallLists(n, (int)type, lists);
        }

        public static void EndList()
        {
            gl.glEndList();
        }

        public static void ListBase(uint abase)
        {
            gl.glListBase(abase);
        }

        public static void NewList(uint list, ListMode mode)
        {
            gl.glNewList(list, (int)mode);
        }

        public static uint GenLists(int range)
        {
            return gl.glGenLists(range);
        }

        public static void DeleteLists(uint list, int range)
        {
            gl.glDeleteLists(list, range);
        }

        public static Boolean IsList(uint list)
        {
            return gl.glIsList(list);
        }

        #endregion Call Lists

        #region Clearing Buffers
        public static void Clear(ClearBufferMask flags)
        {
            gl.glClear((int)flags);
        }


        public static void ClearAccum(ColorRGBA aColor)
        {
            gl.glClearAccum(aColor.R, aColor.G, aColor.B, aColor.A);
        }

        public static void ClearAccum(float red, float green, float blue, float alpha)
        {
            gl.glClearAccum(red, green, blue, alpha);
        }

        public static void ClearColor(ColorRGBA aColor)
        {
            ClearColor(aColor.R, aColor.G, aColor.B, aColor.A);
        }

        public static void ClearColor(float red, float green, float blue, float alpha)
        {
            gl.glClearColor(red, green, blue, alpha);
        }


        public static void ClearDepth(double depth)
        {
            gl.glClearDepth(depth);
        }

        public static void ClearIndex(float c)
        {
            gl.glClearIndex(c);
        }

        public static void ClearStencil(int s)
        {
            gl.glClearStencil(s);
        }
        #endregion


        #region Setting Vertex Color

        public static void Color(ColorRGBA aColor)
        {
            gl.glColor4f(aColor.R, aColor.G, aColor.B, aColor.A);
        }

        //public static void Color3(Color aColor)
        //{
        //    gl.glColor3ub(aColor.R, aColor.G, aColor.B);
        //}

        public static void Color3(sbyte red, sbyte green, sbyte blue)
        {
            gl.glColor3b(red, green, blue);
        }

        public static void Color3(sbyte[] v)
        {
            gl.glColor3bv(v);
        }

        public static void Color3(double red, double green, double blue)
        {
            gl.glColor3d(red, green, blue);
        }

        public static void Color3(double[] v)
        {
            gl.glColor3dv(v);
        }


        public static void Color3(float red, float green, float blue)
        {
            gl.glColor3f(red, green, blue);
        }

        public static void Color3(Vector3f vec)
        {
            gl.glColor3f(vec.x, vec.y, vec.z);
        }

        public static void Color3(float[] v)
        {
            gl.glColor3fv(v);
        }

        public static void Color3(int red, int green, int blue)
        {
            gl.glColor3i(red, green, blue);
        }

        public static void Color3(int[] v)
        {
            gl.glColor3iv(v);
        }

        public static void Color3(short red, short green, short blue)
        {
            gl.glColor3s(red, green, blue);
        }

        public static void Color3(short[] v)
        {
            gl.glColor3sv(v);
        }

        public static void Color3(byte red, byte green, byte blue)
        {
            gl.glColor3ub(red, green, blue);
        }

        public static void Color3(byte[] v)
        {
            gl.glColor3ubv(v);
        }

        public static void Color3(uint red, uint green, uint blue)
        {
            gl.glColor3ui(red, green, blue);
        }

        public static void Color3(uint[] v)
        {
            gl.glColor3uiv(v);
        }

        public static void Color3(ushort red, ushort green, ushort blue)
        {
            gl.glColor3us(red, green, blue);
        }

        public static void Color3(ushort[] v)
        {
            gl.glColor3usv(v);
        }

        public static void Color4(ColorRGBA aColor)
        {
            gl.glColor4f(aColor.R, aColor.G, aColor.B, aColor.A);
        }

        //public static void Color4(Color aColor)
        //{
        //    gl.glColor4ub(aColor.R, aColor.G, aColor.B, aColor.A);
        //}

        public static void Color4(sbyte red, sbyte green, sbyte blue, sbyte alpha)
        {
            gl.glColor4b(red, green, blue, alpha);
        }


        public static void Color4(sbyte[] v)
        {
            gl.glColor4bv(v);
        }

        public static void Color4(double red, double green, double blue, double alpha)
        {
            gl.glColor4d(red, green, blue, alpha);
        }

        public static void Color4(double[] v)
        {
            gl.glColor4dv(v);
        }

        public static void Color4(float red, float green, float blue, float alpha)
        {
            gl.glColor4f(red, green, blue, alpha);
        }

        public static void Color4(float4 vec)
        {
            gl.glColor4f(vec.x, vec.y, vec.z, vec.w);
        }

        public static void Color4(float[] v)
        {
            gl.glColor4fv(v);
        }

        public static void Color4(int red, int green, int blue, int alpha)
        {
            gl.glColor4i(red, green, blue, alpha);
        }

        public static void Color4(int[] v)
        {
            gl.glColor4iv(v);
        }

        public static void Color4(short red, short green, short blue, short alpha)
        {
            gl.glColor4s(red, green, blue, alpha);
        }

        public static void Color4(short[] v)
        {
            gl.glColor4sv(v);
        }

        public static void Color4(byte red, byte green, byte blue, byte alpha)
        {
            gl.glColor4ub(red, green, blue, alpha);
        }

        public static void Color4(byte[] v)
        {
            gl.glColor4ubv(v);
        }

        public static void Color4(uint red, uint green, uint blue, uint alpha)
        {
            gl.glColor4ui(red, green, blue, alpha);
        }

        public static void Color4(uint[] v)
        {
            gl.glColor4uiv(v);
        }

        public static void Color4(ushort red, ushort green, ushort blue, ushort alpha)
        {
            gl.glColor4us(red, green, blue, alpha);
        }

        public static void Color4(ushort[] v)
        {
            gl.glColor4usv(v);
        }
        #endregion Set Vertex Color

        public static void ColorMask(byte red, byte green, byte blue, byte alpha)
        {
            gl.glColorMask(red, green, blue, alpha);
        }

        public static void ColorMaterial(GLFace face, ColorMaterialParameter mode)
        {
            gl.glColorMaterial((int)face, (int)mode);
        }

        #region Color Pointer
        public static void ColorPointer(int size, ColorPointerType type, int stride, byte[] pointer)
        {
            gl.glColorPointer(size, (int)type, stride, pointer);
        }

        public static void ColorPointer(int size, ColorPointerType type, int stride, sbyte[] pointer)
        {
            gl.glColorPointer(size, (int)type, stride, pointer);
        }

        public static void ColorPointer(int size, ColorPointerType type, int stride, short[] pointer)
        {
            gl.glColorPointer(size, (int)type, stride, pointer);
        }

        public static void ColorPointer(int size, ColorPointerType type, int stride, IntPtr pointer)
        {
            gl.glColorPointer(size, (int)type, stride, pointer);
        }

        public static void ColorPointer(int size, ColorPointerType type, int stride, ushort[] pointer)
        {
            gl.glColorPointer(size, (int)type, stride, pointer);
        }

        public static void ColorPointer(int size, ColorPointerType type, int stride, int[] pointer)
        {
            gl.glColorPointer(size, (int)type, stride, pointer);
        }

        public static void ColorPointer(int size, ColorPointerType type, int stride, uint[] pointer)
        {
            gl.glColorPointer(size, (int)type, stride, pointer);
        }

        public static void ColorPointer(int size, ColorPointerType type, int stride, float[,] pointer)
        {
            gl.glColorPointer(size, (int)type, stride, pointer);
        }

        public static void ColorPointer(int size, ColorPointerType type, int stride, double[] pointer)
        {
            gl.glColorPointer(size, (int)type, stride, pointer);
        }
        #endregion

        public static void CopyPixels(int x, int y, int width, int height, PixelCopyType type)
        {
            gl.glCopyPixels(x, y, width, height, (int)type);
        }

        public static void CopyTexImage1D(int level, TextureInternalFormat internalFormat, int x, int y, int width, int border)
        {
            gl.glCopyTexImage1D((int)Texture1DTarget.Texture1d, level, (int)internalFormat, x, y, width, border);
        }

        public static void CopyTexImage2D(Texture2DTarget target, int level, TextureInternalFormat internalFormat, int x, int y, int width, int height, int border)
        {
            gl.glCopyTexImage2D((int)target, level, (int)internalFormat, x, y, width, height, border);
        }

        public static void CopyTexSubImage1D(int level, int xoffset, int x, int y, int width)
        {
            gl.glCopyTexSubImage1D((int)Texture1DTarget.Texture1d, level, xoffset, x, y, width);
        }

        public static void CopyTexSubImage2D(Texture2DTarget target, int level, int xoffset, int yoffset, int x, int y, int width, int height)
        {
            gl.glCopyTexSubImage2D((int)target, level, xoffset, yoffset, x, y, width, height);
        }

        public static void CullFace(GLFace mode)
        {
            gl.glCullFace((int)mode);
        }


        public static void DeleteTextures(int n, uint[] textures)
        {
            gl.glDeleteTextures(n, textures);
        }

        public static void DepthFunc(DepthFunction func)
        {
            gl.glDepthFunc((int)func);
        }

        public static void DepthMask(Boolean flag)
        {
            gl.glDepthMask(flag);
        }

        public static void DepthRange(double zNear, double zFar)
        {
            gl.glDepthRange(zNear, zFar);
        }


        public static void DisableClientState(ClientArrayType array)
        {
            gl.glDisableClientState((int)array);
        }

        #region Drawing
        public static void DrawArrays(BeginMode mode, int first, int count)
        {
            gl.glDrawArrays((int)mode, first, count);
        }

        public static void DrawBuffer(DrawBufferMode mode)
        {
            gl.glDrawBuffer((int)mode);
        }

        #region Draw Elements
        public static void DrawElements(BeginMode mode, int count, DrawElementType type, byte[] indices)
        {
            gl.glDrawElements((int)mode, count, (int)type, indices);
        }

        public static void DrawElements(BeginMode mode, int count, DrawElementType type, ushort[] indices)
        {
            gl.glDrawElements((int)mode, count, (int)type, indices);
        }

        public static void DrawElements(BeginMode mode, int count, DrawElementType type, uint[] indices)
        {
            gl.glDrawElements((int)mode, count, (int)type, indices);
        }

        public static void DrawElements(BeginMode mode, int count, DrawElementType type, int[] indices)
        {
            gl.glDrawElements((int)mode, count, (int)type, indices);
        }
        #endregion

        #region Draw Pixels
        public static void DrawPixels(int width, int height, PixelLayout format, PixelComponentType type, IntPtr pixels)
        {
            gl.glDrawPixels(width, height, (int)format, (int)type, pixels);
        }

        public static void DrawPixels(int width, int height, PixelLayout format, PixelComponentType type, byte[] pixels)
        {
            gl.glDrawPixels(width, height, (int)format, (int)type, pixels);
        }

        public static void DrawPixels(int width, int height, PixelLayout format, PixelComponentType type, sbyte[] pixels)
        {
            gl.glDrawPixels(width, height, (int)format, (int)type, pixels);
        }

        public static void DrawPixels(int width, int height, PixelLayout format, PixelComponentType type, ushort[] pixels)
        {
            gl.glDrawPixels(width, height, (int)format, (int)type, pixels);
        }

        public static void DrawPixels(int width, int height, PixelLayout format, PixelComponentType type, short[] pixels)
        {
            gl.glDrawPixels(width, height, (int)format, (int)type, pixels);
        }

        public static void DrawPixels(int width, int height, PixelLayout format, PixelComponentType type, uint[] pixels)
        {
            gl.glDrawPixels(width, height, (int)format, (int)type, pixels);
        }

        public static void DrawPixels(int width, int height, PixelLayout format, PixelComponentType type, int[] pixels)
        {
            gl.glDrawPixels(width, height, (int)format, (int)type, pixels);
        }

        public static void DrawPixels(int width, int height, PixelLayout format, PixelComponentType type, float[] pixels)
        {
            gl.glDrawPixels(width, height, (int)format, (int)type, pixels);
        }
        #endregion

        #endregion Drawing

        #region Edge Flag
        public static void EdgeFlag(bool flag)
        {
            gl.glEdgeFlag(flag);
        }

        public static void EdgeFlagPointer(int stride, Boolean[] pointer)
        {
            gl.glEdgeFlagPointer(stride, pointer);
        }

        public static void EdgeFlagPointer(int stride, byte[] pointer)
        {
            gl.glEdgeFlagPointer(stride, pointer);
        }

        public static void EdgeFlagPointer(int stride, IntPtr pointer)
        {
            gl.glEdgeFlagPointer(stride, pointer);
        }

        public static void EdgeFlagv(ref Boolean flag)
        {
            gl.glEdgeFlagv(ref flag);
        }
        #endregion

        #region Enabling Attributes of Context
        public static void Enable(GLOption cap)
        {
            gl.glEnable((int)cap);
        }

        public static void Disable(GLOption cap)
        {
            gl.glDisable((int)cap);
        }

        //LineStipple = 2852,
        //PolygonStipple = 2882,
        //CullFace = 2884,
        //ColorMaterial = 2903,
        //StencilTest = 2960,
        //Normalize = 2977,
        //IndexLogicOp = 3057,
        //LogicOp = 3057,
        //ColorLogicOp = 3058,
        //ScissorTest = 3089,
        //TextureGenS = 3168,
        //TextureGenT = 3169,
        //TextureGenR = 3170,
        //TextureGenQ = 3171,
        //Map1Color4 = 3472,
        //Map1Index = 3473,
        //Map1Normal = 3474,
        //Map1TextureCoord1 = 3475,
        //Map1TextureCoord2 = 3476,
        //Map1TextureCoord3 = 3477,
        //Map1TextureCoord4 = 3478,
        //Map1Vertex3 = 3479,
        //Map1Vertex4 = 3480,
        //Map2Color4 = 3504,
        //Map2Index = 3505,
        //Map2Normal = 3506,
        //Map2TextureCoord1 = 3507,
        //Map2TextureCoord2 = 3508,
        //Map2TextureCoord3 = 3509,
        //Map2TextureCoord4 = 3510,
        //Texture1d = 3552,
        //PolygonOffsetPoint = 10753,
        //PolygonOffsetLine = 10754,
        //ClipPlane0 = 12288,
        //ClipPlane1 = 12289,
        //ClipPlane2 = 12290,
        //ClipPlane3 = 12291,
        //ClipPlane4 = 12292,
        //ClipPlane5 = 12293,
        //PolygonOffsetFill = 32823,
        //TextureCoordArray = 32888,
        //EdgeFlagArray = 32889,
        //PointSprite = 34913,

        #endregion Attributes of Context

        public static void EnableClientState(ClientArrayType array)
        {
            gl.glEnableClientState((int)array);
        }

        #region Evaluate Coordinates
        public static void EvalCoord1(double u)
        {
            gl.glEvalCoord1d(u);
        }

        public static void EvalCoord1(double[] u)
        {
            gl.glEvalCoord1dv(u);
        }

        public static void EvalCoord1(float u)
        {
            gl.glEvalCoord1f(u);
        }

        public static void EvalCoord1(float[] u)
        {
            gl.glEvalCoord1fv(u);
        }

        public static void EvalCoord2(double u, double v)
        {
            gl.glEvalCoord2d(u, v);
        }

        public static void EvalCoord2(double[] u)
        {
            gl.glEvalCoord2dv(u);
        }

        public static void EvalCoord2(float u, float v)
        {
            gl.glEvalCoord2f(u, v);
        }

        public static void EvalCoord2(float[] u)
        {
            gl.glEvalCoord2fv(u);
        }
        #endregion

        #region EvalMesh
        public static void EvalMesh1(MeshMode1 mode, int i1, int i2)
        {
            gl.glEvalMesh1((int)mode, i1, i2);
        }

        public static void EvalMesh2(MeshMode2 mode, int i1, int i2, int j1, int j2)
        {
            gl.glEvalMesh2((int)mode, i1, i2, j1, j2);
        }
        #endregion

        #region EvalPoint
        public static void EvalPoint1(int i)
        {
            gl.glEvalPoint1(i);
        }

        public static void EvalPoint2(int i, int j)
        {
            gl.glEvalPoint2(i, j);
        }
        #endregion

        public static void FeedbackBuffer(int size, FeedBackMode type, float[] buffer)
        {
            gl.glFeedbackBuffer(size, (int)type, buffer);
        }

        public static void Finish()
        {
            gl.glFinish();
        }

        public static void Flush()
        {
            gl.glFlush();
        }

        #region Fog
        public static void Fogf(FogParameter pname, float param)
        {
            gl.glFogf((int)pname, (int)param);
        }

        public static void Fogfv(FogParameter pname, float[] parameters)
        {
            gl.glFogfv((int)pname, parameters);
        }

        public static void Fogi(FogParameter pname, int param)
        {
            gl.glFogi((int)pname, param);
        }

        public static void Fogiv(FogParameter pname, int[] parms)
        {
            gl.glFogiv((int)pname, parms);
        }
        #endregion

        public static void FrontFace(FrontFaceDirection mode)
        {
            gl.glFrontFace((int)mode);
        }



        public static void GenTextures(int n, uint[] textures)
        {
            gl.glGenTextures(n, textures);
        }

        public static void GetClipPlane(ClipPlaneName plane, double[] equation)
        {
            gl.glGetClipPlane((int)plane, equation);
        }

        public static GLErrorCode GetError()
        {
            return (GLErrorCode)gl.glGetError();
        }

        #region Get State information
        public static void GetBoolean(GetTarget pname, byte[] parameters)
        {
            gl.glGetBooleanv((int)pname, parameters);
        }

        public static void GetBoolean(GetTarget pname, int[] parameters)
        {
            gl.glGetBooleanv((int)pname, parameters);
        }

        public static bool GetBoolean(GetTarget pname)
        {
            int[] parameters = new int[1];
            GetBoolean(pname, parameters);

            return (parameters[0] == 1);
        }


        public static void GetDouble(GetTarget pname, double[] parameters)
        {
            gl.glGetDoublev((int)pname, parameters);
        }

        public static double GetDouble(GetTarget pname)
        {
            double[] parameters = new double[1];
            GetDouble(pname, parameters);

            return parameters[0];
        }


        public static void GetFloat(GetTarget pname, float[] parameters)
        {
            gl.glGetFloatv((int)pname, parameters);
        }

        public static float GetFloat(GetTarget pname)
        {
            float[] parameters = new float[1];
            GetFloat(pname, parameters);

            return parameters[0];
        }

        public static void GetInteger(GetTarget pname, int[] parameters)
        {
            gl.glGetIntegerv((int)pname, parameters);
        }

        public static int GetInteger(GetTarget pname)
        {
            int[] parameters = new int[1];
            GetInteger(pname, parameters);

            return parameters[0];
        }
        #endregion

        public static void GetLightfv(GLLightName light, LightParameter pname, float[] parameters)
        {
            gl.glGetLightfv((int)light, (int)pname, parameters);
        }

        public static void GetLightiv(GLLightName light, LightParameter pname, int[] parameters)
        {
            gl.glGetLightiv((int)light, (int)pname, parameters);
        }

        public static void GetMapdv(MapTarget target, GetMapTarget query, double[] v)
        {
            gl.glGetMapdv((int)target, (int)query, v);
        }

        public static void GetMapfv(MapTarget target, GetMapTarget query, float[] v)
        {
            gl.glGetMapfv((int)target, (int)query, v);
        }

        public static void GetMapiv(MapTarget target, GetMapTarget query, int[] v)
        {
            gl.glGetMapiv((int)target, (int)query, v);
        }

        #region GetMaterial
        public static void GetMaterialfv(GLFace face, GetMaterialParameter pname, float[] parameters)
        {
            gl.glGetMaterialfv((int)face, (int)pname, parameters);
        }

        public static void GetMaterialiv(GLFace face, GetMaterialParameter pname, int[] parameters)
        {
            gl.glGetMaterialiv((int)face, (int)pname, parameters);
        }
        #endregion

        #region GetPixelMap
        public static void GetPixelMapfv(GetPixelMap map, float[] values)
        {
            gl.glGetPixelMapfv((int)map, values);
        }

        public static void GetPixelMapuiv(GetPixelMap map, uint[] values)
        {
            gl.glGetPixelMapuiv((int)map, values);
        }

        public static void GetPixelMapusv(GetPixelMap map, ushort[] values)
        {
            gl.glGetPixelMapusv((int)map, values);
        }
        #endregion

        public static void GetPointerv(GetPointerTarget pname, ref IntPtr parameters)
        {
            gl.glGetPointerv((int)pname, ref parameters);
        }

        public static void GetPolygonStipple(byte[] mask)
        {
            gl.glGetPolygonStipple(mask);
        }


        #region GetTexEnv
        public static void GetTexEnvfv(TextureEnvironmentTarget target, TextureEnvironmentParameter pname, float[] parameters)
        {
            gl.glGetTexEnvfv((int)target, (int)pname, parameters);
        }

        public static void GetTexEnviv(TextureEnvironmentTarget target, TextureEnvironmentParameter pname, int[] parameters)
        {
            gl.glGetTexEnviv((int)target, (int)pname, parameters);
        }
        #endregion

        #region GetTexGen
        public static void GetTexGendv(TextureCoordName coord, TextureGenParameter pname, double[] parameters)
        {
            gl.glGetTexGendv((int)coord, (int)pname, parameters);
        }

        public static void GetTexGenfv(TextureCoordName coord, TextureGenParameter pname, float[] parameters)
        {
            gl.glGetTexGenfv((int)coord, (int)pname, parameters);
        }

        public static void GetTexGeniv(TextureCoordName coord, TextureGenParameter pname, int[] parameters)
        {
            gl.glGetTexGeniv((int)coord, (int)pname, parameters);
        }
        #endregion

        #region GetTexImage
        public static void GetTexImage(GetTextureImageTarget target, int level, GetTextureImageFormat format, GetTextureImageType type, byte[] pixels)
        {
            gl.glGetTexImage((int)target, level, (int)format, (int)type, pixels);
        }

        public static void GetTexImage(GetTextureImageTarget target, int level, GetTextureImageFormat format, GetTextureImageType type, sbyte[] pixels)
        {
            gl.glGetTexImage((int)target, level, (int)format, (int)type, pixels);
        }

        public static void GetTexImage(GetTextureImageTarget target, int level, GetTextureImageFormat format, GetTextureImageType type, short[] pixels)
        {
            gl.glGetTexImage((int)target, level, (int)format, (int)type, pixels);
        }

        public static void GetTexImage(GetTextureImageTarget target, int level, GetTextureImageFormat format, GetTextureImageType type, ushort[] pixels)
        {
            gl.glGetTexImage((int)target, level, (int)format, (int)type, pixels);
        }

        public static void GetTexImage(GetTextureImageTarget target, int level, GetTextureImageFormat format, GetTextureImageType type, int[] pixels)
        {
            gl.glGetTexImage((int)target, level, (int)format, (int)type, pixels);
        }

        public static void GetTexImage(GetTextureImageTarget target, int level, GetTextureImageFormat format, GetTextureImageType type, uint[] pixels)
        {
            gl.glGetTexImage((int)target, level, (int)format, (int)type, pixels);
        }

        public static void GetTexImage(GetTextureImageTarget target, int level, GetTextureImageFormat format, GetTextureImageType type, float[] pixels)
        {
            gl.glGetTexImage((int)target, level, (int)format, (int)type, pixels);
        }
        #endregion

        #region GetTexLevelParameter
        public static void GetTexLevelParameterfv(GetTextureLevelTarget target, int level, GetTextureLevelParamter pname, float[] parameters)
        {
            gl.glGetTexLevelParameterfv((int)target, level, (int)pname, parameters);
        }

        public static void GetTexLevelParameteriv(GetTextureLevelTarget target, int level, GetTextureLevelParamter pname, int[] parameters)
        {
            gl.glGetTexLevelParameteriv((int)target, level, (int)pname, parameters);
        }
        #endregion

        #region GetTexParameter
        public static void GetTexParameterfv(GetTextureParameterTarget target, GetTextureParameter pname, float[] parameters)
        {
            gl.glGetTexParameterfv((int)target, (int)pname, parameters);
        }

        public static void GetTexParameteriv(GetTextureParameterTarget target, GetTextureParameter pname, int[] parameters)
        {
            gl.glGetTexParameteriv((int)target, (int)pname, parameters);
        }
        #endregion

        public static void Hint(HintTarget target, HintMode mode)
        {
            gl.glHint((int)target, (int)mode);
        }

        public static void IndexMask(uint mask)
        {
            gl.glIndexMask(mask);
        }

        #region IndexPointer
        public static void IndexPointer(IndexPointerType type, int stride, short[] pointer)
        {
            gl.glIndexPointer((int)type, stride, pointer);
        }

        public static void IndexPointer(IndexPointerType type, int stride, int[] pointer)
        {
            gl.glIndexPointer((int)type, stride, pointer);
        }

        public static void IndexPointer(IndexPointerType type, int stride, float[] pointer)
        {
            gl.glIndexPointer((int)type, stride, pointer);
        }

        public static void IndexPointer(IndexPointerType type, int stride, double[] pointer)
        {
            gl.glIndexPointer((int)type, stride, pointer);
        }

        public static void IndexPointer(IndexPointerType type, int stride, IntPtr pointer)
        {
            gl.glIndexPointer((int)type, stride, pointer);
        }
        #endregion

        #region Index
        public static void Index(double c)
        {
            gl.glIndexd(c);
        }

        public static void Index(double[] c)
        {
            gl.glIndexdv(c);
        }

        public static void Index(float c)
        {
            gl.glIndexf(c);
        }

        public static void Index(float[] c)
        {
            gl.glIndexfv(c);
        }

        public static void Index(int c)
        {
            gl.glIndexi(c);
        }

        public static void Index(int[] c)
        {
            gl.glIndexiv(c);
        }

        public static void Index(short c)
        {
            gl.glIndexs(c);
        }

        public static void Index(short[] c)
        {
            gl.glIndexsv(c);
        }

        public static void Index(byte c)
        {
            gl.glIndexub(c);
        }

        public static void Index(byte[] c)
        {
            gl.glIndexubv(c);
        }
        #endregion


        public static void InterleavedArrays(InterleavedArrays format, int stride, byte[] pointer)
        {
            gl.glInterleavedArrays((int)format, stride, pointer);
        }

        public static void InterleavedArrays(InterleavedArrays format, int stride, float[] pointer)
        {
            gl.glInterleavedArrays((int)format, stride, pointer);
        }

        public static Boolean IsEnabled(GLOption cap)
        {
            return gl.glIsEnabled((int)cap);
        }


        public static Boolean IsTexture(uint texture)
        {
            return gl.glIsTexture(texture);
        }

        #region LightModel
        public static void LightModel(LightModelParameter pname, float param)
        {
            gl.glLightModelf((int)pname, param);
        }

        public static void LightModel(LightModelParameter pname, float[] parameters)
        {
            gl.glLightModelfv((int)pname, parameters);
        }

        public static void LightModel(LightModelParameter pname, int param)
        {
            gl.glLightModeli((int)pname, param);
        }

        public static void LightModel(LightModelParameter pname, int[] parameters)
        {
            gl.glLightModeliv((int)pname, parameters);
        }

        public static void LightModel(LightModelParameter pname, LightModel param)
        {
            gl.glLightModeli((int)pname, (int)param);
        }
        #endregion

        #region Light
        public static void Light(GLLightName light, LightParameter pname, float param)
        {
            gl.glLightf((int)light, (int)pname, param);
        }

        public static void Light(GLLightName light, LightParameter pname, float[] parameters)
        {
            gl.glLightfv((int)light, (int)pname, parameters);
        }

        public static void Light(GLLightName light, LightParameter pname, int param)
        {
            gl.glLighti((int)light, (int)pname, param);
        }

        public static void Light(GLLightName light, LightParameter pname, int[] parameters)
        {
            gl.glLightiv((int)light, (int)pname, parameters);
        }
        #endregion

        public static void LineStipple(int factor, ushort pattern)
        {
            gl.glLineStipple(factor, pattern);
        }

        public static void LineWidth(float width)
        {
            gl.glLineWidth(width);
        }

        public static void PixelZoom(float xfactor, float yfactor)
        {
            gl.glPixelZoom(xfactor, yfactor);
        }

        public static void PointSize(float size)
        {
            gl.glPointSize(size);
        }

        public static void LogicOp(LogicOp opcode)
        {
            gl.glLogicOp((int)opcode);
        }

        #region Map
        #region Map1
        public static void Map1(MapTarget target, double u1, double u2, int stride, int order, double[] points)
        {
            gl.glMap1d((int)target, u1, u2, stride, order, points);
        }

        public static void Map1(MapTarget target, float u1, float u2, int stride, int order, float[] points)
        {
            gl.glMap1f((int)target, u1, u2, stride, order, points);
        }
        #endregion Map1

        #region Map2
        public static void Map2(MapTarget target, double u1, double u2, int ustride, int uorder, double v1, double v2, int vstride, int vorder, double[] points)
        {
            gl.glMap2d((int)target, u1, u2, ustride, uorder, v1, v2, vstride, vorder, points);
        }

        public static void Map2(MapTarget target, float u1, float u2, int ustride, int uorder, float v1, float v2, int vstride, int vorder, float[] points)
        {
            gl.glMap2f((int)target, u1, u2, ustride, uorder, v1, v2, vstride, vorder, points);
        }
        #endregion Map2

        #region MapGrid1
        public static void MapGrid1(int un, double u1, double u2)
        {
            gl.glMapGrid1d(un, u1, u2);
        }

        public static void MapGrid1(int un, float u1, float u2)
        {
            gl.glMapGrid1f(un, u1, u2);
        }
        #endregion MapGrid1

        #region MapGrid2
        public static void MapGrid2(int un, double u1, double u2, int vn, double v1, double v2)
        {
            gl.glMapGrid2d(un, u1, u2, vn, v1, v2);
        }

        public static void MapGrid2(int un, float u1, float u2, int vn, float v1, float v2)
        {
            gl.glMapGrid2f(un, u1, u2, vn, v1, v2);
        }
        #endregion MapGrid2
        #endregion Map

        #region Material
        public static void Material(GLFace face, MaterialParameter pname, float param)
        {
            gl.glMaterialf((int)face, (int)pname, param);
        }

        public static void Material(GLFace face, MaterialParameter pname, float[] parameters)
        {
            gl.glMaterialfv((int)face, (int)pname, parameters);
        }

        public static void Material(GLFace face, MaterialParameter pname, int param)
        {
            gl.glMateriali((int)face, (int)pname, param);
        }

        public static void Material(GLFace face, MaterialParameter pname, int[] parameters)
        {
            gl.glMaterialiv((int)face, (int)pname, parameters);
        }
        #endregion

        #region Normal3
        public static void Normal(Vector3f vec)
        {
            gl.glNormal3f(vec.x, vec.y, vec.z);
        }

        public static void Normal3(sbyte nx, sbyte ny, sbyte nz)
        {
            gl.glNormal3b(nx, ny, nz);
        }

        public static void Normal3(sbyte[] v)
        {
            gl.glNormal3bv(v);
        }

        public static void Normal3(double nx, double ny, double nz)
        {
            gl.glNormal3d(nx, ny, nz);
        }

        public static void Normal3(double[] v)
        {
            gl.glNormal3dv(v);
        }

        public static void Normal3(float nx, float ny, float nz)
        {
            gl.glNormal3f(nx, ny, nz);
        }

        public static void Normal3(float[] v)
        {
            gl.glNormal3fv(v);
        }

        public static void Normal3(int nx, int ny, int nz)
        {
            gl.glNormal3i(nx, ny, nz);
        }

        public static void Normal3(int[] v)
        {
            gl.glNormal3iv(v);
        }

        public static void Normal3(short nx, short ny, short nz)
        {
            gl.glNormal3s(nx, ny, nz);
        }

        public static void Normal3(short[] v)
        {
            gl.glNormal3sv(v);
        }
        #endregion

        #region NormalPointer
        public static void NormalPointer(Vector3f[] pointer)
        {
            gl.glNormalPointer((int)NormalPointerType.Float, 0, pointer);
        }

        public static void NormalPointer(NormalPointerType type, int stride, IntPtr pointer)
        {
            gl.glNormalPointer((int)type, stride, pointer);
        }
        
        public static void NormalPointer(NormalPointerType type, int stride, byte[] pointer)
        {
            gl.glNormalPointer((int)type, stride, pointer);
        }

        public static void NormalPointer(NormalPointerType type, int stride, short[] pointer)
        {
            gl.glNormalPointer((int)type, stride, pointer);
        }

        public static void NormalPointer(NormalPointerType type, int stride, int[] pointer)
        {
            gl.glNormalPointer((int)type, stride, pointer);
        }

        public static void NormalPointer(NormalPointerType type, int stride, float[] pointer)
        {
            gl.glNormalPointer((int)type, stride, pointer);
        }

        public static void NormalPointer(NormalPointerType type, int stride, double[] pointer)
        {
            gl.glNormalPointer((int)type, stride, pointer);
        }

        #endregion

        public static void PassThrough(float token)
        {
            gl.glPassThrough(token);
        }

        #region PixelMap
        public static void PixelMapfv(uint map, int mapsize, float[] values)
        {
            gl.glPixelMapfv(map, mapsize, values);
        }

        public static void PixelMapuiv(PixelMap map, int mapsize, uint[] values)
        {
            gl.glPixelMapuiv((int)map, mapsize, values);
        }

        public static void PixelMapusv(PixelMap map, int mapsize, ushort[] values)
        {
            gl.glPixelMapusv((int)map, mapsize, values);
        }
        #endregion

        #region PixelStore
        public static void PixelStore(PixelStore pname, float param)
        {
            gl.glPixelStoref((int)pname, param);
        }

        public static void PixelStore(PixelStore pname, int param)
        {
            gl.glPixelStorei((int)pname, param);
        }
        #endregion

        #region PixelTransfer
        public static void PixelTransfer(PixelTransfer pname, float param)
        {
            gl.glPixelTransferf((int)pname, param);
        }

        public static void PixelTransfer(PixelTransfer pname, int param)
        {
            gl.glPixelTransferi((int)pname, param);
        }
        #endregion PixelTransfer


        #region Polygon
        public static void PolygonMode(GLFace face, PolygonMode mode)
        {
            gl.glPolygonMode((int)face, (int)mode);
        }

        public static void PolygonOffset(float factor, float units)
        {
            gl.glPolygonOffset(factor, units);
        }

        public static void PolygonStipple(byte[] mask)
        {
            gl.glPolygonStipple(mask);
        }
        #endregion

        public static void PopAttrib()
        {
            gl.glPopAttrib();
        }

        public static void PushAttrib(AttribMask mask)
        {
            gl.glPushAttrib((int)mask);
        }

        public static void PopClientAttrib()
        {
            gl.glPopClientAttrib();
        }

        public static void PushClientAttrib(ClientAttribMask mask)
        {
            gl.glPushClientAttrib((int)mask);
        }



        public static void PrioritizeTextures(int n, uint[] textures, float[] priorities)
        {
            gl.glPrioritizeTextures(n, textures, priorities);
        }

        #region string GetString(StringName name)
        public static string GetString(StringName name)
        {
            IntPtr strPtr = gl.glGetString((int)name);
            if (IntPtr.Zero == strPtr)
                return string.Empty;

            string aString = Marshal.PtrToStringAnsi(strPtr);

            return aString;
        }
        #endregion

        #region Names
        public static void InitNames()
        {
            gl.glInitNames();
        }


        public static void LoadName(uint name)
        {
            gl.glLoadName(name);
        }

        public static void PopName()
        {
            gl.glPopName();
        }

        public static void PushName(uint name)
        {
            gl.glPushName(name);
        }
        #endregion

        #region RasterPos
        #region void RasterPos2...
        public static void RasterPos(float2 vec)
        {
            gl.glRasterPos2f(vec.x, vec.y);
        }

        public static void RasterPos2d(double x, double y)
        {
            gl.glRasterPos2d(x, y);
        }

        public static void RasterPos2dv(double[] v)
        {
            gl.glRasterPos2dv(v);
        }

        public static void RasterPos2f(float x, float y)
        {
            gl.glRasterPos2f(x, y);
        }

        public static void RasterPos2fv(float[] v)
        {
            gl.glRasterPos2fv(v);
        }

        public static void RasterPos2i(int x, int y)
        {
            gl.glRasterPos2i(x, y);
        }

        public static void RasterPos2iv(int[] v)
        {
            gl.glRasterPos2iv(v);
        }

        public static void RasterPos2s(short x, short y)
        {
            gl.glRasterPos2s(x, y);
        }

        public static void RasterPos2sv(short[] v)
        {
            gl.glRasterPos2sv(v);
        }
        #endregion

        #region void RasterPos3...
        public static void RasterPos(Vector3f vec)
        {
            gl.glRasterPos3f(vec.x, vec.y, vec.z);
        }

        public static void RasterPos3d(double x, double y, double z)
        {
            gl.glRasterPos3d(x, y, z);
        }

        public static void RasterPos3dv(double[] v)
        {
            gl.glRasterPos3dv(v);
        }

        public static void RasterPos3f(float x, float y, float z)
        {
            gl.glRasterPos3f(x, y, z);
        }

        public static void RasterPos3fv(float[] v)
        {
            gl.glRasterPos3fv(v);
        }

        public static void RasterPos3i(int x, int y, int z)
        {
            gl.glRasterPos3i(x, y, z);
        }

        public static void RasterPos3iv(int[] v)
        {
            gl.glRasterPos3iv(v);
        }

        public static void RasterPos3s(short x, short y, short z)
        {
            gl.glRasterPos3s(x, y, z);
        }

        public static void RasterPos3sv(short[] v)
        {
            gl.glRasterPos3sv(v);
        }
        #endregion

        #region void RasterPos4...
        public static void RasterPos(float4 vec)
        {
            gl.glRasterPos4f(vec.x, vec.y, vec.z, vec.w);
        }

        public static void RasterPos4d(double x, double y, double z, double w)
        {
            gl.glRasterPos4d(x, y, z, w);
        }

        public static void RasterPos4dv(double[] v)
        {
            gl.glRasterPos4dv(v);
        }

        public static void RasterPos4f(float x, float y, float z, float w)
        {
            gl.glRasterPos4f(x, y, z, w);
        }

        public static void RasterPos4fv(float[] v)
        {
            gl.glRasterPos4fv(v);
        }

        public static void RasterPos4i(int x, int y, int z, int w)
        {
            gl.glRasterPos4i(x, y, z, w);
        }

        public static void RasterPos4iv(int[] v)
        {
            gl.glRasterPos4iv(v);
        }

        public static void RasterPos4s(short x, short y, short z, short w)
        {
            gl.glRasterPos4s(x, y, z, w);
        }

        public static void RasterPos4sv(short[] v)
        {
            gl.glRasterPos4sv(v);
        }
        #endregion
        #endregion

        public static void ReadBuffer(ReadBufferMode mode)
        {
            gl.glReadBuffer((int)mode);
        }

        public static void SelectBuffer(int size, uint[] buffer)
        {
            gl.glSelectBuffer(size, buffer);
        }

        #region ReadPixels
        public static void glReadPixels(int x, int y, int width, int height, PixelLayout format, PixelComponentType type, byte[] pixels)
        {
            gl.glReadPixels(x, y, width, height, (int)format, (int)type, pixels);
        }

        public static void glReadPixels(int x, int y, int width, int height, PixelLayout format, PixelComponentType type, sbyte[] pixels)
        {
            gl.glReadPixels(x, y, width, height, (int)format, (int)type, pixels);
        }

        public static void glReadPixels(int x, int y, int width, int height, PixelLayout format, PixelComponentType type, short[] pixels)
        {
            gl.glReadPixels(x, y, width, height, (int)format, (int)type, pixels);
        }

        public static void glReadPixels(int x, int y, int width, int height, PixelLayout format, PixelComponentType type, ushort[] pixels)
        {
            gl.glReadPixels(x, y, width, height, (int)format, (int)type, pixels);
        }

        public static void glReadPixels(int x, int y, int width, int height, PixelLayout format, PixelComponentType type, int[] pixels)
        {
            gl.glReadPixels(x, y, width, height, (int)format, (int)type, pixels);
        }

        public static void glReadPixels(int x, int y, int width, int height, PixelLayout format, PixelComponentType type, uint[] pixels)
        {
            gl.glReadPixels(x, y, width, height, (int)format, (int)type, pixels);
        }

        public static void glReadPixels(int x, int y, int width, int height, PixelLayout format, PixelComponentType type, float[] pixels)
        {
            gl.glReadPixels(x, y, width, height, (int)format, (int)type, pixels);
        }
        #endregion

        #region Rect
        public static void Rect(double x1, double y1, double x2, double y2)
        {
            gl.glRectd(x1, y1, x2, y2);
        }

        public static void Rect(float x1, float y1, float x2, float y2)
        {
            gl.glRectf(x1, y1, x2, y2);
        }

        public static void Rect(int x1, int y1, int x2, int y2)
        {
            gl.glRecti(x1, y1, x2, y2);
        }

        public static void Rect(short x1, short y1, short x2, short y2)
        {
            gl.glRects(x1, y1, x2, y2);
        }

        public static void Rect(double[] v1, double[] v2)
        {
            gl.glRectdv(v1, v2);
        }

        public static void Rect(float[] v1, float[] v2)
        {
            gl.glRectfv(v1, v2);
        }


        public static void Rect(int[] v1, int[] v2)
        {
            gl.glRectiv(v1, v2);
        }

        public static void Rect(short[] v1, short[] v2)
        {
            gl.glRectsv(v1, v2);
        }
        #endregion


        #region View Management
        public static void ClipPlane(ClipPlaneName plane, double[] equation)
        {
            gl.glClipPlane((int)plane, equation);
        }

        public static void Frustum(double left, double right, double bottom, double top, double zNear, double zFar)
        {
            gl.glFrustum(left, right, bottom, top, zNear, zFar);
        }

        public static void Ortho(double left, double right, double bottom, double top, double zNear, double zFar)
        {
            gl.glOrtho(left, right, bottom, top, zNear, zFar);
        }

        public static void Scissor(int x, int y, int width, int height)
        {
            gl.glScissor(x, y, width, height);
        }

        public static void Viewport(int x, int y, int width, int height)
        {
            gl.glViewport(x, y, width, height);
        }
        #endregion View Management

        #region Transform
        public static void Rotate(double angle, double x, double y, double z)
        {
            gl.glRotated(angle, x, y, z);
        }

        public static void Rotate(float angle, float x, float y, float z)
        {
            gl.glRotatef(angle, x, y, z);
        }

        public static void Scale(double x, double y, double z)
        {
            gl.glScaled(x, y, z);
        }

        public static void Scale(float x, float y, float z)
        {
            gl.glScalef(x, y, z);
        }

        public static void Translate(double x, double y, double z)
        {
            gl.glTranslated(x, y, z);
        }

        public static void Translate(float x, float y, float z)
        {
            gl.glTranslatef(x, y, z);
        }
        #endregion

        #region Matrix Management
        public static void LoadIdentity()
        {
            gl.glLoadIdentity();
        }

        public static void LoadMatrixd(double[] m)
        {
            gl.glLoadMatrixd(m);
        }

        public static void LoadMatrixf(float[] m)
        {
            gl.glLoadMatrixf(m);
        }

        public static void MatrixMode(MatrixMode mode)
        {
            gl.glMatrixMode((int)mode);
        }

        public static void MultMatrix(double[] m)
        {
            gl.glMultMatrixd(m);
        }

        public static void MultMatrix(float[] m)
        {
            gl.glMultMatrixf(m);
        }

        public static void PushMatrix()
        {
            gl.glPushMatrix();
        }

        public static void PopMatrix()
        {
            gl.glPopMatrix();
        }
        #endregion


        public static RenderingMode RenderMode(RenderingMode mode)
        {
            int retValue = gl.glRenderMode((int)mode);
            return (RenderingMode)retValue;
        }

        public static void ShadeModel(ShadingModel mode)
        {
            gl.glShadeModel((int)mode);
        }

        #region Stencil
        public static void StencilFunc(StencilFunction func, int reference, uint mask)
        {
            gl.glStencilFunc((int)func, reference, mask);
        }

        public static void StencilMask(uint mask)
        {
            gl.glStencilMask(mask);
        }

        public static void StencilOp(StencilOp fail, StencilOp zfail, StencilOp zpass)
        {
            gl.glStencilOp((int)fail, (int)zfail, (int)zpass);
        }
        #endregion

        #region TexCoord1
        public static void TexCoord1(double s)
        {
            gl.glTexCoord1d(s);
        }

        public static void TexCoord1(double[] v)
        {
            gl.glTexCoord1dv(v);
        }

        public static void TexCoord1(float s)
        {
            gl.glTexCoord1f(s);
        }

        public static void TexCoord1(float[] v)
        {
            gl.glTexCoord1fv(v);
        }

        public static void TexCoord1(int s)
        {
            gl.glTexCoord1i(s);
        }

        public static void TexCoord1(int[] v)
        {
            gl.glTexCoord1iv(v);
        }

        public static void TexCoord1(short s)
        {
            gl.glTexCoord1s(s);
        }

        public static void TexCoord1(short[] v)
        {
            gl.glTexCoord1sv(v);
        }
        #endregion TexCoord1

        #region TexCoord2
        public static void TexCoord(TextureCoordinates coords)
        {
            gl.glTexCoord2f(coords.s, coords.t);
        }

        public static void TexCoord2(double s, double t)
        {
            gl.glTexCoord2d(s, t);
        }

        public static void TexCoord2(double[] v)
        {
            gl.glTexCoord2dv(v);
        }

        public static void TexCoord2(float s, float t)
        {
            gl.glTexCoord2f(s, t);
        }

        public static void TexCoord2(float[] v)
        {
            gl.glTexCoord2fv(v);
        }

        public static void TexCoord2(int s, int t)
        {
            gl.glTexCoord2i(s, t);
        }

        public static void TexCoord2(int[] v)
        {
            gl.glTexCoord2iv(v);
        }

        public static void TexCoord2(short s, short t)
        {
            gl.glTexCoord2s(s, t);
        }

        public static void TexCoord2(short[] v)
        {
            gl.glTexCoord2sv(v);
        }
        #endregion

        #region TexCoord3
        public static void TexCoord3(double s, double t, double r)
        {
            gl.glTexCoord3d(s, t, r);
        }

        public static void TexCoord3(double[] v)
        {
            gl.glTexCoord3dv(v);
        }

        public static void TexCoord3(float s, float t, float r)
        {
            gl.glTexCoord3f(s, t, r);
        }

        public static void TexCoord3(float[] v)
        {
            gl.glTexCoord3fv(v);
        }

        public static void TexCoord3(int s, int t, int r)
        {
            gl.glTexCoord3i(s, t, r);
        }

        public static void TexCoord3(int[] v)
        {
            gl.glTexCoord3iv(v);
        }

        public static void TexCoord3(short s, short t, short r)
        {
            gl.glTexCoord3s(s, t, r);
        }

        public static void TexCoord3(short[] v)
        {
            gl.glTexCoord3sv(v);
        }
        #endregion TexCoord3

        #region TexCoord4
        public static void TexCoord4(double s, double t, double r, double q)
        {
            gl.glTexCoord4d(s, t, r, q);
        }

        public static void TexCoord4(double[] v)
        {
            gl.glTexCoord4dv(v);
        }

        public static void TexCoord4(float s, float t, float r, float q)
        {
            gl.glTexCoord4f(s, t, r, q);
        }

        public static void TexCoord4(float[] v)
        {
            gl.glTexCoord4fv(v);
        }

        public static void TexCoord4(int s, int t, int r, int q)
        {
            gl.glTexCoord4i(s, t, r, q);
        }

        public static void TexCoord4(int[] v)
        {
            gl.glTexCoord4iv(v);
        }

        public static void TexCoord4(short s, short t, short r, short q)
        {
            gl.glTexCoord4s(s, t, r, q);
        }

        public static void TexCoord4(short[] v)
        {
            gl.glTexCoord4sv(v);
        }
        #endregion

        #region TexCoordPointer
        public static void TexCoordPointer(TextureCoordinates[] pointer)
        {
            gl.glTexCoordPointer(2, (int)TexCoordPointerType.Float, 0, pointer);
        }

        public static void TexCoordPointer(int size, TexCoordPointerType type, int stride, IntPtr pointer)
        {
            gl.glTexCoordPointer(size, (int)type, stride, pointer);
        }

        //public static void TexCoordPointer(int size, TexCoordPointerType type, int stride, short[] pointer)
        //{
        //    gl.glTexCoordPointer(size, type, stride, pointer);
        //}

        //public static void TexCoordPointer(int size, TexCoordPointerType type, int stride, int[] pointer)
        //{
        //    gl.glTexCoordPointer(size, type, stride, pointer);
        //}

        //public static void TexCoordPointer(int size, TexCoordPointerType type, int stride, float[] pointer)
        //{
        //    gl.glTexCoordPointer(size, type, stride, pointer);
        //}

        //public static void TexCoordPointer(int size, TexCoordPointerType type, int stride, double[] pointer)
        //{
        //    gl.glTexCoordPointer(size, type, stride, pointer);
        //}

        #endregion TexCoordPointer

        #region TexEnv
        public static void TexEnv(TextureEnvironmentTarget target, TextureEnvironmentParameter pname, int param)
        {
            gl.glTexEnvi((int)target, (int)pname, (int)param);
        }


        public static void TexEnv(TextureEnvironmentTarget target, TextureEnvironmentParameter pname, float[] parameters)
        {
            gl.glTexEnvfv((int)target, (int)pname, parameters);
        }

        public static void TexEnv(TextureEnvironmentTarget target, TextureEnvironmentParameter pname, int[] parameters)
        {
            gl.glTexEnviv((int)target, (int)pname, parameters);
        }
        #endregion TexEnv

        #region TexGen
        public static void TexGen(TextureCoordName coord, TextureGenParameter pname, TextureGenMode param)
        {
            gl.glTexGeni((int)coord, (uint)pname, (int)param);
        }

        public static void TexGen(TextureCoordName coord, TextureGenParameter pname, double[] parameters)
        {
            gl.glTexGendv((int)coord, (int)pname, parameters);
        }

        public static void TexGen(TextureCoordName coord, uint pname, float[] parameters)
        {
            gl.glTexGenfv((int)coord, pname, parameters);
        }


        public static void TexGen(TextureCoordName coord, uint pname, int[] parameters)
        {
            gl.glTexGeniv((int)coord, pname, parameters);
        }
        #endregion TexGen

        #region TexImage1D
        public static void TexImage1D(int level, TextureInternalFormat internalformat, int width, int border, TexturePixelFormat format, PixelComponentType type, IntPtr pixels)
        {
            gl.glTexImage1D((int)Texture1DTarget.Texture1d, level, (int)internalformat, width, border, (int)format, (int)type, pixels);
        }
        public static void TexImage1D(int level, TextureInternalFormat internalformat, int width, int border, TexturePixelFormat format, PixelComponentType type, object pixels)
        {
            gl.glTexImage1D((int)Texture1DTarget.Texture1d, level, (int)internalformat, width, border, (int)format, (int)type, pixels);
        }
        #endregion

        #region TexImage2D
        public static void TexImage2D(Texture2DTarget target, int level, TextureInternalFormat internalformat, int width, int height, int border, TexturePixelFormat format, PixelComponentType type, IntPtr pixels)
        {
            gl.glTexImage2D((int)target, level, (int)internalformat, width, height, border, (int)format, (int)type, pixels);
        }

        public static void TexImage2D(Texture2DTarget target, int level, TextureInternalFormat internalformat, int width, int height, int border, TexturePixelFormat format, PixelComponentType type, object pixels)
        {
            gl.glTexImage2D((int)target, level, (int)internalformat, width, height, border, (int)format, (int)type, pixels);
        }

        #endregion TexImage2D

        #region TexParameter
        public static void TexParameterf(TextureParameterTarget target, TextureParameterName pname, float param)
        {
            gl.glTexParameterf((int)target, (int)pname, param);
        }

        public static void TexParameterfv(TextureArrayParameterTarget target, TextureParameterName pname, float[] parameters)
        {
            gl.glTexParameterfv((int)target, (int)pname, parameters);
        }

        public static void TexParameteri(TextureParameterTarget target, TextureParameterName pname, int param)
        {
            gl.glTexParameteri((int)target, (int)pname, param);
        }

        public static void TexParameter(TextureParameterTarget target, TextureParameterName pname, TextureWrapMode param)
        {
            gl.glTexParameteri((int)target, (int)pname, (int)param);
        }

        public static void TexParameter(TextureParameterTarget target, TextureParameterName pname, TextureMagFilter param)
        {
            gl.glTexParameteri((int)target, (int)pname, (int)param);
        }

        public static void TexParameter(TextureParameterTarget target, TextureParameterName pname, TextureMinFilter param)
        {
            gl.glTexParameteri((int)target, (int)pname, (int)param);
        }

        public static void TexParameter(TextureParameterTarget target, TextureParameterName pname, TextureShadow shadowParam)
        {
            gl.glTexParameteri((int)target, (int)pname, (int)shadowParam);
        }

        public static void TexParameteriv(TextureArrayParameterTarget target, TextureParameterName pname, int[] parameters)
        {
            gl.glTexParameteriv((int)target, (int)pname, parameters);
        }
        #endregion TexParameter

        #region TexSubImage
        public static void TexSubImage1D(int level, int xoffset, int width, TexturePixelFormat format, PixelComponentType type, object[] pixels)
        {
            gl.glTexSubImage1D((int)Texture1DTarget.Texture1d, level, xoffset, width, (int)format, (int)type, pixels);
        }

        public static void TexSubImage2D(Texture2DTarget target, int level, int xoffset, int yoffset, int width, int height, TexturePixelFormat format, PixelComponentType type, IntPtr pixels)
        {
            gl.glTexSubImage2D((int)target, level, xoffset, yoffset, width, height, (int)format, (int)type, pixels);
        }

        public static void TexSubImage2D(Texture2DTarget target, int level, int xoffset, int yoffset, int width, int height, TexturePixelFormat format, PixelComponentType type, byte[] pixels)
        {
            gl.glTexSubImage2D((int)target, level, xoffset, yoffset, width, height, (int)format, (int)type, pixels);
        }
        #endregion

        #region Vertex2
        public static void Vertex(float2 aVector)
        {
            gl.glVertex2f(aVector.x, aVector.y);
        }

        public static void Vertex2(double x, double y)
        {
            gl.glVertex2d(x, y);
        }

        public static void Vertex2(double[] v)
        {
            gl.glVertex2dv(v);
        }

        public static void Vertex(float x, float y)
        {
            gl.glVertex2f(x, y);
        }

        public static void Vertex2(float[] v)
        {
            gl.glVertex2fv(v);
        }

        public static void Vertex2(int x, int y)
        {
            gl.glVertex2i(x, y);
        }

        public static void Vertex2(int[] v)
        {
            gl.glVertex2iv(v);
        }

        public static void Vertex2(short x, short y)
        {
            gl.glVertex2s(x, y);
        }

        public static void Vertex2(short[] v)
        {
            gl.glVertex2sv(v);
        }
        #endregion

        #region Vertex3
        public static void Vertex(Vector3f aVector)
        {
            gl.glVertex3fv((float[])aVector);
        }

        public static void Vertex3(double x, double y, double z)
        {
            gl.glVertex3d(x, y, z);
        }

        public static void Vertex3(double[] v)
        {
            gl.glVertex3dv(v);
        }

        public static void Vertex3(float x, float y, float z)
        {
            gl.glVertex3f(x, y, z);
        }

        public static void Vertex3(float[] v)
        {
            gl.glVertex3fv(v);
        }

        public static void Vertex3(int x, int y, int z)
        {
            gl.glVertex3i(x, y, z);
        }

        public static void Vertex3(int[] v)
        {
            gl.glVertex3iv(v);
        }

        public static void Vertex3(short x, short y, short z)
        {
            gl.glVertex3s(x, y, z);
        }

        public static void Vertex3(short[] v)
        {
            gl.glVertex3sv(v);
        }
        #endregion Vertex3

        #region Vertex4
        public static void Vertex(float4 vec)
        {
            gl.glVertex4d(vec.x, vec.y, vec.z, vec.w);
        }

        public static void Vertex4(double x, double y, double z, double w)
        {
            gl.glVertex4d(x, y, z, w);
        }

        public static void Vertex4(double[] v)
        {
            gl.glVertex4dv(v);
        }

        public static void Vertex4(float x, float y, float z, float w)
        {
            gl.glVertex4f(x, y, z, w);
        }

        public static void Vertex4(float[] v)
        {
            gl.glVertex4fv(v);
        }

        public static void Vertex4(int x, int y, int z, int w)
        {
            gl.glVertex4i(x, y, z, w);
        }

        public static void Vertex4(int[] v)
        {
            gl.glVertex4iv(v);
        }

        public static void Vertex4(short x, short y, short z, short w)
        {
            gl.glVertex4s(x, y, z, w);
        }

        public static void Vertex4(short[] v)
        {
            gl.glVertex4sv(v);
        }
        #endregion

        #region VertexPointer
        public static void VertexPointer(int size, VertexPointerType type, int stride, short[,] pointer)
        {
            gl.glVertexPointer(size, (int)type, stride, pointer);
        }

        public static void VertexPointer(int size, VertexPointerType type, int stride, int[,] pointer)
        {
            gl.glVertexPointer(size, (int)type, stride, pointer);
        }

        public static void VertexPointer(int size, VertexPointerType type, int stride, float[,] pointer)
        {
            gl.glVertexPointer(size, (int)type, stride, pointer);
        }

        public static void VertexPointer(int size, VertexPointerType type, int stride, double[,] pointer)
        {
            gl.glVertexPointer(size, (int)type, stride, pointer);
        }

        public static void VertexPointer(int size, VertexPointerType type, int stride, IntPtr pointer)
        {
            gl.glVertexPointer(size, (int)type, stride, pointer);
        }
        #endregion
    }
}
