namespace NewTOAPIA.Graphics.Processor
{
    using System;

    using NewTOAPIA.Graphics;

    /// <summary>
    /// GPLang represents the set of functions that are typically useful
    /// in the implementation of graphics related programs.
    /// 
    /// The initial set follows the functions that are found in OpenGL
    /// Shader programming (GLSL - 1.4).  The chapter references in the 
    /// regions are from the second edition of the orange book.
    /// 
    /// As they are generally useful, any effort related to graphics programming
    /// might find them to be handy.
    /// 
    /// These functions are implemented as static methods for a couple of reasons:
    /// 1)  There is no instance information needed
    /// 2) It makes it easy to subclass the GPLang class and have an interface that
    /// is easy to utilize without having to use '.' notation.
    /// 
    /// This latter point is used by the GPShader class.  You can essentially copy 
    /// shader programs straight out of the 'Orange' book and implement them in C#.
    /// That's pretty cool.
    /// 
    /// Similarly, if you want to use these routines from another context, there's not
    /// too much to type:
    /// 
    /// GPLang.clamp(x, minVal, maxVal)
    /// 
    /// etc.
    /// </summary>
    abstract public class GPLang
    {
        const float degToRad = (float)(Math.PI / 180);
        const float radToDeg = (float)(180.0f / Math.PI);



        #region Angle and Trigonometry Functions (5.1)
        public static float radians(float degrees)
        {
            return degrees * degToRad;
        }

        public static vec2 radians(vec2 degrees)
        {
            vec2 r = new vec2(degrees.x * degToRad, degrees.y * degToRad);

            return r;
        }

        public static vec3 radians(vec3 degrees)
        {
            vec3 r;

            r.x = degrees.x * degToRad;
            r.y = degrees.y * degToRad;
            r.z = degrees.z * degToRad;

            return r;
        }

        public static vec4 radians(vec4 degrees)
        {
            vec4 r;

            r.x = degrees.x * degToRad;
            r.y = degrees.y * degToRad;
            r.z = degrees.z * degToRad;
            r.w = degrees.w * degToRad;

            return r;

        }

        public static float degrees(float radians)
        {
            return radians * radToDeg;
        }

        public static vec2 degrees(vec2 radians)
        {
            vec2 r;

            r.x = radians.x * radToDeg;
            r.y = radians.y * radToDeg;

            return r;
        }

        public static vec3 degrees(vec3 radians)
        {
            vec3 r;

            r.x = radians.x * radToDeg;
            r.y = radians.y * radToDeg;
            r.z = radians.z * radToDeg;

            return r;

        }

        public static vec4 degrees(vec4 radians)
        {
            vec4 r;

            r.x = radians.x * radToDeg;
            r.y = radians.y * radToDeg;
            r.z = radians.z * radToDeg;
            r.w = radians.w * radToDeg;

            return r;
        }

        public static float sin(float radians)
        {
            return (float)Math.Sin(radians);
        }

        public static vec2 sin(vec2 radians)
        {
            vec2 r = new vec2();

            r.x = (float)Math.Sin(radians.x);
            r.y = (float)Math.Sin(radians.y);

            return r;
        }

        public static vec3 sin(vec3 radians)
        {
            vec3 r;

            r.x = (float)Math.Sin(radians.x);
            r.y = (float)Math.Sin(radians.y);
            r.z = (float)Math.Sin(radians.z);

            return r;
        }

        public static vec4 sin(vec4 radians)
        {
            vec4 r;

            r.x = (float)Math.Sin(radians.x);
            r.y = (float)Math.Sin(radians.y);
            r.z = (float)Math.Sin(radians.z);
            r.w = (float)Math.Sin(radians.w);

            return r;
        }


        public static float cos(float radians)
        {
            return (float)Math.Cos(radians);
        }

        public static vec2 cos(vec2 radians)
        {
            vec2 r = new vec2();

            r.x = (float)Math.Cos(radians.x);
            r.y = (float)Math.Cos(radians.y);

            return r;
        }

        public static vec3 cos(vec3 radians)
        {
            vec3 r;

            r.x = (float)Math.Cos(radians.x);
            r.y = (float)Math.Cos(radians.y);
            r.z = (float)Math.Cos(radians.z);

            return r;
        }

        public static vec4 cos(vec4 radians)
        {
            vec4 r;

            r.x = (float)Math.Cos(radians.x);
            r.y = (float)Math.Cos(radians.y);
            r.z = (float)Math.Cos(radians.z);
            r.w = (float)Math.Cos(radians.w);

            return r;
        }


        public static float tan(float radians)
        {
            return (float)Math.Tan(radians);
        }

        public static vec2 tan(vec2 radians)
        {
            vec2 r = new vec2();

            r.x = (float)Math.Tan(radians.x);
            r.y = (float)Math.Tan(radians.y);

            return r;
        }

        public static vec3 tan(vec3 radians)
        {
            vec3 r;

            r.x = (float)Math.Tan(radians.x);
            r.y = (float)Math.Tan(radians.y);
            r.z = (float)Math.Tan(radians.z);

            return r;
        }

        public static vec4 tan(vec4 radians)
        {
            vec4 r;

            r.x = (float)Math.Tan(radians.x);
            r.y = (float)Math.Tan(radians.y);
            r.z = (float)Math.Tan(radians.z);
            r.w = (float)Math.Tan(radians.w);

            return r;
        }


        public static float asin(float x)
        {
            return (float)Math.Asin(x);
        }

        public static vec2 asin(vec2 x)
        {
            vec2 r = new vec2();

            r.x = (float)Math.Asin(x.x);
            r.y = (float)Math.Asin(x.y);

            return r;
        }

        public static vec3 asin(vec3 x)
        {
            vec3 r;

            r.x = (float)Math.Asin(x.x);
            r.y = (float)Math.Asin(x.y);
            r.z = (float)Math.Asin(x.z);

            return r;
        }

        public static vec4 asin(vec4 x)
        {
            vec4 r;

            r.x = (float)Math.Asin(x.x);
            r.y = (float)Math.Asin(x.y);
            r.z = (float)Math.Asin(x.z);
            r.w = (float)Math.Asin(x.w);

            return r;
        }

        public static float acos(float x)
        {
            return (float)Math.Acos(x);
        }

        public static vec2 acos(vec2 x)
        {
            vec2 r = new vec2();

            r.x = (float)Math.Acos(x.x);
            r.y = (float)Math.Acos(x.y);

            return r;
        }

        public static vec3 acos(vec3 x)
        {
            vec3 r;

            r.x = (float)Math.Acos(x.x);
            r.y = (float)Math.Acos(x.y);
            r.z = (float)Math.Acos(x.z);

            return r;
        }

        public static vec4 acos(vec4 x)
        {
            vec4 r;

            r.x = (float)Math.Acos(x.x);
            r.y = (float)Math.Acos(x.y);
            r.z = (float)Math.Acos(x.z);
            r.w = (float)Math.Acos(x.w);

            return r;
        }


        public static float atan(float y, float x)
        {
            return (float)Math.Atan(y / x);
        }

        public static vec2 atan(vec2 y, vec2 x)
        {
            vec2 r = new vec2();

            r.x = (float)Math.Atan(y.x / x.x);
            r.y = (float)Math.Atan(y.y / x.y);

            return r;
        }

        public static vec3 atan(vec3 y, vec3 x)
        {
            vec3 r;

            r.x = (float)Math.Atan(y.x / x.x);
            r.y = (float)Math.Atan(y.y / x.y);
            r.z = (float)Math.Atan(y.z / x.z);

            return r;
        }

        public static vec4 atan(vec4 y, vec4 x)
        {
            vec4 r;

            r.x = (float)Math.Atan(y.x / x.x);
            r.y = (float)Math.Atan(y.y / x.y);
            r.z = (float)Math.Atan(y.z / x.z);
            r.w = (float)Math.Atan(y.w / x.w);

            return r;
        }


        public static float atan(float y_over_x)
        {
            return (float)Math.Atan(y_over_x);
        }

        public static vec2 atan(vec2 y_over_x)
        {
            vec2 r = new vec2();

            r.x = (float)Math.Atan(y_over_x.x);
            r.y = (float)Math.Atan(y_over_x.y);

            return r;
        }

        public static vec3 atan(vec3 y_over_x)
        {
            vec3 r;

            r.x = (float)Math.Atan(y_over_x.x);
            r.y = (float)Math.Atan(y_over_x.y);
            r.z = (float)Math.Atan(y_over_x.z);

            return r;
        }

        public static vec4 atan(vec4 y_over_x)
        {
            vec4 r;

            r.x = (float)Math.Atan(y_over_x.x);
            r.y = (float)Math.Atan(y_over_x.y);
            r.z = (float)Math.Atan(y_over_x.z);
            r.w = (float)Math.Atan(y_over_x.w);

            return r;
        }


        public static float sinh(float x)
        {
            return (float)Math.Sinh(x);
        }

        public static vec2 sinh(vec2 x)
        {
            vec2 r = new vec2();

            r.x = (float)Math.Sinh(x.x);
            r.y = (float)Math.Sinh(x.y);

            return r;
        }

        public static vec3 sinh(vec3 x)
        {
            vec3 r;

            r.x = (float)Math.Sinh(x.x);
            r.y = (float)Math.Sinh(x.y);
            r.z = (float)Math.Sinh(x.z);

            return r;
        }

        public static vec4 sinh(vec4 x)
        {
            vec4 r;

            r.x = (float)Math.Sinh(x.x);
            r.y = (float)Math.Sinh(x.y);
            r.z = (float)Math.Sinh(x.z);
            r.w = (float)Math.Sinh(x.w);

            return r;
        }


