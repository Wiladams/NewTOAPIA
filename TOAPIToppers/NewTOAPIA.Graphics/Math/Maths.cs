namespace NewTOAPIA.Graphics
{
    public static class Maths
    {
        public const double PI = 3.1415926535897932384;
        public const double TWO_PI = 6.2831853071795864769;
        public const double PI_ON_180 = 0.0174532925199432957;
        public const double invPI = 0.3183098861837906715;
        public const double invTWO_PI = 0.1591549430918953358;

        public const double kEpsilon = 0.0001;
        public const double kHugeValue = 1.0E10;

        /// <summary>
        /// Returns an approximation of the inverse square root of a number.
        /// </summary>
        /// <param name="x">A number.</param>
        /// <returns>An approximation of the inverse square root of the specified number, with an upper error bound of 0.001</returns>
        /// <remarks>
        /// This is an improved implementation of the the method known as Carmack's inverse square root
        /// which is found in the Quake III source code. This implementation comes from
        /// http://www.codemaestro.com/reviews/review00000105.html. For the history of this method, see
        /// http://www.beyond3d.com/content/articles/8/
        /// </remarks>
        /// WAA - This can only be used if compiling with unsafe code.
        //public static float InverseSqrtFast(float x)
        //{
        //    unsafe
        //    {

        //        float xhalf = 0.5f * x;
        //        int i = *(int*)&x;              // Read bits as integer.
        //        i = 0x5f375a86 - (i >> 1);      // Make an initial guess for Newton-Raphson approximation
        //        x = *(float*)&i;                // Convert bits back to float
        //        x = x * (1.5f - xhalf * x * x); // Perform a single Newton-Raphson step.
        //        return x;
        //    }
        //}

        /// <summary>
        /// Convert degrees to Radians.  There are 2*PI radians in a circle, or
        /// PI radians per 180 degrees.  So, some simple multiplication and 
        /// division.
        /// </summary>
        /// <param name="degrees">Value of angle to be converted expressed in degrees.</param>
        /// <returns>Angle converted to radians</returns>
        public static float DegreesToRadians(float degrees)
        {
            const float degToRad = (float)System.Math.PI / 180.0f;
            return degrees * degToRad;
        }

        /// <summary>
        /// Convert radians to degrees
        /// </summary>
        /// <param name="degrees">An angle in radians</param>
        /// <returns>The angle expressed in degrees</returns>
        public static float RadiansToDegrees(float radians)
        {
            const float radToDeg = 180.0f / (float)System.Math.PI;
            return radians * radToDeg;
        }
    }
}