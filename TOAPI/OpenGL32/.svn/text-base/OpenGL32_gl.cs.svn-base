using System;
using System.Runtime.InteropServices;

namespace TOAPI.OpenGL
{
    #region GLDataTypes
    // These define the mapping between OpenGL data types
    // and the .net equivalents.
    using GLsizei = System.Int32;
    using GLsizeiptr = System.IntPtr;
    using GLintptr = System.IntPtr;
    using GLenum = System.Int32;
    using GLboolean = System.Int32;
    using GLbitfield = System.Int32;
    using GLvoid = System.Object;
    using GLchar = System.Char;
    using GLbyte = System.Byte;
    using GLubyte = System.Byte;
    using GLshort = System.Int16;
    using GLushort = System.Int16;
    using GLint = System.Int32;
    using GLuint = System.Int32;
    using GLfloat = System.Single;
    using GLclampf = System.Single;
    using GLdouble = System.Double;
    using GLclampd = System.Double;
    using GLstring = System.String;
    using GLsizeiptrARB = System.IntPtr;
    using GLintptrARB = System.IntPtr;
    using GLhandleARB = System.Int32;
    using GLhalfARB = System.Int16;
    using GLhalfNV = System.Int16;
    using GLcharARB = System.Char;
    using GLint64EXT = System.Int64;
    using GLuint64EXT = System.Int64;
    using GLint64 = System.Int64;
    using GLuint64 = System.Int64;
    #endregion

    // The original list of functions was derived by doing a dumpbin.exe /extract opengl32.dll
    // From this list, functions are filled in with interop signatures as they 
    // are needed.

    public partial class gl
    {
        [DllImport("opengl32.dll", EntryPoint = "glAccum")]
        public static extern void glAccum(int op, float value);

        [DllImport("opengl32.dll", EntryPoint = "glAlphaFunc")]
        public static extern void glAlphaFunc(int func, float @ref);

        [DllImport("opengl32.dll", EntryPoint = "glAreTexturesResident")]
        public static extern Boolean glAreTexturesResident(GLsizei n, [MarshalAs(UnmanagedType.LPArray)]uint[] textures, [MarshalAs(UnmanagedType.LPArray)]byte[] residences);

        [DllImport("opengl32.dll", EntryPoint = "glAreTexturesResident")]
        public static extern Boolean glAreTexturesResident(GLsizei n, [MarshalAs(UnmanagedType.LPArray)]uint[] textures, [MarshalAs(UnmanagedType.LPArray)]Boolean[] residences);

        [DllImport("opengl32.dll", EntryPoint = "glArrayElement")]
        public static extern void glArrayElement(int i);

        [DllImport("opengl32.dll", EntryPoint = "glBegin")]
        public static extern void glBegin(int mode);

        [DllImport("opengl32.dll", EntryPoint = "glBindTexture")]
        public static extern void glBindTexture(int target, uint texture);

        [DllImport("opengl32.dll", EntryPoint = "glBitmap")]
        public static extern void glBitmap(int width, int height, float xorig, float yorig, float xmove, float ymove, [MarshalAs(UnmanagedType.LPArray)]byte[] bitmap);

        [DllImport("opengl32.dll", EntryPoint = "glBlendFunc")]
        public static extern void glBlendFunc(int sfactor, int dfactor);

        [DllImport("opengl32.dll", EntryPoint = "glCallList")]
        public static extern void glCallList(uint list);

        [DllImport("opengl32.dll", EntryPoint = "glCallLists")]
        public static extern void glCallLists(int n, int type, [MarshalAs(UnmanagedType.LPArray)]byte[] lists);

        [DllImport("opengl32.dll", EntryPoint = "glCallLists")]
        public static extern void glCallLists(int n, int type, [MarshalAs(UnmanagedType.LPArray)]sbyte[] lists);

        [DllImport("opengl32.dll", EntryPoint = "glCallLists")]
        public static extern void glCallLists(int n, int type, [MarshalAs(UnmanagedType.LPArray)]short[] lists);

        [DllImport("opengl32.dll", EntryPoint = "glCallLists")]
        public static extern void glCallLists(int n, int type, [MarshalAs(UnmanagedType.LPArray)]ushort[] lists);

        [DllImport("opengl32.dll", EntryPoint = "glCallLists")]
        public static extern void glCallLists(int n, int type, [MarshalAs(UnmanagedType.LPArray)]int[] lists);

        [DllImport("opengl32.dll", EntryPoint = "glCallLists")]
        public static extern void glCallLists(int n, int type, [MarshalAs(UnmanagedType.LPArray)]uint[] lists);

        [DllImport("opengl32.dll", EntryPoint = "glCallLists")]
        public static extern void glCallLists(int n, int type, [MarshalAs(UnmanagedType.LPArray)]float[] lists);

        [DllImport("opengl32.dll", EntryPoint = "glClear")]
        public static extern void glClear(int mask);

        [DllImport("opengl32.dll", EntryPoint = "glClearAccum")]
        public static extern void glClearAccum(float red, float green, float blue, float alpha);

        [DllImport("opengl32.dll", EntryPoint = "glClearColor")]
        public static extern void glClearColor(float red, float green, float blue, float alpha);

        [DllImport("opengl32.dll", EntryPoint = "glClearDepth")]
        public static extern void glClearDepth(double depth);

        [DllImport("opengl32.dll", EntryPoint = "glClearIndex")]
        public static extern void glClearIndex(float c);

        [DllImport("opengl32.dll", EntryPoint = "glClearStencil")]
        public static extern void glClearStencil(int s);

        [DllImport("opengl32.dll", EntryPoint = "glClipPlane")]
        public static extern void glClipPlane(int plane, [MarshalAs(UnmanagedType.LPArray)]double[] equation);

        //24   17 0000276C glColor3b
        [DllImport("opengl32.dll", EntryPoint = "glColor3b")]
        public static extern void glColor3b(sbyte red, sbyte green, sbyte blue);

        //25   18 00002778 glColor3bv
        [DllImport("opengl32.dll", EntryPoint = "glColor3bv")]
        public static extern void glColor3bv([MarshalAs(UnmanagedType.LPArray)]sbyte[] v);

        //26   19 00002784 glColor3d
        [DllImport("opengl32.dll", EntryPoint = "glColor3d")]
        public static extern void glColor3d(double red, double green, double blue);

        //27   1A 00002790 glColor3dv
        [DllImport("opengl32.dll", EntryPoint = "glColor3dv")]
        public static extern void glColor3dv([MarshalAs(UnmanagedType.LPArray)]double[] v);

        //28   1B 0000279C glColor3f
        [DllImport("opengl32.dll", EntryPoint = "glColor3f")]
        public static extern void glColor3f(float red, float green, float blue);

        //29   1C 000027A8 glColor3fv
        [DllImport("opengl32.dll", EntryPoint = "glColor3fv")]
        public static extern void glColor3fv([MarshalAs(UnmanagedType.LPArray)]float[] v);

        //30   1D 000027B4 glColor3i
        [DllImport("opengl32.dll", EntryPoint = "glColor3i")]
        public static extern void glColor3i(int red, int green, int blue);

        //31   1E 000027C0 glColor3iv
        [DllImport("opengl32.dll", EntryPoint = "glColor3iv")]
        public static extern void glColor3iv([MarshalAs(UnmanagedType.LPArray)]int[] v);

        //32   1F 000027CC glColor3s
        [DllImport("opengl32.dll", EntryPoint = "glColor3s")]
        public static extern void glColor3s(short red, short green, short blue);

        //33   20 000027D8 glColor3sv
        [DllImport("opengl32.dll", EntryPoint = "glColor3sv")]
        public static extern void glColor3sv([MarshalAs(UnmanagedType.LPArray)]short[] v);

        //34   21 000027E4 glColor3ub
        [DllImport("opengl32.dll", EntryPoint = "glColor3ub")]
        public static extern void glColor3ub(byte red, byte green, byte blue);

        //35   22 000027F0 glColor3ubv
        [DllImport("opengl32.dll", EntryPoint = "glColor3ubv")]
        public static extern void glColor3ubv([MarshalAs(UnmanagedType.LPArray)]byte[] v);

        //36   23 000027FC glColor3ui
        [DllImport("opengl32.dll", EntryPoint = "glColor3ui")]
        public static extern void glColor3ui(uint red, uint green, uint blue);

        //37   24 00002808 glColor3uiv
        [DllImport("opengl32.dll", EntryPoint = "glColor3uiv")]
        public static extern void glColor3uiv([MarshalAs(UnmanagedType.LPArray)]uint[] v);

        //38   25 00002814 glColor3us
        [DllImport("opengl32.dll", EntryPoint = "glColor3us")]
        public static extern void glColor3us(ushort red, ushort green, ushort blue);

        //39   26 00002820 glColor3usv
        [DllImport("opengl32.dll", EntryPoint = "glColor3usv")]
        public static extern void glColor3usv([MarshalAs(UnmanagedType.LPArray)]ushort[] v);

        //40   27 0000282C glColor4b
        [DllImport("opengl32.dll", EntryPoint = "glColor4b")]
        public static extern void glColor4b(sbyte red, sbyte green, sbyte blue, sbyte alpha);