        public static float cosh(float x)
        {
            return (float)Math.Cosh(x);
        }

        public static vec2 cosh(vec2 x)
        {
            vec2 r = new vec2();

            r.x = (float)Math.Cosh(x.x);
            r.y = (float)Math.Cosh(x.y);

            return r;
        }

        public static vec3 cosh(vec3 x)
        {
            vec3 r;

            r.x = (float)Math.Cosh(x.x);
            r.y = (float)Math.Cosh(x.y);
            r.z = (float)Math.Cosh(x.z);

            return r;
        }

        public static vec4 cosh(vec4 x)
        {
            vec4 r;

            r.x = (float)Math.Cosh(x.x);
            r.y = (float)Math.Cosh(x.y);
            r.z = (float)Math.Cosh(x.z);
            r.w = (float)Math.Cosh(x.w);

            return r;
        }


        public static float tanh(float x)
        {
            return (float)Math.Tanh(x);
        }

        public static vec2 tanh(vec2 x)
        {
            vec2 r = new vec2();

            r.x = (float)Math.Tanh(x.x);
            r.y = (float)Math.Tanh(x.y);

            return r;
        }

        public static vec3 tanh(vec3 x)
        {
            vec3 r;

            r.x = (float)Math.Tanh(x.x);
            r.y = (float)Math.Tanh(x.y);
            r.z = (float)Math.Tanh(x.z);

            return r;
        }

        public static vec4 tanh(vec4 x)
        {
            vec4 r;

            r.x = (float)Math.Tanh(x.x);
            r.y = (float)Math.Tanh(x.y);
            r.z = (float)Math.Tanh(x.z);
            r.w = (float)Math.Tanh(x.w);

            return r;
        }

        #region asinh
        public static float asinh(float x)
        {
            return (float)Math.Log(x + Math.Sqrt(x * x + 1.0));
        }

        public static vec2 asinh(vec2 x)
        {
            vec2 r = new vec2();

            r.x = asinh(x.x);
            r.y = asinh(x.y);

            return r;
        }

        public static vec3 asinh(vec3 x)
        {
            vec3 r;

            r.x = asinh(x.x);
            r.y = asinh(x.y);
            r.z = asinh(x.z);

            return r;
        }

        public static vec4 asinh(vec4 x)
        {
            vec4 r;

            r.x = asinh(x.x);
            r.y = asinh(x.y);
            r.z = asinh(x.z);
            r.w = asinh(x.w);

            return r;
        }
        #endregion asinh

        #region acosh
        public static float acosh(float x)
        {
            return (float)Math.Log(x + Math.Sqrt(x * x - 1.0)); ;
        }

        public static vec2 acosh(vec2 x)
        {
            vec2 r = new vec2();

            r.x = acosh(x.x);
            r.y = acosh(x.y);

            return r;
        }

        public static vec3 acosh(vec3 x)
        {
            vec3 r;

            r.x = acosh(x.x);
            r.y = acosh(x.y);
            r.z = acosh(x.z);

            return r;
        }

        public static vec4 acosh(vec4 x)
        {
            vec4 r;

            r.x = acosh(x.x);
            r.y = acosh(x.y);
            r.z = acosh(x.z);
            r.w = acosh(x.w);

            return r;
        }
        #endregion acosh

        #region atanh
        public static float atanh(float x)
        {
            return (float)(0.5 * Math.Log((1.0 + x) / (1.0 - x)));
        }

        public static vec2 atanh(vec2 x)
        {
            vec2 r = new vec2();

            r.x = atanh(x.x);
            r.y = atanh(x.y);

            return r;
        }

        public static vec3 atanh(vec3 x)
        {
            vec3 r;

            r.x = atanh(x.x);
            r.y = atanh(x.y);
            r.z = atanh(x.z);

            return r;
        }

        public static vec4 atanh(vec4 x)
        {
            vec4 r;

            r.x = atanh(x.x);
            r.y = atanh(x.y);
            r.z = atanh(x.z);
            r.w = atanh(x.w);

            return r;
        }
        #endregion

        #endregion

        #region Exponential Functions (5.2)
        public static float pow(float x, float y)
        {
            return (float)Math.Pow(x, y);
        }

        public static vec2 pow(vec2 x, vec2 y)
        {
            vec2 r = new vec2();

            r.x = (float)Math.Pow(x.x, y.x);
            r.y = (float)Math.Pow(x.y, y.y);

            return r;
        }

        public static vec3 pow(vec3 x, vec3 y)
        {
            vec3 r;

            r.x = (float)Math.Pow(x.x, y.x);
            r.y = (float)Math.Pow(x.y, y.y);
            r.z = (float)Math.Pow(x.z, y.z);

            return r;

        }

        public static vec4 pow(vec4 x, vec4 y)
        {
            vec4 r;

            r.x = (float)Math.Pow(x.x, y.x);
            r.y = (float)Math.Pow(x.y, y.y);
            r.z = (float)Math.Pow(x.z, y.z);
            r.w = (float)Math.Pow(x.w, y.w);

            return r;
        }


        public static float exp(float x)
        {
            return (float)Math.Exp(x);
        }

        public static vec2 exp(vec2 x)
        {
            vec2 r = new vec2();

            r.x = (float)Math.Exp(x.x);
            r.y = (float)Math.Exp(x.y);

            return r;
        }

        public static vec3 exp(vec3 x)
        {
            vec3 r;

            r.x = (float)Math.Exp(x.x);
            r.y = (float)Math.Exp(x.y);
            r.z = (float)Math.Exp(x.z);

            return r;

        }

        public static vec4 exp(vec4 x)
        {
            vec4 r;

            r.x = (float)Math.Exp(x.x);
            r.y = (float)Math.Exp(x.y);
            r.z = (float)Math.Exp(x.z);
            r.w = (float)Math.Exp(x.w);

            return r;
        }


        public static float log(float x)
        {
            return (float)Math.Log(x);
        }

        public static vec2 log(vec2 x)
        {
            vec2 r = new vec2();

            r.x = (float)Math.Log(x.x);
            r.y = (float)Math.Log(x.y);

            return r;
        }

        public static vec3 log(vec3 x)
        {
            vec3 r;

            r.x = (float)Math.Log(x.x);
            r.y = (float)Math.Log(x.y);
            r.z = (float)Math.Log(x.z);

            return r;
        }

        public static vec4 log(vec4 x)
        {
            vec4 r;

            r.x = (float)Math.Log(x.x);
            r.y = (float)Math.Log(x.y);
            r.z = (float)Math.Log(x.z);
            r.w = (float)Math.Log(x.w);

            return r;
        }


        public static float exp2(float x)
        {
            return (float)Math.Pow(2, x);
        }

        public static vec2 exp2(vec2 x)
        {
            vec2 r = new vec2();

            r.x = (float)Math.Pow(2, x.x);
            r.y = (float)Math.Pow(2, x.y);

            return r;
        }

        public static vec3 exp2(vec3 x)
        {
            vec3 r;

            r.x = (float)Math.Pow(2, x.x);
            r.y = (float)Math.Pow(2, x.y);
            r.z = (float)Math.Pow(2, x.z);

            return r;
        }

        public static vec4 exp2(vec4 x)
        {
            vec4 r;

            r.x = (float)Math.Pow(2, x.x);
            r.y = (float)Math.Pow(2, x.y);
            r.z = (float)Math.Pow(2, x.z);
            r.w = (float)Math.Pow(2, x.w);

            return r;
        }


        public static float log2(float x)
        {
            return (float)Math.Log(x, 2);
        }

        public static vec2 log2(vec2 x)
        {
            vec2 r = new vec2();

            r.x = (float)Math.Log(x.x, 2);
            r.y = (float)Math.Log(x.y, 2);

            return r;
        }

        public static vec3 log2(vec3 x)
        {
            vec3 r;

            r.x = (float)Math.Log(x.x, 2);
            r.y = (float)Math.Log(x.y, 2);
            r.z = (float)Math.Log(x.z, 2);

            return r;
        }

        public static vec4 log2(vec4 x)
        {
            vec4 r;

            r.x = (float)Math.Log(x.x, 2);
            r.y = (float)Math.Log(x.y, 2);
            r.z = (float)Math.Log(x.z, 2);
            r.w = (float)Math.Log(x.w, 2);

            return r;
        }


        public static float sqrt(float x)
        {
            return (float)Math.Sqrt(x);
        }

        public static vec2 sqrt(vec2 x)
        {
            vec2 r = new vec2();

            r.x = (float)Math.Sqrt(x.x);
            r.y = (float)Math.Sqrt(x.y);

            return r;
        }

        public static vec3 sqrt(vec3 x)
        {
            vec3 r;

            r.x = (float)Math.Sqrt(x.x);
            r.y = (float)Math.Sqrt(x.y);
            r.z = (float)Math.Sqrt(x.z);

            return r;

        }

        public static vec4 sqrt(vec4 x)
        {
            vec4 r;

            r.x = (float)Math.Sqrt(x.x);
            r.y = (float)Math.Sqrt(x.y);
            r.z = (float)Math.Sqrt(x.z);
            r.w = (float)Math.Sqrt(x.w);

            return r;
        }


        public static float inversesqrt(float x)
        {
            return (float)(1 / Math.Sqrt(x));
        }

        public static vec2 inversesqrt(vec2 x)
        {
            vec2 r = new vec2();

            r.x = (float)(1 / Math.Sqrt(x.x));
            r.y = (float)(1 / Math.Sqrt(x.y));

            return r;
        }

