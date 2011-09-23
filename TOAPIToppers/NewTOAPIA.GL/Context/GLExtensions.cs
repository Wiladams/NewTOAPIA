using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;  // Just for StringBuilder

using TOAPI.OpenGL;


namespace NewTOAPIA.GL
{
    public class GLExtensions
    {
        Hashtable fExtensions;
        private GLInfoStrings fInfoStrings;

        public GLExtensions(GraphicsInterface gi)
        {
            fInfoStrings = new GLInfoStrings(gi);
            fExtensions = new Hashtable();

            foreach (string s in fInfoStrings.ExtensionList)
                fExtensions.Add(s, true);
        }

        public bool IsExtensionAvailable(string extension)
        {
            if (fExtensions.ContainsKey(extension))
            {
                return true;
            }

            return false;
        }

        #region Utility Calls

        #region LoadExtension
        public Delegate LoadExtension(String functionName, Type delegateType, ref bool foundFunction)
        {
            foundFunction = false;


            IntPtr intPtr = IntPtr.Zero;

            // OPENGL extension function (via wglGetProcAddress())
            intPtr = wgl.wglGetProcAddress(functionName);
            if (IntPtr.Zero != intPtr)
            {
                foundFunction = true;
                return (Marshal.GetDelegateForFunctionPointer(intPtr, delegateType));
            }

            // Failed to find a function
            return (null);
        }
        #endregion

        void LoadFunction()
        {
            // OPENGL32.DLL
            //intPtr = Kernel32.GetProcAddress(this.mhModuleGL, functionName);
            //if (IntPtr.Zero != intPtr)
            //{
            //    foundFunction = true;
            //    return (System.Runtime.InteropServices.Marshal.GetDelegateForFunctionPointer(intPtr, delegateType));
            //}

            // GLU32.DLL
            //intPtr = GR.Kernel32_GetProcAddress(this.mhModuleGLU, functionName);
            //if (IntPtr.Zero != intPtr)
            //{
            //    foundFunction = true;
            //    return (System.Runtime.InteropServices.Marshal.GetDelegateForFunctionPointer(intPtr, delegateType));
            //}

            // GDI32.DLL
            //intPtr = GR.Kernel32_GetProcAddress(this.mhModuleGDI, functionName);
            //if (IntPtr.Zero != intPtr)
            //{
            //    foundFunction = true;
            //    return (System.Runtime.InteropServices.Marshal.GetDelegateForFunctionPointer(intPtr, delegateType));
            //}

        }


        #endregion Utility Calls

    }

    public partial class GraphicsInterface
    {

        #region void LoadExtensions()
        #region LoadVBO
        void LoadVBO()
        {
            if (fExtensions.IsExtensionAvailable("GL_ARB_vertex_buffer_object"))
            {
                bool result = false;

                // create delegates for the GL extensions
                glGenBuffers = Extensions.LoadExtension("glGenBuffersARB", typeof(DglGenBuffers), ref result) as DglGenBuffers;
                glDeleteBuffers_IaI = Extensions.LoadExtension("glDeleteBuffers", typeof(DglDeleteBuffers_IaI), ref result) as DglDeleteBuffers_IaI;
                glBindBuffer = Extensions.LoadExtension("glBindBufferARB", typeof(DglBindBuffer), ref result) as DglBindBuffer;
                
                // Write Buffer Data
                glBufferData = Extensions.LoadExtension("glBufferDataARB", typeof(DglBufferData), ref result) as DglBufferData;
                glBufferData_IIPI = Extensions.LoadExtension("glBufferDataARB", typeof(DglBufferData_IIPI), ref result) as DglBufferData_IIPI;
                glBufferData_IIaFI = Extensions.LoadExtension("glBufferDataARB", typeof(DglBufferData_IIaFI), ref result) as DglBufferData_IIaFI;
                glBufferData_IIaBI = Extensions.LoadExtension("glBufferDataARB", typeof(DglBufferData_IIaBI), ref result) as DglBufferData_IIaBI;
                glBufferData_IIaII = Extensions.LoadExtension("glBufferDataARB", typeof(DglBufferData_IIaII), ref result) as DglBufferData_IIaII;

                // Buffer Sub Data
                glBufferSubData = Extensions.LoadExtension("glBufferSubDataARB", typeof(DglBufferSubData), ref result) as DglBufferSubData;
                glBufferSubData_IIIP = Extensions.LoadExtension("glBufferSubDataARB", typeof(DglBufferSubData_IIIP), ref result) as DglBufferSubData_IIIP;
                glBufferSubData_IIIaF = Extensions.LoadExtension("glBufferSubDataARB", typeof(DglBufferSubData_IIIaF), ref result) as DglBufferSubData_IIIaF;
                glBufferSubData_IIIaB = Extensions.LoadExtension("glBufferSubDataARB", typeof(DglBufferSubData_IIIaB), ref result) as DglBufferSubData_IIIaB;
                glBufferSubData_IIIaI = Extensions.LoadExtension("glBufferSubDataARB", typeof(DglBufferSubData_IIIaI), ref result) as DglBufferSubData_IIIaI;

                glGetBufferParameteriv_IIaI = Extensions.LoadExtension("glGetBufferParameterivARB", typeof(DglGetBufferParameteriv_IIaI), ref result) as DglGetBufferParameteriv_IIaI;
                
                glGetBufferPointerv = Extensions.LoadExtension("glGetBufferPointerv", typeof(DglGetBufferPointerv_IIrP), ref result) as DglGetBufferPointerv_IIrP;
                glMapBufferARB = Extensions.LoadExtension("glMapBufferARB", typeof(DglMapBufferARB_II), ref result) as DglMapBufferARB_II;
                glUnmapBuffer_I = Extensions.LoadExtension("glUnmapBuffer", typeof(DglUnmapBuffer_I), ref result) as DglUnmapBuffer_I;
            }

        }
        #endregion