        //41   28 00002838 glColor4bv
        [DllImport("opengl32.dll", EntryPoint = "glColor4bv")]
        public static extern void glColor4bv([MarshalAs(UnmanagedType.LPArray)]sbyte[] v);

        //42   29 00002844 glColor4d
        [DllImport("opengl32.dll", EntryPoint = "glColor4d")]
        public static extern void glColor4d(double red, double green, double blue, double alpha);

        //43   2A 00002850 glColor4dv
        [DllImport("opengl32.dll", EntryPoint = "glColor4dv")]
        public static extern void glColor4dv([MarshalAs(UnmanagedType.LPArray)]double[] v);

        //44   2B 0000285C glColor4f
        [DllImport("opengl32.dll", EntryPoint = "glColor4f")]
        public static extern void glColor4f(float red, float green, float blue, float alpha);

        //45   2C 00002868 glColor4fv
        [DllImport("opengl32.dll", EntryPoint = "glColor4fv")]
        public static extern void glColor4fv([MarshalAs(UnmanagedType.LPArray)]float[] v);

        //46   2D 00002874 glColor4i
        [DllImport("opengl32.dll", EntryPoint = "glColor4i")]
        public static extern void glColor4i(int red, int green, int blue, int alpha);

        //47   2E 00002880 glColor4iv
        [DllImport("opengl32.dll", EntryPoint = "glColor4iv")]
        public static extern void glColor4iv([MarshalAs(UnmanagedType.LPArray)]int[] v);

        //48   2F 0000288C glColor4s
        [DllImport("opengl32.dll", EntryPoint = "glColor4s")]
        public static extern void glColor4s(short red, short green, short blue, short alpha);

        //49   30 00002898 glColor4sv
        [DllImport("opengl32.dll", EntryPoint = "glColor4sv")]
        public static extern void glColor4sv([MarshalAs(UnmanagedType.LPArray)]short[] v);

        //50   31 000028A4 glColor4ub
        [DllImport("opengl32.dll", EntryPoint = "glColor4ub")]
        public static extern void glColor4ub(byte red, byte green, byte blue, byte alpha);

        //51   32 000028B0 glColor4ubv
        [DllImport("opengl32.dll", EntryPoint = "glColor4ubv")]
        public static extern void glColor4ubv([MarshalAs(UnmanagedType.LPArray)]byte[] v);

        //52   33 000028BC glColor4ui
        [DllImport("opengl32.dll", EntryPoint = "glColor4ui")]
        public static extern void glColor4ui(uint red, uint green, uint blue, uint alpha);

        //53   34 000028C8 glColor4uiv
        [DllImport("opengl32.dll", EntryPoint = "glColor4uiv")]
        public static extern void glColor4uiv([MarshalAs(UnmanagedType.LPArray)]uint[] v);

        //54   35 000028D4 glColor4us
        [DllImport("opengl32.dll", EntryPoint = "glColor4us")]
        public static extern void glColor4us(ushort red, ushort green, ushort blue, ushort alpha);

        //55   36 000028E0 glColor4usv
        [DllImport("opengl32.dll", EntryPoint = "glColor4usv")]
        public static extern void glColor4usv([MarshalAs(UnmanagedType.LPArray)]ushort[] v);

        [DllImport("opengl32.dll", EntryPoint = "glColorMask")]
        public static extern void glColorMask(byte red, byte green, byte blue, byte alpha);

        [DllImport("opengl32.dll", EntryPoint = "glColorMaterial")]
        public static extern void glColorMaterial(int face, int mode);

        #region void glColorPointer...
        [DllImport("opengl32.dll", EntryPoint = "glColorPointer")]
        public static extern void glColorPointer(int size, int type, int stride, IntPtr pointer);

        [DllImport("opengl32.dll", EntryPoint = "glColorPointer")]
        public static extern void glColorPointer(int size, int type, int stride, int offset);

        public static void glColorPointer(int size, int type, int stride, object pointer)
        {
            GCHandle h0 = GCHandle.Alloc(pointer, GCHandleType.Pinned);
            try
            {
                glColorPointer(size, type, stride, h0.AddrOfPinnedObject());
            }
            finally
            {
                h0.Free();
            }
        }
        #endregion

        [DllImport("opengl32.dll", EntryPoint = "glCopyPixels")]
        public static extern void glCopyPixels(int x, int y, int width, int height, int type);

        [DllImport("opengl32.dll", EntryPoint = "glCopyTexImage1D")]
        public static extern void glCopyTexImage1D(int target, int level, int internalFormat, int x, int y, int width, int border);

        [DllImport("opengl32.dll", EntryPoint = "glCopyTexImage2D")]
        public static extern void glCopyTexImage2D(int target, int level, int internalFormat, int x, int y, int width, int height, int border);

        [DllImport("opengl32.dll", EntryPoint = "glCopyTexSubImage1D")]
        public static extern void glCopyTexSubImage1D(int target, int level, int xoffset, int x, int y, int width);

        [DllImport("opengl32.dll", EntryPoint = "glCopyTexSubImage2D")]
        public static extern void glCopyTexSubImage2D(int target, int level, int xoffset, int yoffset, int x, int y, int width, int height);

        [DllImport("opengl32.dll", EntryPoint = "glCullFace")]
        public static extern void glCullFace(int mode);

        [DllImport("opengl32.dll", EntryPoint = "glDeleteLists")]
        public static extern void glDeleteLists(uint list, int range);

        [DllImport("opengl32.dll", EntryPoint = "glDeleteTextures")]
        public static extern void glDeleteTextures(int n, [MarshalAs(UnmanagedType.LPArray)]uint[] textures);

        [DllImport("opengl32.dll", EntryPoint = "glDepthFunc")]
        public static extern void glDepthFunc(int func);

        [DllImport("opengl32.dll", EntryPoint = "glDepthMask")]
        public static extern void glDepthMask(Boolean flag);

        [DllImport("opengl32.dll", EntryPoint = "glDepthRange")]
        public static extern void glDepthRange(double zNear, double zFar);

        [DllImport("opengl32.dll", EntryPoint = "glDisable")]
        public static extern void glDisable(int cap);

        [DllImport("opengl32.dll", EntryPoint = "glDisableClientState")]
        public static extern void glDisableClientState(int array);

        [DllImport("opengl32.dll", EntryPoint = "glDrawArrays")]
        public static extern void glDrawArrays(int mode, int first, int count);

        [DllImport("opengl32.dll", EntryPoint = "glDrawBuffer")]
        public static extern void glDrawBuffer(int mode);

        [DllImport("opengl32.dll", EntryPoint = "glDrawElements")]
        public static extern void glDrawElements(int mode, int count, int type, [MarshalAs(UnmanagedType.LPArray)]byte[] indices);

        [DllImport("opengl32.dll", EntryPoint = "glDrawElements")]
        public static extern void glDrawElements(int mode, int count, int type, [MarshalAs(UnmanagedType.LPArray)]ushort[] indices);

        [DllImport("opengl32.dll", EntryPoint = "glDrawElements")]
        public static extern void glDrawElements(int mode, int count, int type, [MarshalAs(UnmanagedType.LPArray)]uint[] indices);

        [DllImport("opengl32.dll", EntryPoint = "glDrawElements")]
        public static extern void glDrawElements(int mode, int count, int type, [MarshalAs(UnmanagedType.LPArray)]int[] indices);

        [DllImport("opengl32.dll", EntryPoint = "glDrawElements")]
        public static extern void glDrawElements(int mode, int count, int type, IntPtr pointer);

        [DllImport("opengl32.dll", EntryPoint = "glDrawPixels")]
        public static extern void glDrawPixels(int width, int height, int format, int type, IntPtr pixels);

        [DllImport("opengl32.dll", EntryPoint = "glDrawPixels")]
        public static extern void glDrawPixels(int width, int height, int format, int type, [MarshalAs(UnmanagedType.LPArray)]byte[] pixels);

        [DllImport("opengl32.dll", EntryPoint = "glDrawPixels")]
        public static extern void glDrawPixels(int width, int height, int format, int type, [MarshalAs(UnmanagedType.LPArray)]sbyte[] pixels);

        [DllImport("opengl32.dll", EntryPoint = "glDrawPixels")]
        public static extern void glDrawPixels(int width, int height, int format, int type, [MarshalAs(UnmanagedType.LPArray)]ushort[] pixels);

        [DllImport("opengl32.dll", EntryPoint = "glDrawPixels")]
        public static extern void glDrawPixels(int width, int height, int format, int type, [MarshalAs(UnmanagedType.LPArray)]short[] pixels);

        [DllImport("opengl32.dll", EntryPoint = "glDrawPixels")]
        public static extern void glDrawPixels(int width, int height, int format, int type, [MarshalAs(UnmanagedType.LPArray)]uint[] pixels);

        [DllImport("opengl32.dll", EntryPoint = "glDrawPixels")]
        public static extern void glDrawPixels(int width, int height, int format, int type, [MarshalAs(UnmanagedType.LPArray)]int[] pixels);

        [DllImport("opengl32.dll", EntryPoint = "glDrawPixels")]
        public static extern void glDrawPixels(int width, int height, int format, int type, [MarshalAs(UnmanagedType.LPArray)]float[] pixels);

