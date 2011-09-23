using System;
using System.Text;

using TOAPI.OpenGL;

namespace NewTOAPIA.GL
{
    public enum ShaderType
    {
        Geometry = gl.GEOMETRY_SHADER_ARB,
        Fragment = gl.GL_FRAGMENT_SHADER,
        Vertex = gl.GL_VERTEX_SHADER,
    }

    public class GLSLShader : IDisposable
    {
        private GraphicsInterface fGI;
        private GLSLShaderProgram fProgram;
        private readonly int fShaderID;

        #region Constructors
        /// <summary>
        /// This constructor is used when you're trying to put a wrapper around
        /// an already existing shader object.  You have the context, and you have
        /// the shaderID.  This is enough information to do useful things with 
        /// the shader.
        /// </summary>
        /// <param name="gi"></param>
        /// <param name="shaderID"></param>
        public GLSLShader(GLSLShaderProgram prg, int shaderID)
        {
            fShaderID = shaderID;
            fProgram = prg;
            if (null != prg)
                fGI = prg.GI;
        }

        public GLSLShader(GraphicsInterface gi, string shaderSourceCode, ShaderType sType)
        {
            fShaderID = -1;
            fGI = gi;

            if (shaderSourceCode.Length > 0)
            {
                fShaderID = gi.CreateShader(sType);

                String[] shaderSourceStringArray = new String[1];
                shaderSourceStringArray[0] = shaderSourceCode;

                gi.ShaderSource(fShaderID, 1, shaderSourceStringArray, null);
            }
        }
        #endregion

        #region Public Methods
        public bool Compile()
        {
            GI.CompileShader(ShaderID);
            
            return (CompileStatus == gl.GL_TRUE);
        }
        #endregion

        #region Properties
        public GraphicsInterface GI
        {
            get { return fGI; }
        }

        public int ShaderID
        {
            get { return fShaderID; }
        }

        public int CompileStatus
        {
            get
            {
                int[] status = new int[1];

                GI.GetShader(ShaderID, gl.GL_COMPILE_STATUS, status);
                
                return  status[0];
            }
        }

        public string InfoLog
        {
            get
            {
                // Find out how big the buffer needs to be to hold all the
                // source information
                int[] riparams = new int[1];
                fGI.GetShader(ShaderID, gl.GL_INFO_LOG_LENGTH, riparams);

                StringBuilder sb = new StringBuilder(riparams[0]);

                // Get the actual source information
                int actualLength = 0;
                fGI.GetShaderInfoLog(ShaderID, riparams[0], ref actualLength, sb);

                return sb.ToString();
            }
        }

        public ShaderType TypeOfShader
        {
            get
            {
                int[] riparams = new int[1];
                GI.GetShader(ShaderID, gl.GL_SHADER_TYPE, riparams);

                return (ShaderType)riparams[0];
            }
        }

        public string SourceString
        {
            get
            {
                // Find out how big the buffer needs to be to hold all the
                // source information
                int[] riparams = new int[1];
                fGI.GetShader(ShaderID, gl.GL_SHADER_SOURCE_LENGTH, riparams);
                
                StringBuilder sb = new StringBuilder(riparams[0]);

                // Get the actual source information
                int actualLength = 0;
                fGI.GetShaderSource(ShaderID, riparams[0] + 1, ref actualLength, sb);

                return sb.ToString();
            }
        }
        #endregion

        #region IDisposable
        public virtual void Dispose()
        {
            fGI.DeleteShader(ShaderID);
        }
        #endregion

        #region Static Methods
        public static GLSLShader CreateFromFile(string filename)
        {
            return null;
        }
        #endregion
    }
}
