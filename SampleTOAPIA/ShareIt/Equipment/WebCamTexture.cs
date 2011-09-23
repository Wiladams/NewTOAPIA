using System;
using System.Collections.Generic;
using System.Text;

using NewTOAPIA.Media;

using NewTOAPIA.GL;
using NewTOAPIA.Media.GL;

namespace ShowIt
{

    public class WebCamTexture : IHaveTexture
    {
        VideoTexture fWebcamTexture;

        public WebCamTexture(GraphicsInterface gi, int webCamDevice)
        {
            //fWebcamTexture = VideoTexture.CreateFromDeviceIndex(gi, webCamDevice, 320, 240, true);
            fWebcamTexture = VideoTexture.CreateFromDeviceIndex(gi, webCamDevice, true);
        }

        public GLTexture Texture
        {
            get { return fWebcamTexture; }
        }
    }
}
