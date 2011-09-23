using System;
using System.Runtime.InteropServices;

namespace TOAPI.OpenGL
{
    // The original list of functions was derived by doing a dumpbin.exe /extract glu.dll
    // From this list, functions are filled in with interop signatures as they 
    // are needed.
    public partial class glu
    {
         // 1    0 000019DA gluBeginCurve
        [DllImport("glu32.dll", EntryPoint = "gluBeginCurve")]
        public static extern void gluBeginCurve(IntPtr nobj);

         // 2    1 00003110 gluBeginPolygon
        [DllImport("glu32.dll", EntryPoint = "gluBeginPolygon", ExactSpelling = true)]
        public static extern void gluBeginPolygon(int tess);
        
        // 3    2 000019A0 gluBeginSurface
        [DllImport("glu32.dll", EntryPoint = "gluBeginSurface", ExactSpelling = true)]
        public static extern void gluBeginSurface(int nurb);

        // 4    3 00001A84 gluBeginTrim
        [DllImport("glu32.dll", EntryPoint = "gluBeginTrim", ExactSpelling = true)]
        public static extern void gluBeginTrim(int nurb);

        // 5    4 00007B3C gluBuild1DMipmaps
        [DllImport("glu32.dll", EntryPoint = "gluBuild1DMipmaps", ExactSpelling = true)]
        public static extern int gluBuild1DMipmaps(int target, int internalFormat, int width, int format, int type, IntPtr data);
        
        // 6    5 00007DF0 gluBuild2DMipmaps
        [DllImport("glu32.dll", EntryPoint = "gluBuild2DMipmaps", ExactSpelling = true)]
        public static extern int gluBuild2DMipmaps(int target, int internalFormat, int width, int height, int format, int type, IntPtr data);

        public static int gluBuild2DMipmaps(int target, int internalFormat, int width, int height, int format, int type, object pointer)
        {
            int retValue = 0;
            GCHandle h0 = GCHandle.Alloc(pointer, GCHandleType.Pinned);
            try
            {
                retValue = gluBuild2DMipmaps(target, internalFormat, width, height, format, type, h0.AddrOfPinnedObject());
            }
            finally
            {
                h0.Free();
            }

            return retValue;
        }

         // 7    6 00008231 gluCylinder
        [DllImport("glu32.dll", EntryPoint = "gluCylinder")]
        public static extern void gluCylinder(IntPtr qobj,double baseRadius,double topRadius,double height,int slices,int stacks);

         // 8    7 000020A8 gluDeleteNurbsRenderer
        [DllImport("glu32.dll", EntryPoint = "gluDeleteNurbsRenderer")]
        public static extern void gluDeleteNurbsRenderer(IntPtr nurbsRenderer);
 
        // 9    8 00008127 gluDeleteQuadric
        [DllImport("glu32.dll", EntryPoint = "gluDeleteQuadric")]
        public static extern void gluDeleteQuadric(IntPtr quadric);

         //10    9 00003236 gluDeleteTess
        [DllImport("glu32.dll", EntryPoint = "gluDeleteTess", ExactSpelling = true)]
        public static extern void gluDeleteTess(int tess);

        //11    A 0000A6A1 gluDisk
        [DllImport("glu32.dll", EntryPoint = "gluDisk")]
        public static extern void gluDisk(IntPtr qobj, double innerRadius, double outerRadius, int slices, int loops);

        //12    B 00001A14 gluEndCurve
        [DllImport("glu32.dll", EntryPoint = "gluEndCurve")]
        public static extern void gluEndCurve(IntPtr nobj);

         //13    C 000034D1 gluEndPolygon
        [DllImport("glu32.dll", EntryPoint = "gluEndPolygon", ExactSpelling = true)]
        public static extern void gluEndPolygon(int tess);
        
        //14    D 00001A4C gluEndSurface
        [DllImport("glu32.dll", EntryPoint = "gluEndSurface", ExactSpelling = true)]
        public static extern void gluEndSurface(int nurb);
        
        //15    E 00001ABC gluEndTrim
        [DllImport("glu32.dll", EntryPoint = "gluEndTrim", ExactSpelling = true)]
        public static extern void gluEndTrim(int nurb);

        //16    F 0000AA44 gluErrorString
        [DllImport("glu32.dll", EntryPoint = "gluErrorString", ExactSpelling = true)]
        public static extern IntPtr gluErrorString(int error);
        