        public static vec3 inversesqrt(vec3 x)
        {
            vec3 r;

            r.x = (float)(1 / Math.Sqrt(x.x));
            r.y = (float)(1 / Math.Sqrt(x.y));
            r.z = (float)(1 / Math.Sqrt(x.z));

            return r;

        }

        public static vec4 inversesqrt(vec4 x)
        {
            vec4 r;

            r.x = (float)(1 / Math.Sqrt(x.x));
            r.y = (float)(1 / Math.Sqrt(x.y));
            r.z = (float)(1 / Math.Sqrt(x.z));
            r.w = (float)(1 / Math.Sqrt(x.w));

            return r;
        }

        #endregion

        #region Common Functions (5.3)
        #region abs
        public static float abs(double x)
        {
            return (float)Math.Abs(x);
        }

        public static float abs(float x)
        {
            return Math.Abs(x);
        }

        public static vec2 abs(vec2 x)
        {
            vec2 r = new vec2();

            r.x = Math.Abs(x.x);
            r.y = Math.Abs(x.y);

            return r;
        }

        public static vec3 abs(vec3 x)
        {
            vec3 r;

            r.x = Math.Abs(x.x);
            r.y = Math.Abs(x.y);
            r.z = Math.Abs(x.z);

            return r;
        }

        public static vec4 abs(vec4 x)
        {
            vec4 r;

            r.x = Math.Abs(x.x);
            r.y = Math.Abs(x.y);
            r.z = Math.Abs(x.z);
            r.w = Math.Abs(x.w);

            return r;
        }

        public static int abs(int x)
        {
            return Math.Abs(x);
        }

        public static ivec2 abs(ivec2 x)
        {
            ivec2 r;

            r.x = Math.Abs(x.x);
            r.y = Math.Abs(x.y);

            return r;
        }

        public static ivec3 abs(ivec3 x)
        {
            ivec3 r;

            r.x = Math.Abs(x.x);
            r.y = Math.Abs(x.y);
            r.z = Math.Abs(x.z);

            return r;
        }

        public static ivec4 abs(ivec4 x)
        {
            ivec4 r;

            r.x = Math.Abs(x.x);
            r.y = Math.Abs(x.y);
            r.z = Math.Abs(x.z);
            r.w = Math.Abs(x.w);

            return r;
        }
        #endregion

        #region ceil
        public static float ceil(float x)
        {
            return (float)Math.Ceiling(x);
        }

        public static vec2 ceil(vec2 x)
        {
            vec2 r = new vec2();

            r.x = (float)Math.Ceiling(x.x);
            r.y = (float)Math.Ceiling(x.y);

            return r;
        }

        public static vec3 ceil(vec3 x)
        {
            vec3 r;

            r.x = (float)Math.Ceiling(x.x);
            r.y = (float)Math.Ceiling(x.y);
            r.z = (float)Math.Ceiling(x.z);

            return r;
        }

        public static vec4 ceil(vec4 x)
        {
            vec4 r;

            r.x = (float)Math.Ceiling(x.x);
            r.y = (float)Math.Ceiling(x.y);
            r.z = (float)Math.Ceiling(x.z);
            r.w = (float)Math.Ceiling(x.w);

            return r;
        }
        #endregion

        #region clamp
        public static float clamp(float x, float minVal, float maxVal)
        {
            return min(max(x, minVal), maxVal);
        }

        public static vec2 clamp(vec2 x, float minVal, float maxVal)
        {
            vec2 r = new vec2();

            r.x = min(max(x.x, minVal), maxVal);
            r.y = min(max(x.y, minVal), maxVal);

            return r;
        }

        public static vec3 clamp(vec3 x, float minVal, float maxVal)
        {
            vec3 r;

            r.x = min(max(x.x, minVal), maxVal);
            r.y = min(max(x.y, minVal), maxVal);
            r.z = min(max(x.z, minVal), maxVal);

            return r;
        }

        public static vec4 clamp(vec4 x, float minVal, float maxVal)
        {
            vec4 r;

            r.x = min(max(x.x, minVal), maxVal);
            r.y = min(max(x.y, minVal), maxVal);
            r.z = min(max(x.z, minVal), maxVal);
            r.w = min(max(x.w, minVal), maxVal);

            return r;
        }


        public static int clamp(int x, int minVal, int maxVal)
        {
            return min(max(x, minVal), maxVal);
        }

        public static ivec2 clamp(ivec2 x, int minVal, int maxVal)
        {
            ivec2 r;

            r.x = min(max(x.x, minVal), maxVal);
            r.y = min(max(x.y, minVal), maxVal);

            return r;
        }

        public static ivec3 clamp(ivec3 x, int minVal, int maxVal)
        {
            ivec3 r;

            r.x = min(max(x.x, minVal), maxVal);
            r.y = min(max(x.y, minVal), maxVal);
            r.z = min(max(x.z, minVal), maxVal);

            return r;
        }

        public static ivec4 clamp(ivec4 x, int minVal, int maxVal)
        {
            ivec4 r;

            r.x = min(max(x.x, minVal), maxVal);
            r.y = min(max(x.y, minVal), maxVal);
            r.z = min(max(x.z, minVal), maxVal);
            r.w = min(max(x.w, minVal), maxVal);

            return r;
        }


        public static uint clamp(uint x, uint minVal, uint maxVal)
        {
            return min(max(x, minVal), maxVal);
        }

        public static uvec2 clamp(uvec2 x, uint minVal, uint maxVal)
        {
            uvec2 r;

            r.x = min(max(x.x, minVal), maxVal);
            r.y = min(max(x.y, minVal), maxVal);

            return r;
        }

        public static uvec3 clamp(uvec3 x, uint minVal, uint maxVal)
        {
            uvec3 r;

            r.x = min(max(x.x, minVal), maxVal);
            r.y = min(max(x.y, minVal), maxVal);
            r.z = min(max(x.z, minVal), maxVal);

            return r;
        }

        public static uvec4 clamp(uvec4 x, uint minVal, uint maxVal)
        {
            uvec4 r;

            r.x = min(max(x.x, minVal), maxVal);
            r.y = min(max(x.y, minVal), maxVal);
            r.z = min(max(x.z, minVal), maxVal);
            r.w = min(max(x.w, minVal), maxVal);

            return r;
        }


        public static vec2 clamp(vec2 x, vec2 minVal, vec2 maxVal)
        {
            vec2 r = new vec2();

            r.x = min(max(x.x, minVal.x), maxVal.x);
            r.y = min(max(x.y, minVal.y), maxVal.y);

            return r;
        }

        public static vec3 clamp(vec3 x, vec3 minVal, vec3 maxVal)
        {
            vec3 r;

            r.x = min(max(x.x, minVal.x), maxVal.x);
            r.y = min(max(x.y, minVal.y), maxVal.y);
            r.z = min(max(x.z, minVal.z), maxVal.z);

            return r;
        }

        public static vec4 clamp(vec4 x, vec4 minVal, vec4 maxVal)
        {
            vec4 r;

            r.x = min(max(x.x, minVal.x), maxVal.x);
            r.y = min(max(x.y, minVal.y), maxVal.y);
            r.z = min(max(x.z, minVal.z), maxVal.z);
            r.w = min(max(x.w, minVal.w), maxVal.w);

            return r;
        }


        public static ivec2 clamp(ivec2 x, ivec2 minVal, ivec2 maxVal)
        {
            ivec2 r;

            r.x = min(max(x.x, minVal.x), maxVal.x);
            r.y = min(max(x.y, minVal.y), maxVal.y);

            return r;
        }

        public static ivec3 clamp(ivec3 x, ivec3 minVal, ivec3 maxVal)
        {
            ivec3 r;

            r.x = min(max(x.x, minVal.x), maxVal.x);
            r.y = min(max(x.y, minVal.y), maxVal.y);
            r.z = min(max(x.z, minVal.z), maxVal.z);

            return r;
        }

        public static ivec4 clamp(ivec4 x, ivec4 minVal, ivec4 maxVal)
        {
            ivec4 r;

            r.x = min(max(x.x, minVal.x), maxVal.x);
            r.y = min(max(x.y, minVal.y), maxVal.y);
            r.z = min(max(x.z, minVal.z), maxVal.z);
            r.w = min(max(x.w, minVal.w), maxVal.w);

            return r;
        }


        public static uvec2 clamp(uvec2 x, uvec2 minVal, uvec2 maxVal)
        {
            uvec2 r;

            r.x = min(max(x.x, minVal.x), maxVal.x);
            r.y = min(max(x.y, minVal.y), maxVal.y);

            return r;
        }

        public static uvec3 clamp(uvec3 x, uvec3 minVal, uvec3 maxVal)
        {
            uvec3 r;

            r.x = min(max(x.x, minVal.x), maxVal.x);
            r.y = min(max(x.y, minVal.y), maxVal.y);
            r.z = min(max(x.z, minVal.z), maxVal.z);

            return r;
        }

        public static uvec4 clamp(uvec4 x, uvec4 minVal, uvec4 maxVal)
        {
            uvec4 r;

            r.x = min(max(x.x, minVal.x), maxVal.x);
            r.y = min(max(x.y, minVal.y), maxVal.y);
            r.z = min(max(x.z, minVal.z), maxVal.z);
            r.w = min(max(x.w, minVal.w), maxVal.w);

            return r;
        }
        #endregion clamp

        #region floor
        public static float floor(float x)
        {
            return (float)Math.Floor(x);
        }

        public static vec2 floor(vec2 x)
        {
            vec2 r = new vec2();

            r.x = (float)Math.Floor(x.x);
            r.y = (float)Math.Floor(x.y);

            return r;
        }

