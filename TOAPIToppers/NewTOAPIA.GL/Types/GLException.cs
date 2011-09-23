using System;
using TOAPI.OpenGL;

namespace NewTOAPIA.GL
{
    public class GLException : Exception
    {
        GLErrorCode fErrorCode;
        GraphicsInterface fGI;

        public GLException(GraphicsInterface gi, GLErrorCode errorCode)
            : base()
        {
            fGI = gi;
            fErrorCode = errorCode;
        }

        public GLErrorCode ErrorCode
        {
            get { return fErrorCode; }
        }

        public string ErrorString
        {
            get
            {
                // Get the error string that corresponds to the error code
                return fGI.Glu.ErrorString(fErrorCode);
            }
        }
    }
}