        [DllImport("opengl32.dll", EntryPoint = "glEdgeFlag")]
        public static extern void glEdgeFlag(Boolean flag);

        [DllImport("opengl32.dll", EntryPoint = "glEdgeFlagPointer")]
        public static extern void glEdgeFlagPointer(int stride, [MarshalAs(UnmanagedType.LPArray)]Boolean[] pointer);

        [DllImport("opengl32.dll", EntryPoint = "glEdgeFlagPointer")]
        public static extern void glEdgeFlagPointer(int stride, [MarshalAs(UnmanagedType.LPArray)]byte[] pointer);

        [DllImport("opengl32.dll", EntryPoint = "glEdgeFlagPointer")]
        public static extern void glEdgeFlagPointer(int stride, IntPtr pointer);

        [DllImport("opengl32.dll", EntryPoint = "glEdgeFlagv")]
        public static extern void glEdgeFlagv(ref Boolean flag);

        [DllImport("opengl32.dll", EntryPoint = "glEnable")]
        public static extern void glEnable(int cap);
        //public static extern void glEnable(GLEnable cap);

        [DllImport("opengl32.dll", EntryPoint = "glEnableClientState")]
        public static extern void glEnableClientState(int array);

        [DllImport("opengl32.dll", EntryPoint = "glEnd")]
        public static extern void glEnd();

        [DllImport("opengl32.dll", EntryPoint = "glEndList")]
        public static extern void glEndList();

        [DllImport("opengl32.dll", EntryPoint = "glEvalCoord1d")]
        public static extern void glEvalCoord1d(double u);

        [DllImport("opengl32.dll", EntryPoint = "glEvalCoord1dv")]
        public static extern void glEvalCoord1dv([MarshalAs(UnmanagedType.LPArray)]double[] u);

        [DllImport("opengl32.dll", EntryPoint = "glEvalCoord1f")]
        public static extern void glEvalCoord1f(float u);

        [DllImport("opengl32.dll", EntryPoint = "glEvalCoord1fv")]
        public static extern void glEvalCoord1fv([MarshalAs(UnmanagedType.LPArray)]float[] u);

        [DllImport("opengl32.dll", EntryPoint = "glEvalCoord2d")]
        public static extern void glEvalCoord2d(double u, double v);

        [DllImport("opengl32.dll", EntryPoint = "glEvalCoord2dv")]
        public static extern void glEvalCoord2dv([MarshalAs(UnmanagedType.LPArray)]double[] u);

        [DllImport("opengl32.dll", EntryPoint = "glEvalCoord2f")]
        public static extern void glEvalCoord2f(float u, float v);

        [DllImport("opengl32.dll", EntryPoint = "glEvalCoord2fv")]
        public static extern void glEvalCoord2fv([MarshalAs(UnmanagedType.LPArray)]float[] u);

        [DllImport("opengl32.dll", EntryPoint = "glEvalMesh1")]
        public static extern void glEvalMesh1(int mode, int i1, int i2);

        [DllImport("opengl32.dll", EntryPoint = "glEvalMesh2")]
        public static extern void glEvalMesh2(int mode, int i1, int i2, int j1, int j2);

        [DllImport("opengl32.dll", EntryPoint = "glEvalPoint1")]
        public static extern void glEvalPoint1(int i);

        [DllImport("opengl32.dll", EntryPoint = "glEvalPoint2")]
        public static extern void glEvalPoint2(int i, int j);

        [DllImport("opengl32.dll", EntryPoint = "glFeedbackBuffer")]
        public static extern void glFeedbackBuffer(int size, int type, [MarshalAs(UnmanagedType.LPArray)]float[] buffer);

        [DllImport("opengl32.dll", EntryPoint = "glFinish")]
        public static extern void glFinish();

        [DllImport("opengl32.dll", EntryPoint = "glFlush")]
        public static extern void glFlush();

        [DllImport("opengl32.dll", EntryPoint = "glFogf")]
        public static extern void glFogf(int pname, float param);

        [DllImport("opengl32.dll", EntryPoint = "glFogfv")]
        public static extern void glFogfv(int pname, [MarshalAs(UnmanagedType.LPArray)]float[] parameters);

        [DllImport("opengl32.dll", EntryPoint = "glFogi")]
        public static extern void glFogi(int pname, int param);

        [DllImport("opengl32.dll", EntryPoint = "glFogiv")]
        public static extern void glFogiv(int pname, [MarshalAs(UnmanagedType.LPArray)]int[] parameters);

        [DllImport("opengl32.dll", EntryPoint = "glFrontFace")]
        public static extern void glFrontFace(int mode);

        [DllImport("opengl32.dll", EntryPoint = "glFrustum")]
        public static extern void glFrustum(double left, double right, double bottom, double top, double zNear, double zFar);

        [DllImport("opengl32.dll", EntryPoint = "glGenLists")]
        public static extern uint glGenLists(int range);

        [DllImport("opengl32.dll", EntryPoint = "glGenTextures")]
        public static extern void glGenTextures(int n, [MarshalAs(UnmanagedType.LPArray)]uint[] textures);

        [DllImport("opengl32.dll", EntryPoint = "glGetBooleanv")]
        public static extern void glGetBooleanv(int pname, [MarshalAs(UnmanagedType.LPArray)]byte[] parameters);

        [DllImport("opengl32.dll", EntryPoint = "glGetBooleanv")]
        public static extern void glGetBooleanv(int pname, [MarshalAs(UnmanagedType.LPArray)]int[] parameters);

        [DllImport("opengl32.dll", EntryPoint = "glGetClipPlane")]
        public static extern void glGetClipPlane(int plane, [MarshalAs(UnmanagedType.LPArray)]double[] equation);

        [DllImport("opengl32.dll", EntryPoint = "glGetDoublev")]
        public static extern void glGetDoublev(int pname, [MarshalAs(UnmanagedType.LPArray)]double[] parameters);

        [DllImport("opengl32.dll", EntryPoint = "glGetError")]
        public static extern int glGetError();

        [DllImport("opengl32.dll", EntryPoint = "glGetFloatv")]
        public static extern void glGetFloatv(int pname, [MarshalAs(UnmanagedType.LPArray)]float[] parameters);

        [DllImport("opengl32.dll", EntryPoint = "glGetIntegerv")]
        public static extern void glGetIntegerv(int pname, [MarshalAs(UnmanagedType.LPArray)]int[] parameters);

        [DllImport("opengl32.dll", EntryPoint = "glGetLightfv")]
        public static extern void glGetLightfv(int light, int pname, [MarshalAs(UnmanagedType.LPArray)]float[] parameters);

        [DllImport("opengl32.dll", EntryPoint = "glGetLightiv")]
        public static extern void glGetLightiv(int light, int pname, [MarshalAs(UnmanagedType.LPArray)]int[] parameters);

        [DllImport("opengl32.dll", EntryPoint = "glGetMapdv")]
        public static extern void glGetMapdv(int target, int query, [MarshalAs(UnmanagedType.LPArray)]double[] v);

        [DllImport("opengl32.dll", EntryPoint = "glGetMapfv")]
        public static extern void glGetMapfv(int target, int query, [MarshalAs(UnmanagedType.LPArray)]float[] v);

        [DllImport("opengl32.dll", EntryPoint = "glGetMapiv")]
        public static extern void glGetMapiv(int target, int query, [MarshalAs(UnmanagedType.LPArray)]int[] v);

        [DllImport("opengl32.dll", EntryPoint = "glGetMaterialfv")]
        public static extern void glGetMaterialfv(int face, int pname, [MarshalAs(UnmanagedType.LPArray)]float[] parameters);

        [DllImport("opengl32.dll", EntryPoint = "glGetMaterialiv")]
        public static extern void glGetMaterialiv(int face, int pname, [MarshalAs(UnmanagedType.LPArray)]int[] parameters);

        [DllImport("opengl32.dll", EntryPoint = "glGetPixelMapfv")]
        public static extern void glGetPixelMapfv(int map, [MarshalAs(UnmanagedType.LPArray)]float[] values);

        [DllImport("opengl32.dll", EntryPoint = "glGetPixelMapuiv")]
        public static extern void glGetPixelMapuiv(int map, [MarshalAs(UnmanagedType.LPArray)]uint[] values);

        [DllImport("opengl32.dll", EntryPoint = "glGetPixelMapusv")]
        public static extern void glGetPixelMapusv(int map, [MarshalAs(UnmanagedType.LPArray)]ushort[] values);

        [DllImport("opengl32.dll", EntryPoint = "glGetPointerv")]
        public static extern void glGetPointerv(int pname, ref IntPtr parameters);

        [DllImport("opengl32.dll", EntryPoint = "glGetPolygonStipple")]
        public static extern void glGetPolygonStipple([MarshalAs(UnmanagedType.LPArray)]byte[] mask);

        [DllImport("opengl32.dll", EntryPoint = "glGetString")]
        public static extern IntPtr glGetString(int name);

        [DllImport("opengl32.dll", EntryPoint = "glGetTexEnvfv")]
        public static extern void glGetTexEnvfv(int target, int pname, [MarshalAs(UnmanagedType.LPArray)]float[] parameters);

        [DllImport("opengl32.dll", EntryPoint = "glGetTexEnviv")]
        public static extern void glGetTexEnviv(int target, int pname, [MarshalAs(UnmanagedType.LPArray)]int[] parameters);