        public static vec3 floor(vec3 x)
        {
            vec3 r;

            r.x = (float)Math.Floor(x.x);
            r.y = (float)Math.Floor(x.y);
            r.z = (float)Math.Floor(x.z);

            return r;
        }

        public static vec4 floor(vec4 x)
        {
            vec4 r;

            r.x = (float)Math.Floor(x.x);
            r.y = (float)Math.Floor(x.y);
            r.z = (float)Math.Floor(x.z);
            r.w = (float)Math.Floor(x.w);

            return r;
        }
        #endregion

        #region fract
        public static float fract(float x)
        {
            return x - floor(x);
        }

        public static vec2 fract(vec2 x)
        {
            vec2 r = new vec2();

            r.x = x.x - floor(x.x);
            r.y = x.y - floor(x.y);

            return r;
        }

        public static vec3 fract(vec3 x)
        {
            vec3 r;

            r.x = x.x - floor(x.x);
            r.y = x.y - floor(x.y);
            r.z = x.z - floor(x.z);

            return r;
        }

        public static vec4 fract(vec4 x)
        {
            vec4 r;

            r.x = x.x - floor(x.x);
            r.y = x.y - floor(x.y);
            r.z = x.z - floor(x.z);
            r.w = x.w - floor(x.w);

            return r;
        }
        #endregion

        #region isnan
        public static bool isnan(float x)
        {
            return float.IsNaN(x);
        }

        public static bvec2 isnan(vec2 x)
        {
            bvec2 r;

            r.x = float.IsNaN(x.x);
            r.y = float.IsNaN(x.y);

            return r;
        }

        public static bvec3 isnan(vec3 x)
        {
            bvec3 r;

            r.x = float.IsNaN(x.x);
            r.y = float.IsNaN(x.y);
            r.z = float.IsNaN(x.z);

            return r;
        }

        public static bvec4 isnan(vec4 x)
        {
            bvec4 r;

            r.x = float.IsNaN(x.x);
            r.y = float.IsNaN(x.y);
            r.z = float.IsNaN(x.z);
            r.w = float.IsNaN(x.w);

            return r;
        }
        #endregion isnan

        #region isinf
        public static bool isinf(float x)
        {
            return float.IsInfinity(x);
        }

        public static bvec2 isinf(vec2 x)
        {
            bvec2 r;

            r.x = float.IsInfinity(x.x);
            r.y = float.IsInfinity(x.y);

            return r;
        }

        public static bvec3 isinf(vec3 x)
        {
            bvec3 r;

            r.x = float.IsInfinity(x.x);
            r.y = float.IsInfinity(x.y);
            r.z = float.IsInfinity(x.z);

            return r;
        }

        public static bvec4 isinf(vec4 x)
        {
            bvec4 r;

            r.x = float.IsInfinity(x.x);
            r.y = float.IsInfinity(x.y);
            r.z = float.IsInfinity(x.z);
            r.w = float.IsInfinity(x.w);

            return r;
        }
        #endregion isinf

        #region mod
        public static float mod(float x, float y)
        {
            return x - y * floor(x / y);
        }

        public static vec2 mod(vec2 x, float y)
        {
            vec2 r = new vec2();

            r.x = x.x - y * floor(x.x / y);
            r.y = x.y - y * floor(x.y / y);

            return r;
        }

        public static vec3 mod(vec3 x, float y)
        {
            vec3 r;

            r.x = x.x - y * floor(x.x / y);
            r.y = x.y - y * floor(x.y / y);
            r.z = x.z - y * floor(x.z / y);

            return r;
        }

        public static vec4 mod(vec4 x, float y)
        {
            vec4 r;

            r.x = x.x - y * floor(x.x / y);
            r.y = x.y - y * floor(x.y / y);
            r.z = x.z - y * floor(x.z / y);
            r.w = x.w - y * floor(x.w / y);

            return r;
        }


        public static vec2 mod(vec2 x, vec2 y)
        {
            vec2 r = new vec2();

            r.x = x.x - y.x * floor(x.x / y.x);
            r.y = x.y - y.y * floor(x.y / y.y);

            return r;
        }

        public static vec3 mod(vec3 x, vec3 y)
        {
            vec3 r;

            r.x = x.x - y.x * floor(x.x / y.x);
            r.y = x.y - y.y * floor(x.y / y.y);
            r.z = x.z - y.z * floor(x.z / y.z);

            return r;
        }

        public static vec4 mod(vec4 x, vec4 y)
        {
            vec4 r;

            r.x = x.x - y.x * floor(x.x / y.x);
            r.y = x.y - y.y * floor(x.y / y.y);
            r.z = x.z - y.z * floor(x.z / y.z);
            r.w = x.w - y.w * floor(x.w / y.w);

            return r;
        }
        #endregion

        #region max
        public static float max(float x, float y)
        {
            return x > y ? x : y;
        }

        public static vec2 max(vec2 x, vec2 y)
        {
            vec2 r = new vec2();

            r.x = max(x.x, y.x);
            r.y = max(x.y, y.y);

            return r;
        }

        public static vec3 max(vec3 x, vec3 y)
        {
            vec3 r;

            r.x = max(x.x, y.x);
            r.y = max(x.y, y.y);
            r.z = max(x.z, y.z);

            return r;
        }

        public static vec4 max(vec4 x, vec4 y)
        {
            vec4 r;

            r.x = max(x.x, y.x);
            r.y = max(x.y, y.y);
            r.z = max(x.z, y.z);
            r.w = max(x.w, y.w);

            return r;
        }


        public static int max(int x, int y)
        {
            return max(x, y);
        }

        public static ivec2 max(ivec2 x, ivec2 y)
        {
            ivec2 r;

            r.x = max(x.x, y.x);
            r.y = max(x.y, y.y);

            return r;
        }

        public static ivec3 max(ivec3 x, ivec3 y)
        {
            ivec3 r;

            r.x = max(x.x, y.x);
            r.y = max(x.y, y.y);
            r.z = max(x.z, y.z);

            return r;
        }

        public static ivec4 max(ivec4 x, ivec4 y)
        {
            ivec4 r;

            r.x = max(x.x, y.x);
            r.y = max(x.y, y.y);
            r.z = max(x.z, y.z);
            r.w = max(x.w, y.w);

            return r;
        }


        public static uint max(uint x, uint y)
        {
            return max(x, y);
        }

        public static uvec2 max(uvec2 x, uvec2 y)
        {
            uvec2 r;

            r.x = max(x.x, y.x);
            r.y = max(x.y, y.y);

            return r;
        }

        public static uvec3 max(uvec3 x, uvec3 y)
        {
            uvec3 r;

            r.x = max(x.x, y.x);
            r.y = max(x.y, y.y);
            r.z = max(x.z, y.z);

            return r;
        }

        public static uvec4 max(uvec4 x, uvec4 y)
        {
            uvec4 r;

            r.x = max(x.x, y.x);
            r.y = max(x.y, y.y);
            r.z = max(x.z, y.z);
            r.w = max(x.w, y.w);

            return r;
        }


        public static vec2 max(vec2 x, float y)
        {
            vec2 r = new vec2();

            r.x = max(x.x, y);
            r.y = max(x.y, y);

            return r;
        }

        public static vec3 max(vec3 x, float y)
        {
            vec3 r;

            r.x = max(x.x, y);
            r.y = max(x.y, y);
            r.z = max(x.z, y);

            return r;
        }

        public static vec4 max(vec4 x, float y)
        {
            vec4 r;

            r.x = max(x.x, y);
            r.y = max(x.y, y);
            r.z = max(x.z, y);
            r.w = max(x.w, y);

            return r;
        }


        public static ivec2 max(ivec2 x, int y)
        {
            ivec2 r;

            r.x = max(x.x, y);
            r.y = max(x.y, y);

            return r;
        }

        public static ivec3 max(ivec3 x, int y)
        {
            ivec3 r;

            r.x = max(x.x, y);
            r.y = max(x.y, y);
            r.z = max(x.z, y);

            return r;
        }

        public static ivec4 max(ivec4 x, int y)
        {
            ivec4 r;

            r.x = max(x.x, y);
            r.y = max(x.y, y);
            r.z = max(x.z, y);
            r.w = max(x.w, y);

            return r;
        }


        public static uvec2 max(uvec2 x, uint y)
        {
            uvec2 r;

            r.x = max(x.x, y);
            r.y = max(x.y, y);

            return r;
        }

        public static uvec3 max(uvec3 x, uint y)
        {
            uvec3 r;

            r.x = max(x.x, y);
            r.y = max(x.y, y);
            r.z = max(x.z, y);

            return r;
        }

        public static uvec4 max(uvec4 x, uint y)
        {
            uvec4 r;

            r.x = max(x.x, y);
            r.y = max(x.y, y);
            r.z = max(x.z, y);
            r.w = max(x.w, y);

            return r;
        }
        #endregion max

        #region min
        public static float min(float x, float y)
        {
            return x < y ? x : y;
        }

        public static vec2 min(vec2 x, vec2 y)
        {
            vec2 r = new vec2();

            r.x = min(x.x, y.x);
            r.y = min(x.y, y.y);

            return r;
        }

        public static vec3 min(vec3 x, vec3 y)
        {
            vec3 r;

            r.x = min(x.x, y.x);
            r.y = min(x.y, y.y);
            r.z = min(x.z, y.z);

            return r;
        }

        public static vec4 min(vec4 x, vec4 y)
        {
            vec4 r;

            r.x = min(x.x, y.x);
            r.y = min(x.y, y.y);
            r.z = min(x.z, y.z);
            r.w = min(x.w, y.w);

            return r;
        }


        public static int min(int x, int y)
        {
            return min(x, y);
        }

