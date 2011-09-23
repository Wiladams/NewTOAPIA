using System;

namespace NewTOAPIA.GL
{
    public class GLSLGeometryShader : GLSLShader
    {
        public GLSLGeometryShader(GraphicsInterface gi, string shaderSource)
            : base(gi, shaderSource, ShaderType.Geometry)
        {
        }
    }
}
