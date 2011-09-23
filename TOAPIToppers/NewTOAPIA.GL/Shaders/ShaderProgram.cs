using System;
using System.Text;
using System.Collections.Generic;

using TOAPI.OpenGL;

namespace NewTOAPIA.GL
{
    public class GLSLShaderProgram : GIObject, IDisposable, IBindable
    {
        private readonly int fProgramID;

        #region Constructors
        public GLSLShaderProgram(GraphicsInterface gi)
            : this(gi, gi.CreateProgram())
        {
        }

        public GLSLShaderProgram(GraphicsInterface gi, int programID)
            :base(gi)
        {
            fProgramID = programID;
        }        
        #endregion

        #region Properties
        public string InfoLog
        {
            get
            {
                // Find out how big the buffer needs to be to hold all the
                // source information
                int[] riparams = new int[1];
                GI.GetProgram(ProgramID, gl.GL_INFO_LOG_LENGTH, riparams);

                StringBuilder sb = new StringBuilder(riparams[0]);

                // Get the actual source information
                int actualLength = 0;
                GI.GetProgramInfoLog(ProgramID, riparams[0], ref actualLength, sb);

                return sb.ToString();
            }
        }

        public int LinkStatus
        {
            get
            {
                int[] riparams = new int[1];
                
                GI.GetProgram(ProgramID, gl.GL_LINK_STATUS, riparams);
                int retValue = riparams[0];
             
                return retValue;
            }
        }

        public bool IsProgram
        {
            get
            {
                return GI.IsProgram(ProgramID);
            }
        }

        public bool IsValid
        {
            get
            {
                // First do a validation
                Validate();

                // Now get the status of the validation
                int[] riparams = new int[1];

                GI.GetProgram(ProgramID, gl.GL_VALIDATE_STATUS, riparams);
                int retValue = riparams[0];

                return (retValue == gl.GL_TRUE);
            }
        }

        public int ProgramID
        {
            get { return fProgramID; }
        }

        public int NumberOfAttachedShaders
        {
            get
            {
                int[] riparams = new int[1];

                GI.GetProgram(ProgramID, gl.GL_ATTACHED_SHADERS, riparams);

                return riparams[0];
            }
        }

        public int NumberOfActiveUniformVariables
        {
            get
            {
                int[] riparams = new int[1];

                GI.GetProgram(ProgramID, gl.GL_ACTIVE_UNIFORMS, riparams);

                return riparams[0];
            }
        }
        #endregion

        #region Public Methods
        public void AttachShader(GLSLShader aShader)
        {
            GI.AttachShader(fProgramID, aShader.ShaderID);            
        }

        public GLSLUniformVariable GetUniformVariable(string varName)
        {
            int varCount = NumberOfActiveUniformVariables;

            int[] riparams = new int[1];
            GI.GetProgram(ProgramID, gl.GL_ACTIVE_UNIFORM_MAX_LENGTH, riparams);
            int maxNameLength = riparams[0];
            StringBuilder sb = new StringBuilder(maxNameLength + 1);
            GLSLUniformVariable returnVar = null;

            int actualLength = 0;
            int varSize = 0;
            int varType = 0;

            for (int ctr = 0; ctr < varCount; ctr++)
            {
                GI.GetActiveUniform(ProgramID, ctr, maxNameLength, ref actualLength, ref varSize, ref varType, sb);
                if (sb.ToString().CompareTo(varName) == 0)
                {
                    returnVar = new GLSLUniformVariable(this, (UniformType)varType, varSize, sb.ToString());
                    GetUniformValue(returnVar);
                    
                    return returnVar;
                }
            }

            return returnVar;
        }

        public List<GLSLUniformVariable> GetListOfActiveUniformVariables()
        {
            int varCount = NumberOfActiveUniformVariables;
            
            int[] riparams = new int[1];
            GI.GetProgram(ProgramID, gl.GL_ACTIVE_UNIFORM_MAX_LENGTH, riparams);
            int maxNameLength = riparams[0];
            StringBuilder sb = new StringBuilder(maxNameLength + 1);
            List<GLSLUniformVariable> varList = new List<GLSLUniformVariable>();

            int actualLength = 0;
            int varSize = 0;
            int varType = 0;

            for (int ctr = 0; ctr < varCount; ctr++)
            {
                GI.GetActiveUniform(ProgramID, ctr, maxNameLength, ref actualLength, ref varSize, ref varType, sb);
                GLSLUniformVariable aUniform = new GLSLUniformVariable(this, (UniformType)varType, varSize, sb.ToString());
                GetUniformValue(aUniform);
                varList.Add(aUniform);
            }

            return varList;
        }

        public List<GLSLShader> GetListOfAttachedShaders()
        {
            int maxCount = NumberOfAttachedShaders;
            int actualCount = 0;
            int[] shaderIDs = new int[maxCount];
            List<GLSLShader> shaderList = new List<GLSLShader>(maxCount);

            GI.GetAttachedShaders(ProgramID, maxCount, ref actualCount, shaderIDs);

            foreach (int i in shaderIDs)
            {
                GLSLShader aShader = new GLSLShader(this, i);
                shaderList.Add(aShader);
            }

            return shaderList;
        }

        public void Link()
        {
            GI.LinkProgram(ProgramID);
        }

