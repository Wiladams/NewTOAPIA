using System;
using System.Collections.Generic;
using System.Text;

using NewTOAPIA;
using NewTOAPIA.GL;

namespace ShowIt
{
    /// <summary>
    /// The DisplaySurface represents a surface that can display an image.  
    /// It is meant to have behavior that is similar to a Monitor on a
    /// desktop in that it has a resolution, contrast, brightness, sharpness
    /// and the like.
    /// 
    /// The Display has an offscreen buffer, which matches the specified
    /// resolution.  All rendering occurs in the offscreen buffer, which is
    /// then displayed when rendering occurs.
    /// 
    /// This allows for various shaders to run while rendering to the offscreen
    /// buffer.  This is how the contrast, brightness and the like can be implemented.
    /// </summary>
    public class DisplaySurface : IHaveTexture
    {
        GraphicsInterface fGI;

        Resolution fResolution;
        IHaveTexture fImageSource;
        GLRenderTarget fRenderTarget;

        #region Constructors
        public DisplaySurface(GraphicsInterface gi, Resolution res, IHaveTexture imageSource)
        {
            fGI = gi;

            fResolution = res;
            fImageSource = imageSource;

            fRenderTarget = new GLRenderTarget(gi, res.Columns, res.Rows);
            fRenderTarget.Unbind();
        }
        #endregion

        #region Properties
        public IHaveTexture ImageSource
        {
            get { return fImageSource; }
            set
            {
                fImageSource = value;
            }
        }

        public Resolution Resolution
        {
            get { return fResolution; }
        }

        public GLTexture Texture
        {
            get 
            { 
                //return fRenderTarget.ColorBuffer;
                if (null != fImageSource)
                    return fImageSource.Texture;
                else
                    return null;
            }
        }
        #endregion

        /// <summary>
        /// This is called by whomever is managing the display surface.
        /// The effect will be to get the current texture from the
        /// image source and render it into the offscreen buffer
        /// applying any image filters that may be active.
        /// </summary>
        public void RefreshDisplayImage()
        {
            // Things that need to be saved/resored
            // Current Color
            // Viewport
            // Transformation matrix
            // Texture enable/disable
            // Make sure the texture is the same size as our frame buffer

            // Activate the render target so we can draw into the offscreen buffer
            fRenderTarget.Bind();

            fGI.Viewport(0, 0, fResolution.Columns, fResolution.Rows);


            fGI.Buffers.ColorBuffer.Color = ColorRGBA.Invisible;
            fGI.Buffers.ColorBuffer.Clear();
            fGI.Buffers.DepthBuffer.Clear();


            fGI.PushMatrix();
            {
                // Bind the shader
                //fShaderProgram.Bind();
                //ApplyUniformVariables();

                //// Setup the orthographic projection so we can draw a quad,
                //// one pixel for every texel in the texture object.
                fGI.MatrixMode(MatrixMode.Projection);
                fGI.LoadIdentity();
                fGI.Ortho(0, fResolution.Columns, 0, fResolution.Rows, -1, 1);

                fGI.MatrixMode(MatrixMode.Modelview);
                fGI.LoadIdentity();



                //// Make sure texturing is enabled, and the input
                //// texture is bound as the current texture.
                fGI.Features.Texturing2D.Enable();
                if ((null != fImageSource) && (null != fImageSource.Texture))
                    fImageSource.Texture.Bind();

                fGI.Drawing.Quads.Begin();
                {
                    // Left bottom
                    fGI.TexCoord(0.0f, 0.0f);
                    fGI.Vertex(0, 0, 0.0f);

                    // Right bottom
                    fGI.TexCoord(1.0f, 0.0f);
                    fGI.Vertex(fResolution.Columns, 0);

                    // Right top
                    fGI.TexCoord(1.0f, 1.0f);
                    fGI.Vertex(fResolution.Columns, fResolution.Rows);

                    // Left top
                    fGI.TexCoord(0.0f, 1.0f);
                    fGI.Vertex(0, fResolution.Rows);
                }
                fGI.Drawing.Quads.End();

                if ((null != fImageSource) && (null != fImageSource.Texture))
                    fImageSource.Texture.Unbind();
            }
            fGI.PopMatrix();

            //fShaderProgram.Unbind();
            fRenderTarget.Unbind();
        }
    }
}