        public static ivec2 min(ivec2 x, ivec2 y)
        {
            ivec2 r;

            r.x = min(x.x, y.x);
            r.y = min(x.y, y.y);

            return r;
        }

        public static ivec3 min(ivec3 x, ivec3 y)
        {
            ivec3 r;

            r.x = min(x.x, y.x);
            r.y = min(x.y, y.y);
            r.z = min(x.z, y.z);

            return r;
        }

        public static ivec4 min(ivec4 x, ivec4 y)
        {
            ivec4 r;

            r.x = min(x.x, y.x);
            r.y = min(x.y, y.y);
            r.z = min(x.z, y.z);
            r.w = min(x.w, y.w);

            return r;
        }


        public static uint min(uint x, uint y)
        {
            return min(x, y);
        }

        public static uvec2 min(uvec2 x, uvec2 y)
        {
            uvec2 r;

            r.x = min(x.x, y.x);
            r.y = min(x.y, y.y);

            return r;
        }

        public static uvec3 min(uvec3 x, uvec3 y)
        {
            uvec3 r;

            r.x = min(x.x, y.x);
            r.y = min(x.y, y.y);
            r.z = min(x.z, y.z);

            return r;
        }

        public static uvec4 min(uvec4 x, uvec4 y)
        {
            uvec4 r;

            r.x = min(x.x, y.x);
            r.y = min(x.y, y.y);
            r.z = min(x.z, y.z);
            r.w = min(x.w, y.w);

            return r;
        }


        public static vec2 min(vec2 x, float y)
        {
            vec2 r = new vec2();

            r.x = min(x.x, y);
            r.y = min(x.y, y);

            return r;
        }

        public static vec3 min(vec3 x, float y)
        {
            vec3 r;

            r.x = min(x.x, y);
            r.y = min(x.y, y);
            r.z = min(x.z, y);

            return r;
        }

        public static vec4 min(vec4 x, float y)
        {
            vec4 r;

            r.x = min(x.x, y);
            r.y = min(x.y, y);
            r.z = min(x.z, y);
            r.w = min(x.w, y);

            return r;
        }


        public static ivec2 min(ivec2 x, int y)
        {
            ivec2 r;

            r.x = min(x.x, y);
            r.y = min(x.y, y);

            return r;
        }

        public static ivec3 min(ivec3 x, int y)
        {
            ivec3 r;

            r.x = min(x.x, y);
            r.y = min(x.y, y);
            r.z = min(x.z, y);

            return r;
        }

        public static ivec4 min(ivec4 x, int y)
        {
            ivec4 r;

            r.x = min(x.x, y);
            r.y = min(x.y, y);
            r.z = min(x.z, y);
            r.w = min(x.w, y);

            return r;
        }


        public static uvec2 min(uvec2 x, uint y)
        {
            uvec2 r;

            r.x = min(x.x, y);
            r.y = min(x.y, y);

            return r;
        }

        public static uvec3 min(uvec3 x, uint y)
        {
            uvec3 r;

            r.x = min(x.x, y);
            r.y = min(x.y, y);
            r.z = min(x.z, y);

            return r;
        }

        public static uvec4 min(uvec4 x, uint y)
        {
            uvec4 r;

            r.x = min(x.x, y);
            r.y = min(x.y, y);
            r.z = min(x.z, y);
            r.w = min(x.w, y);

            return r;
        }
        #endregion min

        #region mix
        public static float mix(float x, float y, float a)
        {
            return x * (1.0f - a) + y * a;
        }

        public static vec2 mix(vec2 x, vec2 y, float a)
        {
            vec2 r = new vec2();

            r.x = x.x * (1.0f - a) + y.x * a;
            r.y = x.y * (1.0f - a) + y.y * a;

            return r;
        }

        public static vec3 mix(vec3 x, vec3 y, float a)
        {
            vec3 r;

            r.x = x.x * (1.0f - a) + y.x * a;
            r.y = x.y * (1.0f - a) + y.y * a;
            r.z = x.z * (1.0f - a) + y.z * a;

            return r;
        }

        public static vec4 mix(vec4 x, vec4 y, float a)
        {
            vec4 r;

            r.x = x.x * (1.0f - a) + y.x * a;
            r.y = x.y * (1.0f - a) + y.y * a;
            r.z = x.z * (1.0f - a) + y.z * a;
            r.w = x.w * (1.0f - a) + y.w * a;

            return r;
        }


        public static vec2 mix(vec2 x, vec2 y, vec2 a)
        {
            vec2 r = new vec2();

            r.x = x.x * (1.0f - a.x) + y.x * a.x;
            r.y = x.y * (1.0f - a.y) + y.y * a.y;

            return r;
        }

        public static vec3 mix(vec3 x, vec3 y, vec3 a)
        {
            vec3 r;

            r.x = x.x * (1.0f - a.x) + y.x * a.x;
            r.y = x.y * (1.0f - a.y) + y.y * a.y;
            r.z = x.z * (1.0f - a.z) + y.z * a.z;

            return r;
        }

        public static vec4 mix(vec4 x, vec4 y, vec4 a)
        {
            vec4 r;

            r.x = x.x * (1.0f - a.x) + y.x * a.x;
            r.y = x.y * (1.0f - a.y) + y.y * a.y;
            r.z = x.z * (1.0f - a.z) + y.z * a.z;
            r.w = x.w * (1.0f - a.w) + y.w * a.w;

            return r;
        }
        #endregion mix

        #region round
        public static float round(float x)
        {
            return (float)Math.Round(x);
        }

        public static vec2 round(vec2 x)
        {
            vec2 r = new vec2();

            r.x = (float)Math.Round(x.x);
            r.y = (float)Math.Round(x.y);

            return r;
        }

        public static vec3 round(vec3 x)
        {
            vec3 r;

            r.x = (float)Math.Round(x.x);
            r.y = (float)Math.Round(x.y);
            r.z = (float)Math.Round(x.z);

            return r;
        }

        public static vec4 round(vec4 x)
        {
            vec4 r;

            r.x = (float)Math.Round(x.x);
            r.y = (float)Math.Round(x.y);
            r.z = (float)Math.Round(x.z);
            r.w = (float)Math.Round(x.w);

            return r;
        }
        #endregion

        #region roundeven
        public static float roundeven(float x)
        {
            return (float)Math.Round(x, MidpointRounding.ToEven);
        }

        public static vec2 roundeven(vec2 x)
        {
            vec2 r = new vec2();

            r.x = (float)Math.Round(x.x, MidpointRounding.ToEven);
            r.y = (float)Math.Round(x.y, MidpointRounding.ToEven);

            return r;
        }

        public static vec3 roundeven(vec3 x)
        {
            vec3 r;

            r.x = (float)Math.Round(x.x, MidpointRounding.ToEven);
            r.y = (float)Math.Round(x.y, MidpointRounding.ToEven);
            r.z = (float)Math.Round(x.z, MidpointRounding.ToEven);

            return r;
        }

        public static vec4 roundeven(vec4 x)
        {
            vec4 r;

            r.x = (float)Math.Round(x.x, MidpointRounding.ToEven);
            r.y = (float)Math.Round(x.y, MidpointRounding.ToEven);
            r.z = (float)Math.Round(x.z, MidpointRounding.ToEven);
            r.w = (float)Math.Round(x.w, MidpointRounding.ToEven);

            return r;
        }
        #endregion

        #region sign
        public static float sign(float x)
        {
            return Math.Sign(x);
        }

        public static vec2 sign(vec2 x)
        {
            vec2 r = new vec2();

            r.x = Math.Sign(x.x);
            r.y = Math.Sign(x.y);

            return r;
        }

        public static vec3 sign(vec3 x)
        {
            vec3 r;

            r.x = Math.Sign(x.x);
            r.y = Math.Sign(x.y);
            r.z = Math.Sign(x.z);

            return r;
        }

        public static vec4 sign(vec4 x)
        {
            vec4 r;

            r.x = Math.Sign(x.x);
            r.y = Math.Sign(x.y);
            r.z = Math.Sign(x.z);
            r.w = Math.Sign(x.w);

            return r;
        }

        public static int sign(int x)
        {
            return Math.Sign(x);
        }

        public static ivec2 sign(ivec2 x)
        {
            ivec2 r;

            r.x = Math.Sign(x.x);
            r.y = Math.Sign(x.y);

            return r;
        }

        public static ivec3 sign(ivec3 x)
        {
            ivec3 r;

            r.x = Math.Sign(x.x);
            r.y = Math.Sign(x.y);
            r.z = Math.Sign(x.z);

            return r;
        }

        public static ivec4 sign(ivec4 x)
        {
            ivec4 r;

            r.x = Math.Sign(x.x);
            r.y = Math.Sign(x.y);
            r.z = Math.Sign(x.z);
            r.w = Math.Sign(x.w);

            return r;
        }
        #endregion

        #region step
        public static float step(float edge, float x)
        {
            return x < edge ? 0.0f : 1.0f;
        }

        public static vec2 step(vec2 edge, vec2 x)
        {
            vec2 r = new vec2();

            r.x = x.x < edge.x ? 0.0f : 1.0f;
            r.y = x.y < edge.y ? 0.0f : 1.0f;

            return r;
        }

        public static vec3 step(vec3 edge, vec3 x)
        {
            vec3 r;

            r.x = x.x < edge.x ? 0.0f : 1.0f;
            r.y = x.y < edge.y ? 0.0f : 1.0f;
            r.z = x.z < edge.z ? 0.0f : 1.0f;

            return r;
        }

