using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.GL
{
    public class GLSLFragmentShader : GLSLShader
    {
        public GLSLFragmentShader(GraphicsInterface gi, string sourceString)
            : base(gi, sourceString, ShaderType.Fragment)
        {
        }
    }
}
