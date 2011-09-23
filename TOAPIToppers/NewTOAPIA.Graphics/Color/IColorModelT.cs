using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.Graphics
{
    /// <summary>
    /// A ColorModel is a representation of a specific color system such as RGB,
    /// or CYMK, or HSL, etc.  This base interface allows the exchange of color 
    /// values between different color models by having all color models able 
    /// to represent their color as RGB tuples.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IColorModel
    {
        ColorRGB GetRGB();
        void SetRGB(ColorRGB aColor);
    }
}