        void LoadExtensions()
        {
            bool result = false;

            // Core functions
            glBlendEquation = Extensions.LoadExtension("glBlendEquation", typeof(DglBlendEquation_I), ref result) as DglBlendEquation_I;

            #region Version 1.2
            #region GL_ARB_imaging
            glHistogram = Extensions.LoadExtension("glHistogram", typeof(DglHistogram_IIIB), ref result) as DglHistogram_IIIB;

            glGetHistogram_P = Extensions.LoadExtension("glGetHistogram", typeof(DglGetHistogram_IBIIP), ref result) as DglGetHistogram_IBIIP;
            glGetHistogram_aI = Extensions.LoadExtension("glGetHistogram", typeof(DglGetHistogram_IBIIaI), ref result) as DglGetHistogram_IBIIaI;
            glGetHistogram_aF = Extensions.LoadExtension("glGetHistogram", typeof(DglGetHistogram_IBIIaF), ref result) as DglGetHistogram_IBIIaF;

            glColorTable_P = Extensions.LoadExtension("glColorTable", typeof(DglColorTable_IIIIIP), ref result) as DglColorTable_IIIIIP;
            glColorTable_aI = Extensions.LoadExtension("glColorTable", typeof(DglColorTable_IIIIIaI), ref result) as DglColorTable_IIIIIaI;
            glColorTable_aB = Extensions.LoadExtension("glColorTable", typeof(DglColorTable_IIIIIaB), ref result) as DglColorTable_IIIIIaB;
            glColorTable_aF = Extensions.LoadExtension("glColorTable", typeof(DglColorTable_IIIIIaF), ref result) as DglColorTable_IIIIIaF;
            
            glConvolutionFilter2D_aF = Extensions.LoadExtension("glConvolutionFilter2D", typeof(DglConvolutionFilter2D_IIIIIIaF), ref result) as DglConvolutionFilter2D_IIIIIIaF;
            #endregion
            #endregion

            // VBO - GL_ARB_vertex_buffer_object
            LoadVBO();
        }
        #endregion

        #region Extension Methods
        #region Blend Equation
        public DglBlendEquation_I glBlendEquation;

        public void BlendEquation(BlendEquation equation)
        {
            glBlendEquation((int)equation);
        }
        #endregion

        #region Version 1.2
        #region GL_ARB_imaging
        
        // Convolution Filter
        #region ConvolutionFilter2D
        public DglConvolutionFilter2D_IIIIIIaF glConvolutionFilter2D_aF;
        public DglConvolutionFilter2D_IIIIIIP glConvolutionFilter2D_P;

        public void ConvolutionFilter2D(int internalformat, int width, int height, int format, int itype, IntPtr data)
        {
            if (null != glConvolutionFilter2D_P)
                glConvolutionFilter2D_P(gl.GL_CONVOLUTION_2D, internalformat, width, height, format, itype, data);
        }

        public void ConvolutionFilter2D(int internalformat, int width, int height, int format, int itype, float[] data)
        {
            if (null != glConvolutionFilter2D_aF)
                glConvolutionFilter2D_aF(gl.GL_CONVOLUTION_2D, internalformat, width, height, format, itype, data);
        }
        #endregion

        // Color Table
        #region ColorTable
        public DglColorTable_IIIIIP glColorTable_P;
        public DglColorTable_IIIIIaI glColorTable_aI;
        public DglColorTable_IIIIIaB glColorTable_aB;
        public DglColorTable_IIIIIaF glColorTable_aF;

        public void ColorTable(int target, int internalformat, int width, int format, int itype, IntPtr table)
        {
            if (null != glColorTable_P)
                glColorTable_P(target, internalformat, width, format, itype, table);
        }

        public void ColorTable(int target, int internalformat, int width, int format, int itype, int[] table)
        {
            if (null != glColorTable_aI)
                glColorTable_aI(target, internalformat, width, format, itype, table);
        }

        public void ColorTable(int target, int internalformat, int width, int format, int itype, float[] table)
        {
            if (null != glColorTable_aF)
                glColorTable_aF(target, internalformat, width, format, itype, table);
        }

        public void ColorTable(int target, int internalformat, int width, int format, int itype, byte[] table)
        {
            if (null != glColorTable_aB)
                glColorTable_aB(target, internalformat, width, format, itype, table);
        }
        #endregion

        // Histogram
        #region Histogram
        public DglHistogram_IIIB glHistogram;

        public void Histogram(int target, int width, int internalformat, byte sink)
        {
            if (null != glHistogram)
                glHistogram(target, width, internalformat, sink);
        }

        public DglGetHistogram_IBIIP glGetHistogram_P;
        public DglGetHistogram_IBIIaI glGetHistogram_aI;
        public DglGetHistogram_IBIIaF glGetHistogram_aF;

        public void GetHistogram(int target, byte reset, int format, int itype, IntPtr values)
        {
            if (null != glGetHistogram_P)
                glGetHistogram_P(target, reset, format, itype, values);
        }

        public void GetHistogram(int target, byte reset, int format, int itype, int[] values)
        {
            if (null != glGetHistogram_aI)
                glGetHistogram_aI(target, reset, format, itype, values);
        }

        public void GetHistogram(int target, byte reset, int format, int itype, float[] values)
        {
            if (null != glGetHistogram_aF)
                glGetHistogram_aF(target, reset, format, itype, values);
        }
        #endregion

