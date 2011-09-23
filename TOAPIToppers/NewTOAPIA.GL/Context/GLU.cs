using System;
using System.Runtime.InteropServices;

using TOAPI.OpenGL;

namespace NewTOAPIA.GL
{
    public class Glu
    {
        GraphicsInterface fGI;

        public Glu(GraphicsInterface gi)
        {
            fGI = gi;
        }

        // 1    0 000019DA gluBeginCurve
        public void BeginCurve(IntPtr nobj)
        {
            glu.gluBeginCurve(nobj);
        }

        // 2    1 00003110 gluBeginPolygon
        public void BeginPolygon(int tess)
        {
            glu.gluBeginPolygon(tess);
        }

        // 3    2 000019A0 gluBeginSurface
        public void BeginSurface(int nurb)
        {
            glu.gluBeginSurface(nurb);
        }

        // 4    3 00001A84 gluBeginTrim
        public void BeginTrim(int nurb)
        {
            glu.gluBeginTrim(nurb);
        }

        // 5    4 00007B3C gluBuild1DMipmaps
        public int Build1DMipmaps(int target, int internalFormat, int width, int format, int type, IntPtr data)
        {
            int retValue = glu.gluBuild1DMipmaps((int)target, (int) internalFormat, width, (int) format, (int) type, data);

            return retValue;
        }

        // 6    5 00007DF0 gluBuild2DMipmaps
        public int Build2DMipmaps(int internalFormat, int width, int height, int format, int type, IntPtr data)
        {
            int retValue = glu.gluBuild2DMipmaps((int) Texture2DTarget.Texture2d, (int) internalFormat, (int) width, (int) height, (int) format, (int) type, data);
            
            return retValue;
        }

        public int Build2DMipmaps(int internalFormat, int width, int height, int format, int type, byte[] data)
        {
            int retValue = glu.gluBuild2DMipmaps((int)Texture2DTarget.Texture2d, internalFormat, width, height, format, type, data);

            return retValue;
        }
        

        // 7    6 00008231 gluCylinder
        public void Cylinder(IntPtr qobj, double baseRadius, double topRadius, double height, int slices, int stacks)
        {
            glu.gluCylinder(qobj, (double) baseRadius, (double) topRadius, (double) height, (int) slices, (int) stacks);
        }

        // 8    7 000020A8 gluDeleteNurbsRenderer
        public void DeleteNurbsRenderer(IntPtr nurbsRenderer)
        {
            glu.gluDeleteNurbsRenderer(nurbsRenderer);
        }

        // 9    8 00008127 gluDeleteQuadric
        public void DeleteQuadric(IntPtr quadric)
        {
            glu.gluDeleteQuadric(quadric);
        }

        //10    9 00003236 gluDeleteTess
        public void DeleteTess(int tess)
        {
            glu.gluDeleteTess(tess);
        }

        //11    A 0000A6A1 gluDisk
        public void Disk(IntPtr qobj, double innerRadius, double outerRadius, int slices, int loops)
        {
            glu.gluDisk(qobj, (double) innerRadius, (double) outerRadius, (int) slices, (int) loops);
        }

        //12    B 00001A14 gluEndCurve
        public void EndCurve(IntPtr nobj)
        {
            glu.gluEndCurve(nobj);
        }

        //13    C 000034D1 gluEndPolygon
        public void EndPolygon(int tess)
        {
            glu.gluEndPolygon(tess);
        }

        //14    D 00001A4C gluEndSurface
        public void EndSurface(int nurb)
        {
            glu.gluEndSurface(nurb);
        }

        //15    E 00001ABC gluEndTrim
        public void EndTrim(int nurb)
        {
            glu.gluEndTrim(nurb);
        }

        //16    F 0000AA44 gluErrorString
        public string ErrorString(GLErrorCode error)
        {
            IntPtr strPtr = glu.gluErrorString((int)error);
            if (IntPtr.Zero == strPtr)
                return string.Empty;

            string aString = Marshal.PtrToStringAnsi(strPtr);

            return aString;
       }

