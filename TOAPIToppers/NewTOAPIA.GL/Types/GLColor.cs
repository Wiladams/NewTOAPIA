

namespace NewTOAPIA.GL
{
    public struct GLColor
    {
        float r, g, b, a;

        #region Static Colors
        public static GLColor Empty = new GLColor();

        public static GLColor Invisible = new GLColor(0, 0, 0, 0);

        public static GLColor Black = new GLColor(0, 0, 0, 255);
        public static GLColor White = new GLColor(1.0f, 1.0f, 1.0f, 1.0f);

        public static GLColor Red = new GLColor(255, 0, 0, 255);
        public static GLColor Green = new GLColor(0, 255, 0, 255);
        public static GLColor Blue = new GLColor(0, 0, 255, 255);
        public static GLColor Magenta = new GLColor(1.0f, 0, 1.0f, 1.0f);
        public static GLColor Cyan = new GLColor(0, 1.0f, 1.0f, 1.0f);
        public static GLColor Yellow = new GLColor(1.0f, 1.0f, 0.0f, 1.0f);

        public static GLColor MediumRed = new GLColor(0.5f, 0.0f, 0.0f, 1.0f);
        public static GLColor MediumGreen = new GLColor(0.0f, 0.5f, 0, 1.0f);
        public static GLColor MediumBlue = new GLColor(0, 0, 0.5f, 1.0f);
        public static GLColor MediumCyan = new GLColor(0, 0.5f, 0.5f, 1.0f);
        public static GLColor MediumYellow = new GLColor(0.5f, 0.5f, 0.0f, 1.0f);
        public static GLColor MediumMagenta = new GLColor(0.5f, 0, 0.5f, 1.0f);
        #endregion

        public GLColor(float[] colors)
        {
            // if the array does not contain at least 4 values
            // throw an invalid argument exception

            r = colors[0];
            g = colors[1];
            b = colors[2];
            a = colors[3];
        }

        public GLColor(float red, float green, float blue, float alpha)
        {
            r = red;
            g = green;
            b = blue;
            a = alpha;
        }

        public GLColor(float red, float green, float blue)
        {
            r = red;
            g = green;
            b = blue;
            a = 1.0f;
        }

        public GLColor(byte red, byte green, byte blue, byte alpha)
        {
            r = red/255;
            g = green/255;
            b = blue/255;
            a = alpha/255;
        }

        public void Set(float red, float green, float blue, float alpha)
        {
            r = red;
            g = green;
            b = blue;
            a = alpha;
        }

        public float R { get { return r; } }
        public float G { get { return g; } }
        public float B { get { return b; } }
        public float A { get { return a; } }

        #region Operator Overloads

        public static explicit operator float[](GLColor aColor)
        {
            return new float[] { aColor.r, aColor.g, aColor.b, aColor.a };
        }

        #endregion

        public override string ToString()
        {
            return string.Format("<color r='{0}', g='{1}', b='{2}', a='{3}'/>",
                r,g,b,a);
        }
    }
}