        [DllImport("opengl32.dll", EntryPoint = "glGetTexGendv")]
        public static extern void glGetTexGendv(int coord, int pname, [MarshalAs(UnmanagedType.LPArray)]double[] parameters);

        [DllImport("opengl32.dll", EntryPoint = "glGetTexGenfv")]
        public static extern void glGetTexGenfv(int coord, int pname, [MarshalAs(UnmanagedType.LPArray)]float[] parameters);

        [DllImport("opengl32.dll", EntryPoint = "glGetTexGeniv")]
        public static extern void glGetTexGeniv(int coord, int pname, [MarshalAs(UnmanagedType.LPArray)]int[] parameters);

        [DllImport("opengl32.dll", EntryPoint = "glGetTexImage")]
        public static extern void glGetTexImage(int target, int level, int format, int type, [MarshalAs(UnmanagedType.LPArray)]byte[] pixels);

        [DllImport("opengl32.dll", EntryPoint = "glGetTexImage")]
        public static extern void glGetTexImage(int target, int level, int format, int type, [MarshalAs(UnmanagedType.LPArray)]sbyte[] pixels);

        [DllImport("opengl32.dll", EntryPoint = "glGetTexImage")]
        public static extern void glGetTexImage(int target, int level, int format, int type, [MarshalAs(UnmanagedType.LPArray)]short[] pixels);

        [DllImport("opengl32.dll", EntryPoint = "glGetTexImage")]
        public static extern void glGetTexImage(int target, int level, int format, int type, [MarshalAs(UnmanagedType.LPArray)]ushort[] pixels);

        [DllImport("opengl32.dll", EntryPoint = "glGetTexImage")]
        public static extern void glGetTexImage(int target, int level, int format, int type, [MarshalAs(UnmanagedType.LPArray)]int[] pixels);

        [DllImport("opengl32.dll", EntryPoint = "glGetTexImage")]
        public static extern void glGetTexImage(int target, int level, int format, int type, [MarshalAs(UnmanagedType.LPArray)]uint[] pixels);

        [DllImport("opengl32.dll", EntryPoint = "glGetTexImage")]
        public static extern void glGetTexImage(int target, int level, int format, int type, [MarshalAs(UnmanagedType.LPArray)]float[] pixels);

        [DllImport("opengl32.dll", EntryPoint = "glGetTexLevelParameterfv")]
        public static extern void glGetTexLevelParameterfv(int target, int level, int pname, [MarshalAs(UnmanagedType.LPArray)]float[] parameters);

        [DllImport("opengl32.dll", EntryPoint = "glGetTexLevelParameteriv")]
        public static extern void glGetTexLevelParameteriv(int target, int level, int pname, [MarshalAs(UnmanagedType.LPArray)]int[] parameters);

        [DllImport("opengl32.dll", EntryPoint = "glGetTexParameterfv")]
        public static extern void glGetTexParameterfv(int target, int pname, [MarshalAs(UnmanagedType.LPArray)]float[] parameters);

        [DllImport("opengl32.dll", EntryPoint = "glGetTexParameteriv")]
        public static extern void glGetTexParameteriv(int target, int pname, [MarshalAs(UnmanagedType.LPArray)]int[] parameters);

        [DllImport("opengl32.dll", EntryPoint = "glHint")]
        public static extern void glHint(int target, int mode);

        [DllImport("opengl32.dll", EntryPoint = "glIndexMask")]
        public static extern void glIndexMask(uint mask);

        [DllImport("opengl32.dll", EntryPoint = "glIndexPointer")]
        public static extern void glIndexPointer(int type, int stride, [MarshalAs(UnmanagedType.LPArray)]short[] pointer);

        [DllImport("opengl32.dll", EntryPoint = "glIndexPointer")]
        public static extern void glIndexPointer(int type, int stride, [MarshalAs(UnmanagedType.LPArray)]int[] pointer);

        [DllImport("opengl32.dll", EntryPoint = "glIndexPointer")]
        public static extern void glIndexPointer(int type, int stride, [MarshalAs(UnmanagedType.LPArray)]float[] pointer);

        [DllImport("opengl32.dll", EntryPoint = "glIndexPointer")]
        public static extern void glIndexPointer(int type, int stride, [MarshalAs(UnmanagedType.LPArray)]double[] pointer);

        [DllImport("opengl32.dll", EntryPoint = "glIndexPointer")]
        public static extern void glIndexPointer(int type, int stride, IntPtr pointer);

        [DllImport("opengl32.dll", EntryPoint = "glIndexd")]
        public static extern void glIndexd(double c);

        [DllImport("opengl32.dll", EntryPoint = "glIndexdv")]
        public static extern void glIndexdv([MarshalAs(UnmanagedType.LPArray)]double[] c);

        [DllImport("opengl32.dll", EntryPoint = "glIndexf")]
        public static extern void glIndexf(float c);

        [DllImport("opengl32.dll", EntryPoint = "glIndexfv")]
        public static extern void glIndexfv([MarshalAs(UnmanagedType.LPArray)]float[] c);

        [DllImport("opengl32.dll", EntryPoint = "glIndexi")]
        public static extern void glIndexi(int c);

        [DllImport("opengl32.dll", EntryPoint = "glIndexiv")]
        public static extern void glIndexiv([MarshalAs(UnmanagedType.LPArray)]int[] c);

        [DllImport("opengl32.dll", EntryPoint = "glIndexs")]
        public static extern void glIndexs(short c);

        [DllImport("opengl32.dll", EntryPoint = "glIndexsv")]
        public static extern void glIndexsv([MarshalAs(UnmanagedType.LPArray)]short[] c);

        [DllImport("opengl32.dll", EntryPoint = "glIndexub")]
        public static extern void glIndexub(byte c);

        [DllImport("opengl32.dll", EntryPoint = "glIndexubv")]
        public static extern void glIndexubv([MarshalAs(UnmanagedType.LPArray)]byte[] c);

        [DllImport("opengl32.dll", EntryPoint = "glInitNames")]
        public static extern void glInitNames();

        [DllImport("opengl32.dll", EntryPoint = "glInterleavedArrays")]
        public static extern void glInterleavedArrays(int format, int stride, [MarshalAs(UnmanagedType.LPArray)]byte[] pointer);

        [DllImport("opengl32.dll", EntryPoint = "glInterleavedArrays")]
        public static extern void glInterleavedArrays(int format, int stride, [MarshalAs(UnmanagedType.LPArray)]float[] pointer);

        [DllImport("opengl32.dll", EntryPoint = "glIsEnabled")]
        public static extern Boolean glIsEnabled(int cap);

        [DllImport("opengl32.dll", EntryPoint = "glIsList")]
        public static extern Boolean glIsList(uint list);

        [DllImport("opengl32.dll", EntryPoint = "glIsTexture")]
        public static extern Boolean glIsTexture(uint texture);

        [DllImport("opengl32.dll", EntryPoint = "glLightModelf")]
        public static extern void glLightModelf(int pname, float param);

        [DllImport("opengl32.dll", EntryPoint = "glLightModelfv")]
        public static extern void glLightModelfv(int pname, [MarshalAs(UnmanagedType.LPArray)]float[] parameters);

        [DllImport("opengl32.dll", EntryPoint = "glLightModeli")]
        public static extern void glLightModeli(int pname, int param);

        [DllImport("opengl32.dll", EntryPoint = "glLightModeliv")]
        public static extern void glLightModeliv(int pname, [MarshalAs(UnmanagedType.LPArray)]int[] parameters);

        #region void glLight...
        [DllImport("opengl32.dll", EntryPoint = "glLightf")]
        public static extern void glLightf(int light, int pname, float param);

        [DllImport("opengl32.dll", EntryPoint = "glLightfv")]
        public static extern void glLightfv(int light, int pname, [MarshalAs(UnmanagedType.LPArray)]float[] parameters);

        [DllImport("opengl32.dll", EntryPoint = "glLighti")]
        public static extern void glLighti(int light, int pname, int param);

        [DllImport("opengl32.dll", EntryPoint = "glLightiv")]
        public static extern void glLightiv(int light, int pname, [MarshalAs(UnmanagedType.LPArray)]int[] parameters);
        #endregion

        [DllImport("opengl32.dll", EntryPoint = "glLineStipple")]
        public static extern void glLineStipple(int factor, ushort pattern);

        [DllImport("opengl32.dll", EntryPoint = "glLineWidth")]
        public static extern void glLineWidth(float width);

        [DllImport("opengl32.dll", EntryPoint = "glListBase")]
        public static extern void glListBase(uint @base);

        [DllImport("opengl32.dll", EntryPoint = "glLoadIdentity")]
        public static extern void glLoadIdentity();

        [DllImport("opengl32.dll", EntryPoint = "glLoadMatrixd")]
        public static extern void glLoadMatrixd([MarshalAs(UnmanagedType.LPArray)]double[] m);

        [DllImport("opengl32.dll", EntryPoint = "glLoadMatrixf")]
        public static extern void glLoadMatrixf([MarshalAs(UnmanagedType.LPArray)]float[] m);

        [DllImport("opengl32.dll", EntryPoint = "glLoadName")]
        public static extern void glLoadName(uint name);

