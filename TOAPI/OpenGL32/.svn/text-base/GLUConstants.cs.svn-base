
namespace TOAPI.OpenGL
{
    public partial class glu
    {
        /* Version */
        public const int
            GLU_VERSION_1_1         = 1,
            GLU_VERSION_1_2         = 1;

        /* Errors: (return value 0 = no error) */
        public const int
            GLU_INVALID_ENUM       = 100900,
            GLU_INVALID_VALUE      = 100901,
            GLU_OUT_OF_MEMORY      = 100902,
            GLU_INCOMPATIBLE_GL_VERSION    = 100903;

        /* StringName */
        public const int
            GLU_VERSION            = 100800,
            GLU_EXTENSIONS         = 100801;

        /* Boolean */
        public const int
            GLU_TRUE              =  gl.GL_TRUE,
            GLU_FALSE             =  gl.GL_FALSE;


        /****           Quadric constants               ****/

        /* QuadricNormal */
        public const int
            GLU_SMOOTH            =  100000,
            GLU_FLAT              =  100001,
            GLU_NONE              =  100002;

        /* QuadricDrawStyle */
        public const int
        GLU_POINT             =  100010,
        GLU_LINE              =  100011,
        GLU_FILL              =  100012,
        GLU_SILHOUETTE        =  100013;

        /* QuadricOrientation */
        public const int
        GLU_OUTSIDE           =  100020,
        GLU_INSIDE            =  100021;

        /* Callback types: */
        /*      GLU_ERROR               100103 */


        /****           Tesselation constants           ****/

        public const double
            GLU_TESS_MAX_COORD     =         1.0e150;

        /* TessProperty */
        public const int
            GLU_TESS_WINDING_RULE       =    100140,
            GLU_TESS_BOUNDARY_ONLY      =    100141,
            GLU_TESS_TOLERANCE          =    100142;

        /* TessWinding */
        public const int
            GLU_TESS_WINDING_ODD          =  100130,
            GLU_TESS_WINDING_NONZERO      =  100131,
            GLU_TESS_WINDING_POSITIVE     =  100132,
            GLU_TESS_WINDING_NEGATIVE     =  100133,
            GLU_TESS_WINDING_ABS_GEQ_TWO  =  100134;

        /* TessCallback */
        public const int
            GLU_TESS_BEGIN        =  100100,  /* void (CALLBACK*)(GLenum    type)  */
            GLU_TESS_VERTEX       =  100101,  /* void (CALLBACK*)(void      *data) */
            GLU_TESS_END          =  100102,  /* void (CALLBACK*)(void)            */
            GLU_TESS_ERROR        =  100103,  /* void (CALLBACK*)(GLenum    errno) */
            GLU_TESS_EDGE_FLAG    =  100104,  /* void (CALLBACK*)(GLboolean boundaryEdge)  */
            GLU_TESS_COMBINE      =  100105,  /* void (CALLBACK*)(GLdouble  coords[3],
                                                            void      *data[4],
                                                            GLfloat   weight[4],
                                                            void      **dataOut)     */
            GLU_TESS_BEGIN_DATA   =  100106,  /* void (CALLBACK*)(GLenum    type,  
                                                                        void      *polygon_data) */
            GLU_TESS_VERTEX_DATA  =  100107,  /* void (CALLBACK*)(void      *data, 
                                                                        void      *polygon_data) */
            GLU_TESS_END_DATA     =  100108,  /* void (CALLBACK*)(void      *polygon_data) */
            GLU_TESS_ERROR_DATA   =  100109,  /* void (CALLBACK*)(GLenum    errno, 
                                                                        void      *polygon_data) */
            GLU_TESS_EDGE_FLAG_DATA= 100110,  /* void (CALLBACK*)(GLboolean boundaryEdge,
                                                                        void      *polygon_data) */
            GLU_TESS_COMBINE_DATA =  100111;  /* void (CALLBACK*)(GLdouble  coords[3],
                                                                        void      *data[4],
                                                                        GLfloat   weight[4],
                                                                        void      **dataOut,
                                                                        void      *polygon_data) */

        /* TessError */
        public const int
            GLU_TESS_ERROR1   =  100151,
            GLU_TESS_ERROR2   =  100152,
            GLU_TESS_ERROR3   =  100153,
            GLU_TESS_ERROR4   =  100154,
            GLU_TESS_ERROR5   =  100155,
            GLU_TESS_ERROR6   =  100156,
            GLU_TESS_ERROR7   =  100157,
            GLU_TESS_ERROR8   =  100158;