        #endregion GL_ARB_imaging
        #endregion

        #region Version 1.4
        #region WindowPos
        public DglWindowPos2i_II glWindowPos;
        
        public void WindowPos(int x, int y)
        {
            bool result = false;

            if (null == glWindowPos)
                glWindowPos = Extensions.LoadExtension("glWindowPos2i", typeof(DglWindowPos2i_II), ref result) as DglWindowPos2i_II;

            if (null != glWindowPos)
                glWindowPos(x, y);
        }
        #endregion
        #endregion

        #region GL_ARB_fragment_program
        /// <summary>
        /// Application-defined fragment programs may be specified in the same 
        /// low-level language as GL_ARB_vertex_program, replacing the standard 
        /// fixed-function vertex texturing, fog, and color sum operations.
        /// </summary>
        #endregion

        #region GL_ARB_fragment_program_shadow
        #endregion

        #region GL_ARB_vertex_program
        #endregion

        #region GL_ARB_shading_language_100
        /// <summary>
        /// The presence of this extension string indicates 
        /// that programs written in version 1 of the Shading 
        /// Language are accepted by OpenGL.
        /// </summary>
        #region GL_ARB_shader_objects - GLSLShader Programs
        #region GL_ARB_geometry_shader4
        
        #endregion

        #region GL_ARB_fragment_shader
        #endregion

        #region GL_ARB_vertex_shader
        #endregion

        #region GLSLShader Management
        private DglCreateShader glCreateShader;
        private DglShaderSource_IIaSaI glShaderSource_IIaSaI;
        private DglCompileShader_I glCompileShader_I;
        private DglDeleteShader_I glDeleteShader_I;

        private DglGetShaderiv_IIaI glGetShaderiv_IIaI;
        private DglGetShaderInfoLog_IIrIS glGetShaderInfoLog_IIrIS;
        private DglGetShaderSource_IIrIS glGetShaderSource_IIrIS;

        public int CreateShader(ShaderType aType)
        {
            bool result = false;

            if (null == glCreateShader)
                glCreateShader = Extensions.LoadExtension("glCreateShader", typeof(DglCreateShader), ref result) as DglCreateShader;

            int retValue = -1;
            if (null != glCreateShader)
                retValue = glCreateShader((int)aType);

            return retValue;
        }

        public void CompileShader(int shaderID)
        {
            bool result = false;

            if (null == glCompileShader_I)
                glCompileShader_I = Extensions.LoadExtension("glCompileShader", typeof(DglCompileShader_I), ref result) as DglCompileShader_I;

            if (null != glCompileShader_I)
                glCompileShader_I(shaderID);

            CheckException();
        }

        public void DeleteShader(int shaderID)
        {
            bool result = false;

            if (null == glDeleteShader_I)
                glDeleteShader_I = Extensions.LoadExtension("glDeleteShader", typeof(DglDeleteShader_I), ref result) as DglDeleteShader_I;

            if (null != glDeleteShader_I)
                glDeleteShader_I(shaderID);
        }

        public void ShaderSource(int shader, int count, String[] textLines, int[] lengths)
        {
            bool result = false;

            if (null == glShaderSource_IIaSaI)
                glShaderSource_IIaSaI = Extensions.LoadExtension("glShaderSource", typeof(DglShaderSource_IIaSaI), ref result) as DglShaderSource_IIaSaI;

            if (null != glCreateShader)
                glShaderSource_IIaSaI(shader, count, textLines, lengths);

            CheckException();
        }

        public void GetShaderInfoLog(int shaderID, int bufferSize, ref int length, StringBuilder buffer)
        {
            bool result = false;

            if (null == glGetShaderInfoLog_IIrIS)
                glGetShaderInfoLog_IIrIS = Extensions.LoadExtension("glGetShaderInfoLog", typeof(DglGetShaderInfoLog_IIrIS), ref result) as DglGetShaderInfoLog_IIrIS;

            if (null != glGetShaderInfoLog_IIrIS)
                glGetShaderInfoLog_IIrIS(shaderID, bufferSize, ref length, buffer);

        }

        public void GetShaderSource(int shaderID, int bufferSize, ref int length, StringBuilder source)
        {
            bool result = false;

            if (null == glGetShaderSource_IIrIS)
                        glGetShaderSource_IIrIS = Extensions.LoadExtension("glGetShaderSource", typeof(DglGetShaderSource_IIrIS), ref result) as DglGetShaderSource_IIrIS;

        
            if (null != glGetShaderSource_IIrIS)
                glGetShaderSource_IIrIS(shaderID, bufferSize, ref length, source);
        }

        public void GetShader(int shader, int pname, int[] riparams)
        {
            bool result = false;

            if (null == glGetShaderiv_IIaI)
                glGetShaderiv_IIaI = Extensions.LoadExtension("glGetShaderiv", typeof(DglGetShaderiv_IIaI), ref result) as DglGetShaderiv_IIaI;

            if (null != glGetShaderiv_IIaI)
                glGetShaderiv_IIaI(shader, pname, riparams);

            CheckException();
        }
        #endregion

        #region Program Management
        private DglAttachShader_II glAttachShader_II;
        private DglGetAttachedShaders_IIrIaI glGetAttachedShaders_IIIaI;
        private DglCreateProgram_V glCreateProgram_V;
        private DglLinkProgram_I glLinkProgram_I;
        private DglUseProgram_I glUseProgram_I;
        private DglValidateProgram_I glValidateProgram_I;

        private DglGetProgramiv_IIaI glGetProgramiv_IIaI;
        private DglGetProgramInfoLog_IIrIS glGetProgramInfoLog_IIrIS;
        private DglIsProgram_I glIsProgram_I;