        public void Validate()
        {
            GI.ValidateProgram(ProgramID);
        }

        public void GetUniformValue(GLSLUniformVariable aVar)
        {
            switch (aVar.TypeOfUniform)
            {
                case UniformType.Float:
                case UniformType.FloatVec2:
                case UniformType.FloatVec3:
                case UniformType.FloatVec4:
                case UniformType.FloatMat2:
                case UniformType.FloatMat3:
                case UniformType.FloatMat4:
                    {
                        float[] _f = new float[GLSLUniformVariable.GetComponentCountForType(aVar.TypeOfUniform)];
                        GI.GetUniform(ProgramID, aVar.Location, _f);
                        aVar.SetFloatValues(_f);
                    }
                    break;

                case UniformType.Int:
                case UniformType.IntVec2:
                case UniformType.IntVec3:
                case UniformType.IntVec4:
                    {
                        int[] _i = new int[GLSLUniformVariable.GetComponentCountForType(aVar.TypeOfUniform)];
                        GI.GetUniform(ProgramID, aVar.Location, _i);
                        aVar.SetIntValues(_i);
                    }
                    break;

                case UniformType.Bool:
                case UniformType.BoolVec2:
                case UniformType.BoolVec3:
                case UniformType.BoolVec4:
                    {
                        int[] _i = new int[GLSLUniformVariable.GetComponentCountForType(aVar.TypeOfUniform)];
                        GI.GetUniform(ProgramID, aVar.Location, _i);
                        aVar.SetIntValues(_i);
                    }
                    break;

                case UniformType.Sampler1D:
                case UniformType.Sampler2D:
                case UniformType.Sampler3D:
                case UniformType.SamplerCube:
                case UniformType.Sampler1DShadow:
                case UniformType.Sampler2DShadow:
                    {
                        int[] _i = new int[GLSLUniformVariable.GetComponentCountForType(aVar.TypeOfUniform)];
                        GI.GetUniform(ProgramID, aVar.Location, _i);
                        aVar.SetIntValues(_i);
                    }
                    break;
            }
        }

        public int GetUniformLocation(string parameterName)
        {
            int location = GI.GetUniformLocation(fProgramID, parameterName);

            return location;
        }

        public void SetUniformValue(GLSLUniformVariable aVar)
        {
            switch (aVar.TypeOfUniform)
            {
                case UniformType.Float:
                case UniformType.FloatVec2:
                case UniformType.FloatVec3:
                case UniformType.FloatVec4:
                case UniformType.FloatMat2:
                case UniformType.FloatMat3:
                case UniformType.FloatMat4:
                    {
                        GI.Uniform1fv(aVar.Location, aVar.Components, aVar.FloatValues);
                    }
                    break;

                case UniformType.Int:
                case UniformType.IntVec2:
                case UniformType.IntVec3:
                case UniformType.IntVec4:
                    {
                        GI.Uniform1iv(aVar.Location, aVar.Components, aVar.IntValues);
                    }
                    break;

                case UniformType.Bool:
                case UniformType.BoolVec2:
                case UniformType.BoolVec3:
                case UniformType.BoolVec4:
                    {
                        GI.Uniform1iv(aVar.Location, aVar.Components, aVar.IntValues);
                    }
                    break;

                case UniformType.Sampler1D:
                case UniformType.Sampler2D:
                case UniformType.Sampler3D:
                case UniformType.SamplerCube:
                case UniformType.Sampler1DShadow:
                case UniformType.Sampler2DShadow:
                    {
                        GI.Uniform1iv(aVar.Location, aVar.Components, aVar.IntValues);
                    }
                    break;

            }

        }

        #endregion

        #region IBindable
        public void Bind()
        {
            GI.UseProgram(ProgramID);
        }

        public void Unbind()
        {
            GI.UseProgram(0);
        }
        #endregion

        #region IDisposable
        public virtual void Dispose()
        {
            // detach shaders
            // dispose of shaders
            // Delete program object resource
        }

        #endregion

        #region Operator Overloads
        public GLSLUniformVariable this[string varName]
        {
            get
            {
                GLSLUniformVariable aVar = GetUniformVariable(varName);
                return aVar;
            }
        }

        #endregion

        #region Static Methods

        public static GLSLShaderProgram CreateUsingVertexAndFragmentStrings(GraphicsInterface gi, String vertexShaderSource, String fragmentShaderSource)
        {
            GLSLShaderProgram program = new GLSLShaderProgram(gi);

            if (vertexShaderSource != null)
            {
                GLSLShader vShader = new GLSLShader(gi, vertexShaderSource, ShaderType.Vertex);
                vShader.Compile();
                program.AttachShader(vShader);
            }

            if (fragmentShaderSource != null)
            {
                GLSLShader fShader = new GLSLShader(gi, fragmentShaderSource, ShaderType.Fragment);
                fShader.Compile();
                program.AttachShader(fShader);
                string log = fShader.InfoLog;
            }

            program.Link();

            return program;
        }

        public static GLSLShaderProgram GetCurrentProgram(GraphicsInterface gi)
        {
            int programID = gi.GetInteger(GetTarget.CurrentProgram);

            GLSLShaderProgram aProgram = new GLSLShaderProgram(gi, programID);

            return aProgram;
        }
        #endregion
    }
}