        //17   10 0000A9C7 gluErrorUnicodeStringEXT

         //18   11 00001EE8 gluGetNurbsProperty
        //[DllImport("glu32.dll", EntryPoint = "gluGetNurbsProperty", ExactSpelling = true)]
        //public static extern unsafe void gluGetNurbsProperty(int nurb, NurbsProperty property, [Out] float* data);
        
        //19   12 0000AAC1 gluGetString
        [DllImport("glu32.dll", EntryPoint = "gluGetString", ExactSpelling = true)]
        public static extern IntPtr gluGetString(int name);
        
        //20   13 00002D95 gluGetTessProperty
        //[DllImport("glu32.dll", EntryPoint = "gluGetTessProperty", ExactSpelling = true)]
        //public static extern unsafe void gluGetTessProperty(int tess, OpenTK.OpenGL.Enums.TessProperty which, [Out] double* data);
        
        //21   14 00001C16 gluLoadSamplingMatrices
        //[DllImport("glu32.dll", EntryPoint = "gluLoadSamplingMatrices", ExactSpelling = true)]
        //public static extern unsafe void gluLoadSamplingMatrices(int nurb, float* model, float* perspective, int* view);

        //22   15 0000AD09 gluLookAt
        [DllImport("glu32.dll", EntryPoint = "gluLookAt")]
        public static extern void gluLookAt(double eyex, double eyey, double eyez, double centerx, double centery, double centerz, double upx, double upy, double upz);
        
        //23   16 00001916 gluNewNurbsRenderer
        [DllImport("glu32.dll", EntryPoint = "gluNewNurbsRenderer")]
        public static extern IntPtr gluNewNurbsRenderer();
        
        //24   17 000080F9 gluNewQuadric
        [DllImport("glu32.dll", EntryPoint = "gluNewQuadric")]
        public static extern IntPtr gluNewQuadric();

         //25   18 00002BF1 gluNewTess
        [DllImport("glu32.dll", EntryPoint = "gluNewTess", ExactSpelling = true)]
        public static extern int gluNewTess();
        
        //26   19 000034B3 gluNextContour
        [DllImport("glu32.dll", EntryPoint = "gluNextContour", ExactSpelling = true)]
        public static extern void gluNextContour(int tess, int type);

        //27   1A 00002025 gluNurbsCallback
        [DllImport("glu32.dll", EntryPoint = "gluNurbsCallback", ExactSpelling = true)]
        public static extern void gluNurbsCallback(int nurb, int which, IntPtr CallBackFunc);
        
        //28   1B 00001B53 gluNurbsCurve
        //[DllImport("glu32.dll", EntryPoint = "gluNurbsCurve", ExactSpelling = true)]
        //public static extern unsafe void gluNurbsCurve(int nurb, int knotCount, [Out] float* knots, int stride, [Out] float* control, int order, MapTarget type);
        
        //29   1C 00001C57 gluNurbsProperty
        [DllImport("glu32.dll", EntryPoint = "gluNurbsProperty", ExactSpelling = true)]
        public static extern void gluNurbsProperty(int nurb, int property, float value);

        //30   1D 00001BB8 gluNurbsSurface
        //[DllImport("glu32.dll", EntryPoint = "gluNurbsSurface", ExactSpelling = true)]
        //public static extern unsafe void gluNurbsSurface(int nurb, int sKnotCount, float* sKnots, int tKnotCount, float* tKnots, int sStride, int tStride, float* control, int sOrder, int tOrder, OpenTK.OpenGL.Enums.MapTarget type);

        //31   1E 0000ABA1 gluOrtho2D
        [DllImport("glu32.dll", EntryPoint = "gluOrtho2D")]
        public static extern void gluOrtho2D(double left,double right,double bottom,double top);

         //32   1F 00008BFA gluPartialDisk
        [DllImport("glu32.dll", EntryPoint = "gluPartialDisk", ExactSpelling = true)]
        public static extern void gluPartialDisk(int quad, double inner, double outer, int slices, int loops, double start, double sweep);

        //33   20 0000ABE3 gluPerspective
        [DllImport("glu32.dll", EntryPoint = "gluPerspective")]
        public static extern void gluPerspective(double fov, double aspect, double zNear, double zFar);

        //34   21 0000B1A8 gluPickMatrix
        [DllImport("glu32.dll", EntryPoint = "gluPickMatrix")]
        public static extern void gluPickMatrix(double x, double y, double width, double height, int [] viewport);