        public int CreateProgram()
        {
            bool result = false;

            if (null == glCreateProgram_V)
                glCreateProgram_V = Extensions.LoadExtension("glCreateProgram", typeof(DglCreateProgram_V), ref result) as DglCreateProgram_V;

            int retValue = -1;
            if (null != glCreateProgram_V)
                retValue = glCreateProgram_V();

            return retValue;
        }

        public void AttachShader(int programID, int shaderID)
        {
            bool result = false;

            if (null == glAttachShader_II)
                glAttachShader_II = Extensions.LoadExtension("glAttachShader", typeof(DglAttachShader_II), ref result) as DglAttachShader_II;

            if (null != glAttachShader_II)
                glAttachShader_II(programID, shaderID);
        }

        public void GetAttachedShaders(int programID, int maxCount, ref int actualCount, int[] shaderIDs)
        {
            bool result = false;

            if (null == glGetAttachedShaders_IIIaI)
                glGetAttachedShaders_IIIaI = Extensions.LoadExtension("glGetAttachedShaders", typeof(DglGetAttachedShaders_IIrIaI), ref result) as DglGetAttachedShaders_IIrIaI;

            if (null != glGetAttachedShaders_IIIaI)
                glGetAttachedShaders_IIIaI(programID, maxCount, ref actualCount, shaderIDs);
        }

        public void LinkProgram(int programID)
        {
            bool result = false;

            if (null == glLinkProgram_I)
                glLinkProgram_I = Extensions.LoadExtension("glLinkProgram", typeof(DglLinkProgram_I), ref result) as DglLinkProgram_I;

            if (null != glLinkProgram_I)
                glLinkProgram_I(programID);

            CheckException();
        }

        public void UseProgram(int program)
        {
            bool result = false;

            if (null == glUseProgram_I)
                glUseProgram_I = Extensions.LoadExtension("glUseProgram", typeof(DglUseProgram_I), ref result) as DglUseProgram_I;

            if (null != glUseProgram_I)
                glUseProgram_I(program);

            CheckException();
        }

        public void ValidateProgram(int programID)
        {
            bool result = false;

            if (null == glValidateProgram_I)
                glValidateProgram_I = Extensions.LoadExtension("glValidateProgram", typeof(DglValidateProgram_I), ref result) as DglValidateProgram_I;

            if (null != glValidateProgram_I)
                glValidateProgram_I(programID);

            CheckException();
        }

        public bool IsProgram(int programID)
        {
            bool result = false;

            if (null == glIsProgram_I)
                glIsProgram_I = Extensions.LoadExtension("glIsProgram", typeof(DglIsProgram_I), ref result) as DglIsProgram_I;

            bool retValue = false;
            if (null != glIsProgram_I)
                retValue = (glIsProgram_I(programID)==gl.GL_TRUE);

            return retValue;
        }

        public void GetProgram(int program, int pname, int[] riparams)
        {
            bool result = false;

            if (null == glGetProgramiv_IIaI)
                glGetProgramiv_IIaI = Extensions.LoadExtension("glGetProgramiv", typeof(DglGetProgramiv_IIaI), ref result) as DglGetProgramiv_IIaI;

            if (null != glGetProgramiv_IIaI)
                glGetProgramiv_IIaI(program, pname, riparams);

            CheckException();
        }

        public void GetProgramInfoLog(int programID, int bufferSize, ref int length, StringBuilder buffer)
        {
            bool result = false;

            if (null == glGetProgramInfoLog_IIrIS)
                glGetProgramInfoLog_IIrIS = Extensions.LoadExtension("glGetProgramInfoLog", typeof(DglGetProgramInfoLog_IIrIS), ref result) as DglGetProgramInfoLog_IIrIS;

            if (null != glGetProgramInfoLog_IIrIS)
                glGetProgramInfoLog_IIrIS(programID, bufferSize, ref length, buffer);

            CheckException();
        }


        #endregion

        #region Setting/Getting Uniform Variables
        private DglGetActiveUniform_IIIrIrIrIS glGetActiveUniform;
        private DglGetUniformLocation_IS glGetUniformLocation_IS;
        private DglGetUniformfv_IIaF glGetUniformfv_IIaF;
        private DglGetUniformiv_IIaI glGetUniformiv_IIaI;

        // Setting a uniform value
        private DglUniform1f_IF glUniform1f_IF;
        private DglUniform2f_IFF glUniform2f_IFF;
        private DglUniform3f_IFFF glUniform3f_IFFF;
        private DglUniform4f_IFFFF glUniform4f_IFFFF;

        private DglUniform1fv_IIaF glUniform1fv_IIaF;
        //private DglUniform2fv_IIaF glUniform2fv_IIaF;
        //private DglUniform3fv_IIaF glUniform3fv_IIaF;
        //private DglUniform4fv_IIaF glUniform4fv_IIaF;

        private DglUniform1i_II glUniform1i_II;
        private DglUniform2i_III glUniform2i_III;
        private DglUniform3i_IIII glUniform3i_IIII;
        private DglUniform4i_IIIII glUniform4i_IIII;

        private DglUniform1iv_IIaI glUniform1iv_IIaI;
        //private DglUniform2iv_IIaI glUniform2iv_IIaI;
        //private DglUniform3iv_IIaI glUniform3iv_IIaI;
        //private DglUniform4iv_IIaI glUniform4iv_IIaI;

