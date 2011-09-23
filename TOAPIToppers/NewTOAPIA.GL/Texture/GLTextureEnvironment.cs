using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.GL
{
    using NewTOAPIA.Drawing;
    using NewTOAPIA.Graphics;

    public class GLTextureEnvironment
    {
        GLState fState;

        public GLTextureEnvironment(GraphicsInterface gi)
        {
            fState = gi.State;
        }

        public ColorRGBA Color
        {
            get
            {
                return fState.TextureEnvColor;
            }

            set
            {
                fState.TextureEnvColor = value;
            }
        }

        TextureEnvModeParam Mode
        {
            get
            {
                return fState.TextureEnvMode;
            }

            set
            {
                fState.TextureEnvMode = value;
            }
        }
    }
}