        public static vec4 step(vec4 edge, vec4 x)
        {
            vec4 r;

            r.x = x.x < edge.x ? 0.0f : 1.0f;
            r.y = x.y < edge.y ? 0.0f : 1.0f;
            r.z = x.z < edge.z ? 0.0f : 1.0f;
            r.w = x.w < edge.w ? 0.0f : 1.0f;

            return r;
        }


        public static vec2 step(float edge, vec2 x)
        {
            vec2 r = new vec2();

            r.x = x.x < edge ? 0.0f : 1.0f;
            r.y = x.y < edge ? 0.0f : 1.0f;

            return r;
        }

        public static vec3 step(float edge, vec3 x)
        {
            vec3 r;

            r.x = x.x < edge ? 0.0f : 1.0f;
            r.y = x.y < edge ? 0.0f : 1.0f;
            r.z = x.z < edge ? 0.0f : 1.0f;

            return r;
        }

        public static vec4 step(float edge, vec4 x)
        {
            vec4 r;

            r.x = x.x < edge ? 0.0f : 1.0f;
            r.y = x.y < edge ? 0.0f : 1.0f;
            r.z = x.z < edge ? 0.0f : 1.0f;
            r.w = x.w < edge ? 0.0f : 1.0f;

            return r;
        }
        #endregion step

        #region smoothstep
        static float herm(float edge0, float edge1, float x)
        {
            float range = edge1 - edge0;
            float distance = x - edge0;

            float t;
            t = clamp((distance / range), 0.0f, 1.0f);

            float r = t * t * (3.0f - 2.0f * t);

            return r;
        }

        public static float smoothstep(float edge0, float edge1, float x)
        {
            if (x <= edge0)
                return 0.0f;

            if (x >= edge1)
                return 1.0f;

            return herm(edge0, edge1, x);
        }

        public static vec2 smoothstep(vec2 edge0, vec2 edge1, vec2 x)
        {
            vec2 r = new vec2();

            r.x = smoothstep(edge0.x, edge1.x, x.x);
            r.y = smoothstep(edge0.y, edge1.y, x.y);

            return r;
        }

        public static vec3 smoothstep(vec3 edge0, vec3 edge1, vec3 x)
        {
            vec3 r;

            r.x = smoothstep(edge0.x, edge1.x, x.x);
            r.y = smoothstep(edge0.y, edge1.y, x.y);
            r.z = smoothstep(edge0.z, edge1.z, x.z);

            return r;
        }

        public static vec4 smoothstep(vec4 edge0, vec4 edge1, vec4 x)
        {
            vec4 r;

            r.x = smoothstep(edge0.x, edge1.x, x.x);
            r.y = smoothstep(edge0.y, edge1.y, x.y);
            r.z = smoothstep(edge0.z, edge1.z, x.z);
            r.w = smoothstep(edge0.w, edge1.w, x.w);

            return r;
        }


        public static vec2 smoothstep(float edge0, float edge1, vec2 x)
        {
            vec2 r = new vec2();

            r.x = smoothstep(edge0, edge1, x.x);
            r.y = smoothstep(edge0, edge1, x.y);

            return r;
        }

        public static vec3 smoothstep(float edge0, float edge1, vec3 x)
        {
            vec3 r;

            r.x = smoothstep(edge0, edge1, x.x);
            r.y = smoothstep(edge0, edge1, x.y);
            r.z = smoothstep(edge0, edge1, x.z);

            return r;
        }

        public static vec4 smoothstep(float edge0, float edge1, vec4 x)
        {
            vec4 r;

            r.x = smoothstep(edge0, edge1, x.x);
            r.y = smoothstep(edge0, edge1, x.y);
            r.z = smoothstep(edge0, edge1, x.z);
            r.w = smoothstep(edge0, edge1, x.w);

            return r;
        }
        #endregion smoothstep

        #region trunc
        public static float trunc(float x)
        {
            return (float)Math.Truncate(x);
        }

        public static vec2 trunc(vec2 x)
        {
            vec2 r = new vec2();

            r.x = (float)Math.Truncate(x.x);
            r.y = (float)Math.Truncate(x.y);

            return r;
        }

        public static vec3 trunc(vec3 x)
        {
            vec3 r;

            r.x = (float)Math.Truncate(x.x);
            r.y = (float)Math.Truncate(x.y);
            r.z = (float)Math.Truncate(x.z);

            return r;
        }

        public static vec4 trunc(vec4 x)
        {
            vec4 r;

            r.x = (float)Math.Truncate(x.x);
            r.y = (float)Math.Truncate(x.y);
            r.z = (float)Math.Truncate(x.z);
            r.w = (float)Math.Truncate(x.w);

            return r;
        }
        #endregion

        #endregion

        #region Geometric Functions (5.4)
        #region length
        public static float length(float x)
        {
            return abs(x);
        }

        public static float length(vec2 x)
        {
            float r = sqrt(x.x * x.x + x.y * x.y);

            return r;
        }

        public static float length(vec3 x)
        {
            float r = sqrt(x.x * x.x + x.y * x.y + x.z*x.z);

            return r;
        }

        public static float length(vec4 x)
        {
            float r = sqrt(x.x * x.x + x.y * x.y + x.z * x.z + x.w*x.w);

            return r;
        }
        #endregion

        #region distance
        public static float distance(float p0, float p1)
        {
            return Math.Abs(p0 - p1);
        }

        public static float distance(vec2 p0, vec2 p1)
        {
            return length(p0 - p1);
        }

        public static float distance(vec3 p0, vec3 p1)
        {
            return length(p0 - p1);
        }

        public static float distance(vec4 p0, vec4 p1)
        {
            return length(p0 - p1);
        }
        #endregion

        #region dot
        public static float dot(float x, float y)
        {
            return x * y;
        }

        public static float dot(vec2 x, vec2 y)
        {
            return x.x * y.x + x.y * y.y;
        }

        public static float dot(vec3 x, vec3 y)
        {
            return x.x * y.x + x.y * y.y + x.z*y.z;
        }

        public static float dot(vec4 x, vec4 y)
        {
            return x.x * y.x + x.y * y.y + x.z * y.z + x.w * y.w;
        }
        #endregion dot

        #region cross
        public static vec3 cross(vec3 x, vec3 y)
        {
            vec3 r;

            r.x = (float)(x[1] * y[2] - y[1] * x[2]);
            r.y = (float)(x[2] * y[0] - y[2] * x[0]);
            r.z = (float)(x[0] * y[1] - y[0] * x[1]);

            return r;
        }
        #endregion

        #region normalize
        public static float normalize(float x)
        {
            return 1.0f;
        }

        public static vec2 normalize(vec2 x)
        {
            float vlength = length(x);

            vec2 r = new vec2();
            
            r.x = x.x / vlength;
            r.y = x.y / vlength;
            
            return r;
        }

        public static vec3 normalize(vec3 x)
        {
            float vlength = length(x);

            vec3 r;

            r.x = x.x / vlength;
            r.y = x.y / vlength;
            r.z = x.z / vlength;

            return r;
        }

        public static vec4 normalize(vec4 x)
        {
            float vlength = length(x);

            vec4 r;

            r.x = x.x / vlength;
            r.y = x.y / vlength;
            r.z = x.z / vlength;
            r.w = x.w / vlength;

            return r;
        }
        #endregion

        #region faceforward
        public static float faceforward(float N, float I, float Nref)
        {
            if (dot(Nref, I) < 0.0)
                return N;

            return -N;
        }

        public static vec2 faceforward(vec2 N, vec2 I, vec2 Nref)
        {
            if (dot(Nref, I) < 0.0)
                return N;

            return -N;
        }

        public static vec3 faceforward(vec3 N, vec3 I, vec3 Nref)
        {
            if (dot(Nref, I) < 0.0)
                return N;

            return -N;
        }

        public static vec4 faceforward(vec4 N, vec4 I, vec4 Nref)
        {
            if (dot(Nref, I) < 0.0)
                return N;

            return -N;
        }
        #endregion

        #region reflect
        public static float reflect(float I, float N)
        {
            float r = I - 2.0f * dot(N, I) * N;

            return r;
        }

        public static vec2 reflect(vec2 I, vec2 N)
        {
            vec2 r = I - 2.0f * dot(N, I) * N;

            return r;
        }

        public static vec3 reflect(vec3 I, vec3 N)
        {
            vec3 r = I - 2.0f * dot(N, I) * N;

            return r;
        }

        public static vec4 reflect(vec4 I, vec4 N)
        {
            vec4 r = I - 2.0f * dot(N, I) * N;

            return r;
        }
        #endregion

        #region refract
        public static float refract(float I, float N, float eta)
        {
            float r = 0;

            float dotNI = dot(N, I);

            float k = 1.0f - eta * eta * (1.0f - dotNI * dotNI);

            if (k < 0.0f)
                return r;

            r = eta * I - (eta * dotNI * sqrt(k)) * N;

            return r;
        }

        public static vec2 refract(vec2 I, vec2 N, float eta)
        {
            vec2 r = new vec2();

            float dotNI = dot(N, I);

            float k = 1.0f - eta * eta * (1.0f - dotNI * dotNI);

            if (k < 0.0f)
                return r;

            r = eta * I - (eta * dotNI * sqrt(k)) * N;

            return r;
        }

        public static vec3 refract(vec3 I, vec3 N, float eta)
        {
            vec3 r = new vec3();

            float dotNI = dot(N, I);

            float k = 1.0f - eta * eta * (1.0f - dotNI * dotNI);

            if (k < 0.0f)
                return r;

            r = eta * I - (eta * dotNI * sqrt(k)) * N;

            return r;
        }