        //private DglUniformMatrix2fv_IIBaF glUniformMatrix2fv_IIBaF;
        private DglUniformMatrix3fv_IIBaF glUniformMatrix3fv_IIBaF;
        //private DglUniformMatrix4fv_IIBaF glUniformMatrix4fv_IIBaF;

        public void Uniform1f(int location, float v1)
        {
            bool result = false;

            if (null == glUniform1f_IF)
                    glUniform1f_IF = Extensions.LoadExtension("glUniform1f", typeof(DglUniform1f_IF), ref result) as DglUniform1f_IF;

            if (null != glUniform1f_IF)
                glUniform1f_IF(location, v1);

            CheckException();
        }

        public void Uniform1fv(int location, int count, float[] values)
        {
            bool result = false;

            if (null == glUniform1fv_IIaF)
                glUniform1fv_IIaF = Extensions.LoadExtension("glUniform1fv", typeof(DglUniform1fv_IIaF), ref result) as DglUniform1fv_IIaF;

            if (null != glUniform1fv_IIaF)
                glUniform1fv_IIaF(location, count, values);

            CheckException();
        }

        public void Uniform1i(int location, int v1)
        {
            bool result = false;

            if (null == glUniform1i_II)
                glUniform1i_II = Extensions.LoadExtension("glUniform1i", typeof(DglUniform1i_II), ref result) as DglUniform1i_II;

            if (null != glUniform1i_II)
                glUniform1i_II(location, v1);

            CheckException();
        }

        public void Uniform1iv(int location, int count, int[] values)
        {
            bool result = false;

            if (null == glUniform1iv_IIaI)
                glUniform1iv_IIaI = Extensions.LoadExtension("glUniform1iv", typeof(DglUniform1iv_IIaI), ref result) as DglUniform1iv_IIaI;

            if (null != glUniform1iv_IIaI)
                glUniform1iv_IIaI(location, count, values);

            CheckException();
        }

        public void Uniform2f(int location, float v1, float v2)
        {
            bool result = false;

            if (null == glUniform2f_IFF)
                    glUniform2f_IFF = Extensions.LoadExtension("glUniform2f", typeof(DglUniform2f_IFF), ref result) as DglUniform2f_IFF;

            if (null != glUniform2f_IFF)
                glUniform2f_IFF(location, v1, v2);

            CheckException();
        }

        public void Uniform3f(int location, float v1, float v2, float v3)
        {
            bool result = false;

            if (null == glUniform3f_IFFF)
                    glUniform3f_IFFF = Extensions.LoadExtension("glUniform3f", typeof(DglUniform3f_IFFF), ref result) as DglUniform3f_IFFF;

            if (null != glUniform3f_IFFF)
                glUniform3f_IFFF(location, v1, v2, v3);
        
            CheckException();
        }


        public void UniformMatrix3(int location, int count, bool transpose, float[] values)
        {
            bool result = false;

            if (null == glUniformMatrix3fv_IIBaF)
                glUniformMatrix3fv_IIBaF = Extensions.LoadExtension("glUniformMatrix3fv", typeof(DglUniformMatrix3fv_IIBaF), ref result) as DglUniformMatrix3fv_IIBaF;

            if (null != glUniformMatrix3fv_IIBaF)
                glUniformMatrix3fv_IIBaF(location, count, transpose?(byte)1:(byte)0, values);
        }

        public void Uniform4f(int location, float v1, float v2, float v3, float v4)
        {
            bool result = false;

            if (null == glUniform4f_IFFFF)
                glUniform4f_IFFFF = Extensions.LoadExtension("glUniform4f", typeof(DglUniform4f_IFFFF), ref result) as DglUniform4f_IFFFF;

            if (null != glUniform4f_IFFFF)
                glUniform4f_IFFFF(location, v1, v2, v3, v4);

            CheckException();
        }

        public void GetActiveUniform(int program, int index, int bufSize, ref int length, ref int size, ref int ritype, StringBuilder name)
        {
            bool result = false;

            if (null == glGetActiveUniform)
                glGetActiveUniform = Extensions.LoadExtension("glGetActiveUniform", typeof(DglGetActiveUniform_IIIrIrIrIS), ref result) as DglGetActiveUniform_IIIrIrIrIS;

            if (null != glGetActiveUniform)
                glGetActiveUniform(program, index, bufSize, out length, out size, out ritype, name);

            CheckException();
        }

        public int GetUniformLocation(int programID, string varName)
        {
            bool result = false;

            if (null == glGetUniformLocation_IS)
                glGetUniformLocation_IS = Extensions.LoadExtension("glGetUniformLocation", typeof(DglGetUniformLocation_IS), ref result) as DglGetUniformLocation_IS;

            int retValue = -1;
            if (null != glGetUniformLocation_IS)
                retValue = glGetUniformLocation_IS(programID, varName);

            CheckException();

            return retValue;
        }

        public void GetUniform(int programID, int location, float[] values)
        {
            bool result = false;

            if (null == glGetUniformfv_IIaF)
                glGetUniformfv_IIaF = Extensions.LoadExtension("glGetUniformfv", typeof(DglGetUniformfv_IIaF), ref result) as DglGetUniformfv_IIaF;

            if (null != glGetUniformfv_IIaF)
                glGetUniformfv_IIaF(programID, location, values);

        }

        public void GetUniform(int programID, int location, int[] values)
        {
            bool result = false;

            if (null == glGetUniformiv_IIaI)
                glGetUniformiv_IIaI = Extensions.LoadExtension("glGetUniformiv", typeof(DglGetUniformiv_IIaI), ref result) as DglGetUniformiv_IIaI;

            if (null != glGetUniformiv_IIaI)
                glGetUniformiv_IIaI(programID, location, values);

        }
        #endregion
        #endregion