        public const int
            GLU_TESS_MISSING_BEGIN_POLYGON = GLU_TESS_ERROR1,
            GLU_TESS_MISSING_BEGIN_CONTOUR = GLU_TESS_ERROR2,
            GLU_TESS_MISSING_END_POLYGON   = GLU_TESS_ERROR3,
            GLU_TESS_MISSING_END_CONTOUR   = GLU_TESS_ERROR4,
            GLU_TESS_COORD_TOO_LARGE       = GLU_TESS_ERROR5,
            GLU_TESS_NEED_COMBINE_CALLBACK = GLU_TESS_ERROR6;

        /****           NURBS constants                 ****/

        /* NurbsProperty */
        public const int
            GLU_AUTO_LOAD_MATRIX   = 100200,
            GLU_CULLING            = 100201,
            GLU_SAMPLING_TOLERANCE = 100203,
            GLU_DISPLAY_MODE       = 100204,
            GLU_PARAMETRIC_TOLERANCE =       100202,
            GLU_SAMPLING_METHOD      =       100205,
            GLU_U_STEP               =       100206,
            GLU_V_STEP               =       100207;

        /* NurbsSampling */
        public const int
            GLU_PATH_LENGTH         =        100215,
            GLU_PARAMETRIC_ERROR    =        100216,
            GLU_DOMAIN_DISTANCE     =        100217;


        /* NurbsTrim */
        public const int
            GLU_MAP1_TRIM_2      =  100210,
            GLU_MAP1_TRIM_3      =   100211;

        /* NurbsDisplay */
        public const int
        /*      GLU_FILL                100012 */
            GLU_OUTLINE_POLYGON     = 100240,
            GLU_OUTLINE_PATCH       = 100241;

        /* NurbsCallback */
        /*      GLU_ERROR               100103 */

        /* NurbsErrors */
        public const int
            GLU_NURBS_ERROR1      =  100251,
            GLU_NURBS_ERROR2      =  100252,
            GLU_NURBS_ERROR3      =  100253,
            GLU_NURBS_ERROR4      =  100254,
            GLU_NURBS_ERROR5      =  100255,
            GLU_NURBS_ERROR6      =  100256,
            GLU_NURBS_ERROR7      =  100257,
            GLU_NURBS_ERROR8      =  100258,
            GLU_NURBS_ERROR9      =  100259,
            GLU_NURBS_ERROR10     =  100260,
            GLU_NURBS_ERROR11     =  100261,
            GLU_NURBS_ERROR12     =  100262,
            GLU_NURBS_ERROR13     =  100263,
            GLU_NURBS_ERROR14     =  100264,
            GLU_NURBS_ERROR15     =  100265,
            GLU_NURBS_ERROR16     =  100266,
            GLU_NURBS_ERROR17     =  100267,
            GLU_NURBS_ERROR18     =  100268,
            GLU_NURBS_ERROR19     =  100269,
            GLU_NURBS_ERROR20     =  100270,
            GLU_NURBS_ERROR21     =  100271,
            GLU_NURBS_ERROR22     =  100272,
            GLU_NURBS_ERROR23     =  100273,
            GLU_NURBS_ERROR24     =  100274,
            GLU_NURBS_ERROR25     =  100275,
            GLU_NURBS_ERROR26     =  100276,
            GLU_NURBS_ERROR27     =  100277,
            GLU_NURBS_ERROR28     =  100278,
            GLU_NURBS_ERROR29     =  100279,
            GLU_NURBS_ERROR30     =  100280,
            GLU_NURBS_ERROR31     =  100281,
            GLU_NURBS_ERROR32     =  100282,
            GLU_NURBS_ERROR33     =  100283,
            GLU_NURBS_ERROR34     =  100284,
            GLU_NURBS_ERROR35     =  100285,
            GLU_NURBS_ERROR36     =  100286,
            GLU_NURBS_ERROR37     =  100287;


        /* Contours types -- obsolete! */
        public const int
            GLU_CW         = 100120,
            GLU_CCW        = 100121,
            GLU_INTERIOR   = 100122,
            GLU_EXTERIOR   = 100123,
            GLU_UNKNOWN    = 100124;

        /* Names without "TESS_" prefix */
        public const int
            GLU_BEGIN      = GLU_TESS_BEGIN,
            GLU_VERTEX     = GLU_TESS_VERTEX,
            GLU_END        = GLU_TESS_END,
            GLU_ERROR      = GLU_TESS_ERROR,
            GLU_EDGE_FLAG  = GLU_TESS_EDGE_FLAG;

    }
}