        //17   10 0000A9C7 gluErrorUnicodeStringEXT
    
    

        //18   11 00001EE8 gluGetNurbsProperty
        //public unsafe void GetNurbsProperty(int nurb, NurbsProperty property, [Out] float* data)

        //19   12 0000AAC1 gluGetString
        #region string GetString(GluStringName name)
        public static string GetString(GluStringName name)
        {
            IntPtr strPtr = glu.gluGetString((int)name);
            if (IntPtr.Zero == strPtr)
                return string.Empty;

            string aString = Marshal.PtrToStringAnsi(strPtr);

            return aString;
        }
        #endregion

        //20   13 00002D95 gluGetTessProperty
        //public unsafe void GetTessProperty(int tess, OpenTK.OpenGL.Enums.TessProperty which, [Out] double* data)

        //21   14 00001C16 gluLoadSamplingMatrices
        //public unsafe void LoadSamplingMatrices(int nurb, float* model, float* perspective, int* view)

        //22   15 0000AD09 gluLookAt
        public void LookAt(double eyex, double eyey, double eyez, double centerx, double centery, double centerz, double upx, double upy, double upz)
        {
            glu.gluLookAt(eyex, eyey, eyez, centerx, centery, centerz, upx, upy, upz);
        }

        //23   16 00001916 gluNewNurbsRenderer
        public IntPtr NewNurbsRenderer()
        {
            IntPtr retValue = glu.gluNewNurbsRenderer();
            return retValue;
        }

        //24   17 000080F9 gluNewQuadric
        public IntPtr NewQuadric()
        {
            IntPtr retValue = glu.gluNewQuadric();
            return retValue;
        }

        //25   18 00002BF1 gluNewTess
        public int NewTess()
        {
            int retValue = glu.gluNewTess();
            return retValue;
        }

        //26   19 000034B3 gluNextContour
        public void NextContour(int tess, int type)
        {
            glu.gluNextContour(tess, (int) type);
        }

        //27   1A 00002025 gluNurbsCallback
        public void NurbsCallback(int nurb, int which, IntPtr CallBackFunc)
        {
            glu.gluNurbsCallback(nurb, (int) which, CallBackFunc);
        }

        //28   1B 00001B53 gluNurbsCurve
        //public unsafe void NurbsCurve(int nurb, (int) knotCount, [Out] float* knots, (int) stride, [Out] float* control, (int) order, MapTarget type)

        //29   1C 00001C57 gluNurbsProperty
        public void NurbsProperty(int nurb, int property, float value)
        {
            glu.gluNurbsProperty(nurb, property, value);
        }

        //30   1D 00001BB8 gluNurbsSurface
        //public unsafe void NurbsSurface(int nurb, (int) sKnotCount, float* sKnots, (int) tKnotCount, float* tKnots, (int) sStride, (int) tStride, float* control, (int) sOrder, (int) tOrder, OpenTK.OpenGL.Enums.MapTarget type)

        //31   1E 0000ABA1 gluOrtho2D
        public void Ortho2D(double left, double right, double bottom, double top)
        {
            glu.gluOrtho2D(left, right, bottom, top);
        }

        //32   1F 00008BFA gluPartialDisk
        public void PartialDisk(int quad, double inner, double outer, int slices, int loops, double start, double sweep)
        {
            glu.gluPartialDisk(quad, (double) inner, (double) outer, (int) slices, (int) loops, (double) start, (double) sweep);
        }

        //33   20 0000ABE3 gluPerspective
        public void Perspective(double fov, double aspect, double zNear, double zFar)
        {
            glu.gluPerspective(fov, (double) aspect, (double) zNear, (double) zFar);
        }

        //34   21 0000B1A8 gluPickMatrix
        public void PickMatrix(double x, double y, double width, double height, int[] viewport)
        {
            glu.gluPickMatrix(x, (double) y, (double) width, (double) height, viewport);
        }