        #endregion

        #region GL_ARB_multitexture
        public DglActiveTexture_I glActiveTexture;

        public void ActiveTexture(TextureUnitID unitID)
        {
            bool result = false ;

            if (null == glActiveTexture)
                glActiveTexture = Extensions.LoadExtension("glActiveTexture", typeof(DglActiveTexture_I), ref result) as DglActiveTexture_I;

            if (null != glActiveTexture)
                glActiveTexture((int)unitID);
        }
        #endregion

        #region GL_ARB_texture_non_power_of_two
        #endregion

        #region GL_ARB_texture_rectangle
        #endregion

        #region GL_ARB_pixel_buffer_object
        #endregion

        #region GL_ARB_vertex_buffer_object - VBO
        private DglGenBuffers glGenBuffers;
        //private DglGenBuffers_IaI glGenBuffers_IaI;
        private DglBindBuffer glBindBuffer;

        public DglBufferData glBufferData;
        public DglBufferData_IIPI glBufferData_IIPI;
        public DglBufferData_IIaFI glBufferData_IIaFI;
        public DglBufferData_IIaBI glBufferData_IIaBI;
        public DglBufferData_IIaII glBufferData_IIaII;

        public DglBufferSubData glBufferSubData;
        public DglBufferSubData_IIIP glBufferSubData_IIIP;
        public DglBufferSubData_IIIaB glBufferSubData_IIIaB;
        public DglBufferSubData_IIIaF glBufferSubData_IIIaF;
        public DglBufferSubData_IIIaI glBufferSubData_IIIaI;

        private DglDeleteBuffers_IaI glDeleteBuffers_IaI;
        public DglGetBufferParameteriv_IIaI glGetBufferParameteriv_IIaI;
        public DglGetBufferPointerv_IIrP glGetBufferPointerv;
        public DglMapBufferARB_II glMapBufferARB;
        private DglUnmapBuffer_I glUnmapBuffer_I;

        #region Buffer Object Methods
        #region GenBufferID
        public void GenBufferID([Out] out int buffers)
        {
            unsafe
            {
                fixed (int* buffers_ptr = &buffers)
                {
                    glGenBuffers(1, (UInt32*)buffers_ptr);
                    buffers = *buffers_ptr;
                }
            }
        }
        #endregion

        #region BindBuffer
        public void BindBuffer(BufferTarget target, int bufferID)
        {
            glBindBuffer((int)target, bufferID);
        }
        #endregion

        #region BufferData...
        public void BufferData(BufferTarget target, int size, IntPtr data, BufferUsage usage)
        {
                glBufferData_IIPI((int)target, size, data, (int)usage);
        }

        //public void BufferData(BufferTarget target, int size, byte[] data, BufferUsage usage)
        //{
        //}

        //public void BufferData(BufferTarget target, int size, float[] data, BufferUsage usage)
        //{
        //}

        //public void BufferData(BufferTarget target, int size, int[] data, BufferUsage usage)
        //{
        //}

        //public void BufferData(BufferTarget target, int size, [In, Out] object data, BufferUsage usage)
        //{
        //    unsafe
        //    {
        //        GCHandle data_ptr = GCHandle.Alloc(data, GCHandleType.Pinned);
        //        try
        //        {
        //            glBufferData((int)target, size, (IntPtr)data_ptr.AddrOfPinnedObject(), (int)usage);
        //        }
        //        finally
        //        {
        //            data_ptr.Free();
        //        }
        //    }
        //}
        #endregion

        #region BufferSubData
        public void BufferSubData(BufferTarget target, int offset, int size, IntPtr data)
        {
            glBufferSubData_IIIP((int)target, offset, size, data);
        }

        public void BufferSubData(BufferTarget target, IntPtr offset, IntPtr size, [In, Out] object data)
        {
            unsafe
            {
                GCHandle data_ptr = GCHandle.Alloc(data, GCHandleType.Pinned);
                try
                {
                    glBufferSubData((int)target, (IntPtr)offset, (IntPtr)size, (IntPtr)data_ptr.AddrOfPinnedObject());
                }
                finally
                {
                    data_ptr.Free();
                }
            }
        }

        public void BufferSubData(BufferTarget target, int offset, int size, float[] data)
        {
            glBufferSubData_IIIaF((int)target, offset, size, data);
        }

        public void BufferSubData(BufferTarget target, int offset, int size, byte[] data)
        {
            glBufferSubData_IIIaB((int)target, offset, size, data);
        }

        public void BufferSubData(BufferTarget target, int offset, int size, int[] data)
        {
            glBufferSubData_IIIaI((int)target, offset, size, data);
        }

        #endregion

        public void DeleteBuffers(int numBuffers, int[] buffers)
        {
            glDeleteBuffers_IaI(numBuffers, buffers);
        }

        public void GetBufferMappedPointer(BufferTarget target, ref IntPtr pointer)
        {
            glGetBufferPointerv((int)target, (int)BufferParameter.BufferMapPointer, ref pointer);
        }

        public void GetBufferParameteriv(BufferTarget target, BufferParameter pname, int[] riparams)
        {
            glGetBufferParameteriv_IIaI((int)target, (int)pname, riparams);
        }

        public IntPtr MapBuffer(BufferTarget target, BufferAccess access)
        {
            IntPtr retValue = glMapBufferARB((int)target, (int)access);
            return retValue;
        }

        public byte UnmapBuffer(BufferTarget target)
        {
            byte retValue = glUnmapBuffer_I((int)target);
            return retValue;
        }

        #endregion

        #endregion