        [DllImport("opengl32.dll", EntryPoint = "glLogicOp")]
        public static extern void glLogicOp(int opcode);

        #region void glMap1...
        [DllImport("opengl32.dll", EntryPoint = "glMap1d")]
        public static extern void glMap1d(int target, double u1, double u2, int stride, int order, [MarshalAs(UnmanagedType.LPArray)]double[] points);

        [DllImport("opengl32.dll", EntryPoint = "glMap1f")]
        public static extern void glMap1f(int target, float u1, float u2, int stride, int order, [MarshalAs(UnmanagedType.LPArray)]float[] points);
        #endregion

        #region void glMap2...
        [DllImport("opengl32.dll", EntryPoint = "glMap2d")]
        public static extern void glMap2d(int target, double u1, double u2, int ustride, int uorder, double v1, double v2, int vstride, int vorder, [MarshalAs(UnmanagedType.LPArray)]double[] points);

        [DllImport("opengl32.dll", EntryPoint = "glMap2f")]
        public static extern void glMap2f(int target, float u1, float u2, int ustride, int uorder, float v1, float v2, int vstride, int vorder, [MarshalAs(UnmanagedType.LPArray)]float[] points);
        #endregion

        #region void glMapGrid1...
        [DllImport("opengl32.dll", EntryPoint = "glMapGrid1d")]
        public static extern void glMapGrid1d(int un, double u1, double u2);

        [DllImport("opengl32.dll", EntryPoint = "glMapGrid1f")]
        public static extern void glMapGrid1f(int un, float u1, float u2);
        #endregion

        #region void glMapGrid2...
        [DllImport("opengl32.dll", EntryPoint = "glMapGrid2d")]
        public static extern void glMapGrid2d(int un, double u1, double u2, int vn, double v1, double v2);

        [DllImport("opengl32.dll", EntryPoint = "glMapGrid2f")]
        public static extern void glMapGrid2f(int un, float u1, float u2, int vn, float v1, float v2);
        #endregion

        #region void glMaterial...
        [DllImport("opengl32.dll", EntryPoint = "glMaterialf")]
        public static extern void glMaterialf(int face, int pname, float param);

        [DllImport("opengl32.dll", EntryPoint = "glMaterialfv")]
        public static extern void glMaterialfv(int face, int pname, [MarshalAs(UnmanagedType.LPArray)]float[] parameters);

        [DllImport("opengl32.dll", EntryPoint = "glMateriali")]
        public static extern void glMateriali(int face, int pname, int param);

        [DllImport("opengl32.dll", EntryPoint = "glMaterialiv")]
        public static extern void glMaterialiv(int face, int pname, [MarshalAs(UnmanagedType.LPArray)]int[] parameters);
        #endregion

        [DllImport("opengl32.dll", EntryPoint = "glMatrixMode")]
        public static extern void glMatrixMode(int mode);

        [DllImport("opengl32.dll", EntryPoint = "glMultMatrixd")]
        public static extern void glMultMatrixd([MarshalAs(UnmanagedType.LPArray)]double[] m);

        [DllImport("opengl32.dll", EntryPoint = "glMultMatrixf")]
        public static extern void glMultMatrixf([MarshalAs(UnmanagedType.LPArray)]float[] m);

        [DllImport("opengl32.dll", EntryPoint = "glNewList")]
        public static extern void glNewList(uint list, int mode);

        #region void glNormal3...
        [DllImport("opengl32.dll", EntryPoint = "glNormal3b")]
        public static extern void glNormal3b(sbyte nx, sbyte ny, sbyte nz);

        [DllImport("opengl32.dll", EntryPoint = "glNormal3bv")]
        public static extern void glNormal3bv([MarshalAs(UnmanagedType.LPArray)]sbyte[] v);

        [DllImport("opengl32.dll", EntryPoint = "glNormal3d")]
        public static extern void glNormal3d(double nx, double ny, double nz);

        [DllImport("opengl32.dll", EntryPoint = "glNormal3dv")]
        public static extern void glNormal3dv([MarshalAs(UnmanagedType.LPArray)]double[] v);

        [DllImport("opengl32.dll", EntryPoint = "glNormal3f")]
        public static extern void glNormal3f(float nx, float ny, float nz);

        [DllImport("opengl32.dll", EntryPoint = "glNormal3fv")]
        public static extern void glNormal3fv([MarshalAs(UnmanagedType.LPArray)]float[] v);

        [DllImport("opengl32.dll", EntryPoint = "glNormal3i")]
        public static extern void glNormal3i(int nx, int ny, int nz);

        [DllImport("opengl32.dll", EntryPoint = "glNormal3iv")]
        public static extern void glNormal3iv([MarshalAs(UnmanagedType.LPArray)]int[] v);

        [DllImport("opengl32.dll", EntryPoint = "glNormal3s")]
        public static extern void glNormal3s(short nx, short ny, short nz);

        [DllImport("opengl32.dll", EntryPoint = "glNormal3sv")]
        public static extern void glNormal3sv([MarshalAs(UnmanagedType.LPArray)]short[] v);
        #endregion

        #region void glNormalPointer
        [DllImport("opengl32.dll", EntryPoint = "glNormalPointer")]
        public static extern void glNormalPointer(int type, int stride, IntPtr pointer);

        // This version is needed when using Buffer Objects
        [DllImport("opengl32.dll", EntryPoint = "glNormalPointer")]
        public static extern void glNormalPointer(int type, int stride, int offset);

        public static void glNormalPointer(int type, int stride, object pointer)
        {
            GCHandle h0 = GCHandle.Alloc(pointer, GCHandleType.Pinned);
            try
            {
                glNormalPointer(type, stride, h0.AddrOfPinnedObject());
            }
            finally
            {
                h0.Free();
            }
        }
        #endregion

        [DllImport("opengl32.dll", EntryPoint = "glOrtho")]
        public static extern void glOrtho(double left, double right, double bottom, double top, double zNear, double zFar);

        [DllImport("opengl32.dll", EntryPoint = "glPassThrough")]
        public static extern void glPassThrough(float token);

        #region void glPixelMap...
        [DllImport("opengl32.dll", EntryPoint = "glPixelMapfv")]
        public static extern void glPixelMapfv(uint map, int mapsize, [MarshalAs(UnmanagedType.LPArray)]float[] values);

        [DllImport("opengl32.dll", EntryPoint = "glPixelMapuiv")]
        public static extern void glPixelMapuiv(int map, int mapsize, [MarshalAs(UnmanagedType.LPArray)]uint[] values);

        [DllImport("opengl32.dll", EntryPoint = "glPixelMapusv")]
        public static extern void glPixelMapusv(int map, int mapsize, [MarshalAs(UnmanagedType.LPArray)]ushort[] values);
        #endregion

        [DllImport("opengl32.dll", EntryPoint = "glPixelStoref")]
        public static extern void glPixelStoref(int pname, float param);

        [DllImport("opengl32.dll", EntryPoint = "glPixelStorei")]
        public static extern void glPixelStorei(int pname, int param);

        [DllImport("opengl32.dll", EntryPoint = "glPixelTransferf")]
        public static extern void glPixelTransferf(int pname, float param);

        [DllImport("opengl32.dll", EntryPoint = "glPixelTransferi")]
        public static extern void glPixelTransferi(int pname, int param);

        [DllImport("opengl32.dll", EntryPoint = "glPixelZoom")]
        public static extern void glPixelZoom(float xfactor, float yfactor);

        [DllImport("opengl32.dll", EntryPoint = "glPointSize")]
        public static extern void glPointSize(float size);

        [DllImport("opengl32.dll", EntryPoint = "glPolygonMode")]
        public static extern void glPolygonMode(int face, int mode);

        [DllImport("opengl32.dll", EntryPoint = "glPolygonOffset")]
        public static extern void glPolygonOffset(float factor, float units);

        [DllImport("opengl32.dll", EntryPoint = "glPolygonStipple")]
        public static extern void glPolygonStipple([MarshalAs(UnmanagedType.LPArray)]byte[] mask);

        [DllImport("opengl32.dll", EntryPoint = "glPopAttrib")]
        public static extern void glPopAttrib();

        [DllImport("opengl32.dll", EntryPoint = "glPopClientAttrib")]
        public static extern void glPopClientAttrib();

        [DllImport("opengl32.dll", EntryPoint = "glPopMatrix")]
        public static extern void glPopMatrix();

        [DllImport("opengl32.dll", EntryPoint = "glPopName")]
        public static extern void glPopName();

        [DllImport("opengl32.dll", EntryPoint = "glPrioritizeTextures")]
        public static extern void glPrioritizeTextures(int n, [MarshalAs(UnmanagedType.LPArray)]uint[] textures, [MarshalAs(UnmanagedType.LPArray)]float[] priorities);

        [DllImport("opengl32.dll", EntryPoint = "glPushAttrib")]
        public static extern void glPushAttrib(int mask);

        [DllImport("opengl32.dll", EntryPoint = "glPushClientAttrib")]
        public static extern void glPushClientAttrib(int mask);

        [DllImport("opengl32.dll", EntryPoint = "glPushMatrix")]
        public static extern void glPushMatrix();

        [DllImport("opengl32.dll", EntryPoint = "glPushName")]
        public static extern void glPushName(uint name);