        public static vec4 refract(vec4 I, vec4 N, float eta)
        {
            vec4 r = new vec4();

            float dotNI = dot(N, I);

            float k = 1.0f - eta * eta * (1.0f - dotNI * dotNI);

            if (k < 0.0f)
                return r;

            r = eta * I - (eta * dotNI * sqrt(k)) * N;

            return r;
        }
        #endregion refract
        #endregion

        #region Matrix Functions (5.5)
        #region matrixCompMult
        public static mat2 matrixCompMult(mat2 x, mat2 y)
        {
            mat2 r = new mat2(MatrixSquare.ComponentMultiply(x, y));
            
            return r;
        }

        public static mat3 matrixCompMult(mat3 x, mat3 y)
        {
            mat3 r = new mat3(MatrixSquare.ComponentMultiply(x, y));

            return r;
        }

        public static mat4 matrixCompMult(mat4 x, mat4 y)
        {
            mat4 r = new mat4(MatrixSquare.ComponentMultiply(x, y));

            return r;
        }
        #endregion

        #region transpose
        public static mat2 transpose(mat2 m)
        {
            return (mat2)m.Transpose;
        }

        public static mat3 transpose(mat3 m)
        {
            return (mat3)m.Transpose;
        }

        public static mat4 transpose(mat4 m)
        {
            return (mat4)m.Transpose;
        }

        public static mat2x3 transpose(mat3x2 m)
        {
            return new mat2x3(m.Transpose);
        }

        public static mat3x2 transpose(mat2x3 m)
        {
            return new mat3x2(m.Transpose);
        }

        public static mat2x4 transpose(mat4x2 m)
        {
            return new mat2x4(m.Transpose);
        }

        public static mat4x2 transpose(mat2x4 m)
        {
            return new mat4x2(m.Transpose);
        }

        public static mat3x4 transpose(mat4x3 m)
        {
            return new mat3x4(m.Transpose);
        }

        public static mat4x3 transpose(mat3x4 m)
        {
            return new mat4x3(m.Transpose);
        }
        #endregion transpose

        #region inverse
        public static mat2 inverse(mat2 m)
        {
            return m.Inverse;
        }

        public static mat3 inverse(mat3 m)
        {
            double[,] i = m.GetInverse();
            mat3 r = new mat3(i);

            return r;
        }

        public static mat4 inverse(mat4 m)
        {
            double[,] i = m.GetInverse();
            mat4 r = new mat4(i);

            return r;
        }
        #endregion

        #region outerProduct
        public static mat2 outerProduct(vec2 c, vec2 r)
        {
            mat2 a = new mat2();

            a[0,0] = c.x * r.x; a[1,0] = c.x * r.y;
            a[0,1] = c.y * r.x; a[1,1] = c.y * r.y;

            return a;
        }

        public static mat3 outerProduct(vec3 c, vec3 r)
        {
            mat3 a = new mat3();

            a[0, 0] = c.x * r.x; a[1, 0] = c.x * r.y; a[2, 0] = c.x * r.z;
            a[0, 1] = c.y * r.x; a[1, 1] = c.y * r.y; a[2, 1] = c.y * r.z;
            a[0, 2] = c.z * r.x; a[1, 2] = c.z * r.y; a[2, 2] = c.z * r.z;

            return a;
        }

        public static mat4 outerProcuct(vec4 c, vec4 r)
        {
            mat4 a = new mat4();

            a[0,0] = c.x * r.x; a[1,0] = c.x * r.y; a[2,0] = c.x * r.z; a[3,0] = c.x * r.w;
            a[0,1] = c.y * r.x; a[1,1] = c.y * r.y; a[2,1] = c.y * r.z; a[3,1] = c.y * r.w;
            a[0,2] = c.z * r.x; a[1,2] = c.z * r.y; a[2,2] = c.z * r.z; a[3,2] = c.z * r.w;
            a[0,3] = c.w * r.x; a[1,3] = c.w * r.y; a[2,3] = c.w * r.z; a[3,3] = c.w * r.w;

            return a;
        }
        #endregion

        //public static mat2x3 outerProduct(vec2 c, vec3 r)
        //{
        //}

        //public static mat3x2 outerProduct(vec3 c, vec2 r)
        //{
        //}

        //public static mat2x4 outerProduct(vec2 c, vec4 r)
        //{
        //}

        //public static mat4x2 outerProduct(vec4 c, vec2 r)
        //{
        //}

        //public static mat3x4 outerProduct(vec3 c, vec4 r)
        //{
        //}

        //public static mat4x3 outerProduct(vec4 c, vec3 r)
        //{
        //}
        #endregion Matrix Functions

        #region Vector Relational Functions (5.6)
        #region lessThan
        public static bvec2 lessThan(vec2 x, vec2 y)
        {
            bvec2 r;

            r.x = x.x < y.x;
            r.y = x.y < y.y;

            return r;
        }

        public static bvec3 lessThan(vec3 x, vec3 y)
        {
            bvec3 r;

            r.x = x.x < y.x;
            r.y = x.y < y.y;
            r.z = x.z < y.z;

            return r;
        }

        public static bvec4 lessThan(vec4 x, vec4 y)
        {
            bvec4 r;

            r.x = x.x < y.x;
            r.y = x.y < y.y;
            r.z = x.z < y.z;
            r.w = x.w < y.w;

            return r;
        }

        public static bvec2 lessThan(ivec2 x, ivec2 y)
        {
            bvec2 r;

            r.x = x.x < y.x;
            r.y = x.y < y.y;

            return r;
        }

        public static bvec3 lessThan(ivec3 x, ivec3 y)
        {
            bvec3 r;

            r.x = x.x < y.x;
            r.y = x.y < y.y;
            r.z = x.z < y.z;

            return r;
        }

        public static bvec4 lessThan(ivec4 x, ivec4 y)
        {
            bvec4 r;

            r.x = x.x < y.x;
            r.y = x.y < y.y;
            r.z = x.z < y.z;
            r.w = x.w < y.w;

            return r;
        }
        #endregion

        #region lessThanEqual
        public static bvec2 lessThanEqual(vec2 x, vec2 y)
        {
            bvec2 r;

            r.x = x.x <= y.x;
            r.y = x.y <= y.y;

            return r;
        }

        public static bvec3 lessThanEqual(vec3 x, vec3 y)
        {
            bvec3 r;

            r.x = x.x <= y.x;
            r.y = x.y <= y.y;
            r.z = x.z <= y.z;

            return r;
        }

        public static bvec4 lessThanEqual(vec4 x, vec4 y)
        {
            bvec4 r;

            r.x = x.x <= y.x;
            r.y = x.y <= y.y;
            r.z = x.z <= y.z;
            r.w = x.w <= y.w;

            return r;
        }

        public static bvec2 lessThanEqual(ivec2 x, ivec2 y)
        {
            bvec2 r;

            r.x = x.x <= y.x;
            r.y = x.y <= y.y;

            return r;
        }

        public static bvec3 lessThanEqual(ivec3 x, ivec3 y)
        {
            bvec3 r;

            r.x = x.x <= y.x;
            r.y = x.y <= y.y;
            r.z = x.z <= y.z;

            return r;
        }

        public static bvec4 lessThanEqual(ivec4 x, ivec4 y)
        {
            bvec4 r;

            r.x = x.x <= y.x;
            r.y = x.y <= y.y;
            r.z = x.z <= y.z;
            r.w = x.w <= y.w;

            return r;
        }
        #endregion

        #region greaterThan
        public static bvec2 greaterThan(vec2 x, vec2 y)
        {
            bvec2 r;

            r.x = x.x > y.x;
            r.y = x.y > y.y;

            return r;
        }

        public static bvec3 greaterThan(vec3 x, vec3 y)
        {
            bvec3 r;

            r.x = x.x > y.x;
            r.y = x.y > y.y;
            r.z = x.z > y.z;

            return r;
        }

        public static bvec4 greaterThan(vec4 x, vec4 y)
        {
            bvec4 r;

            r.x = x.x > y.x;
            r.y = x.y > y.y;
            r.z = x.z > y.z;
            r.w = x.w > y.w;

            return r;
        }

        public static bvec2 greaterThan(ivec2 x, ivec2 y)
        {
            bvec2 r;

            r.x = x.x > y.x;
            r.y = x.y > y.y;

            return r;
        }

        public static bvec3 greaterThan(ivec3 x, ivec3 y)
        {
            bvec3 r;

            r.x = x.x > y.x;
            r.y = x.y > y.y;
            r.z = x.z > y.z;

            return r;
        }

        public static bvec4 greaterThan(ivec4 x, ivec4 y)
        {
            bvec4 r;

            r.x = x.x > y.x;
            r.y = x.y > y.y;
            r.z = x.z > y.z;
            r.w = x.w > y.w;

            return r;
        }
        #endregion greaterThan

        #region greaterThanEqual
        public static bvec2 greaterThanEqual(vec2 x, vec2 y)
        {
            bvec2 r;

            r.x = x.x >= y.x;
            r.y = x.y >= y.y;

            return r;
        }

        public static bvec3 greaterThanEqual(vec3 x, vec3 y)
        {
            bvec3 r;

            r.x = x.x >= y.x;
            r.y = x.y >= y.y;
            r.z = x.z >= y.z;

            return r;
        }

        public static bvec4 greaterThanEqual(vec4 x, vec4 y)
        {
            bvec4 r;

            r.x = x.x >= y.x;
            r.y = x.y >= y.y;
            r.z = x.z >= y.z;
            r.w = x.w >= y.w;

            return r;
        }

        public static bvec2 greaterThanEqual(ivec2 x, ivec2 y)
        {
            bvec2 r;

            r.x = x.x >= y.x;
            r.y = x.y >= y.y;

            return r;
        }