        #region GL_EXT_framebuffer_object
        #region Framebuffer calls
        public DglGenFramebuffersEXT_IP glGenFramebuffersEXT_IP;
        public DglGenFramebuffersEXT_IaI glGenFramebuffersEXT_IaI;
        public DglBindFramebufferEXT_II glBindFramebufferEXT_II;
        public DglDeleteFramebuffersEXT_IP glDeleteFramebuffers_IP;
        public DglDeleteFramebuffersEXT_IaI glDeleteFramebuffers_IaI;
        public DglCheckFramebufferStatusEXT_I glCheckFramebufferStatus_I;

        public void GenFramebuffers(int num, IntPtr bufferIDs)
        {
            bool result=false;

            if (null == glGenFramebuffersEXT_IP)
                glGenFramebuffersEXT_IP = Extensions.LoadExtension("glGenFramebuffersEXT", typeof(DglGenFramebuffersEXT_IP), ref result) as DglGenFramebuffersEXT_IP;

            if (null != glGenFramebuffersEXT_IP)
                glGenFramebuffersEXT_IP(num, bufferIDs);
        }

        public void GenFramebuffers(int num, int[] bufferIDs)
        {
            bool result = false;

            if (null == glGenFramebuffersEXT_IaI)
                glGenFramebuffersEXT_IaI = Extensions.LoadExtension("glGenFramebuffersEXT", typeof(DglGenFramebuffersEXT_IaI), ref result) as DglGenFramebuffersEXT_IaI;

            if (null != glGenFramebuffersEXT_IaI)
                glGenFramebuffersEXT_IaI(num, bufferIDs);
        }

        public void BindFramebuffer(int target, int bufferID)
        {
            bool result = false;

            if (null == glBindFramebufferEXT_II)
                glBindFramebufferEXT_II = Extensions.LoadExtension("glBindFramebufferEXT", typeof(DglBindFramebufferEXT_II), ref result) as DglBindFramebufferEXT_II;
            
            if (null != glBindFramebufferEXT_II)
                glBindFramebufferEXT_II(target, bufferID);
        }

        public int CheckFramebufferStatus(int target)
        {
            bool result = false;
            int retValue = -1;

            if (null == glCheckFramebufferStatus_I)
                glCheckFramebufferStatus_I = Extensions.LoadExtension("glCheckFramebufferStatusEXT", typeof(DglCheckFramebufferStatusEXT_I), ref result) as DglCheckFramebufferStatusEXT_I;

            if (null != glCheckFramebufferStatus_I)
                retValue = glCheckFramebufferStatus_I(target);

            return retValue;
        }

        public void DeleteFramebuffers(int num, IntPtr bufferIDs)
        {
            bool result = false;

            if (null == glDeleteFramebuffers_IP)
                glDeleteFramebuffers_IP = Extensions.LoadExtension("glDeleteFramebuffersEXT", typeof(DglDeleteFramebuffersEXT_IP), ref result) as DglDeleteFramebuffersEXT_IP;

            if (null != glDeleteFramebuffers_IP)
                glDeleteFramebuffers_IP(num, bufferIDs);

        }

        public void DeleteFramebuffers(int num, int[] bufferIDs)
        {
            bool result = false;

            if (null == glDeleteRenderbuffersEXT_IaI)
                glDeleteRenderbuffersEXT_IaI = Extensions.LoadExtension("glDeleteRenderbuffersEXT", typeof(DglDeleteRenderbuffersEXT_IaI), ref result) as DglDeleteRenderbuffersEXT_IaI;

            if (null != glDeleteRenderbuffersEXT_IaI)
                glDeleteRenderbuffersEXT_IaI(num, bufferIDs);
        }
        #endregion

        #region Renderbuffer calls
        public DglGenRenderbuffersEXT_IP glGenRenderbuffersEXT_IP;
        public DglGenRenderbuffersEXT_IaI glGenRenderbuffersEXT_IaI;
        public DglBindRenderbufferEXT_II glBindRenderbufferEXT;
        public DglRenderbufferStorageEXT_IIII glRenderbufferStorageEXT;
        public DglDeleteRenderbuffersEXT_IP glDeleteRenderbuffersEXT_IP;
        public DglDeleteRenderbuffersEXT_IaI glDeleteRenderbuffersEXT_IaI;

        public void GenRenderbuffers(int num, IntPtr bufferIDs)
        {
            bool result = false;

            if (null == glGenRenderbuffersEXT_IP)
                glGenRenderbuffersEXT_IP = Extensions.LoadExtension("glGenRenderbuffersEXT", typeof(DglGenRenderbuffersEXT_IP), ref result) as DglGenRenderbuffersEXT_IP;

            if (null != glGenRenderbuffersEXT_IP)
                glGenRenderbuffersEXT_IP(num, bufferIDs);
        }

        public void GenRenderbuffers(int num, int[] bufferIDs)
        {
            bool result = false;

            if (null == glGenRenderbuffersEXT_IaI)
                glGenRenderbuffersEXT_IaI = Extensions.LoadExtension("glGenRenderbuffersEXT", typeof(DglGenRenderbuffersEXT_IaI), ref result) as DglGenRenderbuffersEXT_IaI;

            if (null != glGenRenderbuffersEXT_IaI)
                glGenRenderbuffersEXT_IaI(num, bufferIDs);
        }

        public void BindRenderbuffer(int target, int bufferID)
        {
            bool result = false;

            if (null == glBindRenderbufferEXT)
                glBindRenderbufferEXT = Extensions.LoadExtension("glBindRenderbufferEXT", typeof(DglBindRenderbufferEXT_II), ref result) as DglBindRenderbufferEXT_II;

            if (null != glBindRenderbufferEXT)
                glBindRenderbufferEXT(target, bufferID);
        }