        #region void glRasterPos2...
        [DllImport("opengl32.dll", EntryPoint = "glRasterPos2d")]
        public static extern void glRasterPos2d(double x, double y);

        [DllImport("opengl32.dll", EntryPoint = "glRasterPos2dv")]
        public static extern void glRasterPos2dv([MarshalAs(UnmanagedType.LPArray)]double[] v);

        [DllImport("opengl32.dll", EntryPoint = "glRasterPos2f")]
        public static extern void glRasterPos2f(float x, float y);

        [DllImport("opengl32.dll", EntryPoint = "glRasterPos2fv")]
        public static extern void glRasterPos2fv([MarshalAs(UnmanagedType.LPArray)]float[] v);

        [DllImport("opengl32.dll", EntryPoint = "glRasterPos2i")]
        public static extern void glRasterPos2i(int x, int y);

        [DllImport("opengl32.dll", EntryPoint = "glRasterPos2iv")]
        public static extern void glRasterPos2iv([MarshalAs(UnmanagedType.LPArray)]int[] v);

        [DllImport("opengl32.dll", EntryPoint = "glRasterPos2s")]
        public static extern void glRasterPos2s(short x, short y);

        [DllImport("opengl32.dll", EntryPoint = "glRasterPos2sv")]
        public static extern void glRasterPos2sv([MarshalAs(UnmanagedType.LPArray)]short[] v);
        #endregion

        #region void glRasterPos3...
        [DllImport("opengl32.dll", EntryPoint = "glRasterPos3d")]
        public static extern void glRasterPos3d(double x, double y, double z);

        [DllImport("opengl32.dll", EntryPoint = "glRasterPos3dv")]
        public static extern void glRasterPos3dv([MarshalAs(UnmanagedType.LPArray)]double[] v);

        [DllImport("opengl32.dll", EntryPoint = "glRasterPos3f")]
        public static extern void glRasterPos3f(float x, float y, float z);

        [DllImport("opengl32.dll", EntryPoint = "glRasterPos3fv")]
        public static extern void glRasterPos3fv([MarshalAs(UnmanagedType.LPArray)]float[] v);

        [DllImport("opengl32.dll", EntryPoint = "glRasterPos3i")]
        public static extern void glRasterPos3i(int x, int y, int z);

        [DllImport("opengl32.dll", EntryPoint = "glRasterPos3iv")]
        public static extern void glRasterPos3iv([MarshalAs(UnmanagedType.LPArray)]int[] v);

        [DllImport("opengl32.dll", EntryPoint = "glRasterPos3s")]
        public static extern void glRasterPos3s(short x, short y, short z);

        [DllImport("opengl32.dll", EntryPoint = "glRasterPos3sv")]
        public static extern void glRasterPos3sv([MarshalAs(UnmanagedType.LPArray)]short[] v);
        #endregion

        #region void glRasterPos4...
        [DllImport("opengl32.dll", EntryPoint = "glRasterPos4d")]
        public static extern void glRasterPos4d(double x, double y, double z, double w);

        [DllImport("opengl32.dll", EntryPoint = "glRasterPos4dv")]
        public static extern void glRasterPos4dv([MarshalAs(UnmanagedType.LPArray)]double[] v);

        [DllImport("opengl32.dll", EntryPoint = "glRasterPos4f")]
        public static extern void glRasterPos4f(float x, float y, float z, float w);

        [DllImport("opengl32.dll", EntryPoint = "glRasterPos4fv")]
        public static extern void glRasterPos4fv([MarshalAs(UnmanagedType.LPArray)]float[] v);

        [DllImport("opengl32.dll", EntryPoint = "glRasterPos4i")]
        public static extern void glRasterPos4i(int x, int y, int z, int w);

        [DllImport("opengl32.dll", EntryPoint = "glRasterPos4iv")]
        public static extern void glRasterPos4iv([MarshalAs(UnmanagedType.LPArray)]int[] v);

        [DllImport("opengl32.dll", EntryPoint = "glRasterPos4s")]
        public static extern void glRasterPos4s(short x, short y, short z, short w);

        [DllImport("opengl32.dll", EntryPoint = "glRasterPos4sv")]
        public static extern void glRasterPos4sv([MarshalAs(UnmanagedType.LPArray)]short[] v);
        #endregion

        [DllImport("opengl32.dll", EntryPoint = "glReadBuffer")]
        public static extern void glReadBuffer(int mode);

        #region void glReadPixels
        [DllImport("opengl32.dll", EntryPoint = "glReadPixels")]
        public static extern void glReadPixels(int x, int y, int width, int height, int format, int type, IntPtr pixels);

        [DllImport("opengl32.dll", EntryPoint = "glReadPixels")]
        public static extern void glReadPixels(int x, int y, int width, int height, int format, int type, [MarshalAs(UnmanagedType.LPArray)]byte[] pixels);

        [DllImport("opengl32.dll", EntryPoint = "glReadPixels")]
        public static extern void glReadPixels(int x, int y, int width, int height, int format, int type, [MarshalAs(UnmanagedType.LPArray)]sbyte[] pixels);

        [DllImport("opengl32.dll", EntryPoint = "glReadPixels")]
        public static extern void glReadPixels(int x, int y, int width, int height, int format, int type, [MarshalAs(UnmanagedType.LPArray)]short[] pixels);

        [DllImport("opengl32.dll", EntryPoint = "glReadPixels")]
        public static extern void glReadPixels(int x, int y, int width, int height, int format, int type, [MarshalAs(UnmanagedType.LPArray)]ushort[] pixels);

        [DllImport("opengl32.dll", EntryPoint = "glReadPixels")]
        public static extern void glReadPixels(int x, int y, int width, int height, int format, int type, [MarshalAs(UnmanagedType.LPArray)]int[] pixels);

        [DllImport("opengl32.dll", EntryPoint = "glReadPixels")]
        public static extern void glReadPixels(int x, int y, int width, int height, int format, int type, [MarshalAs(UnmanagedType.LPArray)]uint[] pixels);

        //245   F4 00003198 glReadPixels
        [DllImport("opengl32.dll", EntryPoint = "glReadPixels")]
        public static extern void glReadPixels(int x, int y, int width, int height, int format, int type, [MarshalAs(UnmanagedType.LPArray)]float[] pixels);
        #endregion

        #region glRect...
        //246   F5 00003200 glRectd
        [DllImport("opengl32.dll", EntryPoint = "glRectd")]
        public static extern void glRectd(double x1, double y1, double x2, double y2);

        //247   F6 00003234 glRectdv
        [DllImport("opengl32.dll", EntryPoint = "glRectdv")]
        public static extern void glRectdv([MarshalAs(UnmanagedType.LPArray)]double[] v1, [MarshalAs(UnmanagedType.LPArray)]double[] v2);

        //248   F7 00003268 glRectf
        [DllImport("opengl32.dll", EntryPoint = "glRectf")]
        public static extern void glRectf(float x1, float y1, float x2, float y2);

        //249   F8 0000329C glRectfv
        [DllImport("opengl32.dll", EntryPoint = "glRectfv")]
        public static extern void glRectfv([MarshalAs(UnmanagedType.LPArray)]float[] v1, [MarshalAs(UnmanagedType.LPArray)]float[] v2);

        //250   F9 000032D0 glRecti
        [DllImport("opengl32.dll", EntryPoint = "glRecti")]
        public static extern void glRecti(int x1, int y1, int x2, int y2);


        //251   FA 00003304 glRectiv
        [DllImport("opengl32.dll", EntryPoint = "glRectiv")]
        public static extern void glRectiv([MarshalAs(UnmanagedType.LPArray)]int[] v1, [MarshalAs(UnmanagedType.LPArray)]int[] v2);

        //252   FB 00003338 glRects
        [DllImport("opengl32.dll", EntryPoint = "glRects")]
        public static extern void glRects(short x1, short y1, short x2, short y2);

        //253   FC 0000336C glRectsv
        [DllImport("opengl32.dll", EntryPoint = "glRectsv")]
        public static extern void glRectsv([MarshalAs(UnmanagedType.LPArray)]short[] v1, [MarshalAs(UnmanagedType.LPArray)]short[] v2);
        #endregion

        [DllImport("opengl32.dll", EntryPoint = "glRenderMode")]
        public static extern int glRenderMode(int mode);

        #region void glRotate...
        [DllImport("opengl32.dll", EntryPoint = "glRotated")]
        public static extern void glRotated(double angle, double x, double y, double z);

        [DllImport("opengl32.dll", EntryPoint = "glRotatef")]
        public static extern void glRotatef(float angle, float x, float y, float z);
#endregion

        #region void glScale...
        [DllImport("opengl32.dll", EntryPoint = "glScaled")]
        public static extern void glScaled(double x, double y, double z);

        [DllImport("opengl32.dll", EntryPoint = "glScalef")]
        public static extern void glScalef(float x, float y, float z);
        #endregion

        #region void glScissor()
        [DllImport("opengl32.dll", EntryPoint = "glScissor")]
        public static extern void glScissor(int x, int y, int width, int height);
        #endregion

        [DllImport("opengl32.dll", EntryPoint = "glSelectBuffer")]
        public static extern void glSelectBuffer(int size, [MarshalAs(UnmanagedType.LPArray)]uint[] buffer);

