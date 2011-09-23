using System;
using System.Runtime.InteropServices;

namespace TOAPI.Types
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct SIZE
    {
        public int cx;
        public int cy;

        public SIZE(int width, int height)
        {
            cx = width;
            cy = height;
        }

        public int Width 
        {
            get { return cx; }
            set { cx = value; }
        }

        public int Height
        {
            get { return cy; }
            set { cy = value; }
        }

        //public static implicit operator System.Drawing.Size(SIZE s)
        //{
        //    return new System.Drawing.Size(s.Width, s.Height);
        //}

        //public static implicit operator SIZE(System.Drawing.Size s)
        //{
        //    return new SIZE(s.Width, s.Height);
        //}

    }
}