        //35   22 0000B043 gluProject
        public int Project(double objx, double objy, double objz,
            double[] modelMatrix, double[] projectionMatrix, int[] viewPort,
            out double winX, out double winY, out double winZ)
        {
            int retValue = glu.gluProject(objx, (double) objy, (double) objz,
            modelMatrix, projectionMatrix, viewPort,
            out winX, out winY, out winZ);

            return retValue;
        }

        //36   23 00001AF4 gluPwlCurve
        //public unsafe void PwlCurve(int nurb, (int) count, float* data, (int) stride, (int) type)

        //37   24 00008150 gluQuadricCallback
        public void QuadricCallback(int quad, int which, IntPtr CallBackFunc)
        {
            glu.gluQuadricCallback(quad, (int) which, CallBackFunc);
        }

        //38   25 000081FD gluQuadricDrawStyle
        public void QuadricDrawStyle(IntPtr qobj, int drawStyle)
        {
            glu.gluQuadricDrawStyle(qobj, (int) drawStyle);
        }

        //39   26 0000817F gluQuadricNormals
        public void QuadricNormals(IntPtr qobj, int normals)
        {
            glu.gluQuadricNormals(qobj, (int) normals);
        }

        //40   27 000081C9 gluQuadricOrientation
        public void QuadricOrientation(IntPtr quadObject, int orientation)
        {
            glu.gluQuadricOrientation(quadObject, (int) orientation);
        }

        //41   28 000081B2 gluQuadricTexture
        public void QuadricTexture(IntPtr quadObject, int textureCoords)
        {
            glu.gluQuadricTexture(quadObject, (int) textureCoords);
        }

        //42   29 000079DD gluScaleImage
        public int ScaleImage(int format, int widthin, int heightin, int typein, IntPtr datain, int widthout, int heightout, int typeout, IntPtr dataout)
        {
            int retValue = glu.gluScaleImage((int) format, (int) widthin, (int) heightin, (int) typein, datain, (int) widthout, (int) heightout, (int) typeout, dataout);
            return retValue;
        }

        //43   2A 00009728 gluSphere
        public void Sphere(IntPtr quadric, double radius, int slices, int stacks)
        {
            glu.gluSphere(quadric, (double) radius, (int) slices, (int) stacks);
        }

        //44   2B 000030D6 gluTessBeginContour
        public void TessBeginContour(int tess)
        {
            glu.gluTessBeginContour(tess);
        }

        //45   2C 00003096 gluTessBeginPolygon
        public void TessBeginPolygon(int tess, IntPtr data)
        {
            glu.gluTessBeginPolygon(tess, data);
        }

        //46   2D 00002E39 gluTessCallback
        public void TessCallback(int tess, int which, IntPtr CallBackFunc)
        {
            glu.gluTessCallback(tess, (int) which, CallBackFunc);
        }

        //47   2E 00003358 gluTessEndContour
        public void TessEndContour(int tess)
        {
            glu.gluTessEndContour(tess);
        }

        //48   2F 0000337E gluTessEndPolygon
        public void TessEndPolygon(int tess)
        {
            glu.gluTessEndPolygon(tess);
        }

        //49   30 00002E16 gluTessNormal
        public void TessNormal(int tess, double valueX, double valueY, double valueZ)
        {
            glu.gluTessNormal(tess, (double) valueX, (double) valueY, (double) valueZ);
        }

        //50   31 00002CA9 gluTessProperty
        public void TessProperty(int tess, int which, double data)
        {
            glu.gluTessProperty(tess, (int) which, (double) data);
        }

        //51   32 0000325D gluTessVertex
        //public unsafe void TessVertex(int tess, [Out] double* location, IntPtr data)

        //52   33 0000B0E8 gluUnProject
        public int UnProject(double winx, double winy, double winz, double[] modelViewMatrix, double[] projectionMatrix, int[] viewPort, double[] posX, double[] posY, double[] posZ)
        {
            int retValue = glu.gluUnProject(winx, winy, winz, modelViewMatrix, projectionMatrix, viewPort, posX, posY, posZ);
            return retValue;
        }
    }
}
