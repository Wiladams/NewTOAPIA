using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.GL
{
    public class GLSLUniformVariable
    {
        GLSLShaderProgram fShaderProgram;
        string fName;               // ASCII string name used to look up variable
        int fLocation;              // Ordinal location of variable within the program
        UniformType fUniformType;   // What type of data
        int fSize;
        int fComponents;            // Number elements of the array that are taken up

        float[] _f;
        int[] _i;

        public GLSLUniformVariable(GLSLShaderProgram program, UniformType uType, int size, string name)
        {
            fShaderProgram = program;
            fName = name;
            fUniformType = uType;
            fSize = size;
            fComponents = GetComponentCountForType(fUniformType);
            fLocation = program.GetUniformLocation(name);

            InitializeSpace();
        }

        void InitializeSpace()
        {
            // Throw exception if location is not valid


            // Allocate memory for the right type
            switch (fUniformType)
            {
                case UniformType.Float:
                case UniformType.FloatVec2:
                case UniformType.FloatVec3:
                case UniformType.FloatVec4:
                case UniformType.FloatMat2:
                case UniformType.FloatMat3:
                case UniformType.FloatMat4:
                    {
                        _f = new float[Components];
                        //GI.GetUniform(fShaderProgram.ProgramID, aVar.Location, _f);
                    }
                    break;

                case UniformType.Int:
                case UniformType.IntVec2:
                case UniformType.IntVec3:
                case UniformType.IntVec4:
                    {
                        _i = new int[Components];
                        //GI.GetUniform(fShaderProgram.ProgramID, aVar.Location, _i);
                    }
                    break;

                case UniformType.Bool:
                case UniformType.BoolVec2:
                case UniformType.BoolVec3:
                case UniformType.BoolVec4:
                    {
                        _i = new int[Components];
                        //GI.GetUniform(fShaderProgram.ProgramID, aVar.Location, _i);
                    }
                    break;

                case UniformType.Sampler1D:
                case UniformType.Sampler2D:
                case UniformType.Sampler3D:
                case UniformType.SamplerCube:
                case UniformType.Sampler1DShadow:
                case UniformType.Sampler2DShadow:
                    {
                        _i = new int[Components];
                        //GI.GetUniform(fShaderProgram.ProgramID, aVar.Location, _i);
                    }
                    break;

            }
        }

        #region Properties
        public int Components
        {
            get { return fComponents; }
        }

        public float[] FloatValues
        {
            get { return _f; }
        }

        public int[] IntValues
        {
            get { return _i; }
        }

        public bool IsInternal
        {
            get
            {
                if (string.Compare(fName, 0, "gl_", 0, 3, true) == 0)
                    return true;

                // Another check that can be done is if the name begins with 'gl_'
                if ((-1 == fLocation) && (null != fName) && (fName != String.Empty))
                    return true;

                return false;
            }
        }

        public int Location
        {
            get { return fLocation; }
        }

        public string Name
        {
            get { return fName; }
        }

        public UniformType TypeOfUniform
        {
            get { return fUniformType; }
        }

        #endregion
        void ErrorCheckType(Type aType, int typeCount)
        {
            if (typeCount != Components)
                throw new ArgumentException("There are: " + Components + " Components for the " + fName + " Variable.");

            switch (TypeOfUniform)
            {
                case UniformType.Float:
                case UniformType.FloatVec2:
                case UniformType.FloatVec3:
                case UniformType.FloatVec4:
                case UniformType.FloatMat2:
                case UniformType.FloatMat3:
                case UniformType.FloatMat4:
                    {
                        if (aType != typeof(float))
                            throw new ArgumentException("Types don't match");
                    }
                    break;

                case UniformType.Int:
                case UniformType.IntVec2:
                case UniformType.IntVec3:
                case UniformType.IntVec4:
                    {
                        if (aType != typeof(int))
                            throw new ArgumentException("Types don't match");
                    }
                    break;

                case UniformType.Bool:
                case UniformType.BoolVec2:
                case UniformType.BoolVec3:
                case UniformType.BoolVec4:
                    {
                        if (aType != typeof(int))
                            throw new ArgumentException("Types don't match");
                    }
                    break;

                case UniformType.Sampler1D:
                case UniformType.Sampler2D:
                case UniformType.Sampler3D:
                case UniformType.SamplerCube:
                case UniformType.Sampler1DShadow:
                case UniformType.Sampler2DShadow:
                    {
                        if (aType != typeof(int))
                            throw new ArgumentException("Types don't match");
                    }
                    break;
            }
        }

        void UpdateProgramWithCurrentValue()
        {
            fShaderProgram.SetUniformValue(this);
        }

        public void Set(float v0)
        {
            ErrorCheckType(typeof(float),1);

            _f[0] = v0;

            UpdateProgramWithCurrentValue();
        }

        public void Set(float v0, float v1)
        {
            ErrorCheckType(typeof(float), 2);

            _f[0] = v0;
            _f[1] = v1;

            UpdateProgramWithCurrentValue();
        }

        public void Set(float v0, float v1, float v2)
        {
            ErrorCheckType(typeof(float), 3);

            _f[0] = v0;
            _f[1] = v1;
            _f[2] = v2;

            UpdateProgramWithCurrentValue();
        }

        public void Set(float v0, float v1, float v2, float v3)
        {
            ErrorCheckType(typeof(float), 4);

            _f[0] = v0;
            _f[1] = v1;
            _f[2] = v2;
            _f[3] = v3;

            UpdateProgramWithCurrentValue();
        }

        
        public void SetFloatValues(float[] values)
        {
            ErrorCheckType(typeof(float), Components);

            _f = values;

            UpdateProgramWithCurrentValue();
        }

        public void Set(int v0)
        {
            ErrorCheckType(typeof(int), 1);

            _i[0] = v0;

            UpdateProgramWithCurrentValue();
        }

        public void Set(int v0, int v1)
        {
            ErrorCheckType(typeof(int), 2);

            _i[0] = v0;
            _i[1] = v1;

            UpdateProgramWithCurrentValue();
        }

        public void Set(int v0, int v1, int v2)
        {
            ErrorCheckType(typeof(int), 3);

            _i[0] = v0;
            _i[1] = v1;
            _i[2] = v2;

            UpdateProgramWithCurrentValue();
        }

        public void SetIntValues(int[] values)
        {
            ErrorCheckType(typeof(int), Components);

            _i = values;

            UpdateProgramWithCurrentValue();
        }

        #region Operator Overloads

        #endregion

        #region Static Methods
        public static int GetComponentCountForType(UniformType aType)
        {
            int retValue = 0;

            switch (aType)
            {
                case UniformType.Float:
                case UniformType.Int:
                case UniformType.Bool:
                case UniformType.Sampler1D:
                case UniformType.Sampler2D:
                case UniformType.Sampler3D:
                case UniformType.SamplerCube:
                case UniformType.Sampler1DShadow:
                case UniformType.Sampler2DShadow:
                    retValue = 1;
                    break;

                case UniformType.FloatVec2:
                case UniformType.IntVec2:
                case UniformType.BoolVec2:
                    retValue = 2;
                    break;

                case UniformType.FloatVec3:
                case UniformType.IntVec3:
                case UniformType.BoolVec3:
                    retValue = 3;
                    break;

                case UniformType.FloatVec4:
                case UniformType.IntVec4:
                case UniformType.BoolVec4:
                    retValue = 4;
                    break;

                case UniformType.FloatMat2:
                    retValue = 4;
                    break;

                case UniformType.FloatMat3:
                    retValue = 9;
                    break;

                case UniformType.FloatMat4:
                    retValue = 16;
                    break;
            }

            return retValue;
        }
        #endregion
    }
}
