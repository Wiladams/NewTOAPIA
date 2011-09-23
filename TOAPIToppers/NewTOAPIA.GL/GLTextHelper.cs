using System;

using TOAPI.GDI32;
using TOAPI.OpenGL;
using TOAPI.Types;

namespace NewTOAPIA.GL
{
    public class GLTextHelper
    {
        #region DrawTextUsingPolygonFont
        public static void DrawTextUsingPolygonFont
        (GraphicsInterface gr, uint fontOpenGLDisplayListBaseIndex,
            int clientWidth, int clientHeight, int fontHeightPixels,
            int textX, int textY, String text)
        {
            int stringLength;
            int stringCharacterIndex;


            // Change rendering conditions
            gl.glDisable(gl.GL_DEPTH_TEST);
            gl.glDisable(gl.GL_CULL_FACE);


            // Preserve current matrices, and switch to an orthographic view, and 
            //   do scaling and translation as necessary.
            gl.glMatrixMode(gl.GL_PROJECTION);
            gl.glPushMatrix();
            gl.glMatrixMode(gl.GL_MODELVIEW);
            gl.glPushMatrix();


            gl.glMatrixMode(gl.GL_PROJECTION);
            gl.glLoadIdentity();
            gl.glOrtho
            (
                (double)0,
                (double)(clientWidth - 1),
                (double)0,
                (double)(clientHeight - 1),
                -1.0,
                1.0
            );

            gl.glMatrixMode(gl.GL_MODELVIEW);
            gl.glLoadIdentity();


            gl.glRasterPos2i(textX, textY); // Only affects by BITMAP fonts:


            // Only affects POLYGON fonts:
            gl.glTranslatef((float)(textX), (float)(textY), 0.0f);
            gl.glScalef((float)fontHeightPixels, (float)fontHeightPixels, 1.0f);

            /*
            gl.glBlendFunc(gl.GL_SRC_ALPHA, gl.GL_ONE_MINUS_SRC_ALPHA);
            gl.glEnable(gl.GL_BLEND);
            //gl.glEnable(gl.GL_POINT_SMOOTH);
            //gl.glHint(gl.GL_POINT_SMOOTH_HINT, gl.GL_NICEST);
            //gl.glEnable(gl.GL_LINE_SMOOTH);
            //gl.glHint(gl.GL_LINE_SMOOTH_HINT, gl.GL_NICEST);
            gl.glEnable(gl.GL_POLYGON_SMOOTH);
            gl.glHint(gl.GL_POLYGON_SMOOTH_HINT, gl.GL_NICEST);
            */

            // Call a display list for each character to be rendered.  The ASCII code
            // is used as the display list number (of the 256 display lists for this
            // font), which is added to the base number of the set of display list
            // indices.

            stringLength = text.Length;
            for (stringCharacterIndex = 0; stringCharacterIndex < stringLength; stringCharacterIndex++)
            {
                uint ASCIICharacter = (text[stringCharacterIndex]);
                gl.glCallList(fontOpenGLDisplayListBaseIndex + ASCIICharacter);
            }

            // gl.glDisable(gl.GL_POLYGON_SMOOTH);


            // Restore original matrices.
            gl.glMatrixMode(gl.GL_MODELVIEW);
            gl.glPopMatrix();
            gl.glMatrixMode(gl.GL_PROJECTION);
            gl.glPopMatrix();


            // Restore rendering conditions
            gl.glFrontFace(gl.GL_CCW); // MUST DO AFTER USING wglUseFontOutlines LISTS!!!
            gl.glEnable(gl.GL_DEPTH_TEST);
            gl.glEnable(gl.GL_CULL_FACE);
        }
        #endregion