        //35   22 0000B043 gluProject
        [DllImport("glu32.dll", EntryPoint = "gluProject")]
        public static extern int gluProject(double objx, double objy, double objz, 
            double[] modelMatrix, double[] projectionMatrix, int[] viewPort, 
            out double winX, out double winY, out double winZ);
        
        //36   23 00001AF4 gluPwlCurve
        //[DllImport("glu32.dll", EntryPoint = "gluPwlCurve", ExactSpelling = true)]
        //public static extern unsafe void gluPwlCurve(int nurb, int count, float* data, int stride, int type);

        //37   24 00008150 gluQuadricCallback
        [DllImport("glu32.dll", EntryPoint = "gluQuadricCallback", ExactSpelling = true)]
        public static extern void gluQuadricCallback(int quad, int which, IntPtr CallBackFunc);

        //38   25 000081FD gluQuadricDrawStyle
        [DllImport("glu32.dll", EntryPoint = "gluQuadricDrawStyle")]
        public static extern void gluQuadricDrawStyle(IntPtr qobj, int drawStyle);

         //39   26 0000817F gluQuadricNormals
        [DllImport("glu32.dll", EntryPoint = "gluQuadricNormals")]
        public static extern void gluQuadricNormals(IntPtr qobj, int normals);

        //40   27 000081C9 gluQuadricOrientation
        [DllImport("glu32.dll", EntryPoint = "gluQuadricOrientation")]
        public static extern void gluQuadricOrientation(IntPtr quadObject, int orientation);

        //41   28 000081B2 gluQuadricTexture
        [DllImport("glu32.dll", EntryPoint = "gluQuadricTexture")]
        public static extern void gluQuadricTexture(IntPtr quadObject, int textureCoords);

         //42   29 000079DD gluScaleImage
        [DllImport("glu32.dll", EntryPoint = "gluScaleImage")]
        public static extern int gluScaleImage(int format, int widthin, int heightin, int typein, IntPtr datain, int widthout, int heightout, int typeout, IntPtr dataout);

         //43   2A 00009728 gluSphere
        [DllImport("glu32.dll", EntryPoint = "gluSphere")]
        public static extern void gluSphere(IntPtr quadric, double radius, int slices, int stacks);

         //44   2B 000030D6 gluTessBeginContour
        [DllImport("glu32.dll", EntryPoint = "gluTessBeginContour", ExactSpelling = true)]
        public static extern void gluTessBeginContour(int tess);
        
        //45   2C 00003096 gluTessBeginPolygon
        [DllImport("glu32.dll", EntryPoint = "gluTessBeginPolygon", ExactSpelling = true)]
        public static extern void gluTessBeginPolygon(int tess, IntPtr data);
        
        //46   2D 00002E39 gluTessCallback
        [DllImport("glu32.dll", EntryPoint = "gluTessCallback", ExactSpelling = true)]
        public static extern void gluTessCallback(int tess, int which, IntPtr CallBackFunc);
        
        //47   2E 00003358 gluTessEndContour
        [DllImport("glu32.dll", EntryPoint = "gluTessEndContour", ExactSpelling = true)]
        public static extern void gluTessEndContour(int tess);
        
        //48   2F 0000337E gluTessEndPolygon
        [DllImport("glu32.dll", EntryPoint = "gluTessEndPolygon", ExactSpelling = true)]
        public static extern void gluTessEndPolygon(int tess);
        
        //49   30 00002E16 gluTessNormal
        [DllImport("glu32.dll", EntryPoint = "gluTessNormal", ExactSpelling = true)]
        public static extern void gluTessNormal(int tess, double valueX, double valueY, double valueZ);
        
        //50   31 00002CA9 gluTessProperty
        [DllImport("glu32.dll", EntryPoint = "gluTessProperty", ExactSpelling = true)]
        public static extern void gluTessProperty(int tess, int which, double data);
        
        //51   32 0000325D gluTessVertex
        //[DllImport("glu32.dll", EntryPoint = "gluTessVertex", ExactSpelling = true)]
        //public static extern unsafe void TessVertex(int tess, [Out] double* location, IntPtr data);

         //52   33 0000B0E8 gluUnProject
        [DllImport("glu32.dll", EntryPoint = "gluUnProject")]
        public static extern int gluUnProject(double winx, double winy, double winz, double[] modelViewMatrix, double[] projectionMatrix, int[] viewPort, double[] posX, double[] posY, double[] posZ);

    }
}
