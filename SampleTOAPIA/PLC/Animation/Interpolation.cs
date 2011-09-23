using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

using NewTOAPIA;

namespace NewTOAPIA
{
    public class Interpolator
    {
        /// <summary>
        /// Return a value that is a linear interpolation between the
        /// beginning value, and the ending value.
        /// </summary>
        /// <param name="starting">The initial vector position.</param>
        /// <param name="ending">The final vector position.</param>
        /// <param name="completion">The amount of units along in the interpolation.
        /// this value should range from 0.0 (returns initial value) and 1.0 (returns final value).</param>
        /// <returns></returns>
        public static float2 LinearInterpolation(float2 starting, float2 ending, double completion)
        {
            // A vector has two components
            // a distance, and a direction

            float interpX = (float)((ending.x - starting.x) * completion);
            float interpY = (float)((ending.y - starting.y) * completion);

            float2 retValue = new float2(interpX, interpY);

            return retValue;
        }



        /// <summary>
        /// Return a value that is a linear interpolation between the
        /// beginning value, and the ending value.
        /// </summary>
        /// <param name="starting">The initial vector position.</param>
        /// <param name="ending">The final vector position.</param>
        /// <param name="completion">The amount of units along in the interpolation.
        /// this value should range from 0.0 (returns initial value) and 1.0 (returns final value).</param>
        /// <returns></returns>
        public static Point LinearInterpolation(Point starting, Point ending, double completion)
        {
            // A vector has two components
            // a distance, and a direction

            int interpX = starting.X + (int)Math.Floor(((ending.X - starting.X) * completion) + 0.5);
            int interpY = starting.Y + (int)Math.Floor(((ending.Y - starting.Y) * completion) + 0.5);

            Point retValue = new Point(interpX, interpY);

            return retValue;
        }

        /// <summary>
        /// Return a value that is a linear interpolation between the
        /// beginning value, and the ending value.
        /// </summary>
        /// <param name="starting">The initial vector position.</param>
        /// <param name="ending">The final vector position.</param>
        /// <param name="completion">The amount of units along in the interpolation.
        /// this value should range from 0.0 (returns initial value) and 1.0 (returns final value).</param>
        /// <returns></returns>
        public static int LinearInterpolation(int starting, int ending, double completion)
        {
            // A vector has two components
            // a distance, and a direction

            int interp = starting + (int)Math.Floor(((ending - starting) * completion) + 0.5);

            return interp;
        }

    }
}