        #region CreateBitmapFont
        public static void CreateBitmapFont
        (
            GraphicsInterface gr,
            IntPtr hdc,  // [in]
            String fontName, // [in] "Verdana", "Arial", "Courier New", "Symbol", "Wingdings", "Wingdings 3"
            int fontCellHeightInPoints, // [in] Cell height of font (in points)
            ref uint bitmapFontOpenGLDisplayListBase  // [in]
        )
        {
            bitmapFontOpenGLDisplayListBase = gl.glGenLists(256);

            // IT IT AN ABSOLUTE NECESSITY TO SELECT A FONT IN TO THE DC FOR THE 
            // wglUseFontOutlines() METHOD TO SUCCEED WITHOUT A MYSTERIOUS 
            // ERROR 126: ERROR_MOD_NOT_FOUND : The specified module could not be found.
            // THAT ERROR CODE, OF COURSE, IS MISLEADING.

            IntPtr hfont;
            System.Drawing.Font font = null;

            bool useGDICreateFontDirectly = false;

            if (useGDICreateFontDirectly)
            {
                // height ; negative means CHARACTER height; positive means CELL height
                hfont = TOAPI.GDI32.GDI32.CreateFont((short)fontCellHeightInPoints,
                  0, 0, 0,
                  GDI32.FW_NORMAL, // weight 
                  0, 0, 0,
                  GDI32.ANSI_CHARSET, // char set 
                  GDI32.OUT_TT_PRECIS, // output precision 
                  GDI32.CLIP_DEFAULT_PRECIS, // clip precision 
                  GDI32.ANTIALIASED_QUALITY, // quality
                  GDI32.FF_DONTCARE | GDI32.DEFAULT_PITCH, // pitch and family 
                  fontName);
            }
            else
            {
                font =
                  new System.Drawing.Font(
                    fontName, // family: "Verdana", "Arial", "Courier New", "Symbol", "Wingdings", "Wingdings 3"
                    (float)fontCellHeightInPoints,            // emSize: Font size (see 4th parameter for units)
                    System.Drawing.FontStyle.Regular, // style
                    System.Drawing.GraphicsUnit.Point, // unit
                    ((System.Byte)(0)) // GDI character set
                    );

                hfont = font.ToHfont();
            }

            GDI32.SelectObject(hdc, hfont);


            bool result;

            // Use wglUseFontBitmapsW() instead if this doesn't work!!!
            result = wgl.wglUseFontBitmapsW(hdc, 0, 255, bitmapFontOpenGLDisplayListBase);

            if (!result)
            {
                String message = "wglUseFontBitmaps() error";
                Console.WriteLine(message);
                //MessageBox.Show(null, message, "Error", MessageBoxButtons.OK);
            }
        }
        #endregion

        #region CreatePolygonFont
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gr"></param>
        /// <param name="hdc">[in]</param>
        /// <param name="fontName">[in] "Verdana", "Arial", "Courier New", "Symbol", "Wingdings", "Wingdings 3"</param>
        /// <param name="GLDisplayListBase">[out]</param>
        public static void CreatePolygonFont(GraphicsInterface gr, IntPtr hdc, String fontName, ref uint GLDisplayListBase)
        {
            GLDisplayListBase = gl.glGenLists(256);

            // A font must be selected into the device context before creating
            // a font for OpenGL usage.  If this is not done, you will get ERROR: 126: ERROOR_MOD_NOT_FOUND
            IntPtr hfont = GDI32.CreateFont(
              -12, // height ; negative means CHARACTER height; positive means CELL height
              0, // width 
              0, // escapement
              0, // orientation 
              GDI32.FW_NORMAL, // weight 
              0, // italic 
              0, // underline 
              0, // strikeout 
              GDI32.ANSI_CHARSET, // char set 
              GDI32.OUT_TT_PRECIS, // output precision 
              GDI32.CLIP_DEFAULT_PRECIS, // clip precision 
              GDI32.ANTIALIASED_QUALITY, // quality
              GDI32.FF_DONTCARE | GDI32.DEFAULT_PITCH, // pitch and family 
              fontName // font name
              );

            GDI32.SelectObject(hdc, hfont);

            GLYPHMETRICSFLOAT[] agmf = new GLYPHMETRICSFLOAT[256];

            bool result;

            result = wgl.wglUseFontOutlinesW(hdc, 0, 255, GLDisplayListBase,
                0.0f, 0.0f, wgl.WGL_FONT_POLYGONS, agmf);

            if (!result)
            {
                // WAA - Throw exception
                String message = "wglUseFontOutlines() error";
                Console.WriteLine(message);
                //MessageBox.Show(null, message, "Error", MessageBoxButtons.OK);
            }
        }
        #endregion

    }
}
