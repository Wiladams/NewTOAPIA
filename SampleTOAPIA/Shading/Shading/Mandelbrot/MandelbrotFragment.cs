using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shading.Mandelbrot
{
    using NewTOAPIA.Graphics;
    using NewTOAPIA.Graphics.Processor;

    public class MandelbrotFragment : GPFragmentShader
    {
        // uniform
        public float MaxIterations;
        public float Zoom;
        public float Xcenter;
        public float Ycenter;
        public vec3 InnerColor;
        public vec3 OuterColor1;
        public vec3 OuterColor2;

        // in
        public vec3 Position;
        public float LightIntensity;
        
        // out
        public vec4 FragColor;

        public override void main()
        {
            float real = Position.x * Zoom + Xcenter;
            float imag = Position.y * Zoom + Ycenter;
            float Creal = real;     // change this line...
            float Cimag = imag;     // ...and this one to get a Julia Set

            // Julia Set
                //float Creal = -1.36f;
                //float Cimag = 0.11f;

            float r2 = 0;
            float iter;

            for (iter = 0.0f; iter < MaxIterations && r2 < 4.0f; ++iter)
            {
                float tempreal = real;
                real = (tempreal * tempreal) - (imag * imag) + Creal;
                imag = 2.0f * tempreal * imag + Cimag;
                r2 = (real * real) + (imag * imag);
            }

            // Base the color on the number of iterations
            vec3 color;

            if (r2 < 4.0f)
                color = InnerColor;
            else
                color = mix(OuterColor1, OuterColor2, fract(iter * 0.05f));

            color *= LightIntensity;

            FragColor = new vec4(color, 1.0f);
        }
    }
}
