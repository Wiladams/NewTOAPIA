using System;

using TOAPI.OpenGL;

namespace NewTOAPIA.GL
{
    public class GLViewer
    {
        #region WriteViewportToImageFile
        public static bool WriteViewportToImageFile(GraphicsInterface gi, String filePathAndName, System.Drawing.Imaging.ImageFormat imageFormat)
        {
            int[] viewportXYWidthHeight = new int[4]; // 0:x, 1:y, 2:width, 3:height
            gl.glGetIntegerv(gl.GL_VIEWPORT, viewportXYWidthHeight);

            int viewportWidth = viewportXYWidthHeight[2];
            int viewportHeight = viewportXYWidthHeight[3];

            // Protect against absurd cases
            if (viewportWidth < 0) { return (false); }
            if (viewportHeight < 0) { return (false); }
            if (viewportWidth < 1) { return (false); }
            if (viewportHeight < 1) { return (false); }


            // Allocate byte buffer to contain RGBA pixel data.
            byte[] rgbaData = new byte[viewportHeight * (viewportWidth * 4)];

            // Set how pixels will be transferred from OpenGL buffer to a client memory buffer; i.e., "stored".
            gl.glPixelStorei(gl.GL_PACK_ALIGNMENT, 1);
            gl.glPixelStorei(gl.GL_PACK_ROW_LENGTH, 0);
            gl.glPixelStorei(gl.GL_PACK_SKIP_ROWS, 0);
            gl.glPixelStorei(gl.GL_PACK_SKIP_PIXELS, 0);


            // Cache the current read buffer setting, change the read buffer setting to the front buffer,
            // then read the front buffer, then restore the read buffer setting.
            int[] previousBufferID = new int[1];
            gl.glGetIntegerv(gl.GL_READ_BUFFER, previousBufferID);
            gl.glReadBuffer(gl.GL_FRONT);
            gl.glReadPixels(0, 0, viewportWidth, viewportHeight, gl.GL_RGBA, gl.GL_UNSIGNED_BYTE, rgbaData);
            gl.glReadBuffer(previousBufferID[0]);


            //bool result = GLImage.WriteRGBABufferToImageFile(rgbaData, viewportWidth, viewportHeight, filePathAndName, imageFormat);

            //if (false == result)
            //{
            //    return (false);
            //}

            return (true);
        }
        #endregion
    }
}