        [DllImport("opengl32.dll", EntryPoint = "glShadeModel")]
        public static extern void glShadeModel(int mode);

        [DllImport("opengl32.dll", EntryPoint = "glStencilFunc")]
        public static extern void glStencilFunc(int func, int @ref, uint mask);

        [DllImport("opengl32.dll", EntryPoint = "glStencilMask")]
        public static extern void glStencilMask(uint mask);

        [DllImport("opengl32.dll", EntryPoint = "glStencilOp")]
        public static extern void glStencilOp(int fail, int zfail, int zpass);

        #region void glTexCoord1...
        [DllImport("opengl32.dll", EntryPoint = "glTexCoord1d")]
        public static extern void glTexCoord1d(double s);

        [DllImport("opengl32.dll", EntryPoint = "glTexCoord1dv")]
        public static extern void glTexCoord1dv([MarshalAs(UnmanagedType.LPArray)]double[] v);

        [DllImport("opengl32.dll", EntryPoint = "glTexCoord1f")]
        public static extern void glTexCoord1f(float s);

        [DllImport("opengl32.dll", EntryPoint = "glTexCoord1fv")]
        public static extern void glTexCoord1fv([MarshalAs(UnmanagedType.LPArray)]float[] v);

        [DllImport("opengl32.dll", EntryPoint = "glTexCoord1i")]
        public static extern void glTexCoord1i(int s);

        [DllImport("opengl32.dll", EntryPoint = "glTexCoord1iv")]
        public static extern void glTexCoord1iv([MarshalAs(UnmanagedType.LPArray)]int[] v);

        [DllImport("opengl32.dll", EntryPoint = "glTexCoord1s")]
        public static extern void glTexCoord1s(short s);

        [DllImport("opengl32.dll", EntryPoint = "glTexCoord1sv")]
        public static extern void glTexCoord1sv([MarshalAs(UnmanagedType.LPArray)]short[] v);
        #endregion

        #region void glTexCoord2...
        [DllImport("opengl32.dll", EntryPoint = "glTexCoord2d")]
        public static extern void glTexCoord2d(double s, double t);

        [DllImport("opengl32.dll", EntryPoint = "glTexCoord2dv")]
        public static extern void glTexCoord2dv([MarshalAs(UnmanagedType.LPArray)]double[] v);

        [DllImport("opengl32.dll", EntryPoint = "glTexCoord2f")]
        public static extern void glTexCoord2f(float s, float t);

        [DllImport("opengl32.dll", EntryPoint = "glTexCoord2fv")]
        public static extern void glTexCoord2fv([MarshalAs(UnmanagedType.LPArray)]float[] v);

        [DllImport("opengl32.dll", EntryPoint = "glTexCoord2i")]
        public static extern void glTexCoord2i(int s, int t);

        [DllImport("opengl32.dll", EntryPoint = "glTexCoord2iv")]
        public static extern void glTexCoord2iv([MarshalAs(UnmanagedType.LPArray)]int[] v);

        [DllImport("opengl32.dll", EntryPoint = "glTexCoord2s")]
        public static extern void glTexCoord2s(short s, short t);

        [DllImport("opengl32.dll", EntryPoint = "glTexCoord2sv")]
        public static extern void glTexCoord2sv([MarshalAs(UnmanagedType.LPArray)]short[] v);
        #endregion

        #region void glTexCoord3...
        [DllImport("opengl32.dll", EntryPoint = "glTexCoord3d")]
        public static extern void glTexCoord3d(double s, double t, double r);

        [DllImport("opengl32.dll", EntryPoint = "glTexCoord3dv")]
        public static extern void glTexCoord3dv([MarshalAs(UnmanagedType.LPArray)]double[] v);

        [DllImport("opengl32.dll", EntryPoint = "glTexCoord3f")]
        public static extern void glTexCoord3f(float s, float t, float r);

        [DllImport("opengl32.dll", EntryPoint = "glTexCoord3fv")]
        public static extern void glTexCoord3fv([MarshalAs(UnmanagedType.LPArray)]float[] v);

        [DllImport("opengl32.dll", EntryPoint = "glTexCoord3i")]
        public static extern void glTexCoord3i(int s, int t, int r);

        [DllImport("opengl32.dll", EntryPoint = "glTexCoord3iv")]
        public static extern void glTexCoord3iv([MarshalAs(UnmanagedType.LPArray)]int[] v);

        [DllImport("opengl32.dll", EntryPoint = "glTexCoord3s")]
        public static extern void glTexCoord3s(short s, short t, short r);

        [DllImport("opengl32.dll", EntryPoint = "glTexCoord3sv")]
        public static extern void glTexCoord3sv([MarshalAs(UnmanagedType.LPArray)]short[] v);
        #endregion

        #region void glTexCoord4...
        [DllImport("opengl32.dll", EntryPoint = "glTexCoord4d")]
        public static extern void glTexCoord4d(double s, double t, double r, double q);

        [DllImport("opengl32.dll", EntryPoint = "glTexCoord4dv")]
        public static extern void glTexCoord4dv([MarshalAs(UnmanagedType.LPArray)]double[] v);

        [DllImport("opengl32.dll", EntryPoint = "glTexCoord4f")]
        public static extern void glTexCoord4f(float s, float t, float r, float q);

        [DllImport("opengl32.dll", EntryPoint = "glTexCoord4fv")]
        public static extern void glTexCoord4fv([MarshalAs(UnmanagedType.LPArray)]float[] v);

        [DllImport("opengl32.dll", EntryPoint = "glTexCoord4i")]
        public static extern void glTexCoord4i(int s, int t, int r, int q);

        [DllImport("opengl32.dll", EntryPoint = "glTexCoord4iv")]
        public static extern void glTexCoord4iv([MarshalAs(UnmanagedType.LPArray)]int[] v);

        [DllImport("opengl32.dll", EntryPoint = "glTexCoord4s")]
        public static extern void glTexCoord4s(short s, short t, short r, short q);

        [DllImport("opengl32.dll", EntryPoint = "glTexCoord4sv")]
        public static extern void glTexCoord4sv([MarshalAs(UnmanagedType.LPArray)]short[] v);
        #endregion

        #region void glTexCoordPointer()
        [DllImport("opengl32.dll", EntryPoint = "glTexCoordPointer")]
        public static extern void glTexCoordPointer(int size, int type, int stride, IntPtr pointer);

        public static void glTexCoordPointer(int size, int type, int stride, object pointer)
        {
            GCHandle h0 = GCHandle.Alloc(pointer, GCHandleType.Pinned);
            try
            {
                glTexCoordPointer(size, type, stride, h0.AddrOfPinnedObject());
            }
            finally
            {
                h0.Free();
            }
        }
        #endregion

        #region void glTexEnv...
        //298  129 00003F50 glTexEnvf
        [DllImport("opengl32.dll", EntryPoint = "glTexEnvf")]
        public static extern void glTexEnvf(int target, int pname, float param);

        //299  12A 00003F84 glTexEnvfv
        [DllImport("opengl32.dll", EntryPoint = "glTexEnvfv")]
        public static extern void glTexEnvfv(int target, int pname, [MarshalAs(UnmanagedType.LPArray)]float[] parameters);

        //300  12B 00003FB8 glTexEnvi
        [DllImport("opengl32.dll", EntryPoint = "glTexEnvi")]
        public static extern void glTexEnvi(int target, int pname, int param);

        //301  12C 00003FEC glTexEnviv
        [DllImport("opengl32.dll", EntryPoint = "glTexEnviv")]
        public static extern void glTexEnviv(int target, int pname, [MarshalAs(UnmanagedType.LPArray)]int[] parameters);
        #endregion

        #region void glTexGen...
        //302  12D 00004020 glTexGend
        [DllImport("opengl32.dll", EntryPoint = "glTexGend")]
        public static extern void glTexGend(int coord, int pname, double param);

        //303  12E 00004054 glTexGendv
        [DllImport("opengl32.dll", EntryPoint = "glTexGendv")]
        public static extern void glTexGendv(int coord, int pname, [MarshalAs(UnmanagedType.LPArray)]double[] parameters);

        //304  12F 00004088 glTexGenf
        [DllImport("opengl32.dll", EntryPoint = "glTexGenf")]
        public static extern void glTexGenf(int coord, int pname, float param);

        //305  130 000040BC glTexGenfv
        [DllImport("opengl32.dll", EntryPoint = "glTexGenfv")]
        public static extern void glTexGenfv(int coord, uint pname, [MarshalAs(UnmanagedType.LPArray)]float[] parameters);

        //306  131 000040F0 glTexGeni
        [DllImport("opengl32.dll", EntryPoint = "glTexGeni")]
        public static extern void glTexGeni(int coord, uint pname, int param);

        //307  132 00004124 glTexGeniv
        [DllImport("opengl32.dll", EntryPoint = "glTexGeniv")]
        public static extern void glTexGeniv(int coord, uint pname, [MarshalAs(UnmanagedType.LPArray)]int[] parameters);
        #endregion

        #region void glTexImage1D
        [DllImport("opengl32.dll", EntryPoint = "glTexImage1D")]
        public static extern void glTexImage1D(int target, int level, int internalformat, int width, int border, int format, int type, IntPtr pixels);