        public static bvec3 greaterThanEqual(ivec3 x, ivec3 y)
        {
            bvec3 r;

            r.x = x.x >= y.x;
            r.y = x.y >= y.y;
            r.z = x.z >= y.z;

            return r;
        }

        public static bvec4 greaterThanEqual(ivec4 x, ivec4 y)
        {
            bvec4 r;

            r.x = x.x >= y.x;
            r.y = x.y >= y.y;
            r.z = x.z >= y.z;
            r.w = x.w >= y.w;

            return r;
        }
        #endregion greaterThanEqual

        #region equal
        public static bvec2 equal(vec2 x, vec2 y)
        {
            bvec2 r;

            r.x = x.x == y.x;
            r.y = x.y == y.y;

            return r;
        }

        public static bvec3 equal(vec3 x, vec3 y)
        {
            bvec3 r;

            r.x = x.x == y.x;
            r.y = x.y == y.y;
            r.z = x.z == y.z;

            return r;
        }

        public static bvec4 equal(vec4 x, vec4 y)
        {
            bvec4 r;

            r.x = x.x == y.x;
            r.y = x.y == y.y;
            r.z = x.z == y.z;
            r.w = x.w == y.w;

            return r;
        }

        public static bvec2 equal(ivec2 x, ivec2 y)
        {
            bvec2 r;

            r.x = x.x == y.x;
            r.y = x.y == y.y;

            return r;
        }

        public static bvec3 equal(ivec3 x, ivec3 y)
        {
            bvec3 r;

            r.x = x.x == y.x;
            r.y = x.y == y.y;
            r.z = x.z == y.z;

            return r;
        }

        public static bvec4 equal(ivec4 x, ivec4 y)
        {
            bvec4 r;

            r.x = x.x == y.x;
            r.y = x.y == y.y;
            r.z = x.z == y.z;
            r.w = x.w == y.w;

            return r;
        }


        public static bvec2 equal(bvec2 x, bvec2 y)
        {
            bvec2 r;

            r.x = x.x == y.x;
            r.y = x.y == y.y;

            return r;
        }

        public static bvec3 equal(bvec3 x, bvec3 y)
        {
            bvec3 r;

            r.x = x.x == y.x;
            r.y = x.y == y.y;
            r.z = x.z == y.z;

            return r;
        }

        public static bvec4 equal(bvec4 x, bvec4 y)
        {
            bvec4 r;

            r.x = x.x == y.x;
            r.y = x.y == y.y;
            r.z = x.z == y.z;
            r.w = x.w == y.w;

            return r;
        }

        #endregion equal

        #region notEqual
        public static bvec2 notEqual(vec2 x, vec2 y)
        {
            bvec2 r;

            r.x = x.x != y.x;
            r.y = x.y != y.y;

            return r;
        }

        public static bvec3 notEqual(vec3 x, vec3 y)
        {
            bvec3 r;

            r.x = x.x != y.x;
            r.y = x.y != y.y;
            r.z = x.z != y.z;

            return r;
        }

        public static bvec4 notEqual(vec4 x, vec4 y)
        {
            bvec4 r;

            r.x = x.x != y.x;
            r.y = x.y != y.y;
            r.z = x.z != y.z;
            r.w = x.w != y.w;

            return r;
        }

        public static bvec2 notEqual(ivec2 x, ivec2 y)
        {
            bvec2 r;

            r.x = x.x != y.x;
            r.y = x.y != y.y;

            return r;
        }

        public static bvec3 notEqual(ivec3 x, ivec3 y)
        {
            bvec3 r;

            r.x = x.x != y.x;
            r.y = x.y != y.y;
            r.z = x.z != y.z;

            return r;
        }

        public static bvec4 notEqual(ivec4 x, ivec4 y)
        {
            bvec4 r;

            r.x = x.x != y.x;
            r.y = x.y != y.y;
            r.z = x.z != y.z;
            r.w = x.w != y.w;

            return r;
        }


        public static bvec2 notEqual(bvec2 x, bvec2 y)
        {
            bvec2 r;

            r.x = x.x != y.x;
            r.y = x.y != y.y;

            return r;
        }

        public static bvec3 notEqual(bvec3 x, bvec3 y)
        {
            bvec3 r;

            r.x = x.x != y.x;
            r.y = x.y != y.y;
            r.z = x.z != y.z;

            return r;
        }

        public static bvec4 notEqual(bvec4 x, bvec4 y)
        {
            bvec4 r;

            r.x = x.x != y.x;
            r.y = x.y != y.y;
            r.z = x.z != y.z;
            r.w = x.w != y.w;

            return r;
        }

        #endregion notEqual

        #region any
        public static bool any(bvec2 x)
        {
            return x.x || x.y;
        }

        public static bool any(bvec3 x)
        {
            return x.x || x.y || x.z;
        }

        public static bool any(bvec4 x)
        {
            return x.x || x.y || x.z || x.w;
        }
        #endregion

        #region all
        public static bool all(bvec2 x)
        {
            return x.x && x.y;
        }

        public static bool all(bvec3 x)
        {
            return x.x && x.y && x.z;
        }

        public static bool all(bvec4 x)
        {
            return x.x && x.y && x.z && x.w;
        }
        #endregion

        #region not
        public static bvec2 not(bvec2 x)
        {
            bvec2 r;

            r.x = !x.x;
            r.y = !x.y;

            return r;
        }

        public static bvec3 not(bvec3 x)
        {
            bvec3 r;

            r.x = !x.x;
            r.y = !x.y;
            r.z = !x.z;

            return r;
        }

        public static bvec4 not(bvec4 x)
        {
            bvec4 r;

            r.x = !x.x;
            r.y = !x.y;
            r.z = !x.z;
            r.w = !x.w;
            
            return r;
        }
        #endregion

        #endregion Vector Relational Functions

        #region Texture Access Functions (5.7)
        public static vec4 texture(sampler1D sampler, float coord)
        {
            return new vec4();
        }

        public static vec4 texture(sampler2D sampler, vec2 coord)
        {
            return new vec4();
        }

        public static vec4 texture(sampler3D sampler, vec3 coord)
        {
            return new vec4();
        }
        #endregion

        #region Noise Functions (5.9)
        #region noise1
        public static float noise1(float x)
        {
            return Noise.Noise1(x);
        }

        public static float noise1(vec2 x)
        {
            return Noise.Noise2(x.x, x.y);
        }

        public static float noise1(vec3 x)
        {
            return Noise.Noise3(x.x, x.y, x.z);
        }

        public static float noise1(vec4 x)
        {
            return Noise.Noise4(x.x, x.y, x.z, x.w);
        }
        #endregion noise1

        #region noise2
        public static vec2 noise2(float x)
        {
            vec2 r = new vec2();
            float nValue = Noise.Noise1(x);

            r.x = nValue;
            r.y = nValue;

            return r;
        }

        public static vec2 noise2(vec2 x)
        {
            vec2 r = new vec2();

            r.x = Noise.Noise1(x.x);
            r.y = Noise.Noise1(x.y);

            return r;
        }

        public static vec2 noise2(vec3 x)
        {
            vec2 r = new vec2();

            r.x = Noise.Noise3(x.x, x.y, x.z);
            r.y = Noise.Noise3(x.x, x.y, x.z);

            return r;
        }

        public static vec2 noise2(vec4 x)
        {
            vec2 r = new vec2();

            r.x = Noise.Noise4(x.x, x.y, x.z, x.w);
            r.y = Noise.Noise4(x.x, x.y, x.z, x.w);

            return r;
        }

        #endregion noise2

        #region noise3
        public static vec3 noise3(float x)
        {
            vec3 r;
            float nValue = Noise.Noise1(x);

            r.x = nValue;
            r.y = nValue;
            r.z = nValue;

            return r;
        }

        public static vec3 noise3(vec2 x)
        {
            vec3 r;
            float nValue = Noise.Noise2(x.x, x.y);

            r.x = nValue;
            r.y = nValue;
            r.z = nValue;

            return r;
        }

        public static vec3 noise3(vec3 x)
        {
            vec3 r;

            r.x = Noise.Noise1(x.x);
            r.y = Noise.Noise1(x.y);
            r.z = Noise.Noise1(x.z);

            return r;
        }

        public static vec3 noise3(vec4 x)
        {
            vec3 r;
            float nValue = Noise.Noise4(x.x, x.y, x.z, x.w);

            r.x = nValue;
            r.y = nValue;
            r.z = nValue;

            return r;
        }
        #endregion noise3

        #region noise4
        public static vec4 noise4(float x)
        {
            vec4 r;
            float nValue = Noise.Noise1(x);

            r.x = nValue;
            r.y = nValue;
            r.z = nValue;
            r.w = nValue;

            return r;
        }

        public static vec4 noise4(vec2 x)
        {
            vec4 r;
            float nValue = Noise.Noise2(x.x, x.y);

            r.x = nValue;
            r.y = nValue;
            r.z = nValue;
            r.w = nValue;

            return r;
        }

        public static vec4 noise4(vec3 x)
        {
            vec4 r;
            float nValue = Noise.Noise3(x.x, x.y, x.z);

            r.x = nValue;
            r.y = nValue;
            r.z = nValue;
            r.w = nValue;

            return r;
        }

        public static vec4 noise4(vec4 x)
        {
            vec4 r;
            float nValue = Noise.Noise3(x.x, x.y, x.z);

            r.x = Noise.Noise1(x.x);
            r.y = Noise.Noise1(x.y);
            r.z = Noise.Noise1(x.z);
            r.w = Noise.Noise1(x.w);

            return r;
        }
        #endregion

        #endregion
    }
}