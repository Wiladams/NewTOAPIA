using System;
using System.Collections.Generic;
using System.Text;

using NewTOAPIA.GL;

namespace ShowIt
{
    /// <summary>
    /// The projector class receives a stream of images from somewhere, and projects
    /// it onto a surface.  In the case of projection, it doesn't actually do anything
    /// actively other than make the received images available as a texture object.
    /// The ViewScreen is responsible for actually displaying the image.
    /// 
    /// This allows one projector to act as a receiver for multiple viewscreens.
    /// </summary>
    public interface IHaveTexture
    {
        GLTexture Texture { get;}
    }
}