        public void RenderbufferStorage(int target, int internalformat, int width, int height)
        {
            bool result = false;

            if (null == glRenderbufferStorageEXT)
                glRenderbufferStorageEXT = Extensions.LoadExtension("glRenderbufferStorageEXT", typeof(DglRenderbufferStorageEXT_IIII), ref result) as DglRenderbufferStorageEXT_IIII;

            if (null != glRenderbufferStorageEXT)
                glRenderbufferStorageEXT(target, internalformat, width, height);
        }

        public void DeleteRenderbuffers(int num, IntPtr bufferIDs)
        {
            bool result = false;

            if (null == glDeleteRenderbuffersEXT_IP)
                glDeleteRenderbuffersEXT_IP = Extensions.LoadExtension("glDeleteRenderbuffersEXT", typeof(DglDeleteRenderbuffersEXT_IP), ref result) as DglDeleteRenderbuffersEXT_IP;

            if (null != glDeleteRenderbuffersEXT_IP)
                glDeleteRenderbuffersEXT_IP(num, bufferIDs);
        }

        public void DeleteRenderbuffers(int num, int[] bufferIDs)
        {
            bool result = false;

            if (null == glDeleteRenderbuffersEXT_IaI)
                glDeleteRenderbuffersEXT_IaI = Extensions.LoadExtension("glDeleteRenderbuffersEXT", typeof(DglDeleteRenderbuffersEXT_IaI), ref result) as DglDeleteRenderbuffersEXT_IaI;

            if (null != glDeleteRenderbuffersEXT_IaI)
                glDeleteRenderbuffersEXT_IaI(num, bufferIDs);
        }
        #endregion

        #region Attaching Images
        public DglFramebufferTexture1DEXT_IIIII glFramebufferTexture1DEXT;
        public DglFramebufferTexture2DEXT_IIIII glFramebufferTexture2DEXT;
        public DglFramebufferTexture3DEXT_IIIIII glFramebufferTexture3DEXT;
        public DglFramebufferRenderbufferEXT_IIII glFramebufferRenderbufferEXT;

        public void FramebufferTexture1D(int target, ColorBufferAttachPoint attachment, int textarget, int textureID, int level)
        {
            bool result = false;

            if (null == glFramebufferTexture1DEXT)
                glFramebufferTexture1DEXT = Extensions.LoadExtension("glFramebufferTexture1DEXT", typeof(DglFramebufferTexture1DEXT_IIIII), ref result) as DglFramebufferTexture1DEXT_IIIII;

            if (null != glFramebufferTexture1DEXT)
                glFramebufferTexture1DEXT(target, (int)attachment, textarget, textureID, level);
        }

        public void FramebufferTexture2D(int target, ColorBufferAttachPoint attachPoint, int textarget, int textureID, int level)
        {
            bool result = false;

            if (null == glFramebufferTexture2DEXT)
                glFramebufferTexture2DEXT = Extensions.LoadExtension("glFramebufferTexture2DEXT", typeof(DglFramebufferTexture2DEXT_IIIII), ref result) as DglFramebufferTexture2DEXT_IIIII;

            if (null != glFramebufferTexture2DEXT)
                glFramebufferTexture2DEXT(target, (int)attachPoint, textarget, textureID, level);
        }

        public void FramebufferTexture3D(int target, ColorBufferAttachPoint attachment, int textarget, int textureID, int level, int zoffset)
        {
            bool result = false;

            if (null == glFramebufferTexture3DEXT)
                glFramebufferTexture3DEXT = Extensions.LoadExtension("glFramebufferTexture3DEXT", typeof(DglFramebufferTexture3DEXT_IIIIII), ref result) as DglFramebufferTexture3DEXT_IIIIII;

            if (null != glFramebufferTexture3DEXT)
                glFramebufferTexture3DEXT(target, (int)attachment, textarget, textureID, level, zoffset);
        }

        public void FramebufferRenderbuffer(int target, int attachment, int renderbuffertarget, int renderbufferID)
        {
            bool result = false;

            if (null == glFramebufferRenderbufferEXT)
                glFramebufferRenderbufferEXT = Extensions.LoadExtension("glFramebufferRenderbufferEXT", typeof(DglFramebufferRenderbufferEXT_IIII), ref result) as DglFramebufferRenderbufferEXT_IIII;

            if (null != glFramebufferRenderbufferEXT)
                glFramebufferRenderbufferEXT(target, attachment, renderbuffertarget, renderbufferID);
        }
        #endregion

        #region Draw Buffers
        public DglDrawBuffers_IP glDrawBuffers_IP;
        public DglDrawBuffers_IaI glDrawBuffers_IaI;

        public void DrawBuffers(int num, IntPtr buffers)
        {
            bool result = false;

            if (null == glDrawBuffers_IP)
                glDrawBuffers_IP = Extensions.LoadExtension("glDrawBuffers", typeof(DglDrawBuffers_IP), ref result) as DglDrawBuffers_IP;

            if (null != glDrawBuffers_IP)
                glDrawBuffers_IP(num, buffers);
        }

        public void DrawBuffers(int num, int[] buffers)
        {
            bool result = false;

            if (null == glDrawBuffers_IaI)
                glDrawBuffers_IaI = Extensions.LoadExtension("glDrawBuffers", typeof(DglDrawBuffers_IaI), ref result) as DglDrawBuffers_IaI;

            if (null != glDrawBuffers_IaI)
                glDrawBuffers_IaI(num, buffers);
        }
        #endregion
        #endregion

        #endregion

    }
}
