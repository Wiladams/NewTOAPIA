using System;
using System.Collections.Generic;
using System.Text;

using NewTOAPIA.GL;

namespace QuadVideo
{
    public class DCTProcessor
    {
        GLRenderTarget fRenderTarget;
        GLTextureRectangle fDCTOutputTexture;
        GLTextureRectangle fCosineBuffer;

        GLSLShaderProgram fForwardDCT;
        GraphicsInterface fGI;
        int fWidth;
        int fHeight;

        public DCTProcessor(GraphicsInterface gi, int width, int height)
        {
            fGI = gi;
            fWidth = width;
            fHeight = height;

            // Create the cosine buffer
            // Calculate the cosines
            // assign the values to the texture object
            fCosineBuffer = new GLTextureRectangle(gi, 8, 8, TextureInternalFormat.Luminance, TexturePixelFormat.Luminance, PixelType.Float);

            fRenderTarget = new GLRenderTarget(gi, width, height);
            fDCTOutputTexture = new GLTextureRectangle(gi, width, height, TextureInternalFormat.Rgba, TexturePixelFormat.Rgba, PixelType.Float);

            // We attach the texture 4 times so we can output to the same texture four times
            // in one shader pass using gl_FragData[0,1,2,3]
            fRenderTarget.AttachColorBuffer(fDCTOutputTexture, ColorBufferAttachPoint.Position0);
            fRenderTarget.AttachColorBuffer(fDCTOutputTexture, ColorBufferAttachPoint.Position1);
            fRenderTarget.AttachColorBuffer(fDCTOutputTexture, ColorBufferAttachPoint.Position2);
            fRenderTarget.AttachColorBuffer(fDCTOutputTexture, ColorBufferAttachPoint.Position3);
            fRenderTarget.Unbind();

            // Precalculate the basis functions (cosine tables)

        }

        #region Properties
        public GLTexture DCTResults
        {
            get { return fDCTOutputTexture; }
        }
        #endregion

        public GLTexture ForwardDCT(GLTexture sourceImage)
        {
            if (null == sourceImage)
                return null;

            // First, create an array of points that divide
            // the texture into 8x8 chunks

            // We require 4 passes of drawing to actually get
            // all the output values, so perform each of those
            // passes, changing the quadrant each time.
            // Use a quadrant specific shader program for each pass
            // pass1
            // pass2
            // pass3
            // pass4

            // At this point, the full DCT should be contained in the output
            // image, so we can just return it.
            return fDCTOutputTexture;
        }

        #region Helper Functions
        public double[,] CalculateBasis(int N)
        {
            double[,] cosBuffer = new double[N, N];

            //
            // compute the table of basis functions and
            // store the result in the cosBuffer
            //
            for (int m = 0; m < N; ++m)
            {
                for (int n = 0; n < N; ++n)
                {
                    double result;
                    if (n == 0)
                    {
                        result =
                           Math.Sqrt(1.0 / N) *
                           Math.Cos(((2 * m + 1) * n * Math.PI) / (2.0 * N));
                    }
                    else
                    {
                        result =
                           Math.Sqrt(2.0 / N) *
                           Math.Cos(((2 * m + 1) * n * Math.PI) / (2.0 * N));
                    }
                    cosBuffer[m, n] = result;
                }
            }

            return cosBuffer;
        }
        #endregion
    }
}