        public static void glTexImage1D(int target, int level, int internalformat, int width, int border, int format, int type, object pixels)
        {
            GCHandle h0 = GCHandle.Alloc(pixels, GCHandleType.Pinned);
            try
            {
                glTexImage1D(target, level, internalformat, width, border, format, type, h0.AddrOfPinnedObject());
            }
            finally
            {
                h0.Free();
            }
        }
        #endregion

        #region void glTexImage2D
        [DllImport("opengl32.dll", EntryPoint = "glTexImage2D")]
        public static extern void glTexImage2D(int target, int level, int internalformat, int width, int height, int border, int format, int type, IntPtr pixels);

        public static void glTexImage2D(int target, int level, int internalformat, int width, int height, int border, int format, int type, object pixels)
        {
            GCHandle h0 = GCHandle.Alloc(pixels, GCHandleType.Pinned);
            try
            {
                glTexImage2D(target, level, internalformat, width, height, border, format, type, h0.AddrOfPinnedObject());
            }
            finally
            {
                h0.Free();
            }
        }
        #endregion

        #region void glTexParameter...
        [DllImport("opengl32.dll", EntryPoint = "glTexParameterf")]
        public static extern void glTexParameterf(int target, int pname, float param);

        [DllImport("opengl32.dll", EntryPoint = "glTexParameterfv")]
        public static extern void glTexParameterfv(int target, int pname, [MarshalAs(UnmanagedType.LPArray)]float[] parameters);

        [DllImport("opengl32.dll", EntryPoint = "glTexParameteri")]
        public static extern void glTexParameteri(int target, int pname, int param);

        [DllImport("opengl32.dll", EntryPoint = "glTexParameteriv")]
        public static extern void glTexParameteriv(int target, int pname, [MarshalAs(UnmanagedType.LPArray)]int[] parameters);
        #endregion

        #region void glTexSubImage1D
        [DllImport("opengl32.dll", EntryPoint = "glTexSubImage1D")]
        public static extern void glTexSubImage1D(int target, int level, int xoffset, int width, int format, int type, [MarshalAs(UnmanagedType.LPArray)]object[] pixels);
        #endregion

        #region void glTexSubImage2D
        [DllImport("opengl32.dll", EntryPoint = "glTexSubImage2D")]
        public static extern void glTexSubImage2D(int target, int level, int xoffset, int yoffset, int width, int height, int format, int type, IntPtr pixels);

        public static void glTexSubImage2D(int target, int level, int xoffset, int yoffset, int width, int height, int format, int type, object pixels)
        {
            GCHandle h0 = GCHandle.Alloc(pixels, GCHandleType.Pinned);
            try
            {
                glTexSubImage2D(target, level, xoffset, yoffset, width, height, format, type, h0.AddrOfPinnedObject());
            }
            finally
            {
                h0.Free();
            }
        }
        #endregion

        #region void glTranslate...
        [DllImport("opengl32.dll", EntryPoint = "glTranslated")]
        public static extern void glTranslated(double x, double y, double z);

        [DllImport("opengl32.dll", EntryPoint = "glTranslatef")]
        public static extern void glTranslatef(float x, float y, float z);
        #endregion

        #region void glVertex2...
        //318  13D 00002B68 glVertex2d
        [DllImport("opengl32.dll", EntryPoint = "glVertex2d")]
        public static extern void glVertex2d(double x, double y);

        //319  13E 00002B74 glVertex2dv
        [DllImport("opengl32.dll", EntryPoint = "glVertex2dv")]
        public static extern void glVertex2dv([MarshalAs(UnmanagedType.LPArray)]double[] v);

        //320  13F 00002B80 glVertex2f
        [DllImport("opengl32.dll", EntryPoint = "glVertex2f")]
        public static extern void glVertex2f(float x, float y);

        //321  140 00002B8C glVertex2fv
        [DllImport("opengl32.dll", EntryPoint = "glVertex2fv")]
        public static extern void glVertex2fv([MarshalAs(UnmanagedType.LPArray)]float[] v);

        //322  141 00002B98 glVertex2i
        [DllImport("opengl32.dll", EntryPoint = "glVertex2i")]
        public static extern void glVertex2i(int x, int y);

        //323  142 00002BA4 glVertex2iv
        [DllImport("opengl32.dll", EntryPoint = "glVertex2iv")]
        public static extern void glVertex2iv([MarshalAs(UnmanagedType.LPArray)]int[] v);

        //324  143 00002BB0 glVertex2s
        [DllImport("opengl32.dll", EntryPoint = "glVertex2s")]
        public static extern void glVertex2s(short x, short y);

        //325  144 00002BBC glVertex2sv
        [DllImport("opengl32.dll", EntryPoint = "glVertex2sv")]
        public static extern void glVertex2sv([MarshalAs(UnmanagedType.LPArray)]short[] v);
        #endregion

        #region void glVertex3...
        //326  145 00002BC8 glVertex3d
        [DllImport("opengl32.dll", EntryPoint = "glVertex3d")]
        public static extern void glVertex3d(double x, double y, double z);

        //327  146 00002BD4 glVertex3dv
        [DllImport("opengl32.dll", EntryPoint = "glVertex3dv")]
        public static extern void glVertex3dv([MarshalAs(UnmanagedType.LPArray)]double[] v);

        //328  147 00002BE0 glVertex3f
        [DllImport("opengl32.dll", EntryPoint = "glVertex3f")]
        public static extern void glVertex3f(float x, float y, float z);

        //329  148 00002BEC glVertex3fv
        [DllImport("opengl32.dll", EntryPoint = "glVertex3fv")]
        public static extern void glVertex3fv([MarshalAs(UnmanagedType.LPArray)]float[] v);

        //330  149 00002BF8 glVertex3i
        [DllImport("opengl32.dll", EntryPoint = "glVertex3i")]
        public static extern void glVertex3i(int x, int y, int z);

        //331  14A 00002C04 glVertex3iv
        [DllImport("opengl32.dll", EntryPoint = "glVertex3iv")]
        public static extern void glVertex3iv([MarshalAs(UnmanagedType.LPArray)]int[] v);

        //332  14B 00002C10 glVertex3s
        [DllImport("opengl32.dll", EntryPoint = "glVertex3s")]
        public static extern void glVertex3s(short x, short y, short z);

        //333  14C 00002C1C glVertex3sv
        [DllImport("opengl32.dll", EntryPoint = "glVertex3sv")]
        public static extern void glVertex3sv([MarshalAs(UnmanagedType.LPArray)]short[] v);
        #endregion

        #region void glVertex4...
        //334  14D 00002C28 glVertex4d
        [DllImport("opengl32.dll", EntryPoint = "glVertex4d")]
        public static extern void glVertex4d(double x, double y, double z, double w);

        //335  14E 00002C34 glVertex4dv
        [DllImport("opengl32.dll", EntryPoint = "glVertex4dv")]
        public static extern void glVertex4dv([MarshalAs(UnmanagedType.LPArray)]double[] v);

        //336  14F 00002C40 glVertex4f
        [DllImport("opengl32.dll", EntryPoint = "glVertex4f")]
        public static extern void glVertex4f(float x, float y, float z, float w);

        //337  150 00002C4C glVertex4fv
        [DllImport("opengl32.dll", EntryPoint = "glVertex4fv")]
        public static extern void glVertex4fv([MarshalAs(UnmanagedType.LPArray)]float[] v);

        //338  151 00002C58 glVertex4i
        [DllImport("opengl32.dll", EntryPoint = "glVertex4i")]
        public static extern void glVertex4i(int x, int y, int z, int w);

        //339  152 00002C64 glVertex4iv
        [DllImport("opengl32.dll", EntryPoint = "glVertex4iv")]
        public static extern void glVertex4iv([MarshalAs(UnmanagedType.LPArray)]int[] v);

        //340  153 00002C70 glVertex4s
        [DllImport("opengl32.dll", EntryPoint = "glVertex4s")]
        public static extern void glVertex4s(short x, short y, short z, short w);

        //341  154 00002C7C glVertex4sv
        [DllImport("opengl32.dll", EntryPoint = "glVertex4sv")]
        public static extern void glVertex4sv([MarshalAs(UnmanagedType.LPArray)]short[] v);
        #endregion

        #region void glVertexPointer...
        //342  155 00002EBC glVertexPointer
        [DllImport("opengl32.dll", EntryPoint = "glVertexPointer")]
        public static extern void glVertexPointer(int size, int type, int stride, IntPtr pointer);

        //342  155 00002EBC glVertexPointer
        [DllImport("opengl32.dll", EntryPoint = "glVertexPointer")]
        public static extern void glVertexPointer(int size, int type, int stride, int offset);

        public static void glVertexPointer(int size, int type, int stride, object pointer)
        {
            GCHandle h0 = GCHandle.Alloc(pointer, GCHandleType.Pinned);
            try
            {
                glVertexPointer(size, type, stride, h0.AddrOfPinnedObject());
            }
            finally
            {
                h0.Free();
            }
        }
        #endregion

        #region void glViewport()
        //343  156 0000502C glViewport
        [DllImport("opengl32.dll", EntryPoint = "glViewport")]
        public static extern void glViewport(int x, int y, int width, int height);
        #endregion
    }
}
