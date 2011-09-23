using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.GL
{
    public class GLSLVertexShader : GLSLShader
    {
        public GLSLVertexShader(GraphicsInterface gi, string shaderSource)
            : base(gi, shaderSource, ShaderType.Vertex)
        {
        }
    }
}
